using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDicom.Helper;
using EvilDicom.VR;

namespace EvilDicom
{
    namespace Components
    {
        ///<summary>
        /// The DICOMFile class is simply a DICOM Collection with methods
        /// to read and write files. This class depends heavily on the DICOMReader
        /// class in the EvilDicom.Helper namespace.
        /// </summary>
        public partial class DICOMFile : DICOMObject
        {

            /// <summary>
            /// Empty constructor for the DicomFile object
            /// </summary>
            public DICOMFile()
            {
                this.DicomObjects = new List<DICOMElement>();
            }


            /// <summary>
            /// The main constructor of the DicomFile object which takes a string
            /// path of a DICOM file and parses the file into a collection of accessable
            /// DICOM objects which it adds to its collection.
            /// </summary>
            /// <param name="path">the string path to the DICOM file</param>
            public DICOMFile(string path)
            {
                this.Path = path;
                Parse(true);
            }

            /// <summary>
            /// The main constructor of the DicomFile object which takes a byte[] 
            /// making up a DICOM file and parses the file into a collection of accessable
            /// DICOM objects which it adds to its collection.
            /// </summary>
            /// <param name="path">the string path to the DICOM file</param>
            public DICOMFile(byte[] file)
            {
                Parse(true,file);
            }

            /// <summary>
            /// This constructor allows a DICOM file with no preamble. The parameter is
            /// set by the hasPreamble input.
            /// </summary>
            /// <param name="path">the string path to the DICOM file</param>
            public DICOMFile(string path, bool hasPreamble)
            {
                this.Path = path;
                Parse(hasPreamble);
            }


            public void Parse(bool hasPreamble)
            {
                using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    //DICOM files use ASCII encoding for characters, not UTF8 (default C#)
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                    using (BinaryReader r = new BinaryReader(fs, enc))
                    {
                        if (!hasPreamble || DICOMReader.IsValidDicom(r))
                        {
                            //After isValidDicom is executed, cursor is at bit 132
                            while (r.BaseStream.Position < r.BaseStream.Length)
                            {
                                DICOMElement d = DICOMReader.ReadObject(r, IsLittleEndian);
                                if (d != null)
                                {
                                    AddObject(d);
                                }
                            }

                            HasBeenParsed = true;
                        }
                        else
                        {
                            Console.Write("File is missing preamble. Check to make sure it is a vaild DICOM 3.0 file.");
                            //Try to parse anyway
                            try
                            {
                                r.BaseStream.Position = 0;
                                //After isValidDicom is executed, cursor is at bit 132
                                while (r.BaseStream.Position < r.BaseStream.Length)
                                {
                                    DICOMElement d = DICOMReader.ReadObject(r, IsLittleEndian);
                                    if (d != null)
                                    {
                                        AddObject(d);
                                    }
                                }

                                HasBeenParsed = true;
                            }
                            catch (Exception e)
                            {
                                Console.Write("Could not parse file. Check to make sure it is a vaild DICOM 3.0 file.");
                            }
                        }
                    }

                }

            }

            public void Parse(bool hasPreamble, byte[] file)
            {
                using (MemoryStream ms = new MemoryStream(file)){
                    //DICOM files use ASCII encoding for characters, not UTF8 (default C#)
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                    using (BinaryReader r = new BinaryReader(ms, enc))
                    {
                        if (!hasPreamble || DICOMReader.IsValidDicom(r))
                        {
                            //After isValidDicom is executed, cursor is at bit 132
                            while (r.BaseStream.Position < r.BaseStream.Length)
                            {
                                DICOMElement d = DICOMReader.ReadObject(r, IsLittleEndian);
                                if (d != null)
                                {
                                    AddObject(d);
                                }
                            }

                            HasBeenParsed = true;
                        }
                        else
                        {
                            Console.Write("File is missing preamble. Check to make sure it is a vaild DICOM 3.0 file.");
                            //Try to parse anyway
                            try
                            {
                                r.BaseStream.Position = 0;
                                //After isValidDicom is executed, cursor is at bit 132
                                while (r.BaseStream.Position < r.BaseStream.Length)
                                {
                                    DICOMElement d = DICOMReader.ReadObject(r, IsLittleEndian);
                                    if (d != null)
                                    {
                                        AddObject(d);
                                    }
                                }

                                HasBeenParsed = true;
                            }
                            catch (Exception e)
                            {
                                Console.Write("Could not parse file. Check to make sure it is a vaild DICOM 3.0 file.");
                            }
                        }
                    }

                }

            }

            /// <summary>
            /// This method writes the bytes of a DICOM file which includes the DICOM preamble of 
            /// 128 null bytes followed by the ASCII characters "DICM".
            /// </summary>
            /// <param name="path">the string path indicating where to write the file and the name of the file</param>
            /// <param name="isLittleEndian">a boolean indicating whether or not the bytes are to be written in little or big endian encoding</param>
            public void WriteFile(string path, bool isLittleEndian)
            {
                using (BinaryWriter b = new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    DICOMWriter.WriteDicomPreamble(b);
                    WriteBytes(b, isLittleEndian);
                }
            }

            /// <summary>
            /// This method writes the bytes of a DICOM file which includes the DICOM preamble of 
            /// 128 null bytes followed by the ASCII characters "DICM".
            /// </summary>
            ///  /// <param name="path">the string path indicating where to write the file and the name of the file</param>
            public void WriteFile(string path)
            {
                using (BinaryWriter b = new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    DICOMWriter.WriteDicomPreamble(b);
                    WriteBytes(b, true);
                }
            }

            /// <summary>
            /// This method writes the bytes of a DICOM file which includes the DICOM preamble of 
            /// 128 null bytes followed by the ASCII characters "DICM".
            /// </summary>
            ///  /// <param name="path">the string path indicating where to write the file and the name of the file</param>
            public void WriteFile(string path, int objectsToWrite)
            {
                using (BinaryWriter b = new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    DICOMWriter.WriteDicomPreamble(b);
                    WriteBytes(b, true, objectsToWrite);
                }
            }

            /// <summary>
            /// The physical path to the Dicom file
            /// </summary>
            public string Path
            {
                get;
                set;
            }

            /// <summary>
            /// Indicates whether or not this file has been parsed into Dicom objects
            /// </summary>
            public bool HasBeenParsed
            {
                get;
                set;
            }

            /// <summary>
            /// Adds a Dicom object to the DicomObject stack and sorts by tag.
            /// </summary>
            /// <param name="d">the Dicom object to be added</param>
            public void AddObject(DICOMElement d)
            {
                //Add object to list
                this.DicomObjects.Add(d);
                //Sort List by ID
                this.DicomObjects.Sort(delegate(DICOMElement d1, DICOMElement d2) { return d1.Tag.Id.CompareTo(d2.Tag.Id); });
            }

            /// <summary>
            /// Removes an object from the DicomObject stack that has the input id
            /// </summary>
            /// <param name="t">the tag containing the id to be removed</param>
            public void RemoveObject(Tag t)
            {
                foreach (DICOMElement d in this.DicomObjects)
                {
                    if (d.Tag.Id == t.Id)
                    {
                        this.DicomObjects.Remove(d);
                    }
                }
            }
            /// <summary>
            /// Removes an object from the DicomObject stack that has the input id
            /// </summary>
            /// <param name="id">the tag id to be removed</param>
            public void RemoveObject(string id)
            {
                foreach (DICOMElement d in this.DicomObjects)
                {
                    if (d.Tag.Id == id)
                    {
                        this.DicomObjects.Remove(d);
                    }
                }
            }

            public void ToXML(string xmlPath)
            {
                XMLHelper.DICOM2XML(this, xmlPath);
            }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


