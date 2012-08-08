using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;
using System.IO;

namespace EvilDicom.Components
{
    /// <summary>
    /// The DicomCollection class is an abstract class to be inherited by any class that
    /// contains a collection of DICOM objects. It contains useful methods for searching
    /// a collection of DICOM objects by Tag ID.
    /// </summary>
    public abstract partial class DICOMObject
    {
        /// <summary>
        /// The collection of DICOM objects
        /// </summary>
        private List<DICOMElement> collection = new List<DICOMElement>();
        /// <summary>
        /// A boolean that indicates whether or not the bytes are written in little or big endian.
        /// </summary>
        private bool isLittleEndian = true;
        /// <summary>
        /// The entire byte length of all DICOM objects in the collection added together
        /// </summary>
        public int Length
        {
            get
            {
                int length = 0;
                foreach (DICOMElement d in collection)
                {                  
                        length += d.Length;                
                }

                return length;
            }
        }
        /// <summary>
        /// A boolean that indicates whether or not the bytes are written in little or big endian.
        /// </summary
        public bool IsLittleEndian
        {
            get { return isLittleEndian; }
            set { isLittleEndian = value; }
        }
        /// <summary>
        /// The collection of DICOM objects
        /// </summary
        public List<DICOMElement> DicomObjects
        {
            get { return collection; }
            set { collection = value; }
        }

        /// <summary>
        /// Adds a DICOM object to the collection
        /// </summary>
        /// <param name="d">the DICOM object to be added</param>
        public void AddObject(DICOMElement d)
        {
            collection.Add(d);
        }

        /// <summary>
        /// This method searches the dicom file for a particular tag.
        /// </summary>
        /// <param name="id">The tag to be searched (eg. "00030000" for 0003,0000)</param>
        /// <returns>Returns the fist tag found in the dicom file that matches the id input parameter.</returns>
        public DICOMElement Find(string id) {
            try
            {
                return Find(new string[] { id })[0];
            }
            catch(Exception e){
                Console.WriteLine("Could not find id {0} in Dicom file", id);
                return null;
            }
        }

        /// <summary>
        /// This method searches the dicom file using a chain of strings. It is intended to be used to find particular tags deep within the Dicom structure.
        /// To use it, simply input a string array containing the descending tags to be searched. The method will return results that match the last tag
        /// in the string array if and only if they have parent tags that match the preceeding tags in the string array. A string array then looks like this:
        /// new string[]{parent, child A, child B (child of A), clild C (child of B)..etc...,child Final (search taget)}
        /// </summary>
        /// <param name="ids">string array that contains the descendant selection tags with the target in the last index</param>
        /// <returns>Returns a list of dicom objects that match the tag search chain</returns>
        public List<DICOMElement> Find(string[] ids)
        {
            List<DICOMElement> found = new List<DICOMElement>();
            List<DICOMElement> searchList = ArrayHelper.CopyList(collection);

            for (int i = 0; i < ids.Length; i++)
            {
                //Find first level objects and iterate downward
                foreach (DICOMElement d in searchList)
                {
                    //If dicom Object matches add to list
                    if (d.Tag.Id.Equals(ids[i]))
                    {
                        found.Add(d);
                    }

                    //Else search children for id
                    if (d.IsSequence)
                    {
                        foreach (DICOMElement o in d.find(new string[] { ids[i] }))
                        {
                            found.Add(o);
                        }
                    }
                }
                if (i != ids.Length - 1)
                {
                    //Copy found objects to search list and clear found
                    searchList = ArrayHelper.CopyList(found);
                    found.Clear();
                }
                else { return found; }
            }
            return null;
        }

        /// <summary>
        /// Writes the byte sequence of this collection in big or little endian
        /// encoding, depending on the setting of the isLittleEndian variable.
        /// </summary>
        /// <param name="b">a Binary writer used to write the bytes</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public virtual void WriteBytes(BinaryWriter b, bool isLittleEndian)
        {
            int i = 0;
            foreach (DICOMElement d in collection)
            {
                int length = d.Length;
                int byteLength = d.ByteData.Length;
                if (d.Tag != null)
                {
                    string tag = d.Tag.Id;
                }
                
                if (d.IsSequence)
                {
                    Console.WriteLine("Error");
                }

                    d.WriteBytes(b, isLittleEndian);
                //}
            }
        }

        /// <summary>
        /// Writes the byte sequence of this collection in big or little endian
        /// encoding, depending on the setting of the isLittleEndian variable. Only writes the number of
        /// objects in the objects to write parameter
        /// </summary>
        /// <param name="b">a Binary writer used to write the bytes</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
       /// <param name="objectsToWrite">Number of objects to write</param>
        public virtual void WriteBytes(BinaryWriter b, bool isLittleEndian, int objectsToWrite)
        {
            int i = 0;
            foreach (DICOMElement d in collection)
            {
                int length = d.Length;
                int byteLength = d.ByteData.Length;
                if (d.Tag != null)
                {
                    string tag = d.Tag.Id;
                }

                if (d.IsSequence)
                {
                    Console.WriteLine("Error");
                }

                d.WriteBytes(b, isLittleEndian);
                i++;
                if (i == objectsToWrite - 1)
                {
                    return;
                }
            }
        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


