using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Modules
{
    public class FileMetadata : IIOD
    {
        #region PRIVATE
        private UnsignedLong _groupLength = new UnsignedLong { Tag = TagHelper.FILE_META_INFORMATION_GROUP_LENGTH };
        private OtherByteString _infoVersion = new OtherByteString { Tag = TagHelper.FILE_META_INFORMATION_VERSION };
        private UniqueIdentifier _mediaStorageSOPClassUID = new UniqueIdentifier { Tag = TagHelper.MEDIA_STORAGE_SOPCLASS_UID };
        private UniqueIdentifier _mediaStorageSOPInstanceUID = new UniqueIdentifier { Tag = TagHelper.MEDIA_STORAGE_SOPINSTANCE_UID };
        private UniqueIdentifier _transferSyntaxUID = new UniqueIdentifier { Tag = TagHelper.TRANSFER_SYNTAX_UID };
        private UniqueIdentifier _implementationClassUID = new UniqueIdentifier { Tag = TagHelper.IMPLEMENTATION_CLASS_UID };
        private UniqueIdentifier _implementationVersionName = new UniqueIdentifier { Tag = TagHelper.IMPLEMENTATION_VERSION_NAME };
        private ApplicationEntity _sourceAETitle = new ApplicationEntity { Tag = TagHelper.SOURCE_APPLICATION_ENTITY_TITLE };
        private UniqueIdentifier _privateInfoCreatorUID = new UniqueIdentifier { Tag = TagHelper.PRIVATE_INFORMATION_CREATOR_UID };
        private OtherByteString _privateInfo = new OtherByteString { Tag = TagHelper.PRIVATE_INFORMATION };
        #endregion

        public uint[] GroupLength
        {
            get
            {
                return _groupLength.Data.MultipicityValue.ToArray();
            }
            set
            {
                _groupLength.Data.MultipicityValue = value.ToList();
            }
        }

        public byte[] InfoVersion
        {
            get
            {
                return _infoVersion.Data.MultipicityValue.ToArray();
            }
            set
            {
                _infoVersion.Data.MultipicityValue= value.ToList();
            }
        }

        public string MediaStorageSOPClassUID
        {
            get
            {
                return _mediaStorageSOPClassUID.Data;
            }
            set
            {
                _mediaStorageSOPClassUID.Data = value;
            }
        }

        public string MediaStorageSOPInstanceUID
        {
            get
            {
                return _mediaStorageSOPInstanceUID.Data;
            }
            set
            {
                _mediaStorageSOPInstanceUID.Data = value;
            }
        }

        public string TransferSyntaxUID
        {
            get
            {
                return _transferSyntaxUID.Data;
            }
            set
            {
                _transferSyntaxUID.Data = value;
            }
        }

        public string ImplementationClassUID
        {
            get
            {
                return _implementationClassUID.Data;
            }
            set
            {
                _implementationClassUID.Data = value;
            }
        }

        public string ImplementationVersionName
        {
            get
            {
                return _implementationVersionName.Data;
            }
            set
            {
                _implementationVersionName.Data = value;
            }
        }

        public string SourceApplicationEntityTitle
        {
            get
            {
                return _sourceAETitle.Data.SingleValue;
            }
            set
            {
                _sourceAETitle.Data.SingleValue = value;
            }
        }

        public string PrivateInfoCreatorUID
        {
            get
            {
                return _privateInfoCreatorUID.Data;
            }
            set
            {
                _privateInfoCreatorUID.Data = value;
            }
        }

        public byte[] PrivateInfo
        {
            get
            {
                return _privateInfo.Data.MultipicityValue.ToArray();
            }
            set
            {
                _privateInfo.Data.MultipicityValue = value.ToList();
            }
        }


        public List<IDICOMElement> Elements
        {
            get
            {
                return new List<IDICOMElement>() {
                _groupLength,_infoVersion,_mediaStorageSOPClassUID,_mediaStorageSOPInstanceUID,_transferSyntaxUID,_implementationClassUID,
                _implementationVersionName,_sourceAETitle,_privateInfoCreatorUID,_privateInfo
            };
            }
        }
    }
}
