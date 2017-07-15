using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EvilDICOM.Core.Element;

using EvilDICOM.Core.Interfaces;

using DateTime = System.DateTime;
using System.Threading.Tasks;

namespace EvilDICOM.Core
{
    /// <summary>
    ///     The DICOM object is a container for DICOM elements. It contains methods for finding elements easily from within the
    ///     structure.
    /// </summary>
    public class DICOMObject
    {
        private List<IDICOMElement> _elements = new List<IDICOMElement>();

        /// <summary>
        ///     Constructor no parameters
        /// </summary>
        public DICOMObject()
        {
            _elements = new List<IDICOMElement>();
        }


        /// <summary>
        ///     The list of first level DICOM elements inside this DICOM object
        /// </summary>
        public List<IDICOMElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        ///     The list of all DICOM elements at every level in the DICOM structure (includes sub-elements of sequences)
        /// </summary>
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
        ///     Searches for a specific element (first instance). If it is found, it sets the data for this element and returns
        ///     true, otherwise returns false;
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
        ///     Searches for a specific element (first instance). If it is found, it sets the data for this element and returns
        ///     true, otherwise returns false;
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
        ///     Returns elements of a certain tag that are of the unknown VR type (because they are not
        ///     in the DICOM dictionary) and reads them as the specified VR type
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
        ///     Returns elements of a certain tag that are of the unknown VR type (because they are not
        ///     in the DICOM dictionary) and reads them as the specified VR type
        /// </summary>
        /// <typeparam name="T">the VR type to read as</typeparam>
        /// <param name="toFind">the tag of this element</param>
        /// <returns>the unknown elements strongly typed to T</returns>
        public List<T> GetUnknownTagAs<T>(string toFind) where T : IDICOMElement
        {
            return GetUnknownTagAs<T>(new Tag(toFind));
        }

        /// <summary>
        ///     Finds all DICOM elements that match an element type
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
        ///     Finds all DICOM elements that match a certain tag
        /// </summary>
        /// <param name="tag">the tag to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(string tag)
        {
            return AllElements.Where(el => el.Tag.CompleteID == tag).ToList();
        }

        /// <summary>
        ///     Finds all DICOM elements that match a certain tag
        /// </summary>
        /// <param name="tag">the tag to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(Tag tag)
        {
            return FindAll(tag.CompleteID);
        }

        /// <summary>
        ///     Finds all DICOM elements that are embedded in the DICOM structure in some particular location. This location
        ///     is defined by descending tags from the outer most elements to the element. It is not necessary to include every
        ///     tag in the descending "treelike" structure. Branches can be skipped.
        /// </summary>
        /// <param name="descendingTags">
        ///     a string array containing in order the tags from the outer most elements to the element
        ///     being searched for
        /// </param>
        /// <returns>a list of all elements that meet the search criteria</returns>




        /// <summary>
        ///     Finds the first element in the entire DICOM structure that has a certain tag
        /// </summary>
        /// <param name="toFind">the tag to be searched</param>
        /// <returns>one single DICOM element that is first occurence of the tag in the structure</returns>
        public IDICOMElement FindFirst(string toFind)
        {
            IDICOMElement found = AllElements.FirstOrDefault(el => el.Tag.CompleteID == toFind);
            return found;
        }

        /// <summary>
        ///     Finds the first element in the entire DICOM structure that has a certain tag
        /// </summary>
        /// <param name="toFind">the tag to be searched</param>
        /// <returns>one single DICOM element that is first occurence of the tag in the structure</returns>
        public IDICOMElement FindFirst(Tag toFind)
        {
            return FindFirst(toFind.CompleteID);
        }

        /// <summary>
        ///     Removes the element with the tag from the DICOM object
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
        ///     Removes the element with the tag from the DICOM object
        /// </summary>
        /// <param name="tag">the tag of the element to be removed</param>
        public void Remove(Tag tag)
        {
            Remove(tag.CompleteID);
        }

        /// <summary>
        ///     Replaces a current instance of the DICOM element in the DICOM object. If the object does not exist, this method
        ///     exits. For this scenario, please use ReplaceOrAdd().
        /// </summary>
        /// <typeparam name="T">the type of the data the element holds (eg. double[], int, DataTime, etc)</typeparam>
        /// <param name="element">the instance of the element</param>
        /// <returns>bool indicating whether or not the element was replaced</returns>
        private bool Replace<T>(AbstractElement<T> element)
        {
            if (element.Tag == null) { return false; }
            var toReplace = FindFirst(element.Tag) as AbstractElement<T>;
            if (toReplace == null) return false;
            toReplace.DataContainer = element.DataContainer;
            return true;
        }

        /// <summary>
        ///     Replaces the underlying DICOM element with input DICOM element of the same tag
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
        ///     Replaces a current instance of the DICOM element in the DICOM object. If the object does not exist, this method
        ///     will add it to the object.
        /// </summary>
        /// <typeparam name="T">the type of the data the element holds (eg. double[], int, DataTime, etc)</typeparam>
        /// <param name="element">the instance of the element</param>



        #region REPLACE OR ADD OVERLOADS

   

        #endregion

        #region IMAGE PROPERTIES

        /// <summary>
        ///     Grabs the pixel data bytes and sends it as a stream. Returns null if no pixel data element is found.
        /// </summary>


        #endregion

        #region GET SELECTOR



        #endregion

        #region IO





        #endregion

        #region REPLACE OVERLOADS

        public bool Replace(AbstractElement<float> element)
        {
            return Replace<float>(element);
        }

        public bool Replace(AbstractElement<double> element)
        {
            return Replace<double>(element);
        }

        public bool Replace(AbstractElement<string> element)
        {
            return Replace<string>(element);
        }

        public bool Replace(AbstractElement<DICOMObject> element)
        {
            return Replace<DICOMObject>(element);
        }

        public bool Replace(AbstractElement<Tag> element)
        {
            return Replace<Tag>(element);
        }

        public bool Replace(AbstractElement<uint> element)
        {
            return Replace<uint>(element);
        }

        public bool Replace(AbstractElement<int> element)
        {
            return Replace<int>(element);
        }

        public bool Replace(AbstractElement<ushort> element)
        {
            return Replace<ushort>(element);
        }

        public bool Replace(AbstractElement<short> element)
        {
            return Replace<short>(element);
        }

        public bool Replace(AbstractElement<byte> element)
        {
            return Replace<byte>(element);
        }

        public bool Replace(AbstractElement<DateTime?> element)
        {
            return Replace<DateTime?>(element);
        }

        #endregion
    }
}