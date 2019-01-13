#region

using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.Modules
{
    public class FileMetadata : IIOD
    {
        public uint GroupLength
        {
            get { return _groupLength.Data; }
            set { _groupLength.Data = value; }
        }

        public byte[] InfoVersion
        {
            get { return _infoVersion.Data_.ToArray(); }
            set { _infoVersion.Data_ = value.ToList(); }
        }

        public string MediaStorageSOPClassUID
        {
            get { return _mediaStorageSOPClassUID.Data; }
            set { _mediaStorageSOPClassUID.Data = value; }
        }

        public string MediaStorageSOPInstanceUID
        {
            get { return _mediaStorageSOPInstanceUID.Data; }
            set { _mediaStorageSOPInstanceUID.Data = value; }
        }

        public string TransferSyntaxUID
        {
            get { return _transferSyntaxUID.Data; }
            set { _transferSyntaxUID.Data = value; }
        }

        public string ImplementationClassUID
        {
            get { return _implementationClassUID.Data; }
            set { _implementationClassUID.Data = value; }
        }

        public string ImplementationVersionName
        {
            get { return _implementationVersionName.Data; }
            set { _implementationVersionName.Data = value; }
        }

        public string SourceApplicationEntityTitle
        {
            get { return _sourceAETitle.Data; }
            set { _sourceAETitle.Data = value; }
        }

        public string PrivateInfoCreatorUID
        {
            get { return _privateInfoCreatorUID.Data; }
            set { _privateInfoCreatorUID.Data = value; }
        }

        public byte[] PrivateInfo
        {
            get { return _privateInfo.Data_.ToArray(); }
            set { _privateInfo.Data_ = value.ToList(); }
        }


        public List<IDICOMElement> Elements
        {
            get
            {
                return new List<IDICOMElement>
                {
                    _groupLength,
                    _infoVersion,
                    _mediaStorageSOPClassUID,
                    _mediaStorageSOPInstanceUID,
                    _transferSyntaxUID,
                    _implementationClassUID,
                    _implementationVersionName,
                    _sourceAETitle,
                    _privateInfoCreatorUID,
                    _privateInfo
                };
            }
        }

        #region PRIVATE

        private readonly UnsignedLong _groupLength = new UnsignedLong
        {
            Tag = TagHelper.FileMetaInformationGroupLength
        };

        private readonly UniqueIdentifier _implementationClassUID = new UniqueIdentifier
        {
            Tag = TagHelper.ImplementationClassUID
        };

        private readonly UniqueIdentifier _implementationVersionName = new UniqueIdentifier
        {
            Tag = TagHelper.ImplementationVersionName
        };

        private readonly OtherByteString _infoVersion = new OtherByteString
        {
            Tag = TagHelper.FileMetaInformationVersion
        };

        private readonly UniqueIdentifier _mediaStorageSOPClassUID = new UniqueIdentifier
        {
            Tag = TagHelper.MediaStorageSOPClassUID
        };

        private readonly UniqueIdentifier _mediaStorageSOPInstanceUID = new UniqueIdentifier
        {
            Tag = TagHelper.MediaStorageSOPInstanceUID
        };

        private readonly OtherByteString _privateInfo = new OtherByteString {Tag = TagHelper.PrivateInformation};

        private readonly UniqueIdentifier _privateInfoCreatorUID = new UniqueIdentifier
        {
            Tag = TagHelper.PrivateInformationCreatorUID
        };

        private readonly ApplicationEntity _sourceAETitle = new ApplicationEntity
        {
            Tag = TagHelper.SourceApplicationEntityTitle
        };

        private readonly UniqueIdentifier _transferSyntaxUID = new UniqueIdentifier
        {
            Tag = TagHelper.TransferSyntaxUID
        };

        #endregion
    }
}