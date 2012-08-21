using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Enums;
using System.IO;
using System.Reflection;
using System.Xml;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Dictionaries
{
    /// <summary>
    /// Interfaces with the DICOM Dictionary resource in order to look up VR types from tags, in the case implicit encoding is used
    /// </summary>
    public class TagVRDictionary
    {
        private Dictionary<string, VR> _dictionary = new Dictionary<string, VR>();

        /// <summary>
        /// Constructor
        /// </summary>
        public TagVRDictionary()
        {
            Initialize();
        }

        /// <summary>
        /// Finds a VR type from a given tag 
        /// </summary>
        /// <param name="tag">the tag from which to find the VR type</param>
        /// <returns>the VR type for the tag</returns>
        public VR GetVRFromTag(Tag tag){
            try
            {
                return _dictionary[tag.CompleteID];
            }
            catch (Exception e)
            {
                return VR.Null;
            }
        }

        /// <summary>
        /// Fills a local dictionary with the Tag-VR relationships
        /// </summary>
        private void Initialize()
        {
            Stream s = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.DICOMDictionary));
            XmlTextReader reader = new XmlTextReader(s);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("element"))
                {
                    string vr = reader.GetAttribute("vr");
                    _dictionary.Add(reader.GetAttribute("tag"), VRDictionary.GetVRFromAbbreviation(vr));
                }
            }
        }
    }
}
