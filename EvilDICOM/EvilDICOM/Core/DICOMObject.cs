﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Selection;
using DateTime = System.DateTime;

namespace EvilDICOM.Core
{
    /// <summary>
    /// The DICOM object is a container for DICOM elements. It contains methods for finding elements easily from within the
    /// structure.
    /// </summary>
    public class DICOMObject
    {
        /// <summary>
        /// The _elements
        /// </summary>
        private List<IDICOMElement> _elements = new List<IDICOMElement>();

        /// <summary>
        /// Constructor no parameters
        /// </summary>
        public DICOMObject()
        {
            _elements = new List<IDICOMElement>();
        }

        /// <summary>
        /// Constructor with elements
        /// </summary>
        /// <param name="elements">a list of elements to be included in the object</param>
        public DICOMObject(List<IDICOMElement> elements)
        {
            _elements = elements;
            _elements.SortByTagID();
        }

        /// <summary>
        /// The list of first level DICOM elements inside this DICOM object
        /// </summary>
        /// <value>The elements.</value>
        public List<IDICOMElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        /// The list of all DICOM elements at every level in the DICOM structure (includes sub-elements of sequences)
        /// </summary>
        /// <value>All elements.</value>
        public List<IDICOMElement> AllElements
        {
            get
            {
                var allElements = new List<IDICOMElement>();
                foreach (IDICOMElement elem in Elements)
                {
                    allElements.Add(elem);
                    if (elem is Sequence)
                    {
                        var s = elem as Sequence;
                        foreach (DICOMObject d in s.Items)
                        {
                            foreach (IDICOMElement elem2 in d.AllElements)
                            {
                                allElements.Add(elem2);
                            }
                        }
                    }
                }
                return allElements;
            }
        }

        /// <summary>
        /// Adds an element to the DICOM object
        /// </summary>
        /// <param name="el">a DICOM element to be added</param>
        public void Add(IDICOMElement el)
        {
            _elements.Add(el);
            _elements.SortByTagID();
        }

        /// <summary>
        /// Searches for a specific element. If it is found, it returns the data from the element. Otherwise,
        /// it will return a provided default value for the element.
        /// </summary>
        /// <typeparam name="T">the type of data to return</typeparam>
        /// <param name="tagToFind">the tag of the element containing the data</param>
        /// <param name="defaultValueIfNull">the default value to return if the element is not found</param>
        /// <returns>DICOMData&lt;T&gt;.</returns>
        public DICOMData<T> TryGetDataValue<T>(Tag tagToFind, object defaultValueIfNull)
        {
            var found = FindFirst(tagToFind) as AbstractElement<T>;
            if (found != null)
            {
                return found.DataContainer;
            }
            var data = new DICOMData<T>();
            if (typeof(T).IsArray)
            {
                data.MultipicityValue = ((T[])defaultValueIfNull).ToList();
            }
            else
            {
                data.SingleValue = (T)defaultValueIfNull;
            }
            return data;
        }

        /// <summary>
        /// Searches for a specific element (first instance). If it is found, it sets the data for this element and returns
        /// true, otherwise returns false;
        /// </summary>
        /// <typeparam name="T">the type of data to return</typeparam>
        /// <param name="tagToFind">the tag of the element containing the data</param>
        /// <param name="data">the data to set in this element</param>
        /// <returns>a boolean indicating whether or not the operation was successful</returns>
        public bool TrySetDataValue<T>(Tag tagToFind, T data)
        {
            var found = FindFirst(tagToFind) as AbstractElement<T>;
            if (found != null)
            {
                found.DataContainer.SingleValue = data;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches for a specific element (first instance). If it is found, it sets the data for this element and returns
        /// true, otherwise returns false;
        /// </summary>
        /// <typeparam name="T">the type of data to return</typeparam>
        /// <param name="tagToFind">the tag of the element containing the data</param>
        /// <param name="data">the data to set in this element</param>
        /// <returns>a boolean indicating whether or not the operation was successful</returns>
        public bool TrySetDataValue<T>(Tag tagToFind, List<T> data)
        {
            var found = FindFirst(tagToFind) as AbstractElement<T>;
            if (found != null)
            {
                found.DataContainer.MultipicityValue = data;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds all DICOM elements that match a VR type
        /// </summary>
        /// <param name="vrToFind">the VR type to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(VR vrToFind)
        {
            return AllElements.Where(el => el.IsVR(vrToFind)).ToList();
        }

        /// <summary>
        /// Returns elements of a certain tag that are of the unknown VR type (because they are not
        /// in the DICOM dictionary) and reads them as the specified VR type
        /// </summary>
        /// <typeparam name="T">the VR type to read as</typeparam>
        /// <param name="toFind">the tag of this element</param>
        /// <returns>the unknown elements strongly typed to T</returns>
        public List<T> GetUnknownTagAs<T>(Tag toFind) where T : IDICOMElement
        {
            return FindAll(toFind)
                .Select(e => e as Unknown)
                .Where(u => u != null)
                .Select(u =>
                {
                    T t;
                    bool success = u.TryReadAs(out t);
                    return new { success, t };
                }).
                Where(u => u.success)
                .Select(u => u.t).ToList();
        }

        /// <summary>
        /// Returns elements of a certain tag that are of the unknown VR type (because they are not
        /// in the DICOM dictionary) and reads them as the specified VR type
        /// </summary>
        /// <typeparam name="T">the VR type to read as</typeparam>
        /// <param name="toFind">the tag of this element</param>
        /// <returns>the unknown elements strongly typed to T</returns>
        public List<T> GetUnknownTagAs<T>(string toFind) where T : IDICOMElement
        {
            return GetUnknownTagAs<T>(new Tag(toFind));
        }

        /// <summary>
        /// Finds all DICOM elements that match an element type
        /// </summary>
        /// <typeparam name="T">the DICOM element class that is being filtered and returned</typeparam>
        /// <returns>a list of all elements that are strongly typed</returns>
        public List<T> FindAll<T>()
        {
            Type t = typeof(T);
            return
                AllElements.Where(el => el is T)
                    .Select(el => (T)Convert.ChangeType(el, t, CultureInfo.CurrentCulture))
                    .ToList();
        }

        /// <summary>
        /// Finds all DICOM elements that match a certain tag
        /// </summary>
        /// <param name="tag">the tag to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(string tag)
        {
            return AllElements.Where(el => el.Tag.CompleteID == tag).ToList();
        }

        /// <summary>
        /// Finds all DICOM elements that match a certain tag
        /// </summary>
        /// <param name="tag">the tag to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(Tag tag)
        {
            return FindAll(tag.CompleteID);
        }

        /// <summary>
        /// Finds all DICOM elements that are embedded in the DICOM structure in some particular location. This location
        /// is defined by descending tags from the outer most elements to the element. It is not necessary to include every
        /// tag in the descending "treelike" structure. Branches can be skipped.
        /// </summary>
        /// <param name="descendingTags">a string array containing in order the tags from the outer most elements to the element
        /// being searched for</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(string[] descendingTags)
        {
            var matches = new List<IDICOMElement>();
            if (descendingTags.Length > 1)
            {
                string[] newDescTags = ArrayHelper.Pop(descendingTags);
                List<IDICOMElement> sequences =
                    AllElements.Where(e => e.IsVR(VR.Sequence))
                        .Where(el => el.Tag.CompleteID == descendingTags[0])
                        .ToList();
                foreach (IDICOMElement seq in sequences)
                {
                    var s = seq as Sequence;
                    foreach (DICOMObject d in s.Items)
                    {
                        foreach (IDICOMElement el in d.FindAll(newDescTags))
                        {
                            matches.Add(el);
                        }
                    }
                }
            }
            else
            {
                matches = AllElements.Where(el => el.Tag.CompleteID == descendingTags[0]).ToList();
            }

            return matches;
        }

        /// <summary>
        /// Finds all DICOM elements that are embedded in the DICOM structure in some particular location. This location
        /// is defined by descending tags from the outer most elements to the element. It is not necessary to include every
        /// tag in the descending "treelike" structure. Branches can be skipped.
        /// </summary>
        /// <param name="descendingTags">a tag array containing in order the tags from the outer most elements to the element being
        /// searched for</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(Tag[] descendingTags)
        {
            string[] strings = descendingTags.Select(t => t.CompleteID).ToArray();
            return FindAll(strings);
        }

        /// <summary>
        /// Finds the first element in the entire DICOM structure that has a certain tag
        /// </summary>
        /// <param name="toFind">the tag to be searched</param>
        /// <returns>one single DICOM element that is first occurence of the tag in the structure</returns>
        public IDICOMElement FindFirst(string toFind)
        {
            IDICOMElement found = AllElements.FirstOrDefault(el => el.Tag.CompleteID == toFind);
            return found;
        }

        /// <summary>
        /// Finds the first element in the entire DICOM structure that has a certain tag
        /// </summary>
        /// <param name="toFind">the tag to be searched</param>
        /// <returns>one single DICOM element that is first occurence of the tag in the structure</returns>
        public IDICOMElement FindFirst(Tag toFind)
        {
            return FindFirst(toFind.CompleteID);
        }

        /// <summary>
        /// Removes the element with the tag from the DICOM object
        /// </summary>
        /// <param name="tag">the tag string in the form of GGGGEEEE to be removed</param>
        public void Remove(string tag)
        {
            Elements.RemoveAll(el => el.Tag.CompleteID == tag);
            foreach (IDICOMElement elem in Elements)
            {
                if (elem is Sequence)
                {
                    var s = elem as Sequence;
                    foreach (DICOMObject d in s.Items)
                    {
                        d.Remove(tag);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the element with the tag from the DICOM object
        /// </summary>
        /// <param name="tag">the tag of the element to be removed</param>
        public void Remove(Tag tag)
        {
            Remove(tag.CompleteID);
        }

        /// <summary>
        /// Replaces a current instance of the DICOM element in the DICOM object. If the object does not exist, this method
        /// exits. For this scenario, please use ReplaceOrAdd().
        /// </summary>
        /// <typeparam name="T">the type of the data the element holds (eg. double[], int, DataTime, etc)</typeparam>
        /// <param name="element">the instance of the element</param>
        /// <returns>bool indicating whether or not the element was replaced</returns>
        private bool Replace<T>(AbstractElement<T> element)
        {
            var toReplace = FindFirst(element.Tag) as AbstractElement<T>;
            if (toReplace == null) return false;
            toReplace.DataContainer = element.DataContainer;
            return true;
        }

        /// <summary>
        /// Replaces the underlying DICOM element with input DICOM element of the same tag
        /// </summary>
        /// <param name="el">the new DICOM element</param>
        /// <returns>whether or not the operation was successful</returns>
        public bool Replace(IDICOMElement el)
        {
            IDICOMElement toReplace = FindFirst(el.Tag);
            if (toReplace == null) return false;
            toReplace.DData_ = el.DData_;
            return true;
        }

        /// <summary>
        /// Replaces a current instance of the DICOM element in the DICOM object. If the object does not exist, this method
        /// will add it to the object.
        /// </summary>
        /// <typeparam name="T">the type of the data the element holds (eg. double[], int, DataTime, etc)</typeparam>
        /// <param name="element">the instance of the element</param>
        public void ReplaceOrAdd<T>(AbstractElement<T> element)
        {
            if (!Replace(element))
            {
                Add(element);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("DICOM Object [{0}] : {1} Elements", this.SOPClass.ToString(), Elements.Count);
        }

        #region REPLACE OR ADD OVERLOADS

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<float> element)
        {
            ReplaceOrAdd<float>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<double> element)
        {
            ReplaceOrAdd<double>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<string> element)
        {
            ReplaceOrAdd<string>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<DICOMObject> element)
        {
            ReplaceOrAdd<DICOMObject>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<Tag> element)
        {
            ReplaceOrAdd<Tag>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<uint> element)
        {
            ReplaceOrAdd<uint>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<int> element)
        {
            ReplaceOrAdd<int>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<ushort> element)
        {
            ReplaceOrAdd<ushort>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<short> element)
        {
            ReplaceOrAdd<short>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<double?> element)
        {
            ReplaceOrAdd<double?>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<float?> element)
        {
            ReplaceOrAdd<float?>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<byte> element)
        {
            ReplaceOrAdd<byte>(element);
        }

        /// <summary>
        /// Replaces the or add.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ReplaceOrAdd(AbstractElement<DateTime?> element)
        {
            ReplaceOrAdd<DateTime?>(element);
        }

        #endregion

        #region IMAGE PROPERTIES

        /// <summary>
        /// Grabs the pixel data bytes and sends it as a stream. Returns null if no pixel data element is found.
        /// </summary>
        /// <value>The pixel stream.</value>
        public Stream PixelStream
        {
            get
            {
                var pixelData = FindFirst(TagHelper.PIXEL_DATA) as AbstractElement<byte>;
                if (pixelData != null)
                {
                    return new MemoryStream(pixelData.DataContainer.MultipicityValue.ToArray());
                }
                return null;
            }
        }

        #endregion

        #region GET SELECTOR

        /// <summary>
        /// Gets the selector.
        /// </summary>
        /// <returns>DICOMSelector.</returns>
        public DICOMSelector GetSelector()
        {
            return new DICOMSelector(this);
        }

        #endregion

        #region IO

        /// <summary>
        /// Reads a DICOM file from a path
        /// </summary>
        /// <param name="filePath">the path to the file</param>
        /// <param name="trySyntax">the transfer syntax to use in case there is no metadata explicitly included</param>
        /// <returns>the DICOM Object</returns>
        /// <example>
        ///   <code>
        /// var dcm = DICOMObject.Read("mydcm.dcm");
        /// </code>
        /// </example>
        public static DICOMObject Read(string filePath,
            TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            return DICOMFileReader.Read(filePath, trySyntax);
        }

        /// <summary>
        /// Reads a DICOM file from a byte array
        /// </summary>
        /// <param name="file">the bytes of the DICOM file</param>
        /// <param name="trySyntax">the transfer syntax to use in case there is no metadata explicitly included</param>
        /// <returns>DICOMObject.</returns>
        public static DICOMObject Read(byte[] file, TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            return DICOMFileReader.Read(file, trySyntax);
        }

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>DICOMObject.</returns>
        public static DICOMObject FromXML(string xml)
        {
            var dcm = new DICOMObject();
            dcm.LoadFromXML(xml);
            return dcm;
        }

        /// <summary>
        /// Writes DICOM object to a file
        /// </summary>
        /// <param name="file">the path to write</param>
        /// <param name="settings">the DICOM settings to write (endianness, and indefinite sequences)</param>
        public void Write(string file, DICOMWriteSettings settings = null)
        {
            settings = settings ?? DICOMWriteSettings.Default();
            DICOMFileWriter.Write(file, settings, this);
        }

        /// <summary>
        /// Writes the DICOM Object to an XML string for visualization and manipluation. Use FromXML() to get back.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToXML()
        {
            return this.ToXMLString();
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetBytes(DICOMWriteSettings settings = null)
        {
            settings = settings ?? DICOMWriteSettings.Default();
            using (var stream = new MemoryStream())
            {
                DICOMFileWriter.Write(stream, settings, this);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Gets the sop class.
        /// </summary>
        /// <value>The sop class.</value>
        public SOPClass SOPClass
        {
            get
            {
                var sel = GetSelector();
                if (sel.SOPClassUID != null)
                {
                    var sopClassUid = sel.SOPClassUID.Data;
                    return SOPClassHelper.FromUID(sopClassUid);
                }
                return SOPClass.Unknown;
            }
        }

        #endregion

        #region REPLACE OVERLOADS

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<float> element)
        {
            return Replace<float>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<double> element)
        {
            return Replace<double>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<string> element)
        {
            return Replace<string>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<DICOMObject> element)
        {
            return Replace<DICOMObject>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<Tag> element)
        {
            return Replace<Tag>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<uint> element)
        {
            return Replace<uint>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<int> element)
        {
            return Replace<int>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<ushort> element)
        {
            return Replace<ushort>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<short> element)
        {
            return Replace<short>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<byte> element)
        {
            return Replace<byte>(element);
        }

        /// <summary>
        /// Replaces the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Replace(AbstractElement<DateTime?> element)
        {
            return Replace<DateTime?>(element);
        }

        #endregion
    }
}