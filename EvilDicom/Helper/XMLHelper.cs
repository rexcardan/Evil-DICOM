using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using System.Xml;
using EvilDicom.VR;

namespace EvilDicom.Helper
{
    public static class XMLHelper
    {
        //Name Settings
        private static string DICOM_FILE_WRAPPER= "DICOMFile";
        private static string SEQUENCE_ITEM_WRAPPER = "SequenceItem";
        private static string NUM_OF_OBJECTS_ATTR_NAME = "NumberOfObjects";
        private static string TAG_ATTR_NAME = "Tag";
        private static string DESCRIPTION_ATTR_NAME = "Description";
        private static string VR_ATTR_NAME = "VR";
        private static string LENGTH_ATTR_NAME = "Length";
        private static string DATA_NAME = "Data";
        //Attribute Settings
        private static bool INCLUDE_NUM_OF_OBJECTS_IN_COLLECTIONS = true;
        private static bool INCLUDE_TAG = true;
        private static bool INCLUDE_DESCRIPTION = true;
        private static bool INCLUDE_VR = true;
        private static bool INCLUDE_LENGTH = true;
        private static bool INCLUDE_DATA = true;

        public static void DICOM2XML(DICOMFile df, string xmlFilePath)
        {
            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, GenerateCleanSettings()))
            {
                writer.WriteStartElement(DICOM_FILE_WRAPPER);
                if (INCLUDE_NUM_OF_OBJECTS_IN_COLLECTIONS)
                {
                    writer.WriteAttributeString(NUM_OF_OBJECTS_ATTR_NAME, df.DicomObjects.Count.ToString());
                }
                WriteObjects(writer, df.DicomObjects);
                writer.WriteEndElement();
            }
        }

        public static XmlWriterSettings GenerateCleanSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;
            return settings;
        }

        public static void WriteChildren(XmlWriter w, Sequence s)
        {
            foreach (SequenceItem si in s.Items)
            {
                w.WriteStartElement(SEQUENCE_ITEM_WRAPPER);
                w.WriteAttributeString(NUM_OF_OBJECTS_ATTR_NAME, si.DicomObjects.Count.ToString());
                WriteObjects(w, si.DicomObjects);
                w.WriteEndElement();
            }
        }

        private static void WriteObjects(XmlWriter w, List<DICOMElement> objects)
        {
            foreach (DICOMElement d in objects)
            {
                w.WriteStartElement(d.GetType().ToString().Replace("EvilDicom.VR.", "").Replace("EvilDicom.Components.", ""));
                if (INCLUDE_TAG)
                {
                    w.WriteAttributeString(TAG_ATTR_NAME, d.Tag.Id);
                }
                if (INCLUDE_DESCRIPTION)
                {
                    w.WriteAttributeString(DESCRIPTION_ATTR_NAME, d.Tag.Description);
                }
                if (INCLUDE_VR)
                {
                    w.WriteAttributeString(VR_ATTR_NAME, d.VR);
                }
                if (INCLUDE_LENGTH)
                {
                    w.WriteAttributeString(LENGTH_ATTR_NAME, d.ByteData.Length.ToString());
                }
                if (d.IsSequence)
                {
                    WriteChildren(w, d as Sequence);
                }
                else
                {
                    if (INCLUDE_DATA)
                    {
                        foreach (string s in d.DataAsStringArray())
                        {
                            //Write Data
                            w.WriteElementString(DATA_NAME, s);
                        }
                    }
                }
                w.WriteEndElement();
            }
        }
    }
}
