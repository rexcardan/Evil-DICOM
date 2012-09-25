using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Helpers;
using System.Reflection;

namespace EvilDICOM.Core
{
    /// <summary>
    /// The DICOM object is a container for DICOM elements. It contains methods for finding elements easily from within the structure.
    /// </summary>
    public class DICOMObject
    {
        private List<IDICOMElement> _elements = new List<IDICOMElement>();

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="elements">a list of elements to be included in the object</param>
        public DICOMObject(List<IDICOMElement> elements)
        {
            _elements = elements;
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
        /// The list of first level DICOM elements inside this DICOM object
        /// </summary>
        public List<IDICOMElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        /// The list of all DICOM elements at every level in the DICOM structure (includes sub-elements of sequences)
        /// </summary>
        public List<IDICOMElement> AllElements
        {
            get
            {
                List<IDICOMElement> allElements = new List<IDICOMElement>();
                foreach (IDICOMElement elem in Elements)
                {
                    allElements.Add(elem);
                    if (elem is Sequence)
                    {
                        Sequence s = elem as Sequence;
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
        /// Finds all DICOM elements that match a VR type
        /// </summary>
        /// <param name="vrToFind">the VR type to find</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(VR vrToFind)
        {
            return AllElements.Where(el => el.IsVR(vrToFind)).ToList();
        }

        /// <summary>
        /// Finds all DICOM elements that match an element type
        /// </summary>
        /// <typeparam name="T">the DICOM element class that is being filtered and returned</typeparam>
        /// <returns>a list of all elements that are strongly typed</returns>
        public List<T> FindAll<T>() 
        {
            Type t = typeof(T);
            return AllElements.Where(el => el is T).Select(el=>(T)Convert.ChangeType(el,t)).ToList();
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
        /// <param name="descendingTags">a string array containing in order the tags from the outer most elements to the element being searched for</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(string[] descendingTags)
        {
            List<IDICOMElement> matches = new List<IDICOMElement>();
            if (descendingTags.Length > 1)
            {
                string[] newDescTags = ArrayHelper.Pop(descendingTags);
                List<IDICOMElement> sequences = AllElements.Where(e => e.IsVR(VR.Sequence)).Where(el => el.Tag.CompleteID == descendingTags[0]).ToList();
                foreach (IDICOMElement seq in sequences)
                {
                    Sequence s = seq as Sequence;
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
        /// <param name="descendingTags">a tag array containing in order the tags from the outer most elements to the element being searched for</param>
        /// <returns>a list of all elements that meet the search criteria</returns>
        public List<IDICOMElement> FindAll(Tag[] descendingTags)
        {
            List<string> stringList = new List<string>();
            descendingTags.ToList().ForEach(t => stringList.Add(t.CompleteID));
            return FindAll(stringList.ToArray());
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

        public void Remove(string tag)
        {
            Elements.RemoveAll(el => el.Tag.CompleteID == tag);
            foreach (IDICOMElement elem in Elements)
            {
                if (elem is Sequence)
                {
                    Sequence s = elem as Sequence;
                    foreach (DICOMObject d in s.Items)
                    {
                        d.Remove(tag);
                    }
                }
            }
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
            toReplace.Data = element.Data;
            return true;
        }

        #region REPLACE OVERLOADS
        public bool Replace(AbstractElement<float[]> element)
        {
            return Replace<float[]>(element);
        }
        public bool Replace(AbstractElement<double[]> element)
        {
            return Replace<double[]>(element);
        }
        public bool Replace(AbstractElement<string> element)
        {
            return Replace<string>(element);
        }
        public bool Replace(AbstractElement<List<DICOMObject>> element)
        {
            return Replace<List<DICOMObject>>(element);
        }
        public bool Replace(AbstractElement<Tag> element)
        {
            return Replace<Tag>(element);
        }
        public bool Replace(AbstractElement<uint?> element)
        {
            return Replace<uint?>(element);
        }
        public bool Replace(AbstractElement<int?> element)
        {
            return Replace<int?>(element);
        }
        public bool Replace(AbstractElement<ushort?> element)
        {
            return Replace<ushort?>(element);
        }
        public bool Replace(AbstractElement<short?> element)
        {
            return Replace<short?>(element);
        }
        public bool Replace(AbstractElement<double?> element)
        {
            return Replace<double?>(element);
        }
        public bool Replace(AbstractElement<float?> element)
        {
            return Replace<float?>(element);
        }
        public bool Replace(AbstractElement<byte[]> element)
        {
            return Replace<byte[]>(element);
        }
        public bool Replace(AbstractElement<int[]> element)
        {
            return Replace<int[]>(element);
        }
        public bool Replace(AbstractElement<System.DateTime?> element)
        {
            return Replace<System.DateTime?>(element);
        }
        #endregion

        /// <summary>
        /// Replaces a current instance of the DICOM element in the DICOM object. If the object does not exist, this method 
        /// will add it to the object.
        /// </summary>
        /// <typeparam name="T">the type of the data the element holds (eg. double[], int, DataTime, etc)</typeparam>
        /// <param name="element">the instance of the element</param>
        public void ReplaceOrAdd<T>(AbstractElement<T> element)
        {
            if(!Replace<T>(element))
            {
                Add(element);
            }
        }

        #region REPLACE OR ADD OVERLOADS
        public void ReplaceOrAdd(AbstractElement<float[]> element)
        {
            ReplaceOrAdd<float[]>(element);
        }
        public void ReplaceOrAdd(AbstractElement<double[]> element)
        {
            ReplaceOrAdd<double[]>(element);
        }
        public void ReplaceOrAdd(AbstractElement<string> element)
        {
            ReplaceOrAdd<string>(element);
        }
        public void ReplaceOrAdd(AbstractElement<List<DICOMObject>> element)
        {
            ReplaceOrAdd<List<DICOMObject>>(element);
        }
        public void ReplaceOrAdd(AbstractElement<Tag> element)
        {
            ReplaceOrAdd<Tag>(element);
        }
        public void ReplaceOrAdd(AbstractElement<uint?> element)
        {
            ReplaceOrAdd<uint?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<int?> element)
        {
            ReplaceOrAdd<int?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<ushort?> element)
        {
            ReplaceOrAdd<ushort?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<short?> element)
        {
            ReplaceOrAdd<short?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<double?> element)
        {
            ReplaceOrAdd<double?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<float?> element)
        {
            ReplaceOrAdd<float?>(element);
        }
        public void ReplaceOrAdd(AbstractElement<byte[]> element)
        {
            ReplaceOrAdd<byte[]>(element);
        }
        public void ReplaceOrAdd(AbstractElement<int[]> element)
        {
            ReplaceOrAdd<int[]>(element);
        }
        public void ReplaceOrAdd(AbstractElement<System.DateTime?> element)
        {
            ReplaceOrAdd<System.DateTime?>(element);
        }
        #endregion
    }
}
