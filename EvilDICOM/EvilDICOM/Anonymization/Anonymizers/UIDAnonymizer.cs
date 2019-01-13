#region

using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// This class anonymizes UIDs while keeping relative mapping between DICOM relationships
    /// </summary>
    public class UIDAnonymizer : IAnonymizer
    {
        private readonly Dictionary<string, string> _uidMap = new Dictionary<string, string>();

        public string Name
        {
            get { return "UID Anonymizer"; }
        }


        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Remapping UIDs...");
            var uids = d.FindAll(VR.UniqueIdentifier).ToList();
            foreach (var el in uids)
            {
                var u = el as UniqueIdentifier;
                string newUID;
                if (u.Data != null)
                {
                    _uidMap.TryGetValue(u.Data, out newUID);

                    if (newUID != null)
                        u.Data = newUID;
                }
            }
        }

        public void AddDICOMObject(DICOMObject d)
        {
            var uids = d.FindAll(VR.UniqueIdentifier).ToList();
            foreach (var el in uids)
            {
                var u = el as UniqueIdentifier;
                if (!IsProtectedUID(u.Tag) && u.Data != null)
                    if (!_uidMap.ContainsKey(u.Data))
                        AddToUIDDictionary(u.Data);
            }
        }

        private bool IsProtectedUID(Tag tag)
        {
            return tag.CompleteID == TagHelper.TransferSyntaxUID.CompleteID ||
                   tag.CompleteID == TagHelper.SOPClassUID.CompleteID ||
                   tag.CompleteID == TagHelper.MediaStorageSOPClassUID.CompleteID ||
                   tag.CompleteID == TagHelper.ImplementationClassUID.CompleteID ||
                   tag.CompleteID == TagHelper.ReferencedSOPClassUID.CompleteID;
        }

        public void AddToUIDDictionary(string uid)
        {
            var newUID = UIDHelper.GenerateUID();
            _uidMap.Add(uid, newUID);
        }
    }
}