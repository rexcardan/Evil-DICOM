using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace EvilDicom.Components
{
    /// <summary>
    /// The DICOMDictionary class is in charge of managing the definitions
    /// in the DICOMDictionary. It has one main method.
    /// </summary>
    public class DICOMDictionary
    {

        //This is the interface for the XML file where the dictionary is stored   
        /// <summary>
        /// This method takes a Tag with just the ID attribute and looks it up
        /// to see if it is in the DICOM dictionary. If it is, it fills in extra
        /// information about that tag including the descriptions of the tag.
        /// It returns a string VR type indicating the VR of the tag.
        /// </summary>
        /// <param name="t">a Tag with (normally with just the ID attribute non-null) that is to be looked up in the DICOM dictionary</param>
        /// <returns>a string indication the two letters of the VR type of the submitted Tag</returns>
        public static string LookupTag(Tag t)
        {
            string vr = "";
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("EvilDicom.dictionary.xml");
            XmlTextReader reader = new XmlTextReader(s);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("element"))
                {
                    if (reader.GetAttribute("tag").Equals(t.Id))
                    {
                        vr = reader.GetAttribute("vr");
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            t.Description = reader.Value;
                            break;
                        }
                    }
                }

            }
            return vr;
        }

        public static List<Tag> GetAllTags()
        {
            List<Tag> tags = new List<Tag>();
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("EvilDicom.dictionary.xml");
            XmlTextReader reader = new XmlTextReader(s);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("element"))
                {
                    Tag t = new Tag();
                    t.Id = reader.GetAttribute("tag");
                    t.Description = reader.GetAttribute("keyword");                
                    tags.Add(t);
                }
            }

            return tags;
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


