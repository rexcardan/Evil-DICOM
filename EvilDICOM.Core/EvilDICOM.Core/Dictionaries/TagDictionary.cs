using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace EvilDICOM.Core.Dictionaries
{
    /// <summary>
    /// The class is used to look up tags in a dictionary for relevant information such as vr type.
    /// </summary>
    public class TagDictionary
    {
        public TagDictionary()
        {
            Tags = new List<TagInfo>();
            Initalize();
        }

        /// <summary>
        /// The tags in the dictionary. Must call initialize method to populate this list.
        /// </summary>
        public List<TagInfo> Tags { get; set; }

        /// <summary>
        /// Loads the resource dictionary for all tags.
        /// </summary>
        private void Initalize()
        {
            Stream s = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.DICOMDictionary));
            XmlTextReader reader = new XmlTextReader(s);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("element"))
                {
                    TagInfo ti = new TagInfo();
                    ti.VRAbbreviation = reader.GetAttribute("vr").ToUpper();
                    ti.Name = reader.GetAttribute("keyword");
                    if (!string.IsNullOrEmpty(ti.VRAbbreviation))
                    {
                        ti.VRType = VRDictionary.GetVRFromAbbreviation(ti.VRAbbreviation).ToString();
                    }
                    ti.TagID = reader.GetAttribute("tag");
                    Tags.Add(ti);
                }
            }
        }
    }
    /// <summary>
    /// A small class to hold the tag information as it comes in from the dictionary.
    /// </summary>
    public class TagInfo
    {
        public string VRAbbreviation { get; set; }
        public string VRType { get; set; }
        public string TagID { get; set; }
        public string Name { get; set; }
    }
}

