using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// This class anonymizes UIDs while keeping relative mapping between DICOM relationships
    /// </summary>
    public class UIDAnonymizer : IAnonymizer
    {
        private Dictionary<string, string> _uidMap = new Dictionary<string, string>();

        /// <summary>
        /// Adds a DICOM object to the mapping so UIDs can be remapped during the anonymization
        /// </summary>
        /// <param name="d"></param>
        public void AddDICOMObject(DICOMObject d)
        {
            List<IDICOMElement> uids = d.FindAll(VR.UniqueIdentifier).ToList();
            foreach (IDICOMElement el in uids)
            {
                UniqueIdentifier u = el as UniqueIdentifier;
                if (!IsProtectedUID(u.Tag) && u.Data != null)
                {
                    //Add only unique ids
                    if (!_uidMap.ContainsKey(u.Data))
                    {
                        AddToUIDDictionary(u.Data);
                    }
                }
            }
        }

        /// <summary>
        /// Keeps a few UIDs from getting scrambled
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private bool IsProtectedUID(Tag tag)
        {
            return tag.CompleteID == TagHelper.TRANSFER_SYNTAX_UID.CompleteID ||
                tag.CompleteID == TagHelper.SOPCLASS_UID.CompleteID ||
                tag.CompleteID == TagHelper.MEDIA_STORAGE_SOPCLASS_UID.CompleteID ||
                tag.CompleteID == TagHelper.IMPLEMENTATION_CLASS_UID.CompleteID ||
                tag.CompleteID == TagHelper.REFERENCED_SOPCLASS_UID.CompleteID;
        }

        /// <summary>
        /// Takes a current UID, generates a new one and adds a mapping to the dictionary
        /// </summary>
        /// <param name="uid"></param>
        public void AddToUIDDictionary(string uid)
        {
            string newUID = UIDHelper.GenerateUID();
            _uidMap.Add(uid, newUID);
        }

        /// <summary>
        /// Clears the original UIDs and adds the newly generated but remapped ones
        /// </summary>
        /// <param name="d"></param>
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Remapping UIDs...");
            List<IDICOMElement> uids = d.FindAll(VR.UniqueIdentifier).ToList();
            foreach (IDICOMElement el in uids)
            {
                UniqueIdentifier u = el as UniqueIdentifier;
                string newUID;
                if (u.Data != null)
                {
                    _uidMap.TryGetValue(u.Data, out newUID);

                    if (newUID != null)
                    {
                        u.Data = newUID;
                    }
                }
            }
        }

        public string Name
        {
            get { return "UID Anonymizer"; }
        }
    }
}
