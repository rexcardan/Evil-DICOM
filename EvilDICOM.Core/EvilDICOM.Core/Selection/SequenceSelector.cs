using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Selection
{
    public class SequenceSelector : Sequence
    {
        public new List<DICOMSelector> Items { get; set; }

        public SequenceSelector(Sequence s)
        {
            this.Items = new List<DICOMSelector>();
            foreach (var item in s.Items)
            {
                this.Items.Add(new DICOMSelector(item));
            }
            this.Tag = s.Tag;
        }

        public Sequence ToSequence()
        {
            Sequence s = new Sequence();
            s.Tag = this.Tag;
            foreach(var item in Items){
                s.Items.Add(item.ToDICOMObject());
            }
            return s;
        }
        #region SELECTORS
        public UnsignedLong CommandGroupLength { get { return Items.FindFirst<UnsignedLong>("00000000") as UnsignedLong; } }
        public List<UnsignedLong> CommandGroupLength_ { get { return Items.FindAll<UnsignedLong>("00000000").ToList(); } }
        public UnsignedLong CommandLengthToEndRetired { get { return Items.FindFirst<UnsignedLong>("00000001") as UnsignedLong; } }
        public List<UnsignedLong> CommandLengthToEndRetired_ { get { return Items.FindAll<UnsignedLong>("00000001").ToList(); } }
        public UniqueIdentifier AffectedSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("00000002") as UniqueIdentifier; } }
        public List<UniqueIdentifier> AffectedSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00000002").ToList(); } }
        public UniqueIdentifier RequestedSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("00000003") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RequestedSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00000003").ToList(); } }
        public ShortString CommandRecognitionCodeRetired { get { return Items.FindFirst<ShortString>("00000010") as ShortString; } }
        public List<ShortString> CommandRecognitionCodeRetired_ { get { return Items.FindAll<ShortString>("00000010").ToList(); } }
        public UnsignedShort CommandField { get { return Items.FindFirst<UnsignedShort>("00000100") as UnsignedShort; } }
        public List<UnsignedShort> CommandField_ { get { return Items.FindAll<UnsignedShort>("00000100").ToList(); } }
        public UnsignedShort MessageID { get { return Items.FindFirst<UnsignedShort>("00000110") as UnsignedShort; } }
        public List<UnsignedShort> MessageID_ { get { return Items.FindAll<UnsignedShort>("00000110").ToList(); } }
        public UnsignedShort MessageIDBeingRespondedTo { get { return Items.FindFirst<UnsignedShort>("00000120") as UnsignedShort; } }
        public List<UnsignedShort> MessageIDBeingRespondedTo_ { get { return Items.FindAll<UnsignedShort>("00000120").ToList(); } }
        public ApplicationEntity InitiatorRetired { get { return Items.FindFirst<ApplicationEntity>("00000200") as ApplicationEntity; } }
        public List<ApplicationEntity> InitiatorRetired_ { get { return Items.FindAll<ApplicationEntity>("00000200").ToList(); } }
        public ApplicationEntity ReceiverRetired { get { return Items.FindFirst<ApplicationEntity>("00000300") as ApplicationEntity; } }
        public List<ApplicationEntity> ReceiverRetired_ { get { return Items.FindAll<ApplicationEntity>("00000300").ToList(); } }
        public ApplicationEntity FindLocationRetired { get { return Items.FindFirst<ApplicationEntity>("00000400") as ApplicationEntity; } }
        public List<ApplicationEntity> FindLocationRetired_ { get { return Items.FindAll<ApplicationEntity>("00000400").ToList(); } }
        public ApplicationEntity MoveDestination { get { return Items.FindFirst<ApplicationEntity>("00000600") as ApplicationEntity; } }
        public List<ApplicationEntity> MoveDestination_ { get { return Items.FindAll<ApplicationEntity>("00000600").ToList(); } }
        public UnsignedShort Priority { get { return Items.FindFirst<UnsignedShort>("00000700") as UnsignedShort; } }
        public List<UnsignedShort> Priority_ { get { return Items.FindAll<UnsignedShort>("00000700").ToList(); } }
        public UnsignedShort CommandDataSetType { get { return Items.FindFirst<UnsignedShort>("00000800") as UnsignedShort; } }
        public List<UnsignedShort> CommandDataSetType_ { get { return Items.FindAll<UnsignedShort>("00000800").ToList(); } }
        public UnsignedShort NumberOfMatchesRetired { get { return Items.FindFirst<UnsignedShort>("00000850") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfMatchesRetired_ { get { return Items.FindAll<UnsignedShort>("00000850").ToList(); } }
        public UnsignedShort ResponseSequenceNumberRetired { get { return Items.FindFirst<UnsignedShort>("00000860") as UnsignedShort; } }
        public List<UnsignedShort> ResponseSequenceNumberRetired_ { get { return Items.FindAll<UnsignedShort>("00000860").ToList(); } }
        public UnsignedShort Status { get { return Items.FindFirst<UnsignedShort>("00000900") as UnsignedShort; } }
        public List<UnsignedShort> Status_ { get { return Items.FindAll<UnsignedShort>("00000900").ToList(); } }
        public AttributeTag OffendingElement { get { return Items.FindFirst<AttributeTag>("00000901") as AttributeTag; } }
        public List<AttributeTag> OffendingElement_ { get { return Items.FindAll<AttributeTag>("00000901").ToList(); } }
        public LongString ErrorComment { get { return Items.FindFirst<LongString>("00000902") as LongString; } }
        public List<LongString> ErrorComment_ { get { return Items.FindAll<LongString>("00000902").ToList(); } }
        public UnsignedShort ErrorID { get { return Items.FindFirst<UnsignedShort>("00000903") as UnsignedShort; } }
        public List<UnsignedShort> ErrorID_ { get { return Items.FindAll<UnsignedShort>("00000903").ToList(); } }
        public UniqueIdentifier AffectedSOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00001000") as UniqueIdentifier; } }
        public List<UniqueIdentifier> AffectedSOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00001000").ToList(); } }
        public UniqueIdentifier RequestedSOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00001001") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RequestedSOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00001001").ToList(); } }
        public UnsignedShort EventTypeID { get { return Items.FindFirst<UnsignedShort>("00001002") as UnsignedShort; } }
        public List<UnsignedShort> EventTypeID_ { get { return Items.FindAll<UnsignedShort>("00001002").ToList(); } }
        public AttributeTag AttributeIdentifierList { get { return Items.FindFirst<AttributeTag>("00001005") as AttributeTag; } }
        public List<AttributeTag> AttributeIdentifierList_ { get { return Items.FindAll<AttributeTag>("00001005").ToList(); } }
        public UnsignedShort ActionTypeID { get { return Items.FindFirst<UnsignedShort>("00001008") as UnsignedShort; } }
        public List<UnsignedShort> ActionTypeID_ { get { return Items.FindAll<UnsignedShort>("00001008").ToList(); } }
        public UnsignedShort NumberOfRemainingSuboperations { get { return Items.FindFirst<UnsignedShort>("00001020") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfRemainingSuboperations_ { get { return Items.FindAll<UnsignedShort>("00001020").ToList(); } }
        public UnsignedShort NumberOfCompletedSuboperations { get { return Items.FindFirst<UnsignedShort>("00001021") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfCompletedSuboperations_ { get { return Items.FindAll<UnsignedShort>("00001021").ToList(); } }
        public UnsignedShort NumberOfFailedSuboperations { get { return Items.FindFirst<UnsignedShort>("00001022") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfFailedSuboperations_ { get { return Items.FindAll<UnsignedShort>("00001022").ToList(); } }
        public UnsignedShort NumberOfWarningSuboperations { get { return Items.FindFirst<UnsignedShort>("00001023") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfWarningSuboperations_ { get { return Items.FindAll<UnsignedShort>("00001023").ToList(); } }
        public ApplicationEntity MoveOriginatorApplicationEntityTitle { get { return Items.FindFirst<ApplicationEntity>("00001030") as ApplicationEntity; } }
        public List<ApplicationEntity> MoveOriginatorApplicationEntityTitle_ { get { return Items.FindAll<ApplicationEntity>("00001030").ToList(); } }
        public UnsignedShort MoveOriginatorMessageID { get { return Items.FindFirst<UnsignedShort>("00001031") as UnsignedShort; } }
        public List<UnsignedShort> MoveOriginatorMessageID_ { get { return Items.FindAll<UnsignedShort>("00001031").ToList(); } }
        public LongText DialogReceiverRetired { get { return Items.FindFirst<LongText>("00004000") as LongText; } }
        public List<LongText> DialogReceiverRetired_ { get { return Items.FindAll<LongText>("00004000").ToList(); } }
        public LongText TerminalTypeRetired { get { return Items.FindFirst<LongText>("00004010") as LongText; } }
        public List<LongText> TerminalTypeRetired_ { get { return Items.FindAll<LongText>("00004010").ToList(); } }
        public ShortString MessageSetIDRetired { get { return Items.FindFirst<ShortString>("00005010") as ShortString; } }
        public List<ShortString> MessageSetIDRetired_ { get { return Items.FindAll<ShortString>("00005010").ToList(); } }
        public ShortString EndMessageIDRetired { get { return Items.FindFirst<ShortString>("00005020") as ShortString; } }
        public List<ShortString> EndMessageIDRetired_ { get { return Items.FindAll<ShortString>("00005020").ToList(); } }
        public LongText DisplayFormatRetired { get { return Items.FindFirst<LongText>("00005110") as LongText; } }
        public List<LongText> DisplayFormatRetired_ { get { return Items.FindAll<LongText>("00005110").ToList(); } }
        public LongText PagePositionIDRetired { get { return Items.FindFirst<LongText>("00005120") as LongText; } }
        public List<LongText> PagePositionIDRetired_ { get { return Items.FindAll<LongText>("00005120").ToList(); } }
        public CodeString TextFormatIDRetired { get { return Items.FindFirst<CodeString>("00005130") as CodeString; } }
        public List<CodeString> TextFormatIDRetired_ { get { return Items.FindAll<CodeString>("00005130").ToList(); } }
        public CodeString NormalReverseRetired { get { return Items.FindFirst<CodeString>("00005140") as CodeString; } }
        public List<CodeString> NormalReverseRetired_ { get { return Items.FindAll<CodeString>("00005140").ToList(); } }
        public CodeString AddGrayScaleRetired { get { return Items.FindFirst<CodeString>("00005150") as CodeString; } }
        public List<CodeString> AddGrayScaleRetired_ { get { return Items.FindAll<CodeString>("00005150").ToList(); } }
        public CodeString BordersRetired { get { return Items.FindFirst<CodeString>("00005160") as CodeString; } }
        public List<CodeString> BordersRetired_ { get { return Items.FindAll<CodeString>("00005160").ToList(); } }
        public IntegerString CopiesRetired { get { return Items.FindFirst<IntegerString>("00005170") as IntegerString; } }
        public List<IntegerString> CopiesRetired_ { get { return Items.FindAll<IntegerString>("00005170").ToList(); } }
        public CodeString CommandMagnificationTypeRetired { get { return Items.FindFirst<CodeString>("00005180") as CodeString; } }
        public List<CodeString> CommandMagnificationTypeRetired_ { get { return Items.FindAll<CodeString>("00005180").ToList(); } }
        public CodeString EraseRetired { get { return Items.FindFirst<CodeString>("00005190") as CodeString; } }
        public List<CodeString> EraseRetired_ { get { return Items.FindAll<CodeString>("00005190").ToList(); } }
        public CodeString PrintRetired { get { return Items.FindFirst<CodeString>("000051A0") as CodeString; } }
        public List<CodeString> PrintRetired_ { get { return Items.FindAll<CodeString>("000051A0").ToList(); } }
        public UnsignedShort OverlaysRetired { get { return Items.FindFirst<UnsignedShort>("000051B0") as UnsignedShort; } }
        public List<UnsignedShort> OverlaysRetired_ { get { return Items.FindAll<UnsignedShort>("000051B0").ToList(); } }
        public UnsignedLong FileMetaInformationGroupLength { get { return Items.FindFirst<UnsignedLong>("00020000") as UnsignedLong; } }
        public List<UnsignedLong> FileMetaInformationGroupLength_ { get { return Items.FindAll<UnsignedLong>("00020000").ToList(); } }
        public OtherByteString FileMetaInformationVersion { get { return Items.FindFirst<OtherByteString>("00020001") as OtherByteString; } }
        public List<OtherByteString> FileMetaInformationVersion_ { get { return Items.FindAll<OtherByteString>("00020001").ToList(); } }
        public UniqueIdentifier MediaStorageSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("00020002") as UniqueIdentifier; } }
        public List<UniqueIdentifier> MediaStorageSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00020002").ToList(); } }
        public UniqueIdentifier MediaStorageSOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00020003") as UniqueIdentifier; } }
        public List<UniqueIdentifier> MediaStorageSOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00020003").ToList(); } }
        public UniqueIdentifier TransferSyntaxUID { get { return Items.FindFirst<UniqueIdentifier>("00020010") as UniqueIdentifier; } }
        public List<UniqueIdentifier> TransferSyntaxUID_ { get { return Items.FindAll<UniqueIdentifier>("00020010").ToList(); } }
        public UniqueIdentifier ImplementationClassUID { get { return Items.FindFirst<UniqueIdentifier>("00020012") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ImplementationClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00020012").ToList(); } }
        public ShortString ImplementationVersionName { get { return Items.FindFirst<ShortString>("00020013") as ShortString; } }
        public List<ShortString> ImplementationVersionName_ { get { return Items.FindAll<ShortString>("00020013").ToList(); } }
        public ApplicationEntity SourceApplicationEntityTitle { get { return Items.FindFirst<ApplicationEntity>("00020016") as ApplicationEntity; } }
        public List<ApplicationEntity> SourceApplicationEntityTitle_ { get { return Items.FindAll<ApplicationEntity>("00020016").ToList(); } }
        public UniqueIdentifier PrivateInformationCreatorUID { get { return Items.FindFirst<UniqueIdentifier>("00020100") as UniqueIdentifier; } }
        public List<UniqueIdentifier> PrivateInformationCreatorUID_ { get { return Items.FindAll<UniqueIdentifier>("00020100").ToList(); } }
        public OtherByteString PrivateInformation { get { return Items.FindFirst<OtherByteString>("00020102") as OtherByteString; } }
        public List<OtherByteString> PrivateInformation_ { get { return Items.FindAll<OtherByteString>("00020102").ToList(); } }
        public CodeString FileSetID { get { return Items.FindFirst<CodeString>("00041130") as CodeString; } }
        public List<CodeString> FileSetID_ { get { return Items.FindAll<CodeString>("00041130").ToList(); } }
        public CodeString FileSetDescriptorFileID { get { return Items.FindFirst<CodeString>("00041141") as CodeString; } }
        public List<CodeString> FileSetDescriptorFileID_ { get { return Items.FindAll<CodeString>("00041141").ToList(); } }
        public CodeString SpecificCharacterSetOfFileSetDescriptorFile { get { return Items.FindFirst<CodeString>("00041142") as CodeString; } }
        public List<CodeString> SpecificCharacterSetOfFileSetDescriptorFile_ { get { return Items.FindAll<CodeString>("00041142").ToList(); } }
        public UnsignedLong OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity { get { return Items.FindFirst<UnsignedLong>("00041200") as UnsignedLong; } }
        public List<UnsignedLong> OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity_ { get { return Items.FindAll<UnsignedLong>("00041200").ToList(); } }
        public UnsignedLong OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity { get { return Items.FindFirst<UnsignedLong>("00041202") as UnsignedLong; } }
        public List<UnsignedLong> OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity_ { get { return Items.FindAll<UnsignedLong>("00041202").ToList(); } }
        public UnsignedShort FileSetConsistencyFlag { get { return Items.FindFirst<UnsignedShort>("00041212") as UnsignedShort; } }
        public List<UnsignedShort> FileSetConsistencyFlag_ { get { return Items.FindAll<UnsignedShort>("00041212").ToList(); } }
        public SequenceSelector DirectoryRecordSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00041220")); } }
        public List<SequenceSelector> DirectoryRecordSequence_ { get { return Items.FindAll<Sequence>("00041220").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong OffsetOfTheNextDirectoryRecord { get { return Items.FindFirst<UnsignedLong>("00041400") as UnsignedLong; } }
        public List<UnsignedLong> OffsetOfTheNextDirectoryRecord_ { get { return Items.FindAll<UnsignedLong>("00041400").ToList(); } }
        public UnsignedShort RecordInUseFlag { get { return Items.FindFirst<UnsignedShort>("00041410") as UnsignedShort; } }
        public List<UnsignedShort> RecordInUseFlag_ { get { return Items.FindAll<UnsignedShort>("00041410").ToList(); } }
        public UnsignedLong OffsetOfReferencedLowerLevelDirectoryEntity { get { return Items.FindFirst<UnsignedLong>("00041420") as UnsignedLong; } }
        public List<UnsignedLong> OffsetOfReferencedLowerLevelDirectoryEntity_ { get { return Items.FindAll<UnsignedLong>("00041420").ToList(); } }
        public CodeString DirectoryRecordType { get { return Items.FindFirst<CodeString>("00041430") as CodeString; } }
        public List<CodeString> DirectoryRecordType_ { get { return Items.FindAll<CodeString>("00041430").ToList(); } }
        public UniqueIdentifier PrivateRecordUID { get { return Items.FindFirst<UniqueIdentifier>("00041432") as UniqueIdentifier; } }
        public List<UniqueIdentifier> PrivateRecordUID_ { get { return Items.FindAll<UniqueIdentifier>("00041432").ToList(); } }
        public CodeString ReferencedFileID { get { return Items.FindFirst<CodeString>("00041500") as CodeString; } }
        public List<CodeString> ReferencedFileID_ { get { return Items.FindAll<CodeString>("00041500").ToList(); } }
        public UnsignedLong MRDRDirectoryRecordOffsetRetired { get { return Items.FindFirst<UnsignedLong>("00041504") as UnsignedLong; } }
        public List<UnsignedLong> MRDRDirectoryRecordOffsetRetired_ { get { return Items.FindAll<UnsignedLong>("00041504").ToList(); } }
        public UniqueIdentifier ReferencedSOPClassUIDInFile { get { return Items.FindFirst<UniqueIdentifier>("00041510") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedSOPClassUIDInFile_ { get { return Items.FindAll<UniqueIdentifier>("00041510").ToList(); } }
        public UniqueIdentifier ReferencedSOPInstanceUIDInFile { get { return Items.FindFirst<UniqueIdentifier>("00041511") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedSOPInstanceUIDInFile_ { get { return Items.FindAll<UniqueIdentifier>("00041511").ToList(); } }
        public UniqueIdentifier ReferencedTransferSyntaxUIDInFile { get { return Items.FindFirst<UniqueIdentifier>("00041512") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedTransferSyntaxUIDInFile_ { get { return Items.FindAll<UniqueIdentifier>("00041512").ToList(); } }
        public UniqueIdentifier ReferencedRelatedGeneralSOPClassUIDInFile { get { return Items.FindFirst<UniqueIdentifier>("0004151A") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedRelatedGeneralSOPClassUIDInFile_ { get { return Items.FindAll<UniqueIdentifier>("0004151A").ToList(); } }
        public UnsignedLong NumberOfReferencesRetired { get { return Items.FindFirst<UnsignedLong>("00041600") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfReferencesRetired_ { get { return Items.FindAll<UnsignedLong>("00041600").ToList(); } }
        public UnsignedLong LengthToEndRetired { get { return Items.FindFirst<UnsignedLong>("00080001") as UnsignedLong; } }
        public List<UnsignedLong> LengthToEndRetired_ { get { return Items.FindAll<UnsignedLong>("00080001").ToList(); } }
        public CodeString SpecificCharacterSet { get { return Items.FindFirst<CodeString>("00080005") as CodeString; } }
        public List<CodeString> SpecificCharacterSet_ { get { return Items.FindAll<CodeString>("00080005").ToList(); } }
        public SequenceSelector LanguageCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00080006")); } }
        public List<SequenceSelector> LanguageCodeSequence_ { get { return Items.FindAll<Sequence>("00080006").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ImageType { get { return Items.FindFirst<CodeString>("00080008") as CodeString; } }
        public List<CodeString> ImageType_ { get { return Items.FindAll<CodeString>("00080008").ToList(); } }
        public ShortString RecognitionCodeRetired { get { return Items.FindFirst<ShortString>("00080010") as ShortString; } }
        public List<ShortString> RecognitionCodeRetired_ { get { return Items.FindAll<ShortString>("00080010").ToList(); } }
        public Date InstanceCreationDate { get { return Items.FindFirst<Date>("00080012") as Date; } }
        public List<Date> InstanceCreationDate_ { get { return Items.FindAll<Date>("00080012").ToList(); } }
        public Time InstanceCreationTime { get { return Items.FindFirst<Time>("00080013") as Time; } }
        public List<Time> InstanceCreationTime_ { get { return Items.FindAll<Time>("00080013").ToList(); } }
        public UniqueIdentifier InstanceCreatorUID { get { return Items.FindFirst<UniqueIdentifier>("00080014") as UniqueIdentifier; } }
        public List<UniqueIdentifier> InstanceCreatorUID_ { get { return Items.FindAll<UniqueIdentifier>("00080014").ToList(); } }
        public UniqueIdentifier SOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("00080016") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00080016").ToList(); } }
        public UniqueIdentifier SOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00080018") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00080018").ToList(); } }
        public UniqueIdentifier RelatedGeneralSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("0008001A") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RelatedGeneralSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("0008001A").ToList(); } }
        public UniqueIdentifier OriginalSpecializedSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("0008001B") as UniqueIdentifier; } }
        public List<UniqueIdentifier> OriginalSpecializedSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("0008001B").ToList(); } }
        public Date StudyDate { get { return Items.FindFirst<Date>("00080020") as Date; } }
        public List<Date> StudyDate_ { get { return Items.FindAll<Date>("00080020").ToList(); } }
        public Date SeriesDate { get { return Items.FindFirst<Date>("00080021") as Date; } }
        public List<Date> SeriesDate_ { get { return Items.FindAll<Date>("00080021").ToList(); } }
        public Date AcquisitionDate { get { return Items.FindFirst<Date>("00080022") as Date; } }
        public List<Date> AcquisitionDate_ { get { return Items.FindAll<Date>("00080022").ToList(); } }
        public Date ContentDate { get { return Items.FindFirst<Date>("00080023") as Date; } }
        public List<Date> ContentDate_ { get { return Items.FindAll<Date>("00080023").ToList(); } }
        public Date OverlayDateRetired { get { return Items.FindFirst<Date>("00080024") as Date; } }
        public List<Date> OverlayDateRetired_ { get { return Items.FindAll<Date>("00080024").ToList(); } }
        public Date CurveDateRetired { get { return Items.FindFirst<Date>("00080025") as Date; } }
        public List<Date> CurveDateRetired_ { get { return Items.FindAll<Date>("00080025").ToList(); } }
        public EvilDICOM.Core.Element.DateTime AcquisitionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0008002A") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> AcquisitionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0008002A").ToList(); } }
        public Time StudyTime { get { return Items.FindFirst<Time>("00080030") as Time; } }
        public List<Time> StudyTime_ { get { return Items.FindAll<Time>("00080030").ToList(); } }
        public Time SeriesTime { get { return Items.FindFirst<Time>("00080031") as Time; } }
        public List<Time> SeriesTime_ { get { return Items.FindAll<Time>("00080031").ToList(); } }
        public Time AcquisitionTime { get { return Items.FindFirst<Time>("00080032") as Time; } }
        public List<Time> AcquisitionTime_ { get { return Items.FindAll<Time>("00080032").ToList(); } }
        public Time ContentTime { get { return Items.FindFirst<Time>("00080033") as Time; } }
        public List<Time> ContentTime_ { get { return Items.FindAll<Time>("00080033").ToList(); } }
        public Time OverlayTimeRetired { get { return Items.FindFirst<Time>("00080034") as Time; } }
        public List<Time> OverlayTimeRetired_ { get { return Items.FindAll<Time>("00080034").ToList(); } }
        public Time CurveTimeRetired { get { return Items.FindFirst<Time>("00080035") as Time; } }
        public List<Time> CurveTimeRetired_ { get { return Items.FindAll<Time>("00080035").ToList(); } }
        public UnsignedShort DataSetTypeRetired { get { return Items.FindFirst<UnsignedShort>("00080040") as UnsignedShort; } }
        public List<UnsignedShort> DataSetTypeRetired_ { get { return Items.FindAll<UnsignedShort>("00080040").ToList(); } }
        public LongString DataSetSubtypeRetired { get { return Items.FindFirst<LongString>("00080041") as LongString; } }
        public List<LongString> DataSetSubtypeRetired_ { get { return Items.FindAll<LongString>("00080041").ToList(); } }
        public CodeString NuclearMedicineSeriesTypeRetired { get { return Items.FindFirst<CodeString>("00080042") as CodeString; } }
        public List<CodeString> NuclearMedicineSeriesTypeRetired_ { get { return Items.FindAll<CodeString>("00080042").ToList(); } }
        public ShortString AccessionNumber { get { return Items.FindFirst<ShortString>("00080050") as ShortString; } }
        public List<ShortString> AccessionNumber_ { get { return Items.FindAll<ShortString>("00080050").ToList(); } }
        public SequenceSelector IssuerOfAccessionNumberSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00080051")); } }
        public List<SequenceSelector> IssuerOfAccessionNumberSequence_ { get { return Items.FindAll<Sequence>("00080051").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString QueryRetrieveLevel { get { return Items.FindFirst<CodeString>("00080052") as CodeString; } }
        public List<CodeString> QueryRetrieveLevel_ { get { return Items.FindAll<CodeString>("00080052").ToList(); } }
        public ApplicationEntity RetrieveAETitle { get { return Items.FindFirst<ApplicationEntity>("00080054") as ApplicationEntity; } }
        public List<ApplicationEntity> RetrieveAETitle_ { get { return Items.FindAll<ApplicationEntity>("00080054").ToList(); } }
        public CodeString InstanceAvailability { get { return Items.FindFirst<CodeString>("00080056") as CodeString; } }
        public List<CodeString> InstanceAvailability_ { get { return Items.FindAll<CodeString>("00080056").ToList(); } }
        public UniqueIdentifier FailedSOPInstanceUIDList { get { return Items.FindFirst<UniqueIdentifier>("00080058") as UniqueIdentifier; } }
        public List<UniqueIdentifier> FailedSOPInstanceUIDList_ { get { return Items.FindAll<UniqueIdentifier>("00080058").ToList(); } }
        public CodeString Modality { get { return Items.FindFirst<CodeString>("00080060") as CodeString; } }
        public List<CodeString> Modality_ { get { return Items.FindAll<CodeString>("00080060").ToList(); } }
        public CodeString ModalitiesInStudy { get { return Items.FindFirst<CodeString>("00080061") as CodeString; } }
        public List<CodeString> ModalitiesInStudy_ { get { return Items.FindAll<CodeString>("00080061").ToList(); } }
        public UniqueIdentifier SOPClassesInStudy { get { return Items.FindFirst<UniqueIdentifier>("00080062") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SOPClassesInStudy_ { get { return Items.FindAll<UniqueIdentifier>("00080062").ToList(); } }
        public CodeString ConversionType { get { return Items.FindFirst<CodeString>("00080064") as CodeString; } }
        public List<CodeString> ConversionType_ { get { return Items.FindAll<CodeString>("00080064").ToList(); } }
        public CodeString PresentationIntentType { get { return Items.FindFirst<CodeString>("00080068") as CodeString; } }
        public List<CodeString> PresentationIntentType_ { get { return Items.FindAll<CodeString>("00080068").ToList(); } }
        public LongString Manufacturer { get { return Items.FindFirst<LongString>("00080070") as LongString; } }
        public List<LongString> Manufacturer_ { get { return Items.FindAll<LongString>("00080070").ToList(); } }
        public LongString InstitutionName { get { return Items.FindFirst<LongString>("00080080") as LongString; } }
        public List<LongString> InstitutionName_ { get { return Items.FindAll<LongString>("00080080").ToList(); } }
        public ShortText InstitutionAddress { get { return Items.FindFirst<ShortText>("00080081") as ShortText; } }
        public List<ShortText> InstitutionAddress_ { get { return Items.FindAll<ShortText>("00080081").ToList(); } }
        public SequenceSelector InstitutionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00080082")); } }
        public List<SequenceSelector> InstitutionCodeSequence_ { get { return Items.FindAll<Sequence>("00080082").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName ReferringPhysicianName { get { return Items.FindFirst<PersonName>("00080090") as PersonName; } }
        public List<PersonName> ReferringPhysicianName_ { get { return Items.FindAll<PersonName>("00080090").ToList(); } }
        public ShortText ReferringPhysicianAddress { get { return Items.FindFirst<ShortText>("00080092") as ShortText; } }
        public List<ShortText> ReferringPhysicianAddress_ { get { return Items.FindAll<ShortText>("00080092").ToList(); } }
        public ShortString ReferringPhysicianTelephoneNumbers { get { return Items.FindFirst<ShortString>("00080094") as ShortString; } }
        public List<ShortString> ReferringPhysicianTelephoneNumbers_ { get { return Items.FindAll<ShortString>("00080094").ToList(); } }
        public SequenceSelector ReferringPhysicianIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00080096")); } }
        public List<SequenceSelector> ReferringPhysicianIdentificationSequence_ { get { return Items.FindAll<Sequence>("00080096").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString CodeValue { get { return Items.FindFirst<ShortString>("00080100") as ShortString; } }
        public List<ShortString> CodeValue_ { get { return Items.FindAll<ShortString>("00080100").ToList(); } }
        public ShortString CodingSchemeDesignator { get { return Items.FindFirst<ShortString>("00080102") as ShortString; } }
        public List<ShortString> CodingSchemeDesignator_ { get { return Items.FindAll<ShortString>("00080102").ToList(); } }
        public ShortString CodingSchemeVersion { get { return Items.FindFirst<ShortString>("00080103") as ShortString; } }
        public List<ShortString> CodingSchemeVersion_ { get { return Items.FindAll<ShortString>("00080103").ToList(); } }
        public LongString CodeMeaning { get { return Items.FindFirst<LongString>("00080104") as LongString; } }
        public List<LongString> CodeMeaning_ { get { return Items.FindAll<LongString>("00080104").ToList(); } }
        public CodeString MappingResource { get { return Items.FindFirst<CodeString>("00080105") as CodeString; } }
        public List<CodeString> MappingResource_ { get { return Items.FindAll<CodeString>("00080105").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ContextGroupVersion { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00080106") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ContextGroupVersion_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00080106").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ContextGroupLocalVersion { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00080107") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ContextGroupLocalVersion_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00080107").ToList(); } }
        public CodeString ContextGroupExtensionFlag { get { return Items.FindFirst<CodeString>("0008010B") as CodeString; } }
        public List<CodeString> ContextGroupExtensionFlag_ { get { return Items.FindAll<CodeString>("0008010B").ToList(); } }
        public UniqueIdentifier CodingSchemeUID { get { return Items.FindFirst<UniqueIdentifier>("0008010C") as UniqueIdentifier; } }
        public List<UniqueIdentifier> CodingSchemeUID_ { get { return Items.FindAll<UniqueIdentifier>("0008010C").ToList(); } }
        public UniqueIdentifier ContextGroupExtensionCreatorUID { get { return Items.FindFirst<UniqueIdentifier>("0008010D") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ContextGroupExtensionCreatorUID_ { get { return Items.FindAll<UniqueIdentifier>("0008010D").ToList(); } }
        public CodeString ContextIdentifier { get { return Items.FindFirst<CodeString>("0008010F") as CodeString; } }
        public List<CodeString> ContextIdentifier_ { get { return Items.FindAll<CodeString>("0008010F").ToList(); } }
        public SequenceSelector CodingSchemeIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00080110")); } }
        public List<SequenceSelector> CodingSchemeIdentificationSequence_ { get { return Items.FindAll<Sequence>("00080110").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString CodingSchemeRegistry { get { return Items.FindFirst<LongString>("00080112") as LongString; } }
        public List<LongString> CodingSchemeRegistry_ { get { return Items.FindAll<LongString>("00080112").ToList(); } }
        public ShortText CodingSchemeExternalID { get { return Items.FindFirst<ShortText>("00080114") as ShortText; } }
        public List<ShortText> CodingSchemeExternalID_ { get { return Items.FindAll<ShortText>("00080114").ToList(); } }
        public ShortText CodingSchemeName { get { return Items.FindFirst<ShortText>("00080115") as ShortText; } }
        public List<ShortText> CodingSchemeName_ { get { return Items.FindAll<ShortText>("00080115").ToList(); } }
        public ShortText CodingSchemeResponsibleOrganization { get { return Items.FindFirst<ShortText>("00080116") as ShortText; } }
        public List<ShortText> CodingSchemeResponsibleOrganization_ { get { return Items.FindAll<ShortText>("00080116").ToList(); } }
        public UniqueIdentifier ContextUID { get { return Items.FindFirst<UniqueIdentifier>("00080117") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ContextUID_ { get { return Items.FindAll<UniqueIdentifier>("00080117").ToList(); } }
        public ShortString TimezoneOffsetFromUTC { get { return Items.FindFirst<ShortString>("00080201") as ShortString; } }
        public List<ShortString> TimezoneOffsetFromUTC_ { get { return Items.FindAll<ShortString>("00080201").ToList(); } }
        public ApplicationEntity NetworkIDRetired { get { return Items.FindFirst<ApplicationEntity>("00081000") as ApplicationEntity; } }
        public List<ApplicationEntity> NetworkIDRetired_ { get { return Items.FindAll<ApplicationEntity>("00081000").ToList(); } }
        public ShortString StationName { get { return Items.FindFirst<ShortString>("00081010") as ShortString; } }
        public List<ShortString> StationName_ { get { return Items.FindAll<ShortString>("00081010").ToList(); } }
        public LongString StudyDescription { get { return Items.FindFirst<LongString>("00081030") as LongString; } }
        public List<LongString> StudyDescription_ { get { return Items.FindAll<LongString>("00081030").ToList(); } }
        public SequenceSelector ProcedureCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081032")); } }
        public List<SequenceSelector> ProcedureCodeSequence_ { get { return Items.FindAll<Sequence>("00081032").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SeriesDescription { get { return Items.FindFirst<LongString>("0008103E") as LongString; } }
        public List<LongString> SeriesDescription_ { get { return Items.FindAll<LongString>("0008103E").ToList(); } }
        public SequenceSelector SeriesDescriptionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008103F")); } }
        public List<SequenceSelector> SeriesDescriptionCodeSequence_ { get { return Items.FindAll<Sequence>("0008103F").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString InstitutionalDepartmentName { get { return Items.FindFirst<LongString>("00081040") as LongString; } }
        public List<LongString> InstitutionalDepartmentName_ { get { return Items.FindAll<LongString>("00081040").ToList(); } }
        public PersonName PhysiciansOfRecord { get { return Items.FindFirst<PersonName>("00081048") as PersonName; } }
        public List<PersonName> PhysiciansOfRecord_ { get { return Items.FindAll<PersonName>("00081048").ToList(); } }
        public SequenceSelector PhysiciansOfRecordIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081049")); } }
        public List<SequenceSelector> PhysiciansOfRecordIdentificationSequence_ { get { return Items.FindAll<Sequence>("00081049").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName PerformingPhysicianName { get { return Items.FindFirst<PersonName>("00081050") as PersonName; } }
        public List<PersonName> PerformingPhysicianName_ { get { return Items.FindAll<PersonName>("00081050").ToList(); } }
        public SequenceSelector PerformingPhysicianIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081052")); } }
        public List<SequenceSelector> PerformingPhysicianIdentificationSequence_ { get { return Items.FindAll<Sequence>("00081052").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName NameOfPhysiciansReadingStudy { get { return Items.FindFirst<PersonName>("00081060") as PersonName; } }
        public List<PersonName> NameOfPhysiciansReadingStudy_ { get { return Items.FindAll<PersonName>("00081060").ToList(); } }
        public SequenceSelector PhysiciansReadingStudyIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081062")); } }
        public List<SequenceSelector> PhysiciansReadingStudyIdentificationSequence_ { get { return Items.FindAll<Sequence>("00081062").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName OperatorsName { get { return Items.FindFirst<PersonName>("00081070") as PersonName; } }
        public List<PersonName> OperatorsName_ { get { return Items.FindAll<PersonName>("00081070").ToList(); } }
        public SequenceSelector OperatorIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081072")); } }
        public List<SequenceSelector> OperatorIdentificationSequence_ { get { return Items.FindAll<Sequence>("00081072").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString AdmittingDiagnosesDescription { get { return Items.FindFirst<LongString>("00081080") as LongString; } }
        public List<LongString> AdmittingDiagnosesDescription_ { get { return Items.FindAll<LongString>("00081080").ToList(); } }
        public SequenceSelector AdmittingDiagnosesCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081084")); } }
        public List<SequenceSelector> AdmittingDiagnosesCodeSequence_ { get { return Items.FindAll<Sequence>("00081084").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ManufacturerModelName { get { return Items.FindFirst<LongString>("00081090") as LongString; } }
        public List<LongString> ManufacturerModelName_ { get { return Items.FindAll<LongString>("00081090").ToList(); } }
        public SequenceSelector ReferencedResultsSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081100")); } }
        public List<SequenceSelector> ReferencedResultsSequenceRetired_ { get { return Items.FindAll<Sequence>("00081100").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedStudySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081110")); } }
        public List<SequenceSelector> ReferencedStudySequence_ { get { return Items.FindAll<Sequence>("00081110").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedPerformedProcedureStepSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081111")); } }
        public List<SequenceSelector> ReferencedPerformedProcedureStepSequence_ { get { return Items.FindAll<Sequence>("00081111").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedSeriesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081115")); } }
        public List<SequenceSelector> ReferencedSeriesSequence_ { get { return Items.FindAll<Sequence>("00081115").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedPatientSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081120")); } }
        public List<SequenceSelector> ReferencedPatientSequence_ { get { return Items.FindAll<Sequence>("00081120").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedVisitSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081125")); } }
        public List<SequenceSelector> ReferencedVisitSequence_ { get { return Items.FindAll<Sequence>("00081125").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedOverlaySequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081130")); } }
        public List<SequenceSelector> ReferencedOverlaySequenceRetired_ { get { return Items.FindAll<Sequence>("00081130").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedStereometricInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081134")); } }
        public List<SequenceSelector> ReferencedStereometricInstanceSequence_ { get { return Items.FindAll<Sequence>("00081134").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedWaveformSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008113A")); } }
        public List<SequenceSelector> ReferencedWaveformSequence_ { get { return Items.FindAll<Sequence>("0008113A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081140")); } }
        public List<SequenceSelector> ReferencedImageSequence_ { get { return Items.FindAll<Sequence>("00081140").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedCurveSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081145")); } }
        public List<SequenceSelector> ReferencedCurveSequenceRetired_ { get { return Items.FindAll<Sequence>("00081145").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008114A")); } }
        public List<SequenceSelector> ReferencedInstanceSequence_ { get { return Items.FindAll<Sequence>("0008114A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedRealWorldValueMappingInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008114B")); } }
        public List<SequenceSelector> ReferencedRealWorldValueMappingInstanceSequence_ { get { return Items.FindAll<Sequence>("0008114B").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier ReferencedSOPClassUID { get { return Items.FindFirst<UniqueIdentifier>("00081150") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedSOPClassUID_ { get { return Items.FindAll<UniqueIdentifier>("00081150").ToList(); } }
        public UniqueIdentifier ReferencedSOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00081155") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedSOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00081155").ToList(); } }
        public UniqueIdentifier SOPClassesSupported { get { return Items.FindFirst<UniqueIdentifier>("0008115A") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SOPClassesSupported_ { get { return Items.FindAll<UniqueIdentifier>("0008115A").ToList(); } }
        public IntegerString ReferencedFrameNumber { get { return Items.FindFirst<IntegerString>("00081160") as IntegerString; } }
        public List<IntegerString> ReferencedFrameNumber_ { get { return Items.FindAll<IntegerString>("00081160").ToList(); } }
        public UnsignedLong SimpleFrameList { get { return Items.FindFirst<UnsignedLong>("00081161") as UnsignedLong; } }
        public List<UnsignedLong> SimpleFrameList_ { get { return Items.FindAll<UnsignedLong>("00081161").ToList(); } }
        public UnsignedLong CalculatedFrameList { get { return Items.FindFirst<UnsignedLong>("00081162") as UnsignedLong; } }
        public List<UnsignedLong> CalculatedFrameList_ { get { return Items.FindAll<UnsignedLong>("00081162").ToList(); } }
        public FloatingPointDouble TimeRange { get { return Items.FindFirst<FloatingPointDouble>("00081163") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TimeRange_ { get { return Items.FindAll<FloatingPointDouble>("00081163").ToList(); } }
        public SequenceSelector FrameExtractionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081164")); } }
        public List<SequenceSelector> FrameExtractionSequence_ { get { return Items.FindAll<Sequence>("00081164").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier MultiFrameSourceSOPInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("00081167") as UniqueIdentifier; } }
        public List<UniqueIdentifier> MultiFrameSourceSOPInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("00081167").ToList(); } }
        public UniqueIdentifier TransactionUID { get { return Items.FindFirst<UniqueIdentifier>("00081195") as UniqueIdentifier; } }
        public List<UniqueIdentifier> TransactionUID_ { get { return Items.FindAll<UniqueIdentifier>("00081195").ToList(); } }
        public UnsignedShort FailureReason { get { return Items.FindFirst<UnsignedShort>("00081197") as UnsignedShort; } }
        public List<UnsignedShort> FailureReason_ { get { return Items.FindAll<UnsignedShort>("00081197").ToList(); } }
        public SequenceSelector FailedSOPSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081198")); } }
        public List<SequenceSelector> FailedSOPSequence_ { get { return Items.FindAll<Sequence>("00081198").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedSOPSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081199")); } }
        public List<SequenceSelector> ReferencedSOPSequence_ { get { return Items.FindAll<Sequence>("00081199").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector StudiesContainingOtherReferencedInstancesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081200")); } }
        public List<SequenceSelector> StudiesContainingOtherReferencedInstancesSequence_ { get { return Items.FindAll<Sequence>("00081200").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelatedSeriesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00081250")); } }
        public List<SequenceSelector> RelatedSeriesSequence_ { get { return Items.FindAll<Sequence>("00081250").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString LossyImageCompressionRetired { get { return Items.FindFirst<CodeString>("00082110") as CodeString; } }
        public List<CodeString> LossyImageCompressionRetired_ { get { return Items.FindAll<CodeString>("00082110").ToList(); } }
        public ShortText DerivationDescription { get { return Items.FindFirst<ShortText>("00082111") as ShortText; } }
        public List<ShortText> DerivationDescription_ { get { return Items.FindAll<ShortText>("00082111").ToList(); } }
        public SequenceSelector SourceImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082112")); } }
        public List<SequenceSelector> SourceImageSequence_ { get { return Items.FindAll<Sequence>("00082112").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString StageName { get { return Items.FindFirst<ShortString>("00082120") as ShortString; } }
        public List<ShortString> StageName_ { get { return Items.FindAll<ShortString>("00082120").ToList(); } }
        public IntegerString StageNumber { get { return Items.FindFirst<IntegerString>("00082122") as IntegerString; } }
        public List<IntegerString> StageNumber_ { get { return Items.FindAll<IntegerString>("00082122").ToList(); } }
        public IntegerString NumberOfStages { get { return Items.FindFirst<IntegerString>("00082124") as IntegerString; } }
        public List<IntegerString> NumberOfStages_ { get { return Items.FindAll<IntegerString>("00082124").ToList(); } }
        public ShortString ViewName { get { return Items.FindFirst<ShortString>("00082127") as ShortString; } }
        public List<ShortString> ViewName_ { get { return Items.FindAll<ShortString>("00082127").ToList(); } }
        public IntegerString ViewNumber { get { return Items.FindFirst<IntegerString>("00082128") as IntegerString; } }
        public List<IntegerString> ViewNumber_ { get { return Items.FindAll<IntegerString>("00082128").ToList(); } }
        public IntegerString NumberOfEventTimers { get { return Items.FindFirst<IntegerString>("00082129") as IntegerString; } }
        public List<IntegerString> NumberOfEventTimers_ { get { return Items.FindAll<IntegerString>("00082129").ToList(); } }
        public IntegerString NumberOfViewsInStage { get { return Items.FindFirst<IntegerString>("0008212A") as IntegerString; } }
        public List<IntegerString> NumberOfViewsInStage_ { get { return Items.FindAll<IntegerString>("0008212A").ToList(); } }
        public DecimalString EventElapsedTimes { get { return Items.FindFirst<DecimalString>("00082130") as DecimalString; } }
        public List<DecimalString> EventElapsedTimes_ { get { return Items.FindAll<DecimalString>("00082130").ToList(); } }
        public LongString EventTimerNames { get { return Items.FindFirst<LongString>("00082132") as LongString; } }
        public List<LongString> EventTimerNames_ { get { return Items.FindAll<LongString>("00082132").ToList(); } }
        public SequenceSelector EventTimerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082133")); } }
        public List<SequenceSelector> EventTimerSequence_ { get { return Items.FindAll<Sequence>("00082133").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble EventTimeOffset { get { return Items.FindFirst<FloatingPointDouble>("00082134") as FloatingPointDouble; } }
        public List<FloatingPointDouble> EventTimeOffset_ { get { return Items.FindAll<FloatingPointDouble>("00082134").ToList(); } }
        public SequenceSelector EventCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082135")); } }
        public List<SequenceSelector> EventCodeSequence_ { get { return Items.FindAll<Sequence>("00082135").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString StartTrim { get { return Items.FindFirst<IntegerString>("00082142") as IntegerString; } }
        public List<IntegerString> StartTrim_ { get { return Items.FindAll<IntegerString>("00082142").ToList(); } }
        public IntegerString StopTrim { get { return Items.FindFirst<IntegerString>("00082143") as IntegerString; } }
        public List<IntegerString> StopTrim_ { get { return Items.FindAll<IntegerString>("00082143").ToList(); } }
        public IntegerString RecommendedDisplayFrameRate { get { return Items.FindFirst<IntegerString>("00082144") as IntegerString; } }
        public List<IntegerString> RecommendedDisplayFrameRate_ { get { return Items.FindAll<IntegerString>("00082144").ToList(); } }
        public CodeString TransducerPositionRetired { get { return Items.FindFirst<CodeString>("00082200") as CodeString; } }
        public List<CodeString> TransducerPositionRetired_ { get { return Items.FindAll<CodeString>("00082200").ToList(); } }
        public CodeString TransducerOrientationRetired { get { return Items.FindFirst<CodeString>("00082204") as CodeString; } }
        public List<CodeString> TransducerOrientationRetired_ { get { return Items.FindAll<CodeString>("00082204").ToList(); } }
        public CodeString AnatomicStructureRetired { get { return Items.FindFirst<CodeString>("00082208") as CodeString; } }
        public List<CodeString> AnatomicStructureRetired_ { get { return Items.FindAll<CodeString>("00082208").ToList(); } }
        public SequenceSelector AnatomicRegionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082218")); } }
        public List<SequenceSelector> AnatomicRegionSequence_ { get { return Items.FindAll<Sequence>("00082218").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicRegionModifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082220")); } }
        public List<SequenceSelector> AnatomicRegionModifierSequence_ { get { return Items.FindAll<Sequence>("00082220").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PrimaryAnatomicStructureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082228")); } }
        public List<SequenceSelector> PrimaryAnatomicStructureSequence_ { get { return Items.FindAll<Sequence>("00082228").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicStructureSpaceOrRegionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082229")); } }
        public List<SequenceSelector> AnatomicStructureSpaceOrRegionSequence_ { get { return Items.FindAll<Sequence>("00082229").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PrimaryAnatomicStructureModifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082230")); } }
        public List<SequenceSelector> PrimaryAnatomicStructureModifierSequence_ { get { return Items.FindAll<Sequence>("00082230").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerPositionSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082240")); } }
        public List<SequenceSelector> TransducerPositionSequenceRetired_ { get { return Items.FindAll<Sequence>("00082240").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerPositionModifierSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082242")); } }
        public List<SequenceSelector> TransducerPositionModifierSequenceRetired_ { get { return Items.FindAll<Sequence>("00082242").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerOrientationSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082244")); } }
        public List<SequenceSelector> TransducerOrientationSequenceRetired_ { get { return Items.FindAll<Sequence>("00082244").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerOrientationModifierSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082246")); } }
        public List<SequenceSelector> TransducerOrientationModifierSequenceRetired_ { get { return Items.FindAll<Sequence>("00082246").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicStructureSpaceOrRegionCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082251")); } }
        public List<SequenceSelector> AnatomicStructureSpaceOrRegionCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00082251").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicPortalOfEntranceCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082253")); } }
        public List<SequenceSelector> AnatomicPortalOfEntranceCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00082253").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicApproachDirectionCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082255")); } }
        public List<SequenceSelector> AnatomicApproachDirectionCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00082255").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText AnatomicPerspectiveDescriptionTrialRetired { get { return Items.FindFirst<ShortText>("00082256") as ShortText; } }
        public List<ShortText> AnatomicPerspectiveDescriptionTrialRetired_ { get { return Items.FindAll<ShortText>("00082256").ToList(); } }
        public SequenceSelector AnatomicPerspectiveCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082257")); } }
        public List<SequenceSelector> AnatomicPerspectiveCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00082257").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText AnatomicLocationOfExaminingInstrumentDescriptionTrialRetired { get { return Items.FindFirst<ShortText>("00082258") as ShortText; } }
        public List<ShortText> AnatomicLocationOfExaminingInstrumentDescriptionTrialRetired_ { get { return Items.FindAll<ShortText>("00082258").ToList(); } }
        public SequenceSelector AnatomicLocationOfExaminingInstrumentCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00082259")); } }
        public List<SequenceSelector> AnatomicLocationOfExaminingInstrumentCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00082259").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnatomicStructureSpaceOrRegionModifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008225A")); } }
        public List<SequenceSelector> AnatomicStructureSpaceOrRegionModifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0008225A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OnAxisBackgroundAnatomicStructureCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0008225C")); } }
        public List<SequenceSelector> OnAxisBackgroundAnatomicStructureCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0008225C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AlternateRepresentationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00083001")); } }
        public List<SequenceSelector> AlternateRepresentationSequence_ { get { return Items.FindAll<Sequence>("00083001").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier IrradiationEventUID { get { return Items.FindFirst<UniqueIdentifier>("00083010") as UniqueIdentifier; } }
        public List<UniqueIdentifier> IrradiationEventUID_ { get { return Items.FindAll<UniqueIdentifier>("00083010").ToList(); } }
        public LongText IdentifyingCommentsRetired { get { return Items.FindFirst<LongText>("00084000") as LongText; } }
        public List<LongText> IdentifyingCommentsRetired_ { get { return Items.FindAll<LongText>("00084000").ToList(); } }
        public CodeString FrameType { get { return Items.FindFirst<CodeString>("00089007") as CodeString; } }
        public List<CodeString> FrameType_ { get { return Items.FindAll<CodeString>("00089007").ToList(); } }
        public SequenceSelector ReferencedImageEvidenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089092")); } }
        public List<SequenceSelector> ReferencedImageEvidenceSequence_ { get { return Items.FindAll<Sequence>("00089092").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedRawDataSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089121")); } }
        public List<SequenceSelector> ReferencedRawDataSequence_ { get { return Items.FindAll<Sequence>("00089121").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier CreatorVersionUID { get { return Items.FindFirst<UniqueIdentifier>("00089123") as UniqueIdentifier; } }
        public List<UniqueIdentifier> CreatorVersionUID_ { get { return Items.FindAll<UniqueIdentifier>("00089123").ToList(); } }
        public SequenceSelector DerivationImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089124")); } }
        public List<SequenceSelector> DerivationImageSequence_ { get { return Items.FindAll<Sequence>("00089124").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SourceImageEvidenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089154")); } }
        public List<SequenceSelector> SourceImageEvidenceSequence_ { get { return Items.FindAll<Sequence>("00089154").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PixelPresentation { get { return Items.FindFirst<CodeString>("00089205") as CodeString; } }
        public List<CodeString> PixelPresentation_ { get { return Items.FindAll<CodeString>("00089205").ToList(); } }
        public CodeString VolumetricProperties { get { return Items.FindFirst<CodeString>("00089206") as CodeString; } }
        public List<CodeString> VolumetricProperties_ { get { return Items.FindAll<CodeString>("00089206").ToList(); } }
        public CodeString VolumeBasedCalculationTechnique { get { return Items.FindFirst<CodeString>("00089207") as CodeString; } }
        public List<CodeString> VolumeBasedCalculationTechnique_ { get { return Items.FindAll<CodeString>("00089207").ToList(); } }
        public CodeString ComplexImageComponent { get { return Items.FindFirst<CodeString>("00089208") as CodeString; } }
        public List<CodeString> ComplexImageComponent_ { get { return Items.FindAll<CodeString>("00089208").ToList(); } }
        public CodeString AcquisitionContrast { get { return Items.FindFirst<CodeString>("00089209") as CodeString; } }
        public List<CodeString> AcquisitionContrast_ { get { return Items.FindAll<CodeString>("00089209").ToList(); } }
        public SequenceSelector DerivationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089215")); } }
        public List<SequenceSelector> DerivationCodeSequence_ { get { return Items.FindAll<Sequence>("00089215").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedPresentationStateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089237")); } }
        public List<SequenceSelector> ReferencedPresentationStateSequence_ { get { return Items.FindAll<Sequence>("00089237").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedOtherPlaneSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089410")); } }
        public List<SequenceSelector> ReferencedOtherPlaneSequence_ { get { return Items.FindAll<Sequence>("00089410").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FrameDisplaySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00089458")); } }
        public List<SequenceSelector> FrameDisplaySequence_ { get { return Items.FindAll<Sequence>("00089458").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle RecommendedDisplayFrameRateInFloat { get { return Items.FindFirst<FloatingPointSingle>("00089459") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RecommendedDisplayFrameRateInFloat_ { get { return Items.FindAll<FloatingPointSingle>("00089459").ToList(); } }
        public CodeString SkipFrameRangeFlag { get { return Items.FindFirst<CodeString>("00089460") as CodeString; } }
        public List<CodeString> SkipFrameRangeFlag_ { get { return Items.FindAll<CodeString>("00089460").ToList(); } }
        public PersonName PatientName { get { return Items.FindFirst<PersonName>("00100010") as PersonName; } }
        public List<PersonName> PatientName_ { get { return Items.FindAll<PersonName>("00100010").ToList(); } }
        public LongString PatientID { get { return Items.FindFirst<LongString>("00100020") as LongString; } }
        public List<LongString> PatientID_ { get { return Items.FindAll<LongString>("00100020").ToList(); } }
        public LongString IssuerOfPatientID { get { return Items.FindFirst<LongString>("00100021") as LongString; } }
        public List<LongString> IssuerOfPatientID_ { get { return Items.FindAll<LongString>("00100021").ToList(); } }
        public CodeString TypeOfPatientID { get { return Items.FindFirst<CodeString>("00100022") as CodeString; } }
        public List<CodeString> TypeOfPatientID_ { get { return Items.FindAll<CodeString>("00100022").ToList(); } }
        public SequenceSelector IssuerOfPatientIDQualifiersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00100024")); } }
        public List<SequenceSelector> IssuerOfPatientIDQualifiersSequence_ { get { return Items.FindAll<Sequence>("00100024").Select(s => new SequenceSelector(s)).ToList(); } }
        public Date PatientBirthDate { get { return Items.FindFirst<Date>("00100030") as Date; } }
        public List<Date> PatientBirthDate_ { get { return Items.FindAll<Date>("00100030").ToList(); } }
        public Time PatientBirthTime { get { return Items.FindFirst<Time>("00100032") as Time; } }
        public List<Time> PatientBirthTime_ { get { return Items.FindAll<Time>("00100032").ToList(); } }
        public CodeString PatientSex { get { return Items.FindFirst<CodeString>("00100040") as CodeString; } }
        public List<CodeString> PatientSex_ { get { return Items.FindAll<CodeString>("00100040").ToList(); } }
        public SequenceSelector PatientInsurancePlanCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00100050")); } }
        public List<SequenceSelector> PatientInsurancePlanCodeSequence_ { get { return Items.FindAll<Sequence>("00100050").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PatientPrimaryLanguageCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00100101")); } }
        public List<SequenceSelector> PatientPrimaryLanguageCodeSequence_ { get { return Items.FindAll<Sequence>("00100101").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PatientPrimaryLanguageModifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00100102")); } }
        public List<SequenceSelector> PatientPrimaryLanguageModifierCodeSequence_ { get { return Items.FindAll<Sequence>("00100102").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString OtherPatientIDs { get { return Items.FindFirst<LongString>("00101000") as LongString; } }
        public List<LongString> OtherPatientIDs_ { get { return Items.FindAll<LongString>("00101000").ToList(); } }
        public PersonName OtherPatientNames { get { return Items.FindFirst<PersonName>("00101001") as PersonName; } }
        public List<PersonName> OtherPatientNames_ { get { return Items.FindAll<PersonName>("00101001").ToList(); } }
        public SequenceSelector OtherPatientIDsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00101002")); } }
        public List<SequenceSelector> OtherPatientIDsSequence_ { get { return Items.FindAll<Sequence>("00101002").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName PatientBirthName { get { return Items.FindFirst<PersonName>("00101005") as PersonName; } }
        public List<PersonName> PatientBirthName_ { get { return Items.FindAll<PersonName>("00101005").ToList(); } }
        public AgeString PatientAge { get { return Items.FindFirst<AgeString>("00101010") as AgeString; } }
        public List<AgeString> PatientAge_ { get { return Items.FindAll<AgeString>("00101010").ToList(); } }
        public DecimalString PatientSize { get { return Items.FindFirst<DecimalString>("00101020") as DecimalString; } }
        public List<DecimalString> PatientSize_ { get { return Items.FindAll<DecimalString>("00101020").ToList(); } }
        public SequenceSelector PatientSizeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00101021")); } }
        public List<SequenceSelector> PatientSizeCodeSequence_ { get { return Items.FindAll<Sequence>("00101021").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString PatientWeight { get { return Items.FindFirst<DecimalString>("00101030") as DecimalString; } }
        public List<DecimalString> PatientWeight_ { get { return Items.FindAll<DecimalString>("00101030").ToList(); } }
        public LongString PatientAddress { get { return Items.FindFirst<LongString>("00101040") as LongString; } }
        public List<LongString> PatientAddress_ { get { return Items.FindAll<LongString>("00101040").ToList(); } }
        public LongString InsurancePlanIdentificationRetired { get { return Items.FindFirst<LongString>("00101050") as LongString; } }
        public List<LongString> InsurancePlanIdentificationRetired_ { get { return Items.FindAll<LongString>("00101050").ToList(); } }
        public PersonName PatientMotherBirthName { get { return Items.FindFirst<PersonName>("00101060") as PersonName; } }
        public List<PersonName> PatientMotherBirthName_ { get { return Items.FindAll<PersonName>("00101060").ToList(); } }
        public LongString MilitaryRank { get { return Items.FindFirst<LongString>("00101080") as LongString; } }
        public List<LongString> MilitaryRank_ { get { return Items.FindAll<LongString>("00101080").ToList(); } }
        public LongString BranchOfService { get { return Items.FindFirst<LongString>("00101081") as LongString; } }
        public List<LongString> BranchOfService_ { get { return Items.FindAll<LongString>("00101081").ToList(); } }
        public LongString MedicalRecordLocator { get { return Items.FindFirst<LongString>("00101090") as LongString; } }
        public List<LongString> MedicalRecordLocator_ { get { return Items.FindAll<LongString>("00101090").ToList(); } }
        public LongString MedicalAlerts { get { return Items.FindFirst<LongString>("00102000") as LongString; } }
        public List<LongString> MedicalAlerts_ { get { return Items.FindAll<LongString>("00102000").ToList(); } }
        public LongString Allergies { get { return Items.FindFirst<LongString>("00102110") as LongString; } }
        public List<LongString> Allergies_ { get { return Items.FindAll<LongString>("00102110").ToList(); } }
        public LongString CountryOfResidence { get { return Items.FindFirst<LongString>("00102150") as LongString; } }
        public List<LongString> CountryOfResidence_ { get { return Items.FindAll<LongString>("00102150").ToList(); } }
        public LongString RegionOfResidence { get { return Items.FindFirst<LongString>("00102152") as LongString; } }
        public List<LongString> RegionOfResidence_ { get { return Items.FindAll<LongString>("00102152").ToList(); } }
        public ShortString PatientTelephoneNumbers { get { return Items.FindFirst<ShortString>("00102154") as ShortString; } }
        public List<ShortString> PatientTelephoneNumbers_ { get { return Items.FindAll<ShortString>("00102154").ToList(); } }
        public ShortString EthnicGroup { get { return Items.FindFirst<ShortString>("00102160") as ShortString; } }
        public List<ShortString> EthnicGroup_ { get { return Items.FindAll<ShortString>("00102160").ToList(); } }
        public ShortString Occupation { get { return Items.FindFirst<ShortString>("00102180") as ShortString; } }
        public List<ShortString> Occupation_ { get { return Items.FindAll<ShortString>("00102180").ToList(); } }
        public CodeString SmokingStatus { get { return Items.FindFirst<CodeString>("001021A0") as CodeString; } }
        public List<CodeString> SmokingStatus_ { get { return Items.FindAll<CodeString>("001021A0").ToList(); } }
        public LongText AdditionalPatientHistory { get { return Items.FindFirst<LongText>("001021B0") as LongText; } }
        public List<LongText> AdditionalPatientHistory_ { get { return Items.FindAll<LongText>("001021B0").ToList(); } }
        public UnsignedShort PregnancyStatus { get { return Items.FindFirst<UnsignedShort>("001021C0") as UnsignedShort; } }
        public List<UnsignedShort> PregnancyStatus_ { get { return Items.FindAll<UnsignedShort>("001021C0").ToList(); } }
        public Date LastMenstrualDate { get { return Items.FindFirst<Date>("001021D0") as Date; } }
        public List<Date> LastMenstrualDate_ { get { return Items.FindAll<Date>("001021D0").ToList(); } }
        public LongString PatientReligiousPreference { get { return Items.FindFirst<LongString>("001021F0") as LongString; } }
        public List<LongString> PatientReligiousPreference_ { get { return Items.FindAll<LongString>("001021F0").ToList(); } }
        public LongString PatientSpeciesDescription { get { return Items.FindFirst<LongString>("00102201") as LongString; } }
        public List<LongString> PatientSpeciesDescription_ { get { return Items.FindAll<LongString>("00102201").ToList(); } }
        public SequenceSelector PatientSpeciesCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00102202")); } }
        public List<SequenceSelector> PatientSpeciesCodeSequence_ { get { return Items.FindAll<Sequence>("00102202").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PatientSexNeutered { get { return Items.FindFirst<CodeString>("00102203") as CodeString; } }
        public List<CodeString> PatientSexNeutered_ { get { return Items.FindAll<CodeString>("00102203").ToList(); } }
        public CodeString AnatomicalOrientationType { get { return Items.FindFirst<CodeString>("00102210") as CodeString; } }
        public List<CodeString> AnatomicalOrientationType_ { get { return Items.FindAll<CodeString>("00102210").ToList(); } }
        public LongString PatientBreedDescription { get { return Items.FindFirst<LongString>("00102292") as LongString; } }
        public List<LongString> PatientBreedDescription_ { get { return Items.FindAll<LongString>("00102292").ToList(); } }
        public SequenceSelector PatientBreedCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00102293")); } }
        public List<SequenceSelector> PatientBreedCodeSequence_ { get { return Items.FindAll<Sequence>("00102293").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BreedRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00102294")); } }
        public List<SequenceSelector> BreedRegistrationSequence_ { get { return Items.FindAll<Sequence>("00102294").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString BreedRegistrationNumber { get { return Items.FindFirst<LongString>("00102295") as LongString; } }
        public List<LongString> BreedRegistrationNumber_ { get { return Items.FindAll<LongString>("00102295").ToList(); } }
        public SequenceSelector BreedRegistryCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00102296")); } }
        public List<SequenceSelector> BreedRegistryCodeSequence_ { get { return Items.FindAll<Sequence>("00102296").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName ResponsiblePerson { get { return Items.FindFirst<PersonName>("00102297") as PersonName; } }
        public List<PersonName> ResponsiblePerson_ { get { return Items.FindAll<PersonName>("00102297").ToList(); } }
        public CodeString ResponsiblePersonRole { get { return Items.FindFirst<CodeString>("00102298") as CodeString; } }
        public List<CodeString> ResponsiblePersonRole_ { get { return Items.FindAll<CodeString>("00102298").ToList(); } }
        public LongString ResponsibleOrganization { get { return Items.FindFirst<LongString>("00102299") as LongString; } }
        public List<LongString> ResponsibleOrganization_ { get { return Items.FindAll<LongString>("00102299").ToList(); } }
        public LongText PatientComments { get { return Items.FindFirst<LongText>("00104000") as LongText; } }
        public List<LongText> PatientComments_ { get { return Items.FindAll<LongText>("00104000").ToList(); } }
        public FloatingPointSingle ExaminedBodyThickness { get { return Items.FindFirst<FloatingPointSingle>("00109431") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ExaminedBodyThickness_ { get { return Items.FindAll<FloatingPointSingle>("00109431").ToList(); } }
        public LongString ClinicalTrialSponsorName { get { return Items.FindFirst<LongString>("00120010") as LongString; } }
        public List<LongString> ClinicalTrialSponsorName_ { get { return Items.FindAll<LongString>("00120010").ToList(); } }
        public LongString ClinicalTrialProtocolID { get { return Items.FindFirst<LongString>("00120020") as LongString; } }
        public List<LongString> ClinicalTrialProtocolID_ { get { return Items.FindAll<LongString>("00120020").ToList(); } }
        public LongString ClinicalTrialProtocolName { get { return Items.FindFirst<LongString>("00120021") as LongString; } }
        public List<LongString> ClinicalTrialProtocolName_ { get { return Items.FindAll<LongString>("00120021").ToList(); } }
        public LongString ClinicalTrialSiteID { get { return Items.FindFirst<LongString>("00120030") as LongString; } }
        public List<LongString> ClinicalTrialSiteID_ { get { return Items.FindAll<LongString>("00120030").ToList(); } }
        public LongString ClinicalTrialSiteName { get { return Items.FindFirst<LongString>("00120031") as LongString; } }
        public List<LongString> ClinicalTrialSiteName_ { get { return Items.FindAll<LongString>("00120031").ToList(); } }
        public LongString ClinicalTrialSubjectID { get { return Items.FindFirst<LongString>("00120040") as LongString; } }
        public List<LongString> ClinicalTrialSubjectID_ { get { return Items.FindAll<LongString>("00120040").ToList(); } }
        public LongString ClinicalTrialSubjectReadingID { get { return Items.FindFirst<LongString>("00120042") as LongString; } }
        public List<LongString> ClinicalTrialSubjectReadingID_ { get { return Items.FindAll<LongString>("00120042").ToList(); } }
        public LongString ClinicalTrialTimePointID { get { return Items.FindFirst<LongString>("00120050") as LongString; } }
        public List<LongString> ClinicalTrialTimePointID_ { get { return Items.FindAll<LongString>("00120050").ToList(); } }
        public ShortText ClinicalTrialTimePointDescription { get { return Items.FindFirst<ShortText>("00120051") as ShortText; } }
        public List<ShortText> ClinicalTrialTimePointDescription_ { get { return Items.FindAll<ShortText>("00120051").ToList(); } }
        public LongString ClinicalTrialCoordinatingCenterName { get { return Items.FindFirst<LongString>("00120060") as LongString; } }
        public List<LongString> ClinicalTrialCoordinatingCenterName_ { get { return Items.FindAll<LongString>("00120060").ToList(); } }
        public CodeString PatientIdentityRemoved { get { return Items.FindFirst<CodeString>("00120062") as CodeString; } }
        public List<CodeString> PatientIdentityRemoved_ { get { return Items.FindAll<CodeString>("00120062").ToList(); } }
        public LongString DeidentificationMethod { get { return Items.FindFirst<LongString>("00120063") as LongString; } }
        public List<LongString> DeidentificationMethod_ { get { return Items.FindAll<LongString>("00120063").ToList(); } }
        public SequenceSelector DeidentificationMethodCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00120064")); } }
        public List<SequenceSelector> DeidentificationMethodCodeSequence_ { get { return Items.FindAll<Sequence>("00120064").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ClinicalTrialSeriesID { get { return Items.FindFirst<LongString>("00120071") as LongString; } }
        public List<LongString> ClinicalTrialSeriesID_ { get { return Items.FindAll<LongString>("00120071").ToList(); } }
        public LongString ClinicalTrialSeriesDescription { get { return Items.FindFirst<LongString>("00120072") as LongString; } }
        public List<LongString> ClinicalTrialSeriesDescription_ { get { return Items.FindAll<LongString>("00120072").ToList(); } }
        public LongString ClinicalTrialProtocolEthicsCommitteeName { get { return Items.FindFirst<LongString>("00120081") as LongString; } }
        public List<LongString> ClinicalTrialProtocolEthicsCommitteeName_ { get { return Items.FindAll<LongString>("00120081").ToList(); } }
        public LongString ClinicalTrialProtocolEthicsCommitteeApprovalNumber { get { return Items.FindFirst<LongString>("00120082") as LongString; } }
        public List<LongString> ClinicalTrialProtocolEthicsCommitteeApprovalNumber_ { get { return Items.FindAll<LongString>("00120082").ToList(); } }
        public SequenceSelector ConsentForClinicalTrialUseSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00120083")); } }
        public List<SequenceSelector> ConsentForClinicalTrialUseSequence_ { get { return Items.FindAll<Sequence>("00120083").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DistributionType { get { return Items.FindFirst<CodeString>("00120084") as CodeString; } }
        public List<CodeString> DistributionType_ { get { return Items.FindAll<CodeString>("00120084").ToList(); } }
        public CodeString ConsentForDistributionFlag { get { return Items.FindFirst<CodeString>("00120085") as CodeString; } }
        public List<CodeString> ConsentForDistributionFlag_ { get { return Items.FindAll<CodeString>("00120085").ToList(); } }
        public ShortText CADFileFormat { get { return Items.FindFirst<ShortText>("00140023") as ShortText; } }
        public List<ShortText> CADFileFormat_ { get { return Items.FindAll<ShortText>("00140023").ToList(); } }
        public ShortText ComponentReferenceSystem { get { return Items.FindFirst<ShortText>("00140024") as ShortText; } }
        public List<ShortText> ComponentReferenceSystem_ { get { return Items.FindAll<ShortText>("00140024").ToList(); } }
        public ShortText ComponentManufacturingProcedure { get { return Items.FindFirst<ShortText>("00140025") as ShortText; } }
        public List<ShortText> ComponentManufacturingProcedure_ { get { return Items.FindAll<ShortText>("00140025").ToList(); } }
        public ShortText ComponentManufacturer { get { return Items.FindFirst<ShortText>("00140028") as ShortText; } }
        public List<ShortText> ComponentManufacturer_ { get { return Items.FindAll<ShortText>("00140028").ToList(); } }
        public DecimalString MaterialThickness { get { return Items.FindFirst<DecimalString>("00140030") as DecimalString; } }
        public List<DecimalString> MaterialThickness_ { get { return Items.FindAll<DecimalString>("00140030").ToList(); } }
        public DecimalString MaterialPipeDiameter { get { return Items.FindFirst<DecimalString>("00140032") as DecimalString; } }
        public List<DecimalString> MaterialPipeDiameter_ { get { return Items.FindAll<DecimalString>("00140032").ToList(); } }
        public DecimalString MaterialIsolationDiameter { get { return Items.FindFirst<DecimalString>("00140034") as DecimalString; } }
        public List<DecimalString> MaterialIsolationDiameter_ { get { return Items.FindAll<DecimalString>("00140034").ToList(); } }
        public ShortText MaterialGrade { get { return Items.FindFirst<ShortText>("00140042") as ShortText; } }
        public List<ShortText> MaterialGrade_ { get { return Items.FindAll<ShortText>("00140042").ToList(); } }
        public ShortText MaterialPropertiesFileID { get { return Items.FindFirst<ShortText>("00140044") as ShortText; } }
        public List<ShortText> MaterialPropertiesFileID_ { get { return Items.FindAll<ShortText>("00140044").ToList(); } }
        public ShortText MaterialPropertiesFileFormat { get { return Items.FindFirst<ShortText>("00140045") as ShortText; } }
        public List<ShortText> MaterialPropertiesFileFormat_ { get { return Items.FindAll<ShortText>("00140045").ToList(); } }
        public LongText MaterialNotes { get { return Items.FindFirst<LongText>("00140046") as LongText; } }
        public List<LongText> MaterialNotes_ { get { return Items.FindAll<LongText>("00140046").ToList(); } }
        public CodeString ComponentShape { get { return Items.FindFirst<CodeString>("00140050") as CodeString; } }
        public List<CodeString> ComponentShape_ { get { return Items.FindAll<CodeString>("00140050").ToList(); } }
        public CodeString CurvatureType { get { return Items.FindFirst<CodeString>("00140052") as CodeString; } }
        public List<CodeString> CurvatureType_ { get { return Items.FindAll<CodeString>("00140052").ToList(); } }
        public DecimalString OuterDiameter { get { return Items.FindFirst<DecimalString>("00140054") as DecimalString; } }
        public List<DecimalString> OuterDiameter_ { get { return Items.FindAll<DecimalString>("00140054").ToList(); } }
        public DecimalString InnerDiameter { get { return Items.FindFirst<DecimalString>("00140056") as DecimalString; } }
        public List<DecimalString> InnerDiameter_ { get { return Items.FindAll<DecimalString>("00140056").ToList(); } }
        public ShortText ActualEnvironmentalConditions { get { return Items.FindFirst<ShortText>("00141010") as ShortText; } }
        public List<ShortText> ActualEnvironmentalConditions_ { get { return Items.FindAll<ShortText>("00141010").ToList(); } }
        public Date ExpiryDate { get { return Items.FindFirst<Date>("00141020") as Date; } }
        public List<Date> ExpiryDate_ { get { return Items.FindAll<Date>("00141020").ToList(); } }
        public ShortText EnvironmentalConditions { get { return Items.FindFirst<ShortText>("00141040") as ShortText; } }
        public List<ShortText> EnvironmentalConditions_ { get { return Items.FindAll<ShortText>("00141040").ToList(); } }
        public SequenceSelector EvaluatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00142002")); } }
        public List<SequenceSelector> EvaluatorSequence_ { get { return Items.FindAll<Sequence>("00142002").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString EvaluatorNumber { get { return Items.FindFirst<IntegerString>("00142004") as IntegerString; } }
        public List<IntegerString> EvaluatorNumber_ { get { return Items.FindAll<IntegerString>("00142004").ToList(); } }
        public PersonName EvaluatorName { get { return Items.FindFirst<PersonName>("00142006") as PersonName; } }
        public List<PersonName> EvaluatorName_ { get { return Items.FindAll<PersonName>("00142006").ToList(); } }
        public IntegerString EvaluationAttempt { get { return Items.FindFirst<IntegerString>("00142008") as IntegerString; } }
        public List<IntegerString> EvaluationAttempt_ { get { return Items.FindAll<IntegerString>("00142008").ToList(); } }
        public SequenceSelector IndicationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00142012")); } }
        public List<SequenceSelector> IndicationSequence_ { get { return Items.FindAll<Sequence>("00142012").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString IndicationNumber { get { return Items.FindFirst<IntegerString>("00142014") as IntegerString; } }
        public List<IntegerString> IndicationNumber_ { get { return Items.FindAll<IntegerString>("00142014").ToList(); } }
        public ShortString IndicationLabel { get { return Items.FindFirst<ShortString>("00142016") as ShortString; } }
        public List<ShortString> IndicationLabel_ { get { return Items.FindAll<ShortString>("00142016").ToList(); } }
        public ShortText IndicationDescription { get { return Items.FindFirst<ShortText>("00142018") as ShortText; } }
        public List<ShortText> IndicationDescription_ { get { return Items.FindAll<ShortText>("00142018").ToList(); } }
        public CodeString IndicationType { get { return Items.FindFirst<CodeString>("0014201A") as CodeString; } }
        public List<CodeString> IndicationType_ { get { return Items.FindAll<CodeString>("0014201A").ToList(); } }
        public CodeString IndicationDisposition { get { return Items.FindFirst<CodeString>("0014201C") as CodeString; } }
        public List<CodeString> IndicationDisposition_ { get { return Items.FindAll<CodeString>("0014201C").ToList(); } }
        public SequenceSelector IndicationROISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0014201E")); } }
        public List<SequenceSelector> IndicationROISequence_ { get { return Items.FindAll<Sequence>("0014201E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IndicationPhysicalPropertySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00142030")); } }
        public List<SequenceSelector> IndicationPhysicalPropertySequence_ { get { return Items.FindAll<Sequence>("00142030").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString PropertyLabel { get { return Items.FindFirst<ShortString>("00142032") as ShortString; } }
        public List<ShortString> PropertyLabel_ { get { return Items.FindAll<ShortString>("00142032").ToList(); } }
        public IntegerString CoordinateSystemNumberOfAxes { get { return Items.FindFirst<IntegerString>("00142202") as IntegerString; } }
        public List<IntegerString> CoordinateSystemNumberOfAxes_ { get { return Items.FindAll<IntegerString>("00142202").ToList(); } }
        public SequenceSelector CoordinateSystemAxesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00142204")); } }
        public List<SequenceSelector> CoordinateSystemAxesSequence_ { get { return Items.FindAll<Sequence>("00142204").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText CoordinateSystemAxisDescription { get { return Items.FindFirst<ShortText>("00142206") as ShortText; } }
        public List<ShortText> CoordinateSystemAxisDescription_ { get { return Items.FindAll<ShortText>("00142206").ToList(); } }
        public CodeString CoordinateSystemDataSetMapping { get { return Items.FindFirst<CodeString>("00142208") as CodeString; } }
        public List<CodeString> CoordinateSystemDataSetMapping_ { get { return Items.FindAll<CodeString>("00142208").ToList(); } }
        public IntegerString CoordinateSystemAxisNumber { get { return Items.FindFirst<IntegerString>("0014220A") as IntegerString; } }
        public List<IntegerString> CoordinateSystemAxisNumber_ { get { return Items.FindAll<IntegerString>("0014220A").ToList(); } }
        public CodeString CoordinateSystemAxisType { get { return Items.FindFirst<CodeString>("0014220C") as CodeString; } }
        public List<CodeString> CoordinateSystemAxisType_ { get { return Items.FindAll<CodeString>("0014220C").ToList(); } }
        public CodeString CoordinateSystemAxisUnits { get { return Items.FindFirst<CodeString>("0014220E") as CodeString; } }
        public List<CodeString> CoordinateSystemAxisUnits_ { get { return Items.FindAll<CodeString>("0014220E").ToList(); } }
        public OtherByteString CoordinateSystemAxisValues { get { return Items.FindFirst<OtherByteString>("00142210") as OtherByteString; } }
        public List<OtherByteString> CoordinateSystemAxisValues_ { get { return Items.FindAll<OtherByteString>("00142210").ToList(); } }
        public SequenceSelector CoordinateSystemTransformSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00142220")); } }
        public List<SequenceSelector> CoordinateSystemTransformSequence_ { get { return Items.FindAll<Sequence>("00142220").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText TransformDescription { get { return Items.FindFirst<ShortText>("00142222") as ShortText; } }
        public List<ShortText> TransformDescription_ { get { return Items.FindAll<ShortText>("00142222").ToList(); } }
        public IntegerString TransformNumberOfAxes { get { return Items.FindFirst<IntegerString>("00142224") as IntegerString; } }
        public List<IntegerString> TransformNumberOfAxes_ { get { return Items.FindAll<IntegerString>("00142224").ToList(); } }
        public IntegerString TransformOrderOfAxes { get { return Items.FindFirst<IntegerString>("00142226") as IntegerString; } }
        public List<IntegerString> TransformOrderOfAxes_ { get { return Items.FindAll<IntegerString>("00142226").ToList(); } }
        public CodeString TransformedAxisUnits { get { return Items.FindFirst<CodeString>("00142228") as CodeString; } }
        public List<CodeString> TransformedAxisUnits_ { get { return Items.FindAll<CodeString>("00142228").ToList(); } }
        public DecimalString CoordinateSystemTransformRotationAndScaleMatrix { get { return Items.FindFirst<DecimalString>("0014222A") as DecimalString; } }
        public List<DecimalString> CoordinateSystemTransformRotationAndScaleMatrix_ { get { return Items.FindAll<DecimalString>("0014222A").ToList(); } }
        public DecimalString CoordinateSystemTransformTranslationMatrix { get { return Items.FindFirst<DecimalString>("0014222C") as DecimalString; } }
        public List<DecimalString> CoordinateSystemTransformTranslationMatrix_ { get { return Items.FindAll<DecimalString>("0014222C").ToList(); } }
        public DecimalString InternalDetectorFrameTime { get { return Items.FindFirst<DecimalString>("00143011") as DecimalString; } }
        public List<DecimalString> InternalDetectorFrameTime_ { get { return Items.FindAll<DecimalString>("00143011").ToList(); } }
        public DecimalString NumberOfFramesIntegrated { get { return Items.FindFirst<DecimalString>("00143012") as DecimalString; } }
        public List<DecimalString> NumberOfFramesIntegrated_ { get { return Items.FindAll<DecimalString>("00143012").ToList(); } }
        public SequenceSelector DetectorTemperatureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00143020")); } }
        public List<SequenceSelector> DetectorTemperatureSequence_ { get { return Items.FindAll<Sequence>("00143020").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString SensorName { get { return Items.FindFirst<DecimalString>("00143022") as DecimalString; } }
        public List<DecimalString> SensorName_ { get { return Items.FindAll<DecimalString>("00143022").ToList(); } }
        public DecimalString HorizontalOffsetOfSensor { get { return Items.FindFirst<DecimalString>("00143024") as DecimalString; } }
        public List<DecimalString> HorizontalOffsetOfSensor_ { get { return Items.FindAll<DecimalString>("00143024").ToList(); } }
        public DecimalString VerticalOffsetOfSensor { get { return Items.FindFirst<DecimalString>("00143026") as DecimalString; } }
        public List<DecimalString> VerticalOffsetOfSensor_ { get { return Items.FindAll<DecimalString>("00143026").ToList(); } }
        public DecimalString SensorTemperature { get { return Items.FindFirst<DecimalString>("00143028") as DecimalString; } }
        public List<DecimalString> SensorTemperature_ { get { return Items.FindAll<DecimalString>("00143028").ToList(); } }
        public SequenceSelector DarkCurrentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00143040")); } }
        public List<SequenceSelector> DarkCurrentSequence_ { get { return Items.FindAll<Sequence>("00143040").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString DarkCurrentCounts { get { return Items.FindFirst<OtherByteString>("00143050") as OtherByteString; } }
        public List<OtherByteString> DarkCurrentCounts_ { get { return Items.FindAll<OtherByteString>("00143050").ToList(); } }
        public SequenceSelector GainCorrectionReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00143060")); } }
        public List<SequenceSelector> GainCorrectionReferenceSequence_ { get { return Items.FindAll<Sequence>("00143060").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString AirCounts { get { return Items.FindFirst<OtherByteString>("00143070") as OtherByteString; } }
        public List<OtherByteString> AirCounts_ { get { return Items.FindAll<OtherByteString>("00143070").ToList(); } }
        public DecimalString KVUsedInGainCalibration { get { return Items.FindFirst<DecimalString>("00143071") as DecimalString; } }
        public List<DecimalString> KVUsedInGainCalibration_ { get { return Items.FindAll<DecimalString>("00143071").ToList(); } }
        public DecimalString MAUsedInGainCalibration { get { return Items.FindFirst<DecimalString>("00143072") as DecimalString; } }
        public List<DecimalString> MAUsedInGainCalibration_ { get { return Items.FindAll<DecimalString>("00143072").ToList(); } }
        public DecimalString NumberOfFramesUsedForIntegration { get { return Items.FindFirst<DecimalString>("00143073") as DecimalString; } }
        public List<DecimalString> NumberOfFramesUsedForIntegration_ { get { return Items.FindAll<DecimalString>("00143073").ToList(); } }
        public LongString FilterMaterialUsedInGainCalibration { get { return Items.FindFirst<LongString>("00143074") as LongString; } }
        public List<LongString> FilterMaterialUsedInGainCalibration_ { get { return Items.FindAll<LongString>("00143074").ToList(); } }
        public DecimalString FilterThicknessUsedInGainCalibration { get { return Items.FindFirst<DecimalString>("00143075") as DecimalString; } }
        public List<DecimalString> FilterThicknessUsedInGainCalibration_ { get { return Items.FindAll<DecimalString>("00143075").ToList(); } }
        public Date DateOfGainCalibration { get { return Items.FindFirst<Date>("00143076") as Date; } }
        public List<Date> DateOfGainCalibration_ { get { return Items.FindAll<Date>("00143076").ToList(); } }
        public Time TimeOfGainCalibration { get { return Items.FindFirst<Time>("00143077") as Time; } }
        public List<Time> TimeOfGainCalibration_ { get { return Items.FindAll<Time>("00143077").ToList(); } }
        public OtherByteString BadPixelImage { get { return Items.FindFirst<OtherByteString>("00143080") as OtherByteString; } }
        public List<OtherByteString> BadPixelImage_ { get { return Items.FindAll<OtherByteString>("00143080").ToList(); } }
        public LongText CalibrationNotes { get { return Items.FindFirst<LongText>("00143099") as LongText; } }
        public List<LongText> CalibrationNotes_ { get { return Items.FindAll<LongText>("00143099").ToList(); } }
        public SequenceSelector PulserEquipmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144002")); } }
        public List<SequenceSelector> PulserEquipmentSequence_ { get { return Items.FindAll<Sequence>("00144002").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PulserType { get { return Items.FindFirst<CodeString>("00144004") as CodeString; } }
        public List<CodeString> PulserType_ { get { return Items.FindAll<CodeString>("00144004").ToList(); } }
        public LongText PulserNotes { get { return Items.FindFirst<LongText>("00144006") as LongText; } }
        public List<LongText> PulserNotes_ { get { return Items.FindAll<LongText>("00144006").ToList(); } }
        public SequenceSelector ReceiverEquipmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144008")); } }
        public List<SequenceSelector> ReceiverEquipmentSequence_ { get { return Items.FindAll<Sequence>("00144008").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString AmplifierType { get { return Items.FindFirst<CodeString>("0014400A") as CodeString; } }
        public List<CodeString> AmplifierType_ { get { return Items.FindAll<CodeString>("0014400A").ToList(); } }
        public LongText ReceiverNotes { get { return Items.FindFirst<LongText>("0014400C") as LongText; } }
        public List<LongText> ReceiverNotes_ { get { return Items.FindAll<LongText>("0014400C").ToList(); } }
        public SequenceSelector PreAmplifierEquipmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0014400E")); } }
        public List<SequenceSelector> PreAmplifierEquipmentSequence_ { get { return Items.FindAll<Sequence>("0014400E").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongText PreAmplifierNotes { get { return Items.FindFirst<LongText>("0014400F") as LongText; } }
        public List<LongText> PreAmplifierNotes_ { get { return Items.FindAll<LongText>("0014400F").ToList(); } }
        public SequenceSelector TransmitTransducerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144010")); } }
        public List<SequenceSelector> TransmitTransducerSequence_ { get { return Items.FindAll<Sequence>("00144010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReceiveTransducerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144011")); } }
        public List<SequenceSelector> ReceiveTransducerSequence_ { get { return Items.FindAll<Sequence>("00144011").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfElements { get { return Items.FindFirst<UnsignedShort>("00144012") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfElements_ { get { return Items.FindAll<UnsignedShort>("00144012").ToList(); } }
        public CodeString ElementShape { get { return Items.FindFirst<CodeString>("00144013") as CodeString; } }
        public List<CodeString> ElementShape_ { get { return Items.FindAll<CodeString>("00144013").ToList(); } }
        public DecimalString ElementDimensionA { get { return Items.FindFirst<DecimalString>("00144014") as DecimalString; } }
        public List<DecimalString> ElementDimensionA_ { get { return Items.FindAll<DecimalString>("00144014").ToList(); } }
        public DecimalString ElementDimensionB { get { return Items.FindFirst<DecimalString>("00144015") as DecimalString; } }
        public List<DecimalString> ElementDimensionB_ { get { return Items.FindAll<DecimalString>("00144015").ToList(); } }
        public DecimalString ElementPitch { get { return Items.FindFirst<DecimalString>("00144016") as DecimalString; } }
        public List<DecimalString> ElementPitch_ { get { return Items.FindAll<DecimalString>("00144016").ToList(); } }
        public DecimalString MeasuredBeamDimensionA { get { return Items.FindFirst<DecimalString>("00144017") as DecimalString; } }
        public List<DecimalString> MeasuredBeamDimensionA_ { get { return Items.FindAll<DecimalString>("00144017").ToList(); } }
        public DecimalString MeasuredBeamDimensionB { get { return Items.FindFirst<DecimalString>("00144018") as DecimalString; } }
        public List<DecimalString> MeasuredBeamDimensionB_ { get { return Items.FindAll<DecimalString>("00144018").ToList(); } }
        public DecimalString LocationOfMeasuredBeamDiameter { get { return Items.FindFirst<DecimalString>("00144019") as DecimalString; } }
        public List<DecimalString> LocationOfMeasuredBeamDiameter_ { get { return Items.FindAll<DecimalString>("00144019").ToList(); } }
        public DecimalString NominalFrequency { get { return Items.FindFirst<DecimalString>("0014401A") as DecimalString; } }
        public List<DecimalString> NominalFrequency_ { get { return Items.FindAll<DecimalString>("0014401A").ToList(); } }
        public DecimalString MeasuredCenterFrequency { get { return Items.FindFirst<DecimalString>("0014401B") as DecimalString; } }
        public List<DecimalString> MeasuredCenterFrequency_ { get { return Items.FindAll<DecimalString>("0014401B").ToList(); } }
        public DecimalString MeasuredBandwidth { get { return Items.FindFirst<DecimalString>("0014401C") as DecimalString; } }
        public List<DecimalString> MeasuredBandwidth_ { get { return Items.FindAll<DecimalString>("0014401C").ToList(); } }
        public SequenceSelector PulserSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144020")); } }
        public List<SequenceSelector> PulserSettingsSequence_ { get { return Items.FindAll<Sequence>("00144020").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString PulseWidth { get { return Items.FindFirst<DecimalString>("00144022") as DecimalString; } }
        public List<DecimalString> PulseWidth_ { get { return Items.FindAll<DecimalString>("00144022").ToList(); } }
        public DecimalString ExcitationFrequency { get { return Items.FindFirst<DecimalString>("00144024") as DecimalString; } }
        public List<DecimalString> ExcitationFrequency_ { get { return Items.FindAll<DecimalString>("00144024").ToList(); } }
        public CodeString ModulationType { get { return Items.FindFirst<CodeString>("00144026") as CodeString; } }
        public List<CodeString> ModulationType_ { get { return Items.FindAll<CodeString>("00144026").ToList(); } }
        public DecimalString Damping { get { return Items.FindFirst<DecimalString>("00144028") as DecimalString; } }
        public List<DecimalString> Damping_ { get { return Items.FindAll<DecimalString>("00144028").ToList(); } }
        public SequenceSelector ReceiverSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144030")); } }
        public List<SequenceSelector> ReceiverSettingsSequence_ { get { return Items.FindAll<Sequence>("00144030").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString AcquiredSoundpathLength { get { return Items.FindFirst<DecimalString>("00144031") as DecimalString; } }
        public List<DecimalString> AcquiredSoundpathLength_ { get { return Items.FindAll<DecimalString>("00144031").ToList(); } }
        public CodeString AcquisitionCompressionType { get { return Items.FindFirst<CodeString>("00144032") as CodeString; } }
        public List<CodeString> AcquisitionCompressionType_ { get { return Items.FindAll<CodeString>("00144032").ToList(); } }
        public IntegerString AcquisitionSampleSize { get { return Items.FindFirst<IntegerString>("00144033") as IntegerString; } }
        public List<IntegerString> AcquisitionSampleSize_ { get { return Items.FindAll<IntegerString>("00144033").ToList(); } }
        public DecimalString RectifierSmoothing { get { return Items.FindFirst<DecimalString>("00144034") as DecimalString; } }
        public List<DecimalString> RectifierSmoothing_ { get { return Items.FindAll<DecimalString>("00144034").ToList(); } }
        public SequenceSelector DACSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144035")); } }
        public List<SequenceSelector> DACSequence_ { get { return Items.FindAll<Sequence>("00144035").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DACType { get { return Items.FindFirst<CodeString>("00144036") as CodeString; } }
        public List<CodeString> DACType_ { get { return Items.FindAll<CodeString>("00144036").ToList(); } }
        public DecimalString DACGainPoints { get { return Items.FindFirst<DecimalString>("00144038") as DecimalString; } }
        public List<DecimalString> DACGainPoints_ { get { return Items.FindAll<DecimalString>("00144038").ToList(); } }
        public DecimalString DACTimePoints { get { return Items.FindFirst<DecimalString>("0014403A") as DecimalString; } }
        public List<DecimalString> DACTimePoints_ { get { return Items.FindAll<DecimalString>("0014403A").ToList(); } }
        public DecimalString DACAmplitude { get { return Items.FindFirst<DecimalString>("0014403C") as DecimalString; } }
        public List<DecimalString> DACAmplitude_ { get { return Items.FindAll<DecimalString>("0014403C").ToList(); } }
        public SequenceSelector PreAmplifierSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144040")); } }
        public List<SequenceSelector> PreAmplifierSettingsSequence_ { get { return Items.FindAll<Sequence>("00144040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransmitTransducerSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144050")); } }
        public List<SequenceSelector> TransmitTransducerSettingsSequence_ { get { return Items.FindAll<Sequence>("00144050").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReceiveTransducerSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144051")); } }
        public List<SequenceSelector> ReceiveTransducerSettingsSequence_ { get { return Items.FindAll<Sequence>("00144051").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString IncidentAngle { get { return Items.FindFirst<DecimalString>("00144052") as DecimalString; } }
        public List<DecimalString> IncidentAngle_ { get { return Items.FindAll<DecimalString>("00144052").ToList(); } }
        public ShortText CouplingTechnique { get { return Items.FindFirst<ShortText>("00144054") as ShortText; } }
        public List<ShortText> CouplingTechnique_ { get { return Items.FindAll<ShortText>("00144054").ToList(); } }
        public ShortText CouplingMedium { get { return Items.FindFirst<ShortText>("00144056") as ShortText; } }
        public List<ShortText> CouplingMedium_ { get { return Items.FindAll<ShortText>("00144056").ToList(); } }
        public DecimalString CouplingVelocity { get { return Items.FindFirst<DecimalString>("00144057") as DecimalString; } }
        public List<DecimalString> CouplingVelocity_ { get { return Items.FindAll<DecimalString>("00144057").ToList(); } }
        public DecimalString CrystalCenterLocationX { get { return Items.FindFirst<DecimalString>("00144058") as DecimalString; } }
        public List<DecimalString> CrystalCenterLocationX_ { get { return Items.FindAll<DecimalString>("00144058").ToList(); } }
        public DecimalString CrystalCenterLocationZ { get { return Items.FindFirst<DecimalString>("00144059") as DecimalString; } }
        public List<DecimalString> CrystalCenterLocationZ_ { get { return Items.FindAll<DecimalString>("00144059").ToList(); } }
        public DecimalString SoundPathLength { get { return Items.FindFirst<DecimalString>("0014405A") as DecimalString; } }
        public List<DecimalString> SoundPathLength_ { get { return Items.FindAll<DecimalString>("0014405A").ToList(); } }
        public ShortText DelayLawIdentifier { get { return Items.FindFirst<ShortText>("0014405C") as ShortText; } }
        public List<ShortText> DelayLawIdentifier_ { get { return Items.FindAll<ShortText>("0014405C").ToList(); } }
        public SequenceSelector GateSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144060")); } }
        public List<SequenceSelector> GateSettingsSequence_ { get { return Items.FindAll<Sequence>("00144060").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString GateThreshold { get { return Items.FindFirst<DecimalString>("00144062") as DecimalString; } }
        public List<DecimalString> GateThreshold_ { get { return Items.FindAll<DecimalString>("00144062").ToList(); } }
        public DecimalString VelocityOfSound { get { return Items.FindFirst<DecimalString>("00144064") as DecimalString; } }
        public List<DecimalString> VelocityOfSound_ { get { return Items.FindAll<DecimalString>("00144064").ToList(); } }
        public SequenceSelector CalibrationSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00144070")); } }
        public List<SequenceSelector> CalibrationSettingsSequence_ { get { return Items.FindAll<Sequence>("00144070").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText CalibrationProcedure { get { return Items.FindFirst<ShortText>("00144072") as ShortText; } }
        public List<ShortText> CalibrationProcedure_ { get { return Items.FindAll<ShortText>("00144072").ToList(); } }
        public ShortString ProcedureVersion { get { return Items.FindFirst<ShortString>("00144074") as ShortString; } }
        public List<ShortString> ProcedureVersion_ { get { return Items.FindAll<ShortString>("00144074").ToList(); } }
        public Date ProcedureCreationDate { get { return Items.FindFirst<Date>("00144076") as Date; } }
        public List<Date> ProcedureCreationDate_ { get { return Items.FindAll<Date>("00144076").ToList(); } }
        public Date ProcedureExpirationDate { get { return Items.FindFirst<Date>("00144078") as Date; } }
        public List<Date> ProcedureExpirationDate_ { get { return Items.FindAll<Date>("00144078").ToList(); } }
        public Date ProcedureLastModifiedDate { get { return Items.FindFirst<Date>("0014407A") as Date; } }
        public List<Date> ProcedureLastModifiedDate_ { get { return Items.FindAll<Date>("0014407A").ToList(); } }
        public Time CalibrationTime { get { return Items.FindFirst<Time>("0014407C") as Time; } }
        public List<Time> CalibrationTime_ { get { return Items.FindAll<Time>("0014407C").ToList(); } }
        public Date CalibrationDate { get { return Items.FindFirst<Date>("0014407E") as Date; } }
        public List<Date> CalibrationDate_ { get { return Items.FindAll<Date>("0014407E").ToList(); } }
        public IntegerString LINACEnergy { get { return Items.FindFirst<IntegerString>("00145002") as IntegerString; } }
        public List<IntegerString> LINACEnergy_ { get { return Items.FindAll<IntegerString>("00145002").ToList(); } }
        public IntegerString LINACOutput { get { return Items.FindFirst<IntegerString>("00145004") as IntegerString; } }
        public List<IntegerString> LINACOutput_ { get { return Items.FindAll<IntegerString>("00145004").ToList(); } }
        public LongString ContrastBolusAgent { get { return Items.FindFirst<LongString>("00180010") as LongString; } }
        public List<LongString> ContrastBolusAgent_ { get { return Items.FindAll<LongString>("00180010").ToList(); } }
        public SequenceSelector ContrastBolusAgentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00180012")); } }
        public List<SequenceSelector> ContrastBolusAgentSequence_ { get { return Items.FindAll<Sequence>("00180012").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContrastBolusAdministrationRouteSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00180014")); } }
        public List<SequenceSelector> ContrastBolusAdministrationRouteSequence_ { get { return Items.FindAll<Sequence>("00180014").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BodyPartExamined { get { return Items.FindFirst<CodeString>("00180015") as CodeString; } }
        public List<CodeString> BodyPartExamined_ { get { return Items.FindAll<CodeString>("00180015").ToList(); } }
        public CodeString ScanningSequence { get { return Items.FindFirst<CodeString>("00180020") as CodeString; } }
        public List<CodeString> ScanningSequence_ { get { return Items.FindAll<CodeString>("00180020").ToList(); } }
        public CodeString SequenceVariant { get { return Items.FindFirst<CodeString>("00180021") as CodeString; } }
        public List<CodeString> SequenceVariant_ { get { return Items.FindAll<CodeString>("00180021").ToList(); } }
        public CodeString ScanOptions { get { return Items.FindFirst<CodeString>("00180022") as CodeString; } }
        public List<CodeString> ScanOptions_ { get { return Items.FindAll<CodeString>("00180022").ToList(); } }
        public CodeString MRAcquisitionType { get { return Items.FindFirst<CodeString>("00180023") as CodeString; } }
        public List<CodeString> MRAcquisitionType_ { get { return Items.FindAll<CodeString>("00180023").ToList(); } }
        public ShortString SequenceName { get { return Items.FindFirst<ShortString>("00180024") as ShortString; } }
        public List<ShortString> SequenceName_ { get { return Items.FindAll<ShortString>("00180024").ToList(); } }
        public CodeString AngioFlag { get { return Items.FindFirst<CodeString>("00180025") as CodeString; } }
        public List<CodeString> AngioFlag_ { get { return Items.FindAll<CodeString>("00180025").ToList(); } }
        public SequenceSelector InterventionDrugInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00180026")); } }
        public List<SequenceSelector> InterventionDrugInformationSequence_ { get { return Items.FindAll<Sequence>("00180026").Select(s => new SequenceSelector(s)).ToList(); } }
        public Time InterventionDrugStopTime { get { return Items.FindFirst<Time>("00180027") as Time; } }
        public List<Time> InterventionDrugStopTime_ { get { return Items.FindAll<Time>("00180027").ToList(); } }
        public DecimalString InterventionDrugDose { get { return Items.FindFirst<DecimalString>("00180028") as DecimalString; } }
        public List<DecimalString> InterventionDrugDose_ { get { return Items.FindAll<DecimalString>("00180028").ToList(); } }
        public SequenceSelector InterventionDrugCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00180029")); } }
        public List<SequenceSelector> InterventionDrugCodeSequence_ { get { return Items.FindAll<Sequence>("00180029").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AdditionalDrugSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018002A")); } }
        public List<SequenceSelector> AdditionalDrugSequence_ { get { return Items.FindAll<Sequence>("0018002A").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString RadionuclideRetired { get { return Items.FindFirst<LongString>("00180030") as LongString; } }
        public List<LongString> RadionuclideRetired_ { get { return Items.FindAll<LongString>("00180030").ToList(); } }
        public LongString Radiopharmaceutical { get { return Items.FindFirst<LongString>("00180031") as LongString; } }
        public List<LongString> Radiopharmaceutical_ { get { return Items.FindAll<LongString>("00180031").ToList(); } }
        public DecimalString EnergyWindowCenterlineRetired { get { return Items.FindFirst<DecimalString>("00180032") as DecimalString; } }
        public List<DecimalString> EnergyWindowCenterlineRetired_ { get { return Items.FindAll<DecimalString>("00180032").ToList(); } }
        public DecimalString EnergyWindowTotalWidthRetired { get { return Items.FindFirst<DecimalString>("00180033") as DecimalString; } }
        public List<DecimalString> EnergyWindowTotalWidthRetired_ { get { return Items.FindAll<DecimalString>("00180033").ToList(); } }
        public LongString InterventionDrugName { get { return Items.FindFirst<LongString>("00180034") as LongString; } }
        public List<LongString> InterventionDrugName_ { get { return Items.FindAll<LongString>("00180034").ToList(); } }
        public Time InterventionDrugStartTime { get { return Items.FindFirst<Time>("00180035") as Time; } }
        public List<Time> InterventionDrugStartTime_ { get { return Items.FindAll<Time>("00180035").ToList(); } }
        public SequenceSelector InterventionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00180036")); } }
        public List<SequenceSelector> InterventionSequence_ { get { return Items.FindAll<Sequence>("00180036").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString TherapyTypeRetired { get { return Items.FindFirst<CodeString>("00180037") as CodeString; } }
        public List<CodeString> TherapyTypeRetired_ { get { return Items.FindAll<CodeString>("00180037").ToList(); } }
        public CodeString InterventionStatus { get { return Items.FindFirst<CodeString>("00180038") as CodeString; } }
        public List<CodeString> InterventionStatus_ { get { return Items.FindAll<CodeString>("00180038").ToList(); } }
        public CodeString TherapyDescriptionRetired { get { return Items.FindFirst<CodeString>("00180039") as CodeString; } }
        public List<CodeString> TherapyDescriptionRetired_ { get { return Items.FindAll<CodeString>("00180039").ToList(); } }
        public ShortText InterventionDescription { get { return Items.FindFirst<ShortText>("0018003A") as ShortText; } }
        public List<ShortText> InterventionDescription_ { get { return Items.FindAll<ShortText>("0018003A").ToList(); } }
        public IntegerString CineRate { get { return Items.FindFirst<IntegerString>("00180040") as IntegerString; } }
        public List<IntegerString> CineRate_ { get { return Items.FindAll<IntegerString>("00180040").ToList(); } }
        public CodeString InitialCineRunState { get { return Items.FindFirst<CodeString>("00180042") as CodeString; } }
        public List<CodeString> InitialCineRunState_ { get { return Items.FindAll<CodeString>("00180042").ToList(); } }
        public DecimalString SliceThickness { get { return Items.FindFirst<DecimalString>("00180050") as DecimalString; } }
        public List<DecimalString> SliceThickness_ { get { return Items.FindAll<DecimalString>("00180050").ToList(); } }
        public DecimalString KVP { get { return Items.FindFirst<DecimalString>("00180060") as DecimalString; } }
        public List<DecimalString> KVP_ { get { return Items.FindAll<DecimalString>("00180060").ToList(); } }
        public IntegerString CountsAccumulated { get { return Items.FindFirst<IntegerString>("00180070") as IntegerString; } }
        public List<IntegerString> CountsAccumulated_ { get { return Items.FindAll<IntegerString>("00180070").ToList(); } }
        public CodeString AcquisitionTerminationCondition { get { return Items.FindFirst<CodeString>("00180071") as CodeString; } }
        public List<CodeString> AcquisitionTerminationCondition_ { get { return Items.FindAll<CodeString>("00180071").ToList(); } }
        public DecimalString EffectiveDuration { get { return Items.FindFirst<DecimalString>("00180072") as DecimalString; } }
        public List<DecimalString> EffectiveDuration_ { get { return Items.FindAll<DecimalString>("00180072").ToList(); } }
        public CodeString AcquisitionStartCondition { get { return Items.FindFirst<CodeString>("00180073") as CodeString; } }
        public List<CodeString> AcquisitionStartCondition_ { get { return Items.FindAll<CodeString>("00180073").ToList(); } }
        public IntegerString AcquisitionStartConditionData { get { return Items.FindFirst<IntegerString>("00180074") as IntegerString; } }
        public List<IntegerString> AcquisitionStartConditionData_ { get { return Items.FindAll<IntegerString>("00180074").ToList(); } }
        public IntegerString AcquisitionTerminationConditionData { get { return Items.FindFirst<IntegerString>("00180075") as IntegerString; } }
        public List<IntegerString> AcquisitionTerminationConditionData_ { get { return Items.FindAll<IntegerString>("00180075").ToList(); } }
        public DecimalString RepetitionTime { get { return Items.FindFirst<DecimalString>("00180080") as DecimalString; } }
        public List<DecimalString> RepetitionTime_ { get { return Items.FindAll<DecimalString>("00180080").ToList(); } }
        public DecimalString EchoTime { get { return Items.FindFirst<DecimalString>("00180081") as DecimalString; } }
        public List<DecimalString> EchoTime_ { get { return Items.FindAll<DecimalString>("00180081").ToList(); } }
        public DecimalString InversionTime { get { return Items.FindFirst<DecimalString>("00180082") as DecimalString; } }
        public List<DecimalString> InversionTime_ { get { return Items.FindAll<DecimalString>("00180082").ToList(); } }
        public DecimalString NumberOfAverages { get { return Items.FindFirst<DecimalString>("00180083") as DecimalString; } }
        public List<DecimalString> NumberOfAverages_ { get { return Items.FindAll<DecimalString>("00180083").ToList(); } }
        public DecimalString ImagingFrequency { get { return Items.FindFirst<DecimalString>("00180084") as DecimalString; } }
        public List<DecimalString> ImagingFrequency_ { get { return Items.FindAll<DecimalString>("00180084").ToList(); } }
        public ShortString ImagedNucleus { get { return Items.FindFirst<ShortString>("00180085") as ShortString; } }
        public List<ShortString> ImagedNucleus_ { get { return Items.FindAll<ShortString>("00180085").ToList(); } }
        public IntegerString EchoNumbers { get { return Items.FindFirst<IntegerString>("00180086") as IntegerString; } }
        public List<IntegerString> EchoNumbers_ { get { return Items.FindAll<IntegerString>("00180086").ToList(); } }
        public DecimalString MagneticFieldStrength { get { return Items.FindFirst<DecimalString>("00180087") as DecimalString; } }
        public List<DecimalString> MagneticFieldStrength_ { get { return Items.FindAll<DecimalString>("00180087").ToList(); } }
        public DecimalString SpacingBetweenSlices { get { return Items.FindFirst<DecimalString>("00180088") as DecimalString; } }
        public List<DecimalString> SpacingBetweenSlices_ { get { return Items.FindAll<DecimalString>("00180088").ToList(); } }
        public IntegerString NumberOfPhaseEncodingSteps { get { return Items.FindFirst<IntegerString>("00180089") as IntegerString; } }
        public List<IntegerString> NumberOfPhaseEncodingSteps_ { get { return Items.FindAll<IntegerString>("00180089").ToList(); } }
        public DecimalString DataCollectionDiameter { get { return Items.FindFirst<DecimalString>("00180090") as DecimalString; } }
        public List<DecimalString> DataCollectionDiameter_ { get { return Items.FindAll<DecimalString>("00180090").ToList(); } }
        public IntegerString EchoTrainLength { get { return Items.FindFirst<IntegerString>("00180091") as IntegerString; } }
        public List<IntegerString> EchoTrainLength_ { get { return Items.FindAll<IntegerString>("00180091").ToList(); } }
        public DecimalString PercentSampling { get { return Items.FindFirst<DecimalString>("00180093") as DecimalString; } }
        public List<DecimalString> PercentSampling_ { get { return Items.FindAll<DecimalString>("00180093").ToList(); } }
        public DecimalString PercentPhaseFieldOfView { get { return Items.FindFirst<DecimalString>("00180094") as DecimalString; } }
        public List<DecimalString> PercentPhaseFieldOfView_ { get { return Items.FindAll<DecimalString>("00180094").ToList(); } }
        public DecimalString PixelBandwidth { get { return Items.FindFirst<DecimalString>("00180095") as DecimalString; } }
        public List<DecimalString> PixelBandwidth_ { get { return Items.FindAll<DecimalString>("00180095").ToList(); } }
        public LongString DeviceSerialNumber { get { return Items.FindFirst<LongString>("00181000") as LongString; } }
        public List<LongString> DeviceSerialNumber_ { get { return Items.FindAll<LongString>("00181000").ToList(); } }
        public UniqueIdentifier DeviceUID { get { return Items.FindFirst<UniqueIdentifier>("00181002") as UniqueIdentifier; } }
        public List<UniqueIdentifier> DeviceUID_ { get { return Items.FindAll<UniqueIdentifier>("00181002").ToList(); } }
        public LongString DeviceID { get { return Items.FindFirst<LongString>("00181003") as LongString; } }
        public List<LongString> DeviceID_ { get { return Items.FindAll<LongString>("00181003").ToList(); } }
        public LongString PlateID { get { return Items.FindFirst<LongString>("00181004") as LongString; } }
        public List<LongString> PlateID_ { get { return Items.FindAll<LongString>("00181004").ToList(); } }
        public LongString GeneratorID { get { return Items.FindFirst<LongString>("00181005") as LongString; } }
        public List<LongString> GeneratorID_ { get { return Items.FindAll<LongString>("00181005").ToList(); } }
        public LongString GridID { get { return Items.FindFirst<LongString>("00181006") as LongString; } }
        public List<LongString> GridID_ { get { return Items.FindAll<LongString>("00181006").ToList(); } }
        public LongString CassetteID { get { return Items.FindFirst<LongString>("00181007") as LongString; } }
        public List<LongString> CassetteID_ { get { return Items.FindAll<LongString>("00181007").ToList(); } }
        public LongString GantryID { get { return Items.FindFirst<LongString>("00181008") as LongString; } }
        public List<LongString> GantryID_ { get { return Items.FindAll<LongString>("00181008").ToList(); } }
        public LongString SecondaryCaptureDeviceID { get { return Items.FindFirst<LongString>("00181010") as LongString; } }
        public List<LongString> SecondaryCaptureDeviceID_ { get { return Items.FindAll<LongString>("00181010").ToList(); } }
        public LongString HardcopyCreationDeviceIDRetired { get { return Items.FindFirst<LongString>("00181011") as LongString; } }
        public List<LongString> HardcopyCreationDeviceIDRetired_ { get { return Items.FindAll<LongString>("00181011").ToList(); } }
        public Date DateOfSecondaryCapture { get { return Items.FindFirst<Date>("00181012") as Date; } }
        public List<Date> DateOfSecondaryCapture_ { get { return Items.FindAll<Date>("00181012").ToList(); } }
        public Time TimeOfSecondaryCapture { get { return Items.FindFirst<Time>("00181014") as Time; } }
        public List<Time> TimeOfSecondaryCapture_ { get { return Items.FindAll<Time>("00181014").ToList(); } }
        public LongString SecondaryCaptureDeviceManufacturer { get { return Items.FindFirst<LongString>("00181016") as LongString; } }
        public List<LongString> SecondaryCaptureDeviceManufacturer_ { get { return Items.FindAll<LongString>("00181016").ToList(); } }
        public LongString HardcopyDeviceManufacturerRetired { get { return Items.FindFirst<LongString>("00181017") as LongString; } }
        public List<LongString> HardcopyDeviceManufacturerRetired_ { get { return Items.FindAll<LongString>("00181017").ToList(); } }
        public LongString SecondaryCaptureDeviceManufacturerModelName { get { return Items.FindFirst<LongString>("00181018") as LongString; } }
        public List<LongString> SecondaryCaptureDeviceManufacturerModelName_ { get { return Items.FindAll<LongString>("00181018").ToList(); } }
        public LongString SecondaryCaptureDeviceSoftwareVersions { get { return Items.FindFirst<LongString>("00181019") as LongString; } }
        public List<LongString> SecondaryCaptureDeviceSoftwareVersions_ { get { return Items.FindAll<LongString>("00181019").ToList(); } }
        public LongString HardcopyDeviceSoftwareVersionRetired { get { return Items.FindFirst<LongString>("0018101A") as LongString; } }
        public List<LongString> HardcopyDeviceSoftwareVersionRetired_ { get { return Items.FindAll<LongString>("0018101A").ToList(); } }
        public LongString HardcopyDeviceManufacturerModelNameRetired { get { return Items.FindFirst<LongString>("0018101B") as LongString; } }
        public List<LongString> HardcopyDeviceManufacturerModelNameRetired_ { get { return Items.FindAll<LongString>("0018101B").ToList(); } }
        public LongString SoftwareVersions { get { return Items.FindFirst<LongString>("00181020") as LongString; } }
        public List<LongString> SoftwareVersions_ { get { return Items.FindAll<LongString>("00181020").ToList(); } }
        public ShortString VideoImageFormatAcquired { get { return Items.FindFirst<ShortString>("00181022") as ShortString; } }
        public List<ShortString> VideoImageFormatAcquired_ { get { return Items.FindAll<ShortString>("00181022").ToList(); } }
        public LongString DigitalImageFormatAcquired { get { return Items.FindFirst<LongString>("00181023") as LongString; } }
        public List<LongString> DigitalImageFormatAcquired_ { get { return Items.FindAll<LongString>("00181023").ToList(); } }
        public LongString ProtocolName { get { return Items.FindFirst<LongString>("00181030") as LongString; } }
        public List<LongString> ProtocolName_ { get { return Items.FindAll<LongString>("00181030").ToList(); } }
        public LongString ContrastBolusRoute { get { return Items.FindFirst<LongString>("00181040") as LongString; } }
        public List<LongString> ContrastBolusRoute_ { get { return Items.FindAll<LongString>("00181040").ToList(); } }
        public DecimalString ContrastBolusVolume { get { return Items.FindFirst<DecimalString>("00181041") as DecimalString; } }
        public List<DecimalString> ContrastBolusVolume_ { get { return Items.FindAll<DecimalString>("00181041").ToList(); } }
        public Time ContrastBolusStartTime { get { return Items.FindFirst<Time>("00181042") as Time; } }
        public List<Time> ContrastBolusStartTime_ { get { return Items.FindAll<Time>("00181042").ToList(); } }
        public Time ContrastBolusStopTime { get { return Items.FindFirst<Time>("00181043") as Time; } }
        public List<Time> ContrastBolusStopTime_ { get { return Items.FindAll<Time>("00181043").ToList(); } }
        public DecimalString ContrastBolusTotalDose { get { return Items.FindFirst<DecimalString>("00181044") as DecimalString; } }
        public List<DecimalString> ContrastBolusTotalDose_ { get { return Items.FindAll<DecimalString>("00181044").ToList(); } }
        public IntegerString SyringeCounts { get { return Items.FindFirst<IntegerString>("00181045") as IntegerString; } }
        public List<IntegerString> SyringeCounts_ { get { return Items.FindAll<IntegerString>("00181045").ToList(); } }
        public DecimalString ContrastFlowRate { get { return Items.FindFirst<DecimalString>("00181046") as DecimalString; } }
        public List<DecimalString> ContrastFlowRate_ { get { return Items.FindAll<DecimalString>("00181046").ToList(); } }
        public DecimalString ContrastFlowDuration { get { return Items.FindFirst<DecimalString>("00181047") as DecimalString; } }
        public List<DecimalString> ContrastFlowDuration_ { get { return Items.FindAll<DecimalString>("00181047").ToList(); } }
        public CodeString ContrastBolusIngredient { get { return Items.FindFirst<CodeString>("00181048") as CodeString; } }
        public List<CodeString> ContrastBolusIngredient_ { get { return Items.FindAll<CodeString>("00181048").ToList(); } }
        public DecimalString ContrastBolusIngredientConcentration { get { return Items.FindFirst<DecimalString>("00181049") as DecimalString; } }
        public List<DecimalString> ContrastBolusIngredientConcentration_ { get { return Items.FindAll<DecimalString>("00181049").ToList(); } }
        public DecimalString SpatialResolution { get { return Items.FindFirst<DecimalString>("00181050") as DecimalString; } }
        public List<DecimalString> SpatialResolution_ { get { return Items.FindAll<DecimalString>("00181050").ToList(); } }
        public DecimalString TriggerTime { get { return Items.FindFirst<DecimalString>("00181060") as DecimalString; } }
        public List<DecimalString> TriggerTime_ { get { return Items.FindAll<DecimalString>("00181060").ToList(); } }
        public LongString TriggerSourceOrType { get { return Items.FindFirst<LongString>("00181061") as LongString; } }
        public List<LongString> TriggerSourceOrType_ { get { return Items.FindAll<LongString>("00181061").ToList(); } }
        public IntegerString NominalInterval { get { return Items.FindFirst<IntegerString>("00181062") as IntegerString; } }
        public List<IntegerString> NominalInterval_ { get { return Items.FindAll<IntegerString>("00181062").ToList(); } }
        public DecimalString FrameTime { get { return Items.FindFirst<DecimalString>("00181063") as DecimalString; } }
        public List<DecimalString> FrameTime_ { get { return Items.FindAll<DecimalString>("00181063").ToList(); } }
        public LongString CardiacFramingType { get { return Items.FindFirst<LongString>("00181064") as LongString; } }
        public List<LongString> CardiacFramingType_ { get { return Items.FindAll<LongString>("00181064").ToList(); } }
        public DecimalString FrameTimeVector { get { return Items.FindFirst<DecimalString>("00181065") as DecimalString; } }
        public List<DecimalString> FrameTimeVector_ { get { return Items.FindAll<DecimalString>("00181065").ToList(); } }
        public DecimalString FrameDelay { get { return Items.FindFirst<DecimalString>("00181066") as DecimalString; } }
        public List<DecimalString> FrameDelay_ { get { return Items.FindAll<DecimalString>("00181066").ToList(); } }
        public DecimalString ImageTriggerDelay { get { return Items.FindFirst<DecimalString>("00181067") as DecimalString; } }
        public List<DecimalString> ImageTriggerDelay_ { get { return Items.FindAll<DecimalString>("00181067").ToList(); } }
        public DecimalString MultiplexGroupTimeOffset { get { return Items.FindFirst<DecimalString>("00181068") as DecimalString; } }
        public List<DecimalString> MultiplexGroupTimeOffset_ { get { return Items.FindAll<DecimalString>("00181068").ToList(); } }
        public DecimalString TriggerTimeOffset { get { return Items.FindFirst<DecimalString>("00181069") as DecimalString; } }
        public List<DecimalString> TriggerTimeOffset_ { get { return Items.FindAll<DecimalString>("00181069").ToList(); } }
        public CodeString SynchronizationTrigger { get { return Items.FindFirst<CodeString>("0018106A") as CodeString; } }
        public List<CodeString> SynchronizationTrigger_ { get { return Items.FindAll<CodeString>("0018106A").ToList(); } }
        public UnsignedShort SynchronizationChannel { get { return Items.FindFirst<UnsignedShort>("0018106C") as UnsignedShort; } }
        public List<UnsignedShort> SynchronizationChannel_ { get { return Items.FindAll<UnsignedShort>("0018106C").ToList(); } }
        public UnsignedLong TriggerSamplePosition { get { return Items.FindFirst<UnsignedLong>("0018106E") as UnsignedLong; } }
        public List<UnsignedLong> TriggerSamplePosition_ { get { return Items.FindAll<UnsignedLong>("0018106E").ToList(); } }
        public LongString RadiopharmaceuticalRoute { get { return Items.FindFirst<LongString>("00181070") as LongString; } }
        public List<LongString> RadiopharmaceuticalRoute_ { get { return Items.FindAll<LongString>("00181070").ToList(); } }
        public DecimalString RadiopharmaceuticalVolume { get { return Items.FindFirst<DecimalString>("00181071") as DecimalString; } }
        public List<DecimalString> RadiopharmaceuticalVolume_ { get { return Items.FindAll<DecimalString>("00181071").ToList(); } }
        public Time RadiopharmaceuticalStartTime { get { return Items.FindFirst<Time>("00181072") as Time; } }
        public List<Time> RadiopharmaceuticalStartTime_ { get { return Items.FindAll<Time>("00181072").ToList(); } }
        public Time RadiopharmaceuticalStopTime { get { return Items.FindFirst<Time>("00181073") as Time; } }
        public List<Time> RadiopharmaceuticalStopTime_ { get { return Items.FindAll<Time>("00181073").ToList(); } }
        public DecimalString RadionuclideTotalDose { get { return Items.FindFirst<DecimalString>("00181074") as DecimalString; } }
        public List<DecimalString> RadionuclideTotalDose_ { get { return Items.FindAll<DecimalString>("00181074").ToList(); } }
        public DecimalString RadionuclideHalfLife { get { return Items.FindFirst<DecimalString>("00181075") as DecimalString; } }
        public List<DecimalString> RadionuclideHalfLife_ { get { return Items.FindAll<DecimalString>("00181075").ToList(); } }
        public DecimalString RadionuclidePositronFraction { get { return Items.FindFirst<DecimalString>("00181076") as DecimalString; } }
        public List<DecimalString> RadionuclidePositronFraction_ { get { return Items.FindAll<DecimalString>("00181076").ToList(); } }
        public DecimalString RadiopharmaceuticalSpecificActivity { get { return Items.FindFirst<DecimalString>("00181077") as DecimalString; } }
        public List<DecimalString> RadiopharmaceuticalSpecificActivity_ { get { return Items.FindAll<DecimalString>("00181077").ToList(); } }
        public EvilDICOM.Core.Element.DateTime RadiopharmaceuticalStartDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00181078") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> RadiopharmaceuticalStartDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00181078").ToList(); } }
        public EvilDICOM.Core.Element.DateTime RadiopharmaceuticalStopDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00181079") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> RadiopharmaceuticalStopDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00181079").ToList(); } }
        public CodeString BeatRejectionFlag { get { return Items.FindFirst<CodeString>("00181080") as CodeString; } }
        public List<CodeString> BeatRejectionFlag_ { get { return Items.FindAll<CodeString>("00181080").ToList(); } }
        public IntegerString LowRRValue { get { return Items.FindFirst<IntegerString>("00181081") as IntegerString; } }
        public List<IntegerString> LowRRValue_ { get { return Items.FindAll<IntegerString>("00181081").ToList(); } }
        public IntegerString HighRRValue { get { return Items.FindFirst<IntegerString>("00181082") as IntegerString; } }
        public List<IntegerString> HighRRValue_ { get { return Items.FindAll<IntegerString>("00181082").ToList(); } }
        public IntegerString IntervalsAcquired { get { return Items.FindFirst<IntegerString>("00181083") as IntegerString; } }
        public List<IntegerString> IntervalsAcquired_ { get { return Items.FindAll<IntegerString>("00181083").ToList(); } }
        public IntegerString IntervalsRejected { get { return Items.FindFirst<IntegerString>("00181084") as IntegerString; } }
        public List<IntegerString> IntervalsRejected_ { get { return Items.FindAll<IntegerString>("00181084").ToList(); } }
        public LongString PVCRejection { get { return Items.FindFirst<LongString>("00181085") as LongString; } }
        public List<LongString> PVCRejection_ { get { return Items.FindAll<LongString>("00181085").ToList(); } }
        public IntegerString SkipBeats { get { return Items.FindFirst<IntegerString>("00181086") as IntegerString; } }
        public List<IntegerString> SkipBeats_ { get { return Items.FindAll<IntegerString>("00181086").ToList(); } }
        public IntegerString HeartRate { get { return Items.FindFirst<IntegerString>("00181088") as IntegerString; } }
        public List<IntegerString> HeartRate_ { get { return Items.FindAll<IntegerString>("00181088").ToList(); } }
        public IntegerString CardiacNumberOfImages { get { return Items.FindFirst<IntegerString>("00181090") as IntegerString; } }
        public List<IntegerString> CardiacNumberOfImages_ { get { return Items.FindAll<IntegerString>("00181090").ToList(); } }
        public IntegerString TriggerWindow { get { return Items.FindFirst<IntegerString>("00181094") as IntegerString; } }
        public List<IntegerString> TriggerWindow_ { get { return Items.FindAll<IntegerString>("00181094").ToList(); } }
        public DecimalString ReconstructionDiameter { get { return Items.FindFirst<DecimalString>("00181100") as DecimalString; } }
        public List<DecimalString> ReconstructionDiameter_ { get { return Items.FindAll<DecimalString>("00181100").ToList(); } }
        public DecimalString DistanceSourceToDetector { get { return Items.FindFirst<DecimalString>("00181110") as DecimalString; } }
        public List<DecimalString> DistanceSourceToDetector_ { get { return Items.FindAll<DecimalString>("00181110").ToList(); } }
        public DecimalString DistanceSourceToPatient { get { return Items.FindFirst<DecimalString>("00181111") as DecimalString; } }
        public List<DecimalString> DistanceSourceToPatient_ { get { return Items.FindAll<DecimalString>("00181111").ToList(); } }
        public DecimalString EstimatedRadiographicMagnificationFactor { get { return Items.FindFirst<DecimalString>("00181114") as DecimalString; } }
        public List<DecimalString> EstimatedRadiographicMagnificationFactor_ { get { return Items.FindAll<DecimalString>("00181114").ToList(); } }
        public DecimalString GantryDetectorTilt { get { return Items.FindFirst<DecimalString>("00181120") as DecimalString; } }
        public List<DecimalString> GantryDetectorTilt_ { get { return Items.FindAll<DecimalString>("00181120").ToList(); } }
        public DecimalString GantryDetectorSlew { get { return Items.FindFirst<DecimalString>("00181121") as DecimalString; } }
        public List<DecimalString> GantryDetectorSlew_ { get { return Items.FindAll<DecimalString>("00181121").ToList(); } }
        public DecimalString TableHeight { get { return Items.FindFirst<DecimalString>("00181130") as DecimalString; } }
        public List<DecimalString> TableHeight_ { get { return Items.FindAll<DecimalString>("00181130").ToList(); } }
        public DecimalString TableTraverse { get { return Items.FindFirst<DecimalString>("00181131") as DecimalString; } }
        public List<DecimalString> TableTraverse_ { get { return Items.FindAll<DecimalString>("00181131").ToList(); } }
        public CodeString TableMotion { get { return Items.FindFirst<CodeString>("00181134") as CodeString; } }
        public List<CodeString> TableMotion_ { get { return Items.FindAll<CodeString>("00181134").ToList(); } }
        public DecimalString TableVerticalIncrement { get { return Items.FindFirst<DecimalString>("00181135") as DecimalString; } }
        public List<DecimalString> TableVerticalIncrement_ { get { return Items.FindAll<DecimalString>("00181135").ToList(); } }
        public DecimalString TableLateralIncrement { get { return Items.FindFirst<DecimalString>("00181136") as DecimalString; } }
        public List<DecimalString> TableLateralIncrement_ { get { return Items.FindAll<DecimalString>("00181136").ToList(); } }
        public DecimalString TableLongitudinalIncrement { get { return Items.FindFirst<DecimalString>("00181137") as DecimalString; } }
        public List<DecimalString> TableLongitudinalIncrement_ { get { return Items.FindAll<DecimalString>("00181137").ToList(); } }
        public DecimalString TableAngle { get { return Items.FindFirst<DecimalString>("00181138") as DecimalString; } }
        public List<DecimalString> TableAngle_ { get { return Items.FindAll<DecimalString>("00181138").ToList(); } }
        public CodeString TableType { get { return Items.FindFirst<CodeString>("0018113A") as CodeString; } }
        public List<CodeString> TableType_ { get { return Items.FindAll<CodeString>("0018113A").ToList(); } }
        public CodeString RotationDirection { get { return Items.FindFirst<CodeString>("00181140") as CodeString; } }
        public List<CodeString> RotationDirection_ { get { return Items.FindAll<CodeString>("00181140").ToList(); } }
        public DecimalString AngularPositionRetired { get { return Items.FindFirst<DecimalString>("00181141") as DecimalString; } }
        public List<DecimalString> AngularPositionRetired_ { get { return Items.FindAll<DecimalString>("00181141").ToList(); } }
        public DecimalString RadialPosition { get { return Items.FindFirst<DecimalString>("00181142") as DecimalString; } }
        public List<DecimalString> RadialPosition_ { get { return Items.FindAll<DecimalString>("00181142").ToList(); } }
        public DecimalString ScanArc { get { return Items.FindFirst<DecimalString>("00181143") as DecimalString; } }
        public List<DecimalString> ScanArc_ { get { return Items.FindAll<DecimalString>("00181143").ToList(); } }
        public DecimalString AngularStep { get { return Items.FindFirst<DecimalString>("00181144") as DecimalString; } }
        public List<DecimalString> AngularStep_ { get { return Items.FindAll<DecimalString>("00181144").ToList(); } }
        public DecimalString CenterOfRotationOffset { get { return Items.FindFirst<DecimalString>("00181145") as DecimalString; } }
        public List<DecimalString> CenterOfRotationOffset_ { get { return Items.FindAll<DecimalString>("00181145").ToList(); } }
        public DecimalString RotationOffsetRetired { get { return Items.FindFirst<DecimalString>("00181146") as DecimalString; } }
        public List<DecimalString> RotationOffsetRetired_ { get { return Items.FindAll<DecimalString>("00181146").ToList(); } }
        public CodeString FieldOfViewShape { get { return Items.FindFirst<CodeString>("00181147") as CodeString; } }
        public List<CodeString> FieldOfViewShape_ { get { return Items.FindAll<CodeString>("00181147").ToList(); } }
        public IntegerString FieldOfViewDimensions { get { return Items.FindFirst<IntegerString>("00181149") as IntegerString; } }
        public List<IntegerString> FieldOfViewDimensions_ { get { return Items.FindAll<IntegerString>("00181149").ToList(); } }
        public IntegerString ExposureTime { get { return Items.FindFirst<IntegerString>("00181150") as IntegerString; } }
        public List<IntegerString> ExposureTime_ { get { return Items.FindAll<IntegerString>("00181150").ToList(); } }
        public IntegerString XRayTubeCurrent { get { return Items.FindFirst<IntegerString>("00181151") as IntegerString; } }
        public List<IntegerString> XRayTubeCurrent_ { get { return Items.FindAll<IntegerString>("00181151").ToList(); } }
        public IntegerString Exposure { get { return Items.FindFirst<IntegerString>("00181152") as IntegerString; } }
        public List<IntegerString> Exposure_ { get { return Items.FindAll<IntegerString>("00181152").ToList(); } }
        public IntegerString ExposureInuAs { get { return Items.FindFirst<IntegerString>("00181153") as IntegerString; } }
        public List<IntegerString> ExposureInuAs_ { get { return Items.FindAll<IntegerString>("00181153").ToList(); } }
        public DecimalString AveragePulseWidth { get { return Items.FindFirst<DecimalString>("00181154") as DecimalString; } }
        public List<DecimalString> AveragePulseWidth_ { get { return Items.FindAll<DecimalString>("00181154").ToList(); } }
        public CodeString RadiationSetting { get { return Items.FindFirst<CodeString>("00181155") as CodeString; } }
        public List<CodeString> RadiationSetting_ { get { return Items.FindAll<CodeString>("00181155").ToList(); } }
        public CodeString RectificationType { get { return Items.FindFirst<CodeString>("00181156") as CodeString; } }
        public List<CodeString> RectificationType_ { get { return Items.FindAll<CodeString>("00181156").ToList(); } }
        public CodeString RadiationMode { get { return Items.FindFirst<CodeString>("0018115A") as CodeString; } }
        public List<CodeString> RadiationMode_ { get { return Items.FindAll<CodeString>("0018115A").ToList(); } }
        public DecimalString ImageAndFluoroscopyAreaDoseProduct { get { return Items.FindFirst<DecimalString>("0018115E") as DecimalString; } }
        public List<DecimalString> ImageAndFluoroscopyAreaDoseProduct_ { get { return Items.FindAll<DecimalString>("0018115E").ToList(); } }
        public ShortString FilterType { get { return Items.FindFirst<ShortString>("00181160") as ShortString; } }
        public List<ShortString> FilterType_ { get { return Items.FindAll<ShortString>("00181160").ToList(); } }
        public LongString TypeOfFilters { get { return Items.FindFirst<LongString>("00181161") as LongString; } }
        public List<LongString> TypeOfFilters_ { get { return Items.FindAll<LongString>("00181161").ToList(); } }
        public DecimalString IntensifierSize { get { return Items.FindFirst<DecimalString>("00181162") as DecimalString; } }
        public List<DecimalString> IntensifierSize_ { get { return Items.FindAll<DecimalString>("00181162").ToList(); } }
        public DecimalString ImagerPixelSpacing { get { return Items.FindFirst<DecimalString>("00181164") as DecimalString; } }
        public List<DecimalString> ImagerPixelSpacing_ { get { return Items.FindAll<DecimalString>("00181164").ToList(); } }
        public CodeString Grid { get { return Items.FindFirst<CodeString>("00181166") as CodeString; } }
        public List<CodeString> Grid_ { get { return Items.FindAll<CodeString>("00181166").ToList(); } }
        public IntegerString GeneratorPower { get { return Items.FindFirst<IntegerString>("00181170") as IntegerString; } }
        public List<IntegerString> GeneratorPower_ { get { return Items.FindAll<IntegerString>("00181170").ToList(); } }
        public ShortString CollimatorGridName { get { return Items.FindFirst<ShortString>("00181180") as ShortString; } }
        public List<ShortString> CollimatorGridName_ { get { return Items.FindAll<ShortString>("00181180").ToList(); } }
        public CodeString CollimatorType { get { return Items.FindFirst<CodeString>("00181181") as CodeString; } }
        public List<CodeString> CollimatorType_ { get { return Items.FindAll<CodeString>("00181181").ToList(); } }
        public IntegerString FocalDistance { get { return Items.FindFirst<IntegerString>("00181182") as IntegerString; } }
        public List<IntegerString> FocalDistance_ { get { return Items.FindAll<IntegerString>("00181182").ToList(); } }
        public DecimalString XFocusCenter { get { return Items.FindFirst<DecimalString>("00181183") as DecimalString; } }
        public List<DecimalString> XFocusCenter_ { get { return Items.FindAll<DecimalString>("00181183").ToList(); } }
        public DecimalString YFocusCenter { get { return Items.FindFirst<DecimalString>("00181184") as DecimalString; } }
        public List<DecimalString> YFocusCenter_ { get { return Items.FindAll<DecimalString>("00181184").ToList(); } }
        public DecimalString FocalSpots { get { return Items.FindFirst<DecimalString>("00181190") as DecimalString; } }
        public List<DecimalString> FocalSpots_ { get { return Items.FindAll<DecimalString>("00181190").ToList(); } }
        public CodeString AnodeTargetMaterial { get { return Items.FindFirst<CodeString>("00181191") as CodeString; } }
        public List<CodeString> AnodeTargetMaterial_ { get { return Items.FindAll<CodeString>("00181191").ToList(); } }
        public DecimalString BodyPartThickness { get { return Items.FindFirst<DecimalString>("001811A0") as DecimalString; } }
        public List<DecimalString> BodyPartThickness_ { get { return Items.FindAll<DecimalString>("001811A0").ToList(); } }
        public DecimalString CompressionForce { get { return Items.FindFirst<DecimalString>("001811A2") as DecimalString; } }
        public List<DecimalString> CompressionForce_ { get { return Items.FindAll<DecimalString>("001811A2").ToList(); } }
        public Date DateOfLastCalibration { get { return Items.FindFirst<Date>("00181200") as Date; } }
        public List<Date> DateOfLastCalibration_ { get { return Items.FindAll<Date>("00181200").ToList(); } }
        public Time TimeOfLastCalibration { get { return Items.FindFirst<Time>("00181201") as Time; } }
        public List<Time> TimeOfLastCalibration_ { get { return Items.FindAll<Time>("00181201").ToList(); } }
        public ShortString ConvolutionKernel { get { return Items.FindFirst<ShortString>("00181210") as ShortString; } }
        public List<ShortString> ConvolutionKernel_ { get { return Items.FindAll<ShortString>("00181210").ToList(); } }
        public IntegerString UpperLowerPixelValuesRetired { get { return Items.FindFirst<IntegerString>("00181240") as IntegerString; } }
        public List<IntegerString> UpperLowerPixelValuesRetired_ { get { return Items.FindAll<IntegerString>("00181240").ToList(); } }
        public IntegerString ActualFrameDuration { get { return Items.FindFirst<IntegerString>("00181242") as IntegerString; } }
        public List<IntegerString> ActualFrameDuration_ { get { return Items.FindAll<IntegerString>("00181242").ToList(); } }
        public IntegerString CountRate { get { return Items.FindFirst<IntegerString>("00181243") as IntegerString; } }
        public List<IntegerString> CountRate_ { get { return Items.FindAll<IntegerString>("00181243").ToList(); } }
        public UnsignedShort PreferredPlaybackSequencing { get { return Items.FindFirst<UnsignedShort>("00181244") as UnsignedShort; } }
        public List<UnsignedShort> PreferredPlaybackSequencing_ { get { return Items.FindAll<UnsignedShort>("00181244").ToList(); } }
        public ShortString ReceiveCoilName { get { return Items.FindFirst<ShortString>("00181250") as ShortString; } }
        public List<ShortString> ReceiveCoilName_ { get { return Items.FindAll<ShortString>("00181250").ToList(); } }
        public ShortString TransmitCoilName { get { return Items.FindFirst<ShortString>("00181251") as ShortString; } }
        public List<ShortString> TransmitCoilName_ { get { return Items.FindAll<ShortString>("00181251").ToList(); } }
        public ShortString PlateType { get { return Items.FindFirst<ShortString>("00181260") as ShortString; } }
        public List<ShortString> PlateType_ { get { return Items.FindAll<ShortString>("00181260").ToList(); } }
        public LongString PhosphorType { get { return Items.FindFirst<LongString>("00181261") as LongString; } }
        public List<LongString> PhosphorType_ { get { return Items.FindAll<LongString>("00181261").ToList(); } }
        public DecimalString ScanVelocity { get { return Items.FindFirst<DecimalString>("00181300") as DecimalString; } }
        public List<DecimalString> ScanVelocity_ { get { return Items.FindAll<DecimalString>("00181300").ToList(); } }
        public CodeString WholeBodyTechnique { get { return Items.FindFirst<CodeString>("00181301") as CodeString; } }
        public List<CodeString> WholeBodyTechnique_ { get { return Items.FindAll<CodeString>("00181301").ToList(); } }
        public IntegerString ScanLength { get { return Items.FindFirst<IntegerString>("00181302") as IntegerString; } }
        public List<IntegerString> ScanLength_ { get { return Items.FindAll<IntegerString>("00181302").ToList(); } }
        public UnsignedShort AcquisitionMatrix { get { return Items.FindFirst<UnsignedShort>("00181310") as UnsignedShort; } }
        public List<UnsignedShort> AcquisitionMatrix_ { get { return Items.FindAll<UnsignedShort>("00181310").ToList(); } }
        public CodeString InPlanePhaseEncodingDirection { get { return Items.FindFirst<CodeString>("00181312") as CodeString; } }
        public List<CodeString> InPlanePhaseEncodingDirection_ { get { return Items.FindAll<CodeString>("00181312").ToList(); } }
        public DecimalString FlipAngle { get { return Items.FindFirst<DecimalString>("00181314") as DecimalString; } }
        public List<DecimalString> FlipAngle_ { get { return Items.FindAll<DecimalString>("00181314").ToList(); } }
        public CodeString VariableFlipAngleFlag { get { return Items.FindFirst<CodeString>("00181315") as CodeString; } }
        public List<CodeString> VariableFlipAngleFlag_ { get { return Items.FindAll<CodeString>("00181315").ToList(); } }
        public DecimalString SAR { get { return Items.FindFirst<DecimalString>("00181316") as DecimalString; } }
        public List<DecimalString> SAR_ { get { return Items.FindAll<DecimalString>("00181316").ToList(); } }
        public DecimalString dBdt { get { return Items.FindFirst<DecimalString>("00181318") as DecimalString; } }
        public List<DecimalString> dBdt_ { get { return Items.FindAll<DecimalString>("00181318").ToList(); } }
        public LongString AcquisitionDeviceProcessingDescription { get { return Items.FindFirst<LongString>("00181400") as LongString; } }
        public List<LongString> AcquisitionDeviceProcessingDescription_ { get { return Items.FindAll<LongString>("00181400").ToList(); } }
        public LongString AcquisitionDeviceProcessingCode { get { return Items.FindFirst<LongString>("00181401") as LongString; } }
        public List<LongString> AcquisitionDeviceProcessingCode_ { get { return Items.FindAll<LongString>("00181401").ToList(); } }
        public CodeString CassetteOrientation { get { return Items.FindFirst<CodeString>("00181402") as CodeString; } }
        public List<CodeString> CassetteOrientation_ { get { return Items.FindAll<CodeString>("00181402").ToList(); } }
        public CodeString CassetteSize { get { return Items.FindFirst<CodeString>("00181403") as CodeString; } }
        public List<CodeString> CassetteSize_ { get { return Items.FindAll<CodeString>("00181403").ToList(); } }
        public UnsignedShort ExposuresOnPlate { get { return Items.FindFirst<UnsignedShort>("00181404") as UnsignedShort; } }
        public List<UnsignedShort> ExposuresOnPlate_ { get { return Items.FindAll<UnsignedShort>("00181404").ToList(); } }
        public IntegerString RelativeXRayExposure { get { return Items.FindFirst<IntegerString>("00181405") as IntegerString; } }
        public List<IntegerString> RelativeXRayExposure_ { get { return Items.FindAll<IntegerString>("00181405").ToList(); } }
        public DecimalString ExposureIndex { get { return Items.FindFirst<DecimalString>("00181411") as DecimalString; } }
        public List<DecimalString> ExposureIndex_ { get { return Items.FindAll<DecimalString>("00181411").ToList(); } }
        public DecimalString TargetExposureIndex { get { return Items.FindFirst<DecimalString>("00181412") as DecimalString; } }
        public List<DecimalString> TargetExposureIndex_ { get { return Items.FindAll<DecimalString>("00181412").ToList(); } }
        public DecimalString DeviationIndex { get { return Items.FindFirst<DecimalString>("00181413") as DecimalString; } }
        public List<DecimalString> DeviationIndex_ { get { return Items.FindAll<DecimalString>("00181413").ToList(); } }
        public DecimalString ColumnAngulation { get { return Items.FindFirst<DecimalString>("00181450") as DecimalString; } }
        public List<DecimalString> ColumnAngulation_ { get { return Items.FindAll<DecimalString>("00181450").ToList(); } }
        public DecimalString TomoLayerHeight { get { return Items.FindFirst<DecimalString>("00181460") as DecimalString; } }
        public List<DecimalString> TomoLayerHeight_ { get { return Items.FindAll<DecimalString>("00181460").ToList(); } }
        public DecimalString TomoAngle { get { return Items.FindFirst<DecimalString>("00181470") as DecimalString; } }
        public List<DecimalString> TomoAngle_ { get { return Items.FindAll<DecimalString>("00181470").ToList(); } }
        public DecimalString TomoTime { get { return Items.FindFirst<DecimalString>("00181480") as DecimalString; } }
        public List<DecimalString> TomoTime_ { get { return Items.FindAll<DecimalString>("00181480").ToList(); } }
        public CodeString TomoType { get { return Items.FindFirst<CodeString>("00181490") as CodeString; } }
        public List<CodeString> TomoType_ { get { return Items.FindAll<CodeString>("00181490").ToList(); } }
        public CodeString TomoClass { get { return Items.FindFirst<CodeString>("00181491") as CodeString; } }
        public List<CodeString> TomoClass_ { get { return Items.FindAll<CodeString>("00181491").ToList(); } }
        public IntegerString NumberOfTomosynthesisSourceImages { get { return Items.FindFirst<IntegerString>("00181495") as IntegerString; } }
        public List<IntegerString> NumberOfTomosynthesisSourceImages_ { get { return Items.FindAll<IntegerString>("00181495").ToList(); } }
        public CodeString PositionerMotion { get { return Items.FindFirst<CodeString>("00181500") as CodeString; } }
        public List<CodeString> PositionerMotion_ { get { return Items.FindAll<CodeString>("00181500").ToList(); } }
        public CodeString PositionerType { get { return Items.FindFirst<CodeString>("00181508") as CodeString; } }
        public List<CodeString> PositionerType_ { get { return Items.FindAll<CodeString>("00181508").ToList(); } }
        public DecimalString PositionerPrimaryAngle { get { return Items.FindFirst<DecimalString>("00181510") as DecimalString; } }
        public List<DecimalString> PositionerPrimaryAngle_ { get { return Items.FindAll<DecimalString>("00181510").ToList(); } }
        public DecimalString PositionerSecondaryAngle { get { return Items.FindFirst<DecimalString>("00181511") as DecimalString; } }
        public List<DecimalString> PositionerSecondaryAngle_ { get { return Items.FindAll<DecimalString>("00181511").ToList(); } }
        public DecimalString PositionerPrimaryAngleIncrement { get { return Items.FindFirst<DecimalString>("00181520") as DecimalString; } }
        public List<DecimalString> PositionerPrimaryAngleIncrement_ { get { return Items.FindAll<DecimalString>("00181520").ToList(); } }
        public DecimalString PositionerSecondaryAngleIncrement { get { return Items.FindFirst<DecimalString>("00181521") as DecimalString; } }
        public List<DecimalString> PositionerSecondaryAngleIncrement_ { get { return Items.FindAll<DecimalString>("00181521").ToList(); } }
        public DecimalString DetectorPrimaryAngle { get { return Items.FindFirst<DecimalString>("00181530") as DecimalString; } }
        public List<DecimalString> DetectorPrimaryAngle_ { get { return Items.FindAll<DecimalString>("00181530").ToList(); } }
        public DecimalString DetectorSecondaryAngle { get { return Items.FindFirst<DecimalString>("00181531") as DecimalString; } }
        public List<DecimalString> DetectorSecondaryAngle_ { get { return Items.FindAll<DecimalString>("00181531").ToList(); } }
        public CodeString ShutterShape { get { return Items.FindFirst<CodeString>("00181600") as CodeString; } }
        public List<CodeString> ShutterShape_ { get { return Items.FindAll<CodeString>("00181600").ToList(); } }
        public IntegerString ShutterLeftVerticalEdge { get { return Items.FindFirst<IntegerString>("00181602") as IntegerString; } }
        public List<IntegerString> ShutterLeftVerticalEdge_ { get { return Items.FindAll<IntegerString>("00181602").ToList(); } }
        public IntegerString ShutterRightVerticalEdge { get { return Items.FindFirst<IntegerString>("00181604") as IntegerString; } }
        public List<IntegerString> ShutterRightVerticalEdge_ { get { return Items.FindAll<IntegerString>("00181604").ToList(); } }
        public IntegerString ShutterUpperHorizontalEdge { get { return Items.FindFirst<IntegerString>("00181606") as IntegerString; } }
        public List<IntegerString> ShutterUpperHorizontalEdge_ { get { return Items.FindAll<IntegerString>("00181606").ToList(); } }
        public IntegerString ShutterLowerHorizontalEdge { get { return Items.FindFirst<IntegerString>("00181608") as IntegerString; } }
        public List<IntegerString> ShutterLowerHorizontalEdge_ { get { return Items.FindAll<IntegerString>("00181608").ToList(); } }
        public IntegerString CenterOfCircularShutter { get { return Items.FindFirst<IntegerString>("00181610") as IntegerString; } }
        public List<IntegerString> CenterOfCircularShutter_ { get { return Items.FindAll<IntegerString>("00181610").ToList(); } }
        public IntegerString RadiusOfCircularShutter { get { return Items.FindFirst<IntegerString>("00181612") as IntegerString; } }
        public List<IntegerString> RadiusOfCircularShutter_ { get { return Items.FindAll<IntegerString>("00181612").ToList(); } }
        public IntegerString VerticesOfThePolygonalShutter { get { return Items.FindFirst<IntegerString>("00181620") as IntegerString; } }
        public List<IntegerString> VerticesOfThePolygonalShutter_ { get { return Items.FindAll<IntegerString>("00181620").ToList(); } }
        public UnsignedShort ShutterPresentationValue { get { return Items.FindFirst<UnsignedShort>("00181622") as UnsignedShort; } }
        public List<UnsignedShort> ShutterPresentationValue_ { get { return Items.FindAll<UnsignedShort>("00181622").ToList(); } }
        public UnsignedShort ShutterOverlayGroup { get { return Items.FindFirst<UnsignedShort>("00181623") as UnsignedShort; } }
        public List<UnsignedShort> ShutterOverlayGroup_ { get { return Items.FindAll<UnsignedShort>("00181623").ToList(); } }
        public UnsignedShort ShutterPresentationColorCIELabValue { get { return Items.FindFirst<UnsignedShort>("00181624") as UnsignedShort; } }
        public List<UnsignedShort> ShutterPresentationColorCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00181624").ToList(); } }
        public CodeString CollimatorShape { get { return Items.FindFirst<CodeString>("00181700") as CodeString; } }
        public List<CodeString> CollimatorShape_ { get { return Items.FindAll<CodeString>("00181700").ToList(); } }
        public IntegerString CollimatorLeftVerticalEdge { get { return Items.FindFirst<IntegerString>("00181702") as IntegerString; } }
        public List<IntegerString> CollimatorLeftVerticalEdge_ { get { return Items.FindAll<IntegerString>("00181702").ToList(); } }
        public IntegerString CollimatorRightVerticalEdge { get { return Items.FindFirst<IntegerString>("00181704") as IntegerString; } }
        public List<IntegerString> CollimatorRightVerticalEdge_ { get { return Items.FindAll<IntegerString>("00181704").ToList(); } }
        public IntegerString CollimatorUpperHorizontalEdge { get { return Items.FindFirst<IntegerString>("00181706") as IntegerString; } }
        public List<IntegerString> CollimatorUpperHorizontalEdge_ { get { return Items.FindAll<IntegerString>("00181706").ToList(); } }
        public IntegerString CollimatorLowerHorizontalEdge { get { return Items.FindFirst<IntegerString>("00181708") as IntegerString; } }
        public List<IntegerString> CollimatorLowerHorizontalEdge_ { get { return Items.FindAll<IntegerString>("00181708").ToList(); } }
        public IntegerString CenterOfCircularCollimator { get { return Items.FindFirst<IntegerString>("00181710") as IntegerString; } }
        public List<IntegerString> CenterOfCircularCollimator_ { get { return Items.FindAll<IntegerString>("00181710").ToList(); } }
        public IntegerString RadiusOfCircularCollimator { get { return Items.FindFirst<IntegerString>("00181712") as IntegerString; } }
        public List<IntegerString> RadiusOfCircularCollimator_ { get { return Items.FindAll<IntegerString>("00181712").ToList(); } }
        public IntegerString VerticesOfThePolygonalCollimator { get { return Items.FindFirst<IntegerString>("00181720") as IntegerString; } }
        public List<IntegerString> VerticesOfThePolygonalCollimator_ { get { return Items.FindAll<IntegerString>("00181720").ToList(); } }
        public CodeString AcquisitionTimeSynchronized { get { return Items.FindFirst<CodeString>("00181800") as CodeString; } }
        public List<CodeString> AcquisitionTimeSynchronized_ { get { return Items.FindAll<CodeString>("00181800").ToList(); } }
        public ShortString TimeSource { get { return Items.FindFirst<ShortString>("00181801") as ShortString; } }
        public List<ShortString> TimeSource_ { get { return Items.FindAll<ShortString>("00181801").ToList(); } }
        public CodeString TimeDistributionProtocol { get { return Items.FindFirst<CodeString>("00181802") as CodeString; } }
        public List<CodeString> TimeDistributionProtocol_ { get { return Items.FindAll<CodeString>("00181802").ToList(); } }
        public LongString NTPSourceAddress { get { return Items.FindFirst<LongString>("00181803") as LongString; } }
        public List<LongString> NTPSourceAddress_ { get { return Items.FindAll<LongString>("00181803").ToList(); } }
        public IntegerString PageNumberVector { get { return Items.FindFirst<IntegerString>("00182001") as IntegerString; } }
        public List<IntegerString> PageNumberVector_ { get { return Items.FindAll<IntegerString>("00182001").ToList(); } }
        public ShortString FrameLabelVector { get { return Items.FindFirst<ShortString>("00182002") as ShortString; } }
        public List<ShortString> FrameLabelVector_ { get { return Items.FindAll<ShortString>("00182002").ToList(); } }
        public DecimalString FramePrimaryAngleVector { get { return Items.FindFirst<DecimalString>("00182003") as DecimalString; } }
        public List<DecimalString> FramePrimaryAngleVector_ { get { return Items.FindAll<DecimalString>("00182003").ToList(); } }
        public DecimalString FrameSecondaryAngleVector { get { return Items.FindFirst<DecimalString>("00182004") as DecimalString; } }
        public List<DecimalString> FrameSecondaryAngleVector_ { get { return Items.FindAll<DecimalString>("00182004").ToList(); } }
        public DecimalString SliceLocationVector { get { return Items.FindFirst<DecimalString>("00182005") as DecimalString; } }
        public List<DecimalString> SliceLocationVector_ { get { return Items.FindAll<DecimalString>("00182005").ToList(); } }
        public ShortString DisplayWindowLabelVector { get { return Items.FindFirst<ShortString>("00182006") as ShortString; } }
        public List<ShortString> DisplayWindowLabelVector_ { get { return Items.FindAll<ShortString>("00182006").ToList(); } }
        public DecimalString NominalScannedPixelSpacing { get { return Items.FindFirst<DecimalString>("00182010") as DecimalString; } }
        public List<DecimalString> NominalScannedPixelSpacing_ { get { return Items.FindAll<DecimalString>("00182010").ToList(); } }
        public CodeString DigitizingDeviceTransportDirection { get { return Items.FindFirst<CodeString>("00182020") as CodeString; } }
        public List<CodeString> DigitizingDeviceTransportDirection_ { get { return Items.FindAll<CodeString>("00182020").ToList(); } }
        public DecimalString RotationOfScannedFilm { get { return Items.FindFirst<DecimalString>("00182030") as DecimalString; } }
        public List<DecimalString> RotationOfScannedFilm_ { get { return Items.FindAll<DecimalString>("00182030").ToList(); } }
        public CodeString IVUSAcquisition { get { return Items.FindFirst<CodeString>("00183100") as CodeString; } }
        public List<CodeString> IVUSAcquisition_ { get { return Items.FindAll<CodeString>("00183100").ToList(); } }
        public DecimalString IVUSPullbackRate { get { return Items.FindFirst<DecimalString>("00183101") as DecimalString; } }
        public List<DecimalString> IVUSPullbackRate_ { get { return Items.FindAll<DecimalString>("00183101").ToList(); } }
        public DecimalString IVUSGatedRate { get { return Items.FindFirst<DecimalString>("00183102") as DecimalString; } }
        public List<DecimalString> IVUSGatedRate_ { get { return Items.FindAll<DecimalString>("00183102").ToList(); } }
        public IntegerString IVUSPullbackStartFrameNumber { get { return Items.FindFirst<IntegerString>("00183103") as IntegerString; } }
        public List<IntegerString> IVUSPullbackStartFrameNumber_ { get { return Items.FindAll<IntegerString>("00183103").ToList(); } }
        public IntegerString IVUSPullbackStopFrameNumber { get { return Items.FindFirst<IntegerString>("00183104") as IntegerString; } }
        public List<IntegerString> IVUSPullbackStopFrameNumber_ { get { return Items.FindAll<IntegerString>("00183104").ToList(); } }
        public IntegerString LesionNumber { get { return Items.FindFirst<IntegerString>("00183105") as IntegerString; } }
        public List<IntegerString> LesionNumber_ { get { return Items.FindAll<IntegerString>("00183105").ToList(); } }
        public LongText AcquisitionCommentsRetired { get { return Items.FindFirst<LongText>("00184000") as LongText; } }
        public List<LongText> AcquisitionCommentsRetired_ { get { return Items.FindAll<LongText>("00184000").ToList(); } }
        public ShortString OutputPower { get { return Items.FindFirst<ShortString>("00185000") as ShortString; } }
        public List<ShortString> OutputPower_ { get { return Items.FindAll<ShortString>("00185000").ToList(); } }
        public LongString TransducerData { get { return Items.FindFirst<LongString>("00185010") as LongString; } }
        public List<LongString> TransducerData_ { get { return Items.FindAll<LongString>("00185010").ToList(); } }
        public DecimalString FocusDepth { get { return Items.FindFirst<DecimalString>("00185012") as DecimalString; } }
        public List<DecimalString> FocusDepth_ { get { return Items.FindAll<DecimalString>("00185012").ToList(); } }
        public LongString ProcessingFunction { get { return Items.FindFirst<LongString>("00185020") as LongString; } }
        public List<LongString> ProcessingFunction_ { get { return Items.FindAll<LongString>("00185020").ToList(); } }
        public LongString PostprocessingFunctionRetired { get { return Items.FindFirst<LongString>("00185021") as LongString; } }
        public List<LongString> PostprocessingFunctionRetired_ { get { return Items.FindAll<LongString>("00185021").ToList(); } }
        public DecimalString MechanicalIndex { get { return Items.FindFirst<DecimalString>("00185022") as DecimalString; } }
        public List<DecimalString> MechanicalIndex_ { get { return Items.FindAll<DecimalString>("00185022").ToList(); } }
        public DecimalString BoneThermalIndex { get { return Items.FindFirst<DecimalString>("00185024") as DecimalString; } }
        public List<DecimalString> BoneThermalIndex_ { get { return Items.FindAll<DecimalString>("00185024").ToList(); } }
        public DecimalString CranialThermalIndex { get { return Items.FindFirst<DecimalString>("00185026") as DecimalString; } }
        public List<DecimalString> CranialThermalIndex_ { get { return Items.FindAll<DecimalString>("00185026").ToList(); } }
        public DecimalString SoftTissueThermalIndex { get { return Items.FindFirst<DecimalString>("00185027") as DecimalString; } }
        public List<DecimalString> SoftTissueThermalIndex_ { get { return Items.FindAll<DecimalString>("00185027").ToList(); } }
        public DecimalString SoftTissueFocusThermalIndex { get { return Items.FindFirst<DecimalString>("00185028") as DecimalString; } }
        public List<DecimalString> SoftTissueFocusThermalIndex_ { get { return Items.FindAll<DecimalString>("00185028").ToList(); } }
        public DecimalString SoftTissueSurfaceThermalIndex { get { return Items.FindFirst<DecimalString>("00185029") as DecimalString; } }
        public List<DecimalString> SoftTissueSurfaceThermalIndex_ { get { return Items.FindAll<DecimalString>("00185029").ToList(); } }
        public DecimalString DynamicRangeRetired { get { return Items.FindFirst<DecimalString>("00185030") as DecimalString; } }
        public List<DecimalString> DynamicRangeRetired_ { get { return Items.FindAll<DecimalString>("00185030").ToList(); } }
        public DecimalString TotalGainRetired { get { return Items.FindFirst<DecimalString>("00185040") as DecimalString; } }
        public List<DecimalString> TotalGainRetired_ { get { return Items.FindAll<DecimalString>("00185040").ToList(); } }
        public IntegerString DepthOfScanField { get { return Items.FindFirst<IntegerString>("00185050") as IntegerString; } }
        public List<IntegerString> DepthOfScanField_ { get { return Items.FindAll<IntegerString>("00185050").ToList(); } }
        public CodeString PatientPosition { get { return Items.FindFirst<CodeString>("00185100") as CodeString; } }
        public List<CodeString> PatientPosition_ { get { return Items.FindAll<CodeString>("00185100").ToList(); } }
        public CodeString ViewPosition { get { return Items.FindFirst<CodeString>("00185101") as CodeString; } }
        public List<CodeString> ViewPosition_ { get { return Items.FindAll<CodeString>("00185101").ToList(); } }
        public SequenceSelector ProjectionEponymousNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00185104")); } }
        public List<SequenceSelector> ProjectionEponymousNameCodeSequence_ { get { return Items.FindAll<Sequence>("00185104").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString ImageTransformationMatrixRetired { get { return Items.FindFirst<DecimalString>("00185210") as DecimalString; } }
        public List<DecimalString> ImageTransformationMatrixRetired_ { get { return Items.FindAll<DecimalString>("00185210").ToList(); } }
        public DecimalString ImageTranslationVectorRetired { get { return Items.FindFirst<DecimalString>("00185212") as DecimalString; } }
        public List<DecimalString> ImageTranslationVectorRetired_ { get { return Items.FindAll<DecimalString>("00185212").ToList(); } }
        public DecimalString Sensitivity { get { return Items.FindFirst<DecimalString>("00186000") as DecimalString; } }
        public List<DecimalString> Sensitivity_ { get { return Items.FindAll<DecimalString>("00186000").ToList(); } }
        public SequenceSelector SequenceOfUltrasoundRegions { get { return new SequenceSelector(Items.FindFirst<Sequence>("00186011")); } }
        public List<SequenceSelector> SequenceOfUltrasoundRegions_ { get { return Items.FindAll<Sequence>("00186011").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort RegionSpatialFormat { get { return Items.FindFirst<UnsignedShort>("00186012") as UnsignedShort; } }
        public List<UnsignedShort> RegionSpatialFormat_ { get { return Items.FindAll<UnsignedShort>("00186012").ToList(); } }
        public UnsignedShort RegionDataType { get { return Items.FindFirst<UnsignedShort>("00186014") as UnsignedShort; } }
        public List<UnsignedShort> RegionDataType_ { get { return Items.FindAll<UnsignedShort>("00186014").ToList(); } }
        public UnsignedLong RegionFlags { get { return Items.FindFirst<UnsignedLong>("00186016") as UnsignedLong; } }
        public List<UnsignedLong> RegionFlags_ { get { return Items.FindAll<UnsignedLong>("00186016").ToList(); } }
        public UnsignedLong RegionLocationMinX0 { get { return Items.FindFirst<UnsignedLong>("00186018") as UnsignedLong; } }
        public List<UnsignedLong> RegionLocationMinX0_ { get { return Items.FindAll<UnsignedLong>("00186018").ToList(); } }
        public UnsignedLong RegionLocationMinY0 { get { return Items.FindFirst<UnsignedLong>("0018601A") as UnsignedLong; } }
        public List<UnsignedLong> RegionLocationMinY0_ { get { return Items.FindAll<UnsignedLong>("0018601A").ToList(); } }
        public UnsignedLong RegionLocationMaxX1 { get { return Items.FindFirst<UnsignedLong>("0018601C") as UnsignedLong; } }
        public List<UnsignedLong> RegionLocationMaxX1_ { get { return Items.FindAll<UnsignedLong>("0018601C").ToList(); } }
        public UnsignedLong RegionLocationMaxY1 { get { return Items.FindFirst<UnsignedLong>("0018601E") as UnsignedLong; } }
        public List<UnsignedLong> RegionLocationMaxY1_ { get { return Items.FindAll<UnsignedLong>("0018601E").ToList(); } }
        public SignedLong ReferencePixelX0 { get { return Items.FindFirst<SignedLong>("00186020") as SignedLong; } }
        public List<SignedLong> ReferencePixelX0_ { get { return Items.FindAll<SignedLong>("00186020").ToList(); } }
        public SignedLong ReferencePixelY0 { get { return Items.FindFirst<SignedLong>("00186022") as SignedLong; } }
        public List<SignedLong> ReferencePixelY0_ { get { return Items.FindAll<SignedLong>("00186022").ToList(); } }
        public UnsignedShort PhysicalUnitsXDirection { get { return Items.FindFirst<UnsignedShort>("00186024") as UnsignedShort; } }
        public List<UnsignedShort> PhysicalUnitsXDirection_ { get { return Items.FindAll<UnsignedShort>("00186024").ToList(); } }
        public UnsignedShort PhysicalUnitsYDirection { get { return Items.FindFirst<UnsignedShort>("00186026") as UnsignedShort; } }
        public List<UnsignedShort> PhysicalUnitsYDirection_ { get { return Items.FindAll<UnsignedShort>("00186026").ToList(); } }
        public FloatingPointDouble ReferencePixelPhysicalValueX { get { return Items.FindFirst<FloatingPointDouble>("00186028") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReferencePixelPhysicalValueX_ { get { return Items.FindAll<FloatingPointDouble>("00186028").ToList(); } }
        public FloatingPointDouble ReferencePixelPhysicalValueY { get { return Items.FindFirst<FloatingPointDouble>("0018602A") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReferencePixelPhysicalValueY_ { get { return Items.FindAll<FloatingPointDouble>("0018602A").ToList(); } }
        public FloatingPointDouble PhysicalDeltaX { get { return Items.FindFirst<FloatingPointDouble>("0018602C") as FloatingPointDouble; } }
        public List<FloatingPointDouble> PhysicalDeltaX_ { get { return Items.FindAll<FloatingPointDouble>("0018602C").ToList(); } }
        public FloatingPointDouble PhysicalDeltaY { get { return Items.FindFirst<FloatingPointDouble>("0018602E") as FloatingPointDouble; } }
        public List<FloatingPointDouble> PhysicalDeltaY_ { get { return Items.FindAll<FloatingPointDouble>("0018602E").ToList(); } }
        public UnsignedLong TransducerFrequency { get { return Items.FindFirst<UnsignedLong>("00186030") as UnsignedLong; } }
        public List<UnsignedLong> TransducerFrequency_ { get { return Items.FindAll<UnsignedLong>("00186030").ToList(); } }
        public CodeString TransducerType { get { return Items.FindFirst<CodeString>("00186031") as CodeString; } }
        public List<CodeString> TransducerType_ { get { return Items.FindAll<CodeString>("00186031").ToList(); } }
        public UnsignedLong PulseRepetitionFrequency { get { return Items.FindFirst<UnsignedLong>("00186032") as UnsignedLong; } }
        public List<UnsignedLong> PulseRepetitionFrequency_ { get { return Items.FindAll<UnsignedLong>("00186032").ToList(); } }
        public FloatingPointDouble DopplerCorrectionAngle { get { return Items.FindFirst<FloatingPointDouble>("00186034") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DopplerCorrectionAngle_ { get { return Items.FindAll<FloatingPointDouble>("00186034").ToList(); } }
        public FloatingPointDouble SteeringAngle { get { return Items.FindFirst<FloatingPointDouble>("00186036") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SteeringAngle_ { get { return Items.FindAll<FloatingPointDouble>("00186036").ToList(); } }
        public UnsignedLong DopplerSampleVolumeXPositionRetired { get { return Items.FindFirst<UnsignedLong>("00186038") as UnsignedLong; } }
        public List<UnsignedLong> DopplerSampleVolumeXPositionRetired_ { get { return Items.FindAll<UnsignedLong>("00186038").ToList(); } }
        public SignedLong DopplerSampleVolumeXPosition { get { return Items.FindFirst<SignedLong>("00186039") as SignedLong; } }
        public List<SignedLong> DopplerSampleVolumeXPosition_ { get { return Items.FindAll<SignedLong>("00186039").ToList(); } }
        public UnsignedLong DopplerSampleVolumeYPositionRetired { get { return Items.FindFirst<UnsignedLong>("0018603A") as UnsignedLong; } }
        public List<UnsignedLong> DopplerSampleVolumeYPositionRetired_ { get { return Items.FindAll<UnsignedLong>("0018603A").ToList(); } }
        public SignedLong DopplerSampleVolumeYPosition { get { return Items.FindFirst<SignedLong>("0018603B") as SignedLong; } }
        public List<SignedLong> DopplerSampleVolumeYPosition_ { get { return Items.FindAll<SignedLong>("0018603B").ToList(); } }
        public UnsignedLong TMLinePositionX0Retired { get { return Items.FindFirst<UnsignedLong>("0018603C") as UnsignedLong; } }
        public List<UnsignedLong> TMLinePositionX0Retired_ { get { return Items.FindAll<UnsignedLong>("0018603C").ToList(); } }
        public SignedLong TMLinePositionX0 { get { return Items.FindFirst<SignedLong>("0018603D") as SignedLong; } }
        public List<SignedLong> TMLinePositionX0_ { get { return Items.FindAll<SignedLong>("0018603D").ToList(); } }
        public UnsignedLong TMLinePositionY0Retired { get { return Items.FindFirst<UnsignedLong>("0018603E") as UnsignedLong; } }
        public List<UnsignedLong> TMLinePositionY0Retired_ { get { return Items.FindAll<UnsignedLong>("0018603E").ToList(); } }
        public SignedLong TMLinePositionY0 { get { return Items.FindFirst<SignedLong>("0018603F") as SignedLong; } }
        public List<SignedLong> TMLinePositionY0_ { get { return Items.FindAll<SignedLong>("0018603F").ToList(); } }
        public UnsignedLong TMLinePositionX1Retired { get { return Items.FindFirst<UnsignedLong>("00186040") as UnsignedLong; } }
        public List<UnsignedLong> TMLinePositionX1Retired_ { get { return Items.FindAll<UnsignedLong>("00186040").ToList(); } }
        public SignedLong TMLinePositionX1 { get { return Items.FindFirst<SignedLong>("00186041") as SignedLong; } }
        public List<SignedLong> TMLinePositionX1_ { get { return Items.FindAll<SignedLong>("00186041").ToList(); } }
        public UnsignedLong TMLinePositionY1Retired { get { return Items.FindFirst<UnsignedLong>("00186042") as UnsignedLong; } }
        public List<UnsignedLong> TMLinePositionY1Retired_ { get { return Items.FindAll<UnsignedLong>("00186042").ToList(); } }
        public SignedLong TMLinePositionY1 { get { return Items.FindFirst<SignedLong>("00186043") as SignedLong; } }
        public List<SignedLong> TMLinePositionY1_ { get { return Items.FindAll<SignedLong>("00186043").ToList(); } }
        public UnsignedShort PixelComponentOrganization { get { return Items.FindFirst<UnsignedShort>("00186044") as UnsignedShort; } }
        public List<UnsignedShort> PixelComponentOrganization_ { get { return Items.FindAll<UnsignedShort>("00186044").ToList(); } }
        public UnsignedLong PixelComponentMask { get { return Items.FindFirst<UnsignedLong>("00186046") as UnsignedLong; } }
        public List<UnsignedLong> PixelComponentMask_ { get { return Items.FindAll<UnsignedLong>("00186046").ToList(); } }
        public UnsignedLong PixelComponentRangeStart { get { return Items.FindFirst<UnsignedLong>("00186048") as UnsignedLong; } }
        public List<UnsignedLong> PixelComponentRangeStart_ { get { return Items.FindAll<UnsignedLong>("00186048").ToList(); } }
        public UnsignedLong PixelComponentRangeStop { get { return Items.FindFirst<UnsignedLong>("0018604A") as UnsignedLong; } }
        public List<UnsignedLong> PixelComponentRangeStop_ { get { return Items.FindAll<UnsignedLong>("0018604A").ToList(); } }
        public UnsignedShort PixelComponentPhysicalUnits { get { return Items.FindFirst<UnsignedShort>("0018604C") as UnsignedShort; } }
        public List<UnsignedShort> PixelComponentPhysicalUnits_ { get { return Items.FindAll<UnsignedShort>("0018604C").ToList(); } }
        public UnsignedShort PixelComponentDataType { get { return Items.FindFirst<UnsignedShort>("0018604E") as UnsignedShort; } }
        public List<UnsignedShort> PixelComponentDataType_ { get { return Items.FindAll<UnsignedShort>("0018604E").ToList(); } }
        public UnsignedLong NumberOfTableBreakPoints { get { return Items.FindFirst<UnsignedLong>("00186050") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfTableBreakPoints_ { get { return Items.FindAll<UnsignedLong>("00186050").ToList(); } }
        public UnsignedLong TableOfXBreakPoints { get { return Items.FindFirst<UnsignedLong>("00186052") as UnsignedLong; } }
        public List<UnsignedLong> TableOfXBreakPoints_ { get { return Items.FindAll<UnsignedLong>("00186052").ToList(); } }
        public FloatingPointDouble TableOfYBreakPoints { get { return Items.FindFirst<FloatingPointDouble>("00186054") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableOfYBreakPoints_ { get { return Items.FindAll<FloatingPointDouble>("00186054").ToList(); } }
        public UnsignedLong NumberOfTableEntries { get { return Items.FindFirst<UnsignedLong>("00186056") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfTableEntries_ { get { return Items.FindAll<UnsignedLong>("00186056").ToList(); } }
        public UnsignedLong TableOfPixelValues { get { return Items.FindFirst<UnsignedLong>("00186058") as UnsignedLong; } }
        public List<UnsignedLong> TableOfPixelValues_ { get { return Items.FindAll<UnsignedLong>("00186058").ToList(); } }
        public FloatingPointSingle TableOfParameterValues { get { return Items.FindFirst<FloatingPointSingle>("0018605A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableOfParameterValues_ { get { return Items.FindAll<FloatingPointSingle>("0018605A").ToList(); } }
        public FloatingPointSingle RWaveTimeVector { get { return Items.FindFirst<FloatingPointSingle>("00186060") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RWaveTimeVector_ { get { return Items.FindAll<FloatingPointSingle>("00186060").ToList(); } }
        public CodeString DetectorConditionsNominalFlag { get { return Items.FindFirst<CodeString>("00187000") as CodeString; } }
        public List<CodeString> DetectorConditionsNominalFlag_ { get { return Items.FindAll<CodeString>("00187000").ToList(); } }
        public DecimalString DetectorTemperature { get { return Items.FindFirst<DecimalString>("00187001") as DecimalString; } }
        public List<DecimalString> DetectorTemperature_ { get { return Items.FindAll<DecimalString>("00187001").ToList(); } }
        public CodeString DetectorType { get { return Items.FindFirst<CodeString>("00187004") as CodeString; } }
        public List<CodeString> DetectorType_ { get { return Items.FindAll<CodeString>("00187004").ToList(); } }
        public CodeString DetectorConfiguration { get { return Items.FindFirst<CodeString>("00187005") as CodeString; } }
        public List<CodeString> DetectorConfiguration_ { get { return Items.FindAll<CodeString>("00187005").ToList(); } }
        public LongText DetectorDescription { get { return Items.FindFirst<LongText>("00187006") as LongText; } }
        public List<LongText> DetectorDescription_ { get { return Items.FindAll<LongText>("00187006").ToList(); } }
        public LongText DetectorMode { get { return Items.FindFirst<LongText>("00187008") as LongText; } }
        public List<LongText> DetectorMode_ { get { return Items.FindAll<LongText>("00187008").ToList(); } }
        public ShortString DetectorID { get { return Items.FindFirst<ShortString>("0018700A") as ShortString; } }
        public List<ShortString> DetectorID_ { get { return Items.FindAll<ShortString>("0018700A").ToList(); } }
        public Date DateOfLastDetectorCalibration { get { return Items.FindFirst<Date>("0018700C") as Date; } }
        public List<Date> DateOfLastDetectorCalibration_ { get { return Items.FindAll<Date>("0018700C").ToList(); } }
        public Time TimeOfLastDetectorCalibration { get { return Items.FindFirst<Time>("0018700E") as Time; } }
        public List<Time> TimeOfLastDetectorCalibration_ { get { return Items.FindAll<Time>("0018700E").ToList(); } }
        public IntegerString ExposuresOnDetectorSinceLastCalibration { get { return Items.FindFirst<IntegerString>("00187010") as IntegerString; } }
        public List<IntegerString> ExposuresOnDetectorSinceLastCalibration_ { get { return Items.FindAll<IntegerString>("00187010").ToList(); } }
        public IntegerString ExposuresOnDetectorSinceManufactured { get { return Items.FindFirst<IntegerString>("00187011") as IntegerString; } }
        public List<IntegerString> ExposuresOnDetectorSinceManufactured_ { get { return Items.FindAll<IntegerString>("00187011").ToList(); } }
        public DecimalString DetectorTimeSinceLastExposure { get { return Items.FindFirst<DecimalString>("00187012") as DecimalString; } }
        public List<DecimalString> DetectorTimeSinceLastExposure_ { get { return Items.FindAll<DecimalString>("00187012").ToList(); } }
        public DecimalString DetectorActiveTime { get { return Items.FindFirst<DecimalString>("00187014") as DecimalString; } }
        public List<DecimalString> DetectorActiveTime_ { get { return Items.FindAll<DecimalString>("00187014").ToList(); } }
        public DecimalString DetectorActivationOffsetFromExposure { get { return Items.FindFirst<DecimalString>("00187016") as DecimalString; } }
        public List<DecimalString> DetectorActivationOffsetFromExposure_ { get { return Items.FindAll<DecimalString>("00187016").ToList(); } }
        public DecimalString DetectorBinning { get { return Items.FindFirst<DecimalString>("0018701A") as DecimalString; } }
        public List<DecimalString> DetectorBinning_ { get { return Items.FindAll<DecimalString>("0018701A").ToList(); } }
        public DecimalString DetectorElementPhysicalSize { get { return Items.FindFirst<DecimalString>("00187020") as DecimalString; } }
        public List<DecimalString> DetectorElementPhysicalSize_ { get { return Items.FindAll<DecimalString>("00187020").ToList(); } }
        public DecimalString DetectorElementSpacing { get { return Items.FindFirst<DecimalString>("00187022") as DecimalString; } }
        public List<DecimalString> DetectorElementSpacing_ { get { return Items.FindAll<DecimalString>("00187022").ToList(); } }
        public CodeString DetectorActiveShape { get { return Items.FindFirst<CodeString>("00187024") as CodeString; } }
        public List<CodeString> DetectorActiveShape_ { get { return Items.FindAll<CodeString>("00187024").ToList(); } }
        public DecimalString DetectorActiveDimensions { get { return Items.FindFirst<DecimalString>("00187026") as DecimalString; } }
        public List<DecimalString> DetectorActiveDimensions_ { get { return Items.FindAll<DecimalString>("00187026").ToList(); } }
        public DecimalString DetectorActiveOrigin { get { return Items.FindFirst<DecimalString>("00187028") as DecimalString; } }
        public List<DecimalString> DetectorActiveOrigin_ { get { return Items.FindAll<DecimalString>("00187028").ToList(); } }
        public LongString DetectorManufacturerName { get { return Items.FindFirst<LongString>("0018702A") as LongString; } }
        public List<LongString> DetectorManufacturerName_ { get { return Items.FindAll<LongString>("0018702A").ToList(); } }
        public LongString DetectorManufacturerModelName { get { return Items.FindFirst<LongString>("0018702B") as LongString; } }
        public List<LongString> DetectorManufacturerModelName_ { get { return Items.FindAll<LongString>("0018702B").ToList(); } }
        public DecimalString FieldOfViewOrigin { get { return Items.FindFirst<DecimalString>("00187030") as DecimalString; } }
        public List<DecimalString> FieldOfViewOrigin_ { get { return Items.FindAll<DecimalString>("00187030").ToList(); } }
        public DecimalString FieldOfViewRotation { get { return Items.FindFirst<DecimalString>("00187032") as DecimalString; } }
        public List<DecimalString> FieldOfViewRotation_ { get { return Items.FindAll<DecimalString>("00187032").ToList(); } }
        public CodeString FieldOfViewHorizontalFlip { get { return Items.FindFirst<CodeString>("00187034") as CodeString; } }
        public List<CodeString> FieldOfViewHorizontalFlip_ { get { return Items.FindAll<CodeString>("00187034").ToList(); } }
        public FloatingPointSingle PixelDataAreaOriginRelativeToFOV { get { return Items.FindFirst<FloatingPointSingle>("00187036") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PixelDataAreaOriginRelativeToFOV_ { get { return Items.FindAll<FloatingPointSingle>("00187036").ToList(); } }
        public FloatingPointSingle PixelDataAreaRotationAngleRelativeToFOV { get { return Items.FindFirst<FloatingPointSingle>("00187038") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PixelDataAreaRotationAngleRelativeToFOV_ { get { return Items.FindAll<FloatingPointSingle>("00187038").ToList(); } }
        public LongText GridAbsorbingMaterial { get { return Items.FindFirst<LongText>("00187040") as LongText; } }
        public List<LongText> GridAbsorbingMaterial_ { get { return Items.FindAll<LongText>("00187040").ToList(); } }
        public LongText GridSpacingMaterial { get { return Items.FindFirst<LongText>("00187041") as LongText; } }
        public List<LongText> GridSpacingMaterial_ { get { return Items.FindAll<LongText>("00187041").ToList(); } }
        public DecimalString GridThickness { get { return Items.FindFirst<DecimalString>("00187042") as DecimalString; } }
        public List<DecimalString> GridThickness_ { get { return Items.FindAll<DecimalString>("00187042").ToList(); } }
        public DecimalString GridPitch { get { return Items.FindFirst<DecimalString>("00187044") as DecimalString; } }
        public List<DecimalString> GridPitch_ { get { return Items.FindAll<DecimalString>("00187044").ToList(); } }
        public IntegerString GridAspectRatio { get { return Items.FindFirst<IntegerString>("00187046") as IntegerString; } }
        public List<IntegerString> GridAspectRatio_ { get { return Items.FindAll<IntegerString>("00187046").ToList(); } }
        public DecimalString GridPeriod { get { return Items.FindFirst<DecimalString>("00187048") as DecimalString; } }
        public List<DecimalString> GridPeriod_ { get { return Items.FindAll<DecimalString>("00187048").ToList(); } }
        public DecimalString GridFocalDistance { get { return Items.FindFirst<DecimalString>("0018704C") as DecimalString; } }
        public List<DecimalString> GridFocalDistance_ { get { return Items.FindAll<DecimalString>("0018704C").ToList(); } }
        public CodeString FilterMaterial { get { return Items.FindFirst<CodeString>("00187050") as CodeString; } }
        public List<CodeString> FilterMaterial_ { get { return Items.FindAll<CodeString>("00187050").ToList(); } }
        public DecimalString FilterThicknessMinimum { get { return Items.FindFirst<DecimalString>("00187052") as DecimalString; } }
        public List<DecimalString> FilterThicknessMinimum_ { get { return Items.FindAll<DecimalString>("00187052").ToList(); } }
        public DecimalString FilterThicknessMaximum { get { return Items.FindFirst<DecimalString>("00187054") as DecimalString; } }
        public List<DecimalString> FilterThicknessMaximum_ { get { return Items.FindAll<DecimalString>("00187054").ToList(); } }
        public FloatingPointSingle FilterBeamPathLengthMinimum { get { return Items.FindFirst<FloatingPointSingle>("00187056") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FilterBeamPathLengthMinimum_ { get { return Items.FindAll<FloatingPointSingle>("00187056").ToList(); } }
        public FloatingPointSingle FilterBeamPathLengthMaximum { get { return Items.FindFirst<FloatingPointSingle>("00187058") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FilterBeamPathLengthMaximum_ { get { return Items.FindAll<FloatingPointSingle>("00187058").ToList(); } }
        public CodeString ExposureControlMode { get { return Items.FindFirst<CodeString>("00187060") as CodeString; } }
        public List<CodeString> ExposureControlMode_ { get { return Items.FindAll<CodeString>("00187060").ToList(); } }
        public LongText ExposureControlModeDescription { get { return Items.FindFirst<LongText>("00187062") as LongText; } }
        public List<LongText> ExposureControlModeDescription_ { get { return Items.FindAll<LongText>("00187062").ToList(); } }
        public CodeString ExposureStatus { get { return Items.FindFirst<CodeString>("00187064") as CodeString; } }
        public List<CodeString> ExposureStatus_ { get { return Items.FindAll<CodeString>("00187064").ToList(); } }
        public DecimalString PhototimerSetting { get { return Items.FindFirst<DecimalString>("00187065") as DecimalString; } }
        public List<DecimalString> PhototimerSetting_ { get { return Items.FindAll<DecimalString>("00187065").ToList(); } }
        public DecimalString ExposureTimeInuS { get { return Items.FindFirst<DecimalString>("00188150") as DecimalString; } }
        public List<DecimalString> ExposureTimeInuS_ { get { return Items.FindAll<DecimalString>("00188150").ToList(); } }
        public DecimalString XRayTubeCurrentInuA { get { return Items.FindFirst<DecimalString>("00188151") as DecimalString; } }
        public List<DecimalString> XRayTubeCurrentInuA_ { get { return Items.FindAll<DecimalString>("00188151").ToList(); } }
        public CodeString ContentQualification { get { return Items.FindFirst<CodeString>("00189004") as CodeString; } }
        public List<CodeString> ContentQualification_ { get { return Items.FindAll<CodeString>("00189004").ToList(); } }
        public ShortString PulseSequenceName { get { return Items.FindFirst<ShortString>("00189005") as ShortString; } }
        public List<ShortString> PulseSequenceName_ { get { return Items.FindAll<ShortString>("00189005").ToList(); } }
        public SequenceSelector MRImagingModifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189006")); } }
        public List<SequenceSelector> MRImagingModifierSequence_ { get { return Items.FindAll<Sequence>("00189006").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString EchoPulseSequence { get { return Items.FindFirst<CodeString>("00189008") as CodeString; } }
        public List<CodeString> EchoPulseSequence_ { get { return Items.FindAll<CodeString>("00189008").ToList(); } }
        public CodeString InversionRecovery { get { return Items.FindFirst<CodeString>("00189009") as CodeString; } }
        public List<CodeString> InversionRecovery_ { get { return Items.FindAll<CodeString>("00189009").ToList(); } }
        public CodeString FlowCompensation { get { return Items.FindFirst<CodeString>("00189010") as CodeString; } }
        public List<CodeString> FlowCompensation_ { get { return Items.FindAll<CodeString>("00189010").ToList(); } }
        public CodeString MultipleSpinEcho { get { return Items.FindFirst<CodeString>("00189011") as CodeString; } }
        public List<CodeString> MultipleSpinEcho_ { get { return Items.FindAll<CodeString>("00189011").ToList(); } }
        public CodeString MultiPlanarExcitation { get { return Items.FindFirst<CodeString>("00189012") as CodeString; } }
        public List<CodeString> MultiPlanarExcitation_ { get { return Items.FindAll<CodeString>("00189012").ToList(); } }
        public CodeString PhaseContrast { get { return Items.FindFirst<CodeString>("00189014") as CodeString; } }
        public List<CodeString> PhaseContrast_ { get { return Items.FindAll<CodeString>("00189014").ToList(); } }
        public CodeString TimeOfFlightContrast { get { return Items.FindFirst<CodeString>("00189015") as CodeString; } }
        public List<CodeString> TimeOfFlightContrast_ { get { return Items.FindAll<CodeString>("00189015").ToList(); } }
        public CodeString Spoiling { get { return Items.FindFirst<CodeString>("00189016") as CodeString; } }
        public List<CodeString> Spoiling_ { get { return Items.FindAll<CodeString>("00189016").ToList(); } }
        public CodeString SteadyStatePulseSequence { get { return Items.FindFirst<CodeString>("00189017") as CodeString; } }
        public List<CodeString> SteadyStatePulseSequence_ { get { return Items.FindAll<CodeString>("00189017").ToList(); } }
        public CodeString EchoPlanarPulseSequence { get { return Items.FindFirst<CodeString>("00189018") as CodeString; } }
        public List<CodeString> EchoPlanarPulseSequence_ { get { return Items.FindAll<CodeString>("00189018").ToList(); } }
        public FloatingPointDouble TagAngleFirstAxis { get { return Items.FindFirst<FloatingPointDouble>("00189019") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TagAngleFirstAxis_ { get { return Items.FindAll<FloatingPointDouble>("00189019").ToList(); } }
        public CodeString MagnetizationTransfer { get { return Items.FindFirst<CodeString>("00189020") as CodeString; } }
        public List<CodeString> MagnetizationTransfer_ { get { return Items.FindAll<CodeString>("00189020").ToList(); } }
        public CodeString T2Preparation { get { return Items.FindFirst<CodeString>("00189021") as CodeString; } }
        public List<CodeString> T2Preparation_ { get { return Items.FindAll<CodeString>("00189021").ToList(); } }
        public CodeString BloodSignalNulling { get { return Items.FindFirst<CodeString>("00189022") as CodeString; } }
        public List<CodeString> BloodSignalNulling_ { get { return Items.FindAll<CodeString>("00189022").ToList(); } }
        public CodeString SaturationRecovery { get { return Items.FindFirst<CodeString>("00189024") as CodeString; } }
        public List<CodeString> SaturationRecovery_ { get { return Items.FindAll<CodeString>("00189024").ToList(); } }
        public CodeString SpectrallySelectedSuppression { get { return Items.FindFirst<CodeString>("00189025") as CodeString; } }
        public List<CodeString> SpectrallySelectedSuppression_ { get { return Items.FindAll<CodeString>("00189025").ToList(); } }
        public CodeString SpectrallySelectedExcitation { get { return Items.FindFirst<CodeString>("00189026") as CodeString; } }
        public List<CodeString> SpectrallySelectedExcitation_ { get { return Items.FindAll<CodeString>("00189026").ToList(); } }
        public CodeString SpatialPresaturation { get { return Items.FindFirst<CodeString>("00189027") as CodeString; } }
        public List<CodeString> SpatialPresaturation_ { get { return Items.FindAll<CodeString>("00189027").ToList(); } }
        public CodeString Tagging { get { return Items.FindFirst<CodeString>("00189028") as CodeString; } }
        public List<CodeString> Tagging_ { get { return Items.FindAll<CodeString>("00189028").ToList(); } }
        public CodeString OversamplingPhase { get { return Items.FindFirst<CodeString>("00189029") as CodeString; } }
        public List<CodeString> OversamplingPhase_ { get { return Items.FindAll<CodeString>("00189029").ToList(); } }
        public FloatingPointDouble TagSpacingFirstDimension { get { return Items.FindFirst<FloatingPointDouble>("00189030") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TagSpacingFirstDimension_ { get { return Items.FindAll<FloatingPointDouble>("00189030").ToList(); } }
        public CodeString GeometryOfKSpaceTraversal { get { return Items.FindFirst<CodeString>("00189032") as CodeString; } }
        public List<CodeString> GeometryOfKSpaceTraversal_ { get { return Items.FindAll<CodeString>("00189032").ToList(); } }
        public CodeString SegmentedKSpaceTraversal { get { return Items.FindFirst<CodeString>("00189033") as CodeString; } }
        public List<CodeString> SegmentedKSpaceTraversal_ { get { return Items.FindAll<CodeString>("00189033").ToList(); } }
        public CodeString RectilinearPhaseEncodeReordering { get { return Items.FindFirst<CodeString>("00189034") as CodeString; } }
        public List<CodeString> RectilinearPhaseEncodeReordering_ { get { return Items.FindAll<CodeString>("00189034").ToList(); } }
        public FloatingPointDouble TagThickness { get { return Items.FindFirst<FloatingPointDouble>("00189035") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TagThickness_ { get { return Items.FindAll<FloatingPointDouble>("00189035").ToList(); } }
        public CodeString PartialFourierDirection { get { return Items.FindFirst<CodeString>("00189036") as CodeString; } }
        public List<CodeString> PartialFourierDirection_ { get { return Items.FindAll<CodeString>("00189036").ToList(); } }
        public CodeString CardiacSynchronizationTechnique { get { return Items.FindFirst<CodeString>("00189037") as CodeString; } }
        public List<CodeString> CardiacSynchronizationTechnique_ { get { return Items.FindAll<CodeString>("00189037").ToList(); } }
        public LongString ReceiveCoilManufacturerName { get { return Items.FindFirst<LongString>("00189041") as LongString; } }
        public List<LongString> ReceiveCoilManufacturerName_ { get { return Items.FindAll<LongString>("00189041").ToList(); } }
        public SequenceSelector MRReceiveCoilSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189042")); } }
        public List<SequenceSelector> MRReceiveCoilSequence_ { get { return Items.FindAll<Sequence>("00189042").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ReceiveCoilType { get { return Items.FindFirst<CodeString>("00189043") as CodeString; } }
        public List<CodeString> ReceiveCoilType_ { get { return Items.FindAll<CodeString>("00189043").ToList(); } }
        public CodeString QuadratureReceiveCoil { get { return Items.FindFirst<CodeString>("00189044") as CodeString; } }
        public List<CodeString> QuadratureReceiveCoil_ { get { return Items.FindAll<CodeString>("00189044").ToList(); } }
        public SequenceSelector MultiCoilDefinitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189045")); } }
        public List<SequenceSelector> MultiCoilDefinitionSequence_ { get { return Items.FindAll<Sequence>("00189045").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString MultiCoilConfiguration { get { return Items.FindFirst<LongString>("00189046") as LongString; } }
        public List<LongString> MultiCoilConfiguration_ { get { return Items.FindAll<LongString>("00189046").ToList(); } }
        public ShortString MultiCoilElementName { get { return Items.FindFirst<ShortString>("00189047") as ShortString; } }
        public List<ShortString> MultiCoilElementName_ { get { return Items.FindAll<ShortString>("00189047").ToList(); } }
        public CodeString MultiCoilElementUsed { get { return Items.FindFirst<CodeString>("00189048") as CodeString; } }
        public List<CodeString> MultiCoilElementUsed_ { get { return Items.FindAll<CodeString>("00189048").ToList(); } }
        public SequenceSelector MRTransmitCoilSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189049")); } }
        public List<SequenceSelector> MRTransmitCoilSequence_ { get { return Items.FindAll<Sequence>("00189049").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString TransmitCoilManufacturerName { get { return Items.FindFirst<LongString>("00189050") as LongString; } }
        public List<LongString> TransmitCoilManufacturerName_ { get { return Items.FindAll<LongString>("00189050").ToList(); } }
        public CodeString TransmitCoilType { get { return Items.FindFirst<CodeString>("00189051") as CodeString; } }
        public List<CodeString> TransmitCoilType_ { get { return Items.FindAll<CodeString>("00189051").ToList(); } }
        public FloatingPointDouble SpectralWidth { get { return Items.FindFirst<FloatingPointDouble>("00189052") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SpectralWidth_ { get { return Items.FindAll<FloatingPointDouble>("00189052").ToList(); } }
        public FloatingPointDouble ChemicalShiftReference { get { return Items.FindFirst<FloatingPointDouble>("00189053") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChemicalShiftReference_ { get { return Items.FindAll<FloatingPointDouble>("00189053").ToList(); } }
        public CodeString VolumeLocalizationTechnique { get { return Items.FindFirst<CodeString>("00189054") as CodeString; } }
        public List<CodeString> VolumeLocalizationTechnique_ { get { return Items.FindAll<CodeString>("00189054").ToList(); } }
        public UnsignedShort MRAcquisitionFrequencyEncodingSteps { get { return Items.FindFirst<UnsignedShort>("00189058") as UnsignedShort; } }
        public List<UnsignedShort> MRAcquisitionFrequencyEncodingSteps_ { get { return Items.FindAll<UnsignedShort>("00189058").ToList(); } }
        public CodeString Decoupling { get { return Items.FindFirst<CodeString>("00189059") as CodeString; } }
        public List<CodeString> Decoupling_ { get { return Items.FindAll<CodeString>("00189059").ToList(); } }
        public CodeString DecoupledNucleus { get { return Items.FindFirst<CodeString>("00189060") as CodeString; } }
        public List<CodeString> DecoupledNucleus_ { get { return Items.FindAll<CodeString>("00189060").ToList(); } }
        public FloatingPointDouble DecouplingFrequency { get { return Items.FindFirst<FloatingPointDouble>("00189061") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DecouplingFrequency_ { get { return Items.FindAll<FloatingPointDouble>("00189061").ToList(); } }
        public CodeString DecouplingMethod { get { return Items.FindFirst<CodeString>("00189062") as CodeString; } }
        public List<CodeString> DecouplingMethod_ { get { return Items.FindAll<CodeString>("00189062").ToList(); } }
        public FloatingPointDouble DecouplingChemicalShiftReference { get { return Items.FindFirst<FloatingPointDouble>("00189063") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DecouplingChemicalShiftReference_ { get { return Items.FindAll<FloatingPointDouble>("00189063").ToList(); } }
        public CodeString KSpaceFiltering { get { return Items.FindFirst<CodeString>("00189064") as CodeString; } }
        public List<CodeString> KSpaceFiltering_ { get { return Items.FindAll<CodeString>("00189064").ToList(); } }
        public CodeString TimeDomainFiltering { get { return Items.FindFirst<CodeString>("00189065") as CodeString; } }
        public List<CodeString> TimeDomainFiltering_ { get { return Items.FindAll<CodeString>("00189065").ToList(); } }
        public UnsignedShort NumberOfZeroFills { get { return Items.FindFirst<UnsignedShort>("00189066") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfZeroFills_ { get { return Items.FindAll<UnsignedShort>("00189066").ToList(); } }
        public CodeString BaselineCorrection { get { return Items.FindFirst<CodeString>("00189067") as CodeString; } }
        public List<CodeString> BaselineCorrection_ { get { return Items.FindAll<CodeString>("00189067").ToList(); } }
        public FloatingPointDouble ParallelReductionFactorInPlane { get { return Items.FindFirst<FloatingPointDouble>("00189069") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ParallelReductionFactorInPlane_ { get { return Items.FindAll<FloatingPointDouble>("00189069").ToList(); } }
        public FloatingPointDouble CardiacRRIntervalSpecified { get { return Items.FindFirst<FloatingPointDouble>("00189070") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CardiacRRIntervalSpecified_ { get { return Items.FindAll<FloatingPointDouble>("00189070").ToList(); } }
        public FloatingPointDouble AcquisitionDuration { get { return Items.FindFirst<FloatingPointDouble>("00189073") as FloatingPointDouble; } }
        public List<FloatingPointDouble> AcquisitionDuration_ { get { return Items.FindAll<FloatingPointDouble>("00189073").ToList(); } }
        public EvilDICOM.Core.Element.DateTime FrameAcquisitionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189074") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> FrameAcquisitionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189074").ToList(); } }
        public CodeString DiffusionDirectionality { get { return Items.FindFirst<CodeString>("00189075") as CodeString; } }
        public List<CodeString> DiffusionDirectionality_ { get { return Items.FindAll<CodeString>("00189075").ToList(); } }
        public SequenceSelector DiffusionGradientDirectionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189076")); } }
        public List<SequenceSelector> DiffusionGradientDirectionSequence_ { get { return Items.FindAll<Sequence>("00189076").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ParallelAcquisition { get { return Items.FindFirst<CodeString>("00189077") as CodeString; } }
        public List<CodeString> ParallelAcquisition_ { get { return Items.FindAll<CodeString>("00189077").ToList(); } }
        public CodeString ParallelAcquisitionTechnique { get { return Items.FindFirst<CodeString>("00189078") as CodeString; } }
        public List<CodeString> ParallelAcquisitionTechnique_ { get { return Items.FindAll<CodeString>("00189078").ToList(); } }
        public FloatingPointDouble InversionTimes { get { return Items.FindFirst<FloatingPointDouble>("00189079") as FloatingPointDouble; } }
        public List<FloatingPointDouble> InversionTimes_ { get { return Items.FindAll<FloatingPointDouble>("00189079").ToList(); } }
        public ShortText MetaboliteMapDescription { get { return Items.FindFirst<ShortText>("00189080") as ShortText; } }
        public List<ShortText> MetaboliteMapDescription_ { get { return Items.FindAll<ShortText>("00189080").ToList(); } }
        public CodeString PartialFourier { get { return Items.FindFirst<CodeString>("00189081") as CodeString; } }
        public List<CodeString> PartialFourier_ { get { return Items.FindAll<CodeString>("00189081").ToList(); } }
        public FloatingPointDouble EffectiveEchoTime { get { return Items.FindFirst<FloatingPointDouble>("00189082") as FloatingPointDouble; } }
        public List<FloatingPointDouble> EffectiveEchoTime_ { get { return Items.FindAll<FloatingPointDouble>("00189082").ToList(); } }
        public SequenceSelector MetaboliteMapCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189083")); } }
        public List<SequenceSelector> MetaboliteMapCodeSequence_ { get { return Items.FindAll<Sequence>("00189083").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ChemicalShiftSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189084")); } }
        public List<SequenceSelector> ChemicalShiftSequence_ { get { return Items.FindAll<Sequence>("00189084").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString CardiacSignalSource { get { return Items.FindFirst<CodeString>("00189085") as CodeString; } }
        public List<CodeString> CardiacSignalSource_ { get { return Items.FindAll<CodeString>("00189085").ToList(); } }
        public FloatingPointDouble DiffusionBValue { get { return Items.FindFirst<FloatingPointDouble>("00189087") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValue_ { get { return Items.FindAll<FloatingPointDouble>("00189087").ToList(); } }
        public FloatingPointDouble DiffusionGradientOrientation { get { return Items.FindFirst<FloatingPointDouble>("00189089") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionGradientOrientation_ { get { return Items.FindAll<FloatingPointDouble>("00189089").ToList(); } }
        public FloatingPointDouble VelocityEncodingDirection { get { return Items.FindFirst<FloatingPointDouble>("00189090") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VelocityEncodingDirection_ { get { return Items.FindAll<FloatingPointDouble>("00189090").ToList(); } }
        public FloatingPointDouble VelocityEncodingMinimumValue { get { return Items.FindFirst<FloatingPointDouble>("00189091") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VelocityEncodingMinimumValue_ { get { return Items.FindAll<FloatingPointDouble>("00189091").ToList(); } }
        public SequenceSelector VelocityEncodingAcquisitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189092")); } }
        public List<SequenceSelector> VelocityEncodingAcquisitionSequence_ { get { return Items.FindAll<Sequence>("00189092").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfKSpaceTrajectories { get { return Items.FindFirst<UnsignedShort>("00189093") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfKSpaceTrajectories_ { get { return Items.FindAll<UnsignedShort>("00189093").ToList(); } }
        public CodeString CoverageOfKSpace { get { return Items.FindFirst<CodeString>("00189094") as CodeString; } }
        public List<CodeString> CoverageOfKSpace_ { get { return Items.FindAll<CodeString>("00189094").ToList(); } }
        public UnsignedLong SpectroscopyAcquisitionPhaseRows { get { return Items.FindFirst<UnsignedLong>("00189095") as UnsignedLong; } }
        public List<UnsignedLong> SpectroscopyAcquisitionPhaseRows_ { get { return Items.FindAll<UnsignedLong>("00189095").ToList(); } }
        public FloatingPointDouble ParallelReductionFactorInPlaneRetired { get { return Items.FindFirst<FloatingPointDouble>("00189096") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ParallelReductionFactorInPlaneRetired_ { get { return Items.FindAll<FloatingPointDouble>("00189096").ToList(); } }
        public FloatingPointDouble TransmitterFrequency { get { return Items.FindFirst<FloatingPointDouble>("00189098") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TransmitterFrequency_ { get { return Items.FindAll<FloatingPointDouble>("00189098").ToList(); } }
        public CodeString ResonantNucleus { get { return Items.FindFirst<CodeString>("00189100") as CodeString; } }
        public List<CodeString> ResonantNucleus_ { get { return Items.FindAll<CodeString>("00189100").ToList(); } }
        public CodeString FrequencyCorrection { get { return Items.FindFirst<CodeString>("00189101") as CodeString; } }
        public List<CodeString> FrequencyCorrection_ { get { return Items.FindAll<CodeString>("00189101").ToList(); } }
        public SequenceSelector MRSpectroscopyFOVGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189103")); } }
        public List<SequenceSelector> MRSpectroscopyFOVGeometrySequence_ { get { return Items.FindAll<Sequence>("00189103").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble SlabThickness { get { return Items.FindFirst<FloatingPointDouble>("00189104") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SlabThickness_ { get { return Items.FindAll<FloatingPointDouble>("00189104").ToList(); } }
        public FloatingPointDouble SlabOrientation { get { return Items.FindFirst<FloatingPointDouble>("00189105") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SlabOrientation_ { get { return Items.FindAll<FloatingPointDouble>("00189105").ToList(); } }
        public FloatingPointDouble MidSlabPosition { get { return Items.FindFirst<FloatingPointDouble>("00189106") as FloatingPointDouble; } }
        public List<FloatingPointDouble> MidSlabPosition_ { get { return Items.FindAll<FloatingPointDouble>("00189106").ToList(); } }
        public SequenceSelector MRSpatialSaturationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189107")); } }
        public List<SequenceSelector> MRSpatialSaturationSequence_ { get { return Items.FindAll<Sequence>("00189107").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRTimingAndRelatedParametersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189112")); } }
        public List<SequenceSelector> MRTimingAndRelatedParametersSequence_ { get { return Items.FindAll<Sequence>("00189112").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MREchoSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189114")); } }
        public List<SequenceSelector> MREchoSequence_ { get { return Items.FindAll<Sequence>("00189114").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRModifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189115")); } }
        public List<SequenceSelector> MRModifierSequence_ { get { return Items.FindAll<Sequence>("00189115").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRDiffusionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189117")); } }
        public List<SequenceSelector> MRDiffusionSequence_ { get { return Items.FindAll<Sequence>("00189117").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CardiacSynchronizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189118")); } }
        public List<SequenceSelector> CardiacSynchronizationSequence_ { get { return Items.FindAll<Sequence>("00189118").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRAveragesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189119")); } }
        public List<SequenceSelector> MRAveragesSequence_ { get { return Items.FindAll<Sequence>("00189119").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRFOVGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189125")); } }
        public List<SequenceSelector> MRFOVGeometrySequence_ { get { return Items.FindAll<Sequence>("00189125").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VolumeLocalizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189126")); } }
        public List<SequenceSelector> VolumeLocalizationSequence_ { get { return Items.FindAll<Sequence>("00189126").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong SpectroscopyAcquisitionDataColumns { get { return Items.FindFirst<UnsignedLong>("00189127") as UnsignedLong; } }
        public List<UnsignedLong> SpectroscopyAcquisitionDataColumns_ { get { return Items.FindAll<UnsignedLong>("00189127").ToList(); } }
        public CodeString DiffusionAnisotropyType { get { return Items.FindFirst<CodeString>("00189147") as CodeString; } }
        public List<CodeString> DiffusionAnisotropyType_ { get { return Items.FindAll<CodeString>("00189147").ToList(); } }
        public EvilDICOM.Core.Element.DateTime FrameReferenceDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189151") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> FrameReferenceDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189151").ToList(); } }
        public SequenceSelector MRMetaboliteMapSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189152")); } }
        public List<SequenceSelector> MRMetaboliteMapSequence_ { get { return Items.FindAll<Sequence>("00189152").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ParallelReductionFactorOutOfPlane { get { return Items.FindFirst<FloatingPointDouble>("00189155") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ParallelReductionFactorOutOfPlane_ { get { return Items.FindAll<FloatingPointDouble>("00189155").ToList(); } }
        public UnsignedLong SpectroscopyAcquisitionOutOfPlanePhaseSteps { get { return Items.FindFirst<UnsignedLong>("00189159") as UnsignedLong; } }
        public List<UnsignedLong> SpectroscopyAcquisitionOutOfPlanePhaseSteps_ { get { return Items.FindAll<UnsignedLong>("00189159").ToList(); } }
        public CodeString BulkMotionStatusRetired { get { return Items.FindFirst<CodeString>("00189166") as CodeString; } }
        public List<CodeString> BulkMotionStatusRetired_ { get { return Items.FindAll<CodeString>("00189166").ToList(); } }
        public FloatingPointDouble ParallelReductionFactorSecondInPlane { get { return Items.FindFirst<FloatingPointDouble>("00189168") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ParallelReductionFactorSecondInPlane_ { get { return Items.FindAll<FloatingPointDouble>("00189168").ToList(); } }
        public CodeString CardiacBeatRejectionTechnique { get { return Items.FindFirst<CodeString>("00189169") as CodeString; } }
        public List<CodeString> CardiacBeatRejectionTechnique_ { get { return Items.FindAll<CodeString>("00189169").ToList(); } }
        public CodeString RespiratoryMotionCompensationTechnique { get { return Items.FindFirst<CodeString>("00189170") as CodeString; } }
        public List<CodeString> RespiratoryMotionCompensationTechnique_ { get { return Items.FindAll<CodeString>("00189170").ToList(); } }
        public CodeString RespiratorySignalSource { get { return Items.FindFirst<CodeString>("00189171") as CodeString; } }
        public List<CodeString> RespiratorySignalSource_ { get { return Items.FindAll<CodeString>("00189171").ToList(); } }
        public CodeString BulkMotionCompensationTechnique { get { return Items.FindFirst<CodeString>("00189172") as CodeString; } }
        public List<CodeString> BulkMotionCompensationTechnique_ { get { return Items.FindAll<CodeString>("00189172").ToList(); } }
        public CodeString BulkMotionSignalSource { get { return Items.FindFirst<CodeString>("00189173") as CodeString; } }
        public List<CodeString> BulkMotionSignalSource_ { get { return Items.FindAll<CodeString>("00189173").ToList(); } }
        public CodeString ApplicableSafetyStandardAgency { get { return Items.FindFirst<CodeString>("00189174") as CodeString; } }
        public List<CodeString> ApplicableSafetyStandardAgency_ { get { return Items.FindAll<CodeString>("00189174").ToList(); } }
        public LongString ApplicableSafetyStandardDescription { get { return Items.FindFirst<LongString>("00189175") as LongString; } }
        public List<LongString> ApplicableSafetyStandardDescription_ { get { return Items.FindAll<LongString>("00189175").ToList(); } }
        public SequenceSelector OperatingModeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189176")); } }
        public List<SequenceSelector> OperatingModeSequence_ { get { return Items.FindAll<Sequence>("00189176").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString OperatingModeType { get { return Items.FindFirst<CodeString>("00189177") as CodeString; } }
        public List<CodeString> OperatingModeType_ { get { return Items.FindAll<CodeString>("00189177").ToList(); } }
        public CodeString OperatingMode { get { return Items.FindFirst<CodeString>("00189178") as CodeString; } }
        public List<CodeString> OperatingMode_ { get { return Items.FindAll<CodeString>("00189178").ToList(); } }
        public CodeString SpecificAbsorptionRateDefinition { get { return Items.FindFirst<CodeString>("00189179") as CodeString; } }
        public List<CodeString> SpecificAbsorptionRateDefinition_ { get { return Items.FindAll<CodeString>("00189179").ToList(); } }
        public CodeString GradientOutputType { get { return Items.FindFirst<CodeString>("00189180") as CodeString; } }
        public List<CodeString> GradientOutputType_ { get { return Items.FindAll<CodeString>("00189180").ToList(); } }
        public FloatingPointDouble SpecificAbsorptionRateValue { get { return Items.FindFirst<FloatingPointDouble>("00189181") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SpecificAbsorptionRateValue_ { get { return Items.FindAll<FloatingPointDouble>("00189181").ToList(); } }
        public FloatingPointDouble GradientOutput { get { return Items.FindFirst<FloatingPointDouble>("00189182") as FloatingPointDouble; } }
        public List<FloatingPointDouble> GradientOutput_ { get { return Items.FindAll<FloatingPointDouble>("00189182").ToList(); } }
        public CodeString FlowCompensationDirection { get { return Items.FindFirst<CodeString>("00189183") as CodeString; } }
        public List<CodeString> FlowCompensationDirection_ { get { return Items.FindAll<CodeString>("00189183").ToList(); } }
        public FloatingPointDouble TaggingDelay { get { return Items.FindFirst<FloatingPointDouble>("00189184") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TaggingDelay_ { get { return Items.FindAll<FloatingPointDouble>("00189184").ToList(); } }
        public ShortText RespiratoryMotionCompensationTechniqueDescription { get { return Items.FindFirst<ShortText>("00189185") as ShortText; } }
        public List<ShortText> RespiratoryMotionCompensationTechniqueDescription_ { get { return Items.FindAll<ShortText>("00189185").ToList(); } }
        public ShortString RespiratorySignalSourceID { get { return Items.FindFirst<ShortString>("00189186") as ShortString; } }
        public List<ShortString> RespiratorySignalSourceID_ { get { return Items.FindAll<ShortString>("00189186").ToList(); } }
        public FloatingPointDouble ChemicalShiftMinimumIntegrationLimitInHzRetired { get { return Items.FindFirst<FloatingPointDouble>("00189195") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChemicalShiftMinimumIntegrationLimitInHzRetired_ { get { return Items.FindAll<FloatingPointDouble>("00189195").ToList(); } }
        public FloatingPointDouble ChemicalShiftMaximumIntegrationLimitInHzRetired { get { return Items.FindFirst<FloatingPointDouble>("00189196") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChemicalShiftMaximumIntegrationLimitInHzRetired_ { get { return Items.FindAll<FloatingPointDouble>("00189196").ToList(); } }
        public SequenceSelector MRVelocityEncodingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189197")); } }
        public List<SequenceSelector> MRVelocityEncodingSequence_ { get { return Items.FindAll<Sequence>("00189197").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FirstOrderPhaseCorrection { get { return Items.FindFirst<CodeString>("00189198") as CodeString; } }
        public List<CodeString> FirstOrderPhaseCorrection_ { get { return Items.FindAll<CodeString>("00189198").ToList(); } }
        public CodeString WaterReferencedPhaseCorrection { get { return Items.FindFirst<CodeString>("00189199") as CodeString; } }
        public List<CodeString> WaterReferencedPhaseCorrection_ { get { return Items.FindAll<CodeString>("00189199").ToList(); } }
        public CodeString MRSpectroscopyAcquisitionType { get { return Items.FindFirst<CodeString>("00189200") as CodeString; } }
        public List<CodeString> MRSpectroscopyAcquisitionType_ { get { return Items.FindAll<CodeString>("00189200").ToList(); } }
        public CodeString RespiratoryCyclePosition { get { return Items.FindFirst<CodeString>("00189214") as CodeString; } }
        public List<CodeString> RespiratoryCyclePosition_ { get { return Items.FindAll<CodeString>("00189214").ToList(); } }
        public FloatingPointDouble VelocityEncodingMaximumValue { get { return Items.FindFirst<FloatingPointDouble>("00189217") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VelocityEncodingMaximumValue_ { get { return Items.FindAll<FloatingPointDouble>("00189217").ToList(); } }
        public FloatingPointDouble TagSpacingSecondDimension { get { return Items.FindFirst<FloatingPointDouble>("00189218") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TagSpacingSecondDimension_ { get { return Items.FindAll<FloatingPointDouble>("00189218").ToList(); } }
        public SignedShort TagAngleSecondAxis { get { return Items.FindFirst<SignedShort>("00189219") as SignedShort; } }
        public List<SignedShort> TagAngleSecondAxis_ { get { return Items.FindAll<SignedShort>("00189219").ToList(); } }
        public FloatingPointDouble FrameAcquisitionDuration { get { return Items.FindFirst<FloatingPointDouble>("00189220") as FloatingPointDouble; } }
        public List<FloatingPointDouble> FrameAcquisitionDuration_ { get { return Items.FindAll<FloatingPointDouble>("00189220").ToList(); } }
        public SequenceSelector MRImageFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189226")); } }
        public List<SequenceSelector> MRImageFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00189226").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MRSpectroscopyFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189227")); } }
        public List<SequenceSelector> MRSpectroscopyFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00189227").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort MRAcquisitionPhaseEncodingStepsInPlane { get { return Items.FindFirst<UnsignedShort>("00189231") as UnsignedShort; } }
        public List<UnsignedShort> MRAcquisitionPhaseEncodingStepsInPlane_ { get { return Items.FindAll<UnsignedShort>("00189231").ToList(); } }
        public UnsignedShort MRAcquisitionPhaseEncodingStepsOutOfPlane { get { return Items.FindFirst<UnsignedShort>("00189232") as UnsignedShort; } }
        public List<UnsignedShort> MRAcquisitionPhaseEncodingStepsOutOfPlane_ { get { return Items.FindAll<UnsignedShort>("00189232").ToList(); } }
        public UnsignedLong SpectroscopyAcquisitionPhaseColumns { get { return Items.FindFirst<UnsignedLong>("00189234") as UnsignedLong; } }
        public List<UnsignedLong> SpectroscopyAcquisitionPhaseColumns_ { get { return Items.FindAll<UnsignedLong>("00189234").ToList(); } }
        public CodeString CardiacCyclePosition { get { return Items.FindFirst<CodeString>("00189236") as CodeString; } }
        public List<CodeString> CardiacCyclePosition_ { get { return Items.FindAll<CodeString>("00189236").ToList(); } }
        public SequenceSelector SpecificAbsorptionRateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189239")); } }
        public List<SequenceSelector> SpecificAbsorptionRateSequence_ { get { return Items.FindAll<Sequence>("00189239").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort RFEchoTrainLength { get { return Items.FindFirst<UnsignedShort>("00189240") as UnsignedShort; } }
        public List<UnsignedShort> RFEchoTrainLength_ { get { return Items.FindAll<UnsignedShort>("00189240").ToList(); } }
        public UnsignedShort GradientEchoTrainLength { get { return Items.FindFirst<UnsignedShort>("00189241") as UnsignedShort; } }
        public List<UnsignedShort> GradientEchoTrainLength_ { get { return Items.FindAll<UnsignedShort>("00189241").ToList(); } }
        public CodeString ArterialSpinLabelingContrast { get { return Items.FindFirst<CodeString>("00189250") as CodeString; } }
        public List<CodeString> ArterialSpinLabelingContrast_ { get { return Items.FindAll<CodeString>("00189250").ToList(); } }
        public SequenceSelector MRArterialSpinLabelingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189251")); } }
        public List<SequenceSelector> MRArterialSpinLabelingSequence_ { get { return Items.FindAll<Sequence>("00189251").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ASLTechniqueDescription { get { return Items.FindFirst<LongString>("00189252") as LongString; } }
        public List<LongString> ASLTechniqueDescription_ { get { return Items.FindAll<LongString>("00189252").ToList(); } }
        public UnsignedShort ASLSlabNumber { get { return Items.FindFirst<UnsignedShort>("00189253") as UnsignedShort; } }
        public List<UnsignedShort> ASLSlabNumber_ { get { return Items.FindAll<UnsignedShort>("00189253").ToList(); } }
        public FloatingPointDouble ASLSlabThickness { get { return Items.FindFirst<FloatingPointDouble>("00189254") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ASLSlabThickness_ { get { return Items.FindAll<FloatingPointDouble>("00189254").ToList(); } }
        public FloatingPointDouble ASLSlabOrientation { get { return Items.FindFirst<FloatingPointDouble>("00189255") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ASLSlabOrientation_ { get { return Items.FindAll<FloatingPointDouble>("00189255").ToList(); } }
        public FloatingPointDouble ASLMidSlabPosition { get { return Items.FindFirst<FloatingPointDouble>("00189256") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ASLMidSlabPosition_ { get { return Items.FindAll<FloatingPointDouble>("00189256").ToList(); } }
        public CodeString ASLContext { get { return Items.FindFirst<CodeString>("00189257") as CodeString; } }
        public List<CodeString> ASLContext_ { get { return Items.FindAll<CodeString>("00189257").ToList(); } }
        public UnsignedLong ASLPulseTrainDuration { get { return Items.FindFirst<UnsignedLong>("00189258") as UnsignedLong; } }
        public List<UnsignedLong> ASLPulseTrainDuration_ { get { return Items.FindAll<UnsignedLong>("00189258").ToList(); } }
        public CodeString ASLCrusherFlag { get { return Items.FindFirst<CodeString>("00189259") as CodeString; } }
        public List<CodeString> ASLCrusherFlag_ { get { return Items.FindAll<CodeString>("00189259").ToList(); } }
        public FloatingPointDouble ASLCrusherFlow { get { return Items.FindFirst<FloatingPointDouble>("0018925A") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ASLCrusherFlow_ { get { return Items.FindAll<FloatingPointDouble>("0018925A").ToList(); } }
        public LongString ASLCrusherDescription { get { return Items.FindFirst<LongString>("0018925B") as LongString; } }
        public List<LongString> ASLCrusherDescription_ { get { return Items.FindAll<LongString>("0018925B").ToList(); } }
        public CodeString ASLBolusCutoffFlag { get { return Items.FindFirst<CodeString>("0018925C") as CodeString; } }
        public List<CodeString> ASLBolusCutoffFlag_ { get { return Items.FindAll<CodeString>("0018925C").ToList(); } }
        public SequenceSelector ASLBolusCutoffTimingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018925D")); } }
        public List<SequenceSelector> ASLBolusCutoffTimingSequence_ { get { return Items.FindAll<Sequence>("0018925D").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ASLBolusCutoffTechnique { get { return Items.FindFirst<LongString>("0018925E") as LongString; } }
        public List<LongString> ASLBolusCutoffTechnique_ { get { return Items.FindAll<LongString>("0018925E").ToList(); } }
        public UnsignedLong ASLBolusCutoffDelayTime { get { return Items.FindFirst<UnsignedLong>("0018925F") as UnsignedLong; } }
        public List<UnsignedLong> ASLBolusCutoffDelayTime_ { get { return Items.FindAll<UnsignedLong>("0018925F").ToList(); } }
        public SequenceSelector ASLSlabSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189260")); } }
        public List<SequenceSelector> ASLSlabSequence_ { get { return Items.FindAll<Sequence>("00189260").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ChemicalShiftMinimumIntegrationLimitInppm { get { return Items.FindFirst<FloatingPointDouble>("00189295") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChemicalShiftMinimumIntegrationLimitInppm_ { get { return Items.FindAll<FloatingPointDouble>("00189295").ToList(); } }
        public FloatingPointDouble ChemicalShiftMaximumIntegrationLimitInppm { get { return Items.FindFirst<FloatingPointDouble>("00189296") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChemicalShiftMaximumIntegrationLimitInppm_ { get { return Items.FindAll<FloatingPointDouble>("00189296").ToList(); } }
        public SequenceSelector CTAcquisitionTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189301")); } }
        public List<SequenceSelector> CTAcquisitionTypeSequence_ { get { return Items.FindAll<Sequence>("00189301").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString AcquisitionType { get { return Items.FindFirst<CodeString>("00189302") as CodeString; } }
        public List<CodeString> AcquisitionType_ { get { return Items.FindAll<CodeString>("00189302").ToList(); } }
        public FloatingPointDouble TubeAngle { get { return Items.FindFirst<FloatingPointDouble>("00189303") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TubeAngle_ { get { return Items.FindAll<FloatingPointDouble>("00189303").ToList(); } }
        public SequenceSelector CTAcquisitionDetailsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189304")); } }
        public List<SequenceSelector> CTAcquisitionDetailsSequence_ { get { return Items.FindAll<Sequence>("00189304").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble RevolutionTime { get { return Items.FindFirst<FloatingPointDouble>("00189305") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RevolutionTime_ { get { return Items.FindAll<FloatingPointDouble>("00189305").ToList(); } }
        public FloatingPointDouble SingleCollimationWidth { get { return Items.FindFirst<FloatingPointDouble>("00189306") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SingleCollimationWidth_ { get { return Items.FindAll<FloatingPointDouble>("00189306").ToList(); } }
        public FloatingPointDouble TotalCollimationWidth { get { return Items.FindFirst<FloatingPointDouble>("00189307") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TotalCollimationWidth_ { get { return Items.FindAll<FloatingPointDouble>("00189307").ToList(); } }
        public SequenceSelector CTTableDynamicsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189308")); } }
        public List<SequenceSelector> CTTableDynamicsSequence_ { get { return Items.FindAll<Sequence>("00189308").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TableSpeed { get { return Items.FindFirst<FloatingPointDouble>("00189309") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableSpeed_ { get { return Items.FindAll<FloatingPointDouble>("00189309").ToList(); } }
        public FloatingPointDouble TableFeedPerRotation { get { return Items.FindFirst<FloatingPointDouble>("00189310") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableFeedPerRotation_ { get { return Items.FindAll<FloatingPointDouble>("00189310").ToList(); } }
        public FloatingPointDouble SpiralPitchFactor { get { return Items.FindFirst<FloatingPointDouble>("00189311") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SpiralPitchFactor_ { get { return Items.FindAll<FloatingPointDouble>("00189311").ToList(); } }
        public SequenceSelector CTGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189312")); } }
        public List<SequenceSelector> CTGeometrySequence_ { get { return Items.FindAll<Sequence>("00189312").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble DataCollectionCenterPatient { get { return Items.FindFirst<FloatingPointDouble>("00189313") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DataCollectionCenterPatient_ { get { return Items.FindAll<FloatingPointDouble>("00189313").ToList(); } }
        public SequenceSelector CTReconstructionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189314")); } }
        public List<SequenceSelector> CTReconstructionSequence_ { get { return Items.FindAll<Sequence>("00189314").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ReconstructionAlgorithm { get { return Items.FindFirst<CodeString>("00189315") as CodeString; } }
        public List<CodeString> ReconstructionAlgorithm_ { get { return Items.FindAll<CodeString>("00189315").ToList(); } }
        public CodeString ConvolutionKernelGroup { get { return Items.FindFirst<CodeString>("00189316") as CodeString; } }
        public List<CodeString> ConvolutionKernelGroup_ { get { return Items.FindAll<CodeString>("00189316").ToList(); } }
        public FloatingPointDouble ReconstructionFieldOfView { get { return Items.FindFirst<FloatingPointDouble>("00189317") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReconstructionFieldOfView_ { get { return Items.FindAll<FloatingPointDouble>("00189317").ToList(); } }
        public FloatingPointDouble ReconstructionTargetCenterPatient { get { return Items.FindFirst<FloatingPointDouble>("00189318") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReconstructionTargetCenterPatient_ { get { return Items.FindAll<FloatingPointDouble>("00189318").ToList(); } }
        public FloatingPointDouble ReconstructionAngle { get { return Items.FindFirst<FloatingPointDouble>("00189319") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReconstructionAngle_ { get { return Items.FindAll<FloatingPointDouble>("00189319").ToList(); } }
        public ShortString ImageFilter { get { return Items.FindFirst<ShortString>("00189320") as ShortString; } }
        public List<ShortString> ImageFilter_ { get { return Items.FindAll<ShortString>("00189320").ToList(); } }
        public SequenceSelector CTExposureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189321")); } }
        public List<SequenceSelector> CTExposureSequence_ { get { return Items.FindAll<Sequence>("00189321").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ReconstructionPixelSpacing { get { return Items.FindFirst<FloatingPointDouble>("00189322") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReconstructionPixelSpacing_ { get { return Items.FindAll<FloatingPointDouble>("00189322").ToList(); } }
        public CodeString ExposureModulationType { get { return Items.FindFirst<CodeString>("00189323") as CodeString; } }
        public List<CodeString> ExposureModulationType_ { get { return Items.FindAll<CodeString>("00189323").ToList(); } }
        public FloatingPointDouble EstimatedDoseSaving { get { return Items.FindFirst<FloatingPointDouble>("00189324") as FloatingPointDouble; } }
        public List<FloatingPointDouble> EstimatedDoseSaving_ { get { return Items.FindAll<FloatingPointDouble>("00189324").ToList(); } }
        public SequenceSelector CTXRayDetailsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189325")); } }
        public List<SequenceSelector> CTXRayDetailsSequence_ { get { return Items.FindAll<Sequence>("00189325").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CTPositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189326")); } }
        public List<SequenceSelector> CTPositionSequence_ { get { return Items.FindAll<Sequence>("00189326").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TablePosition { get { return Items.FindFirst<FloatingPointDouble>("00189327") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TablePosition_ { get { return Items.FindAll<FloatingPointDouble>("00189327").ToList(); } }
        public FloatingPointDouble ExposureTimeInms { get { return Items.FindFirst<FloatingPointDouble>("00189328") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ExposureTimeInms_ { get { return Items.FindAll<FloatingPointDouble>("00189328").ToList(); } }
        public SequenceSelector CTImageFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189329")); } }
        public List<SequenceSelector> CTImageFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00189329").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble XRayTubeCurrentInmA { get { return Items.FindFirst<FloatingPointDouble>("00189330") as FloatingPointDouble; } }
        public List<FloatingPointDouble> XRayTubeCurrentInmA_ { get { return Items.FindAll<FloatingPointDouble>("00189330").ToList(); } }
        public FloatingPointDouble ExposureInmAs { get { return Items.FindFirst<FloatingPointDouble>("00189332") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ExposureInmAs_ { get { return Items.FindAll<FloatingPointDouble>("00189332").ToList(); } }
        public CodeString ConstantVolumeFlag { get { return Items.FindFirst<CodeString>("00189333") as CodeString; } }
        public List<CodeString> ConstantVolumeFlag_ { get { return Items.FindAll<CodeString>("00189333").ToList(); } }
        public CodeString FluoroscopyFlag { get { return Items.FindFirst<CodeString>("00189334") as CodeString; } }
        public List<CodeString> FluoroscopyFlag_ { get { return Items.FindAll<CodeString>("00189334").ToList(); } }
        public FloatingPointDouble DistanceSourceToDataCollectionCenter { get { return Items.FindFirst<FloatingPointDouble>("00189335") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DistanceSourceToDataCollectionCenter_ { get { return Items.FindAll<FloatingPointDouble>("00189335").ToList(); } }
        public UnsignedShort ContrastBolusAgentNumber { get { return Items.FindFirst<UnsignedShort>("00189337") as UnsignedShort; } }
        public List<UnsignedShort> ContrastBolusAgentNumber_ { get { return Items.FindAll<UnsignedShort>("00189337").ToList(); } }
        public SequenceSelector ContrastBolusIngredientCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189338")); } }
        public List<SequenceSelector> ContrastBolusIngredientCodeSequence_ { get { return Items.FindAll<Sequence>("00189338").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContrastAdministrationProfileSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189340")); } }
        public List<SequenceSelector> ContrastAdministrationProfileSequence_ { get { return Items.FindAll<Sequence>("00189340").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContrastBolusUsageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189341")); } }
        public List<SequenceSelector> ContrastBolusUsageSequence_ { get { return Items.FindAll<Sequence>("00189341").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ContrastBolusAgentAdministered { get { return Items.FindFirst<CodeString>("00189342") as CodeString; } }
        public List<CodeString> ContrastBolusAgentAdministered_ { get { return Items.FindAll<CodeString>("00189342").ToList(); } }
        public CodeString ContrastBolusAgentDetected { get { return Items.FindFirst<CodeString>("00189343") as CodeString; } }
        public List<CodeString> ContrastBolusAgentDetected_ { get { return Items.FindAll<CodeString>("00189343").ToList(); } }
        public CodeString ContrastBolusAgentPhase { get { return Items.FindFirst<CodeString>("00189344") as CodeString; } }
        public List<CodeString> ContrastBolusAgentPhase_ { get { return Items.FindAll<CodeString>("00189344").ToList(); } }
        public FloatingPointDouble CTDIvol { get { return Items.FindFirst<FloatingPointDouble>("00189345") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CTDIvol_ { get { return Items.FindAll<FloatingPointDouble>("00189345").ToList(); } }
        public SequenceSelector CTDIPhantomTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189346")); } }
        public List<SequenceSelector> CTDIPhantomTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00189346").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle CalciumScoringMassFactorPatient { get { return Items.FindFirst<FloatingPointSingle>("00189351") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CalciumScoringMassFactorPatient_ { get { return Items.FindAll<FloatingPointSingle>("00189351").ToList(); } }
        public FloatingPointSingle CalciumScoringMassFactorDevice { get { return Items.FindFirst<FloatingPointSingle>("00189352") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CalciumScoringMassFactorDevice_ { get { return Items.FindAll<FloatingPointSingle>("00189352").ToList(); } }
        public FloatingPointSingle EnergyWeightingFactor { get { return Items.FindFirst<FloatingPointSingle>("00189353") as FloatingPointSingle; } }
        public List<FloatingPointSingle> EnergyWeightingFactor_ { get { return Items.FindAll<FloatingPointSingle>("00189353").ToList(); } }
        public SequenceSelector CTAdditionalXRaySourceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189360")); } }
        public List<SequenceSelector> CTAdditionalXRaySourceSequence_ { get { return Items.FindAll<Sequence>("00189360").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ProjectionPixelCalibrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189401")); } }
        public List<SequenceSelector> ProjectionPixelCalibrationSequence_ { get { return Items.FindAll<Sequence>("00189401").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle DistanceSourceToIsocenter { get { return Items.FindFirst<FloatingPointSingle>("00189402") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DistanceSourceToIsocenter_ { get { return Items.FindAll<FloatingPointSingle>("00189402").ToList(); } }
        public FloatingPointSingle DistanceObjectToTableTop { get { return Items.FindFirst<FloatingPointSingle>("00189403") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DistanceObjectToTableTop_ { get { return Items.FindAll<FloatingPointSingle>("00189403").ToList(); } }
        public FloatingPointSingle ObjectPixelSpacingInCenterOfBeam { get { return Items.FindFirst<FloatingPointSingle>("00189404") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ObjectPixelSpacingInCenterOfBeam_ { get { return Items.FindAll<FloatingPointSingle>("00189404").ToList(); } }
        public SequenceSelector PositionerPositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189405")); } }
        public List<SequenceSelector> PositionerPositionSequence_ { get { return Items.FindAll<Sequence>("00189405").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TablePositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189406")); } }
        public List<SequenceSelector> TablePositionSequence_ { get { return Items.FindAll<Sequence>("00189406").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CollimatorShapeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189407")); } }
        public List<SequenceSelector> CollimatorShapeSequence_ { get { return Items.FindAll<Sequence>("00189407").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PlanesInAcquisition { get { return Items.FindFirst<CodeString>("00189410") as CodeString; } }
        public List<CodeString> PlanesInAcquisition_ { get { return Items.FindAll<CodeString>("00189410").ToList(); } }
        public SequenceSelector XAXRFFrameCharacteristicsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189412")); } }
        public List<SequenceSelector> XAXRFFrameCharacteristicsSequence_ { get { return Items.FindAll<Sequence>("00189412").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FrameAcquisitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189417")); } }
        public List<SequenceSelector> FrameAcquisitionSequence_ { get { return Items.FindAll<Sequence>("00189417").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString XRayReceptorType { get { return Items.FindFirst<CodeString>("00189420") as CodeString; } }
        public List<CodeString> XRayReceptorType_ { get { return Items.FindAll<CodeString>("00189420").ToList(); } }
        public LongString AcquisitionProtocolName { get { return Items.FindFirst<LongString>("00189423") as LongString; } }
        public List<LongString> AcquisitionProtocolName_ { get { return Items.FindAll<LongString>("00189423").ToList(); } }
        public LongText AcquisitionProtocolDescription { get { return Items.FindFirst<LongText>("00189424") as LongText; } }
        public List<LongText> AcquisitionProtocolDescription_ { get { return Items.FindAll<LongText>("00189424").ToList(); } }
        public CodeString ContrastBolusIngredientOpaque { get { return Items.FindFirst<CodeString>("00189425") as CodeString; } }
        public List<CodeString> ContrastBolusIngredientOpaque_ { get { return Items.FindAll<CodeString>("00189425").ToList(); } }
        public FloatingPointSingle DistanceReceptorPlaneToDetectorHousing { get { return Items.FindFirst<FloatingPointSingle>("00189426") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DistanceReceptorPlaneToDetectorHousing_ { get { return Items.FindAll<FloatingPointSingle>("00189426").ToList(); } }
        public CodeString IntensifierActiveShape { get { return Items.FindFirst<CodeString>("00189427") as CodeString; } }
        public List<CodeString> IntensifierActiveShape_ { get { return Items.FindAll<CodeString>("00189427").ToList(); } }
        public FloatingPointSingle IntensifierActiveDimensions { get { return Items.FindFirst<FloatingPointSingle>("00189428") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IntensifierActiveDimensions_ { get { return Items.FindAll<FloatingPointSingle>("00189428").ToList(); } }
        public FloatingPointSingle PhysicalDetectorSize { get { return Items.FindFirst<FloatingPointSingle>("00189429") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PhysicalDetectorSize_ { get { return Items.FindAll<FloatingPointSingle>("00189429").ToList(); } }
        public FloatingPointSingle PositionOfIsocenterProjection { get { return Items.FindFirst<FloatingPointSingle>("00189430") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PositionOfIsocenterProjection_ { get { return Items.FindAll<FloatingPointSingle>("00189430").ToList(); } }
        public SequenceSelector FieldOfViewSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189432")); } }
        public List<SequenceSelector> FieldOfViewSequence_ { get { return Items.FindAll<Sequence>("00189432").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString FieldOfViewDescription { get { return Items.FindFirst<LongString>("00189433") as LongString; } }
        public List<LongString> FieldOfViewDescription_ { get { return Items.FindAll<LongString>("00189433").ToList(); } }
        public SequenceSelector ExposureControlSensingRegionsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189434")); } }
        public List<SequenceSelector> ExposureControlSensingRegionsSequence_ { get { return Items.FindAll<Sequence>("00189434").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ExposureControlSensingRegionShape { get { return Items.FindFirst<CodeString>("00189435") as CodeString; } }
        public List<CodeString> ExposureControlSensingRegionShape_ { get { return Items.FindAll<CodeString>("00189435").ToList(); } }
        public SignedShort ExposureControlSensingRegionLeftVerticalEdge { get { return Items.FindFirst<SignedShort>("00189436") as SignedShort; } }
        public List<SignedShort> ExposureControlSensingRegionLeftVerticalEdge_ { get { return Items.FindAll<SignedShort>("00189436").ToList(); } }
        public SignedShort ExposureControlSensingRegionRightVerticalEdge { get { return Items.FindFirst<SignedShort>("00189437") as SignedShort; } }
        public List<SignedShort> ExposureControlSensingRegionRightVerticalEdge_ { get { return Items.FindAll<SignedShort>("00189437").ToList(); } }
        public SignedShort ExposureControlSensingRegionUpperHorizontalEdge { get { return Items.FindFirst<SignedShort>("00189438") as SignedShort; } }
        public List<SignedShort> ExposureControlSensingRegionUpperHorizontalEdge_ { get { return Items.FindAll<SignedShort>("00189438").ToList(); } }
        public SignedShort ExposureControlSensingRegionLowerHorizontalEdge { get { return Items.FindFirst<SignedShort>("00189439") as SignedShort; } }
        public List<SignedShort> ExposureControlSensingRegionLowerHorizontalEdge_ { get { return Items.FindAll<SignedShort>("00189439").ToList(); } }
        public SignedShort CenterOfCircularExposureControlSensingRegion { get { return Items.FindFirst<SignedShort>("00189440") as SignedShort; } }
        public List<SignedShort> CenterOfCircularExposureControlSensingRegion_ { get { return Items.FindAll<SignedShort>("00189440").ToList(); } }
        public UnsignedShort RadiusOfCircularExposureControlSensingRegion { get { return Items.FindFirst<UnsignedShort>("00189441") as UnsignedShort; } }
        public List<UnsignedShort> RadiusOfCircularExposureControlSensingRegion_ { get { return Items.FindAll<UnsignedShort>("00189441").ToList(); } }
        public SignedShort VerticesOfThePolygonalExposureControlSensingRegion { get { return Items.FindFirst<SignedShort>("00189442") as SignedShort; } }
        public List<SignedShort> VerticesOfThePolygonalExposureControlSensingRegion_ { get { return Items.FindAll<SignedShort>("00189442").ToList(); } }
        public FloatingPointSingle ColumnAngulationPatient { get { return Items.FindFirst<FloatingPointSingle>("00189447") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ColumnAngulationPatient_ { get { return Items.FindAll<FloatingPointSingle>("00189447").ToList(); } }
        public FloatingPointSingle BeamAngle { get { return Items.FindFirst<FloatingPointSingle>("00189449") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BeamAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189449").ToList(); } }
        public SequenceSelector FrameDetectorParametersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189451")); } }
        public List<SequenceSelector> FrameDetectorParametersSequence_ { get { return Items.FindAll<Sequence>("00189451").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle CalculatedAnatomyThickness { get { return Items.FindFirst<FloatingPointSingle>("00189452") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CalculatedAnatomyThickness_ { get { return Items.FindAll<FloatingPointSingle>("00189452").ToList(); } }
        public SequenceSelector CalibrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189455")); } }
        public List<SequenceSelector> CalibrationSequence_ { get { return Items.FindAll<Sequence>("00189455").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ObjectThicknessSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189456")); } }
        public List<SequenceSelector> ObjectThicknessSequence_ { get { return Items.FindAll<Sequence>("00189456").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PlaneIdentification { get { return Items.FindFirst<CodeString>("00189457") as CodeString; } }
        public List<CodeString> PlaneIdentification_ { get { return Items.FindAll<CodeString>("00189457").ToList(); } }
        public FloatingPointSingle FieldOfViewDimensionsInFloat { get { return Items.FindFirst<FloatingPointSingle>("00189461") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FieldOfViewDimensionsInFloat_ { get { return Items.FindAll<FloatingPointSingle>("00189461").ToList(); } }
        public SequenceSelector IsocenterReferenceSystemSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189462")); } }
        public List<SequenceSelector> IsocenterReferenceSystemSequence_ { get { return Items.FindAll<Sequence>("00189462").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle PositionerIsocenterPrimaryAngle { get { return Items.FindFirst<FloatingPointSingle>("00189463") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PositionerIsocenterPrimaryAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189463").ToList(); } }
        public FloatingPointSingle PositionerIsocenterSecondaryAngle { get { return Items.FindFirst<FloatingPointSingle>("00189464") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PositionerIsocenterSecondaryAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189464").ToList(); } }
        public FloatingPointSingle PositionerIsocenterDetectorRotationAngle { get { return Items.FindFirst<FloatingPointSingle>("00189465") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PositionerIsocenterDetectorRotationAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189465").ToList(); } }
        public FloatingPointSingle TableXPositionToIsocenter { get { return Items.FindFirst<FloatingPointSingle>("00189466") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableXPositionToIsocenter_ { get { return Items.FindAll<FloatingPointSingle>("00189466").ToList(); } }
        public FloatingPointSingle TableYPositionToIsocenter { get { return Items.FindFirst<FloatingPointSingle>("00189467") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableYPositionToIsocenter_ { get { return Items.FindAll<FloatingPointSingle>("00189467").ToList(); } }
        public FloatingPointSingle TableZPositionToIsocenter { get { return Items.FindFirst<FloatingPointSingle>("00189468") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableZPositionToIsocenter_ { get { return Items.FindAll<FloatingPointSingle>("00189468").ToList(); } }
        public FloatingPointSingle TableHorizontalRotationAngle { get { return Items.FindFirst<FloatingPointSingle>("00189469") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableHorizontalRotationAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189469").ToList(); } }
        public FloatingPointSingle TableHeadTiltAngle { get { return Items.FindFirst<FloatingPointSingle>("00189470") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableHeadTiltAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189470").ToList(); } }
        public FloatingPointSingle TableCradleTiltAngle { get { return Items.FindFirst<FloatingPointSingle>("00189471") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableCradleTiltAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189471").ToList(); } }
        public SequenceSelector FrameDisplayShutterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189472")); } }
        public List<SequenceSelector> FrameDisplayShutterSequence_ { get { return Items.FindAll<Sequence>("00189472").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle AcquiredImageAreaDoseProduct { get { return Items.FindFirst<FloatingPointSingle>("00189473") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AcquiredImageAreaDoseProduct_ { get { return Items.FindAll<FloatingPointSingle>("00189473").ToList(); } }
        public CodeString CArmPositionerTabletopRelationship { get { return Items.FindFirst<CodeString>("00189474") as CodeString; } }
        public List<CodeString> CArmPositionerTabletopRelationship_ { get { return Items.FindAll<CodeString>("00189474").ToList(); } }
        public SequenceSelector XRayGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189476")); } }
        public List<SequenceSelector> XRayGeometrySequence_ { get { return Items.FindAll<Sequence>("00189476").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IrradiationEventIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189477")); } }
        public List<SequenceSelector> IrradiationEventIdentificationSequence_ { get { return Items.FindAll<Sequence>("00189477").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector XRay3DFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189504")); } }
        public List<SequenceSelector> XRay3DFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00189504").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContributingSourcesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189506")); } }
        public List<SequenceSelector> ContributingSourcesSequence_ { get { return Items.FindAll<Sequence>("00189506").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector XRay3DAcquisitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189507")); } }
        public List<SequenceSelector> XRay3DAcquisitionSequence_ { get { return Items.FindAll<Sequence>("00189507").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle PrimaryPositionerScanArc { get { return Items.FindFirst<FloatingPointSingle>("00189508") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PrimaryPositionerScanArc_ { get { return Items.FindAll<FloatingPointSingle>("00189508").ToList(); } }
        public FloatingPointSingle SecondaryPositionerScanArc { get { return Items.FindFirst<FloatingPointSingle>("00189509") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SecondaryPositionerScanArc_ { get { return Items.FindAll<FloatingPointSingle>("00189509").ToList(); } }
        public FloatingPointSingle PrimaryPositionerScanStartAngle { get { return Items.FindFirst<FloatingPointSingle>("00189510") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PrimaryPositionerScanStartAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189510").ToList(); } }
        public FloatingPointSingle SecondaryPositionerScanStartAngle { get { return Items.FindFirst<FloatingPointSingle>("00189511") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SecondaryPositionerScanStartAngle_ { get { return Items.FindAll<FloatingPointSingle>("00189511").ToList(); } }
        public FloatingPointSingle PrimaryPositionerIncrement { get { return Items.FindFirst<FloatingPointSingle>("00189514") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PrimaryPositionerIncrement_ { get { return Items.FindAll<FloatingPointSingle>("00189514").ToList(); } }
        public FloatingPointSingle SecondaryPositionerIncrement { get { return Items.FindFirst<FloatingPointSingle>("00189515") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SecondaryPositionerIncrement_ { get { return Items.FindAll<FloatingPointSingle>("00189515").ToList(); } }
        public EvilDICOM.Core.Element.DateTime StartAcquisitionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189516") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> StartAcquisitionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189516").ToList(); } }
        public EvilDICOM.Core.Element.DateTime EndAcquisitionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189517") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> EndAcquisitionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189517").ToList(); } }
        public LongString ApplicationName { get { return Items.FindFirst<LongString>("00189524") as LongString; } }
        public List<LongString> ApplicationName_ { get { return Items.FindAll<LongString>("00189524").ToList(); } }
        public LongString ApplicationVersion { get { return Items.FindFirst<LongString>("00189525") as LongString; } }
        public List<LongString> ApplicationVersion_ { get { return Items.FindAll<LongString>("00189525").ToList(); } }
        public LongString ApplicationManufacturer { get { return Items.FindFirst<LongString>("00189526") as LongString; } }
        public List<LongString> ApplicationManufacturer_ { get { return Items.FindAll<LongString>("00189526").ToList(); } }
        public CodeString AlgorithmType { get { return Items.FindFirst<CodeString>("00189527") as CodeString; } }
        public List<CodeString> AlgorithmType_ { get { return Items.FindAll<CodeString>("00189527").ToList(); } }
        public LongString AlgorithmDescription { get { return Items.FindFirst<LongString>("00189528") as LongString; } }
        public List<LongString> AlgorithmDescription_ { get { return Items.FindAll<LongString>("00189528").ToList(); } }
        public SequenceSelector XRay3DReconstructionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189530")); } }
        public List<SequenceSelector> XRay3DReconstructionSequence_ { get { return Items.FindAll<Sequence>("00189530").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ReconstructionDescription { get { return Items.FindFirst<LongString>("00189531") as LongString; } }
        public List<LongString> ReconstructionDescription_ { get { return Items.FindAll<LongString>("00189531").ToList(); } }
        public SequenceSelector PerProjectionAcquisitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189538")); } }
        public List<SequenceSelector> PerProjectionAcquisitionSequence_ { get { return Items.FindAll<Sequence>("00189538").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DiffusionBMatrixSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189601")); } }
        public List<SequenceSelector> DiffusionBMatrixSequence_ { get { return Items.FindAll<Sequence>("00189601").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble DiffusionBValueXX { get { return Items.FindFirst<FloatingPointDouble>("00189602") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueXX_ { get { return Items.FindAll<FloatingPointDouble>("00189602").ToList(); } }
        public FloatingPointDouble DiffusionBValueXY { get { return Items.FindFirst<FloatingPointDouble>("00189603") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueXY_ { get { return Items.FindAll<FloatingPointDouble>("00189603").ToList(); } }
        public FloatingPointDouble DiffusionBValueXZ { get { return Items.FindFirst<FloatingPointDouble>("00189604") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueXZ_ { get { return Items.FindAll<FloatingPointDouble>("00189604").ToList(); } }
        public FloatingPointDouble DiffusionBValueYY { get { return Items.FindFirst<FloatingPointDouble>("00189605") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueYY_ { get { return Items.FindAll<FloatingPointDouble>("00189605").ToList(); } }
        public FloatingPointDouble DiffusionBValueYZ { get { return Items.FindFirst<FloatingPointDouble>("00189606") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueYZ_ { get { return Items.FindAll<FloatingPointDouble>("00189606").ToList(); } }
        public FloatingPointDouble DiffusionBValueZZ { get { return Items.FindFirst<FloatingPointDouble>("00189607") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DiffusionBValueZZ_ { get { return Items.FindAll<FloatingPointDouble>("00189607").ToList(); } }
        public EvilDICOM.Core.Element.DateTime DecayCorrectionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189701") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> DecayCorrectionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189701").ToList(); } }
        public FloatingPointDouble StartDensityThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189715") as FloatingPointDouble; } }
        public List<FloatingPointDouble> StartDensityThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189715").ToList(); } }
        public FloatingPointDouble StartRelativeDensityDifferenceThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189716") as FloatingPointDouble; } }
        public List<FloatingPointDouble> StartRelativeDensityDifferenceThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189716").ToList(); } }
        public FloatingPointDouble StartCardiacTriggerCountThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189717") as FloatingPointDouble; } }
        public List<FloatingPointDouble> StartCardiacTriggerCountThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189717").ToList(); } }
        public FloatingPointDouble StartRespiratoryTriggerCountThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189718") as FloatingPointDouble; } }
        public List<FloatingPointDouble> StartRespiratoryTriggerCountThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189718").ToList(); } }
        public FloatingPointDouble TerminationCountsThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189719") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationCountsThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189719").ToList(); } }
        public FloatingPointDouble TerminationDensityThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189720") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationDensityThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189720").ToList(); } }
        public FloatingPointDouble TerminationRelativeDensityThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189721") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationRelativeDensityThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189721").ToList(); } }
        public FloatingPointDouble TerminationTimeThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189722") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationTimeThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189722").ToList(); } }
        public FloatingPointDouble TerminationCardiacTriggerCountThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189723") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationCardiacTriggerCountThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189723").ToList(); } }
        public FloatingPointDouble TerminationRespiratoryTriggerCountThreshold { get { return Items.FindFirst<FloatingPointDouble>("00189724") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TerminationRespiratoryTriggerCountThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00189724").ToList(); } }
        public CodeString DetectorGeometry { get { return Items.FindFirst<CodeString>("00189725") as CodeString; } }
        public List<CodeString> DetectorGeometry_ { get { return Items.FindAll<CodeString>("00189725").ToList(); } }
        public FloatingPointDouble TransverseDetectorSeparation { get { return Items.FindFirst<FloatingPointDouble>("00189726") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TransverseDetectorSeparation_ { get { return Items.FindAll<FloatingPointDouble>("00189726").ToList(); } }
        public FloatingPointDouble AxialDetectorDimension { get { return Items.FindFirst<FloatingPointDouble>("00189727") as FloatingPointDouble; } }
        public List<FloatingPointDouble> AxialDetectorDimension_ { get { return Items.FindAll<FloatingPointDouble>("00189727").ToList(); } }
        public UnsignedShort RadiopharmaceuticalAgentNumber { get { return Items.FindFirst<UnsignedShort>("00189729") as UnsignedShort; } }
        public List<UnsignedShort> RadiopharmaceuticalAgentNumber_ { get { return Items.FindAll<UnsignedShort>("00189729").ToList(); } }
        public SequenceSelector PETFrameAcquisitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189732")); } }
        public List<SequenceSelector> PETFrameAcquisitionSequence_ { get { return Items.FindAll<Sequence>("00189732").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PETDetectorMotionDetailsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189733")); } }
        public List<SequenceSelector> PETDetectorMotionDetailsSequence_ { get { return Items.FindAll<Sequence>("00189733").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PETTableDynamicsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189734")); } }
        public List<SequenceSelector> PETTableDynamicsSequence_ { get { return Items.FindAll<Sequence>("00189734").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PETPositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189735")); } }
        public List<SequenceSelector> PETPositionSequence_ { get { return Items.FindAll<Sequence>("00189735").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PETFrameCorrectionFactorsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189736")); } }
        public List<SequenceSelector> PETFrameCorrectionFactorsSequence_ { get { return Items.FindAll<Sequence>("00189736").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RadiopharmaceuticalUsageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189737")); } }
        public List<SequenceSelector> RadiopharmaceuticalUsageSequence_ { get { return Items.FindAll<Sequence>("00189737").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString AttenuationCorrectionSource { get { return Items.FindFirst<CodeString>("00189738") as CodeString; } }
        public List<CodeString> AttenuationCorrectionSource_ { get { return Items.FindAll<CodeString>("00189738").ToList(); } }
        public UnsignedShort NumberOfIterations { get { return Items.FindFirst<UnsignedShort>("00189739") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfIterations_ { get { return Items.FindAll<UnsignedShort>("00189739").ToList(); } }
        public UnsignedShort NumberOfSubsets { get { return Items.FindFirst<UnsignedShort>("00189740") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfSubsets_ { get { return Items.FindAll<UnsignedShort>("00189740").ToList(); } }
        public SequenceSelector PETReconstructionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189749")); } }
        public List<SequenceSelector> PETReconstructionSequence_ { get { return Items.FindAll<Sequence>("00189749").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PETFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189751")); } }
        public List<SequenceSelector> PETFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00189751").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString TimeOfFlightInformationUsed { get { return Items.FindFirst<CodeString>("00189755") as CodeString; } }
        public List<CodeString> TimeOfFlightInformationUsed_ { get { return Items.FindAll<CodeString>("00189755").ToList(); } }
        public CodeString ReconstructionType { get { return Items.FindFirst<CodeString>("00189756") as CodeString; } }
        public List<CodeString> ReconstructionType_ { get { return Items.FindAll<CodeString>("00189756").ToList(); } }
        public CodeString DecayCorrected { get { return Items.FindFirst<CodeString>("00189758") as CodeString; } }
        public List<CodeString> DecayCorrected_ { get { return Items.FindAll<CodeString>("00189758").ToList(); } }
        public CodeString AttenuationCorrected { get { return Items.FindFirst<CodeString>("00189759") as CodeString; } }
        public List<CodeString> AttenuationCorrected_ { get { return Items.FindAll<CodeString>("00189759").ToList(); } }
        public CodeString ScatterCorrected { get { return Items.FindFirst<CodeString>("00189760") as CodeString; } }
        public List<CodeString> ScatterCorrected_ { get { return Items.FindAll<CodeString>("00189760").ToList(); } }
        public CodeString DeadTimeCorrected { get { return Items.FindFirst<CodeString>("00189761") as CodeString; } }
        public List<CodeString> DeadTimeCorrected_ { get { return Items.FindAll<CodeString>("00189761").ToList(); } }
        public CodeString GantryMotionCorrected { get { return Items.FindFirst<CodeString>("00189762") as CodeString; } }
        public List<CodeString> GantryMotionCorrected_ { get { return Items.FindAll<CodeString>("00189762").ToList(); } }
        public CodeString PatientMotionCorrected { get { return Items.FindFirst<CodeString>("00189763") as CodeString; } }
        public List<CodeString> PatientMotionCorrected_ { get { return Items.FindAll<CodeString>("00189763").ToList(); } }
        public CodeString CountLossNormalizationCorrected { get { return Items.FindFirst<CodeString>("00189764") as CodeString; } }
        public List<CodeString> CountLossNormalizationCorrected_ { get { return Items.FindAll<CodeString>("00189764").ToList(); } }
        public CodeString RandomsCorrected { get { return Items.FindFirst<CodeString>("00189765") as CodeString; } }
        public List<CodeString> RandomsCorrected_ { get { return Items.FindAll<CodeString>("00189765").ToList(); } }
        public CodeString NonUniformRadialSamplingCorrected { get { return Items.FindFirst<CodeString>("00189766") as CodeString; } }
        public List<CodeString> NonUniformRadialSamplingCorrected_ { get { return Items.FindAll<CodeString>("00189766").ToList(); } }
        public CodeString SensitivityCalibrated { get { return Items.FindFirst<CodeString>("00189767") as CodeString; } }
        public List<CodeString> SensitivityCalibrated_ { get { return Items.FindAll<CodeString>("00189767").ToList(); } }
        public CodeString DetectorNormalizationCorrection { get { return Items.FindFirst<CodeString>("00189768") as CodeString; } }
        public List<CodeString> DetectorNormalizationCorrection_ { get { return Items.FindAll<CodeString>("00189768").ToList(); } }
        public CodeString IterativeReconstructionMethod { get { return Items.FindFirst<CodeString>("00189769") as CodeString; } }
        public List<CodeString> IterativeReconstructionMethod_ { get { return Items.FindAll<CodeString>("00189769").ToList(); } }
        public CodeString AttenuationCorrectionTemporalRelationship { get { return Items.FindFirst<CodeString>("00189770") as CodeString; } }
        public List<CodeString> AttenuationCorrectionTemporalRelationship_ { get { return Items.FindAll<CodeString>("00189770").ToList(); } }
        public SequenceSelector PatientPhysiologicalStateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189771")); } }
        public List<SequenceSelector> PatientPhysiologicalStateSequence_ { get { return Items.FindAll<Sequence>("00189771").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PatientPhysiologicalStateCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189772")); } }
        public List<SequenceSelector> PatientPhysiologicalStateCodeSequence_ { get { return Items.FindAll<Sequence>("00189772").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble DepthsOfFocus { get { return Items.FindFirst<FloatingPointDouble>("00189801") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DepthsOfFocus_ { get { return Items.FindAll<FloatingPointDouble>("00189801").ToList(); } }
        public SequenceSelector ExcludedIntervalsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189803")); } }
        public List<SequenceSelector> ExcludedIntervalsSequence_ { get { return Items.FindAll<Sequence>("00189803").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime ExclusionStartDatetime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00189804") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ExclusionStartDatetime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00189804").ToList(); } }
        public FloatingPointDouble ExclusionDuration { get { return Items.FindFirst<FloatingPointDouble>("00189805") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ExclusionDuration_ { get { return Items.FindAll<FloatingPointDouble>("00189805").ToList(); } }
        public SequenceSelector USImageDescriptionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189806")); } }
        public List<SequenceSelector> USImageDescriptionSequence_ { get { return Items.FindAll<Sequence>("00189806").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImageDataTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189807")); } }
        public List<SequenceSelector> ImageDataTypeSequence_ { get { return Items.FindAll<Sequence>("00189807").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DataType { get { return Items.FindFirst<CodeString>("00189808") as CodeString; } }
        public List<CodeString> DataType_ { get { return Items.FindAll<CodeString>("00189808").ToList(); } }
        public SequenceSelector TransducerScanPatternCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00189809")); } }
        public List<SequenceSelector> TransducerScanPatternCodeSequence_ { get { return Items.FindAll<Sequence>("00189809").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString AliasedDataType { get { return Items.FindFirst<CodeString>("0018980B") as CodeString; } }
        public List<CodeString> AliasedDataType_ { get { return Items.FindAll<CodeString>("0018980B").ToList(); } }
        public CodeString PositionMeasuringDeviceUsed { get { return Items.FindFirst<CodeString>("0018980C") as CodeString; } }
        public List<CodeString> PositionMeasuringDeviceUsed_ { get { return Items.FindAll<CodeString>("0018980C").ToList(); } }
        public SequenceSelector TransducerGeometryCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018980D")); } }
        public List<SequenceSelector> TransducerGeometryCodeSequence_ { get { return Items.FindAll<Sequence>("0018980D").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerBeamSteeringCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018980E")); } }
        public List<SequenceSelector> TransducerBeamSteeringCodeSequence_ { get { return Items.FindAll<Sequence>("0018980E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TransducerApplicationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018980F")); } }
        public List<SequenceSelector> TransducerApplicationCodeSequence_ { get { return Items.FindAll<Sequence>("0018980F").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContributingEquipmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0018A001")); } }
        public List<SequenceSelector> ContributingEquipmentSequence_ { get { return Items.FindAll<Sequence>("0018A001").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime ContributionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0018A002") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ContributionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0018A002").ToList(); } }
        public ShortText ContributionDescription { get { return Items.FindFirst<ShortText>("0018A003") as ShortText; } }
        public List<ShortText> ContributionDescription_ { get { return Items.FindAll<ShortText>("0018A003").ToList(); } }
        public UniqueIdentifier StudyInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("0020000D") as UniqueIdentifier; } }
        public List<UniqueIdentifier> StudyInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("0020000D").ToList(); } }
        public UniqueIdentifier SeriesInstanceUID { get { return Items.FindFirst<UniqueIdentifier>("0020000E") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SeriesInstanceUID_ { get { return Items.FindAll<UniqueIdentifier>("0020000E").ToList(); } }
        public ShortString StudyID { get { return Items.FindFirst<ShortString>("00200010") as ShortString; } }
        public List<ShortString> StudyID_ { get { return Items.FindAll<ShortString>("00200010").ToList(); } }
        public IntegerString SeriesNumber { get { return Items.FindFirst<IntegerString>("00200011") as IntegerString; } }
        public List<IntegerString> SeriesNumber_ { get { return Items.FindAll<IntegerString>("00200011").ToList(); } }
        public IntegerString AcquisitionNumber { get { return Items.FindFirst<IntegerString>("00200012") as IntegerString; } }
        public List<IntegerString> AcquisitionNumber_ { get { return Items.FindAll<IntegerString>("00200012").ToList(); } }
        public IntegerString InstanceNumber { get { return Items.FindFirst<IntegerString>("00200013") as IntegerString; } }
        public List<IntegerString> InstanceNumber_ { get { return Items.FindAll<IntegerString>("00200013").ToList(); } }
        public IntegerString IsotopeNumberRetired { get { return Items.FindFirst<IntegerString>("00200014") as IntegerString; } }
        public List<IntegerString> IsotopeNumberRetired_ { get { return Items.FindAll<IntegerString>("00200014").ToList(); } }
        public IntegerString PhaseNumberRetired { get { return Items.FindFirst<IntegerString>("00200015") as IntegerString; } }
        public List<IntegerString> PhaseNumberRetired_ { get { return Items.FindAll<IntegerString>("00200015").ToList(); } }
        public IntegerString IntervalNumberRetired { get { return Items.FindFirst<IntegerString>("00200016") as IntegerString; } }
        public List<IntegerString> IntervalNumberRetired_ { get { return Items.FindAll<IntegerString>("00200016").ToList(); } }
        public IntegerString TimeSlotNumberRetired { get { return Items.FindFirst<IntegerString>("00200017") as IntegerString; } }
        public List<IntegerString> TimeSlotNumberRetired_ { get { return Items.FindAll<IntegerString>("00200017").ToList(); } }
        public IntegerString AngleNumberRetired { get { return Items.FindFirst<IntegerString>("00200018") as IntegerString; } }
        public List<IntegerString> AngleNumberRetired_ { get { return Items.FindAll<IntegerString>("00200018").ToList(); } }
        public IntegerString ItemNumber { get { return Items.FindFirst<IntegerString>("00200019") as IntegerString; } }
        public List<IntegerString> ItemNumber_ { get { return Items.FindAll<IntegerString>("00200019").ToList(); } }
        public CodeString PatientOrientation { get { return Items.FindFirst<CodeString>("00200020") as CodeString; } }
        public List<CodeString> PatientOrientation_ { get { return Items.FindAll<CodeString>("00200020").ToList(); } }
        public IntegerString OverlayNumberRetired { get { return Items.FindFirst<IntegerString>("00200022") as IntegerString; } }
        public List<IntegerString> OverlayNumberRetired_ { get { return Items.FindAll<IntegerString>("00200022").ToList(); } }
        public IntegerString CurveNumberRetired { get { return Items.FindFirst<IntegerString>("00200024") as IntegerString; } }
        public List<IntegerString> CurveNumberRetired_ { get { return Items.FindAll<IntegerString>("00200024").ToList(); } }
        public IntegerString LUTNumberRetired { get { return Items.FindFirst<IntegerString>("00200026") as IntegerString; } }
        public List<IntegerString> LUTNumberRetired_ { get { return Items.FindAll<IntegerString>("00200026").ToList(); } }
        public DecimalString ImagePositionRetired { get { return Items.FindFirst<DecimalString>("00200030") as DecimalString; } }
        public List<DecimalString> ImagePositionRetired_ { get { return Items.FindAll<DecimalString>("00200030").ToList(); } }
        public DecimalString ImagePositionPatient { get { return Items.FindFirst<DecimalString>("00200032") as DecimalString; } }
        public List<DecimalString> ImagePositionPatient_ { get { return Items.FindAll<DecimalString>("00200032").ToList(); } }
        public DecimalString ImageOrientationRetired { get { return Items.FindFirst<DecimalString>("00200035") as DecimalString; } }
        public List<DecimalString> ImageOrientationRetired_ { get { return Items.FindAll<DecimalString>("00200035").ToList(); } }
        public DecimalString ImageOrientationPatient { get { return Items.FindFirst<DecimalString>("00200037") as DecimalString; } }
        public List<DecimalString> ImageOrientationPatient_ { get { return Items.FindAll<DecimalString>("00200037").ToList(); } }
        public DecimalString LocationRetired { get { return Items.FindFirst<DecimalString>("00200050") as DecimalString; } }
        public List<DecimalString> LocationRetired_ { get { return Items.FindAll<DecimalString>("00200050").ToList(); } }
        public UniqueIdentifier FrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("00200052") as UniqueIdentifier; } }
        public List<UniqueIdentifier> FrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("00200052").ToList(); } }
        public CodeString Laterality { get { return Items.FindFirst<CodeString>("00200060") as CodeString; } }
        public List<CodeString> Laterality_ { get { return Items.FindAll<CodeString>("00200060").ToList(); } }
        public CodeString ImageLaterality { get { return Items.FindFirst<CodeString>("00200062") as CodeString; } }
        public List<CodeString> ImageLaterality_ { get { return Items.FindAll<CodeString>("00200062").ToList(); } }
        public LongString ImageGeometryTypeRetired { get { return Items.FindFirst<LongString>("00200070") as LongString; } }
        public List<LongString> ImageGeometryTypeRetired_ { get { return Items.FindAll<LongString>("00200070").ToList(); } }
        public CodeString MaskingImageRetired { get { return Items.FindFirst<CodeString>("00200080") as CodeString; } }
        public List<CodeString> MaskingImageRetired_ { get { return Items.FindAll<CodeString>("00200080").ToList(); } }
        public IntegerString ReportNumberRetired { get { return Items.FindFirst<IntegerString>("002000AA") as IntegerString; } }
        public List<IntegerString> ReportNumberRetired_ { get { return Items.FindAll<IntegerString>("002000AA").ToList(); } }
        public IntegerString TemporalPositionIdentifier { get { return Items.FindFirst<IntegerString>("00200100") as IntegerString; } }
        public List<IntegerString> TemporalPositionIdentifier_ { get { return Items.FindAll<IntegerString>("00200100").ToList(); } }
        public IntegerString NumberOfTemporalPositions { get { return Items.FindFirst<IntegerString>("00200105") as IntegerString; } }
        public List<IntegerString> NumberOfTemporalPositions_ { get { return Items.FindAll<IntegerString>("00200105").ToList(); } }
        public DecimalString TemporalResolution { get { return Items.FindFirst<DecimalString>("00200110") as DecimalString; } }
        public List<DecimalString> TemporalResolution_ { get { return Items.FindAll<DecimalString>("00200110").ToList(); } }
        public UniqueIdentifier SynchronizationFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("00200200") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SynchronizationFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("00200200").ToList(); } }
        public UniqueIdentifier SOPInstanceUIDOfConcatenationSource { get { return Items.FindFirst<UniqueIdentifier>("00200242") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SOPInstanceUIDOfConcatenationSource_ { get { return Items.FindAll<UniqueIdentifier>("00200242").ToList(); } }
        public IntegerString SeriesInStudyRetired { get { return Items.FindFirst<IntegerString>("00201000") as IntegerString; } }
        public List<IntegerString> SeriesInStudyRetired_ { get { return Items.FindAll<IntegerString>("00201000").ToList(); } }
        public IntegerString AcquisitionsInSeriesRetired { get { return Items.FindFirst<IntegerString>("00201001") as IntegerString; } }
        public List<IntegerString> AcquisitionsInSeriesRetired_ { get { return Items.FindAll<IntegerString>("00201001").ToList(); } }
        public IntegerString ImagesInAcquisition { get { return Items.FindFirst<IntegerString>("00201002") as IntegerString; } }
        public List<IntegerString> ImagesInAcquisition_ { get { return Items.FindAll<IntegerString>("00201002").ToList(); } }
        public IntegerString ImagesInSeriesRetired { get { return Items.FindFirst<IntegerString>("00201003") as IntegerString; } }
        public List<IntegerString> ImagesInSeriesRetired_ { get { return Items.FindAll<IntegerString>("00201003").ToList(); } }
        public IntegerString AcquisitionsInStudyRetired { get { return Items.FindFirst<IntegerString>("00201004") as IntegerString; } }
        public List<IntegerString> AcquisitionsInStudyRetired_ { get { return Items.FindAll<IntegerString>("00201004").ToList(); } }
        public IntegerString ImagesInStudyRetired { get { return Items.FindFirst<IntegerString>("00201005") as IntegerString; } }
        public List<IntegerString> ImagesInStudyRetired_ { get { return Items.FindAll<IntegerString>("00201005").ToList(); } }
        public LongString ReferenceRetired { get { return Items.FindFirst<LongString>("00201020") as LongString; } }
        public List<LongString> ReferenceRetired_ { get { return Items.FindAll<LongString>("00201020").ToList(); } }
        public LongString PositionReferenceIndicator { get { return Items.FindFirst<LongString>("00201040") as LongString; } }
        public List<LongString> PositionReferenceIndicator_ { get { return Items.FindAll<LongString>("00201040").ToList(); } }
        public DecimalString SliceLocation { get { return Items.FindFirst<DecimalString>("00201041") as DecimalString; } }
        public List<DecimalString> SliceLocation_ { get { return Items.FindAll<DecimalString>("00201041").ToList(); } }
        public IntegerString OtherStudyNumbersRetired { get { return Items.FindFirst<IntegerString>("00201070") as IntegerString; } }
        public List<IntegerString> OtherStudyNumbersRetired_ { get { return Items.FindAll<IntegerString>("00201070").ToList(); } }
        public IntegerString NumberOfPatientRelatedStudies { get { return Items.FindFirst<IntegerString>("00201200") as IntegerString; } }
        public List<IntegerString> NumberOfPatientRelatedStudies_ { get { return Items.FindAll<IntegerString>("00201200").ToList(); } }
        public IntegerString NumberOfPatientRelatedSeries { get { return Items.FindFirst<IntegerString>("00201202") as IntegerString; } }
        public List<IntegerString> NumberOfPatientRelatedSeries_ { get { return Items.FindAll<IntegerString>("00201202").ToList(); } }
        public IntegerString NumberOfPatientRelatedInstances { get { return Items.FindFirst<IntegerString>("00201204") as IntegerString; } }
        public List<IntegerString> NumberOfPatientRelatedInstances_ { get { return Items.FindAll<IntegerString>("00201204").ToList(); } }
        public IntegerString NumberOfStudyRelatedSeries { get { return Items.FindFirst<IntegerString>("00201206") as IntegerString; } }
        public List<IntegerString> NumberOfStudyRelatedSeries_ { get { return Items.FindAll<IntegerString>("00201206").ToList(); } }
        public IntegerString NumberOfStudyRelatedInstances { get { return Items.FindFirst<IntegerString>("00201208") as IntegerString; } }
        public List<IntegerString> NumberOfStudyRelatedInstances_ { get { return Items.FindAll<IntegerString>("00201208").ToList(); } }
        public IntegerString NumberOfSeriesRelatedInstances { get { return Items.FindFirst<IntegerString>("00201209") as IntegerString; } }
        public List<IntegerString> NumberOfSeriesRelatedInstances_ { get { return Items.FindAll<IntegerString>("00201209").ToList(); } }
        public CodeString SourceImageIDsRetired { get { return Items.FindFirst<CodeString>("002031xx") as CodeString; } }
        public List<CodeString> SourceImageIDsRetired_ { get { return Items.FindAll<CodeString>("002031xx").ToList(); } }
        public CodeString ModifyingDeviceIDRetired { get { return Items.FindFirst<CodeString>("00203401") as CodeString; } }
        public List<CodeString> ModifyingDeviceIDRetired_ { get { return Items.FindAll<CodeString>("00203401").ToList(); } }
        public CodeString ModifiedImageIDRetired { get { return Items.FindFirst<CodeString>("00203402") as CodeString; } }
        public List<CodeString> ModifiedImageIDRetired_ { get { return Items.FindAll<CodeString>("00203402").ToList(); } }
        public Date ModifiedImageDateRetired { get { return Items.FindFirst<Date>("00203403") as Date; } }
        public List<Date> ModifiedImageDateRetired_ { get { return Items.FindAll<Date>("00203403").ToList(); } }
        public LongString ModifyingDeviceManufacturerRetired { get { return Items.FindFirst<LongString>("00203404") as LongString; } }
        public List<LongString> ModifyingDeviceManufacturerRetired_ { get { return Items.FindAll<LongString>("00203404").ToList(); } }
        public Time ModifiedImageTimeRetired { get { return Items.FindFirst<Time>("00203405") as Time; } }
        public List<Time> ModifiedImageTimeRetired_ { get { return Items.FindAll<Time>("00203405").ToList(); } }
        public LongString ModifiedImageDescriptionRetired { get { return Items.FindFirst<LongString>("00203406") as LongString; } }
        public List<LongString> ModifiedImageDescriptionRetired_ { get { return Items.FindAll<LongString>("00203406").ToList(); } }
        public LongText ImageComments { get { return Items.FindFirst<LongText>("00204000") as LongText; } }
        public List<LongText> ImageComments_ { get { return Items.FindAll<LongText>("00204000").ToList(); } }
        public AttributeTag OriginalImageIdentificationRetired { get { return Items.FindFirst<AttributeTag>("00205000") as AttributeTag; } }
        public List<AttributeTag> OriginalImageIdentificationRetired_ { get { return Items.FindAll<AttributeTag>("00205000").ToList(); } }
        public LongString OriginalImageIdentificationNomenclatureRetired { get { return Items.FindFirst<LongString>("00205002") as LongString; } }
        public List<LongString> OriginalImageIdentificationNomenclatureRetired_ { get { return Items.FindAll<LongString>("00205002").ToList(); } }
        public ShortString StackID { get { return Items.FindFirst<ShortString>("00209056") as ShortString; } }
        public List<ShortString> StackID_ { get { return Items.FindAll<ShortString>("00209056").ToList(); } }
        public UnsignedLong InStackPositionNumber { get { return Items.FindFirst<UnsignedLong>("00209057") as UnsignedLong; } }
        public List<UnsignedLong> InStackPositionNumber_ { get { return Items.FindAll<UnsignedLong>("00209057").ToList(); } }
        public SequenceSelector FrameAnatomySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209071")); } }
        public List<SequenceSelector> FrameAnatomySequence_ { get { return Items.FindAll<Sequence>("00209071").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FrameLaterality { get { return Items.FindFirst<CodeString>("00209072") as CodeString; } }
        public List<CodeString> FrameLaterality_ { get { return Items.FindAll<CodeString>("00209072").ToList(); } }
        public SequenceSelector FrameContentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209111")); } }
        public List<SequenceSelector> FrameContentSequence_ { get { return Items.FindAll<Sequence>("00209111").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlanePositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209113")); } }
        public List<SequenceSelector> PlanePositionSequence_ { get { return Items.FindAll<Sequence>("00209113").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlaneOrientationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209116")); } }
        public List<SequenceSelector> PlaneOrientationSequence_ { get { return Items.FindAll<Sequence>("00209116").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong TemporalPositionIndex { get { return Items.FindFirst<UnsignedLong>("00209128") as UnsignedLong; } }
        public List<UnsignedLong> TemporalPositionIndex_ { get { return Items.FindAll<UnsignedLong>("00209128").ToList(); } }
        public FloatingPointDouble NominalCardiacTriggerDelayTime { get { return Items.FindFirst<FloatingPointDouble>("00209153") as FloatingPointDouble; } }
        public List<FloatingPointDouble> NominalCardiacTriggerDelayTime_ { get { return Items.FindAll<FloatingPointDouble>("00209153").ToList(); } }
        public FloatingPointSingle NominalCardiacTriggerTimePriorToRPeak { get { return Items.FindFirst<FloatingPointSingle>("00209154") as FloatingPointSingle; } }
        public List<FloatingPointSingle> NominalCardiacTriggerTimePriorToRPeak_ { get { return Items.FindAll<FloatingPointSingle>("00209154").ToList(); } }
        public FloatingPointSingle ActualCardiacTriggerTimePriorToRPeak { get { return Items.FindFirst<FloatingPointSingle>("00209155") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ActualCardiacTriggerTimePriorToRPeak_ { get { return Items.FindAll<FloatingPointSingle>("00209155").ToList(); } }
        public UnsignedShort FrameAcquisitionNumber { get { return Items.FindFirst<UnsignedShort>("00209156") as UnsignedShort; } }
        public List<UnsignedShort> FrameAcquisitionNumber_ { get { return Items.FindAll<UnsignedShort>("00209156").ToList(); } }
        public UnsignedLong DimensionIndexValues { get { return Items.FindFirst<UnsignedLong>("00209157") as UnsignedLong; } }
        public List<UnsignedLong> DimensionIndexValues_ { get { return Items.FindAll<UnsignedLong>("00209157").ToList(); } }
        public LongText FrameComments { get { return Items.FindFirst<LongText>("00209158") as LongText; } }
        public List<LongText> FrameComments_ { get { return Items.FindAll<LongText>("00209158").ToList(); } }
        public UniqueIdentifier ConcatenationUID { get { return Items.FindFirst<UniqueIdentifier>("00209161") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ConcatenationUID_ { get { return Items.FindAll<UniqueIdentifier>("00209161").ToList(); } }
        public UnsignedShort InConcatenationNumber { get { return Items.FindFirst<UnsignedShort>("00209162") as UnsignedShort; } }
        public List<UnsignedShort> InConcatenationNumber_ { get { return Items.FindAll<UnsignedShort>("00209162").ToList(); } }
        public UnsignedShort InConcatenationTotalNumber { get { return Items.FindFirst<UnsignedShort>("00209163") as UnsignedShort; } }
        public List<UnsignedShort> InConcatenationTotalNumber_ { get { return Items.FindAll<UnsignedShort>("00209163").ToList(); } }
        public UniqueIdentifier DimensionOrganizationUID { get { return Items.FindFirst<UniqueIdentifier>("00209164") as UniqueIdentifier; } }
        public List<UniqueIdentifier> DimensionOrganizationUID_ { get { return Items.FindAll<UniqueIdentifier>("00209164").ToList(); } }
        public AttributeTag DimensionIndexPointer { get { return Items.FindFirst<AttributeTag>("00209165") as AttributeTag; } }
        public List<AttributeTag> DimensionIndexPointer_ { get { return Items.FindAll<AttributeTag>("00209165").ToList(); } }
        public AttributeTag FunctionalGroupPointer { get { return Items.FindFirst<AttributeTag>("00209167") as AttributeTag; } }
        public List<AttributeTag> FunctionalGroupPointer_ { get { return Items.FindAll<AttributeTag>("00209167").ToList(); } }
        public LongString DimensionIndexPrivateCreator { get { return Items.FindFirst<LongString>("00209213") as LongString; } }
        public List<LongString> DimensionIndexPrivateCreator_ { get { return Items.FindAll<LongString>("00209213").ToList(); } }
        public SequenceSelector DimensionOrganizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209221")); } }
        public List<SequenceSelector> DimensionOrganizationSequence_ { get { return Items.FindAll<Sequence>("00209221").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DimensionIndexSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209222")); } }
        public List<SequenceSelector> DimensionIndexSequence_ { get { return Items.FindAll<Sequence>("00209222").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong ConcatenationFrameOffsetNumber { get { return Items.FindFirst<UnsignedLong>("00209228") as UnsignedLong; } }
        public List<UnsignedLong> ConcatenationFrameOffsetNumber_ { get { return Items.FindAll<UnsignedLong>("00209228").ToList(); } }
        public LongString FunctionalGroupPrivateCreator { get { return Items.FindFirst<LongString>("00209238") as LongString; } }
        public List<LongString> FunctionalGroupPrivateCreator_ { get { return Items.FindAll<LongString>("00209238").ToList(); } }
        public FloatingPointSingle NominalPercentageOfCardiacPhase { get { return Items.FindFirst<FloatingPointSingle>("00209241") as FloatingPointSingle; } }
        public List<FloatingPointSingle> NominalPercentageOfCardiacPhase_ { get { return Items.FindAll<FloatingPointSingle>("00209241").ToList(); } }
        public FloatingPointSingle NominalPercentageOfRespiratoryPhase { get { return Items.FindFirst<FloatingPointSingle>("00209245") as FloatingPointSingle; } }
        public List<FloatingPointSingle> NominalPercentageOfRespiratoryPhase_ { get { return Items.FindAll<FloatingPointSingle>("00209245").ToList(); } }
        public FloatingPointSingle StartingRespiratoryAmplitude { get { return Items.FindFirst<FloatingPointSingle>("00209246") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StartingRespiratoryAmplitude_ { get { return Items.FindAll<FloatingPointSingle>("00209246").ToList(); } }
        public CodeString StartingRespiratoryPhase { get { return Items.FindFirst<CodeString>("00209247") as CodeString; } }
        public List<CodeString> StartingRespiratoryPhase_ { get { return Items.FindAll<CodeString>("00209247").ToList(); } }
        public FloatingPointSingle EndingRespiratoryAmplitude { get { return Items.FindFirst<FloatingPointSingle>("00209248") as FloatingPointSingle; } }
        public List<FloatingPointSingle> EndingRespiratoryAmplitude_ { get { return Items.FindAll<FloatingPointSingle>("00209248").ToList(); } }
        public CodeString EndingRespiratoryPhase { get { return Items.FindFirst<CodeString>("00209249") as CodeString; } }
        public List<CodeString> EndingRespiratoryPhase_ { get { return Items.FindAll<CodeString>("00209249").ToList(); } }
        public CodeString RespiratoryTriggerType { get { return Items.FindFirst<CodeString>("00209250") as CodeString; } }
        public List<CodeString> RespiratoryTriggerType_ { get { return Items.FindAll<CodeString>("00209250").ToList(); } }
        public FloatingPointDouble RRIntervalTimeNominal { get { return Items.FindFirst<FloatingPointDouble>("00209251") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RRIntervalTimeNominal_ { get { return Items.FindAll<FloatingPointDouble>("00209251").ToList(); } }
        public FloatingPointDouble ActualCardiacTriggerDelayTime { get { return Items.FindFirst<FloatingPointDouble>("00209252") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ActualCardiacTriggerDelayTime_ { get { return Items.FindAll<FloatingPointDouble>("00209252").ToList(); } }
        public SequenceSelector RespiratorySynchronizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209253")); } }
        public List<SequenceSelector> RespiratorySynchronizationSequence_ { get { return Items.FindAll<Sequence>("00209253").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble RespiratoryIntervalTime { get { return Items.FindFirst<FloatingPointDouble>("00209254") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RespiratoryIntervalTime_ { get { return Items.FindAll<FloatingPointDouble>("00209254").ToList(); } }
        public FloatingPointDouble NominalRespiratoryTriggerDelayTime { get { return Items.FindFirst<FloatingPointDouble>("00209255") as FloatingPointDouble; } }
        public List<FloatingPointDouble> NominalRespiratoryTriggerDelayTime_ { get { return Items.FindAll<FloatingPointDouble>("00209255").ToList(); } }
        public FloatingPointDouble RespiratoryTriggerDelayThreshold { get { return Items.FindFirst<FloatingPointDouble>("00209256") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RespiratoryTriggerDelayThreshold_ { get { return Items.FindAll<FloatingPointDouble>("00209256").ToList(); } }
        public FloatingPointDouble ActualRespiratoryTriggerDelayTime { get { return Items.FindFirst<FloatingPointDouble>("00209257") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ActualRespiratoryTriggerDelayTime_ { get { return Items.FindAll<FloatingPointDouble>("00209257").ToList(); } }
        public FloatingPointDouble ImagePositionVolume { get { return Items.FindFirst<FloatingPointDouble>("00209301") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ImagePositionVolume_ { get { return Items.FindAll<FloatingPointDouble>("00209301").ToList(); } }
        public FloatingPointDouble ImageOrientationVolume { get { return Items.FindFirst<FloatingPointDouble>("00209302") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ImageOrientationVolume_ { get { return Items.FindAll<FloatingPointDouble>("00209302").ToList(); } }
        public CodeString UltrasoundAcquisitionGeometry { get { return Items.FindFirst<CodeString>("00209307") as CodeString; } }
        public List<CodeString> UltrasoundAcquisitionGeometry_ { get { return Items.FindAll<CodeString>("00209307").ToList(); } }
        public FloatingPointDouble ApexPosition { get { return Items.FindFirst<FloatingPointDouble>("00209308") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ApexPosition_ { get { return Items.FindAll<FloatingPointDouble>("00209308").ToList(); } }
        public FloatingPointDouble VolumeToTransducerMappingMatrix { get { return Items.FindFirst<FloatingPointDouble>("00209309") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VolumeToTransducerMappingMatrix_ { get { return Items.FindAll<FloatingPointDouble>("00209309").ToList(); } }
        public FloatingPointDouble VolumeToTableMappingMatrix { get { return Items.FindFirst<FloatingPointDouble>("0020930A") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VolumeToTableMappingMatrix_ { get { return Items.FindAll<FloatingPointDouble>("0020930A").ToList(); } }
        public CodeString PatientFrameOfReferenceSource { get { return Items.FindFirst<CodeString>("0020930C") as CodeString; } }
        public List<CodeString> PatientFrameOfReferenceSource_ { get { return Items.FindAll<CodeString>("0020930C").ToList(); } }
        public FloatingPointDouble TemporalPositionTimeOffset { get { return Items.FindFirst<FloatingPointDouble>("0020930D") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TemporalPositionTimeOffset_ { get { return Items.FindAll<FloatingPointDouble>("0020930D").ToList(); } }
        public SequenceSelector PlanePositionVolumeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0020930E")); } }
        public List<SequenceSelector> PlanePositionVolumeSequence_ { get { return Items.FindAll<Sequence>("0020930E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlaneOrientationVolumeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0020930F")); } }
        public List<SequenceSelector> PlaneOrientationVolumeSequence_ { get { return Items.FindAll<Sequence>("0020930F").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TemporalPositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209310")); } }
        public List<SequenceSelector> TemporalPositionSequence_ { get { return Items.FindAll<Sequence>("00209310").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DimensionOrganizationType { get { return Items.FindFirst<CodeString>("00209311") as CodeString; } }
        public List<CodeString> DimensionOrganizationType_ { get { return Items.FindAll<CodeString>("00209311").ToList(); } }
        public UniqueIdentifier VolumeFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("00209312") as UniqueIdentifier; } }
        public List<UniqueIdentifier> VolumeFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("00209312").ToList(); } }
        public UniqueIdentifier TableFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("00209313") as UniqueIdentifier; } }
        public List<UniqueIdentifier> TableFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("00209313").ToList(); } }
        public LongString DimensionDescriptionLabel { get { return Items.FindFirst<LongString>("00209421") as LongString; } }
        public List<LongString> DimensionDescriptionLabel_ { get { return Items.FindAll<LongString>("00209421").ToList(); } }
        public SequenceSelector PatientOrientationInFrameSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209450")); } }
        public List<SequenceSelector> PatientOrientationInFrameSequence_ { get { return Items.FindAll<Sequence>("00209450").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString FrameLabel { get { return Items.FindFirst<LongString>("00209453") as LongString; } }
        public List<LongString> FrameLabel_ { get { return Items.FindAll<LongString>("00209453").ToList(); } }
        public UnsignedShort AcquisitionIndex { get { return Items.FindFirst<UnsignedShort>("00209518") as UnsignedShort; } }
        public List<UnsignedShort> AcquisitionIndex_ { get { return Items.FindAll<UnsignedShort>("00209518").ToList(); } }
        public SequenceSelector ContributingSOPInstancesReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00209529")); } }
        public List<SequenceSelector> ContributingSOPInstancesReferenceSequence_ { get { return Items.FindAll<Sequence>("00209529").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReconstructionIndex { get { return Items.FindFirst<UnsignedShort>("00209536") as UnsignedShort; } }
        public List<UnsignedShort> ReconstructionIndex_ { get { return Items.FindAll<UnsignedShort>("00209536").ToList(); } }
        public UnsignedShort LightPathFilterPassThroughWavelength { get { return Items.FindFirst<UnsignedShort>("00220001") as UnsignedShort; } }
        public List<UnsignedShort> LightPathFilterPassThroughWavelength_ { get { return Items.FindAll<UnsignedShort>("00220001").ToList(); } }
        public UnsignedShort LightPathFilterPassBand { get { return Items.FindFirst<UnsignedShort>("00220002") as UnsignedShort; } }
        public List<UnsignedShort> LightPathFilterPassBand_ { get { return Items.FindAll<UnsignedShort>("00220002").ToList(); } }
        public UnsignedShort ImagePathFilterPassThroughWavelength { get { return Items.FindFirst<UnsignedShort>("00220003") as UnsignedShort; } }
        public List<UnsignedShort> ImagePathFilterPassThroughWavelength_ { get { return Items.FindAll<UnsignedShort>("00220003").ToList(); } }
        public UnsignedShort ImagePathFilterPassBand { get { return Items.FindFirst<UnsignedShort>("00220004") as UnsignedShort; } }
        public List<UnsignedShort> ImagePathFilterPassBand_ { get { return Items.FindAll<UnsignedShort>("00220004").ToList(); } }
        public CodeString PatientEyeMovementCommanded { get { return Items.FindFirst<CodeString>("00220005") as CodeString; } }
        public List<CodeString> PatientEyeMovementCommanded_ { get { return Items.FindAll<CodeString>("00220005").ToList(); } }
        public SequenceSelector PatientEyeMovementCommandCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220006")); } }
        public List<SequenceSelector> PatientEyeMovementCommandCodeSequence_ { get { return Items.FindAll<Sequence>("00220006").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle SphericalLensPower { get { return Items.FindFirst<FloatingPointSingle>("00220007") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SphericalLensPower_ { get { return Items.FindAll<FloatingPointSingle>("00220007").ToList(); } }
        public FloatingPointSingle CylinderLensPower { get { return Items.FindFirst<FloatingPointSingle>("00220008") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CylinderLensPower_ { get { return Items.FindAll<FloatingPointSingle>("00220008").ToList(); } }
        public FloatingPointSingle CylinderAxis { get { return Items.FindFirst<FloatingPointSingle>("00220009") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CylinderAxis_ { get { return Items.FindAll<FloatingPointSingle>("00220009").ToList(); } }
        public FloatingPointSingle EmmetropicMagnification { get { return Items.FindFirst<FloatingPointSingle>("0022000A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> EmmetropicMagnification_ { get { return Items.FindAll<FloatingPointSingle>("0022000A").ToList(); } }
        public FloatingPointSingle IntraOcularPressure { get { return Items.FindFirst<FloatingPointSingle>("0022000B") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IntraOcularPressure_ { get { return Items.FindAll<FloatingPointSingle>("0022000B").ToList(); } }
        public FloatingPointSingle HorizontalFieldOfView { get { return Items.FindFirst<FloatingPointSingle>("0022000C") as FloatingPointSingle; } }
        public List<FloatingPointSingle> HorizontalFieldOfView_ { get { return Items.FindAll<FloatingPointSingle>("0022000C").ToList(); } }
        public CodeString PupilDilated { get { return Items.FindFirst<CodeString>("0022000D") as CodeString; } }
        public List<CodeString> PupilDilated_ { get { return Items.FindAll<CodeString>("0022000D").ToList(); } }
        public FloatingPointSingle DegreeOfDilation { get { return Items.FindFirst<FloatingPointSingle>("0022000E") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DegreeOfDilation_ { get { return Items.FindAll<FloatingPointSingle>("0022000E").ToList(); } }
        public FloatingPointSingle StereoBaselineAngle { get { return Items.FindFirst<FloatingPointSingle>("00220010") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StereoBaselineAngle_ { get { return Items.FindAll<FloatingPointSingle>("00220010").ToList(); } }
        public FloatingPointSingle StereoBaselineDisplacement { get { return Items.FindFirst<FloatingPointSingle>("00220011") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StereoBaselineDisplacement_ { get { return Items.FindAll<FloatingPointSingle>("00220011").ToList(); } }
        public FloatingPointSingle StereoHorizontalPixelOffset { get { return Items.FindFirst<FloatingPointSingle>("00220012") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StereoHorizontalPixelOffset_ { get { return Items.FindAll<FloatingPointSingle>("00220012").ToList(); } }
        public FloatingPointSingle StereoVerticalPixelOffset { get { return Items.FindFirst<FloatingPointSingle>("00220013") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StereoVerticalPixelOffset_ { get { return Items.FindAll<FloatingPointSingle>("00220013").ToList(); } }
        public FloatingPointSingle StereoRotation { get { return Items.FindFirst<FloatingPointSingle>("00220014") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StereoRotation_ { get { return Items.FindAll<FloatingPointSingle>("00220014").ToList(); } }
        public SequenceSelector AcquisitionDeviceTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220015")); } }
        public List<SequenceSelector> AcquisitionDeviceTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00220015").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IlluminationTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220016")); } }
        public List<SequenceSelector> IlluminationTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00220016").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LightPathFilterTypeStackCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220017")); } }
        public List<SequenceSelector> LightPathFilterTypeStackCodeSequence_ { get { return Items.FindAll<Sequence>("00220017").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImagePathFilterTypeStackCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220018")); } }
        public List<SequenceSelector> ImagePathFilterTypeStackCodeSequence_ { get { return Items.FindAll<Sequence>("00220018").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LensesCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220019")); } }
        public List<SequenceSelector> LensesCodeSequence_ { get { return Items.FindAll<Sequence>("00220019").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ChannelDescriptionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0022001A")); } }
        public List<SequenceSelector> ChannelDescriptionCodeSequence_ { get { return Items.FindAll<Sequence>("0022001A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RefractiveStateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0022001B")); } }
        public List<SequenceSelector> RefractiveStateSequence_ { get { return Items.FindAll<Sequence>("0022001B").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MydriaticAgentCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0022001C")); } }
        public List<SequenceSelector> MydriaticAgentCodeSequence_ { get { return Items.FindAll<Sequence>("0022001C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelativeImagePositionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0022001D")); } }
        public List<SequenceSelector> RelativeImagePositionCodeSequence_ { get { return Items.FindAll<Sequence>("0022001D").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle CameraAngleOfView { get { return Items.FindFirst<FloatingPointSingle>("0022001E") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CameraAngleOfView_ { get { return Items.FindAll<FloatingPointSingle>("0022001E").ToList(); } }
        public SequenceSelector StereoPairsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220020")); } }
        public List<SequenceSelector> StereoPairsSequence_ { get { return Items.FindAll<Sequence>("00220020").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LeftImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220021")); } }
        public List<SequenceSelector> LeftImageSequence_ { get { return Items.FindAll<Sequence>("00220021").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RightImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220022")); } }
        public List<SequenceSelector> RightImageSequence_ { get { return Items.FindAll<Sequence>("00220022").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle AxialLengthOfTheEye { get { return Items.FindFirst<FloatingPointSingle>("00220030") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AxialLengthOfTheEye_ { get { return Items.FindAll<FloatingPointSingle>("00220030").ToList(); } }
        public SequenceSelector OphthalmicFrameLocationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220031")); } }
        public List<SequenceSelector> OphthalmicFrameLocationSequence_ { get { return Items.FindAll<Sequence>("00220031").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle ReferenceCoordinates { get { return Items.FindFirst<FloatingPointSingle>("00220032") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ReferenceCoordinates_ { get { return Items.FindAll<FloatingPointSingle>("00220032").ToList(); } }
        public FloatingPointSingle DepthSpatialResolution { get { return Items.FindFirst<FloatingPointSingle>("00220035") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DepthSpatialResolution_ { get { return Items.FindAll<FloatingPointSingle>("00220035").ToList(); } }
        public FloatingPointSingle MaximumDepthDistortion { get { return Items.FindFirst<FloatingPointSingle>("00220036") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaximumDepthDistortion_ { get { return Items.FindAll<FloatingPointSingle>("00220036").ToList(); } }
        public FloatingPointSingle AlongScanSpatialResolution { get { return Items.FindFirst<FloatingPointSingle>("00220037") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AlongScanSpatialResolution_ { get { return Items.FindAll<FloatingPointSingle>("00220037").ToList(); } }
        public FloatingPointSingle MaximumAlongScanDistortion { get { return Items.FindFirst<FloatingPointSingle>("00220038") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaximumAlongScanDistortion_ { get { return Items.FindAll<FloatingPointSingle>("00220038").ToList(); } }
        public CodeString OphthalmicImageOrientation { get { return Items.FindFirst<CodeString>("00220039") as CodeString; } }
        public List<CodeString> OphthalmicImageOrientation_ { get { return Items.FindAll<CodeString>("00220039").ToList(); } }
        public FloatingPointSingle DepthOfTransverseImage { get { return Items.FindFirst<FloatingPointSingle>("00220041") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DepthOfTransverseImage_ { get { return Items.FindAll<FloatingPointSingle>("00220041").ToList(); } }
        public SequenceSelector MydriaticAgentConcentrationUnitsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220042")); } }
        public List<SequenceSelector> MydriaticAgentConcentrationUnitsSequence_ { get { return Items.FindAll<Sequence>("00220042").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle AcrossScanSpatialResolution { get { return Items.FindFirst<FloatingPointSingle>("00220048") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AcrossScanSpatialResolution_ { get { return Items.FindAll<FloatingPointSingle>("00220048").ToList(); } }
        public FloatingPointSingle MaximumAcrossScanDistortion { get { return Items.FindFirst<FloatingPointSingle>("00220049") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaximumAcrossScanDistortion_ { get { return Items.FindAll<FloatingPointSingle>("00220049").ToList(); } }
        public DecimalString MydriaticAgentConcentration { get { return Items.FindFirst<DecimalString>("0022004E") as DecimalString; } }
        public List<DecimalString> MydriaticAgentConcentration_ { get { return Items.FindAll<DecimalString>("0022004E").ToList(); } }
        public FloatingPointSingle IlluminationWaveLength { get { return Items.FindFirst<FloatingPointSingle>("00220055") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IlluminationWaveLength_ { get { return Items.FindAll<FloatingPointSingle>("00220055").ToList(); } }
        public FloatingPointSingle IlluminationPower { get { return Items.FindFirst<FloatingPointSingle>("00220056") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IlluminationPower_ { get { return Items.FindAll<FloatingPointSingle>("00220056").ToList(); } }
        public FloatingPointSingle IlluminationBandwidth { get { return Items.FindFirst<FloatingPointSingle>("00220057") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IlluminationBandwidth_ { get { return Items.FindAll<FloatingPointSingle>("00220057").ToList(); } }
        public SequenceSelector MydriaticAgentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00220058")); } }
        public List<SequenceSelector> MydriaticAgentSequence_ { get { return Items.FindAll<Sequence>("00220058").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialMeasurementsRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221007")); } }
        public List<SequenceSelector> OphthalmicAxialMeasurementsRightEyeSequence_ { get { return Items.FindAll<Sequence>("00221007").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialMeasurementsLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221008")); } }
        public List<SequenceSelector> OphthalmicAxialMeasurementsLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00221008").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString OphthalmicAxialLengthMeasurementsType { get { return Items.FindFirst<CodeString>("00221010") as CodeString; } }
        public List<CodeString> OphthalmicAxialLengthMeasurementsType_ { get { return Items.FindAll<CodeString>("00221010").ToList(); } }
        public FloatingPointSingle OphthalmicAxialLength { get { return Items.FindFirst<FloatingPointSingle>("00221019") as FloatingPointSingle; } }
        public List<FloatingPointSingle> OphthalmicAxialLength_ { get { return Items.FindAll<FloatingPointSingle>("00221019").ToList(); } }
        public SequenceSelector LensStatusCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221024")); } }
        public List<SequenceSelector> LensStatusCodeSequence_ { get { return Items.FindAll<Sequence>("00221024").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VitreousStatusCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221025")); } }
        public List<SequenceSelector> VitreousStatusCodeSequence_ { get { return Items.FindAll<Sequence>("00221025").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IOLFormulaCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221028")); } }
        public List<SequenceSelector> IOLFormulaCodeSequence_ { get { return Items.FindAll<Sequence>("00221028").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString IOLFormulaDetail { get { return Items.FindFirst<LongString>("00221029") as LongString; } }
        public List<LongString> IOLFormulaDetail_ { get { return Items.FindAll<LongString>("00221029").ToList(); } }
        public FloatingPointSingle KeratometerIndex { get { return Items.FindFirst<FloatingPointSingle>("00221033") as FloatingPointSingle; } }
        public List<FloatingPointSingle> KeratometerIndex_ { get { return Items.FindAll<FloatingPointSingle>("00221033").ToList(); } }
        public SequenceSelector SourceOfOphthalmicAxialLengthCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221035")); } }
        public List<SequenceSelector> SourceOfOphthalmicAxialLengthCodeSequence_ { get { return Items.FindAll<Sequence>("00221035").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle TargetRefraction { get { return Items.FindFirst<FloatingPointSingle>("00221037") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TargetRefraction_ { get { return Items.FindAll<FloatingPointSingle>("00221037").ToList(); } }
        public CodeString RefractiveProcedureOccurred { get { return Items.FindFirst<CodeString>("00221039") as CodeString; } }
        public List<CodeString> RefractiveProcedureOccurred_ { get { return Items.FindAll<CodeString>("00221039").ToList(); } }
        public SequenceSelector RefractiveSurgeryTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221040")); } }
        public List<SequenceSelector> RefractiveSurgeryTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00221040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicUltrasoundAxialMeasurementsTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221044")); } }
        public List<SequenceSelector> OphthalmicUltrasoundAxialMeasurementsTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00221044").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthMeasurementsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221050")); } }
        public List<SequenceSelector> OphthalmicAxialLengthMeasurementsSequence_ { get { return Items.FindAll<Sequence>("00221050").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle IOLPower { get { return Items.FindFirst<FloatingPointSingle>("00221053") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IOLPower_ { get { return Items.FindAll<FloatingPointSingle>("00221053").ToList(); } }
        public FloatingPointSingle PredictedRefractiveError { get { return Items.FindFirst<FloatingPointSingle>("00221054") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PredictedRefractiveError_ { get { return Items.FindAll<FloatingPointSingle>("00221054").ToList(); } }
        public FloatingPointSingle OphthalmicAxialLengthVelocity { get { return Items.FindFirst<FloatingPointSingle>("00221059") as FloatingPointSingle; } }
        public List<FloatingPointSingle> OphthalmicAxialLengthVelocity_ { get { return Items.FindAll<FloatingPointSingle>("00221059").ToList(); } }
        public LongString LensStatusDescription { get { return Items.FindFirst<LongString>("00221065") as LongString; } }
        public List<LongString> LensStatusDescription_ { get { return Items.FindAll<LongString>("00221065").ToList(); } }
        public LongString VitreousStatusDescription { get { return Items.FindFirst<LongString>("00221066") as LongString; } }
        public List<LongString> VitreousStatusDescription_ { get { return Items.FindAll<LongString>("00221066").ToList(); } }
        public SequenceSelector IOLPowerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221090")); } }
        public List<SequenceSelector> IOLPowerSequence_ { get { return Items.FindAll<Sequence>("00221090").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LensConstantSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221092")); } }
        public List<SequenceSelector> LensConstantSequence_ { get { return Items.FindAll<Sequence>("00221092").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString IOLManufacturer { get { return Items.FindFirst<LongString>("00221093") as LongString; } }
        public List<LongString> IOLManufacturer_ { get { return Items.FindAll<LongString>("00221093").ToList(); } }
        public LongString LensConstantDescription { get { return Items.FindFirst<LongString>("00221094") as LongString; } }
        public List<LongString> LensConstantDescription_ { get { return Items.FindAll<LongString>("00221094").ToList(); } }
        public SequenceSelector KeratometryMeasurementTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221096")); } }
        public List<SequenceSelector> KeratometryMeasurementTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00221096").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedOphthalmicAxialMeasurementsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221100")); } }
        public List<SequenceSelector> ReferencedOphthalmicAxialMeasurementsSequence_ { get { return Items.FindAll<Sequence>("00221100").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthMeasurementsSegmentNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221101")); } }
        public List<SequenceSelector> OphthalmicAxialLengthMeasurementsSegmentNameCodeSequence_ { get { return Items.FindAll<Sequence>("00221101").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RefractiveErrorBeforeRefractiveSurgeryCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221103")); } }
        public List<SequenceSelector> RefractiveErrorBeforeRefractiveSurgeryCodeSequence_ { get { return Items.FindAll<Sequence>("00221103").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle IOLPowerForExactEmmetropia { get { return Items.FindFirst<FloatingPointSingle>("00221121") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IOLPowerForExactEmmetropia_ { get { return Items.FindAll<FloatingPointSingle>("00221121").ToList(); } }
        public FloatingPointSingle IOLPowerForExactTargetRefraction { get { return Items.FindFirst<FloatingPointSingle>("00221122") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IOLPowerForExactTargetRefraction_ { get { return Items.FindAll<FloatingPointSingle>("00221122").ToList(); } }
        public SequenceSelector AnteriorChamberDepthDefinitionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221125")); } }
        public List<SequenceSelector> AnteriorChamberDepthDefinitionCodeSequence_ { get { return Items.FindAll<Sequence>("00221125").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle LensThickness { get { return Items.FindFirst<FloatingPointSingle>("00221130") as FloatingPointSingle; } }
        public List<FloatingPointSingle> LensThickness_ { get { return Items.FindAll<FloatingPointSingle>("00221130").ToList(); } }
        public FloatingPointSingle AnteriorChamberDepth { get { return Items.FindFirst<FloatingPointSingle>("00221131") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AnteriorChamberDepth_ { get { return Items.FindAll<FloatingPointSingle>("00221131").ToList(); } }
        public SequenceSelector SourceOfLensThicknessDataCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221132")); } }
        public List<SequenceSelector> SourceOfLensThicknessDataCodeSequence_ { get { return Items.FindAll<Sequence>("00221132").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SourceOfAnteriorChamberDepthDataCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221133")); } }
        public List<SequenceSelector> SourceOfAnteriorChamberDepthDataCodeSequence_ { get { return Items.FindAll<Sequence>("00221133").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SourceOfRefractiveErrorDataCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221135")); } }
        public List<SequenceSelector> SourceOfRefractiveErrorDataCodeSequence_ { get { return Items.FindAll<Sequence>("00221135").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString OphthalmicAxialLengthMeasurementModified { get { return Items.FindFirst<CodeString>("00221140") as CodeString; } }
        public List<CodeString> OphthalmicAxialLengthMeasurementModified_ { get { return Items.FindAll<CodeString>("00221140").ToList(); } }
        public SequenceSelector OphthalmicAxialLengthDataSourceCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221150")); } }
        public List<SequenceSelector> OphthalmicAxialLengthDataSourceCodeSequence_ { get { return Items.FindAll<Sequence>("00221150").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthAcquisitionMethodCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221153")); } }
        public List<SequenceSelector> OphthalmicAxialLengthAcquisitionMethodCodeSequence_ { get { return Items.FindAll<Sequence>("00221153").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle SignalToNoiseRatio { get { return Items.FindFirst<FloatingPointSingle>("00221155") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SignalToNoiseRatio_ { get { return Items.FindAll<FloatingPointSingle>("00221155").ToList(); } }
        public LongString OphthalmicAxialLengthDataSourceDescription { get { return Items.FindFirst<LongString>("00221159") as LongString; } }
        public List<LongString> OphthalmicAxialLengthDataSourceDescription_ { get { return Items.FindAll<LongString>("00221159").ToList(); } }
        public SequenceSelector OphthalmicAxialLengthMeasurementsTotalLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221210")); } }
        public List<SequenceSelector> OphthalmicAxialLengthMeasurementsTotalLengthSequence_ { get { return Items.FindAll<Sequence>("00221210").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthMeasurementsSegmentalLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221211")); } }
        public List<SequenceSelector> OphthalmicAxialLengthMeasurementsSegmentalLengthSequence_ { get { return Items.FindAll<Sequence>("00221211").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthMeasurementsLengthSummationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221212")); } }
        public List<SequenceSelector> OphthalmicAxialLengthMeasurementsLengthSummationSequence_ { get { return Items.FindAll<Sequence>("00221212").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector UltrasoundOphthalmicAxialLengthMeasurementsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221220")); } }
        public List<SequenceSelector> UltrasoundOphthalmicAxialLengthMeasurementsSequence_ { get { return Items.FindAll<Sequence>("00221220").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OpticalOphthalmicAxialLengthMeasurementsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221225")); } }
        public List<SequenceSelector> OpticalOphthalmicAxialLengthMeasurementsSequence_ { get { return Items.FindAll<Sequence>("00221225").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector UltrasoundSelectedOphthalmicAxialLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221230")); } }
        public List<SequenceSelector> UltrasoundSelectedOphthalmicAxialLengthSequence_ { get { return Items.FindAll<Sequence>("00221230").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthSelectionMethodCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221250")); } }
        public List<SequenceSelector> OphthalmicAxialLengthSelectionMethodCodeSequence_ { get { return Items.FindAll<Sequence>("00221250").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OpticalSelectedOphthalmicAxialLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221255")); } }
        public List<SequenceSelector> OpticalSelectedOphthalmicAxialLengthSequence_ { get { return Items.FindAll<Sequence>("00221255").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SelectedSegmentalOphthalmicAxialLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221257")); } }
        public List<SequenceSelector> SelectedSegmentalOphthalmicAxialLengthSequence_ { get { return Items.FindAll<Sequence>("00221257").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SelectedTotalOphthalmicAxialLengthSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221260")); } }
        public List<SequenceSelector> SelectedTotalOphthalmicAxialLengthSequence_ { get { return Items.FindAll<Sequence>("00221260").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicAxialLengthQualityMetricSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221262")); } }
        public List<SequenceSelector> OphthalmicAxialLengthQualityMetricSequence_ { get { return Items.FindAll<Sequence>("00221262").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString OphthalmicAxialLengthQualityMetricTypeDescription { get { return Items.FindFirst<LongString>("00221273") as LongString; } }
        public List<LongString> OphthalmicAxialLengthQualityMetricTypeDescription_ { get { return Items.FindAll<LongString>("00221273").ToList(); } }
        public SequenceSelector IntraocularLensCalculationsRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221300")); } }
        public List<SequenceSelector> IntraocularLensCalculationsRightEyeSequence_ { get { return Items.FindAll<Sequence>("00221300").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IntraocularLensCalculationsLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221310")); } }
        public List<SequenceSelector> IntraocularLensCalculationsLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00221310").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedOphthalmicAxialLengthMeasurementQCImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00221330")); } }
        public List<SequenceSelector> ReferencedOphthalmicAxialLengthMeasurementQCImageSequence_ { get { return Items.FindAll<Sequence>("00221330").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle VisualFieldHorizontalExtent { get { return Items.FindFirst<FloatingPointSingle>("00240010") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldHorizontalExtent_ { get { return Items.FindAll<FloatingPointSingle>("00240010").ToList(); } }
        public FloatingPointSingle VisualFieldVerticalExtent { get { return Items.FindFirst<FloatingPointSingle>("00240011") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldVerticalExtent_ { get { return Items.FindAll<FloatingPointSingle>("00240011").ToList(); } }
        public CodeString VisualFieldShape { get { return Items.FindFirst<CodeString>("00240012") as CodeString; } }
        public List<CodeString> VisualFieldShape_ { get { return Items.FindAll<CodeString>("00240012").ToList(); } }
        public SequenceSelector ScreeningTestModeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240016")); } }
        public List<SequenceSelector> ScreeningTestModeCodeSequence_ { get { return Items.FindAll<Sequence>("00240016").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle MaximumStimulusLuminance { get { return Items.FindFirst<FloatingPointSingle>("00240018") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaximumStimulusLuminance_ { get { return Items.FindAll<FloatingPointSingle>("00240018").ToList(); } }
        public FloatingPointSingle BackgroundLuminance { get { return Items.FindFirst<FloatingPointSingle>("00240020") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BackgroundLuminance_ { get { return Items.FindAll<FloatingPointSingle>("00240020").ToList(); } }
        public SequenceSelector StimulusColorCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240021")); } }
        public List<SequenceSelector> StimulusColorCodeSequence_ { get { return Items.FindAll<Sequence>("00240021").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BackgroundIlluminationColorCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240024")); } }
        public List<SequenceSelector> BackgroundIlluminationColorCodeSequence_ { get { return Items.FindAll<Sequence>("00240024").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle StimulusArea { get { return Items.FindFirst<FloatingPointSingle>("00240025") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StimulusArea_ { get { return Items.FindAll<FloatingPointSingle>("00240025").ToList(); } }
        public FloatingPointSingle StimulusPresentationTime { get { return Items.FindFirst<FloatingPointSingle>("00240028") as FloatingPointSingle; } }
        public List<FloatingPointSingle> StimulusPresentationTime_ { get { return Items.FindAll<FloatingPointSingle>("00240028").ToList(); } }
        public SequenceSelector FixationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240032")); } }
        public List<SequenceSelector> FixationSequence_ { get { return Items.FindAll<Sequence>("00240032").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FixationMonitoringCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240033")); } }
        public List<SequenceSelector> FixationMonitoringCodeSequence_ { get { return Items.FindAll<Sequence>("00240033").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VisualFieldCatchTrialSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240034")); } }
        public List<SequenceSelector> VisualFieldCatchTrialSequence_ { get { return Items.FindAll<Sequence>("00240034").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort FixationCheckedQuantity { get { return Items.FindFirst<UnsignedShort>("00240035") as UnsignedShort; } }
        public List<UnsignedShort> FixationCheckedQuantity_ { get { return Items.FindAll<UnsignedShort>("00240035").ToList(); } }
        public UnsignedShort PatientNotProperlyFixatedQuantity { get { return Items.FindFirst<UnsignedShort>("00240036") as UnsignedShort; } }
        public List<UnsignedShort> PatientNotProperlyFixatedQuantity_ { get { return Items.FindAll<UnsignedShort>("00240036").ToList(); } }
        public CodeString PresentedVisualStimuliDataFlag { get { return Items.FindFirst<CodeString>("00240037") as CodeString; } }
        public List<CodeString> PresentedVisualStimuliDataFlag_ { get { return Items.FindAll<CodeString>("00240037").ToList(); } }
        public UnsignedShort NumberOfVisualStimuli { get { return Items.FindFirst<UnsignedShort>("00240038") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfVisualStimuli_ { get { return Items.FindAll<UnsignedShort>("00240038").ToList(); } }
        public CodeString ExcessiveFixationLossesDataFlag { get { return Items.FindFirst<CodeString>("00240039") as CodeString; } }
        public List<CodeString> ExcessiveFixationLossesDataFlag_ { get { return Items.FindAll<CodeString>("00240039").ToList(); } }
        public CodeString ExcessiveFixationLosses { get { return Items.FindFirst<CodeString>("00240040") as CodeString; } }
        public List<CodeString> ExcessiveFixationLosses_ { get { return Items.FindAll<CodeString>("00240040").ToList(); } }
        public UnsignedShort StimuliRetestingQuantity { get { return Items.FindFirst<UnsignedShort>("00240042") as UnsignedShort; } }
        public List<UnsignedShort> StimuliRetestingQuantity_ { get { return Items.FindAll<UnsignedShort>("00240042").ToList(); } }
        public LongText CommentsOnPatientPerformanceOfVisualField { get { return Items.FindFirst<LongText>("00240044") as LongText; } }
        public List<LongText> CommentsOnPatientPerformanceOfVisualField_ { get { return Items.FindAll<LongText>("00240044").ToList(); } }
        public CodeString FalseNegativesEstimateFlag { get { return Items.FindFirst<CodeString>("00240045") as CodeString; } }
        public List<CodeString> FalseNegativesEstimateFlag_ { get { return Items.FindAll<CodeString>("00240045").ToList(); } }
        public FloatingPointSingle FalseNegativesEstimate { get { return Items.FindFirst<FloatingPointSingle>("00240046") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FalseNegativesEstimate_ { get { return Items.FindAll<FloatingPointSingle>("00240046").ToList(); } }
        public UnsignedShort NegativeCatchTrialsQuantity { get { return Items.FindFirst<UnsignedShort>("00240048") as UnsignedShort; } }
        public List<UnsignedShort> NegativeCatchTrialsQuantity_ { get { return Items.FindAll<UnsignedShort>("00240048").ToList(); } }
        public UnsignedShort FalseNegativesQuantity { get { return Items.FindFirst<UnsignedShort>("00240050") as UnsignedShort; } }
        public List<UnsignedShort> FalseNegativesQuantity_ { get { return Items.FindAll<UnsignedShort>("00240050").ToList(); } }
        public CodeString ExcessiveFalseNegativesDataFlag { get { return Items.FindFirst<CodeString>("00240051") as CodeString; } }
        public List<CodeString> ExcessiveFalseNegativesDataFlag_ { get { return Items.FindAll<CodeString>("00240051").ToList(); } }
        public CodeString ExcessiveFalseNegatives { get { return Items.FindFirst<CodeString>("00240052") as CodeString; } }
        public List<CodeString> ExcessiveFalseNegatives_ { get { return Items.FindAll<CodeString>("00240052").ToList(); } }
        public CodeString FalsePositivesEstimateFlag { get { return Items.FindFirst<CodeString>("00240053") as CodeString; } }
        public List<CodeString> FalsePositivesEstimateFlag_ { get { return Items.FindAll<CodeString>("00240053").ToList(); } }
        public FloatingPointSingle FalsePositivesEstimate { get { return Items.FindFirst<FloatingPointSingle>("00240054") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FalsePositivesEstimate_ { get { return Items.FindAll<FloatingPointSingle>("00240054").ToList(); } }
        public CodeString CatchTrialsDataFlag { get { return Items.FindFirst<CodeString>("00240055") as CodeString; } }
        public List<CodeString> CatchTrialsDataFlag_ { get { return Items.FindAll<CodeString>("00240055").ToList(); } }
        public UnsignedShort PositiveCatchTrialsQuantity { get { return Items.FindFirst<UnsignedShort>("00240056") as UnsignedShort; } }
        public List<UnsignedShort> PositiveCatchTrialsQuantity_ { get { return Items.FindAll<UnsignedShort>("00240056").ToList(); } }
        public CodeString TestPointNormalsDataFlag { get { return Items.FindFirst<CodeString>("00240057") as CodeString; } }
        public List<CodeString> TestPointNormalsDataFlag_ { get { return Items.FindAll<CodeString>("00240057").ToList(); } }
        public SequenceSelector TestPointNormalsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240058")); } }
        public List<SequenceSelector> TestPointNormalsSequence_ { get { return Items.FindAll<Sequence>("00240058").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString GlobalDeviationProbabilityNormalsFlag { get { return Items.FindFirst<CodeString>("00240059") as CodeString; } }
        public List<CodeString> GlobalDeviationProbabilityNormalsFlag_ { get { return Items.FindAll<CodeString>("00240059").ToList(); } }
        public UnsignedShort FalsePositivesQuantity { get { return Items.FindFirst<UnsignedShort>("00240060") as UnsignedShort; } }
        public List<UnsignedShort> FalsePositivesQuantity_ { get { return Items.FindAll<UnsignedShort>("00240060").ToList(); } }
        public CodeString ExcessiveFalsePositivesDataFlag { get { return Items.FindFirst<CodeString>("00240061") as CodeString; } }
        public List<CodeString> ExcessiveFalsePositivesDataFlag_ { get { return Items.FindAll<CodeString>("00240061").ToList(); } }
        public CodeString ExcessiveFalsePositives { get { return Items.FindFirst<CodeString>("00240062") as CodeString; } }
        public List<CodeString> ExcessiveFalsePositives_ { get { return Items.FindAll<CodeString>("00240062").ToList(); } }
        public CodeString VisualFieldTestNormalsFlag { get { return Items.FindFirst<CodeString>("00240063") as CodeString; } }
        public List<CodeString> VisualFieldTestNormalsFlag_ { get { return Items.FindAll<CodeString>("00240063").ToList(); } }
        public SequenceSelector ResultsNormalsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240064")); } }
        public List<SequenceSelector> ResultsNormalsSequence_ { get { return Items.FindAll<Sequence>("00240064").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AgeCorrectedSensitivityDeviationAlgorithmSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240065")); } }
        public List<SequenceSelector> AgeCorrectedSensitivityDeviationAlgorithmSequence_ { get { return Items.FindAll<Sequence>("00240065").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle GlobalDeviationFromNormal { get { return Items.FindFirst<FloatingPointSingle>("00240066") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GlobalDeviationFromNormal_ { get { return Items.FindAll<FloatingPointSingle>("00240066").ToList(); } }
        public SequenceSelector GeneralizedDefectSensitivityDeviationAlgorithmSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240067")); } }
        public List<SequenceSelector> GeneralizedDefectSensitivityDeviationAlgorithmSequence_ { get { return Items.FindAll<Sequence>("00240067").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle LocalizedDeviationfromNormal { get { return Items.FindFirst<FloatingPointSingle>("00240068") as FloatingPointSingle; } }
        public List<FloatingPointSingle> LocalizedDeviationfromNormal_ { get { return Items.FindAll<FloatingPointSingle>("00240068").ToList(); } }
        public LongString PatientReliabilityIndicator { get { return Items.FindFirst<LongString>("00240069") as LongString; } }
        public List<LongString> PatientReliabilityIndicator_ { get { return Items.FindAll<LongString>("00240069").ToList(); } }
        public FloatingPointSingle VisualFieldMeanSensitivity { get { return Items.FindFirst<FloatingPointSingle>("00240070") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldMeanSensitivity_ { get { return Items.FindAll<FloatingPointSingle>("00240070").ToList(); } }
        public FloatingPointSingle GlobalDeviationProbability { get { return Items.FindFirst<FloatingPointSingle>("00240071") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GlobalDeviationProbability_ { get { return Items.FindAll<FloatingPointSingle>("00240071").ToList(); } }
        public CodeString LocalDeviationProbabilityNormalsFlag { get { return Items.FindFirst<CodeString>("00240072") as CodeString; } }
        public List<CodeString> LocalDeviationProbabilityNormalsFlag_ { get { return Items.FindAll<CodeString>("00240072").ToList(); } }
        public FloatingPointSingle LocalizedDeviationProbability { get { return Items.FindFirst<FloatingPointSingle>("00240073") as FloatingPointSingle; } }
        public List<FloatingPointSingle> LocalizedDeviationProbability_ { get { return Items.FindAll<FloatingPointSingle>("00240073").ToList(); } }
        public CodeString ShortTermFluctuationCalculated { get { return Items.FindFirst<CodeString>("00240074") as CodeString; } }
        public List<CodeString> ShortTermFluctuationCalculated_ { get { return Items.FindAll<CodeString>("00240074").ToList(); } }
        public FloatingPointSingle ShortTermFluctuation { get { return Items.FindFirst<FloatingPointSingle>("00240075") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ShortTermFluctuation_ { get { return Items.FindAll<FloatingPointSingle>("00240075").ToList(); } }
        public CodeString ShortTermFluctuationProbabilityCalculated { get { return Items.FindFirst<CodeString>("00240076") as CodeString; } }
        public List<CodeString> ShortTermFluctuationProbabilityCalculated_ { get { return Items.FindAll<CodeString>("00240076").ToList(); } }
        public FloatingPointSingle ShortTermFluctuationProbability { get { return Items.FindFirst<FloatingPointSingle>("00240077") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ShortTermFluctuationProbability_ { get { return Items.FindAll<FloatingPointSingle>("00240077").ToList(); } }
        public CodeString CorrectedLocalizedDeviationFromNormalCalculated { get { return Items.FindFirst<CodeString>("00240078") as CodeString; } }
        public List<CodeString> CorrectedLocalizedDeviationFromNormalCalculated_ { get { return Items.FindAll<CodeString>("00240078").ToList(); } }
        public FloatingPointSingle CorrectedLocalizedDeviationFromNormal { get { return Items.FindFirst<FloatingPointSingle>("00240079") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CorrectedLocalizedDeviationFromNormal_ { get { return Items.FindAll<FloatingPointSingle>("00240079").ToList(); } }
        public CodeString CorrectedLocalizedDeviationFromNormalProbabilityCalculated { get { return Items.FindFirst<CodeString>("00240080") as CodeString; } }
        public List<CodeString> CorrectedLocalizedDeviationFromNormalProbabilityCalculated_ { get { return Items.FindAll<CodeString>("00240080").ToList(); } }
        public FloatingPointSingle CorrectedLocalizedDeviationFromNormalProbability { get { return Items.FindFirst<FloatingPointSingle>("00240081") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CorrectedLocalizedDeviationFromNormalProbability_ { get { return Items.FindAll<FloatingPointSingle>("00240081").ToList(); } }
        public SequenceSelector GlobalDeviationProbabilitySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240083")); } }
        public List<SequenceSelector> GlobalDeviationProbabilitySequence_ { get { return Items.FindAll<Sequence>("00240083").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LocalizedDeviationProbabilitySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240085")); } }
        public List<SequenceSelector> LocalizedDeviationProbabilitySequence_ { get { return Items.FindAll<Sequence>("00240085").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FovealSensitivityMeasured { get { return Items.FindFirst<CodeString>("00240086") as CodeString; } }
        public List<CodeString> FovealSensitivityMeasured_ { get { return Items.FindAll<CodeString>("00240086").ToList(); } }
        public FloatingPointSingle FovealSensitivity { get { return Items.FindFirst<FloatingPointSingle>("00240087") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FovealSensitivity_ { get { return Items.FindAll<FloatingPointSingle>("00240087").ToList(); } }
        public FloatingPointSingle VisualFieldTestDuration { get { return Items.FindFirst<FloatingPointSingle>("00240088") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldTestDuration_ { get { return Items.FindAll<FloatingPointSingle>("00240088").ToList(); } }
        public SequenceSelector VisualFieldTestPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240089")); } }
        public List<SequenceSelector> VisualFieldTestPointSequence_ { get { return Items.FindAll<Sequence>("00240089").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle VisualFieldTestPointXCoordinate { get { return Items.FindFirst<FloatingPointSingle>("00240090") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldTestPointXCoordinate_ { get { return Items.FindAll<FloatingPointSingle>("00240090").ToList(); } }
        public FloatingPointSingle VisualFieldTestPointYCoordinate { get { return Items.FindFirst<FloatingPointSingle>("00240091") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VisualFieldTestPointYCoordinate_ { get { return Items.FindAll<FloatingPointSingle>("00240091").ToList(); } }
        public FloatingPointSingle AgeCorrectedSensitivityDeviationValue { get { return Items.FindFirst<FloatingPointSingle>("00240092") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AgeCorrectedSensitivityDeviationValue_ { get { return Items.FindAll<FloatingPointSingle>("00240092").ToList(); } }
        public CodeString StimulusResults { get { return Items.FindFirst<CodeString>("00240093") as CodeString; } }
        public List<CodeString> StimulusResults_ { get { return Items.FindAll<CodeString>("00240093").ToList(); } }
        public FloatingPointSingle SensitivityValue { get { return Items.FindFirst<FloatingPointSingle>("00240094") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SensitivityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240094").ToList(); } }
        public CodeString RetestStimulusSeen { get { return Items.FindFirst<CodeString>("00240095") as CodeString; } }
        public List<CodeString> RetestStimulusSeen_ { get { return Items.FindAll<CodeString>("00240095").ToList(); } }
        public FloatingPointSingle RetestSensitivityValue { get { return Items.FindFirst<FloatingPointSingle>("00240096") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RetestSensitivityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240096").ToList(); } }
        public SequenceSelector VisualFieldTestPointNormalsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240097")); } }
        public List<SequenceSelector> VisualFieldTestPointNormalsSequence_ { get { return Items.FindAll<Sequence>("00240097").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle QuantifiedDefect { get { return Items.FindFirst<FloatingPointSingle>("00240098") as FloatingPointSingle; } }
        public List<FloatingPointSingle> QuantifiedDefect_ { get { return Items.FindAll<FloatingPointSingle>("00240098").ToList(); } }
        public FloatingPointSingle AgeCorrectedSensitivityDeviationProbabilityValue { get { return Items.FindFirst<FloatingPointSingle>("00240100") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AgeCorrectedSensitivityDeviationProbabilityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240100").ToList(); } }
        public CodeString GeneralizedDefectCorrectedSensitivityDeviationFlag { get { return Items.FindFirst<CodeString>("00240102") as CodeString; } }
        public List<CodeString> GeneralizedDefectCorrectedSensitivityDeviationFlag_ { get { return Items.FindAll<CodeString>("00240102").ToList(); } }
        public FloatingPointSingle GeneralizedDefectCorrectedSensitivityDeviationValue { get { return Items.FindFirst<FloatingPointSingle>("00240103") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GeneralizedDefectCorrectedSensitivityDeviationValue_ { get { return Items.FindAll<FloatingPointSingle>("00240103").ToList(); } }
        public FloatingPointSingle GeneralizedDefectCorrectedSensitivityDeviationProbabilityValue { get { return Items.FindFirst<FloatingPointSingle>("00240104") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GeneralizedDefectCorrectedSensitivityDeviationProbabilityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240104").ToList(); } }
        public FloatingPointSingle MinimumSensitivityValue { get { return Items.FindFirst<FloatingPointSingle>("00240105") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MinimumSensitivityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240105").ToList(); } }
        public CodeString BlindSpotLocalized { get { return Items.FindFirst<CodeString>("00240106") as CodeString; } }
        public List<CodeString> BlindSpotLocalized_ { get { return Items.FindAll<CodeString>("00240106").ToList(); } }
        public FloatingPointSingle BlindSpotXCoordinate { get { return Items.FindFirst<FloatingPointSingle>("00240107") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BlindSpotXCoordinate_ { get { return Items.FindAll<FloatingPointSingle>("00240107").ToList(); } }
        public FloatingPointSingle BlindSpotYCoordinate { get { return Items.FindFirst<FloatingPointSingle>("00240108") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BlindSpotYCoordinate_ { get { return Items.FindAll<FloatingPointSingle>("00240108").ToList(); } }
        public SequenceSelector VisualAcuityMeasurementSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240110")); } }
        public List<SequenceSelector> VisualAcuityMeasurementSequence_ { get { return Items.FindAll<Sequence>("00240110").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RefractiveParametersUsedOnPatientSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240112")); } }
        public List<SequenceSelector> RefractiveParametersUsedOnPatientSequence_ { get { return Items.FindAll<Sequence>("00240112").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString MeasurementLaterality { get { return Items.FindFirst<CodeString>("00240113") as CodeString; } }
        public List<CodeString> MeasurementLaterality_ { get { return Items.FindAll<CodeString>("00240113").ToList(); } }
        public SequenceSelector OphthalmicPatientClinicalInformationLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240114")); } }
        public List<SequenceSelector> OphthalmicPatientClinicalInformationLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00240114").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OphthalmicPatientClinicalInformationRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240115")); } }
        public List<SequenceSelector> OphthalmicPatientClinicalInformationRightEyeSequence_ { get { return Items.FindAll<Sequence>("00240115").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FovealPointNormativeDataFlag { get { return Items.FindFirst<CodeString>("00240117") as CodeString; } }
        public List<CodeString> FovealPointNormativeDataFlag_ { get { return Items.FindAll<CodeString>("00240117").ToList(); } }
        public FloatingPointSingle FovealPointProbabilityValue { get { return Items.FindFirst<FloatingPointSingle>("00240118") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FovealPointProbabilityValue_ { get { return Items.FindAll<FloatingPointSingle>("00240118").ToList(); } }
        public CodeString ScreeningBaselineMeasured { get { return Items.FindFirst<CodeString>("00240120") as CodeString; } }
        public List<CodeString> ScreeningBaselineMeasured_ { get { return Items.FindAll<CodeString>("00240120").ToList(); } }
        public SequenceSelector ScreeningBaselineMeasuredSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240122")); } }
        public List<SequenceSelector> ScreeningBaselineMeasuredSequence_ { get { return Items.FindAll<Sequence>("00240122").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ScreeningBaselineType { get { return Items.FindFirst<CodeString>("00240124") as CodeString; } }
        public List<CodeString> ScreeningBaselineType_ { get { return Items.FindAll<CodeString>("00240124").ToList(); } }
        public FloatingPointSingle ScreeningBaselineValue { get { return Items.FindFirst<FloatingPointSingle>("00240126") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ScreeningBaselineValue_ { get { return Items.FindAll<FloatingPointSingle>("00240126").ToList(); } }
        public LongString AlgorithmSource { get { return Items.FindFirst<LongString>("00240202") as LongString; } }
        public List<LongString> AlgorithmSource_ { get { return Items.FindAll<LongString>("00240202").ToList(); } }
        public LongString DataSetName { get { return Items.FindFirst<LongString>("00240306") as LongString; } }
        public List<LongString> DataSetName_ { get { return Items.FindAll<LongString>("00240306").ToList(); } }
        public LongString DataSetVersion { get { return Items.FindFirst<LongString>("00240307") as LongString; } }
        public List<LongString> DataSetVersion_ { get { return Items.FindAll<LongString>("00240307").ToList(); } }
        public LongString DataSetSource { get { return Items.FindFirst<LongString>("00240308") as LongString; } }
        public List<LongString> DataSetSource_ { get { return Items.FindAll<LongString>("00240308").ToList(); } }
        public LongString DataSetDescription { get { return Items.FindFirst<LongString>("00240309") as LongString; } }
        public List<LongString> DataSetDescription_ { get { return Items.FindAll<LongString>("00240309").ToList(); } }
        public SequenceSelector VisualFieldTestReliabilityGlobalIndexSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240317")); } }
        public List<SequenceSelector> VisualFieldTestReliabilityGlobalIndexSequence_ { get { return Items.FindAll<Sequence>("00240317").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VisualFieldGlobalResultsIndexSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240320")); } }
        public List<SequenceSelector> VisualFieldGlobalResultsIndexSequence_ { get { return Items.FindAll<Sequence>("00240320").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DataObservationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240325")); } }
        public List<SequenceSelector> DataObservationSequence_ { get { return Items.FindAll<Sequence>("00240325").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString IndexNormalsFlag { get { return Items.FindFirst<CodeString>("00240338") as CodeString; } }
        public List<CodeString> IndexNormalsFlag_ { get { return Items.FindAll<CodeString>("00240338").ToList(); } }
        public FloatingPointSingle IndexProbability { get { return Items.FindFirst<FloatingPointSingle>("00240341") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IndexProbability_ { get { return Items.FindAll<FloatingPointSingle>("00240341").ToList(); } }
        public SequenceSelector IndexProbabilitySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00240344")); } }
        public List<SequenceSelector> IndexProbabilitySequence_ { get { return Items.FindAll<Sequence>("00240344").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort SamplesPerPixel { get { return Items.FindFirst<UnsignedShort>("00280002") as UnsignedShort; } }
        public List<UnsignedShort> SamplesPerPixel_ { get { return Items.FindAll<UnsignedShort>("00280002").ToList(); } }
        public UnsignedShort SamplesPerPixelUsed { get { return Items.FindFirst<UnsignedShort>("00280003") as UnsignedShort; } }
        public List<UnsignedShort> SamplesPerPixelUsed_ { get { return Items.FindAll<UnsignedShort>("00280003").ToList(); } }
        public CodeString PhotometricInterpretation { get { return Items.FindFirst<CodeString>("00280004") as CodeString; } }
        public List<CodeString> PhotometricInterpretation_ { get { return Items.FindAll<CodeString>("00280004").ToList(); } }
        public UnsignedShort ImageDimensionsRetired { get { return Items.FindFirst<UnsignedShort>("00280005") as UnsignedShort; } }
        public List<UnsignedShort> ImageDimensionsRetired_ { get { return Items.FindAll<UnsignedShort>("00280005").ToList(); } }
        public UnsignedShort PlanarConfiguration { get { return Items.FindFirst<UnsignedShort>("00280006") as UnsignedShort; } }
        public List<UnsignedShort> PlanarConfiguration_ { get { return Items.FindAll<UnsignedShort>("00280006").ToList(); } }
        public IntegerString NumberOfFrames { get { return Items.FindFirst<IntegerString>("00280008") as IntegerString; } }
        public List<IntegerString> NumberOfFrames_ { get { return Items.FindAll<IntegerString>("00280008").ToList(); } }
        public AttributeTag FrameIncrementPointer { get { return Items.FindFirst<AttributeTag>("00280009") as AttributeTag; } }
        public List<AttributeTag> FrameIncrementPointer_ { get { return Items.FindAll<AttributeTag>("00280009").ToList(); } }
        public AttributeTag FrameDimensionPointer { get { return Items.FindFirst<AttributeTag>("0028000A") as AttributeTag; } }
        public List<AttributeTag> FrameDimensionPointer_ { get { return Items.FindAll<AttributeTag>("0028000A").ToList(); } }
        public UnsignedShort Rows { get { return Items.FindFirst<UnsignedShort>("00280010") as UnsignedShort; } }
        public List<UnsignedShort> Rows_ { get { return Items.FindAll<UnsignedShort>("00280010").ToList(); } }
        public UnsignedShort Columns { get { return Items.FindFirst<UnsignedShort>("00280011") as UnsignedShort; } }
        public List<UnsignedShort> Columns_ { get { return Items.FindAll<UnsignedShort>("00280011").ToList(); } }
        public UnsignedShort PlanesRetired { get { return Items.FindFirst<UnsignedShort>("00280012") as UnsignedShort; } }
        public List<UnsignedShort> PlanesRetired_ { get { return Items.FindAll<UnsignedShort>("00280012").ToList(); } }
        public UnsignedShort UltrasoundColorDataPresent { get { return Items.FindFirst<UnsignedShort>("00280014") as UnsignedShort; } }
        public List<UnsignedShort> UltrasoundColorDataPresent_ { get { return Items.FindAll<UnsignedShort>("00280014").ToList(); } }
        public DecimalString PixelSpacing { get { return Items.FindFirst<DecimalString>("00280030") as DecimalString; } }
        public List<DecimalString> PixelSpacing_ { get { return Items.FindAll<DecimalString>("00280030").ToList(); } }
        public DecimalString ZoomFactor { get { return Items.FindFirst<DecimalString>("00280031") as DecimalString; } }
        public List<DecimalString> ZoomFactor_ { get { return Items.FindAll<DecimalString>("00280031").ToList(); } }
        public DecimalString ZoomCenter { get { return Items.FindFirst<DecimalString>("00280032") as DecimalString; } }
        public List<DecimalString> ZoomCenter_ { get { return Items.FindAll<DecimalString>("00280032").ToList(); } }
        public IntegerString PixelAspectRatio { get { return Items.FindFirst<IntegerString>("00280034") as IntegerString; } }
        public List<IntegerString> PixelAspectRatio_ { get { return Items.FindAll<IntegerString>("00280034").ToList(); } }
        public CodeString ImageFormatRetired { get { return Items.FindFirst<CodeString>("00280040") as CodeString; } }
        public List<CodeString> ImageFormatRetired_ { get { return Items.FindAll<CodeString>("00280040").ToList(); } }
        public LongString ManipulatedImageRetired { get { return Items.FindFirst<LongString>("00280050") as LongString; } }
        public List<LongString> ManipulatedImageRetired_ { get { return Items.FindAll<LongString>("00280050").ToList(); } }
        public CodeString CorrectedImage { get { return Items.FindFirst<CodeString>("00280051") as CodeString; } }
        public List<CodeString> CorrectedImage_ { get { return Items.FindAll<CodeString>("00280051").ToList(); } }
        public LongString CompressionRecognitionCodeRetired { get { return Items.FindFirst<LongString>("0028005F") as LongString; } }
        public List<LongString> CompressionRecognitionCodeRetired_ { get { return Items.FindAll<LongString>("0028005F").ToList(); } }
        public CodeString CompressionCodeRetired { get { return Items.FindFirst<CodeString>("00280060") as CodeString; } }
        public List<CodeString> CompressionCodeRetired_ { get { return Items.FindAll<CodeString>("00280060").ToList(); } }
        public ShortString CompressionOriginatorRetired { get { return Items.FindFirst<ShortString>("00280061") as ShortString; } }
        public List<ShortString> CompressionOriginatorRetired_ { get { return Items.FindAll<ShortString>("00280061").ToList(); } }
        public LongString CompressionLabelRetired { get { return Items.FindFirst<LongString>("00280062") as LongString; } }
        public List<LongString> CompressionLabelRetired_ { get { return Items.FindAll<LongString>("00280062").ToList(); } }
        public ShortString CompressionDescriptionRetired { get { return Items.FindFirst<ShortString>("00280063") as ShortString; } }
        public List<ShortString> CompressionDescriptionRetired_ { get { return Items.FindAll<ShortString>("00280063").ToList(); } }
        public CodeString CompressionSequenceRetired { get { return Items.FindFirst<CodeString>("00280065") as CodeString; } }
        public List<CodeString> CompressionSequenceRetired_ { get { return Items.FindAll<CodeString>("00280065").ToList(); } }
        public AttributeTag CompressionStepPointersRetired { get { return Items.FindFirst<AttributeTag>("00280066") as AttributeTag; } }
        public List<AttributeTag> CompressionStepPointersRetired_ { get { return Items.FindAll<AttributeTag>("00280066").ToList(); } }
        public UnsignedShort RepeatIntervalRetired { get { return Items.FindFirst<UnsignedShort>("00280068") as UnsignedShort; } }
        public List<UnsignedShort> RepeatIntervalRetired_ { get { return Items.FindAll<UnsignedShort>("00280068").ToList(); } }
        public UnsignedShort BitsGroupedRetired { get { return Items.FindFirst<UnsignedShort>("00280069") as UnsignedShort; } }
        public List<UnsignedShort> BitsGroupedRetired_ { get { return Items.FindAll<UnsignedShort>("00280069").ToList(); } }
        public UnsignedShort PerimeterTableRetired { get { return Items.FindFirst<UnsignedShort>("00280070") as UnsignedShort; } }
        public List<UnsignedShort> PerimeterTableRetired_ { get { return Items.FindAll<UnsignedShort>("00280070").ToList(); } }
        public UnsignedShort PerimeterValueRetired { get { return Items.FindFirst<UnsignedShort>("00280071") as UnsignedShort; } }
        public List<UnsignedShort> PerimeterValueRetired_ { get { return Items.FindAll<UnsignedShort>("00280071").ToList(); } }
        public UnsignedShort PredictorRowsRetired { get { return Items.FindFirst<UnsignedShort>("00280080") as UnsignedShort; } }
        public List<UnsignedShort> PredictorRowsRetired_ { get { return Items.FindAll<UnsignedShort>("00280080").ToList(); } }
        public UnsignedShort PredictorColumnsRetired { get { return Items.FindFirst<UnsignedShort>("00280081") as UnsignedShort; } }
        public List<UnsignedShort> PredictorColumnsRetired_ { get { return Items.FindAll<UnsignedShort>("00280081").ToList(); } }
        public UnsignedShort PredictorConstantsRetired { get { return Items.FindFirst<UnsignedShort>("00280082") as UnsignedShort; } }
        public List<UnsignedShort> PredictorConstantsRetired_ { get { return Items.FindAll<UnsignedShort>("00280082").ToList(); } }
        public CodeString BlockedPixelsRetired { get { return Items.FindFirst<CodeString>("00280090") as CodeString; } }
        public List<CodeString> BlockedPixelsRetired_ { get { return Items.FindAll<CodeString>("00280090").ToList(); } }
        public UnsignedShort BlockRowsRetired { get { return Items.FindFirst<UnsignedShort>("00280091") as UnsignedShort; } }
        public List<UnsignedShort> BlockRowsRetired_ { get { return Items.FindAll<UnsignedShort>("00280091").ToList(); } }
        public UnsignedShort BlockColumnsRetired { get { return Items.FindFirst<UnsignedShort>("00280092") as UnsignedShort; } }
        public List<UnsignedShort> BlockColumnsRetired_ { get { return Items.FindAll<UnsignedShort>("00280092").ToList(); } }
        public UnsignedShort RowOverlapRetired { get { return Items.FindFirst<UnsignedShort>("00280093") as UnsignedShort; } }
        public List<UnsignedShort> RowOverlapRetired_ { get { return Items.FindAll<UnsignedShort>("00280093").ToList(); } }
        public UnsignedShort ColumnOverlapRetired { get { return Items.FindFirst<UnsignedShort>("00280094") as UnsignedShort; } }
        public List<UnsignedShort> ColumnOverlapRetired_ { get { return Items.FindAll<UnsignedShort>("00280094").ToList(); } }
        public UnsignedShort BitsAllocated { get { return Items.FindFirst<UnsignedShort>("00280100") as UnsignedShort; } }
        public List<UnsignedShort> BitsAllocated_ { get { return Items.FindAll<UnsignedShort>("00280100").ToList(); } }
        public UnsignedShort BitsStored { get { return Items.FindFirst<UnsignedShort>("00280101") as UnsignedShort; } }
        public List<UnsignedShort> BitsStored_ { get { return Items.FindAll<UnsignedShort>("00280101").ToList(); } }
        public UnsignedShort HighBit { get { return Items.FindFirst<UnsignedShort>("00280102") as UnsignedShort; } }
        public List<UnsignedShort> HighBit_ { get { return Items.FindAll<UnsignedShort>("00280102").ToList(); } }
        public UnsignedShort PixelRepresentation { get { return Items.FindFirst<UnsignedShort>("00280103") as UnsignedShort; } }
        public List<UnsignedShort> PixelRepresentation_ { get { return Items.FindAll<UnsignedShort>("00280103").ToList(); } }
        public UnsignedShort SmallestValidPixelValueRetired { get { return Items.FindFirst<UnsignedShort>("00280104") as UnsignedShort; } }
        public List<UnsignedShort> SmallestValidPixelValueRetired_ { get { return Items.FindAll<UnsignedShort>("00280104").ToList(); } }
        public UnsignedShort LargestValidPixelValueRetired { get { return Items.FindFirst<UnsignedShort>("00280105") as UnsignedShort; } }
        public List<UnsignedShort> LargestValidPixelValueRetired_ { get { return Items.FindAll<UnsignedShort>("00280105").ToList(); } }
        public UnsignedShort SmallestImagePixelValue { get { return Items.FindFirst<UnsignedShort>("00280106") as UnsignedShort; } }
        public List<UnsignedShort> SmallestImagePixelValue_ { get { return Items.FindAll<UnsignedShort>("00280106").ToList(); } }
        public UnsignedShort LargestImagePixelValue { get { return Items.FindFirst<UnsignedShort>("00280107") as UnsignedShort; } }
        public List<UnsignedShort> LargestImagePixelValue_ { get { return Items.FindAll<UnsignedShort>("00280107").ToList(); } }
        public UnsignedShort SmallestPixelValueInSeries { get { return Items.FindFirst<UnsignedShort>("00280108") as UnsignedShort; } }
        public List<UnsignedShort> SmallestPixelValueInSeries_ { get { return Items.FindAll<UnsignedShort>("00280108").ToList(); } }
        public UnsignedShort LargestPixelValueInSeries { get { return Items.FindFirst<UnsignedShort>("00280109") as UnsignedShort; } }
        public List<UnsignedShort> LargestPixelValueInSeries_ { get { return Items.FindAll<UnsignedShort>("00280109").ToList(); } }
        public UnsignedShort SmallestImagePixelValueInPlaneRetired { get { return Items.FindFirst<UnsignedShort>("00280110") as UnsignedShort; } }
        public List<UnsignedShort> SmallestImagePixelValueInPlaneRetired_ { get { return Items.FindAll<UnsignedShort>("00280110").ToList(); } }
        public UnsignedShort LargestImagePixelValueInPlaneRetired { get { return Items.FindFirst<UnsignedShort>("00280111") as UnsignedShort; } }
        public List<UnsignedShort> LargestImagePixelValueInPlaneRetired_ { get { return Items.FindAll<UnsignedShort>("00280111").ToList(); } }
        public UnsignedShort PixelPaddingValue { get { return Items.FindFirst<UnsignedShort>("00280120") as UnsignedShort; } }
        public List<UnsignedShort> PixelPaddingValue_ { get { return Items.FindAll<UnsignedShort>("00280120").ToList(); } }
        public UnsignedShort PixelPaddingRangeLimit { get { return Items.FindFirst<UnsignedShort>("00280121") as UnsignedShort; } }
        public List<UnsignedShort> PixelPaddingRangeLimit_ { get { return Items.FindAll<UnsignedShort>("00280121").ToList(); } }
        public UnsignedShort ImageLocationRetired { get { return Items.FindFirst<UnsignedShort>("00280200") as UnsignedShort; } }
        public List<UnsignedShort> ImageLocationRetired_ { get { return Items.FindAll<UnsignedShort>("00280200").ToList(); } }
        public CodeString QualityControlImage { get { return Items.FindFirst<CodeString>("00280300") as CodeString; } }
        public List<CodeString> QualityControlImage_ { get { return Items.FindAll<CodeString>("00280300").ToList(); } }
        public CodeString BurnedInAnnotation { get { return Items.FindFirst<CodeString>("00280301") as CodeString; } }
        public List<CodeString> BurnedInAnnotation_ { get { return Items.FindAll<CodeString>("00280301").ToList(); } }
        public CodeString RecognizableVisualFeatures { get { return Items.FindFirst<CodeString>("00280302") as CodeString; } }
        public List<CodeString> RecognizableVisualFeatures_ { get { return Items.FindAll<CodeString>("00280302").ToList(); } }
        public CodeString LongitudinalTemporalInformationModified { get { return Items.FindFirst<CodeString>("00280303") as CodeString; } }
        public List<CodeString> LongitudinalTemporalInformationModified_ { get { return Items.FindAll<CodeString>("00280303").ToList(); } }
        public LongString TransformLabelRetired { get { return Items.FindFirst<LongString>("00280400") as LongString; } }
        public List<LongString> TransformLabelRetired_ { get { return Items.FindAll<LongString>("00280400").ToList(); } }
        public LongString TransformVersionNumberRetired { get { return Items.FindFirst<LongString>("00280401") as LongString; } }
        public List<LongString> TransformVersionNumberRetired_ { get { return Items.FindAll<LongString>("00280401").ToList(); } }
        public UnsignedShort NumberOfTransformStepsRetired { get { return Items.FindFirst<UnsignedShort>("00280402") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTransformStepsRetired_ { get { return Items.FindAll<UnsignedShort>("00280402").ToList(); } }
        public LongString SequenceOfCompressedDataRetired { get { return Items.FindFirst<LongString>("00280403") as LongString; } }
        public List<LongString> SequenceOfCompressedDataRetired_ { get { return Items.FindAll<LongString>("00280403").ToList(); } }
        public AttributeTag DetailsOfCoefficientsRetired { get { return Items.FindFirst<AttributeTag>("00280404") as AttributeTag; } }
        public List<AttributeTag> DetailsOfCoefficientsRetired_ { get { return Items.FindAll<AttributeTag>("00280404").ToList(); } }
        public UnsignedShort RowsForNthOrderCoefficientsRetired { get { return Items.FindFirst<UnsignedShort>("002804x0") as UnsignedShort; } }
        public List<UnsignedShort> RowsForNthOrderCoefficientsRetired_ { get { return Items.FindAll<UnsignedShort>("002804x0").ToList(); } }
        public UnsignedShort ColumnsForNthOrderCoefficientsRetired { get { return Items.FindFirst<UnsignedShort>("002804x1") as UnsignedShort; } }
        public List<UnsignedShort> ColumnsForNthOrderCoefficientsRetired_ { get { return Items.FindAll<UnsignedShort>("002804x1").ToList(); } }
        public LongString CoefficientCodingRetired { get { return Items.FindFirst<LongString>("002804x2") as LongString; } }
        public List<LongString> CoefficientCodingRetired_ { get { return Items.FindAll<LongString>("002804x2").ToList(); } }
        public AttributeTag CoefficientCodingPointersRetired { get { return Items.FindFirst<AttributeTag>("002804x3") as AttributeTag; } }
        public List<AttributeTag> CoefficientCodingPointersRetired_ { get { return Items.FindAll<AttributeTag>("002804x3").ToList(); } }
        public LongString DCTLabelRetired { get { return Items.FindFirst<LongString>("00280700") as LongString; } }
        public List<LongString> DCTLabelRetired_ { get { return Items.FindAll<LongString>("00280700").ToList(); } }
        public CodeString DataBlockDescriptionRetired { get { return Items.FindFirst<CodeString>("00280701") as CodeString; } }
        public List<CodeString> DataBlockDescriptionRetired_ { get { return Items.FindAll<CodeString>("00280701").ToList(); } }
        public AttributeTag DataBlockRetired { get { return Items.FindFirst<AttributeTag>("00280702") as AttributeTag; } }
        public List<AttributeTag> DataBlockRetired_ { get { return Items.FindAll<AttributeTag>("00280702").ToList(); } }
        public UnsignedShort NormalizationFactorFormatRetired { get { return Items.FindFirst<UnsignedShort>("00280710") as UnsignedShort; } }
        public List<UnsignedShort> NormalizationFactorFormatRetired_ { get { return Items.FindAll<UnsignedShort>("00280710").ToList(); } }
        public UnsignedShort ZonalMapNumberFormatRetired { get { return Items.FindFirst<UnsignedShort>("00280720") as UnsignedShort; } }
        public List<UnsignedShort> ZonalMapNumberFormatRetired_ { get { return Items.FindAll<UnsignedShort>("00280720").ToList(); } }
        public AttributeTag ZonalMapLocationRetired { get { return Items.FindFirst<AttributeTag>("00280721") as AttributeTag; } }
        public List<AttributeTag> ZonalMapLocationRetired_ { get { return Items.FindAll<AttributeTag>("00280721").ToList(); } }
        public UnsignedShort ZonalMapFormatRetired { get { return Items.FindFirst<UnsignedShort>("00280722") as UnsignedShort; } }
        public List<UnsignedShort> ZonalMapFormatRetired_ { get { return Items.FindAll<UnsignedShort>("00280722").ToList(); } }
        public UnsignedShort AdaptiveMapFormatRetired { get { return Items.FindFirst<UnsignedShort>("00280730") as UnsignedShort; } }
        public List<UnsignedShort> AdaptiveMapFormatRetired_ { get { return Items.FindAll<UnsignedShort>("00280730").ToList(); } }
        public UnsignedShort CodeNumberFormatRetired { get { return Items.FindFirst<UnsignedShort>("00280740") as UnsignedShort; } }
        public List<UnsignedShort> CodeNumberFormatRetired_ { get { return Items.FindAll<UnsignedShort>("00280740").ToList(); } }
        public CodeString CodeLabelRetired { get { return Items.FindFirst<CodeString>("002808x0") as CodeString; } }
        public List<CodeString> CodeLabelRetired_ { get { return Items.FindAll<CodeString>("002808x0").ToList(); } }
        public UnsignedShort NumberOfTablesRetired { get { return Items.FindFirst<UnsignedShort>("002808x2") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTablesRetired_ { get { return Items.FindAll<UnsignedShort>("002808x2").ToList(); } }
        public AttributeTag CodeTableLocationRetired { get { return Items.FindFirst<AttributeTag>("002808x3") as AttributeTag; } }
        public List<AttributeTag> CodeTableLocationRetired_ { get { return Items.FindAll<AttributeTag>("002808x3").ToList(); } }
        public UnsignedShort BitsForCodeWordRetired { get { return Items.FindFirst<UnsignedShort>("002808x4") as UnsignedShort; } }
        public List<UnsignedShort> BitsForCodeWordRetired_ { get { return Items.FindAll<UnsignedShort>("002808x4").ToList(); } }
        public AttributeTag ImageDataLocationRetired { get { return Items.FindFirst<AttributeTag>("002808x8") as AttributeTag; } }
        public List<AttributeTag> ImageDataLocationRetired_ { get { return Items.FindAll<AttributeTag>("002808x8").ToList(); } }
        public CodeString PixelSpacingCalibrationType { get { return Items.FindFirst<CodeString>("00280A02") as CodeString; } }
        public List<CodeString> PixelSpacingCalibrationType_ { get { return Items.FindAll<CodeString>("00280A02").ToList(); } }
        public LongString PixelSpacingCalibrationDescription { get { return Items.FindFirst<LongString>("00280A04") as LongString; } }
        public List<LongString> PixelSpacingCalibrationDescription_ { get { return Items.FindAll<LongString>("00280A04").ToList(); } }
        public CodeString PixelIntensityRelationship { get { return Items.FindFirst<CodeString>("00281040") as CodeString; } }
        public List<CodeString> PixelIntensityRelationship_ { get { return Items.FindAll<CodeString>("00281040").ToList(); } }
        public SignedShort PixelIntensityRelationshipSign { get { return Items.FindFirst<SignedShort>("00281041") as SignedShort; } }
        public List<SignedShort> PixelIntensityRelationshipSign_ { get { return Items.FindAll<SignedShort>("00281041").ToList(); } }
        public DecimalString WindowCenter { get { return Items.FindFirst<DecimalString>("00281050") as DecimalString; } }
        public List<DecimalString> WindowCenter_ { get { return Items.FindAll<DecimalString>("00281050").ToList(); } }
        public DecimalString WindowWidth { get { return Items.FindFirst<DecimalString>("00281051") as DecimalString; } }
        public List<DecimalString> WindowWidth_ { get { return Items.FindAll<DecimalString>("00281051").ToList(); } }
        public DecimalString RescaleIntercept { get { return Items.FindFirst<DecimalString>("00281052") as DecimalString; } }
        public List<DecimalString> RescaleIntercept_ { get { return Items.FindAll<DecimalString>("00281052").ToList(); } }
        public DecimalString RescaleSlope { get { return Items.FindFirst<DecimalString>("00281053") as DecimalString; } }
        public List<DecimalString> RescaleSlope_ { get { return Items.FindAll<DecimalString>("00281053").ToList(); } }
        public LongString RescaleType { get { return Items.FindFirst<LongString>("00281054") as LongString; } }
        public List<LongString> RescaleType_ { get { return Items.FindAll<LongString>("00281054").ToList(); } }
        public LongString WindowCenterWidthExplanation { get { return Items.FindFirst<LongString>("00281055") as LongString; } }
        public List<LongString> WindowCenterWidthExplanation_ { get { return Items.FindAll<LongString>("00281055").ToList(); } }
        public CodeString VOILUTFunction { get { return Items.FindFirst<CodeString>("00281056") as CodeString; } }
        public List<CodeString> VOILUTFunction_ { get { return Items.FindAll<CodeString>("00281056").ToList(); } }
        public CodeString GrayScaleRetired { get { return Items.FindFirst<CodeString>("00281080") as CodeString; } }
        public List<CodeString> GrayScaleRetired_ { get { return Items.FindAll<CodeString>("00281080").ToList(); } }
        public CodeString RecommendedViewingMode { get { return Items.FindFirst<CodeString>("00281090") as CodeString; } }
        public List<CodeString> RecommendedViewingMode_ { get { return Items.FindAll<CodeString>("00281090").ToList(); } }
        public UnsignedShort GrayLookupTableDescriptorRetired { get { return Items.FindFirst<UnsignedShort>("00281100") as UnsignedShort; } }
        public List<UnsignedShort> GrayLookupTableDescriptorRetired_ { get { return Items.FindAll<UnsignedShort>("00281100").ToList(); } }
        public UnsignedShort RedPaletteColorLookupTableDescriptor { get { return Items.FindFirst<UnsignedShort>("00281101") as UnsignedShort; } }
        public List<UnsignedShort> RedPaletteColorLookupTableDescriptor_ { get { return Items.FindAll<UnsignedShort>("00281101").ToList(); } }
        public UnsignedShort GreenPaletteColorLookupTableDescriptor { get { return Items.FindFirst<UnsignedShort>("00281102") as UnsignedShort; } }
        public List<UnsignedShort> GreenPaletteColorLookupTableDescriptor_ { get { return Items.FindAll<UnsignedShort>("00281102").ToList(); } }
        public UnsignedShort BluePaletteColorLookupTableDescriptor { get { return Items.FindFirst<UnsignedShort>("00281103") as UnsignedShort; } }
        public List<UnsignedShort> BluePaletteColorLookupTableDescriptor_ { get { return Items.FindAll<UnsignedShort>("00281103").ToList(); } }
        public UnsignedShort AlphaPaletteColorLookupTableDescriptor { get { return Items.FindFirst<UnsignedShort>("00281104") as UnsignedShort; } }
        public List<UnsignedShort> AlphaPaletteColorLookupTableDescriptor_ { get { return Items.FindAll<UnsignedShort>("00281104").ToList(); } }
        public UnsignedShort LargeRedPaletteColorLookupTableDescriptorRetired { get { return Items.FindFirst<UnsignedShort>("00281111") as UnsignedShort; } }
        public List<UnsignedShort> LargeRedPaletteColorLookupTableDescriptorRetired_ { get { return Items.FindAll<UnsignedShort>("00281111").ToList(); } }
        public UnsignedShort LargeGreenPaletteColorLookupTableDescriptorRetired { get { return Items.FindFirst<UnsignedShort>("00281112") as UnsignedShort; } }
        public List<UnsignedShort> LargeGreenPaletteColorLookupTableDescriptorRetired_ { get { return Items.FindAll<UnsignedShort>("00281112").ToList(); } }
        public UnsignedShort LargeBluePaletteColorLookupTableDescriptorRetired { get { return Items.FindFirst<UnsignedShort>("00281113") as UnsignedShort; } }
        public List<UnsignedShort> LargeBluePaletteColorLookupTableDescriptorRetired_ { get { return Items.FindAll<UnsignedShort>("00281113").ToList(); } }
        public UniqueIdentifier PaletteColorLookupTableUID { get { return Items.FindFirst<UniqueIdentifier>("00281199") as UniqueIdentifier; } }
        public List<UniqueIdentifier> PaletteColorLookupTableUID_ { get { return Items.FindAll<UniqueIdentifier>("00281199").ToList(); } }
        public UnsignedShort GrayLookupTableDataRetired { get { return Items.FindFirst<UnsignedShort>("00281200") as UnsignedShort; } }
        public List<UnsignedShort> GrayLookupTableDataRetired_ { get { return Items.FindAll<UnsignedShort>("00281200").ToList(); } }
        public OtherWordString RedPaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281201") as OtherWordString; } }
        public List<OtherWordString> RedPaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281201").ToList(); } }
        public OtherWordString GreenPaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281202") as OtherWordString; } }
        public List<OtherWordString> GreenPaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281202").ToList(); } }
        public OtherWordString BluePaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281203") as OtherWordString; } }
        public List<OtherWordString> BluePaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281203").ToList(); } }
        public OtherWordString AlphaPaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281204") as OtherWordString; } }
        public List<OtherWordString> AlphaPaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281204").ToList(); } }
        public OtherWordString LargeRedPaletteColorLookupTableDataRetired { get { return Items.FindFirst<OtherWordString>("00281211") as OtherWordString; } }
        public List<OtherWordString> LargeRedPaletteColorLookupTableDataRetired_ { get { return Items.FindAll<OtherWordString>("00281211").ToList(); } }
        public OtherWordString LargeGreenPaletteColorLookupTableDataRetired { get { return Items.FindFirst<OtherWordString>("00281212") as OtherWordString; } }
        public List<OtherWordString> LargeGreenPaletteColorLookupTableDataRetired_ { get { return Items.FindAll<OtherWordString>("00281212").ToList(); } }
        public OtherWordString LargeBluePaletteColorLookupTableDataRetired { get { return Items.FindFirst<OtherWordString>("00281213") as OtherWordString; } }
        public List<OtherWordString> LargeBluePaletteColorLookupTableDataRetired_ { get { return Items.FindAll<OtherWordString>("00281213").ToList(); } }
        public UniqueIdentifier LargePaletteColorLookupTableUIDRetired { get { return Items.FindFirst<UniqueIdentifier>("00281214") as UniqueIdentifier; } }
        public List<UniqueIdentifier> LargePaletteColorLookupTableUIDRetired_ { get { return Items.FindAll<UniqueIdentifier>("00281214").ToList(); } }
        public OtherWordString SegmentedRedPaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281221") as OtherWordString; } }
        public List<OtherWordString> SegmentedRedPaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281221").ToList(); } }
        public OtherWordString SegmentedGreenPaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281222") as OtherWordString; } }
        public List<OtherWordString> SegmentedGreenPaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281222").ToList(); } }
        public OtherWordString SegmentedBluePaletteColorLookupTableData { get { return Items.FindFirst<OtherWordString>("00281223") as OtherWordString; } }
        public List<OtherWordString> SegmentedBluePaletteColorLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281223").ToList(); } }
        public CodeString BreastImplantPresent { get { return Items.FindFirst<CodeString>("00281300") as CodeString; } }
        public List<CodeString> BreastImplantPresent_ { get { return Items.FindAll<CodeString>("00281300").ToList(); } }
        public CodeString PartialView { get { return Items.FindFirst<CodeString>("00281350") as CodeString; } }
        public List<CodeString> PartialView_ { get { return Items.FindAll<CodeString>("00281350").ToList(); } }
        public ShortText PartialViewDescription { get { return Items.FindFirst<ShortText>("00281351") as ShortText; } }
        public List<ShortText> PartialViewDescription_ { get { return Items.FindAll<ShortText>("00281351").ToList(); } }
        public SequenceSelector PartialViewCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00281352")); } }
        public List<SequenceSelector> PartialViewCodeSequence_ { get { return Items.FindAll<Sequence>("00281352").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SpatialLocationsPreserved { get { return Items.FindFirst<CodeString>("0028135A") as CodeString; } }
        public List<CodeString> SpatialLocationsPreserved_ { get { return Items.FindAll<CodeString>("0028135A").ToList(); } }
        public SequenceSelector DataFrameAssignmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00281401")); } }
        public List<SequenceSelector> DataFrameAssignmentSequence_ { get { return Items.FindAll<Sequence>("00281401").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DataPathAssignment { get { return Items.FindFirst<CodeString>("00281402") as CodeString; } }
        public List<CodeString> DataPathAssignment_ { get { return Items.FindAll<CodeString>("00281402").ToList(); } }
        public UnsignedShort BitsMappedToColorLookupTable { get { return Items.FindFirst<UnsignedShort>("00281403") as UnsignedShort; } }
        public List<UnsignedShort> BitsMappedToColorLookupTable_ { get { return Items.FindAll<UnsignedShort>("00281403").ToList(); } }
        public SequenceSelector BlendingLUT1Sequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00281404")); } }
        public List<SequenceSelector> BlendingLUT1Sequence_ { get { return Items.FindAll<Sequence>("00281404").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BlendingLUT1TransferFunction { get { return Items.FindFirst<CodeString>("00281405") as CodeString; } }
        public List<CodeString> BlendingLUT1TransferFunction_ { get { return Items.FindAll<CodeString>("00281405").ToList(); } }
        public FloatingPointDouble BlendingWeightConstant { get { return Items.FindFirst<FloatingPointDouble>("00281406") as FloatingPointDouble; } }
        public List<FloatingPointDouble> BlendingWeightConstant_ { get { return Items.FindAll<FloatingPointDouble>("00281406").ToList(); } }
        public UnsignedShort BlendingLookupTableDescriptor { get { return Items.FindFirst<UnsignedShort>("00281407") as UnsignedShort; } }
        public List<UnsignedShort> BlendingLookupTableDescriptor_ { get { return Items.FindAll<UnsignedShort>("00281407").ToList(); } }
        public OtherWordString BlendingLookupTableData { get { return Items.FindFirst<OtherWordString>("00281408") as OtherWordString; } }
        public List<OtherWordString> BlendingLookupTableData_ { get { return Items.FindAll<OtherWordString>("00281408").ToList(); } }
        public SequenceSelector EnhancedPaletteColorLookupTableSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0028140B")); } }
        public List<SequenceSelector> EnhancedPaletteColorLookupTableSequence_ { get { return Items.FindAll<Sequence>("0028140B").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BlendingLUT2Sequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0028140C")); } }
        public List<SequenceSelector> BlendingLUT2Sequence_ { get { return Items.FindAll<Sequence>("0028140C").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BlendingLUT2TransferFunction { get { return Items.FindFirst<CodeString>("0028140D") as CodeString; } }
        public List<CodeString> BlendingLUT2TransferFunction_ { get { return Items.FindAll<CodeString>("0028140D").ToList(); } }
        public CodeString DataPathID { get { return Items.FindFirst<CodeString>("0028140E") as CodeString; } }
        public List<CodeString> DataPathID_ { get { return Items.FindAll<CodeString>("0028140E").ToList(); } }
        public CodeString RGBLUTTransferFunction { get { return Items.FindFirst<CodeString>("0028140F") as CodeString; } }
        public List<CodeString> RGBLUTTransferFunction_ { get { return Items.FindAll<CodeString>("0028140F").ToList(); } }
        public CodeString AlphaLUTTransferFunction { get { return Items.FindFirst<CodeString>("00281410") as CodeString; } }
        public List<CodeString> AlphaLUTTransferFunction_ { get { return Items.FindAll<CodeString>("00281410").ToList(); } }
        public OtherByteString ICCProfile { get { return Items.FindFirst<OtherByteString>("00282000") as OtherByteString; } }
        public List<OtherByteString> ICCProfile_ { get { return Items.FindAll<OtherByteString>("00282000").ToList(); } }
        public CodeString LossyImageCompression { get { return Items.FindFirst<CodeString>("00282110") as CodeString; } }
        public List<CodeString> LossyImageCompression_ { get { return Items.FindAll<CodeString>("00282110").ToList(); } }
        public DecimalString LossyImageCompressionRatio { get { return Items.FindFirst<DecimalString>("00282112") as DecimalString; } }
        public List<DecimalString> LossyImageCompressionRatio_ { get { return Items.FindAll<DecimalString>("00282112").ToList(); } }
        public CodeString LossyImageCompressionMethod { get { return Items.FindFirst<CodeString>("00282114") as CodeString; } }
        public List<CodeString> LossyImageCompressionMethod_ { get { return Items.FindAll<CodeString>("00282114").ToList(); } }
        public SequenceSelector ModalityLUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00283000")); } }
        public List<SequenceSelector> ModalityLUTSequence_ { get { return Items.FindAll<Sequence>("00283000").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort LUTDescriptor { get { return Items.FindFirst<UnsignedShort>("00283002") as UnsignedShort; } }
        public List<UnsignedShort> LUTDescriptor_ { get { return Items.FindAll<UnsignedShort>("00283002").ToList(); } }
        public LongString LUTExplanation { get { return Items.FindFirst<LongString>("00283003") as LongString; } }
        public List<LongString> LUTExplanation_ { get { return Items.FindAll<LongString>("00283003").ToList(); } }
        public LongString ModalityLUTType { get { return Items.FindFirst<LongString>("00283004") as LongString; } }
        public List<LongString> ModalityLUTType_ { get { return Items.FindAll<LongString>("00283004").ToList(); } }
        public UnsignedShort LUTData { get { return Items.FindFirst<UnsignedShort>("00283006") as UnsignedShort; } }
        public List<UnsignedShort> LUTData_ { get { return Items.FindAll<UnsignedShort>("00283006").ToList(); } }
        public SequenceSelector VOILUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00283010")); } }
        public List<SequenceSelector> VOILUTSequence_ { get { return Items.FindAll<Sequence>("00283010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SoftcopyVOILUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00283110")); } }
        public List<SequenceSelector> SoftcopyVOILUTSequence_ { get { return Items.FindAll<Sequence>("00283110").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongText ImagePresentationCommentsRetired { get { return Items.FindFirst<LongText>("00284000") as LongText; } }
        public List<LongText> ImagePresentationCommentsRetired_ { get { return Items.FindAll<LongText>("00284000").ToList(); } }
        public SequenceSelector BiPlaneAcquisitionSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00285000")); } }
        public List<SequenceSelector> BiPlaneAcquisitionSequenceRetired_ { get { return Items.FindAll<Sequence>("00285000").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort RepresentativeFrameNumber { get { return Items.FindFirst<UnsignedShort>("00286010") as UnsignedShort; } }
        public List<UnsignedShort> RepresentativeFrameNumber_ { get { return Items.FindAll<UnsignedShort>("00286010").ToList(); } }
        public UnsignedShort FrameNumbersOfInterest { get { return Items.FindFirst<UnsignedShort>("00286020") as UnsignedShort; } }
        public List<UnsignedShort> FrameNumbersOfInterest_ { get { return Items.FindAll<UnsignedShort>("00286020").ToList(); } }
        public LongString FrameOfInterestDescription { get { return Items.FindFirst<LongString>("00286022") as LongString; } }
        public List<LongString> FrameOfInterestDescription_ { get { return Items.FindAll<LongString>("00286022").ToList(); } }
        public CodeString FrameOfInterestType { get { return Items.FindFirst<CodeString>("00286023") as CodeString; } }
        public List<CodeString> FrameOfInterestType_ { get { return Items.FindAll<CodeString>("00286023").ToList(); } }
        public UnsignedShort MaskPointersRetired { get { return Items.FindFirst<UnsignedShort>("00286030") as UnsignedShort; } }
        public List<UnsignedShort> MaskPointersRetired_ { get { return Items.FindAll<UnsignedShort>("00286030").ToList(); } }
        public UnsignedShort RWavePointer { get { return Items.FindFirst<UnsignedShort>("00286040") as UnsignedShort; } }
        public List<UnsignedShort> RWavePointer_ { get { return Items.FindAll<UnsignedShort>("00286040").ToList(); } }
        public SequenceSelector MaskSubtractionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00286100")); } }
        public List<SequenceSelector> MaskSubtractionSequence_ { get { return Items.FindAll<Sequence>("00286100").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString MaskOperation { get { return Items.FindFirst<CodeString>("00286101") as CodeString; } }
        public List<CodeString> MaskOperation_ { get { return Items.FindAll<CodeString>("00286101").ToList(); } }
        public UnsignedShort ApplicableFrameRange { get { return Items.FindFirst<UnsignedShort>("00286102") as UnsignedShort; } }
        public List<UnsignedShort> ApplicableFrameRange_ { get { return Items.FindAll<UnsignedShort>("00286102").ToList(); } }
        public UnsignedShort MaskFrameNumbers { get { return Items.FindFirst<UnsignedShort>("00286110") as UnsignedShort; } }
        public List<UnsignedShort> MaskFrameNumbers_ { get { return Items.FindAll<UnsignedShort>("00286110").ToList(); } }
        public UnsignedShort ContrastFrameAveraging { get { return Items.FindFirst<UnsignedShort>("00286112") as UnsignedShort; } }
        public List<UnsignedShort> ContrastFrameAveraging_ { get { return Items.FindAll<UnsignedShort>("00286112").ToList(); } }
        public FloatingPointSingle MaskSubPixelShift { get { return Items.FindFirst<FloatingPointSingle>("00286114") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaskSubPixelShift_ { get { return Items.FindAll<FloatingPointSingle>("00286114").ToList(); } }
        public SignedShort TIDOffset { get { return Items.FindFirst<SignedShort>("00286120") as SignedShort; } }
        public List<SignedShort> TIDOffset_ { get { return Items.FindAll<SignedShort>("00286120").ToList(); } }
        public ShortText MaskOperationExplanation { get { return Items.FindFirst<ShortText>("00286190") as ShortText; } }
        public List<ShortText> MaskOperationExplanation_ { get { return Items.FindAll<ShortText>("00286190").ToList(); } }
        public UnlimitedText PixelDataProviderURL { get { return Items.FindFirst<UnlimitedText>("00287FE0") as UnlimitedText; } }
        public List<UnlimitedText> PixelDataProviderURL_ { get { return Items.FindAll<UnlimitedText>("00287FE0").ToList(); } }
        public UnsignedLong DataPointRows { get { return Items.FindFirst<UnsignedLong>("00289001") as UnsignedLong; } }
        public List<UnsignedLong> DataPointRows_ { get { return Items.FindAll<UnsignedLong>("00289001").ToList(); } }
        public UnsignedLong DataPointColumns { get { return Items.FindFirst<UnsignedLong>("00289002") as UnsignedLong; } }
        public List<UnsignedLong> DataPointColumns_ { get { return Items.FindAll<UnsignedLong>("00289002").ToList(); } }
        public CodeString SignalDomainColumns { get { return Items.FindFirst<CodeString>("00289003") as CodeString; } }
        public List<CodeString> SignalDomainColumns_ { get { return Items.FindAll<CodeString>("00289003").ToList(); } }
        public UnsignedShort LargestMonochromePixelValueRetired { get { return Items.FindFirst<UnsignedShort>("00289099") as UnsignedShort; } }
        public List<UnsignedShort> LargestMonochromePixelValueRetired_ { get { return Items.FindAll<UnsignedShort>("00289099").ToList(); } }
        public CodeString DataRepresentation { get { return Items.FindFirst<CodeString>("00289108") as CodeString; } }
        public List<CodeString> DataRepresentation_ { get { return Items.FindAll<CodeString>("00289108").ToList(); } }
        public SequenceSelector PixelMeasuresSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289110")); } }
        public List<SequenceSelector> PixelMeasuresSequence_ { get { return Items.FindAll<Sequence>("00289110").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FrameVOILUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289132")); } }
        public List<SequenceSelector> FrameVOILUTSequence_ { get { return Items.FindAll<Sequence>("00289132").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PixelValueTransformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289145")); } }
        public List<SequenceSelector> PixelValueTransformationSequence_ { get { return Items.FindAll<Sequence>("00289145").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SignalDomainRows { get { return Items.FindFirst<CodeString>("00289235") as CodeString; } }
        public List<CodeString> SignalDomainRows_ { get { return Items.FindAll<CodeString>("00289235").ToList(); } }
        public FloatingPointSingle DisplayFilterPercentage { get { return Items.FindFirst<FloatingPointSingle>("00289411") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DisplayFilterPercentage_ { get { return Items.FindAll<FloatingPointSingle>("00289411").ToList(); } }
        public SequenceSelector FramePixelShiftSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289415")); } }
        public List<SequenceSelector> FramePixelShiftSequence_ { get { return Items.FindAll<Sequence>("00289415").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort SubtractionItemID { get { return Items.FindFirst<UnsignedShort>("00289416") as UnsignedShort; } }
        public List<UnsignedShort> SubtractionItemID_ { get { return Items.FindAll<UnsignedShort>("00289416").ToList(); } }
        public SequenceSelector PixelIntensityRelationshipLUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289422")); } }
        public List<SequenceSelector> PixelIntensityRelationshipLUTSequence_ { get { return Items.FindAll<Sequence>("00289422").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FramePixelDataPropertiesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289443")); } }
        public List<SequenceSelector> FramePixelDataPropertiesSequence_ { get { return Items.FindAll<Sequence>("00289443").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString GeometricalProperties { get { return Items.FindFirst<CodeString>("00289444") as CodeString; } }
        public List<CodeString> GeometricalProperties_ { get { return Items.FindAll<CodeString>("00289444").ToList(); } }
        public FloatingPointSingle GeometricMaximumDistortion { get { return Items.FindFirst<FloatingPointSingle>("00289445") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GeometricMaximumDistortion_ { get { return Items.FindAll<FloatingPointSingle>("00289445").ToList(); } }
        public CodeString ImageProcessingApplied { get { return Items.FindFirst<CodeString>("00289446") as CodeString; } }
        public List<CodeString> ImageProcessingApplied_ { get { return Items.FindAll<CodeString>("00289446").ToList(); } }
        public CodeString MaskSelectionMode { get { return Items.FindFirst<CodeString>("00289454") as CodeString; } }
        public List<CodeString> MaskSelectionMode_ { get { return Items.FindAll<CodeString>("00289454").ToList(); } }
        public CodeString LUTFunction { get { return Items.FindFirst<CodeString>("00289474") as CodeString; } }
        public List<CodeString> LUTFunction_ { get { return Items.FindAll<CodeString>("00289474").ToList(); } }
        public FloatingPointSingle MaskVisibilityPercentage { get { return Items.FindFirst<FloatingPointSingle>("00289478") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaskVisibilityPercentage_ { get { return Items.FindAll<FloatingPointSingle>("00289478").ToList(); } }
        public SequenceSelector PixelShiftSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289501")); } }
        public List<SequenceSelector> PixelShiftSequence_ { get { return Items.FindAll<Sequence>("00289501").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RegionPixelShiftSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289502")); } }
        public List<SequenceSelector> RegionPixelShiftSequence_ { get { return Items.FindAll<Sequence>("00289502").Select(s => new SequenceSelector(s)).ToList(); } }
        public SignedShort VerticesOfTheRegion { get { return Items.FindFirst<SignedShort>("00289503") as SignedShort; } }
        public List<SignedShort> VerticesOfTheRegion_ { get { return Items.FindAll<SignedShort>("00289503").ToList(); } }
        public SequenceSelector MultiFramePresentationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00289505")); } }
        public List<SequenceSelector> MultiFramePresentationSequence_ { get { return Items.FindAll<Sequence>("00289505").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort PixelShiftFrameRange { get { return Items.FindFirst<UnsignedShort>("00289506") as UnsignedShort; } }
        public List<UnsignedShort> PixelShiftFrameRange_ { get { return Items.FindAll<UnsignedShort>("00289506").ToList(); } }
        public UnsignedShort LUTFrameRange { get { return Items.FindFirst<UnsignedShort>("00289507") as UnsignedShort; } }
        public List<UnsignedShort> LUTFrameRange_ { get { return Items.FindAll<UnsignedShort>("00289507").ToList(); } }
        public DecimalString ImageToEquipmentMappingMatrix { get { return Items.FindFirst<DecimalString>("00289520") as DecimalString; } }
        public List<DecimalString> ImageToEquipmentMappingMatrix_ { get { return Items.FindAll<DecimalString>("00289520").ToList(); } }
        public CodeString EquipmentCoordinateSystemIdentification { get { return Items.FindFirst<CodeString>("00289537") as CodeString; } }
        public List<CodeString> EquipmentCoordinateSystemIdentification_ { get { return Items.FindAll<CodeString>("00289537").ToList(); } }
        public CodeString StudyStatusIDRetired { get { return Items.FindFirst<CodeString>("0032000A") as CodeString; } }
        public List<CodeString> StudyStatusIDRetired_ { get { return Items.FindAll<CodeString>("0032000A").ToList(); } }
        public CodeString StudyPriorityIDRetired { get { return Items.FindFirst<CodeString>("0032000C") as CodeString; } }
        public List<CodeString> StudyPriorityIDRetired_ { get { return Items.FindAll<CodeString>("0032000C").ToList(); } }
        public LongString StudyIDIssuerRetired { get { return Items.FindFirst<LongString>("00320012") as LongString; } }
        public List<LongString> StudyIDIssuerRetired_ { get { return Items.FindAll<LongString>("00320012").ToList(); } }
        public Date StudyVerifiedDateRetired { get { return Items.FindFirst<Date>("00320032") as Date; } }
        public List<Date> StudyVerifiedDateRetired_ { get { return Items.FindAll<Date>("00320032").ToList(); } }
        public Time StudyVerifiedTimeRetired { get { return Items.FindFirst<Time>("00320033") as Time; } }
        public List<Time> StudyVerifiedTimeRetired_ { get { return Items.FindAll<Time>("00320033").ToList(); } }
        public Date StudyReadDateRetired { get { return Items.FindFirst<Date>("00320034") as Date; } }
        public List<Date> StudyReadDateRetired_ { get { return Items.FindAll<Date>("00320034").ToList(); } }
        public Time StudyReadTimeRetired { get { return Items.FindFirst<Time>("00320035") as Time; } }
        public List<Time> StudyReadTimeRetired_ { get { return Items.FindAll<Time>("00320035").ToList(); } }
        public Date ScheduledStudyStartDateRetired { get { return Items.FindFirst<Date>("00321000") as Date; } }
        public List<Date> ScheduledStudyStartDateRetired_ { get { return Items.FindAll<Date>("00321000").ToList(); } }
        public Time ScheduledStudyStartTimeRetired { get { return Items.FindFirst<Time>("00321001") as Time; } }
        public List<Time> ScheduledStudyStartTimeRetired_ { get { return Items.FindAll<Time>("00321001").ToList(); } }
        public Date ScheduledStudyStopDateRetired { get { return Items.FindFirst<Date>("00321010") as Date; } }
        public List<Date> ScheduledStudyStopDateRetired_ { get { return Items.FindAll<Date>("00321010").ToList(); } }
        public Time ScheduledStudyStopTimeRetired { get { return Items.FindFirst<Time>("00321011") as Time; } }
        public List<Time> ScheduledStudyStopTimeRetired_ { get { return Items.FindAll<Time>("00321011").ToList(); } }
        public LongString ScheduledStudyLocationRetired { get { return Items.FindFirst<LongString>("00321020") as LongString; } }
        public List<LongString> ScheduledStudyLocationRetired_ { get { return Items.FindAll<LongString>("00321020").ToList(); } }
        public ApplicationEntity ScheduledStudyLocationAETitleRetired { get { return Items.FindFirst<ApplicationEntity>("00321021") as ApplicationEntity; } }
        public List<ApplicationEntity> ScheduledStudyLocationAETitleRetired_ { get { return Items.FindAll<ApplicationEntity>("00321021").ToList(); } }
        public LongString ReasonForStudyRetired { get { return Items.FindFirst<LongString>("00321030") as LongString; } }
        public List<LongString> ReasonForStudyRetired_ { get { return Items.FindAll<LongString>("00321030").ToList(); } }
        public SequenceSelector RequestingPhysicianIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00321031")); } }
        public List<SequenceSelector> RequestingPhysicianIdentificationSequence_ { get { return Items.FindAll<Sequence>("00321031").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName RequestingPhysician { get { return Items.FindFirst<PersonName>("00321032") as PersonName; } }
        public List<PersonName> RequestingPhysician_ { get { return Items.FindAll<PersonName>("00321032").ToList(); } }
        public LongString RequestingService { get { return Items.FindFirst<LongString>("00321033") as LongString; } }
        public List<LongString> RequestingService_ { get { return Items.FindAll<LongString>("00321033").ToList(); } }
        public SequenceSelector RequestingServiceCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00321034")); } }
        public List<SequenceSelector> RequestingServiceCodeSequence_ { get { return Items.FindAll<Sequence>("00321034").Select(s => new SequenceSelector(s)).ToList(); } }
        public Date StudyArrivalDateRetired { get { return Items.FindFirst<Date>("00321040") as Date; } }
        public List<Date> StudyArrivalDateRetired_ { get { return Items.FindAll<Date>("00321040").ToList(); } }
        public Time StudyArrivalTimeRetired { get { return Items.FindFirst<Time>("00321041") as Time; } }
        public List<Time> StudyArrivalTimeRetired_ { get { return Items.FindAll<Time>("00321041").ToList(); } }
        public Date StudyCompletionDateRetired { get { return Items.FindFirst<Date>("00321050") as Date; } }
        public List<Date> StudyCompletionDateRetired_ { get { return Items.FindAll<Date>("00321050").ToList(); } }
        public Time StudyCompletionTimeRetired { get { return Items.FindFirst<Time>("00321051") as Time; } }
        public List<Time> StudyCompletionTimeRetired_ { get { return Items.FindAll<Time>("00321051").ToList(); } }
        public CodeString StudyComponentStatusIDRetired { get { return Items.FindFirst<CodeString>("00321055") as CodeString; } }
        public List<CodeString> StudyComponentStatusIDRetired_ { get { return Items.FindAll<CodeString>("00321055").ToList(); } }
        public LongString RequestedProcedureDescription { get { return Items.FindFirst<LongString>("00321060") as LongString; } }
        public List<LongString> RequestedProcedureDescription_ { get { return Items.FindAll<LongString>("00321060").ToList(); } }
        public SequenceSelector RequestedProcedureCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00321064")); } }
        public List<SequenceSelector> RequestedProcedureCodeSequence_ { get { return Items.FindAll<Sequence>("00321064").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString RequestedContrastAgent { get { return Items.FindFirst<LongString>("00321070") as LongString; } }
        public List<LongString> RequestedContrastAgent_ { get { return Items.FindAll<LongString>("00321070").ToList(); } }
        public LongText StudyCommentsRetired { get { return Items.FindFirst<LongText>("00324000") as LongText; } }
        public List<LongText> StudyCommentsRetired_ { get { return Items.FindAll<LongText>("00324000").ToList(); } }
        public SequenceSelector ReferencedPatientAliasSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380004")); } }
        public List<SequenceSelector> ReferencedPatientAliasSequence_ { get { return Items.FindAll<Sequence>("00380004").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString VisitStatusID { get { return Items.FindFirst<CodeString>("00380008") as CodeString; } }
        public List<CodeString> VisitStatusID_ { get { return Items.FindAll<CodeString>("00380008").ToList(); } }
        public LongString AdmissionID { get { return Items.FindFirst<LongString>("00380010") as LongString; } }
        public List<LongString> AdmissionID_ { get { return Items.FindAll<LongString>("00380010").ToList(); } }
        public LongString IssuerOfAdmissionIDRetired { get { return Items.FindFirst<LongString>("00380011") as LongString; } }
        public List<LongString> IssuerOfAdmissionIDRetired_ { get { return Items.FindAll<LongString>("00380011").ToList(); } }
        public SequenceSelector IssuerOfAdmissionIDSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380014")); } }
        public List<SequenceSelector> IssuerOfAdmissionIDSequence_ { get { return Items.FindAll<Sequence>("00380014").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString RouteOfAdmissions { get { return Items.FindFirst<LongString>("00380016") as LongString; } }
        public List<LongString> RouteOfAdmissions_ { get { return Items.FindAll<LongString>("00380016").ToList(); } }
        public Date ScheduledAdmissionDateRetired { get { return Items.FindFirst<Date>("0038001A") as Date; } }
        public List<Date> ScheduledAdmissionDateRetired_ { get { return Items.FindAll<Date>("0038001A").ToList(); } }
        public Time ScheduledAdmissionTimeRetired { get { return Items.FindFirst<Time>("0038001B") as Time; } }
        public List<Time> ScheduledAdmissionTimeRetired_ { get { return Items.FindAll<Time>("0038001B").ToList(); } }
        public Date ScheduledDischargeDateRetired { get { return Items.FindFirst<Date>("0038001C") as Date; } }
        public List<Date> ScheduledDischargeDateRetired_ { get { return Items.FindAll<Date>("0038001C").ToList(); } }
        public Time ScheduledDischargeTimeRetired { get { return Items.FindFirst<Time>("0038001D") as Time; } }
        public List<Time> ScheduledDischargeTimeRetired_ { get { return Items.FindAll<Time>("0038001D").ToList(); } }
        public LongString ScheduledPatientInstitutionResidenceRetired { get { return Items.FindFirst<LongString>("0038001E") as LongString; } }
        public List<LongString> ScheduledPatientInstitutionResidenceRetired_ { get { return Items.FindAll<LongString>("0038001E").ToList(); } }
        public Date AdmittingDate { get { return Items.FindFirst<Date>("00380020") as Date; } }
        public List<Date> AdmittingDate_ { get { return Items.FindAll<Date>("00380020").ToList(); } }
        public Time AdmittingTime { get { return Items.FindFirst<Time>("00380021") as Time; } }
        public List<Time> AdmittingTime_ { get { return Items.FindAll<Time>("00380021").ToList(); } }
        public Date DischargeDateRetired { get { return Items.FindFirst<Date>("00380030") as Date; } }
        public List<Date> DischargeDateRetired_ { get { return Items.FindAll<Date>("00380030").ToList(); } }
        public Time DischargeTimeRetired { get { return Items.FindFirst<Time>("00380032") as Time; } }
        public List<Time> DischargeTimeRetired_ { get { return Items.FindAll<Time>("00380032").ToList(); } }
        public LongString DischargeDiagnosisDescriptionRetired { get { return Items.FindFirst<LongString>("00380040") as LongString; } }
        public List<LongString> DischargeDiagnosisDescriptionRetired_ { get { return Items.FindAll<LongString>("00380040").ToList(); } }
        public SequenceSelector DischargeDiagnosisCodeSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380044")); } }
        public List<SequenceSelector> DischargeDiagnosisCodeSequenceRetired_ { get { return Items.FindAll<Sequence>("00380044").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SpecialNeeds { get { return Items.FindFirst<LongString>("00380050") as LongString; } }
        public List<LongString> SpecialNeeds_ { get { return Items.FindAll<LongString>("00380050").ToList(); } }
        public LongString ServiceEpisodeID { get { return Items.FindFirst<LongString>("00380060") as LongString; } }
        public List<LongString> ServiceEpisodeID_ { get { return Items.FindAll<LongString>("00380060").ToList(); } }
        public LongString IssuerOfServiceEpisodeIDRetired { get { return Items.FindFirst<LongString>("00380061") as LongString; } }
        public List<LongString> IssuerOfServiceEpisodeIDRetired_ { get { return Items.FindAll<LongString>("00380061").ToList(); } }
        public LongString ServiceEpisodeDescription { get { return Items.FindFirst<LongString>("00380062") as LongString; } }
        public List<LongString> ServiceEpisodeDescription_ { get { return Items.FindAll<LongString>("00380062").ToList(); } }
        public SequenceSelector IssuerOfServiceEpisodeIDSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380064")); } }
        public List<SequenceSelector> IssuerOfServiceEpisodeIDSequence_ { get { return Items.FindAll<Sequence>("00380064").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PertinentDocumentsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380100")); } }
        public List<SequenceSelector> PertinentDocumentsSequence_ { get { return Items.FindAll<Sequence>("00380100").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString CurrentPatientLocation { get { return Items.FindFirst<LongString>("00380300") as LongString; } }
        public List<LongString> CurrentPatientLocation_ { get { return Items.FindAll<LongString>("00380300").ToList(); } }
        public LongString PatientInstitutionResidence { get { return Items.FindFirst<LongString>("00380400") as LongString; } }
        public List<LongString> PatientInstitutionResidence_ { get { return Items.FindAll<LongString>("00380400").ToList(); } }
        public LongString PatientState { get { return Items.FindFirst<LongString>("00380500") as LongString; } }
        public List<LongString> PatientState_ { get { return Items.FindAll<LongString>("00380500").ToList(); } }
        public SequenceSelector PatientClinicalTrialParticipationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00380502")); } }
        public List<SequenceSelector> PatientClinicalTrialParticipationSequence_ { get { return Items.FindAll<Sequence>("00380502").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongText VisitComments { get { return Items.FindFirst<LongText>("00384000") as LongText; } }
        public List<LongText> VisitComments_ { get { return Items.FindAll<LongText>("00384000").ToList(); } }
        public CodeString WaveformOriginality { get { return Items.FindFirst<CodeString>("003A0004") as CodeString; } }
        public List<CodeString> WaveformOriginality_ { get { return Items.FindAll<CodeString>("003A0004").ToList(); } }
        public UnsignedShort NumberOfWaveformChannels { get { return Items.FindFirst<UnsignedShort>("003A0005") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfWaveformChannels_ { get { return Items.FindAll<UnsignedShort>("003A0005").ToList(); } }
        public UnsignedLong NumberOfWaveformSamples { get { return Items.FindFirst<UnsignedLong>("003A0010") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfWaveformSamples_ { get { return Items.FindAll<UnsignedLong>("003A0010").ToList(); } }
        public DecimalString SamplingFrequency { get { return Items.FindFirst<DecimalString>("003A001A") as DecimalString; } }
        public List<DecimalString> SamplingFrequency_ { get { return Items.FindAll<DecimalString>("003A001A").ToList(); } }
        public ShortString MultiplexGroupLabel { get { return Items.FindFirst<ShortString>("003A0020") as ShortString; } }
        public List<ShortString> MultiplexGroupLabel_ { get { return Items.FindAll<ShortString>("003A0020").ToList(); } }
        public SequenceSelector ChannelDefinitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0200")); } }
        public List<SequenceSelector> ChannelDefinitionSequence_ { get { return Items.FindAll<Sequence>("003A0200").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString WaveformChannelNumber { get { return Items.FindFirst<IntegerString>("003A0202") as IntegerString; } }
        public List<IntegerString> WaveformChannelNumber_ { get { return Items.FindAll<IntegerString>("003A0202").ToList(); } }
        public ShortString ChannelLabel { get { return Items.FindFirst<ShortString>("003A0203") as ShortString; } }
        public List<ShortString> ChannelLabel_ { get { return Items.FindAll<ShortString>("003A0203").ToList(); } }
        public CodeString ChannelStatus { get { return Items.FindFirst<CodeString>("003A0205") as CodeString; } }
        public List<CodeString> ChannelStatus_ { get { return Items.FindAll<CodeString>("003A0205").ToList(); } }
        public SequenceSelector ChannelSourceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0208")); } }
        public List<SequenceSelector> ChannelSourceSequence_ { get { return Items.FindAll<Sequence>("003A0208").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ChannelSourceModifiersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0209")); } }
        public List<SequenceSelector> ChannelSourceModifiersSequence_ { get { return Items.FindAll<Sequence>("003A0209").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SourceWaveformSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A020A")); } }
        public List<SequenceSelector> SourceWaveformSequence_ { get { return Items.FindAll<Sequence>("003A020A").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ChannelDerivationDescription { get { return Items.FindFirst<LongString>("003A020C") as LongString; } }
        public List<LongString> ChannelDerivationDescription_ { get { return Items.FindAll<LongString>("003A020C").ToList(); } }
        public DecimalString ChannelSensitivity { get { return Items.FindFirst<DecimalString>("003A0210") as DecimalString; } }
        public List<DecimalString> ChannelSensitivity_ { get { return Items.FindAll<DecimalString>("003A0210").ToList(); } }
        public SequenceSelector ChannelSensitivityUnitsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0211")); } }
        public List<SequenceSelector> ChannelSensitivityUnitsSequence_ { get { return Items.FindAll<Sequence>("003A0211").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString ChannelSensitivityCorrectionFactor { get { return Items.FindFirst<DecimalString>("003A0212") as DecimalString; } }
        public List<DecimalString> ChannelSensitivityCorrectionFactor_ { get { return Items.FindAll<DecimalString>("003A0212").ToList(); } }
        public DecimalString ChannelBaseline { get { return Items.FindFirst<DecimalString>("003A0213") as DecimalString; } }
        public List<DecimalString> ChannelBaseline_ { get { return Items.FindAll<DecimalString>("003A0213").ToList(); } }
        public DecimalString ChannelTimeSkew { get { return Items.FindFirst<DecimalString>("003A0214") as DecimalString; } }
        public List<DecimalString> ChannelTimeSkew_ { get { return Items.FindAll<DecimalString>("003A0214").ToList(); } }
        public DecimalString ChannelSampleSkew { get { return Items.FindFirst<DecimalString>("003A0215") as DecimalString; } }
        public List<DecimalString> ChannelSampleSkew_ { get { return Items.FindAll<DecimalString>("003A0215").ToList(); } }
        public DecimalString ChannelOffset { get { return Items.FindFirst<DecimalString>("003A0218") as DecimalString; } }
        public List<DecimalString> ChannelOffset_ { get { return Items.FindAll<DecimalString>("003A0218").ToList(); } }
        public UnsignedShort WaveformBitsStored { get { return Items.FindFirst<UnsignedShort>("003A021A") as UnsignedShort; } }
        public List<UnsignedShort> WaveformBitsStored_ { get { return Items.FindAll<UnsignedShort>("003A021A").ToList(); } }
        public DecimalString FilterLowFrequency { get { return Items.FindFirst<DecimalString>("003A0220") as DecimalString; } }
        public List<DecimalString> FilterLowFrequency_ { get { return Items.FindAll<DecimalString>("003A0220").ToList(); } }
        public DecimalString FilterHighFrequency { get { return Items.FindFirst<DecimalString>("003A0221") as DecimalString; } }
        public List<DecimalString> FilterHighFrequency_ { get { return Items.FindAll<DecimalString>("003A0221").ToList(); } }
        public DecimalString NotchFilterFrequency { get { return Items.FindFirst<DecimalString>("003A0222") as DecimalString; } }
        public List<DecimalString> NotchFilterFrequency_ { get { return Items.FindAll<DecimalString>("003A0222").ToList(); } }
        public DecimalString NotchFilterBandwidth { get { return Items.FindFirst<DecimalString>("003A0223") as DecimalString; } }
        public List<DecimalString> NotchFilterBandwidth_ { get { return Items.FindAll<DecimalString>("003A0223").ToList(); } }
        public FloatingPointSingle WaveformDataDisplayScale { get { return Items.FindFirst<FloatingPointSingle>("003A0230") as FloatingPointSingle; } }
        public List<FloatingPointSingle> WaveformDataDisplayScale_ { get { return Items.FindAll<FloatingPointSingle>("003A0230").ToList(); } }
        public UnsignedShort WaveformDisplayBackgroundCIELabValue { get { return Items.FindFirst<UnsignedShort>("003A0231") as UnsignedShort; } }
        public List<UnsignedShort> WaveformDisplayBackgroundCIELabValue_ { get { return Items.FindAll<UnsignedShort>("003A0231").ToList(); } }
        public SequenceSelector WaveformPresentationGroupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0240")); } }
        public List<SequenceSelector> WaveformPresentationGroupSequence_ { get { return Items.FindAll<Sequence>("003A0240").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort PresentationGroupNumber { get { return Items.FindFirst<UnsignedShort>("003A0241") as UnsignedShort; } }
        public List<UnsignedShort> PresentationGroupNumber_ { get { return Items.FindAll<UnsignedShort>("003A0241").ToList(); } }
        public SequenceSelector ChannelDisplaySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0242")); } }
        public List<SequenceSelector> ChannelDisplaySequence_ { get { return Items.FindAll<Sequence>("003A0242").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ChannelRecommendedDisplayCIELabValue { get { return Items.FindFirst<UnsignedShort>("003A0244") as UnsignedShort; } }
        public List<UnsignedShort> ChannelRecommendedDisplayCIELabValue_ { get { return Items.FindAll<UnsignedShort>("003A0244").ToList(); } }
        public FloatingPointSingle ChannelPosition { get { return Items.FindFirst<FloatingPointSingle>("003A0245") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ChannelPosition_ { get { return Items.FindAll<FloatingPointSingle>("003A0245").ToList(); } }
        public CodeString DisplayShadingFlag { get { return Items.FindFirst<CodeString>("003A0246") as CodeString; } }
        public List<CodeString> DisplayShadingFlag_ { get { return Items.FindAll<CodeString>("003A0246").ToList(); } }
        public FloatingPointSingle FractionalChannelDisplayScale { get { return Items.FindFirst<FloatingPointSingle>("003A0247") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FractionalChannelDisplayScale_ { get { return Items.FindAll<FloatingPointSingle>("003A0247").ToList(); } }
        public FloatingPointSingle AbsoluteChannelDisplayScale { get { return Items.FindFirst<FloatingPointSingle>("003A0248") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AbsoluteChannelDisplayScale_ { get { return Items.FindAll<FloatingPointSingle>("003A0248").ToList(); } }
        public SequenceSelector MultiplexedAudioChannelsDescriptionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("003A0300")); } }
        public List<SequenceSelector> MultiplexedAudioChannelsDescriptionCodeSequence_ { get { return Items.FindAll<Sequence>("003A0300").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ChannelIdentificationCode { get { return Items.FindFirst<IntegerString>("003A0301") as IntegerString; } }
        public List<IntegerString> ChannelIdentificationCode_ { get { return Items.FindAll<IntegerString>("003A0301").ToList(); } }
        public CodeString ChannelMode { get { return Items.FindFirst<CodeString>("003A0302") as CodeString; } }
        public List<CodeString> ChannelMode_ { get { return Items.FindAll<CodeString>("003A0302").ToList(); } }
        public ApplicationEntity ScheduledStationAETitle { get { return Items.FindFirst<ApplicationEntity>("00400001") as ApplicationEntity; } }
        public List<ApplicationEntity> ScheduledStationAETitle_ { get { return Items.FindAll<ApplicationEntity>("00400001").ToList(); } }
        public Date ScheduledProcedureStepStartDate { get { return Items.FindFirst<Date>("00400002") as Date; } }
        public List<Date> ScheduledProcedureStepStartDate_ { get { return Items.FindAll<Date>("00400002").ToList(); } }
        public Time ScheduledProcedureStepStartTime { get { return Items.FindFirst<Time>("00400003") as Time; } }
        public List<Time> ScheduledProcedureStepStartTime_ { get { return Items.FindAll<Time>("00400003").ToList(); } }
        public Date ScheduledProcedureStepEndDate { get { return Items.FindFirst<Date>("00400004") as Date; } }
        public List<Date> ScheduledProcedureStepEndDate_ { get { return Items.FindAll<Date>("00400004").ToList(); } }
        public Time ScheduledProcedureStepEndTime { get { return Items.FindFirst<Time>("00400005") as Time; } }
        public List<Time> ScheduledProcedureStepEndTime_ { get { return Items.FindAll<Time>("00400005").ToList(); } }
        public PersonName ScheduledPerformingPhysicianName { get { return Items.FindFirst<PersonName>("00400006") as PersonName; } }
        public List<PersonName> ScheduledPerformingPhysicianName_ { get { return Items.FindAll<PersonName>("00400006").ToList(); } }
        public LongString ScheduledProcedureStepDescription { get { return Items.FindFirst<LongString>("00400007") as LongString; } }
        public List<LongString> ScheduledProcedureStepDescription_ { get { return Items.FindAll<LongString>("00400007").ToList(); } }
        public SequenceSelector ScheduledProtocolCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400008")); } }
        public List<SequenceSelector> ScheduledProtocolCodeSequence_ { get { return Items.FindAll<Sequence>("00400008").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString ScheduledProcedureStepID { get { return Items.FindFirst<ShortString>("00400009") as ShortString; } }
        public List<ShortString> ScheduledProcedureStepID_ { get { return Items.FindAll<ShortString>("00400009").ToList(); } }
        public SequenceSelector StageCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040000A")); } }
        public List<SequenceSelector> StageCodeSequence_ { get { return Items.FindAll<Sequence>("0040000A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledPerformingPhysicianIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040000B")); } }
        public List<SequenceSelector> ScheduledPerformingPhysicianIdentificationSequence_ { get { return Items.FindAll<Sequence>("0040000B").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString ScheduledStationName { get { return Items.FindFirst<ShortString>("00400010") as ShortString; } }
        public List<ShortString> ScheduledStationName_ { get { return Items.FindAll<ShortString>("00400010").ToList(); } }
        public ShortString ScheduledProcedureStepLocation { get { return Items.FindFirst<ShortString>("00400011") as ShortString; } }
        public List<ShortString> ScheduledProcedureStepLocation_ { get { return Items.FindAll<ShortString>("00400011").ToList(); } }
        public LongString PreMedication { get { return Items.FindFirst<LongString>("00400012") as LongString; } }
        public List<LongString> PreMedication_ { get { return Items.FindAll<LongString>("00400012").ToList(); } }
        public CodeString ScheduledProcedureStepStatus { get { return Items.FindFirst<CodeString>("00400020") as CodeString; } }
        public List<CodeString> ScheduledProcedureStepStatus_ { get { return Items.FindAll<CodeString>("00400020").ToList(); } }
        public SequenceSelector OrderPlacerIdentifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400026")); } }
        public List<SequenceSelector> OrderPlacerIdentifierSequence_ { get { return Items.FindAll<Sequence>("00400026").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OrderFillerIdentifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400027")); } }
        public List<SequenceSelector> OrderFillerIdentifierSequence_ { get { return Items.FindAll<Sequence>("00400027").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnlimitedText LocalNamespaceEntityID { get { return Items.FindFirst<UnlimitedText>("00400031") as UnlimitedText; } }
        public List<UnlimitedText> LocalNamespaceEntityID_ { get { return Items.FindAll<UnlimitedText>("00400031").ToList(); } }
        public UnlimitedText UniversalEntityID { get { return Items.FindFirst<UnlimitedText>("00400032") as UnlimitedText; } }
        public List<UnlimitedText> UniversalEntityID_ { get { return Items.FindAll<UnlimitedText>("00400032").ToList(); } }
        public CodeString UniversalEntityIDType { get { return Items.FindFirst<CodeString>("00400033") as CodeString; } }
        public List<CodeString> UniversalEntityIDType_ { get { return Items.FindAll<CodeString>("00400033").ToList(); } }
        public CodeString IdentifierTypeCode { get { return Items.FindFirst<CodeString>("00400035") as CodeString; } }
        public List<CodeString> IdentifierTypeCode_ { get { return Items.FindAll<CodeString>("00400035").ToList(); } }
        public SequenceSelector AssigningFacilitySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400036")); } }
        public List<SequenceSelector> AssigningFacilitySequence_ { get { return Items.FindAll<Sequence>("00400036").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AssigningJurisdictionCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400039")); } }
        public List<SequenceSelector> AssigningJurisdictionCodeSequence_ { get { return Items.FindAll<Sequence>("00400039").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AssigningAgencyOrDepartmentCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040003A")); } }
        public List<SequenceSelector> AssigningAgencyOrDepartmentCodeSequence_ { get { return Items.FindAll<Sequence>("0040003A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledProcedureStepSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400100")); } }
        public List<SequenceSelector> ScheduledProcedureStepSequence_ { get { return Items.FindAll<Sequence>("00400100").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedNonImageCompositeSOPInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400220")); } }
        public List<SequenceSelector> ReferencedNonImageCompositeSOPInstanceSequence_ { get { return Items.FindAll<Sequence>("00400220").Select(s => new SequenceSelector(s)).ToList(); } }
        public ApplicationEntity PerformedStationAETitle { get { return Items.FindFirst<ApplicationEntity>("00400241") as ApplicationEntity; } }
        public List<ApplicationEntity> PerformedStationAETitle_ { get { return Items.FindAll<ApplicationEntity>("00400241").ToList(); } }
        public ShortString PerformedStationName { get { return Items.FindFirst<ShortString>("00400242") as ShortString; } }
        public List<ShortString> PerformedStationName_ { get { return Items.FindAll<ShortString>("00400242").ToList(); } }
        public ShortString PerformedLocation { get { return Items.FindFirst<ShortString>("00400243") as ShortString; } }
        public List<ShortString> PerformedLocation_ { get { return Items.FindAll<ShortString>("00400243").ToList(); } }
        public Date PerformedProcedureStepStartDate { get { return Items.FindFirst<Date>("00400244") as Date; } }
        public List<Date> PerformedProcedureStepStartDate_ { get { return Items.FindAll<Date>("00400244").ToList(); } }
        public Time PerformedProcedureStepStartTime { get { return Items.FindFirst<Time>("00400245") as Time; } }
        public List<Time> PerformedProcedureStepStartTime_ { get { return Items.FindAll<Time>("00400245").ToList(); } }
        public Date PerformedProcedureStepEndDate { get { return Items.FindFirst<Date>("00400250") as Date; } }
        public List<Date> PerformedProcedureStepEndDate_ { get { return Items.FindAll<Date>("00400250").ToList(); } }
        public Time PerformedProcedureStepEndTime { get { return Items.FindFirst<Time>("00400251") as Time; } }
        public List<Time> PerformedProcedureStepEndTime_ { get { return Items.FindAll<Time>("00400251").ToList(); } }
        public CodeString PerformedProcedureStepStatus { get { return Items.FindFirst<CodeString>("00400252") as CodeString; } }
        public List<CodeString> PerformedProcedureStepStatus_ { get { return Items.FindAll<CodeString>("00400252").ToList(); } }
        public ShortString PerformedProcedureStepID { get { return Items.FindFirst<ShortString>("00400253") as ShortString; } }
        public List<ShortString> PerformedProcedureStepID_ { get { return Items.FindAll<ShortString>("00400253").ToList(); } }
        public LongString PerformedProcedureStepDescription { get { return Items.FindFirst<LongString>("00400254") as LongString; } }
        public List<LongString> PerformedProcedureStepDescription_ { get { return Items.FindAll<LongString>("00400254").ToList(); } }
        public LongString PerformedProcedureTypeDescription { get { return Items.FindFirst<LongString>("00400255") as LongString; } }
        public List<LongString> PerformedProcedureTypeDescription_ { get { return Items.FindAll<LongString>("00400255").ToList(); } }
        public SequenceSelector PerformedProtocolCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400260")); } }
        public List<SequenceSelector> PerformedProtocolCodeSequence_ { get { return Items.FindAll<Sequence>("00400260").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PerformedProtocolType { get { return Items.FindFirst<CodeString>("00400261") as CodeString; } }
        public List<CodeString> PerformedProtocolType_ { get { return Items.FindAll<CodeString>("00400261").ToList(); } }
        public SequenceSelector ScheduledStepAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400270")); } }
        public List<SequenceSelector> ScheduledStepAttributesSequence_ { get { return Items.FindAll<Sequence>("00400270").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RequestAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400275")); } }
        public List<SequenceSelector> RequestAttributesSequence_ { get { return Items.FindAll<Sequence>("00400275").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText CommentsOnThePerformedProcedureStep { get { return Items.FindFirst<ShortText>("00400280") as ShortText; } }
        public List<ShortText> CommentsOnThePerformedProcedureStep_ { get { return Items.FindAll<ShortText>("00400280").ToList(); } }
        public SequenceSelector PerformedProcedureStepDiscontinuationReasonCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400281")); } }
        public List<SequenceSelector> PerformedProcedureStepDiscontinuationReasonCodeSequence_ { get { return Items.FindAll<Sequence>("00400281").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector QuantitySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400293")); } }
        public List<SequenceSelector> QuantitySequence_ { get { return Items.FindAll<Sequence>("00400293").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString Quantity { get { return Items.FindFirst<DecimalString>("00400294") as DecimalString; } }
        public List<DecimalString> Quantity_ { get { return Items.FindAll<DecimalString>("00400294").ToList(); } }
        public SequenceSelector MeasuringUnitsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400295")); } }
        public List<SequenceSelector> MeasuringUnitsSequence_ { get { return Items.FindAll<Sequence>("00400295").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BillingItemSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400296")); } }
        public List<SequenceSelector> BillingItemSequence_ { get { return Items.FindAll<Sequence>("00400296").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort TotalTimeOfFluoroscopy { get { return Items.FindFirst<UnsignedShort>("00400300") as UnsignedShort; } }
        public List<UnsignedShort> TotalTimeOfFluoroscopy_ { get { return Items.FindAll<UnsignedShort>("00400300").ToList(); } }
        public UnsignedShort TotalNumberOfExposures { get { return Items.FindFirst<UnsignedShort>("00400301") as UnsignedShort; } }
        public List<UnsignedShort> TotalNumberOfExposures_ { get { return Items.FindAll<UnsignedShort>("00400301").ToList(); } }
        public UnsignedShort EntranceDose { get { return Items.FindFirst<UnsignedShort>("00400302") as UnsignedShort; } }
        public List<UnsignedShort> EntranceDose_ { get { return Items.FindAll<UnsignedShort>("00400302").ToList(); } }
        public UnsignedShort ExposedArea { get { return Items.FindFirst<UnsignedShort>("00400303") as UnsignedShort; } }
        public List<UnsignedShort> ExposedArea_ { get { return Items.FindAll<UnsignedShort>("00400303").ToList(); } }
        public DecimalString DistanceSourceToEntrance { get { return Items.FindFirst<DecimalString>("00400306") as DecimalString; } }
        public List<DecimalString> DistanceSourceToEntrance_ { get { return Items.FindAll<DecimalString>("00400306").ToList(); } }
        public DecimalString DistanceSourceToSupportRetired { get { return Items.FindFirst<DecimalString>("00400307") as DecimalString; } }
        public List<DecimalString> DistanceSourceToSupportRetired_ { get { return Items.FindAll<DecimalString>("00400307").ToList(); } }
        public SequenceSelector ExposureDoseSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040030E")); } }
        public List<SequenceSelector> ExposureDoseSequence_ { get { return Items.FindAll<Sequence>("0040030E").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText CommentsOnRadiationDose { get { return Items.FindFirst<ShortText>("00400310") as ShortText; } }
        public List<ShortText> CommentsOnRadiationDose_ { get { return Items.FindAll<ShortText>("00400310").ToList(); } }
        public DecimalString XRayOutput { get { return Items.FindFirst<DecimalString>("00400312") as DecimalString; } }
        public List<DecimalString> XRayOutput_ { get { return Items.FindAll<DecimalString>("00400312").ToList(); } }
        public DecimalString HalfValueLayer { get { return Items.FindFirst<DecimalString>("00400314") as DecimalString; } }
        public List<DecimalString> HalfValueLayer_ { get { return Items.FindAll<DecimalString>("00400314").ToList(); } }
        public DecimalString OrganDose { get { return Items.FindFirst<DecimalString>("00400316") as DecimalString; } }
        public List<DecimalString> OrganDose_ { get { return Items.FindAll<DecimalString>("00400316").ToList(); } }
        public CodeString OrganExposed { get { return Items.FindFirst<CodeString>("00400318") as CodeString; } }
        public List<CodeString> OrganExposed_ { get { return Items.FindAll<CodeString>("00400318").ToList(); } }
        public SequenceSelector BillingProcedureStepSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400320")); } }
        public List<SequenceSelector> BillingProcedureStepSequence_ { get { return Items.FindAll<Sequence>("00400320").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FilmConsumptionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400321")); } }
        public List<SequenceSelector> FilmConsumptionSequence_ { get { return Items.FindAll<Sequence>("00400321").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BillingSuppliesAndDevicesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400324")); } }
        public List<SequenceSelector> BillingSuppliesAndDevicesSequence_ { get { return Items.FindAll<Sequence>("00400324").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedProcedureStepSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400330")); } }
        public List<SequenceSelector> ReferencedProcedureStepSequenceRetired_ { get { return Items.FindAll<Sequence>("00400330").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedSeriesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400340")); } }
        public List<SequenceSelector> PerformedSeriesSequence_ { get { return Items.FindAll<Sequence>("00400340").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongText CommentsOnTheScheduledProcedureStep { get { return Items.FindFirst<LongText>("00400400") as LongText; } }
        public List<LongText> CommentsOnTheScheduledProcedureStep_ { get { return Items.FindAll<LongText>("00400400").ToList(); } }
        public SequenceSelector ProtocolContextSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400440")); } }
        public List<SequenceSelector> ProtocolContextSequence_ { get { return Items.FindAll<Sequence>("00400440").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContentItemModifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400441")); } }
        public List<SequenceSelector> ContentItemModifierSequence_ { get { return Items.FindAll<Sequence>("00400441").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledSpecimenSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400500")); } }
        public List<SequenceSelector> ScheduledSpecimenSequence_ { get { return Items.FindAll<Sequence>("00400500").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SpecimenAccessionNumberRetired { get { return Items.FindFirst<LongString>("0040050A") as LongString; } }
        public List<LongString> SpecimenAccessionNumberRetired_ { get { return Items.FindAll<LongString>("0040050A").ToList(); } }
        public LongString ContainerIdentifier { get { return Items.FindFirst<LongString>("00400512") as LongString; } }
        public List<LongString> ContainerIdentifier_ { get { return Items.FindAll<LongString>("00400512").ToList(); } }
        public SequenceSelector IssuerOfTheContainerIdentifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400513")); } }
        public List<SequenceSelector> IssuerOfTheContainerIdentifierSequence_ { get { return Items.FindAll<Sequence>("00400513").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AlternateContainerIdentifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400515")); } }
        public List<SequenceSelector> AlternateContainerIdentifierSequence_ { get { return Items.FindAll<Sequence>("00400515").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContainerTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400518")); } }
        public List<SequenceSelector> ContainerTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00400518").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ContainerDescription { get { return Items.FindFirst<LongString>("0040051A") as LongString; } }
        public List<LongString> ContainerDescription_ { get { return Items.FindAll<LongString>("0040051A").ToList(); } }
        public SequenceSelector ContainerComponentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400520")); } }
        public List<SequenceSelector> ContainerComponentSequence_ { get { return Items.FindAll<Sequence>("00400520").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SpecimenSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400550")); } }
        public List<SequenceSelector> SpecimenSequenceRetired_ { get { return Items.FindAll<Sequence>("00400550").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SpecimenIdentifier { get { return Items.FindFirst<LongString>("00400551") as LongString; } }
        public List<LongString> SpecimenIdentifier_ { get { return Items.FindAll<LongString>("00400551").ToList(); } }
        public SequenceSelector SpecimenDescriptionSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400552")); } }
        public List<SequenceSelector> SpecimenDescriptionSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("00400552").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText SpecimenDescriptionTrialRetired { get { return Items.FindFirst<ShortText>("00400553") as ShortText; } }
        public List<ShortText> SpecimenDescriptionTrialRetired_ { get { return Items.FindAll<ShortText>("00400553").ToList(); } }
        public UniqueIdentifier SpecimenUID { get { return Items.FindFirst<UniqueIdentifier>("00400554") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SpecimenUID_ { get { return Items.FindAll<UniqueIdentifier>("00400554").ToList(); } }
        public SequenceSelector AcquisitionContextSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400555")); } }
        public List<SequenceSelector> AcquisitionContextSequence_ { get { return Items.FindAll<Sequence>("00400555").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText AcquisitionContextDescription { get { return Items.FindFirst<ShortText>("00400556") as ShortText; } }
        public List<ShortText> AcquisitionContextDescription_ { get { return Items.FindAll<ShortText>("00400556").ToList(); } }
        public SequenceSelector SpecimenDescriptionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400560")); } }
        public List<SequenceSelector> SpecimenDescriptionSequence_ { get { return Items.FindAll<Sequence>("00400560").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IssuerOfTheSpecimenIdentifierSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400562")); } }
        public List<SequenceSelector> IssuerOfTheSpecimenIdentifierSequence_ { get { return Items.FindAll<Sequence>("00400562").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SpecimenTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040059A")); } }
        public List<SequenceSelector> SpecimenTypeCodeSequence_ { get { return Items.FindAll<Sequence>("0040059A").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SpecimenShortDescription { get { return Items.FindFirst<LongString>("00400600") as LongString; } }
        public List<LongString> SpecimenShortDescription_ { get { return Items.FindAll<LongString>("00400600").ToList(); } }
        public UnlimitedText SpecimenDetailedDescription { get { return Items.FindFirst<UnlimitedText>("00400602") as UnlimitedText; } }
        public List<UnlimitedText> SpecimenDetailedDescription_ { get { return Items.FindAll<UnlimitedText>("00400602").ToList(); } }
        public SequenceSelector SpecimenPreparationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400610")); } }
        public List<SequenceSelector> SpecimenPreparationSequence_ { get { return Items.FindAll<Sequence>("00400610").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SpecimenPreparationStepContentItemSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400612")); } }
        public List<SequenceSelector> SpecimenPreparationStepContentItemSequence_ { get { return Items.FindAll<Sequence>("00400612").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SpecimenLocalizationContentItemSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00400620")); } }
        public List<SequenceSelector> SpecimenLocalizationContentItemSequence_ { get { return Items.FindAll<Sequence>("00400620").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SlideIdentifierRetired { get { return Items.FindFirst<LongString>("004006FA") as LongString; } }
        public List<LongString> SlideIdentifierRetired_ { get { return Items.FindAll<LongString>("004006FA").ToList(); } }
        public SequenceSelector ImageCenterPointCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040071A")); } }
        public List<SequenceSelector> ImageCenterPointCoordinatesSequence_ { get { return Items.FindAll<Sequence>("0040071A").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString XOffsetInSlideCoordinateSystem { get { return Items.FindFirst<DecimalString>("0040072A") as DecimalString; } }
        public List<DecimalString> XOffsetInSlideCoordinateSystem_ { get { return Items.FindAll<DecimalString>("0040072A").ToList(); } }
        public DecimalString YOffsetInSlideCoordinateSystem { get { return Items.FindFirst<DecimalString>("0040073A") as DecimalString; } }
        public List<DecimalString> YOffsetInSlideCoordinateSystem_ { get { return Items.FindAll<DecimalString>("0040073A").ToList(); } }
        public DecimalString ZOffsetInSlideCoordinateSystem { get { return Items.FindFirst<DecimalString>("0040074A") as DecimalString; } }
        public List<DecimalString> ZOffsetInSlideCoordinateSystem_ { get { return Items.FindAll<DecimalString>("0040074A").ToList(); } }
        public SequenceSelector PixelSpacingSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("004008D8")); } }
        public List<SequenceSelector> PixelSpacingSequenceRetired_ { get { return Items.FindAll<Sequence>("004008D8").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CoordinateSystemAxisCodeSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("004008DA")); } }
        public List<SequenceSelector> CoordinateSystemAxisCodeSequenceRetired_ { get { return Items.FindAll<Sequence>("004008DA").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MeasurementUnitsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("004008EA")); } }
        public List<SequenceSelector> MeasurementUnitsCodeSequence_ { get { return Items.FindAll<Sequence>("004008EA").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VitalStainCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("004009F8")); } }
        public List<SequenceSelector> VitalStainCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("004009F8").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString RequestedProcedureID { get { return Items.FindFirst<ShortString>("00401001") as ShortString; } }
        public List<ShortString> RequestedProcedureID_ { get { return Items.FindAll<ShortString>("00401001").ToList(); } }
        public LongString ReasonForTheRequestedProcedure { get { return Items.FindFirst<LongString>("00401002") as LongString; } }
        public List<LongString> ReasonForTheRequestedProcedure_ { get { return Items.FindAll<LongString>("00401002").ToList(); } }
        public ShortString RequestedProcedurePriority { get { return Items.FindFirst<ShortString>("00401003") as ShortString; } }
        public List<ShortString> RequestedProcedurePriority_ { get { return Items.FindAll<ShortString>("00401003").ToList(); } }
        public LongString PatientTransportArrangements { get { return Items.FindFirst<LongString>("00401004") as LongString; } }
        public List<LongString> PatientTransportArrangements_ { get { return Items.FindAll<LongString>("00401004").ToList(); } }
        public LongString RequestedProcedureLocation { get { return Items.FindFirst<LongString>("00401005") as LongString; } }
        public List<LongString> RequestedProcedureLocation_ { get { return Items.FindAll<LongString>("00401005").ToList(); } }
        public ShortString PlacerOrderNumberProcedureRetired { get { return Items.FindFirst<ShortString>("00401006") as ShortString; } }
        public List<ShortString> PlacerOrderNumberProcedureRetired_ { get { return Items.FindAll<ShortString>("00401006").ToList(); } }
        public ShortString FillerOrderNumberProcedureRetired { get { return Items.FindFirst<ShortString>("00401007") as ShortString; } }
        public List<ShortString> FillerOrderNumberProcedureRetired_ { get { return Items.FindAll<ShortString>("00401007").ToList(); } }
        public LongString ConfidentialityCode { get { return Items.FindFirst<LongString>("00401008") as LongString; } }
        public List<LongString> ConfidentialityCode_ { get { return Items.FindAll<LongString>("00401008").ToList(); } }
        public ShortString ReportingPriority { get { return Items.FindFirst<ShortString>("00401009") as ShortString; } }
        public List<ShortString> ReportingPriority_ { get { return Items.FindAll<ShortString>("00401009").ToList(); } }
        public SequenceSelector ReasonForRequestedProcedureCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040100A")); } }
        public List<SequenceSelector> ReasonForRequestedProcedureCodeSequence_ { get { return Items.FindAll<Sequence>("0040100A").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName NamesOfIntendedRecipientsOfResults { get { return Items.FindFirst<PersonName>("00401010") as PersonName; } }
        public List<PersonName> NamesOfIntendedRecipientsOfResults_ { get { return Items.FindAll<PersonName>("00401010").ToList(); } }
        public SequenceSelector IntendedRecipientsOfResultsIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00401011")); } }
        public List<SequenceSelector> IntendedRecipientsOfResultsIdentificationSequence_ { get { return Items.FindAll<Sequence>("00401011").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReasonForPerformedProcedureCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00401012")); } }
        public List<SequenceSelector> ReasonForPerformedProcedureCodeSequence_ { get { return Items.FindAll<Sequence>("00401012").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString RequestedProcedureDescriptionTrialRetired { get { return Items.FindFirst<LongString>("00401060") as LongString; } }
        public List<LongString> RequestedProcedureDescriptionTrialRetired_ { get { return Items.FindAll<LongString>("00401060").ToList(); } }
        public SequenceSelector PersonIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00401101")); } }
        public List<SequenceSelector> PersonIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("00401101").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText PersonAddress { get { return Items.FindFirst<ShortText>("00401102") as ShortText; } }
        public List<ShortText> PersonAddress_ { get { return Items.FindAll<ShortText>("00401102").ToList(); } }
        public LongString PersonTelephoneNumbers { get { return Items.FindFirst<LongString>("00401103") as LongString; } }
        public List<LongString> PersonTelephoneNumbers_ { get { return Items.FindAll<LongString>("00401103").ToList(); } }
        public LongText RequestedProcedureComments { get { return Items.FindFirst<LongText>("00401400") as LongText; } }
        public List<LongText> RequestedProcedureComments_ { get { return Items.FindAll<LongText>("00401400").ToList(); } }
        public LongString ReasonForTheImagingServiceRequestRetired { get { return Items.FindFirst<LongString>("00402001") as LongString; } }
        public List<LongString> ReasonForTheImagingServiceRequestRetired_ { get { return Items.FindAll<LongString>("00402001").ToList(); } }
        public Date IssueDateOfImagingServiceRequest { get { return Items.FindFirst<Date>("00402004") as Date; } }
        public List<Date> IssueDateOfImagingServiceRequest_ { get { return Items.FindAll<Date>("00402004").ToList(); } }
        public Time IssueTimeOfImagingServiceRequest { get { return Items.FindFirst<Time>("00402005") as Time; } }
        public List<Time> IssueTimeOfImagingServiceRequest_ { get { return Items.FindAll<Time>("00402005").ToList(); } }
        public ShortString PlacerOrderNumberImagingServiceRequestRetired { get { return Items.FindFirst<ShortString>("00402006") as ShortString; } }
        public List<ShortString> PlacerOrderNumberImagingServiceRequestRetired_ { get { return Items.FindAll<ShortString>("00402006").ToList(); } }
        public ShortString FillerOrderNumberImagingServiceRequestRetired { get { return Items.FindFirst<ShortString>("00402007") as ShortString; } }
        public List<ShortString> FillerOrderNumberImagingServiceRequestRetired_ { get { return Items.FindAll<ShortString>("00402007").ToList(); } }
        public PersonName OrderEnteredBy { get { return Items.FindFirst<PersonName>("00402008") as PersonName; } }
        public List<PersonName> OrderEnteredBy_ { get { return Items.FindAll<PersonName>("00402008").ToList(); } }
        public ShortString OrderEntererLocation { get { return Items.FindFirst<ShortString>("00402009") as ShortString; } }
        public List<ShortString> OrderEntererLocation_ { get { return Items.FindAll<ShortString>("00402009").ToList(); } }
        public ShortString OrderCallbackPhoneNumber { get { return Items.FindFirst<ShortString>("00402010") as ShortString; } }
        public List<ShortString> OrderCallbackPhoneNumber_ { get { return Items.FindAll<ShortString>("00402010").ToList(); } }
        public LongString PlacerOrderNumberImagingServiceRequest { get { return Items.FindFirst<LongString>("00402016") as LongString; } }
        public List<LongString> PlacerOrderNumberImagingServiceRequest_ { get { return Items.FindAll<LongString>("00402016").ToList(); } }
        public LongString FillerOrderNumberImagingServiceRequest { get { return Items.FindFirst<LongString>("00402017") as LongString; } }
        public List<LongString> FillerOrderNumberImagingServiceRequest_ { get { return Items.FindAll<LongString>("00402017").ToList(); } }
        public LongText ImagingServiceRequestComments { get { return Items.FindFirst<LongText>("00402400") as LongText; } }
        public List<LongText> ImagingServiceRequestComments_ { get { return Items.FindAll<LongText>("00402400").ToList(); } }
        public LongString ConfidentialityConstraintOnPatientDataDescription { get { return Items.FindFirst<LongString>("00403001") as LongString; } }
        public List<LongString> ConfidentialityConstraintOnPatientDataDescription_ { get { return Items.FindAll<LongString>("00403001").ToList(); } }
        public CodeString GeneralPurposeScheduledProcedureStepStatus { get { return Items.FindFirst<CodeString>("00404001") as CodeString; } }
        public List<CodeString> GeneralPurposeScheduledProcedureStepStatus_ { get { return Items.FindAll<CodeString>("00404001").ToList(); } }
        public CodeString GeneralPurposePerformedProcedureStepStatus { get { return Items.FindFirst<CodeString>("00404002") as CodeString; } }
        public List<CodeString> GeneralPurposePerformedProcedureStepStatus_ { get { return Items.FindAll<CodeString>("00404002").ToList(); } }
        public CodeString GeneralPurposeScheduledProcedureStepPriority { get { return Items.FindFirst<CodeString>("00404003") as CodeString; } }
        public List<CodeString> GeneralPurposeScheduledProcedureStepPriority_ { get { return Items.FindAll<CodeString>("00404003").ToList(); } }
        public SequenceSelector ScheduledProcessingApplicationsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404004")); } }
        public List<SequenceSelector> ScheduledProcessingApplicationsCodeSequence_ { get { return Items.FindAll<Sequence>("00404004").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime ScheduledProcedureStepStartDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404005") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ScheduledProcedureStepStartDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404005").ToList(); } }
        public CodeString MultipleCopiesFlag { get { return Items.FindFirst<CodeString>("00404006") as CodeString; } }
        public List<CodeString> MultipleCopiesFlag_ { get { return Items.FindAll<CodeString>("00404006").ToList(); } }
        public SequenceSelector PerformedProcessingApplicationsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404007")); } }
        public List<SequenceSelector> PerformedProcessingApplicationsCodeSequence_ { get { return Items.FindAll<Sequence>("00404007").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector HumanPerformerCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404009")); } }
        public List<SequenceSelector> HumanPerformerCodeSequence_ { get { return Items.FindAll<Sequence>("00404009").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime ScheduledProcedureStepModificationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404010") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ScheduledProcedureStepModificationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404010").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ExpectedCompletionDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404011") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ExpectedCompletionDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404011").ToList(); } }
        public SequenceSelector ResultingGeneralPurposePerformedProcedureStepsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404015")); } }
        public List<SequenceSelector> ResultingGeneralPurposePerformedProcedureStepsSequence_ { get { return Items.FindAll<Sequence>("00404015").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedGeneralPurposeScheduledProcedureStepSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404016")); } }
        public List<SequenceSelector> ReferencedGeneralPurposeScheduledProcedureStepSequence_ { get { return Items.FindAll<Sequence>("00404016").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledWorkitemCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404018")); } }
        public List<SequenceSelector> ScheduledWorkitemCodeSequence_ { get { return Items.FindAll<Sequence>("00404018").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedWorkitemCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404019")); } }
        public List<SequenceSelector> PerformedWorkitemCodeSequence_ { get { return Items.FindAll<Sequence>("00404019").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString InputAvailabilityFlag { get { return Items.FindFirst<CodeString>("00404020") as CodeString; } }
        public List<CodeString> InputAvailabilityFlag_ { get { return Items.FindAll<CodeString>("00404020").ToList(); } }
        public SequenceSelector InputInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404021")); } }
        public List<SequenceSelector> InputInformationSequence_ { get { return Items.FindAll<Sequence>("00404021").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelevantInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404022")); } }
        public List<SequenceSelector> RelevantInformationSequence_ { get { return Items.FindAll<Sequence>("00404022").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier ReferencedGeneralPurposeScheduledProcedureStepTransactionUID { get { return Items.FindFirst<UniqueIdentifier>("00404023") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedGeneralPurposeScheduledProcedureStepTransactionUID_ { get { return Items.FindAll<UniqueIdentifier>("00404023").ToList(); } }
        public SequenceSelector ScheduledStationNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404025")); } }
        public List<SequenceSelector> ScheduledStationNameCodeSequence_ { get { return Items.FindAll<Sequence>("00404025").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledStationClassCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404026")); } }
        public List<SequenceSelector> ScheduledStationClassCodeSequence_ { get { return Items.FindAll<Sequence>("00404026").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledStationGeographicLocationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404027")); } }
        public List<SequenceSelector> ScheduledStationGeographicLocationCodeSequence_ { get { return Items.FindAll<Sequence>("00404027").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedStationNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404028")); } }
        public List<SequenceSelector> PerformedStationNameCodeSequence_ { get { return Items.FindAll<Sequence>("00404028").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedStationClassCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404029")); } }
        public List<SequenceSelector> PerformedStationClassCodeSequence_ { get { return Items.FindAll<Sequence>("00404029").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedStationGeographicLocationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404030")); } }
        public List<SequenceSelector> PerformedStationGeographicLocationCodeSequence_ { get { return Items.FindAll<Sequence>("00404030").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RequestedSubsequentWorkitemCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404031")); } }
        public List<SequenceSelector> RequestedSubsequentWorkitemCodeSequence_ { get { return Items.FindAll<Sequence>("00404031").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector NonDICOMOutputCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404032")); } }
        public List<SequenceSelector> NonDICOMOutputCodeSequence_ { get { return Items.FindAll<Sequence>("00404032").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OutputInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404033")); } }
        public List<SequenceSelector> OutputInformationSequence_ { get { return Items.FindAll<Sequence>("00404033").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ScheduledHumanPerformersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404034")); } }
        public List<SequenceSelector> ScheduledHumanPerformersSequence_ { get { return Items.FindAll<Sequence>("00404034").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ActualHumanPerformersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00404035")); } }
        public List<SequenceSelector> ActualHumanPerformersSequence_ { get { return Items.FindAll<Sequence>("00404035").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString HumanPerformerOrganization { get { return Items.FindFirst<LongString>("00404036") as LongString; } }
        public List<LongString> HumanPerformerOrganization_ { get { return Items.FindAll<LongString>("00404036").ToList(); } }
        public PersonName HumanPerformerName { get { return Items.FindFirst<PersonName>("00404037") as PersonName; } }
        public List<PersonName> HumanPerformerName_ { get { return Items.FindAll<PersonName>("00404037").ToList(); } }
        public CodeString RawDataHandling { get { return Items.FindFirst<CodeString>("00404040") as CodeString; } }
        public List<CodeString> RawDataHandling_ { get { return Items.FindAll<CodeString>("00404040").ToList(); } }
        public CodeString InputReadinessState { get { return Items.FindFirst<CodeString>("00404041") as CodeString; } }
        public List<CodeString> InputReadinessState_ { get { return Items.FindAll<CodeString>("00404041").ToList(); } }
        public EvilDICOM.Core.Element.DateTime PerformedProcedureStepStartDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404050") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> PerformedProcedureStepStartDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404050").ToList(); } }
        public EvilDICOM.Core.Element.DateTime PerformedProcedureStepEndDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404051") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> PerformedProcedureStepEndDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404051").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ProcedureStepCancellationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00404052") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ProcedureStepCancellationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00404052").ToList(); } }
        public DecimalString EntranceDoseInmGy { get { return Items.FindFirst<DecimalString>("00408302") as DecimalString; } }
        public List<DecimalString> EntranceDoseInmGy_ { get { return Items.FindAll<DecimalString>("00408302").ToList(); } }
        public SequenceSelector ReferencedImageRealWorldValueMappingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00409094")); } }
        public List<SequenceSelector> ReferencedImageRealWorldValueMappingSequence_ { get { return Items.FindAll<Sequence>("00409094").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RealWorldValueMappingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00409096")); } }
        public List<SequenceSelector> RealWorldValueMappingSequence_ { get { return Items.FindAll<Sequence>("00409096").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PixelValueMappingCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00409098")); } }
        public List<SequenceSelector> PixelValueMappingCodeSequence_ { get { return Items.FindAll<Sequence>("00409098").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString LUTLabel { get { return Items.FindFirst<ShortString>("00409210") as ShortString; } }
        public List<ShortString> LUTLabel_ { get { return Items.FindAll<ShortString>("00409210").ToList(); } }
        public UnsignedShort RealWorldValueLastValueMapped { get { return Items.FindFirst<UnsignedShort>("00409211") as UnsignedShort; } }
        public List<UnsignedShort> RealWorldValueLastValueMapped_ { get { return Items.FindAll<UnsignedShort>("00409211").ToList(); } }
        public FloatingPointDouble RealWorldValueLUTData { get { return Items.FindFirst<FloatingPointDouble>("00409212") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RealWorldValueLUTData_ { get { return Items.FindAll<FloatingPointDouble>("00409212").ToList(); } }
        public UnsignedShort RealWorldValueFirstValueMapped { get { return Items.FindFirst<UnsignedShort>("00409216") as UnsignedShort; } }
        public List<UnsignedShort> RealWorldValueFirstValueMapped_ { get { return Items.FindAll<UnsignedShort>("00409216").ToList(); } }
        public FloatingPointDouble RealWorldValueIntercept { get { return Items.FindFirst<FloatingPointDouble>("00409224") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RealWorldValueIntercept_ { get { return Items.FindAll<FloatingPointDouble>("00409224").ToList(); } }
        public FloatingPointDouble RealWorldValueSlope { get { return Items.FindFirst<FloatingPointDouble>("00409225") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RealWorldValueSlope_ { get { return Items.FindAll<FloatingPointDouble>("00409225").ToList(); } }
        public CodeString FindingsFlagTrialRetired { get { return Items.FindFirst<CodeString>("0040A007") as CodeString; } }
        public List<CodeString> FindingsFlagTrialRetired_ { get { return Items.FindAll<CodeString>("0040A007").ToList(); } }
        public CodeString RelationshipType { get { return Items.FindFirst<CodeString>("0040A010") as CodeString; } }
        public List<CodeString> RelationshipType_ { get { return Items.FindAll<CodeString>("0040A010").ToList(); } }
        public SequenceSelector FindingsSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A020")); } }
        public List<SequenceSelector> FindingsSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A020").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier FindingsGroupUIDTrialRetired { get { return Items.FindFirst<UniqueIdentifier>("0040A021") as UniqueIdentifier; } }
        public List<UniqueIdentifier> FindingsGroupUIDTrialRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040A021").ToList(); } }
        public UniqueIdentifier ReferencedFindingsGroupUIDTrialRetired { get { return Items.FindFirst<UniqueIdentifier>("0040A022") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedFindingsGroupUIDTrialRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040A022").ToList(); } }
        public Date FindingsGroupRecordingDateTrialRetired { get { return Items.FindFirst<Date>("0040A023") as Date; } }
        public List<Date> FindingsGroupRecordingDateTrialRetired_ { get { return Items.FindAll<Date>("0040A023").ToList(); } }
        public Time FindingsGroupRecordingTimeTrialRetired { get { return Items.FindFirst<Time>("0040A024") as Time; } }
        public List<Time> FindingsGroupRecordingTimeTrialRetired_ { get { return Items.FindAll<Time>("0040A024").ToList(); } }
        public SequenceSelector FindingsSourceCategoryCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A026")); } }
        public List<SequenceSelector> FindingsSourceCategoryCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A026").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString VerifyingOrganization { get { return Items.FindFirst<LongString>("0040A027") as LongString; } }
        public List<LongString> VerifyingOrganization_ { get { return Items.FindAll<LongString>("0040A027").ToList(); } }
        public SequenceSelector DocumentingOrganizationIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A028")); } }
        public List<SequenceSelector> DocumentingOrganizationIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A028").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime VerificationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040A030") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> VerificationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040A030").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ObservationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040A032") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ObservationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040A032").ToList(); } }
        public CodeString ValueType { get { return Items.FindFirst<CodeString>("0040A040") as CodeString; } }
        public List<CodeString> ValueType_ { get { return Items.FindAll<CodeString>("0040A040").ToList(); } }
        public SequenceSelector ConceptNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A043")); } }
        public List<SequenceSelector> ConceptNameCodeSequence_ { get { return Items.FindAll<Sequence>("0040A043").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString MeasurementPrecisionDescriptionTrialRetired { get { return Items.FindFirst<LongString>("0040A047") as LongString; } }
        public List<LongString> MeasurementPrecisionDescriptionTrialRetired_ { get { return Items.FindAll<LongString>("0040A047").ToList(); } }
        public CodeString ContinuityOfContent { get { return Items.FindFirst<CodeString>("0040A050") as CodeString; } }
        public List<CodeString> ContinuityOfContent_ { get { return Items.FindAll<CodeString>("0040A050").ToList(); } }
        public CodeString UrgencyOrPriorityAlertsTrialRetired { get { return Items.FindFirst<CodeString>("0040A057") as CodeString; } }
        public List<CodeString> UrgencyOrPriorityAlertsTrialRetired_ { get { return Items.FindAll<CodeString>("0040A057").ToList(); } }
        public LongString SequencingIndicatorTrialRetired { get { return Items.FindFirst<LongString>("0040A060") as LongString; } }
        public List<LongString> SequencingIndicatorTrialRetired_ { get { return Items.FindAll<LongString>("0040A060").ToList(); } }
        public SequenceSelector DocumentIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A066")); } }
        public List<SequenceSelector> DocumentIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A066").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName DocumentAuthorTrialRetired { get { return Items.FindFirst<PersonName>("0040A067") as PersonName; } }
        public List<PersonName> DocumentAuthorTrialRetired_ { get { return Items.FindAll<PersonName>("0040A067").ToList(); } }
        public SequenceSelector DocumentAuthorIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A068")); } }
        public List<SequenceSelector> DocumentAuthorIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A068").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A070")); } }
        public List<SequenceSelector> IdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A070").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VerifyingObserverSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A073")); } }
        public List<SequenceSelector> VerifyingObserverSequence_ { get { return Items.FindAll<Sequence>("0040A073").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString ObjectBinaryIdentifierTrialRetired { get { return Items.FindFirst<OtherByteString>("0040A074") as OtherByteString; } }
        public List<OtherByteString> ObjectBinaryIdentifierTrialRetired_ { get { return Items.FindAll<OtherByteString>("0040A074").ToList(); } }
        public PersonName VerifyingObserverName { get { return Items.FindFirst<PersonName>("0040A075") as PersonName; } }
        public List<PersonName> VerifyingObserverName_ { get { return Items.FindAll<PersonName>("0040A075").ToList(); } }
        public SequenceSelector DocumentingObserverIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A076")); } }
        public List<SequenceSelector> DocumentingObserverIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A076").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AuthorObserverSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A078")); } }
        public List<SequenceSelector> AuthorObserverSequence_ { get { return Items.FindAll<Sequence>("0040A078").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ParticipantSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A07A")); } }
        public List<SequenceSelector> ParticipantSequence_ { get { return Items.FindAll<Sequence>("0040A07A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CustodialOrganizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A07C")); } }
        public List<SequenceSelector> CustodialOrganizationSequence_ { get { return Items.FindAll<Sequence>("0040A07C").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ParticipationType { get { return Items.FindFirst<CodeString>("0040A080") as CodeString; } }
        public List<CodeString> ParticipationType_ { get { return Items.FindAll<CodeString>("0040A080").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ParticipationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040A082") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ParticipationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040A082").ToList(); } }
        public CodeString ObserverType { get { return Items.FindFirst<CodeString>("0040A084") as CodeString; } }
        public List<CodeString> ObserverType_ { get { return Items.FindAll<CodeString>("0040A084").ToList(); } }
        public SequenceSelector ProcedureIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A085")); } }
        public List<SequenceSelector> ProcedureIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A085").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VerifyingObserverIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A088")); } }
        public List<SequenceSelector> VerifyingObserverIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("0040A088").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString ObjectDirectoryBinaryIdentifierTrialRetired { get { return Items.FindFirst<OtherByteString>("0040A089") as OtherByteString; } }
        public List<OtherByteString> ObjectDirectoryBinaryIdentifierTrialRetired_ { get { return Items.FindAll<OtherByteString>("0040A089").ToList(); } }
        public SequenceSelector EquivalentCDADocumentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A090")); } }
        public List<SequenceSelector> EquivalentCDADocumentSequenceRetired_ { get { return Items.FindAll<Sequence>("0040A090").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReferencedWaveformChannels { get { return Items.FindFirst<UnsignedShort>("0040A0B0") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedWaveformChannels_ { get { return Items.FindAll<UnsignedShort>("0040A0B0").ToList(); } }
        public Date DateOfDocumentOrVerbalTransactionTrialRetired { get { return Items.FindFirst<Date>("0040A110") as Date; } }
        public List<Date> DateOfDocumentOrVerbalTransactionTrialRetired_ { get { return Items.FindAll<Date>("0040A110").ToList(); } }
        public Time TimeOfDocumentCreationOrVerbalTransactionTrialRetired { get { return Items.FindFirst<Time>("0040A112") as Time; } }
        public List<Time> TimeOfDocumentCreationOrVerbalTransactionTrialRetired_ { get { return Items.FindAll<Time>("0040A112").ToList(); } }
        public EvilDICOM.Core.Element.DateTime DateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040A120") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> DateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040A120").ToList(); } }
        public Date Date { get { return Items.FindFirst<Date>("0040A121") as Date; } }
        public List<Date> Date_ { get { return Items.FindAll<Date>("0040A121").ToList(); } }
        public Time Time { get { return Items.FindFirst<Time>("0040A122") as Time; } }
        public List<Time> Time_ { get { return Items.FindAll<Time>("0040A122").ToList(); } }
        public PersonName PersonName { get { return Items.FindFirst<PersonName>("0040A123") as PersonName; } }
        public List<PersonName> PersonName_ { get { return Items.FindAll<PersonName>("0040A123").ToList(); } }
        public UniqueIdentifier UID { get { return Items.FindFirst<UniqueIdentifier>("0040A124") as UniqueIdentifier; } }
        public List<UniqueIdentifier> UID_ { get { return Items.FindAll<UniqueIdentifier>("0040A124").ToList(); } }
        public CodeString ReportStatusIDTrialRetired { get { return Items.FindFirst<CodeString>("0040A125") as CodeString; } }
        public List<CodeString> ReportStatusIDTrialRetired_ { get { return Items.FindAll<CodeString>("0040A125").ToList(); } }
        public CodeString TemporalRangeType { get { return Items.FindFirst<CodeString>("0040A130") as CodeString; } }
        public List<CodeString> TemporalRangeType_ { get { return Items.FindAll<CodeString>("0040A130").ToList(); } }
        public UnsignedLong ReferencedSamplePositions { get { return Items.FindFirst<UnsignedLong>("0040A132") as UnsignedLong; } }
        public List<UnsignedLong> ReferencedSamplePositions_ { get { return Items.FindAll<UnsignedLong>("0040A132").ToList(); } }
        public UnsignedShort ReferencedFrameNumbers { get { return Items.FindFirst<UnsignedShort>("0040A136") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedFrameNumbers_ { get { return Items.FindAll<UnsignedShort>("0040A136").ToList(); } }
        public DecimalString ReferencedTimeOffsets { get { return Items.FindFirst<DecimalString>("0040A138") as DecimalString; } }
        public List<DecimalString> ReferencedTimeOffsets_ { get { return Items.FindAll<DecimalString>("0040A138").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ReferencedDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040A13A") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ReferencedDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040A13A").ToList(); } }
        public UnlimitedText TextValue { get { return Items.FindFirst<UnlimitedText>("0040A160") as UnlimitedText; } }
        public List<UnlimitedText> TextValue_ { get { return Items.FindAll<UnlimitedText>("0040A160").ToList(); } }
        public SequenceSelector ObservationCategoryCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A167")); } }
        public List<SequenceSelector> ObservationCategoryCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A167").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ConceptCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A168")); } }
        public List<SequenceSelector> ConceptCodeSequence_ { get { return Items.FindAll<Sequence>("0040A168").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText BibliographicCitationTrialRetired { get { return Items.FindFirst<ShortText>("0040A16A") as ShortText; } }
        public List<ShortText> BibliographicCitationTrialRetired_ { get { return Items.FindAll<ShortText>("0040A16A").ToList(); } }
        public SequenceSelector PurposeOfReferenceCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A170")); } }
        public List<SequenceSelector> PurposeOfReferenceCodeSequence_ { get { return Items.FindAll<Sequence>("0040A170").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier ObservationUIDTrialRetired { get { return Items.FindFirst<UniqueIdentifier>("0040A171") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ObservationUIDTrialRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040A171").ToList(); } }
        public UniqueIdentifier ReferencedObservationUIDTrialRetired { get { return Items.FindFirst<UniqueIdentifier>("0040A172") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedObservationUIDTrialRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040A172").ToList(); } }
        public CodeString ReferencedObservationClassTrialRetired { get { return Items.FindFirst<CodeString>("0040A173") as CodeString; } }
        public List<CodeString> ReferencedObservationClassTrialRetired_ { get { return Items.FindAll<CodeString>("0040A173").ToList(); } }
        public CodeString ReferencedObjectObservationClassTrialRetired { get { return Items.FindFirst<CodeString>("0040A174") as CodeString; } }
        public List<CodeString> ReferencedObjectObservationClassTrialRetired_ { get { return Items.FindAll<CodeString>("0040A174").ToList(); } }
        public UnsignedShort AnnotationGroupNumber { get { return Items.FindFirst<UnsignedShort>("0040A180") as UnsignedShort; } }
        public List<UnsignedShort> AnnotationGroupNumber_ { get { return Items.FindAll<UnsignedShort>("0040A180").ToList(); } }
        public Date ObservationDateTrialRetired { get { return Items.FindFirst<Date>("0040A192") as Date; } }
        public List<Date> ObservationDateTrialRetired_ { get { return Items.FindAll<Date>("0040A192").ToList(); } }
        public Time ObservationTimeTrialRetired { get { return Items.FindFirst<Time>("0040A193") as Time; } }
        public List<Time> ObservationTimeTrialRetired_ { get { return Items.FindAll<Time>("0040A193").ToList(); } }
        public CodeString MeasurementAutomationTrialRetired { get { return Items.FindFirst<CodeString>("0040A194") as CodeString; } }
        public List<CodeString> MeasurementAutomationTrialRetired_ { get { return Items.FindAll<CodeString>("0040A194").ToList(); } }
        public SequenceSelector ModifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A195")); } }
        public List<SequenceSelector> ModifierCodeSequence_ { get { return Items.FindAll<Sequence>("0040A195").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText IdentificationDescriptionTrialRetired { get { return Items.FindFirst<ShortText>("0040A224") as ShortText; } }
        public List<ShortText> IdentificationDescriptionTrialRetired_ { get { return Items.FindAll<ShortText>("0040A224").ToList(); } }
        public CodeString CoordinatesSetGeometricTypeTrialRetired { get { return Items.FindFirst<CodeString>("0040A290") as CodeString; } }
        public List<CodeString> CoordinatesSetGeometricTypeTrialRetired_ { get { return Items.FindAll<CodeString>("0040A290").ToList(); } }
        public SequenceSelector AlgorithmCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A296")); } }
        public List<SequenceSelector> AlgorithmCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A296").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText AlgorithmDescriptionTrialRetired { get { return Items.FindFirst<ShortText>("0040A297") as ShortText; } }
        public List<ShortText> AlgorithmDescriptionTrialRetired_ { get { return Items.FindAll<ShortText>("0040A297").ToList(); } }
        public SignedLong PixelCoordinatesSetTrialRetired { get { return Items.FindFirst<SignedLong>("0040A29A") as SignedLong; } }
        public List<SignedLong> PixelCoordinatesSetTrialRetired_ { get { return Items.FindAll<SignedLong>("0040A29A").ToList(); } }
        public SequenceSelector MeasuredValueSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A300")); } }
        public List<SequenceSelector> MeasuredValueSequence_ { get { return Items.FindAll<Sequence>("0040A300").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector NumericValueQualifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A301")); } }
        public List<SequenceSelector> NumericValueQualifierCodeSequence_ { get { return Items.FindAll<Sequence>("0040A301").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName CurrentObserverTrialRetired { get { return Items.FindFirst<PersonName>("0040A307") as PersonName; } }
        public List<PersonName> CurrentObserverTrialRetired_ { get { return Items.FindAll<PersonName>("0040A307").ToList(); } }
        public DecimalString NumericValue { get { return Items.FindFirst<DecimalString>("0040A30A") as DecimalString; } }
        public List<DecimalString> NumericValue_ { get { return Items.FindAll<DecimalString>("0040A30A").ToList(); } }
        public SequenceSelector ReferencedAccessionSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A313")); } }
        public List<SequenceSelector> ReferencedAccessionSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A313").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText ReportStatusCommentTrialRetired { get { return Items.FindFirst<ShortText>("0040A33A") as ShortText; } }
        public List<ShortText> ReportStatusCommentTrialRetired_ { get { return Items.FindAll<ShortText>("0040A33A").ToList(); } }
        public SequenceSelector ProcedureContextSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A340")); } }
        public List<SequenceSelector> ProcedureContextSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A340").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName VerbalSourceTrialRetired { get { return Items.FindFirst<PersonName>("0040A352") as PersonName; } }
        public List<PersonName> VerbalSourceTrialRetired_ { get { return Items.FindAll<PersonName>("0040A352").ToList(); } }
        public ShortText AddressTrialRetired { get { return Items.FindFirst<ShortText>("0040A353") as ShortText; } }
        public List<ShortText> AddressTrialRetired_ { get { return Items.FindAll<ShortText>("0040A353").ToList(); } }
        public LongString TelephoneNumberTrialRetired { get { return Items.FindFirst<LongString>("0040A354") as LongString; } }
        public List<LongString> TelephoneNumberTrialRetired_ { get { return Items.FindAll<LongString>("0040A354").ToList(); } }
        public SequenceSelector VerbalSourceIdentifierCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A358")); } }
        public List<SequenceSelector> VerbalSourceIdentifierCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A358").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PredecessorDocumentsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A360")); } }
        public List<SequenceSelector> PredecessorDocumentsSequence_ { get { return Items.FindAll<Sequence>("0040A360").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedRequestSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A370")); } }
        public List<SequenceSelector> ReferencedRequestSequence_ { get { return Items.FindAll<Sequence>("0040A370").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedProcedureCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A372")); } }
        public List<SequenceSelector> PerformedProcedureCodeSequence_ { get { return Items.FindAll<Sequence>("0040A372").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CurrentRequestedProcedureEvidenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A375")); } }
        public List<SequenceSelector> CurrentRequestedProcedureEvidenceSequence_ { get { return Items.FindAll<Sequence>("0040A375").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReportDetailSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A380")); } }
        public List<SequenceSelector> ReportDetailSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A380").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PertinentOtherEvidenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A385")); } }
        public List<SequenceSelector> PertinentOtherEvidenceSequence_ { get { return Items.FindAll<Sequence>("0040A385").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector HL7StructuredDocumentReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A390")); } }
        public List<SequenceSelector> HL7StructuredDocumentReferenceSequence_ { get { return Items.FindAll<Sequence>("0040A390").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier ObservationSubjectUIDTrialRetired { get { return Items.FindFirst<UniqueIdentifier>("0040A402") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ObservationSubjectUIDTrialRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040A402").ToList(); } }
        public CodeString ObservationSubjectClassTrialRetired { get { return Items.FindFirst<CodeString>("0040A403") as CodeString; } }
        public List<CodeString> ObservationSubjectClassTrialRetired_ { get { return Items.FindAll<CodeString>("0040A403").ToList(); } }
        public SequenceSelector ObservationSubjectTypeCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A404")); } }
        public List<SequenceSelector> ObservationSubjectTypeCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A404").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString CompletionFlag { get { return Items.FindFirst<CodeString>("0040A491") as CodeString; } }
        public List<CodeString> CompletionFlag_ { get { return Items.FindAll<CodeString>("0040A491").ToList(); } }
        public LongString CompletionFlagDescription { get { return Items.FindFirst<LongString>("0040A492") as LongString; } }
        public List<LongString> CompletionFlagDescription_ { get { return Items.FindAll<LongString>("0040A492").ToList(); } }
        public CodeString VerificationFlag { get { return Items.FindFirst<CodeString>("0040A493") as CodeString; } }
        public List<CodeString> VerificationFlag_ { get { return Items.FindAll<CodeString>("0040A493").ToList(); } }
        public CodeString ArchiveRequested { get { return Items.FindFirst<CodeString>("0040A494") as CodeString; } }
        public List<CodeString> ArchiveRequested_ { get { return Items.FindAll<CodeString>("0040A494").ToList(); } }
        public CodeString PreliminaryFlag { get { return Items.FindFirst<CodeString>("0040A496") as CodeString; } }
        public List<CodeString> PreliminaryFlag_ { get { return Items.FindAll<CodeString>("0040A496").ToList(); } }
        public SequenceSelector ContentTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A504")); } }
        public List<SequenceSelector> ContentTemplateSequence_ { get { return Items.FindAll<Sequence>("0040A504").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IdenticalDocumentsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A525")); } }
        public List<SequenceSelector> IdenticalDocumentsSequence_ { get { return Items.FindAll<Sequence>("0040A525").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ObservationSubjectContextFlagTrialRetired { get { return Items.FindFirst<CodeString>("0040A600") as CodeString; } }
        public List<CodeString> ObservationSubjectContextFlagTrialRetired_ { get { return Items.FindAll<CodeString>("0040A600").ToList(); } }
        public CodeString ObserverContextFlagTrialRetired { get { return Items.FindFirst<CodeString>("0040A601") as CodeString; } }
        public List<CodeString> ObserverContextFlagTrialRetired_ { get { return Items.FindAll<CodeString>("0040A601").ToList(); } }
        public CodeString ProcedureContextFlagTrialRetired { get { return Items.FindFirst<CodeString>("0040A603") as CodeString; } }
        public List<CodeString> ProcedureContextFlagTrialRetired_ { get { return Items.FindAll<CodeString>("0040A603").ToList(); } }
        public SequenceSelector ContentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A730")); } }
        public List<SequenceSelector> ContentSequence_ { get { return Items.FindAll<Sequence>("0040A730").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelationshipSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A731")); } }
        public List<SequenceSelector> RelationshipSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A731").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelationshipTypeCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A732")); } }
        public List<SequenceSelector> RelationshipTypeCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A732").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LanguageCodeSequenceTrialRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040A744")); } }
        public List<SequenceSelector> LanguageCodeSequenceTrialRetired_ { get { return Items.FindAll<Sequence>("0040A744").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText UniformResourceLocatorTrialRetired { get { return Items.FindFirst<ShortText>("0040A992") as ShortText; } }
        public List<ShortText> UniformResourceLocatorTrialRetired_ { get { return Items.FindAll<ShortText>("0040A992").ToList(); } }
        public SequenceSelector WaveformAnnotationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040B020")); } }
        public List<SequenceSelector> WaveformAnnotationSequence_ { get { return Items.FindAll<Sequence>("0040B020").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString TemplateIdentifier { get { return Items.FindFirst<CodeString>("0040DB00") as CodeString; } }
        public List<CodeString> TemplateIdentifier_ { get { return Items.FindAll<CodeString>("0040DB00").ToList(); } }
        public EvilDICOM.Core.Element.DateTime TemplateVersionRetired { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040DB06") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> TemplateVersionRetired_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040DB06").ToList(); } }
        public EvilDICOM.Core.Element.DateTime TemplateLocalVersionRetired { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040DB07") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> TemplateLocalVersionRetired_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040DB07").ToList(); } }
        public CodeString TemplateExtensionFlagRetired { get { return Items.FindFirst<CodeString>("0040DB0B") as CodeString; } }
        public List<CodeString> TemplateExtensionFlagRetired_ { get { return Items.FindAll<CodeString>("0040DB0B").ToList(); } }
        public UniqueIdentifier TemplateExtensionOrganizationUIDRetired { get { return Items.FindFirst<UniqueIdentifier>("0040DB0C") as UniqueIdentifier; } }
        public List<UniqueIdentifier> TemplateExtensionOrganizationUIDRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040DB0C").ToList(); } }
        public UniqueIdentifier TemplateExtensionCreatorUIDRetired { get { return Items.FindFirst<UniqueIdentifier>("0040DB0D") as UniqueIdentifier; } }
        public List<UniqueIdentifier> TemplateExtensionCreatorUIDRetired_ { get { return Items.FindAll<UniqueIdentifier>("0040DB0D").ToList(); } }
        public UnsignedLong ReferencedContentItemIdentifier { get { return Items.FindFirst<UnsignedLong>("0040DB73") as UnsignedLong; } }
        public List<UnsignedLong> ReferencedContentItemIdentifier_ { get { return Items.FindAll<UnsignedLong>("0040DB73").ToList(); } }
        public ShortText HL7InstanceIdentifier { get { return Items.FindFirst<ShortText>("0040E001") as ShortText; } }
        public List<ShortText> HL7InstanceIdentifier_ { get { return Items.FindAll<ShortText>("0040E001").ToList(); } }
        public EvilDICOM.Core.Element.DateTime HL7DocumentEffectiveTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0040E004") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> HL7DocumentEffectiveTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0040E004").ToList(); } }
        public SequenceSelector HL7DocumentTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E006")); } }
        public List<SequenceSelector> HL7DocumentTypeCodeSequence_ { get { return Items.FindAll<Sequence>("0040E006").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DocumentClassCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E008")); } }
        public List<SequenceSelector> DocumentClassCodeSequence_ { get { return Items.FindAll<Sequence>("0040E008").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnlimitedText RetrieveURI { get { return Items.FindFirst<UnlimitedText>("0040E010") as UnlimitedText; } }
        public List<UnlimitedText> RetrieveURI_ { get { return Items.FindAll<UnlimitedText>("0040E010").ToList(); } }
        public UniqueIdentifier RetrieveLocationUID { get { return Items.FindFirst<UniqueIdentifier>("0040E011") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RetrieveLocationUID_ { get { return Items.FindAll<UniqueIdentifier>("0040E011").ToList(); } }
        public CodeString TypeOfInstances { get { return Items.FindFirst<CodeString>("0040E020") as CodeString; } }
        public List<CodeString> TypeOfInstances_ { get { return Items.FindAll<CodeString>("0040E020").ToList(); } }
        public SequenceSelector DICOMRetrievalSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E021")); } }
        public List<SequenceSelector> DICOMRetrievalSequence_ { get { return Items.FindAll<Sequence>("0040E021").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DICOMMediaRetrievalSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E022")); } }
        public List<SequenceSelector> DICOMMediaRetrievalSequence_ { get { return Items.FindAll<Sequence>("0040E022").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector WADORetrievalSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E023")); } }
        public List<SequenceSelector> WADORetrievalSequence_ { get { return Items.FindAll<Sequence>("0040E023").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector XDSRetrievalSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0040E024")); } }
        public List<SequenceSelector> XDSRetrievalSequence_ { get { return Items.FindAll<Sequence>("0040E024").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier RepositoryUniqueID { get { return Items.FindFirst<UniqueIdentifier>("0040E030") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RepositoryUniqueID_ { get { return Items.FindAll<UniqueIdentifier>("0040E030").ToList(); } }
        public UniqueIdentifier HomeCommunityID { get { return Items.FindFirst<UniqueIdentifier>("0040E031") as UniqueIdentifier; } }
        public List<UniqueIdentifier> HomeCommunityID_ { get { return Items.FindAll<UniqueIdentifier>("0040E031").ToList(); } }
        public ShortText DocumentTitle { get { return Items.FindFirst<ShortText>("00420010") as ShortText; } }
        public List<ShortText> DocumentTitle_ { get { return Items.FindAll<ShortText>("00420010").ToList(); } }
        public OtherByteString EncapsulatedDocument { get { return Items.FindFirst<OtherByteString>("00420011") as OtherByteString; } }
        public List<OtherByteString> EncapsulatedDocument_ { get { return Items.FindAll<OtherByteString>("00420011").ToList(); } }
        public LongString MIMETypeOfEncapsulatedDocument { get { return Items.FindFirst<LongString>("00420012") as LongString; } }
        public List<LongString> MIMETypeOfEncapsulatedDocument_ { get { return Items.FindAll<LongString>("00420012").ToList(); } }
        public SequenceSelector SourceInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00420013")); } }
        public List<SequenceSelector> SourceInstanceSequence_ { get { return Items.FindAll<Sequence>("00420013").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ListOfMIMETypes { get { return Items.FindFirst<LongString>("00420014") as LongString; } }
        public List<LongString> ListOfMIMETypes_ { get { return Items.FindAll<LongString>("00420014").ToList(); } }
        public ShortText ProductPackageIdentifier { get { return Items.FindFirst<ShortText>("00440001") as ShortText; } }
        public List<ShortText> ProductPackageIdentifier_ { get { return Items.FindAll<ShortText>("00440001").ToList(); } }
        public CodeString SubstanceAdministrationApproval { get { return Items.FindFirst<CodeString>("00440002") as CodeString; } }
        public List<CodeString> SubstanceAdministrationApproval_ { get { return Items.FindAll<CodeString>("00440002").ToList(); } }
        public LongText ApprovalStatusFurtherDescription { get { return Items.FindFirst<LongText>("00440003") as LongText; } }
        public List<LongText> ApprovalStatusFurtherDescription_ { get { return Items.FindAll<LongText>("00440003").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ApprovalStatusDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00440004") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ApprovalStatusDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00440004").ToList(); } }
        public SequenceSelector ProductTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00440007")); } }
        public List<SequenceSelector> ProductTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00440007").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ProductName { get { return Items.FindFirst<LongString>("00440008") as LongString; } }
        public List<LongString> ProductName_ { get { return Items.FindAll<LongString>("00440008").ToList(); } }
        public LongText ProductDescription { get { return Items.FindFirst<LongText>("00440009") as LongText; } }
        public List<LongText> ProductDescription_ { get { return Items.FindAll<LongText>("00440009").ToList(); } }
        public LongString ProductLotIdentifier { get { return Items.FindFirst<LongString>("0044000A") as LongString; } }
        public List<LongString> ProductLotIdentifier_ { get { return Items.FindAll<LongString>("0044000A").ToList(); } }
        public EvilDICOM.Core.Element.DateTime ProductExpirationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0044000B") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> ProductExpirationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0044000B").ToList(); } }
        public EvilDICOM.Core.Element.DateTime SubstanceAdministrationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00440010") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> SubstanceAdministrationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00440010").ToList(); } }
        public LongString SubstanceAdministrationNotes { get { return Items.FindFirst<LongString>("00440011") as LongString; } }
        public List<LongString> SubstanceAdministrationNotes_ { get { return Items.FindAll<LongString>("00440011").ToList(); } }
        public LongString SubstanceAdministrationDeviceID { get { return Items.FindFirst<LongString>("00440012") as LongString; } }
        public List<LongString> SubstanceAdministrationDeviceID_ { get { return Items.FindAll<LongString>("00440012").ToList(); } }
        public SequenceSelector ProductParameterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00440013")); } }
        public List<SequenceSelector> ProductParameterSequence_ { get { return Items.FindAll<Sequence>("00440013").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SubstanceAdministrationParameterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00440019")); } }
        public List<SequenceSelector> SubstanceAdministrationParameterSequence_ { get { return Items.FindAll<Sequence>("00440019").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString LensDescription { get { return Items.FindFirst<LongString>("00460012") as LongString; } }
        public List<LongString> LensDescription_ { get { return Items.FindAll<LongString>("00460012").ToList(); } }
        public SequenceSelector RightLensSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460014")); } }
        public List<SequenceSelector> RightLensSequence_ { get { return Items.FindAll<Sequence>("00460014").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LeftLensSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460015")); } }
        public List<SequenceSelector> LeftLensSequence_ { get { return Items.FindAll<Sequence>("00460015").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector UnspecifiedLateralityLensSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460016")); } }
        public List<SequenceSelector> UnspecifiedLateralityLensSequence_ { get { return Items.FindAll<Sequence>("00460016").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CylinderSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460018")); } }
        public List<SequenceSelector> CylinderSequence_ { get { return Items.FindAll<Sequence>("00460018").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PrismSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460028")); } }
        public List<SequenceSelector> PrismSequence_ { get { return Items.FindAll<Sequence>("00460028").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble HorizontalPrismPower { get { return Items.FindFirst<FloatingPointDouble>("00460030") as FloatingPointDouble; } }
        public List<FloatingPointDouble> HorizontalPrismPower_ { get { return Items.FindAll<FloatingPointDouble>("00460030").ToList(); } }
        public CodeString HorizontalPrismBase { get { return Items.FindFirst<CodeString>("00460032") as CodeString; } }
        public List<CodeString> HorizontalPrismBase_ { get { return Items.FindAll<CodeString>("00460032").ToList(); } }
        public FloatingPointDouble VerticalPrismPower { get { return Items.FindFirst<FloatingPointDouble>("00460034") as FloatingPointDouble; } }
        public List<FloatingPointDouble> VerticalPrismPower_ { get { return Items.FindAll<FloatingPointDouble>("00460034").ToList(); } }
        public CodeString VerticalPrismBase { get { return Items.FindFirst<CodeString>("00460036") as CodeString; } }
        public List<CodeString> VerticalPrismBase_ { get { return Items.FindAll<CodeString>("00460036").ToList(); } }
        public CodeString LensSegmentType { get { return Items.FindFirst<CodeString>("00460038") as CodeString; } }
        public List<CodeString> LensSegmentType_ { get { return Items.FindAll<CodeString>("00460038").ToList(); } }
        public FloatingPointDouble OpticalTransmittance { get { return Items.FindFirst<FloatingPointDouble>("00460040") as FloatingPointDouble; } }
        public List<FloatingPointDouble> OpticalTransmittance_ { get { return Items.FindAll<FloatingPointDouble>("00460040").ToList(); } }
        public FloatingPointDouble ChannelWidth { get { return Items.FindFirst<FloatingPointDouble>("00460042") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ChannelWidth_ { get { return Items.FindAll<FloatingPointDouble>("00460042").ToList(); } }
        public FloatingPointDouble PupilSize { get { return Items.FindFirst<FloatingPointDouble>("00460044") as FloatingPointDouble; } }
        public List<FloatingPointDouble> PupilSize_ { get { return Items.FindAll<FloatingPointDouble>("00460044").ToList(); } }
        public FloatingPointDouble CornealSize { get { return Items.FindFirst<FloatingPointDouble>("00460046") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CornealSize_ { get { return Items.FindAll<FloatingPointDouble>("00460046").ToList(); } }
        public SequenceSelector AutorefractionRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460050")); } }
        public List<SequenceSelector> AutorefractionRightEyeSequence_ { get { return Items.FindAll<Sequence>("00460050").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AutorefractionLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460052")); } }
        public List<SequenceSelector> AutorefractionLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00460052").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble DistancePupillaryDistance { get { return Items.FindFirst<FloatingPointDouble>("00460060") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DistancePupillaryDistance_ { get { return Items.FindAll<FloatingPointDouble>("00460060").ToList(); } }
        public FloatingPointDouble NearPupillaryDistance { get { return Items.FindFirst<FloatingPointDouble>("00460062") as FloatingPointDouble; } }
        public List<FloatingPointDouble> NearPupillaryDistance_ { get { return Items.FindAll<FloatingPointDouble>("00460062").ToList(); } }
        public FloatingPointDouble IntermediatePupillaryDistance { get { return Items.FindFirst<FloatingPointDouble>("00460063") as FloatingPointDouble; } }
        public List<FloatingPointDouble> IntermediatePupillaryDistance_ { get { return Items.FindAll<FloatingPointDouble>("00460063").ToList(); } }
        public FloatingPointDouble OtherPupillaryDistance { get { return Items.FindFirst<FloatingPointDouble>("00460064") as FloatingPointDouble; } }
        public List<FloatingPointDouble> OtherPupillaryDistance_ { get { return Items.FindAll<FloatingPointDouble>("00460064").ToList(); } }
        public SequenceSelector KeratometryRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460070")); } }
        public List<SequenceSelector> KeratometryRightEyeSequence_ { get { return Items.FindAll<Sequence>("00460070").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector KeratometryLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460071")); } }
        public List<SequenceSelector> KeratometryLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00460071").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SteepKeratometricAxisSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460074")); } }
        public List<SequenceSelector> SteepKeratometricAxisSequence_ { get { return Items.FindAll<Sequence>("00460074").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble RadiusOfCurvature { get { return Items.FindFirst<FloatingPointDouble>("00460075") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RadiusOfCurvature_ { get { return Items.FindAll<FloatingPointDouble>("00460075").ToList(); } }
        public FloatingPointDouble KeratometricPower { get { return Items.FindFirst<FloatingPointDouble>("00460076") as FloatingPointDouble; } }
        public List<FloatingPointDouble> KeratometricPower_ { get { return Items.FindAll<FloatingPointDouble>("00460076").ToList(); } }
        public FloatingPointDouble KeratometricAxis { get { return Items.FindFirst<FloatingPointDouble>("00460077") as FloatingPointDouble; } }
        public List<FloatingPointDouble> KeratometricAxis_ { get { return Items.FindAll<FloatingPointDouble>("00460077").ToList(); } }
        public SequenceSelector FlatKeratometricAxisSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460080")); } }
        public List<SequenceSelector> FlatKeratometricAxisSequence_ { get { return Items.FindAll<Sequence>("00460080").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BackgroundColor { get { return Items.FindFirst<CodeString>("00460092") as CodeString; } }
        public List<CodeString> BackgroundColor_ { get { return Items.FindAll<CodeString>("00460092").ToList(); } }
        public CodeString Optotype { get { return Items.FindFirst<CodeString>("00460094") as CodeString; } }
        public List<CodeString> Optotype_ { get { return Items.FindAll<CodeString>("00460094").ToList(); } }
        public CodeString OptotypePresentation { get { return Items.FindFirst<CodeString>("00460095") as CodeString; } }
        public List<CodeString> OptotypePresentation_ { get { return Items.FindAll<CodeString>("00460095").ToList(); } }
        public SequenceSelector SubjectiveRefractionRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460097")); } }
        public List<SequenceSelector> SubjectiveRefractionRightEyeSequence_ { get { return Items.FindAll<Sequence>("00460097").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SubjectiveRefractionLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460098")); } }
        public List<SequenceSelector> SubjectiveRefractionLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00460098").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AddNearSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460100")); } }
        public List<SequenceSelector> AddNearSequence_ { get { return Items.FindAll<Sequence>("00460100").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AddIntermediateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460101")); } }
        public List<SequenceSelector> AddIntermediateSequence_ { get { return Items.FindAll<Sequence>("00460101").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AddOtherSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460102")); } }
        public List<SequenceSelector> AddOtherSequence_ { get { return Items.FindAll<Sequence>("00460102").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble AddPower { get { return Items.FindFirst<FloatingPointDouble>("00460104") as FloatingPointDouble; } }
        public List<FloatingPointDouble> AddPower_ { get { return Items.FindAll<FloatingPointDouble>("00460104").ToList(); } }
        public FloatingPointDouble ViewingDistance { get { return Items.FindFirst<FloatingPointDouble>("00460106") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ViewingDistance_ { get { return Items.FindAll<FloatingPointDouble>("00460106").ToList(); } }
        public SequenceSelector VisualAcuityTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460121")); } }
        public List<SequenceSelector> VisualAcuityTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00460121").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VisualAcuityRightEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460122")); } }
        public List<SequenceSelector> VisualAcuityRightEyeSequence_ { get { return Items.FindAll<Sequence>("00460122").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VisualAcuityLeftEyeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460123")); } }
        public List<SequenceSelector> VisualAcuityLeftEyeSequence_ { get { return Items.FindAll<Sequence>("00460123").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector VisualAcuityBothEyesOpenSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460124")); } }
        public List<SequenceSelector> VisualAcuityBothEyesOpenSequence_ { get { return Items.FindAll<Sequence>("00460124").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ViewingDistanceType { get { return Items.FindFirst<CodeString>("00460125") as CodeString; } }
        public List<CodeString> ViewingDistanceType_ { get { return Items.FindAll<CodeString>("00460125").ToList(); } }
        public SignedShort VisualAcuityModifiers { get { return Items.FindFirst<SignedShort>("00460135") as SignedShort; } }
        public List<SignedShort> VisualAcuityModifiers_ { get { return Items.FindAll<SignedShort>("00460135").ToList(); } }
        public FloatingPointDouble DecimalVisualAcuity { get { return Items.FindFirst<FloatingPointDouble>("00460137") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DecimalVisualAcuity_ { get { return Items.FindAll<FloatingPointDouble>("00460137").ToList(); } }
        public LongString OptotypeDetailedDefinition { get { return Items.FindFirst<LongString>("00460139") as LongString; } }
        public List<LongString> OptotypeDetailedDefinition_ { get { return Items.FindAll<LongString>("00460139").ToList(); } }
        public SequenceSelector ReferencedRefractiveMeasurementsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00460145")); } }
        public List<SequenceSelector> ReferencedRefractiveMeasurementsSequence_ { get { return Items.FindAll<Sequence>("00460145").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble SpherePower { get { return Items.FindFirst<FloatingPointDouble>("00460146") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SpherePower_ { get { return Items.FindAll<FloatingPointDouble>("00460146").ToList(); } }
        public FloatingPointDouble CylinderPower { get { return Items.FindFirst<FloatingPointDouble>("00460147") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CylinderPower_ { get { return Items.FindAll<FloatingPointDouble>("00460147").ToList(); } }
        public FloatingPointSingle ImagedVolumeWidth { get { return Items.FindFirst<FloatingPointSingle>("00480001") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ImagedVolumeWidth_ { get { return Items.FindAll<FloatingPointSingle>("00480001").ToList(); } }
        public FloatingPointSingle ImagedVolumeHeight { get { return Items.FindFirst<FloatingPointSingle>("00480002") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ImagedVolumeHeight_ { get { return Items.FindAll<FloatingPointSingle>("00480002").ToList(); } }
        public FloatingPointSingle ImagedVolumeDepth { get { return Items.FindFirst<FloatingPointSingle>("00480003") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ImagedVolumeDepth_ { get { return Items.FindAll<FloatingPointSingle>("00480003").ToList(); } }
        public UnsignedLong TotalPixelMatrixColumns { get { return Items.FindFirst<UnsignedLong>("00480006") as UnsignedLong; } }
        public List<UnsignedLong> TotalPixelMatrixColumns_ { get { return Items.FindAll<UnsignedLong>("00480006").ToList(); } }
        public UnsignedLong TotalPixelMatrixRows { get { return Items.FindFirst<UnsignedLong>("00480007") as UnsignedLong; } }
        public List<UnsignedLong> TotalPixelMatrixRows_ { get { return Items.FindAll<UnsignedLong>("00480007").ToList(); } }
        public SequenceSelector TotalPixelMatrixOriginSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480008")); } }
        public List<SequenceSelector> TotalPixelMatrixOriginSequence_ { get { return Items.FindAll<Sequence>("00480008").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SpecimenLabelInImage { get { return Items.FindFirst<CodeString>("00480010") as CodeString; } }
        public List<CodeString> SpecimenLabelInImage_ { get { return Items.FindAll<CodeString>("00480010").ToList(); } }
        public CodeString FocusMethod { get { return Items.FindFirst<CodeString>("00480011") as CodeString; } }
        public List<CodeString> FocusMethod_ { get { return Items.FindAll<CodeString>("00480011").ToList(); } }
        public CodeString ExtendedDepthOfField { get { return Items.FindFirst<CodeString>("00480012") as CodeString; } }
        public List<CodeString> ExtendedDepthOfField_ { get { return Items.FindAll<CodeString>("00480012").ToList(); } }
        public UnsignedShort NumberOfFocalPlanes { get { return Items.FindFirst<UnsignedShort>("00480013") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfFocalPlanes_ { get { return Items.FindAll<UnsignedShort>("00480013").ToList(); } }
        public FloatingPointSingle DistanceBetweenFocalPlanes { get { return Items.FindFirst<FloatingPointSingle>("00480014") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DistanceBetweenFocalPlanes_ { get { return Items.FindAll<FloatingPointSingle>("00480014").ToList(); } }
        public UnsignedShort RecommendedAbsentPixelCIELabValue { get { return Items.FindFirst<UnsignedShort>("00480015") as UnsignedShort; } }
        public List<UnsignedShort> RecommendedAbsentPixelCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00480015").ToList(); } }
        public SequenceSelector IlluminatorTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480100")); } }
        public List<SequenceSelector> IlluminatorTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00480100").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString ImageOrientationSlide { get { return Items.FindFirst<DecimalString>("00480102") as DecimalString; } }
        public List<DecimalString> ImageOrientationSlide_ { get { return Items.FindAll<DecimalString>("00480102").ToList(); } }
        public SequenceSelector OpticalPathSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480105")); } }
        public List<SequenceSelector> OpticalPathSequence_ { get { return Items.FindAll<Sequence>("00480105").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString OpticalPathIdentifier { get { return Items.FindFirst<ShortString>("00480106") as ShortString; } }
        public List<ShortString> OpticalPathIdentifier_ { get { return Items.FindAll<ShortString>("00480106").ToList(); } }
        public ShortText OpticalPathDescription { get { return Items.FindFirst<ShortText>("00480107") as ShortText; } }
        public List<ShortText> OpticalPathDescription_ { get { return Items.FindAll<ShortText>("00480107").ToList(); } }
        public SequenceSelector IlluminationColorCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480108")); } }
        public List<SequenceSelector> IlluminationColorCodeSequence_ { get { return Items.FindAll<Sequence>("00480108").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SpecimenReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480110")); } }
        public List<SequenceSelector> SpecimenReferenceSequence_ { get { return Items.FindAll<Sequence>("00480110").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString CondenserLensPower { get { return Items.FindFirst<DecimalString>("00480111") as DecimalString; } }
        public List<DecimalString> CondenserLensPower_ { get { return Items.FindAll<DecimalString>("00480111").ToList(); } }
        public DecimalString ObjectiveLensPower { get { return Items.FindFirst<DecimalString>("00480112") as DecimalString; } }
        public List<DecimalString> ObjectiveLensPower_ { get { return Items.FindAll<DecimalString>("00480112").ToList(); } }
        public DecimalString ObjectiveLensNumericalAperture { get { return Items.FindFirst<DecimalString>("00480113") as DecimalString; } }
        public List<DecimalString> ObjectiveLensNumericalAperture_ { get { return Items.FindAll<DecimalString>("00480113").ToList(); } }
        public SequenceSelector PaletteColorLookupTableSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480120")); } }
        public List<SequenceSelector> PaletteColorLookupTableSequence_ { get { return Items.FindAll<Sequence>("00480120").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedImageNavigationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480200")); } }
        public List<SequenceSelector> ReferencedImageNavigationSequence_ { get { return Items.FindAll<Sequence>("00480200").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort TopLeftHandCornerOfLocalizerArea { get { return Items.FindFirst<UnsignedShort>("00480201") as UnsignedShort; } }
        public List<UnsignedShort> TopLeftHandCornerOfLocalizerArea_ { get { return Items.FindAll<UnsignedShort>("00480201").ToList(); } }
        public UnsignedShort BottomRightHandCornerOfLocalizerArea { get { return Items.FindFirst<UnsignedShort>("00480202") as UnsignedShort; } }
        public List<UnsignedShort> BottomRightHandCornerOfLocalizerArea_ { get { return Items.FindAll<UnsignedShort>("00480202").ToList(); } }
        public SequenceSelector OpticalPathIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00480207")); } }
        public List<SequenceSelector> OpticalPathIdentificationSequence_ { get { return Items.FindAll<Sequence>("00480207").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlanePositionSlideSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0048021A")); } }
        public List<SequenceSelector> PlanePositionSlideSequence_ { get { return Items.FindAll<Sequence>("0048021A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SignedLong RowPositionInTotalImagePixelMatrix { get { return Items.FindFirst<SignedLong>("0048021E") as SignedLong; } }
        public List<SignedLong> RowPositionInTotalImagePixelMatrix_ { get { return Items.FindAll<SignedLong>("0048021E").ToList(); } }
        public SignedLong ColumnPositionInTotalImagePixelMatrix { get { return Items.FindFirst<SignedLong>("0048021F") as SignedLong; } }
        public List<SignedLong> ColumnPositionInTotalImagePixelMatrix_ { get { return Items.FindAll<SignedLong>("0048021F").ToList(); } }
        public CodeString PixelOriginInterpretation { get { return Items.FindFirst<CodeString>("00480301") as CodeString; } }
        public List<CodeString> PixelOriginInterpretation_ { get { return Items.FindAll<CodeString>("00480301").ToList(); } }
        public CodeString CalibrationImage { get { return Items.FindFirst<CodeString>("00500004") as CodeString; } }
        public List<CodeString> CalibrationImage_ { get { return Items.FindAll<CodeString>("00500004").ToList(); } }
        public SequenceSelector DeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00500010")); } }
        public List<SequenceSelector> DeviceSequence_ { get { return Items.FindAll<Sequence>("00500010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContainerComponentTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00500012")); } }
        public List<SequenceSelector> ContainerComponentTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00500012").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ContainerComponentThickness { get { return Items.FindFirst<FloatingPointDouble>("00500013") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContainerComponentThickness_ { get { return Items.FindAll<FloatingPointDouble>("00500013").ToList(); } }
        public DecimalString DeviceLength { get { return Items.FindFirst<DecimalString>("00500014") as DecimalString; } }
        public List<DecimalString> DeviceLength_ { get { return Items.FindAll<DecimalString>("00500014").ToList(); } }
        public FloatingPointDouble ContainerComponentWidth { get { return Items.FindFirst<FloatingPointDouble>("00500015") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContainerComponentWidth_ { get { return Items.FindAll<FloatingPointDouble>("00500015").ToList(); } }
        public DecimalString DeviceDiameter { get { return Items.FindFirst<DecimalString>("00500016") as DecimalString; } }
        public List<DecimalString> DeviceDiameter_ { get { return Items.FindAll<DecimalString>("00500016").ToList(); } }
        public CodeString DeviceDiameterUnits { get { return Items.FindFirst<CodeString>("00500017") as CodeString; } }
        public List<CodeString> DeviceDiameterUnits_ { get { return Items.FindAll<CodeString>("00500017").ToList(); } }
        public DecimalString DeviceVolume { get { return Items.FindFirst<DecimalString>("00500018") as DecimalString; } }
        public List<DecimalString> DeviceVolume_ { get { return Items.FindAll<DecimalString>("00500018").ToList(); } }
        public DecimalString InterMarkerDistance { get { return Items.FindFirst<DecimalString>("00500019") as DecimalString; } }
        public List<DecimalString> InterMarkerDistance_ { get { return Items.FindAll<DecimalString>("00500019").ToList(); } }
        public CodeString ContainerComponentMaterial { get { return Items.FindFirst<CodeString>("0050001A") as CodeString; } }
        public List<CodeString> ContainerComponentMaterial_ { get { return Items.FindAll<CodeString>("0050001A").ToList(); } }
        public LongString ContainerComponentID { get { return Items.FindFirst<LongString>("0050001B") as LongString; } }
        public List<LongString> ContainerComponentID_ { get { return Items.FindAll<LongString>("0050001B").ToList(); } }
        public FloatingPointDouble ContainerComponentLength { get { return Items.FindFirst<FloatingPointDouble>("0050001C") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContainerComponentLength_ { get { return Items.FindAll<FloatingPointDouble>("0050001C").ToList(); } }
        public FloatingPointDouble ContainerComponentDiameter { get { return Items.FindFirst<FloatingPointDouble>("0050001D") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContainerComponentDiameter_ { get { return Items.FindAll<FloatingPointDouble>("0050001D").ToList(); } }
        public LongString ContainerComponentDescription { get { return Items.FindFirst<LongString>("0050001E") as LongString; } }
        public List<LongString> ContainerComponentDescription_ { get { return Items.FindAll<LongString>("0050001E").ToList(); } }
        public LongString DeviceDescription { get { return Items.FindFirst<LongString>("00500020") as LongString; } }
        public List<LongString> DeviceDescription_ { get { return Items.FindAll<LongString>("00500020").ToList(); } }
        public FloatingPointSingle ContrastBolusIngredientPercentByVolume { get { return Items.FindFirst<FloatingPointSingle>("00520001") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ContrastBolusIngredientPercentByVolume_ { get { return Items.FindAll<FloatingPointSingle>("00520001").ToList(); } }
        public FloatingPointDouble OCTFocalDistance { get { return Items.FindFirst<FloatingPointDouble>("00520002") as FloatingPointDouble; } }
        public List<FloatingPointDouble> OCTFocalDistance_ { get { return Items.FindAll<FloatingPointDouble>("00520002").ToList(); } }
        public FloatingPointDouble BeamSpotSize { get { return Items.FindFirst<FloatingPointDouble>("00520003") as FloatingPointDouble; } }
        public List<FloatingPointDouble> BeamSpotSize_ { get { return Items.FindAll<FloatingPointDouble>("00520003").ToList(); } }
        public FloatingPointDouble EffectiveRefractiveIndex { get { return Items.FindFirst<FloatingPointDouble>("00520004") as FloatingPointDouble; } }
        public List<FloatingPointDouble> EffectiveRefractiveIndex_ { get { return Items.FindAll<FloatingPointDouble>("00520004").ToList(); } }
        public CodeString OCTAcquisitionDomain { get { return Items.FindFirst<CodeString>("00520006") as CodeString; } }
        public List<CodeString> OCTAcquisitionDomain_ { get { return Items.FindAll<CodeString>("00520006").ToList(); } }
        public FloatingPointDouble OCTOpticalCenterWavelength { get { return Items.FindFirst<FloatingPointDouble>("00520007") as FloatingPointDouble; } }
        public List<FloatingPointDouble> OCTOpticalCenterWavelength_ { get { return Items.FindAll<FloatingPointDouble>("00520007").ToList(); } }
        public FloatingPointDouble AxialResolution { get { return Items.FindFirst<FloatingPointDouble>("00520008") as FloatingPointDouble; } }
        public List<FloatingPointDouble> AxialResolution_ { get { return Items.FindAll<FloatingPointDouble>("00520008").ToList(); } }
        public FloatingPointDouble RangingDepth { get { return Items.FindFirst<FloatingPointDouble>("00520009") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RangingDepth_ { get { return Items.FindAll<FloatingPointDouble>("00520009").ToList(); } }
        public FloatingPointDouble ALineRate { get { return Items.FindFirst<FloatingPointDouble>("00520011") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ALineRate_ { get { return Items.FindAll<FloatingPointDouble>("00520011").ToList(); } }
        public UnsignedShort ALinesPerFrame { get { return Items.FindFirst<UnsignedShort>("00520012") as UnsignedShort; } }
        public List<UnsignedShort> ALinesPerFrame_ { get { return Items.FindAll<UnsignedShort>("00520012").ToList(); } }
        public FloatingPointDouble CatheterRotationalRate { get { return Items.FindFirst<FloatingPointDouble>("00520013") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CatheterRotationalRate_ { get { return Items.FindAll<FloatingPointDouble>("00520013").ToList(); } }
        public FloatingPointDouble ALinePixelSpacing { get { return Items.FindFirst<FloatingPointDouble>("00520014") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ALinePixelSpacing_ { get { return Items.FindAll<FloatingPointDouble>("00520014").ToList(); } }
        public SequenceSelector ModeOfPercutaneousAccessSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00520016")); } }
        public List<SequenceSelector> ModeOfPercutaneousAccessSequence_ { get { return Items.FindAll<Sequence>("00520016").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IntravascularOCTFrameTypeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00520025")); } }
        public List<SequenceSelector> IntravascularOCTFrameTypeSequence_ { get { return Items.FindAll<Sequence>("00520025").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString OCTZOffsetApplied { get { return Items.FindFirst<CodeString>("00520026") as CodeString; } }
        public List<CodeString> OCTZOffsetApplied_ { get { return Items.FindAll<CodeString>("00520026").ToList(); } }
        public SequenceSelector IntravascularFrameContentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00520027")); } }
        public List<SequenceSelector> IntravascularFrameContentSequence_ { get { return Items.FindAll<Sequence>("00520027").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble IntravascularLongitudinalDistance { get { return Items.FindFirst<FloatingPointDouble>("00520028") as FloatingPointDouble; } }
        public List<FloatingPointDouble> IntravascularLongitudinalDistance_ { get { return Items.FindAll<FloatingPointDouble>("00520028").ToList(); } }
        public SequenceSelector IntravascularOCTFrameContentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00520029")); } }
        public List<SequenceSelector> IntravascularOCTFrameContentSequence_ { get { return Items.FindAll<Sequence>("00520029").Select(s => new SequenceSelector(s)).ToList(); } }
        public SignedShort OCTZOffsetCorrection { get { return Items.FindFirst<SignedShort>("00520030") as SignedShort; } }
        public List<SignedShort> OCTZOffsetCorrection_ { get { return Items.FindAll<SignedShort>("00520030").ToList(); } }
        public CodeString CatheterDirectionOfRotation { get { return Items.FindFirst<CodeString>("00520031") as CodeString; } }
        public List<CodeString> CatheterDirectionOfRotation_ { get { return Items.FindAll<CodeString>("00520031").ToList(); } }
        public FloatingPointDouble SeamLineLocation { get { return Items.FindFirst<FloatingPointDouble>("00520033") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SeamLineLocation_ { get { return Items.FindAll<FloatingPointDouble>("00520033").ToList(); } }
        public FloatingPointDouble FirstALineLocation { get { return Items.FindFirst<FloatingPointDouble>("00520034") as FloatingPointDouble; } }
        public List<FloatingPointDouble> FirstALineLocation_ { get { return Items.FindAll<FloatingPointDouble>("00520034").ToList(); } }
        public UnsignedShort SeamLineIndex { get { return Items.FindFirst<UnsignedShort>("00520036") as UnsignedShort; } }
        public List<UnsignedShort> SeamLineIndex_ { get { return Items.FindAll<UnsignedShort>("00520036").ToList(); } }
        public UnsignedShort NumberOfPaddedAlines { get { return Items.FindFirst<UnsignedShort>("00520038") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfPaddedAlines_ { get { return Items.FindAll<UnsignedShort>("00520038").ToList(); } }
        public CodeString InterpolationType { get { return Items.FindFirst<CodeString>("00520039") as CodeString; } }
        public List<CodeString> InterpolationType_ { get { return Items.FindAll<CodeString>("00520039").ToList(); } }
        public CodeString RefractiveIndexApplied { get { return Items.FindFirst<CodeString>("0052003A") as CodeString; } }
        public List<CodeString> RefractiveIndexApplied_ { get { return Items.FindAll<CodeString>("0052003A").ToList(); } }
        public UnsignedShort NumberOfEnergyWindows { get { return Items.FindFirst<UnsignedShort>("00540011") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfEnergyWindows_ { get { return Items.FindAll<UnsignedShort>("00540011").ToList(); } }
        public SequenceSelector EnergyWindowInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540012")); } }
        public List<SequenceSelector> EnergyWindowInformationSequence_ { get { return Items.FindAll<Sequence>("00540012").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector EnergyWindowRangeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540013")); } }
        public List<SequenceSelector> EnergyWindowRangeSequence_ { get { return Items.FindAll<Sequence>("00540013").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString EnergyWindowLowerLimit { get { return Items.FindFirst<DecimalString>("00540014") as DecimalString; } }
        public List<DecimalString> EnergyWindowLowerLimit_ { get { return Items.FindAll<DecimalString>("00540014").ToList(); } }
        public DecimalString EnergyWindowUpperLimit { get { return Items.FindFirst<DecimalString>("00540015") as DecimalString; } }
        public List<DecimalString> EnergyWindowUpperLimit_ { get { return Items.FindAll<DecimalString>("00540015").ToList(); } }
        public SequenceSelector RadiopharmaceuticalInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540016")); } }
        public List<SequenceSelector> RadiopharmaceuticalInformationSequence_ { get { return Items.FindAll<Sequence>("00540016").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ResidualSyringeCounts { get { return Items.FindFirst<IntegerString>("00540017") as IntegerString; } }
        public List<IntegerString> ResidualSyringeCounts_ { get { return Items.FindAll<IntegerString>("00540017").ToList(); } }
        public ShortString EnergyWindowName { get { return Items.FindFirst<ShortString>("00540018") as ShortString; } }
        public List<ShortString> EnergyWindowName_ { get { return Items.FindAll<ShortString>("00540018").ToList(); } }
        public UnsignedShort DetectorVector { get { return Items.FindFirst<UnsignedShort>("00540020") as UnsignedShort; } }
        public List<UnsignedShort> DetectorVector_ { get { return Items.FindAll<UnsignedShort>("00540020").ToList(); } }
        public UnsignedShort NumberOfDetectors { get { return Items.FindFirst<UnsignedShort>("00540021") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfDetectors_ { get { return Items.FindAll<UnsignedShort>("00540021").ToList(); } }
        public SequenceSelector DetectorInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540022")); } }
        public List<SequenceSelector> DetectorInformationSequence_ { get { return Items.FindAll<Sequence>("00540022").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort PhaseVector { get { return Items.FindFirst<UnsignedShort>("00540030") as UnsignedShort; } }
        public List<UnsignedShort> PhaseVector_ { get { return Items.FindAll<UnsignedShort>("00540030").ToList(); } }
        public UnsignedShort NumberOfPhases { get { return Items.FindFirst<UnsignedShort>("00540031") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfPhases_ { get { return Items.FindAll<UnsignedShort>("00540031").ToList(); } }
        public SequenceSelector PhaseInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540032")); } }
        public List<SequenceSelector> PhaseInformationSequence_ { get { return Items.FindAll<Sequence>("00540032").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfFramesInPhase { get { return Items.FindFirst<UnsignedShort>("00540033") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfFramesInPhase_ { get { return Items.FindAll<UnsignedShort>("00540033").ToList(); } }
        public IntegerString PhaseDelay { get { return Items.FindFirst<IntegerString>("00540036") as IntegerString; } }
        public List<IntegerString> PhaseDelay_ { get { return Items.FindAll<IntegerString>("00540036").ToList(); } }
        public IntegerString PauseBetweenFrames { get { return Items.FindFirst<IntegerString>("00540038") as IntegerString; } }
        public List<IntegerString> PauseBetweenFrames_ { get { return Items.FindAll<IntegerString>("00540038").ToList(); } }
        public CodeString PhaseDescription { get { return Items.FindFirst<CodeString>("00540039") as CodeString; } }
        public List<CodeString> PhaseDescription_ { get { return Items.FindAll<CodeString>("00540039").ToList(); } }
        public UnsignedShort RotationVector { get { return Items.FindFirst<UnsignedShort>("00540050") as UnsignedShort; } }
        public List<UnsignedShort> RotationVector_ { get { return Items.FindAll<UnsignedShort>("00540050").ToList(); } }
        public UnsignedShort NumberOfRotations { get { return Items.FindFirst<UnsignedShort>("00540051") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfRotations_ { get { return Items.FindAll<UnsignedShort>("00540051").ToList(); } }
        public SequenceSelector RotationInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540052")); } }
        public List<SequenceSelector> RotationInformationSequence_ { get { return Items.FindAll<Sequence>("00540052").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfFramesInRotation { get { return Items.FindFirst<UnsignedShort>("00540053") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfFramesInRotation_ { get { return Items.FindAll<UnsignedShort>("00540053").ToList(); } }
        public UnsignedShort RRIntervalVector { get { return Items.FindFirst<UnsignedShort>("00540060") as UnsignedShort; } }
        public List<UnsignedShort> RRIntervalVector_ { get { return Items.FindAll<UnsignedShort>("00540060").ToList(); } }
        public UnsignedShort NumberOfRRIntervals { get { return Items.FindFirst<UnsignedShort>("00540061") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfRRIntervals_ { get { return Items.FindAll<UnsignedShort>("00540061").ToList(); } }
        public SequenceSelector GatedInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540062")); } }
        public List<SequenceSelector> GatedInformationSequence_ { get { return Items.FindAll<Sequence>("00540062").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DataInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540063")); } }
        public List<SequenceSelector> DataInformationSequence_ { get { return Items.FindAll<Sequence>("00540063").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort TimeSlotVector { get { return Items.FindFirst<UnsignedShort>("00540070") as UnsignedShort; } }
        public List<UnsignedShort> TimeSlotVector_ { get { return Items.FindAll<UnsignedShort>("00540070").ToList(); } }
        public UnsignedShort NumberOfTimeSlots { get { return Items.FindFirst<UnsignedShort>("00540071") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTimeSlots_ { get { return Items.FindAll<UnsignedShort>("00540071").ToList(); } }
        public SequenceSelector TimeSlotInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540072")); } }
        public List<SequenceSelector> TimeSlotInformationSequence_ { get { return Items.FindAll<Sequence>("00540072").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString TimeSlotTime { get { return Items.FindFirst<DecimalString>("00540073") as DecimalString; } }
        public List<DecimalString> TimeSlotTime_ { get { return Items.FindAll<DecimalString>("00540073").ToList(); } }
        public UnsignedShort SliceVector { get { return Items.FindFirst<UnsignedShort>("00540080") as UnsignedShort; } }
        public List<UnsignedShort> SliceVector_ { get { return Items.FindAll<UnsignedShort>("00540080").ToList(); } }
        public UnsignedShort NumberOfSlices { get { return Items.FindFirst<UnsignedShort>("00540081") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfSlices_ { get { return Items.FindAll<UnsignedShort>("00540081").ToList(); } }
        public UnsignedShort AngularViewVector { get { return Items.FindFirst<UnsignedShort>("00540090") as UnsignedShort; } }
        public List<UnsignedShort> AngularViewVector_ { get { return Items.FindAll<UnsignedShort>("00540090").ToList(); } }
        public UnsignedShort TimeSliceVector { get { return Items.FindFirst<UnsignedShort>("00540100") as UnsignedShort; } }
        public List<UnsignedShort> TimeSliceVector_ { get { return Items.FindAll<UnsignedShort>("00540100").ToList(); } }
        public UnsignedShort NumberOfTimeSlices { get { return Items.FindFirst<UnsignedShort>("00540101") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTimeSlices_ { get { return Items.FindAll<UnsignedShort>("00540101").ToList(); } }
        public DecimalString StartAngle { get { return Items.FindFirst<DecimalString>("00540200") as DecimalString; } }
        public List<DecimalString> StartAngle_ { get { return Items.FindAll<DecimalString>("00540200").ToList(); } }
        public CodeString TypeOfDetectorMotion { get { return Items.FindFirst<CodeString>("00540202") as CodeString; } }
        public List<CodeString> TypeOfDetectorMotion_ { get { return Items.FindAll<CodeString>("00540202").ToList(); } }
        public IntegerString TriggerVector { get { return Items.FindFirst<IntegerString>("00540210") as IntegerString; } }
        public List<IntegerString> TriggerVector_ { get { return Items.FindAll<IntegerString>("00540210").ToList(); } }
        public UnsignedShort NumberOfTriggersInPhase { get { return Items.FindFirst<UnsignedShort>("00540211") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTriggersInPhase_ { get { return Items.FindAll<UnsignedShort>("00540211").ToList(); } }
        public SequenceSelector ViewCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540220")); } }
        public List<SequenceSelector> ViewCodeSequence_ { get { return Items.FindAll<Sequence>("00540220").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ViewModifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540222")); } }
        public List<SequenceSelector> ViewModifierCodeSequence_ { get { return Items.FindAll<Sequence>("00540222").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RadionuclideCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540300")); } }
        public List<SequenceSelector> RadionuclideCodeSequence_ { get { return Items.FindAll<Sequence>("00540300").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AdministrationRouteCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540302")); } }
        public List<SequenceSelector> AdministrationRouteCodeSequence_ { get { return Items.FindAll<Sequence>("00540302").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RadiopharmaceuticalCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540304")); } }
        public List<SequenceSelector> RadiopharmaceuticalCodeSequence_ { get { return Items.FindAll<Sequence>("00540304").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CalibrationDataSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540306")); } }
        public List<SequenceSelector> CalibrationDataSequence_ { get { return Items.FindAll<Sequence>("00540306").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort EnergyWindowNumber { get { return Items.FindFirst<UnsignedShort>("00540308") as UnsignedShort; } }
        public List<UnsignedShort> EnergyWindowNumber_ { get { return Items.FindAll<UnsignedShort>("00540308").ToList(); } }
        public ShortString ImageID { get { return Items.FindFirst<ShortString>("00540400") as ShortString; } }
        public List<ShortString> ImageID_ { get { return Items.FindAll<ShortString>("00540400").ToList(); } }
        public SequenceSelector PatientOrientationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540410")); } }
        public List<SequenceSelector> PatientOrientationCodeSequence_ { get { return Items.FindAll<Sequence>("00540410").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PatientOrientationModifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540412")); } }
        public List<SequenceSelector> PatientOrientationModifierCodeSequence_ { get { return Items.FindAll<Sequence>("00540412").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PatientGantryRelationshipCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00540414")); } }
        public List<SequenceSelector> PatientGantryRelationshipCodeSequence_ { get { return Items.FindAll<Sequence>("00540414").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SliceProgressionDirection { get { return Items.FindFirst<CodeString>("00540500") as CodeString; } }
        public List<CodeString> SliceProgressionDirection_ { get { return Items.FindAll<CodeString>("00540500").ToList(); } }
        public CodeString SeriesType { get { return Items.FindFirst<CodeString>("00541000") as CodeString; } }
        public List<CodeString> SeriesType_ { get { return Items.FindAll<CodeString>("00541000").ToList(); } }
        public CodeString Units { get { return Items.FindFirst<CodeString>("00541001") as CodeString; } }
        public List<CodeString> Units_ { get { return Items.FindAll<CodeString>("00541001").ToList(); } }
        public CodeString CountsSource { get { return Items.FindFirst<CodeString>("00541002") as CodeString; } }
        public List<CodeString> CountsSource_ { get { return Items.FindAll<CodeString>("00541002").ToList(); } }
        public CodeString ReprojectionMethod { get { return Items.FindFirst<CodeString>("00541004") as CodeString; } }
        public List<CodeString> ReprojectionMethod_ { get { return Items.FindAll<CodeString>("00541004").ToList(); } }
        public CodeString SUVType { get { return Items.FindFirst<CodeString>("00541006") as CodeString; } }
        public List<CodeString> SUVType_ { get { return Items.FindAll<CodeString>("00541006").ToList(); } }
        public CodeString RandomsCorrectionMethod { get { return Items.FindFirst<CodeString>("00541100") as CodeString; } }
        public List<CodeString> RandomsCorrectionMethod_ { get { return Items.FindAll<CodeString>("00541100").ToList(); } }
        public LongString AttenuationCorrectionMethod { get { return Items.FindFirst<LongString>("00541101") as LongString; } }
        public List<LongString> AttenuationCorrectionMethod_ { get { return Items.FindAll<LongString>("00541101").ToList(); } }
        public CodeString DecayCorrection { get { return Items.FindFirst<CodeString>("00541102") as CodeString; } }
        public List<CodeString> DecayCorrection_ { get { return Items.FindAll<CodeString>("00541102").ToList(); } }
        public LongString ReconstructionMethod { get { return Items.FindFirst<LongString>("00541103") as LongString; } }
        public List<LongString> ReconstructionMethod_ { get { return Items.FindAll<LongString>("00541103").ToList(); } }
        public LongString DetectorLinesOfResponseUsed { get { return Items.FindFirst<LongString>("00541104") as LongString; } }
        public List<LongString> DetectorLinesOfResponseUsed_ { get { return Items.FindAll<LongString>("00541104").ToList(); } }
        public LongString ScatterCorrectionMethod { get { return Items.FindFirst<LongString>("00541105") as LongString; } }
        public List<LongString> ScatterCorrectionMethod_ { get { return Items.FindAll<LongString>("00541105").ToList(); } }
        public DecimalString AxialAcceptance { get { return Items.FindFirst<DecimalString>("00541200") as DecimalString; } }
        public List<DecimalString> AxialAcceptance_ { get { return Items.FindAll<DecimalString>("00541200").ToList(); } }
        public IntegerString AxialMash { get { return Items.FindFirst<IntegerString>("00541201") as IntegerString; } }
        public List<IntegerString> AxialMash_ { get { return Items.FindAll<IntegerString>("00541201").ToList(); } }
        public IntegerString TransverseMash { get { return Items.FindFirst<IntegerString>("00541202") as IntegerString; } }
        public List<IntegerString> TransverseMash_ { get { return Items.FindAll<IntegerString>("00541202").ToList(); } }
        public DecimalString DetectorElementSize { get { return Items.FindFirst<DecimalString>("00541203") as DecimalString; } }
        public List<DecimalString> DetectorElementSize_ { get { return Items.FindAll<DecimalString>("00541203").ToList(); } }
        public DecimalString CoincidenceWindowWidth { get { return Items.FindFirst<DecimalString>("00541210") as DecimalString; } }
        public List<DecimalString> CoincidenceWindowWidth_ { get { return Items.FindAll<DecimalString>("00541210").ToList(); } }
        public CodeString SecondaryCountsType { get { return Items.FindFirst<CodeString>("00541220") as CodeString; } }
        public List<CodeString> SecondaryCountsType_ { get { return Items.FindAll<CodeString>("00541220").ToList(); } }
        public DecimalString FrameReferenceTime { get { return Items.FindFirst<DecimalString>("00541300") as DecimalString; } }
        public List<DecimalString> FrameReferenceTime_ { get { return Items.FindAll<DecimalString>("00541300").ToList(); } }
        public IntegerString PrimaryPromptsCountsAccumulated { get { return Items.FindFirst<IntegerString>("00541310") as IntegerString; } }
        public List<IntegerString> PrimaryPromptsCountsAccumulated_ { get { return Items.FindAll<IntegerString>("00541310").ToList(); } }
        public IntegerString SecondaryCountsAccumulated { get { return Items.FindFirst<IntegerString>("00541311") as IntegerString; } }
        public List<IntegerString> SecondaryCountsAccumulated_ { get { return Items.FindAll<IntegerString>("00541311").ToList(); } }
        public DecimalString SliceSensitivityFactor { get { return Items.FindFirst<DecimalString>("00541320") as DecimalString; } }
        public List<DecimalString> SliceSensitivityFactor_ { get { return Items.FindAll<DecimalString>("00541320").ToList(); } }
        public DecimalString DecayFactor { get { return Items.FindFirst<DecimalString>("00541321") as DecimalString; } }
        public List<DecimalString> DecayFactor_ { get { return Items.FindAll<DecimalString>("00541321").ToList(); } }
        public DecimalString DoseCalibrationFactor { get { return Items.FindFirst<DecimalString>("00541322") as DecimalString; } }
        public List<DecimalString> DoseCalibrationFactor_ { get { return Items.FindAll<DecimalString>("00541322").ToList(); } }
        public DecimalString ScatterFractionFactor { get { return Items.FindFirst<DecimalString>("00541323") as DecimalString; } }
        public List<DecimalString> ScatterFractionFactor_ { get { return Items.FindAll<DecimalString>("00541323").ToList(); } }
        public DecimalString DeadTimeFactor { get { return Items.FindFirst<DecimalString>("00541324") as DecimalString; } }
        public List<DecimalString> DeadTimeFactor_ { get { return Items.FindAll<DecimalString>("00541324").ToList(); } }
        public UnsignedShort ImageIndex { get { return Items.FindFirst<UnsignedShort>("00541330") as UnsignedShort; } }
        public List<UnsignedShort> ImageIndex_ { get { return Items.FindAll<UnsignedShort>("00541330").ToList(); } }
        public CodeString CountsIncludedRetired { get { return Items.FindFirst<CodeString>("00541400") as CodeString; } }
        public List<CodeString> CountsIncludedRetired_ { get { return Items.FindAll<CodeString>("00541400").ToList(); } }
        public CodeString DeadTimeCorrectionFlagRetired { get { return Items.FindFirst<CodeString>("00541401") as CodeString; } }
        public List<CodeString> DeadTimeCorrectionFlagRetired_ { get { return Items.FindAll<CodeString>("00541401").ToList(); } }
        public SequenceSelector HistogramSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00603000")); } }
        public List<SequenceSelector> HistogramSequence_ { get { return Items.FindAll<Sequence>("00603000").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort HistogramNumberOfBins { get { return Items.FindFirst<UnsignedShort>("00603002") as UnsignedShort; } }
        public List<UnsignedShort> HistogramNumberOfBins_ { get { return Items.FindAll<UnsignedShort>("00603002").ToList(); } }
        public UnsignedShort HistogramFirstBinValue { get { return Items.FindFirst<UnsignedShort>("00603004") as UnsignedShort; } }
        public List<UnsignedShort> HistogramFirstBinValue_ { get { return Items.FindAll<UnsignedShort>("00603004").ToList(); } }
        public UnsignedShort HistogramLastBinValue { get { return Items.FindFirst<UnsignedShort>("00603006") as UnsignedShort; } }
        public List<UnsignedShort> HistogramLastBinValue_ { get { return Items.FindAll<UnsignedShort>("00603006").ToList(); } }
        public UnsignedShort HistogramBinWidth { get { return Items.FindFirst<UnsignedShort>("00603008") as UnsignedShort; } }
        public List<UnsignedShort> HistogramBinWidth_ { get { return Items.FindAll<UnsignedShort>("00603008").ToList(); } }
        public LongString HistogramExplanation { get { return Items.FindFirst<LongString>("00603010") as LongString; } }
        public List<LongString> HistogramExplanation_ { get { return Items.FindAll<LongString>("00603010").ToList(); } }
        public UnsignedLong HistogramData { get { return Items.FindFirst<UnsignedLong>("00603020") as UnsignedLong; } }
        public List<UnsignedLong> HistogramData_ { get { return Items.FindAll<UnsignedLong>("00603020").ToList(); } }
        public CodeString SegmentationType { get { return Items.FindFirst<CodeString>("00620001") as CodeString; } }
        public List<CodeString> SegmentationType_ { get { return Items.FindAll<CodeString>("00620001").ToList(); } }
        public SequenceSelector SegmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00620002")); } }
        public List<SequenceSelector> SegmentSequence_ { get { return Items.FindAll<Sequence>("00620002").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SegmentedPropertyCategoryCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00620003")); } }
        public List<SequenceSelector> SegmentedPropertyCategoryCodeSequence_ { get { return Items.FindAll<Sequence>("00620003").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort SegmentNumber { get { return Items.FindFirst<UnsignedShort>("00620004") as UnsignedShort; } }
        public List<UnsignedShort> SegmentNumber_ { get { return Items.FindAll<UnsignedShort>("00620004").ToList(); } }
        public LongString SegmentLabel { get { return Items.FindFirst<LongString>("00620005") as LongString; } }
        public List<LongString> SegmentLabel_ { get { return Items.FindAll<LongString>("00620005").ToList(); } }
        public ShortText SegmentDescription { get { return Items.FindFirst<ShortText>("00620006") as ShortText; } }
        public List<ShortText> SegmentDescription_ { get { return Items.FindAll<ShortText>("00620006").ToList(); } }
        public CodeString SegmentAlgorithmType { get { return Items.FindFirst<CodeString>("00620008") as CodeString; } }
        public List<CodeString> SegmentAlgorithmType_ { get { return Items.FindAll<CodeString>("00620008").ToList(); } }
        public LongString SegmentAlgorithmName { get { return Items.FindFirst<LongString>("00620009") as LongString; } }
        public List<LongString> SegmentAlgorithmName_ { get { return Items.FindAll<LongString>("00620009").ToList(); } }
        public SequenceSelector SegmentIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0062000A")); } }
        public List<SequenceSelector> SegmentIdentificationSequence_ { get { return Items.FindAll<Sequence>("0062000A").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReferencedSegmentNumber { get { return Items.FindFirst<UnsignedShort>("0062000B") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedSegmentNumber_ { get { return Items.FindAll<UnsignedShort>("0062000B").ToList(); } }
        public UnsignedShort RecommendedDisplayGrayscaleValue { get { return Items.FindFirst<UnsignedShort>("0062000C") as UnsignedShort; } }
        public List<UnsignedShort> RecommendedDisplayGrayscaleValue_ { get { return Items.FindAll<UnsignedShort>("0062000C").ToList(); } }
        public UnsignedShort RecommendedDisplayCIELabValue { get { return Items.FindFirst<UnsignedShort>("0062000D") as UnsignedShort; } }
        public List<UnsignedShort> RecommendedDisplayCIELabValue_ { get { return Items.FindAll<UnsignedShort>("0062000D").ToList(); } }
        public UnsignedShort MaximumFractionalValue { get { return Items.FindFirst<UnsignedShort>("0062000E") as UnsignedShort; } }
        public List<UnsignedShort> MaximumFractionalValue_ { get { return Items.FindAll<UnsignedShort>("0062000E").ToList(); } }
        public SequenceSelector SegmentedPropertyTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0062000F")); } }
        public List<SequenceSelector> SegmentedPropertyTypeCodeSequence_ { get { return Items.FindAll<Sequence>("0062000F").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SegmentationFractionalType { get { return Items.FindFirst<CodeString>("00620010") as CodeString; } }
        public List<CodeString> SegmentationFractionalType_ { get { return Items.FindAll<CodeString>("00620010").ToList(); } }
        public SequenceSelector DeformableRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00640002")); } }
        public List<SequenceSelector> DeformableRegistrationSequence_ { get { return Items.FindAll<Sequence>("00640002").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier SourceFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("00640003") as UniqueIdentifier; } }
        public List<UniqueIdentifier> SourceFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("00640003").ToList(); } }
        public SequenceSelector DeformableRegistrationGridSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00640005")); } }
        public List<SequenceSelector> DeformableRegistrationGridSequence_ { get { return Items.FindAll<Sequence>("00640005").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong GridDimensions { get { return Items.FindFirst<UnsignedLong>("00640007") as UnsignedLong; } }
        public List<UnsignedLong> GridDimensions_ { get { return Items.FindAll<UnsignedLong>("00640007").ToList(); } }
        public FloatingPointDouble GridResolution { get { return Items.FindFirst<FloatingPointDouble>("00640008") as FloatingPointDouble; } }
        public List<FloatingPointDouble> GridResolution_ { get { return Items.FindAll<FloatingPointDouble>("00640008").ToList(); } }
        public OtherFloatString VectorGridData { get { return Items.FindFirst<OtherFloatString>("00640009") as OtherFloatString; } }
        public List<OtherFloatString> VectorGridData_ { get { return Items.FindAll<OtherFloatString>("00640009").ToList(); } }
        public SequenceSelector PreDeformationMatrixRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0064000F")); } }
        public List<SequenceSelector> PreDeformationMatrixRegistrationSequence_ { get { return Items.FindAll<Sequence>("0064000F").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PostDeformationMatrixRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00640010")); } }
        public List<SequenceSelector> PostDeformationMatrixRegistrationSequence_ { get { return Items.FindAll<Sequence>("00640010").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong NumberOfSurfaces { get { return Items.FindFirst<UnsignedLong>("00660001") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfSurfaces_ { get { return Items.FindAll<UnsignedLong>("00660001").ToList(); } }
        public SequenceSelector SurfaceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660002")); } }
        public List<SequenceSelector> SurfaceSequence_ { get { return Items.FindAll<Sequence>("00660002").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong SurfaceNumber { get { return Items.FindFirst<UnsignedLong>("00660003") as UnsignedLong; } }
        public List<UnsignedLong> SurfaceNumber_ { get { return Items.FindAll<UnsignedLong>("00660003").ToList(); } }
        public LongText SurfaceComments { get { return Items.FindFirst<LongText>("00660004") as LongText; } }
        public List<LongText> SurfaceComments_ { get { return Items.FindAll<LongText>("00660004").ToList(); } }
        public CodeString SurfaceProcessing { get { return Items.FindFirst<CodeString>("00660009") as CodeString; } }
        public List<CodeString> SurfaceProcessing_ { get { return Items.FindAll<CodeString>("00660009").ToList(); } }
        public FloatingPointSingle SurfaceProcessingRatio { get { return Items.FindFirst<FloatingPointSingle>("0066000A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SurfaceProcessingRatio_ { get { return Items.FindAll<FloatingPointSingle>("0066000A").ToList(); } }
        public LongString SurfaceProcessingDescription { get { return Items.FindFirst<LongString>("0066000B") as LongString; } }
        public List<LongString> SurfaceProcessingDescription_ { get { return Items.FindAll<LongString>("0066000B").ToList(); } }
        public FloatingPointSingle RecommendedPresentationOpacity { get { return Items.FindFirst<FloatingPointSingle>("0066000C") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RecommendedPresentationOpacity_ { get { return Items.FindAll<FloatingPointSingle>("0066000C").ToList(); } }
        public CodeString RecommendedPresentationType { get { return Items.FindFirst<CodeString>("0066000D") as CodeString; } }
        public List<CodeString> RecommendedPresentationType_ { get { return Items.FindAll<CodeString>("0066000D").ToList(); } }
        public CodeString FiniteVolume { get { return Items.FindFirst<CodeString>("0066000E") as CodeString; } }
        public List<CodeString> FiniteVolume_ { get { return Items.FindAll<CodeString>("0066000E").ToList(); } }
        public CodeString Manifold { get { return Items.FindFirst<CodeString>("00660010") as CodeString; } }
        public List<CodeString> Manifold_ { get { return Items.FindAll<CodeString>("00660010").ToList(); } }
        public SequenceSelector SurfacePointsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660011")); } }
        public List<SequenceSelector> SurfacePointsSequence_ { get { return Items.FindAll<Sequence>("00660011").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SurfacePointsNormalsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660012")); } }
        public List<SequenceSelector> SurfacePointsNormalsSequence_ { get { return Items.FindAll<Sequence>("00660012").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SurfaceMeshPrimitivesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660013")); } }
        public List<SequenceSelector> SurfaceMeshPrimitivesSequence_ { get { return Items.FindAll<Sequence>("00660013").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong NumberOfSurfacePoints { get { return Items.FindFirst<UnsignedLong>("00660015") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfSurfacePoints_ { get { return Items.FindAll<UnsignedLong>("00660015").ToList(); } }
        public OtherFloatString PointCoordinatesData { get { return Items.FindFirst<OtherFloatString>("00660016") as OtherFloatString; } }
        public List<OtherFloatString> PointCoordinatesData_ { get { return Items.FindAll<OtherFloatString>("00660016").ToList(); } }
        public FloatingPointSingle PointPositionAccuracy { get { return Items.FindFirst<FloatingPointSingle>("00660017") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PointPositionAccuracy_ { get { return Items.FindAll<FloatingPointSingle>("00660017").ToList(); } }
        public FloatingPointSingle MeanPointDistance { get { return Items.FindFirst<FloatingPointSingle>("00660018") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MeanPointDistance_ { get { return Items.FindAll<FloatingPointSingle>("00660018").ToList(); } }
        public FloatingPointSingle MaximumPointDistance { get { return Items.FindFirst<FloatingPointSingle>("00660019") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MaximumPointDistance_ { get { return Items.FindAll<FloatingPointSingle>("00660019").ToList(); } }
        public FloatingPointSingle PointsBoundingBoxCoordinates { get { return Items.FindFirst<FloatingPointSingle>("0066001A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PointsBoundingBoxCoordinates_ { get { return Items.FindAll<FloatingPointSingle>("0066001A").ToList(); } }
        public FloatingPointSingle AxisOfRotation { get { return Items.FindFirst<FloatingPointSingle>("0066001B") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AxisOfRotation_ { get { return Items.FindAll<FloatingPointSingle>("0066001B").ToList(); } }
        public FloatingPointSingle CenterOfRotation { get { return Items.FindFirst<FloatingPointSingle>("0066001C") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CenterOfRotation_ { get { return Items.FindAll<FloatingPointSingle>("0066001C").ToList(); } }
        public UnsignedLong NumberOfVectors { get { return Items.FindFirst<UnsignedLong>("0066001E") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfVectors_ { get { return Items.FindAll<UnsignedLong>("0066001E").ToList(); } }
        public UnsignedShort VectorDimensionality { get { return Items.FindFirst<UnsignedShort>("0066001F") as UnsignedShort; } }
        public List<UnsignedShort> VectorDimensionality_ { get { return Items.FindAll<UnsignedShort>("0066001F").ToList(); } }
        public FloatingPointSingle VectorAccuracy { get { return Items.FindFirst<FloatingPointSingle>("00660020") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VectorAccuracy_ { get { return Items.FindAll<FloatingPointSingle>("00660020").ToList(); } }
        public OtherFloatString VectorCoordinateData { get { return Items.FindFirst<OtherFloatString>("00660021") as OtherFloatString; } }
        public List<OtherFloatString> VectorCoordinateData_ { get { return Items.FindAll<OtherFloatString>("00660021").ToList(); } }
        public OtherWordString TrianglePointIndexList { get { return Items.FindFirst<OtherWordString>("00660023") as OtherWordString; } }
        public List<OtherWordString> TrianglePointIndexList_ { get { return Items.FindAll<OtherWordString>("00660023").ToList(); } }
        public OtherWordString EdgePointIndexList { get { return Items.FindFirst<OtherWordString>("00660024") as OtherWordString; } }
        public List<OtherWordString> EdgePointIndexList_ { get { return Items.FindAll<OtherWordString>("00660024").ToList(); } }
        public OtherWordString VertexPointIndexList { get { return Items.FindFirst<OtherWordString>("00660025") as OtherWordString; } }
        public List<OtherWordString> VertexPointIndexList_ { get { return Items.FindAll<OtherWordString>("00660025").ToList(); } }
        public SequenceSelector TriangleStripSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660026")); } }
        public List<SequenceSelector> TriangleStripSequence_ { get { return Items.FindAll<Sequence>("00660026").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TriangleFanSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660027")); } }
        public List<SequenceSelector> TriangleFanSequence_ { get { return Items.FindAll<Sequence>("00660027").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LineSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660028")); } }
        public List<SequenceSelector> LineSequence_ { get { return Items.FindAll<Sequence>("00660028").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherWordString PrimitivePointIndexList { get { return Items.FindFirst<OtherWordString>("00660029") as OtherWordString; } }
        public List<OtherWordString> PrimitivePointIndexList_ { get { return Items.FindAll<OtherWordString>("00660029").ToList(); } }
        public UnsignedLong SurfaceCount { get { return Items.FindFirst<UnsignedLong>("0066002A") as UnsignedLong; } }
        public List<UnsignedLong> SurfaceCount_ { get { return Items.FindAll<UnsignedLong>("0066002A").ToList(); } }
        public SequenceSelector ReferencedSurfaceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0066002B")); } }
        public List<SequenceSelector> ReferencedSurfaceSequence_ { get { return Items.FindAll<Sequence>("0066002B").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong ReferencedSurfaceNumber { get { return Items.FindFirst<UnsignedLong>("0066002C") as UnsignedLong; } }
        public List<UnsignedLong> ReferencedSurfaceNumber_ { get { return Items.FindAll<UnsignedLong>("0066002C").ToList(); } }
        public SequenceSelector SegmentSurfaceGenerationAlgorithmIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0066002D")); } }
        public List<SequenceSelector> SegmentSurfaceGenerationAlgorithmIdentificationSequence_ { get { return Items.FindAll<Sequence>("0066002D").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SegmentSurfaceSourceInstanceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0066002E")); } }
        public List<SequenceSelector> SegmentSurfaceSourceInstanceSequence_ { get { return Items.FindAll<Sequence>("0066002E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AlgorithmFamilyCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0066002F")); } }
        public List<SequenceSelector> AlgorithmFamilyCodeSequence_ { get { return Items.FindAll<Sequence>("0066002F").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AlgorithmNameCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660030")); } }
        public List<SequenceSelector> AlgorithmNameCodeSequence_ { get { return Items.FindAll<Sequence>("00660030").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString AlgorithmVersion { get { return Items.FindFirst<LongString>("00660031") as LongString; } }
        public List<LongString> AlgorithmVersion_ { get { return Items.FindAll<LongString>("00660031").ToList(); } }
        public LongText AlgorithmParameters { get { return Items.FindFirst<LongText>("00660032") as LongText; } }
        public List<LongText> AlgorithmParameters_ { get { return Items.FindAll<LongText>("00660032").ToList(); } }
        public SequenceSelector FacetSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660034")); } }
        public List<SequenceSelector> FacetSequence_ { get { return Items.FindAll<Sequence>("00660034").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SurfaceProcessingAlgorithmIdentificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00660035")); } }
        public List<SequenceSelector> SurfaceProcessingAlgorithmIdentificationSequence_ { get { return Items.FindAll<Sequence>("00660035").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString AlgorithmName { get { return Items.FindFirst<LongString>("00660036") as LongString; } }
        public List<LongString> AlgorithmName_ { get { return Items.FindAll<LongString>("00660036").ToList(); } }
        public LongString ImplantSize { get { return Items.FindFirst<LongString>("00686210") as LongString; } }
        public List<LongString> ImplantSize_ { get { return Items.FindAll<LongString>("00686210").ToList(); } }
        public LongString ImplantTemplateVersion { get { return Items.FindFirst<LongString>("00686221") as LongString; } }
        public List<LongString> ImplantTemplateVersion_ { get { return Items.FindAll<LongString>("00686221").ToList(); } }
        public SequenceSelector ReplacedImplantTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686222")); } }
        public List<SequenceSelector> ReplacedImplantTemplateSequence_ { get { return Items.FindAll<Sequence>("00686222").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ImplantType { get { return Items.FindFirst<CodeString>("00686223") as CodeString; } }
        public List<CodeString> ImplantType_ { get { return Items.FindAll<CodeString>("00686223").ToList(); } }
        public SequenceSelector DerivationImplantTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686224")); } }
        public List<SequenceSelector> DerivationImplantTemplateSequence_ { get { return Items.FindAll<Sequence>("00686224").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OriginalImplantTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686225")); } }
        public List<SequenceSelector> OriginalImplantTemplateSequence_ { get { return Items.FindAll<Sequence>("00686225").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime EffectiveDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00686226") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> EffectiveDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00686226").ToList(); } }
        public SequenceSelector ImplantTargetAnatomySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686230")); } }
        public List<SequenceSelector> ImplantTargetAnatomySequence_ { get { return Items.FindAll<Sequence>("00686230").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector InformationFromManufacturerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686260")); } }
        public List<SequenceSelector> InformationFromManufacturerSequence_ { get { return Items.FindAll<Sequence>("00686260").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector NotificationFromManufacturerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686265")); } }
        public List<SequenceSelector> NotificationFromManufacturerSequence_ { get { return Items.FindAll<Sequence>("00686265").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime InformationIssueDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("00686270") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> InformationIssueDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("00686270").ToList(); } }
        public ShortText InformationSummary { get { return Items.FindFirst<ShortText>("00686280") as ShortText; } }
        public List<ShortText> InformationSummary_ { get { return Items.FindAll<ShortText>("00686280").ToList(); } }
        public SequenceSelector ImplantRegulatoryDisapprovalCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006862A0")); } }
        public List<SequenceSelector> ImplantRegulatoryDisapprovalCodeSequence_ { get { return Items.FindAll<Sequence>("006862A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble OverallTemplateSpatialTolerance { get { return Items.FindFirst<FloatingPointDouble>("006862A5") as FloatingPointDouble; } }
        public List<FloatingPointDouble> OverallTemplateSpatialTolerance_ { get { return Items.FindAll<FloatingPointDouble>("006862A5").ToList(); } }
        public SequenceSelector HPGLDocumentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006862C0")); } }
        public List<SequenceSelector> HPGLDocumentSequence_ { get { return Items.FindAll<Sequence>("006862C0").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort HPGLDocumentID { get { return Items.FindFirst<UnsignedShort>("006862D0") as UnsignedShort; } }
        public List<UnsignedShort> HPGLDocumentID_ { get { return Items.FindAll<UnsignedShort>("006862D0").ToList(); } }
        public LongString HPGLDocumentLabel { get { return Items.FindFirst<LongString>("006862D5") as LongString; } }
        public List<LongString> HPGLDocumentLabel_ { get { return Items.FindAll<LongString>("006862D5").ToList(); } }
        public SequenceSelector ViewOrientationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006862E0")); } }
        public List<SequenceSelector> ViewOrientationCodeSequence_ { get { return Items.FindAll<Sequence>("006862E0").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ViewOrientationModifier { get { return Items.FindFirst<FloatingPointDouble>("006862F0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ViewOrientationModifier_ { get { return Items.FindAll<FloatingPointDouble>("006862F0").ToList(); } }
        public FloatingPointDouble HPGLDocumentScaling { get { return Items.FindFirst<FloatingPointDouble>("006862F2") as FloatingPointDouble; } }
        public List<FloatingPointDouble> HPGLDocumentScaling_ { get { return Items.FindAll<FloatingPointDouble>("006862F2").ToList(); } }
        public OtherByteString HPGLDocument { get { return Items.FindFirst<OtherByteString>("00686300") as OtherByteString; } }
        public List<OtherByteString> HPGLDocument_ { get { return Items.FindAll<OtherByteString>("00686300").ToList(); } }
        public UnsignedShort HPGLContourPenNumber { get { return Items.FindFirst<UnsignedShort>("00686310") as UnsignedShort; } }
        public List<UnsignedShort> HPGLContourPenNumber_ { get { return Items.FindAll<UnsignedShort>("00686310").ToList(); } }
        public SequenceSelector HPGLPenSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686320")); } }
        public List<SequenceSelector> HPGLPenSequence_ { get { return Items.FindAll<Sequence>("00686320").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort HPGLPenNumber { get { return Items.FindFirst<UnsignedShort>("00686330") as UnsignedShort; } }
        public List<UnsignedShort> HPGLPenNumber_ { get { return Items.FindAll<UnsignedShort>("00686330").ToList(); } }
        public LongString HPGLPenLabel { get { return Items.FindFirst<LongString>("00686340") as LongString; } }
        public List<LongString> HPGLPenLabel_ { get { return Items.FindAll<LongString>("00686340").ToList(); } }
        public ShortText HPGLPenDescription { get { return Items.FindFirst<ShortText>("00686345") as ShortText; } }
        public List<ShortText> HPGLPenDescription_ { get { return Items.FindAll<ShortText>("00686345").ToList(); } }
        public FloatingPointDouble RecommendedRotationPoint { get { return Items.FindFirst<FloatingPointDouble>("00686346") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RecommendedRotationPoint_ { get { return Items.FindAll<FloatingPointDouble>("00686346").ToList(); } }
        public FloatingPointDouble BoundingRectangle { get { return Items.FindFirst<FloatingPointDouble>("00686347") as FloatingPointDouble; } }
        public List<FloatingPointDouble> BoundingRectangle_ { get { return Items.FindAll<FloatingPointDouble>("00686347").ToList(); } }
        public UnsignedShort ImplantTemplate3DModelSurfaceNumber { get { return Items.FindFirst<UnsignedShort>("00686350") as UnsignedShort; } }
        public List<UnsignedShort> ImplantTemplate3DModelSurfaceNumber_ { get { return Items.FindAll<UnsignedShort>("00686350").ToList(); } }
        public SequenceSelector SurfaceModelDescriptionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686360")); } }
        public List<SequenceSelector> SurfaceModelDescriptionSequence_ { get { return Items.FindAll<Sequence>("00686360").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SurfaceModelLabel { get { return Items.FindFirst<LongString>("00686380") as LongString; } }
        public List<LongString> SurfaceModelLabel_ { get { return Items.FindAll<LongString>("00686380").ToList(); } }
        public FloatingPointDouble SurfaceModelScalingFactor { get { return Items.FindFirst<FloatingPointDouble>("00686390") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SurfaceModelScalingFactor_ { get { return Items.FindAll<FloatingPointDouble>("00686390").ToList(); } }
        public SequenceSelector MaterialsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863A0")); } }
        public List<SequenceSelector> MaterialsCodeSequence_ { get { return Items.FindAll<Sequence>("006863A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector CoatingMaterialsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863A4")); } }
        public List<SequenceSelector> CoatingMaterialsCodeSequence_ { get { return Items.FindAll<Sequence>("006863A4").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImplantTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863A8")); } }
        public List<SequenceSelector> ImplantTypeCodeSequence_ { get { return Items.FindAll<Sequence>("006863A8").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FixationMethodCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863AC")); } }
        public List<SequenceSelector> FixationMethodCodeSequence_ { get { return Items.FindAll<Sequence>("006863AC").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MatingFeatureSetsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863B0")); } }
        public List<SequenceSelector> MatingFeatureSetsSequence_ { get { return Items.FindAll<Sequence>("006863B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort MatingFeatureSetID { get { return Items.FindFirst<UnsignedShort>("006863C0") as UnsignedShort; } }
        public List<UnsignedShort> MatingFeatureSetID_ { get { return Items.FindAll<UnsignedShort>("006863C0").ToList(); } }
        public LongString MatingFeatureSetLabel { get { return Items.FindFirst<LongString>("006863D0") as LongString; } }
        public List<LongString> MatingFeatureSetLabel_ { get { return Items.FindAll<LongString>("006863D0").ToList(); } }
        public SequenceSelector MatingFeatureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006863E0")); } }
        public List<SequenceSelector> MatingFeatureSequence_ { get { return Items.FindAll<Sequence>("006863E0").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort MatingFeatureID { get { return Items.FindFirst<UnsignedShort>("006863F0") as UnsignedShort; } }
        public List<UnsignedShort> MatingFeatureID_ { get { return Items.FindAll<UnsignedShort>("006863F0").ToList(); } }
        public SequenceSelector MatingFeatureDegreeOfFreedomSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686400")); } }
        public List<SequenceSelector> MatingFeatureDegreeOfFreedomSequence_ { get { return Items.FindAll<Sequence>("00686400").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort DegreeOfFreedomID { get { return Items.FindFirst<UnsignedShort>("00686410") as UnsignedShort; } }
        public List<UnsignedShort> DegreeOfFreedomID_ { get { return Items.FindAll<UnsignedShort>("00686410").ToList(); } }
        public CodeString DegreeOfFreedomType { get { return Items.FindFirst<CodeString>("00686420") as CodeString; } }
        public List<CodeString> DegreeOfFreedomType_ { get { return Items.FindAll<CodeString>("00686420").ToList(); } }
        public SequenceSelector TwoDMatingFeatureCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686430")); } }
        public List<SequenceSelector> TwoDMatingFeatureCoordinatesSequence_ { get { return Items.FindAll<Sequence>("00686430").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReferencedHPGLDocumentID { get { return Items.FindFirst<UnsignedShort>("00686440") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedHPGLDocumentID_ { get { return Items.FindAll<UnsignedShort>("00686440").ToList(); } }
        public FloatingPointDouble TwoDMatingPoint { get { return Items.FindFirst<FloatingPointDouble>("00686450") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDMatingPoint_ { get { return Items.FindAll<FloatingPointDouble>("00686450").ToList(); } }
        public FloatingPointDouble TwoDMatingAxes { get { return Items.FindFirst<FloatingPointDouble>("00686460") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDMatingAxes_ { get { return Items.FindAll<FloatingPointDouble>("00686460").ToList(); } }
        public SequenceSelector TwoDDegreeOfFreedomSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686470")); } }
        public List<SequenceSelector> TwoDDegreeOfFreedomSequence_ { get { return Items.FindAll<Sequence>("00686470").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ThreeDDegreeOfFreedomAxis { get { return Items.FindFirst<FloatingPointDouble>("00686490") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDDegreeOfFreedomAxis_ { get { return Items.FindAll<FloatingPointDouble>("00686490").ToList(); } }
        public FloatingPointDouble RangeOfFreedom { get { return Items.FindFirst<FloatingPointDouble>("006864A0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RangeOfFreedom_ { get { return Items.FindAll<FloatingPointDouble>("006864A0").ToList(); } }
        public FloatingPointDouble ThreeDMatingPoint { get { return Items.FindFirst<FloatingPointDouble>("006864C0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDMatingPoint_ { get { return Items.FindAll<FloatingPointDouble>("006864C0").ToList(); } }
        public FloatingPointDouble ThreeDMatingAxes { get { return Items.FindFirst<FloatingPointDouble>("006864D0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDMatingAxes_ { get { return Items.FindAll<FloatingPointDouble>("006864D0").ToList(); } }
        public FloatingPointDouble TwoDDegreeOfFreedomAxis { get { return Items.FindFirst<FloatingPointDouble>("006864F0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDDegreeOfFreedomAxis_ { get { return Items.FindAll<FloatingPointDouble>("006864F0").ToList(); } }
        public SequenceSelector PlanningLandmarkPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686500")); } }
        public List<SequenceSelector> PlanningLandmarkPointSequence_ { get { return Items.FindAll<Sequence>("00686500").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlanningLandmarkLineSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686510")); } }
        public List<SequenceSelector> PlanningLandmarkLineSequence_ { get { return Items.FindAll<Sequence>("00686510").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PlanningLandmarkPlaneSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686520")); } }
        public List<SequenceSelector> PlanningLandmarkPlaneSequence_ { get { return Items.FindAll<Sequence>("00686520").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort PlanningLandmarkID { get { return Items.FindFirst<UnsignedShort>("00686530") as UnsignedShort; } }
        public List<UnsignedShort> PlanningLandmarkID_ { get { return Items.FindAll<UnsignedShort>("00686530").ToList(); } }
        public LongString PlanningLandmarkDescription { get { return Items.FindFirst<LongString>("00686540") as LongString; } }
        public List<LongString> PlanningLandmarkDescription_ { get { return Items.FindAll<LongString>("00686540").ToList(); } }
        public SequenceSelector PlanningLandmarkIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686545")); } }
        public List<SequenceSelector> PlanningLandmarkIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("00686545").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TwoDPointCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00686550")); } }
        public List<SequenceSelector> TwoDPointCoordinatesSequence_ { get { return Items.FindAll<Sequence>("00686550").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TwoDPointCoordinates { get { return Items.FindFirst<FloatingPointDouble>("00686560") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDPointCoordinates_ { get { return Items.FindAll<FloatingPointDouble>("00686560").ToList(); } }
        public FloatingPointDouble ThreeDPointCoordinates { get { return Items.FindFirst<FloatingPointDouble>("00686590") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDPointCoordinates_ { get { return Items.FindAll<FloatingPointDouble>("00686590").ToList(); } }
        public SequenceSelector TwoDLineCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006865A0")); } }
        public List<SequenceSelector> TwoDLineCoordinatesSequence_ { get { return Items.FindAll<Sequence>("006865A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TwoDLineCoordinates { get { return Items.FindFirst<FloatingPointDouble>("006865B0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDLineCoordinates_ { get { return Items.FindAll<FloatingPointDouble>("006865B0").ToList(); } }
        public FloatingPointDouble ThreeDLineCoordinates { get { return Items.FindFirst<FloatingPointDouble>("006865D0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDLineCoordinates_ { get { return Items.FindAll<FloatingPointDouble>("006865D0").ToList(); } }
        public SequenceSelector TwoDPlaneCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("006865E0")); } }
        public List<SequenceSelector> TwoDPlaneCoordinatesSequence_ { get { return Items.FindAll<Sequence>("006865E0").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TwoDPlaneIntersection { get { return Items.FindFirst<FloatingPointDouble>("006865F0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDPlaneIntersection_ { get { return Items.FindAll<FloatingPointDouble>("006865F0").ToList(); } }
        public FloatingPointDouble ThreeDPlaneOrigin { get { return Items.FindFirst<FloatingPointDouble>("00686610") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDPlaneOrigin_ { get { return Items.FindAll<FloatingPointDouble>("00686610").ToList(); } }
        public FloatingPointDouble ThreeDPlaneNormal { get { return Items.FindFirst<FloatingPointDouble>("00686620") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDPlaneNormal_ { get { return Items.FindAll<FloatingPointDouble>("00686620").ToList(); } }
        public SequenceSelector GraphicAnnotationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700001")); } }
        public List<SequenceSelector> GraphicAnnotationSequence_ { get { return Items.FindAll<Sequence>("00700001").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString GraphicLayer { get { return Items.FindFirst<CodeString>("00700002") as CodeString; } }
        public List<CodeString> GraphicLayer_ { get { return Items.FindAll<CodeString>("00700002").ToList(); } }
        public CodeString BoundingBoxAnnotationUnits { get { return Items.FindFirst<CodeString>("00700003") as CodeString; } }
        public List<CodeString> BoundingBoxAnnotationUnits_ { get { return Items.FindAll<CodeString>("00700003").ToList(); } }
        public CodeString AnchorPointAnnotationUnits { get { return Items.FindFirst<CodeString>("00700004") as CodeString; } }
        public List<CodeString> AnchorPointAnnotationUnits_ { get { return Items.FindAll<CodeString>("00700004").ToList(); } }
        public CodeString GraphicAnnotationUnits { get { return Items.FindFirst<CodeString>("00700005") as CodeString; } }
        public List<CodeString> GraphicAnnotationUnits_ { get { return Items.FindAll<CodeString>("00700005").ToList(); } }
        public ShortText UnformattedTextValue { get { return Items.FindFirst<ShortText>("00700006") as ShortText; } }
        public List<ShortText> UnformattedTextValue_ { get { return Items.FindAll<ShortText>("00700006").ToList(); } }
        public SequenceSelector TextObjectSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700008")); } }
        public List<SequenceSelector> TextObjectSequence_ { get { return Items.FindAll<Sequence>("00700008").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector GraphicObjectSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700009")); } }
        public List<SequenceSelector> GraphicObjectSequence_ { get { return Items.FindAll<Sequence>("00700009").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle BoundingBoxTopLeftHandCorner { get { return Items.FindFirst<FloatingPointSingle>("00700010") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BoundingBoxTopLeftHandCorner_ { get { return Items.FindAll<FloatingPointSingle>("00700010").ToList(); } }
        public FloatingPointSingle BoundingBoxBottomRightHandCorner { get { return Items.FindFirst<FloatingPointSingle>("00700011") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BoundingBoxBottomRightHandCorner_ { get { return Items.FindAll<FloatingPointSingle>("00700011").ToList(); } }
        public CodeString BoundingBoxTextHorizontalJustification { get { return Items.FindFirst<CodeString>("00700012") as CodeString; } }
        public List<CodeString> BoundingBoxTextHorizontalJustification_ { get { return Items.FindAll<CodeString>("00700012").ToList(); } }
        public FloatingPointSingle AnchorPoint { get { return Items.FindFirst<FloatingPointSingle>("00700014") as FloatingPointSingle; } }
        public List<FloatingPointSingle> AnchorPoint_ { get { return Items.FindAll<FloatingPointSingle>("00700014").ToList(); } }
        public CodeString AnchorPointVisibility { get { return Items.FindFirst<CodeString>("00700015") as CodeString; } }
        public List<CodeString> AnchorPointVisibility_ { get { return Items.FindAll<CodeString>("00700015").ToList(); } }
        public UnsignedShort GraphicDimensions { get { return Items.FindFirst<UnsignedShort>("00700020") as UnsignedShort; } }
        public List<UnsignedShort> GraphicDimensions_ { get { return Items.FindAll<UnsignedShort>("00700020").ToList(); } }
        public UnsignedShort NumberOfGraphicPoints { get { return Items.FindFirst<UnsignedShort>("00700021") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfGraphicPoints_ { get { return Items.FindAll<UnsignedShort>("00700021").ToList(); } }
        public FloatingPointSingle GraphicData { get { return Items.FindFirst<FloatingPointSingle>("00700022") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GraphicData_ { get { return Items.FindAll<FloatingPointSingle>("00700022").ToList(); } }
        public CodeString GraphicType { get { return Items.FindFirst<CodeString>("00700023") as CodeString; } }
        public List<CodeString> GraphicType_ { get { return Items.FindAll<CodeString>("00700023").ToList(); } }
        public CodeString GraphicFilled { get { return Items.FindFirst<CodeString>("00700024") as CodeString; } }
        public List<CodeString> GraphicFilled_ { get { return Items.FindAll<CodeString>("00700024").ToList(); } }
        public IntegerString ImageRotationRetired { get { return Items.FindFirst<IntegerString>("00700040") as IntegerString; } }
        public List<IntegerString> ImageRotationRetired_ { get { return Items.FindAll<IntegerString>("00700040").ToList(); } }
        public CodeString ImageHorizontalFlip { get { return Items.FindFirst<CodeString>("00700041") as CodeString; } }
        public List<CodeString> ImageHorizontalFlip_ { get { return Items.FindAll<CodeString>("00700041").ToList(); } }
        public UnsignedShort ImageRotation { get { return Items.FindFirst<UnsignedShort>("00700042") as UnsignedShort; } }
        public List<UnsignedShort> ImageRotation_ { get { return Items.FindAll<UnsignedShort>("00700042").ToList(); } }
        public UnsignedShort DisplayedAreaTopLeftHandCornerTrialRetired { get { return Items.FindFirst<UnsignedShort>("00700050") as UnsignedShort; } }
        public List<UnsignedShort> DisplayedAreaTopLeftHandCornerTrialRetired_ { get { return Items.FindAll<UnsignedShort>("00700050").ToList(); } }
        public UnsignedShort DisplayedAreaBottomRightHandCornerTrialRetired { get { return Items.FindFirst<UnsignedShort>("00700051") as UnsignedShort; } }
        public List<UnsignedShort> DisplayedAreaBottomRightHandCornerTrialRetired_ { get { return Items.FindAll<UnsignedShort>("00700051").ToList(); } }
        public SignedLong DisplayedAreaTopLeftHandCorner { get { return Items.FindFirst<SignedLong>("00700052") as SignedLong; } }
        public List<SignedLong> DisplayedAreaTopLeftHandCorner_ { get { return Items.FindAll<SignedLong>("00700052").ToList(); } }
        public SignedLong DisplayedAreaBottomRightHandCorner { get { return Items.FindFirst<SignedLong>("00700053") as SignedLong; } }
        public List<SignedLong> DisplayedAreaBottomRightHandCorner_ { get { return Items.FindAll<SignedLong>("00700053").ToList(); } }
        public SequenceSelector DisplayedAreaSelectionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0070005A")); } }
        public List<SequenceSelector> DisplayedAreaSelectionSequence_ { get { return Items.FindAll<Sequence>("0070005A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector GraphicLayerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700060")); } }
        public List<SequenceSelector> GraphicLayerSequence_ { get { return Items.FindAll<Sequence>("00700060").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString GraphicLayerOrder { get { return Items.FindFirst<IntegerString>("00700062") as IntegerString; } }
        public List<IntegerString> GraphicLayerOrder_ { get { return Items.FindAll<IntegerString>("00700062").ToList(); } }
        public UnsignedShort GraphicLayerRecommendedDisplayGrayscaleValue { get { return Items.FindFirst<UnsignedShort>("00700066") as UnsignedShort; } }
        public List<UnsignedShort> GraphicLayerRecommendedDisplayGrayscaleValue_ { get { return Items.FindAll<UnsignedShort>("00700066").ToList(); } }
        public UnsignedShort GraphicLayerRecommendedDisplayRGBValueRetired { get { return Items.FindFirst<UnsignedShort>("00700067") as UnsignedShort; } }
        public List<UnsignedShort> GraphicLayerRecommendedDisplayRGBValueRetired_ { get { return Items.FindAll<UnsignedShort>("00700067").ToList(); } }
        public LongString GraphicLayerDescription { get { return Items.FindFirst<LongString>("00700068") as LongString; } }
        public List<LongString> GraphicLayerDescription_ { get { return Items.FindAll<LongString>("00700068").ToList(); } }
        public CodeString ContentLabel { get { return Items.FindFirst<CodeString>("00700080") as CodeString; } }
        public List<CodeString> ContentLabel_ { get { return Items.FindAll<CodeString>("00700080").ToList(); } }
        public LongString ContentDescription { get { return Items.FindFirst<LongString>("00700081") as LongString; } }
        public List<LongString> ContentDescription_ { get { return Items.FindAll<LongString>("00700081").ToList(); } }
        public Date PresentationCreationDate { get { return Items.FindFirst<Date>("00700082") as Date; } }
        public List<Date> PresentationCreationDate_ { get { return Items.FindAll<Date>("00700082").ToList(); } }
        public Time PresentationCreationTime { get { return Items.FindFirst<Time>("00700083") as Time; } }
        public List<Time> PresentationCreationTime_ { get { return Items.FindAll<Time>("00700083").ToList(); } }
        public PersonName ContentCreatorName { get { return Items.FindFirst<PersonName>("00700084") as PersonName; } }
        public List<PersonName> ContentCreatorName_ { get { return Items.FindAll<PersonName>("00700084").ToList(); } }
        public SequenceSelector ContentCreatorIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700086")); } }
        public List<SequenceSelector> ContentCreatorIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("00700086").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AlternateContentDescriptionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700087")); } }
        public List<SequenceSelector> AlternateContentDescriptionSequence_ { get { return Items.FindAll<Sequence>("00700087").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PresentationSizeMode { get { return Items.FindFirst<CodeString>("00700100") as CodeString; } }
        public List<CodeString> PresentationSizeMode_ { get { return Items.FindAll<CodeString>("00700100").ToList(); } }
        public DecimalString PresentationPixelSpacing { get { return Items.FindFirst<DecimalString>("00700101") as DecimalString; } }
        public List<DecimalString> PresentationPixelSpacing_ { get { return Items.FindAll<DecimalString>("00700101").ToList(); } }
        public IntegerString PresentationPixelAspectRatio { get { return Items.FindFirst<IntegerString>("00700102") as IntegerString; } }
        public List<IntegerString> PresentationPixelAspectRatio_ { get { return Items.FindAll<IntegerString>("00700102").ToList(); } }
        public FloatingPointSingle PresentationPixelMagnificationRatio { get { return Items.FindFirst<FloatingPointSingle>("00700103") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PresentationPixelMagnificationRatio_ { get { return Items.FindAll<FloatingPointSingle>("00700103").ToList(); } }
        public LongString GraphicGroupLabel { get { return Items.FindFirst<LongString>("00700207") as LongString; } }
        public List<LongString> GraphicGroupLabel_ { get { return Items.FindAll<LongString>("00700207").ToList(); } }
        public ShortText GraphicGroupDescription { get { return Items.FindFirst<ShortText>("00700208") as ShortText; } }
        public List<ShortText> GraphicGroupDescription_ { get { return Items.FindAll<ShortText>("00700208").ToList(); } }
        public SequenceSelector CompoundGraphicSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700209")); } }
        public List<SequenceSelector> CompoundGraphicSequence_ { get { return Items.FindAll<Sequence>("00700209").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedLong CompoundGraphicInstanceID { get { return Items.FindFirst<UnsignedLong>("00700226") as UnsignedLong; } }
        public List<UnsignedLong> CompoundGraphicInstanceID_ { get { return Items.FindAll<UnsignedLong>("00700226").ToList(); } }
        public LongString FontName { get { return Items.FindFirst<LongString>("00700227") as LongString; } }
        public List<LongString> FontName_ { get { return Items.FindAll<LongString>("00700227").ToList(); } }
        public CodeString FontNameType { get { return Items.FindFirst<CodeString>("00700228") as CodeString; } }
        public List<CodeString> FontNameType_ { get { return Items.FindAll<CodeString>("00700228").ToList(); } }
        public LongString CSSFontName { get { return Items.FindFirst<LongString>("00700229") as LongString; } }
        public List<LongString> CSSFontName_ { get { return Items.FindAll<LongString>("00700229").ToList(); } }
        public FloatingPointDouble RotationAngle { get { return Items.FindFirst<FloatingPointDouble>("00700230") as FloatingPointDouble; } }
        public List<FloatingPointDouble> RotationAngle_ { get { return Items.FindAll<FloatingPointDouble>("00700230").ToList(); } }
        public SequenceSelector TextStyleSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700231")); } }
        public List<SequenceSelector> TextStyleSequence_ { get { return Items.FindAll<Sequence>("00700231").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector LineStyleSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700232")); } }
        public List<SequenceSelector> LineStyleSequence_ { get { return Items.FindAll<Sequence>("00700232").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FillStyleSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700233")); } }
        public List<SequenceSelector> FillStyleSequence_ { get { return Items.FindAll<Sequence>("00700233").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector GraphicGroupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700234")); } }
        public List<SequenceSelector> GraphicGroupSequence_ { get { return Items.FindAll<Sequence>("00700234").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort TextColorCIELabValue { get { return Items.FindFirst<UnsignedShort>("00700241") as UnsignedShort; } }
        public List<UnsignedShort> TextColorCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00700241").ToList(); } }
        public CodeString HorizontalAlignment { get { return Items.FindFirst<CodeString>("00700242") as CodeString; } }
        public List<CodeString> HorizontalAlignment_ { get { return Items.FindAll<CodeString>("00700242").ToList(); } }
        public CodeString VerticalAlignment { get { return Items.FindFirst<CodeString>("00700243") as CodeString; } }
        public List<CodeString> VerticalAlignment_ { get { return Items.FindAll<CodeString>("00700243").ToList(); } }
        public CodeString ShadowStyle { get { return Items.FindFirst<CodeString>("00700244") as CodeString; } }
        public List<CodeString> ShadowStyle_ { get { return Items.FindAll<CodeString>("00700244").ToList(); } }
        public FloatingPointSingle ShadowOffsetX { get { return Items.FindFirst<FloatingPointSingle>("00700245") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ShadowOffsetX_ { get { return Items.FindAll<FloatingPointSingle>("00700245").ToList(); } }
        public FloatingPointSingle ShadowOffsetY { get { return Items.FindFirst<FloatingPointSingle>("00700246") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ShadowOffsetY_ { get { return Items.FindAll<FloatingPointSingle>("00700246").ToList(); } }
        public UnsignedShort ShadowColorCIELabValue { get { return Items.FindFirst<UnsignedShort>("00700247") as UnsignedShort; } }
        public List<UnsignedShort> ShadowColorCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00700247").ToList(); } }
        public CodeString Underlined { get { return Items.FindFirst<CodeString>("00700248") as CodeString; } }
        public List<CodeString> Underlined_ { get { return Items.FindAll<CodeString>("00700248").ToList(); } }
        public CodeString Bold { get { return Items.FindFirst<CodeString>("00700249") as CodeString; } }
        public List<CodeString> Bold_ { get { return Items.FindAll<CodeString>("00700249").ToList(); } }
        public CodeString Italic { get { return Items.FindFirst<CodeString>("00700250") as CodeString; } }
        public List<CodeString> Italic_ { get { return Items.FindAll<CodeString>("00700250").ToList(); } }
        public UnsignedShort PatternOnColorCIELabValue { get { return Items.FindFirst<UnsignedShort>("00700251") as UnsignedShort; } }
        public List<UnsignedShort> PatternOnColorCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00700251").ToList(); } }
        public UnsignedShort PatternOffColorCIELabValue { get { return Items.FindFirst<UnsignedShort>("00700252") as UnsignedShort; } }
        public List<UnsignedShort> PatternOffColorCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00700252").ToList(); } }
        public FloatingPointSingle LineThickness { get { return Items.FindFirst<FloatingPointSingle>("00700253") as FloatingPointSingle; } }
        public List<FloatingPointSingle> LineThickness_ { get { return Items.FindAll<FloatingPointSingle>("00700253").ToList(); } }
        public CodeString LineDashingStyle { get { return Items.FindFirst<CodeString>("00700254") as CodeString; } }
        public List<CodeString> LineDashingStyle_ { get { return Items.FindAll<CodeString>("00700254").ToList(); } }
        public UnsignedLong LinePattern { get { return Items.FindFirst<UnsignedLong>("00700255") as UnsignedLong; } }
        public List<UnsignedLong> LinePattern_ { get { return Items.FindAll<UnsignedLong>("00700255").ToList(); } }
        public OtherByteString FillPattern { get { return Items.FindFirst<OtherByteString>("00700256") as OtherByteString; } }
        public List<OtherByteString> FillPattern_ { get { return Items.FindAll<OtherByteString>("00700256").ToList(); } }
        public CodeString FillMode { get { return Items.FindFirst<CodeString>("00700257") as CodeString; } }
        public List<CodeString> FillMode_ { get { return Items.FindAll<CodeString>("00700257").ToList(); } }
        public FloatingPointSingle ShadowOpacity { get { return Items.FindFirst<FloatingPointSingle>("00700258") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ShadowOpacity_ { get { return Items.FindAll<FloatingPointSingle>("00700258").ToList(); } }
        public FloatingPointSingle GapLength { get { return Items.FindFirst<FloatingPointSingle>("00700261") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GapLength_ { get { return Items.FindAll<FloatingPointSingle>("00700261").ToList(); } }
        public FloatingPointSingle DiameterOfVisibility { get { return Items.FindFirst<FloatingPointSingle>("00700262") as FloatingPointSingle; } }
        public List<FloatingPointSingle> DiameterOfVisibility_ { get { return Items.FindAll<FloatingPointSingle>("00700262").ToList(); } }
        public FloatingPointSingle RotationPoint { get { return Items.FindFirst<FloatingPointSingle>("00700273") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RotationPoint_ { get { return Items.FindAll<FloatingPointSingle>("00700273").ToList(); } }
        public CodeString TickAlignment { get { return Items.FindFirst<CodeString>("00700274") as CodeString; } }
        public List<CodeString> TickAlignment_ { get { return Items.FindAll<CodeString>("00700274").ToList(); } }
        public CodeString ShowTickLabel { get { return Items.FindFirst<CodeString>("00700278") as CodeString; } }
        public List<CodeString> ShowTickLabel_ { get { return Items.FindAll<CodeString>("00700278").ToList(); } }
        public CodeString TickLabelAlignment { get { return Items.FindFirst<CodeString>("00700279") as CodeString; } }
        public List<CodeString> TickLabelAlignment_ { get { return Items.FindAll<CodeString>("00700279").ToList(); } }
        public CodeString CompoundGraphicUnits { get { return Items.FindFirst<CodeString>("00700282") as CodeString; } }
        public List<CodeString> CompoundGraphicUnits_ { get { return Items.FindAll<CodeString>("00700282").ToList(); } }
        public FloatingPointSingle PatternOnOpacity { get { return Items.FindFirst<FloatingPointSingle>("00700284") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PatternOnOpacity_ { get { return Items.FindAll<FloatingPointSingle>("00700284").ToList(); } }
        public FloatingPointSingle PatternOffOpacity { get { return Items.FindFirst<FloatingPointSingle>("00700285") as FloatingPointSingle; } }
        public List<FloatingPointSingle> PatternOffOpacity_ { get { return Items.FindAll<FloatingPointSingle>("00700285").ToList(); } }
        public SequenceSelector MajorTicksSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700287")); } }
        public List<SequenceSelector> MajorTicksSequence_ { get { return Items.FindAll<Sequence>("00700287").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle TickPosition { get { return Items.FindFirst<FloatingPointSingle>("00700288") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TickPosition_ { get { return Items.FindAll<FloatingPointSingle>("00700288").ToList(); } }
        public ShortString TickLabel { get { return Items.FindFirst<ShortString>("00700289") as ShortString; } }
        public List<ShortString> TickLabel_ { get { return Items.FindAll<ShortString>("00700289").ToList(); } }
        public CodeString CompoundGraphicType { get { return Items.FindFirst<CodeString>("00700294") as CodeString; } }
        public List<CodeString> CompoundGraphicType_ { get { return Items.FindAll<CodeString>("00700294").ToList(); } }
        public UnsignedLong GraphicGroupID { get { return Items.FindFirst<UnsignedLong>("00700295") as UnsignedLong; } }
        public List<UnsignedLong> GraphicGroupID_ { get { return Items.FindAll<UnsignedLong>("00700295").ToList(); } }
        public CodeString ShapeType { get { return Items.FindFirst<CodeString>("00700306") as CodeString; } }
        public List<CodeString> ShapeType_ { get { return Items.FindAll<CodeString>("00700306").ToList(); } }
        public SequenceSelector RegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700308")); } }
        public List<SequenceSelector> RegistrationSequence_ { get { return Items.FindAll<Sequence>("00700308").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MatrixRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700309")); } }
        public List<SequenceSelector> MatrixRegistrationSequence_ { get { return Items.FindAll<Sequence>("00700309").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector MatrixSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0070030A")); } }
        public List<SequenceSelector> MatrixSequence_ { get { return Items.FindAll<Sequence>("0070030A").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FrameOfReferenceTransformationMatrixType { get { return Items.FindFirst<CodeString>("0070030C") as CodeString; } }
        public List<CodeString> FrameOfReferenceTransformationMatrixType_ { get { return Items.FindAll<CodeString>("0070030C").ToList(); } }
        public SequenceSelector RegistrationTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0070030D")); } }
        public List<SequenceSelector> RegistrationTypeCodeSequence_ { get { return Items.FindAll<Sequence>("0070030D").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText FiducialDescription { get { return Items.FindFirst<ShortText>("0070030F") as ShortText; } }
        public List<ShortText> FiducialDescription_ { get { return Items.FindAll<ShortText>("0070030F").ToList(); } }
        public ShortString FiducialIdentifier { get { return Items.FindFirst<ShortString>("00700310") as ShortString; } }
        public List<ShortString> FiducialIdentifier_ { get { return Items.FindAll<ShortString>("00700310").ToList(); } }
        public SequenceSelector FiducialIdentifierCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700311")); } }
        public List<SequenceSelector> FiducialIdentifierCodeSequence_ { get { return Items.FindAll<Sequence>("00700311").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble ContourUncertaintyRadius { get { return Items.FindFirst<FloatingPointDouble>("00700312") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContourUncertaintyRadius_ { get { return Items.FindAll<FloatingPointDouble>("00700312").ToList(); } }
        public SequenceSelector UsedFiducialsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700314")); } }
        public List<SequenceSelector> UsedFiducialsSequence_ { get { return Items.FindAll<Sequence>("00700314").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector GraphicCoordinatesDataSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700318")); } }
        public List<SequenceSelector> GraphicCoordinatesDataSequence_ { get { return Items.FindAll<Sequence>("00700318").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier FiducialUID { get { return Items.FindFirst<UniqueIdentifier>("0070031A") as UniqueIdentifier; } }
        public List<UniqueIdentifier> FiducialUID_ { get { return Items.FindAll<UniqueIdentifier>("0070031A").ToList(); } }
        public SequenceSelector FiducialSetSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0070031C")); } }
        public List<SequenceSelector> FiducialSetSequence_ { get { return Items.FindAll<Sequence>("0070031C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FiducialSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0070031E")); } }
        public List<SequenceSelector> FiducialSequence_ { get { return Items.FindAll<Sequence>("0070031E").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort GraphicLayerRecommendedDisplayCIELabValue { get { return Items.FindFirst<UnsignedShort>("00700401") as UnsignedShort; } }
        public List<UnsignedShort> GraphicLayerRecommendedDisplayCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00700401").ToList(); } }
        public SequenceSelector BlendingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700402")); } }
        public List<SequenceSelector> BlendingSequence_ { get { return Items.FindAll<Sequence>("00700402").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle RelativeOpacity { get { return Items.FindFirst<FloatingPointSingle>("00700403") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RelativeOpacity_ { get { return Items.FindAll<FloatingPointSingle>("00700403").ToList(); } }
        public SequenceSelector ReferencedSpatialRegistrationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00700404")); } }
        public List<SequenceSelector> ReferencedSpatialRegistrationSequence_ { get { return Items.FindAll<Sequence>("00700404").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BlendingPosition { get { return Items.FindFirst<CodeString>("00700405") as CodeString; } }
        public List<CodeString> BlendingPosition_ { get { return Items.FindAll<CodeString>("00700405").ToList(); } }
        public ShortString HangingProtocolName { get { return Items.FindFirst<ShortString>("00720002") as ShortString; } }
        public List<ShortString> HangingProtocolName_ { get { return Items.FindAll<ShortString>("00720002").ToList(); } }
        public LongString HangingProtocolDescription { get { return Items.FindFirst<LongString>("00720004") as LongString; } }
        public List<LongString> HangingProtocolDescription_ { get { return Items.FindAll<LongString>("00720004").ToList(); } }
        public CodeString HangingProtocolLevel { get { return Items.FindFirst<CodeString>("00720006") as CodeString; } }
        public List<CodeString> HangingProtocolLevel_ { get { return Items.FindAll<CodeString>("00720006").ToList(); } }
        public LongString HangingProtocolCreator { get { return Items.FindFirst<LongString>("00720008") as LongString; } }
        public List<LongString> HangingProtocolCreator_ { get { return Items.FindAll<LongString>("00720008").ToList(); } }
        public EvilDICOM.Core.Element.DateTime HangingProtocolCreationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("0072000A") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> HangingProtocolCreationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("0072000A").ToList(); } }
        public SequenceSelector HangingProtocolDefinitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0072000C")); } }
        public List<SequenceSelector> HangingProtocolDefinitionSequence_ { get { return Items.FindAll<Sequence>("0072000C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector HangingProtocolUserIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0072000E")); } }
        public List<SequenceSelector> HangingProtocolUserIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("0072000E").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString HangingProtocolUserGroupName { get { return Items.FindFirst<LongString>("00720010") as LongString; } }
        public List<LongString> HangingProtocolUserGroupName_ { get { return Items.FindAll<LongString>("00720010").ToList(); } }
        public SequenceSelector SourceHangingProtocolSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720012")); } }
        public List<SequenceSelector> SourceHangingProtocolSequence_ { get { return Items.FindAll<Sequence>("00720012").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfPriorsReferenced { get { return Items.FindFirst<UnsignedShort>("00720014") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfPriorsReferenced_ { get { return Items.FindAll<UnsignedShort>("00720014").ToList(); } }
        public SequenceSelector ImageSetsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720020")); } }
        public List<SequenceSelector> ImageSetsSequence_ { get { return Items.FindAll<Sequence>("00720020").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImageSetSelectorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720022")); } }
        public List<SequenceSelector> ImageSetSelectorSequence_ { get { return Items.FindAll<Sequence>("00720022").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ImageSetSelectorUsageFlag { get { return Items.FindFirst<CodeString>("00720024") as CodeString; } }
        public List<CodeString> ImageSetSelectorUsageFlag_ { get { return Items.FindAll<CodeString>("00720024").ToList(); } }
        public AttributeTag SelectorAttribute { get { return Items.FindFirst<AttributeTag>("00720026") as AttributeTag; } }
        public List<AttributeTag> SelectorAttribute_ { get { return Items.FindAll<AttributeTag>("00720026").ToList(); } }
        public UnsignedShort SelectorValueNumber { get { return Items.FindFirst<UnsignedShort>("00720028") as UnsignedShort; } }
        public List<UnsignedShort> SelectorValueNumber_ { get { return Items.FindAll<UnsignedShort>("00720028").ToList(); } }
        public SequenceSelector TimeBasedImageSetsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720030")); } }
        public List<SequenceSelector> TimeBasedImageSetsSequence_ { get { return Items.FindAll<Sequence>("00720030").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ImageSetNumber { get { return Items.FindFirst<UnsignedShort>("00720032") as UnsignedShort; } }
        public List<UnsignedShort> ImageSetNumber_ { get { return Items.FindAll<UnsignedShort>("00720032").ToList(); } }
        public CodeString ImageSetSelectorCategory { get { return Items.FindFirst<CodeString>("00720034") as CodeString; } }
        public List<CodeString> ImageSetSelectorCategory_ { get { return Items.FindAll<CodeString>("00720034").ToList(); } }
        public UnsignedShort RelativeTime { get { return Items.FindFirst<UnsignedShort>("00720038") as UnsignedShort; } }
        public List<UnsignedShort> RelativeTime_ { get { return Items.FindAll<UnsignedShort>("00720038").ToList(); } }
        public CodeString RelativeTimeUnits { get { return Items.FindFirst<CodeString>("0072003A") as CodeString; } }
        public List<CodeString> RelativeTimeUnits_ { get { return Items.FindAll<CodeString>("0072003A").ToList(); } }
        public SignedShort AbstractPriorValue { get { return Items.FindFirst<SignedShort>("0072003C") as SignedShort; } }
        public List<SignedShort> AbstractPriorValue_ { get { return Items.FindAll<SignedShort>("0072003C").ToList(); } }
        public SequenceSelector AbstractPriorCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0072003E")); } }
        public List<SequenceSelector> AbstractPriorCodeSequence_ { get { return Items.FindAll<Sequence>("0072003E").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ImageSetLabel { get { return Items.FindFirst<LongString>("00720040") as LongString; } }
        public List<LongString> ImageSetLabel_ { get { return Items.FindAll<LongString>("00720040").ToList(); } }
        public CodeString SelectorAttributeVR { get { return Items.FindFirst<CodeString>("00720050") as CodeString; } }
        public List<CodeString> SelectorAttributeVR_ { get { return Items.FindAll<CodeString>("00720050").ToList(); } }
        public AttributeTag SelectorSequencePointer { get { return Items.FindFirst<AttributeTag>("00720052") as AttributeTag; } }
        public List<AttributeTag> SelectorSequencePointer_ { get { return Items.FindAll<AttributeTag>("00720052").ToList(); } }
        public LongString SelectorSequencePointerPrivateCreator { get { return Items.FindFirst<LongString>("00720054") as LongString; } }
        public List<LongString> SelectorSequencePointerPrivateCreator_ { get { return Items.FindAll<LongString>("00720054").ToList(); } }
        public LongString SelectorAttributePrivateCreator { get { return Items.FindFirst<LongString>("00720056") as LongString; } }
        public List<LongString> SelectorAttributePrivateCreator_ { get { return Items.FindAll<LongString>("00720056").ToList(); } }
        public AttributeTag SelectorATValue { get { return Items.FindFirst<AttributeTag>("00720060") as AttributeTag; } }
        public List<AttributeTag> SelectorATValue_ { get { return Items.FindAll<AttributeTag>("00720060").ToList(); } }
        public CodeString SelectorCSValue { get { return Items.FindFirst<CodeString>("00720062") as CodeString; } }
        public List<CodeString> SelectorCSValue_ { get { return Items.FindAll<CodeString>("00720062").ToList(); } }
        public IntegerString SelectorISValue { get { return Items.FindFirst<IntegerString>("00720064") as IntegerString; } }
        public List<IntegerString> SelectorISValue_ { get { return Items.FindAll<IntegerString>("00720064").ToList(); } }
        public LongString SelectorLOValue { get { return Items.FindFirst<LongString>("00720066") as LongString; } }
        public List<LongString> SelectorLOValue_ { get { return Items.FindAll<LongString>("00720066").ToList(); } }
        public LongText SelectorLTValue { get { return Items.FindFirst<LongText>("00720068") as LongText; } }
        public List<LongText> SelectorLTValue_ { get { return Items.FindAll<LongText>("00720068").ToList(); } }
        public PersonName SelectorPNValue { get { return Items.FindFirst<PersonName>("0072006A") as PersonName; } }
        public List<PersonName> SelectorPNValue_ { get { return Items.FindAll<PersonName>("0072006A").ToList(); } }
        public ShortString SelectorSHValue { get { return Items.FindFirst<ShortString>("0072006C") as ShortString; } }
        public List<ShortString> SelectorSHValue_ { get { return Items.FindAll<ShortString>("0072006C").ToList(); } }
        public ShortText SelectorSTValue { get { return Items.FindFirst<ShortText>("0072006E") as ShortText; } }
        public List<ShortText> SelectorSTValue_ { get { return Items.FindAll<ShortText>("0072006E").ToList(); } }
        public UnlimitedText SelectorUTValue { get { return Items.FindFirst<UnlimitedText>("00720070") as UnlimitedText; } }
        public List<UnlimitedText> SelectorUTValue_ { get { return Items.FindAll<UnlimitedText>("00720070").ToList(); } }
        public DecimalString SelectorDSValue { get { return Items.FindFirst<DecimalString>("00720072") as DecimalString; } }
        public List<DecimalString> SelectorDSValue_ { get { return Items.FindAll<DecimalString>("00720072").ToList(); } }
        public FloatingPointDouble SelectorFDValue { get { return Items.FindFirst<FloatingPointDouble>("00720074") as FloatingPointDouble; } }
        public List<FloatingPointDouble> SelectorFDValue_ { get { return Items.FindAll<FloatingPointDouble>("00720074").ToList(); } }
        public FloatingPointSingle SelectorFLValue { get { return Items.FindFirst<FloatingPointSingle>("00720076") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SelectorFLValue_ { get { return Items.FindAll<FloatingPointSingle>("00720076").ToList(); } }
        public UnsignedLong SelectorULValue { get { return Items.FindFirst<UnsignedLong>("00720078") as UnsignedLong; } }
        public List<UnsignedLong> SelectorULValue_ { get { return Items.FindAll<UnsignedLong>("00720078").ToList(); } }
        public UnsignedShort SelectorUSValue { get { return Items.FindFirst<UnsignedShort>("0072007A") as UnsignedShort; } }
        public List<UnsignedShort> SelectorUSValue_ { get { return Items.FindAll<UnsignedShort>("0072007A").ToList(); } }
        public SignedLong SelectorSLValue { get { return Items.FindFirst<SignedLong>("0072007C") as SignedLong; } }
        public List<SignedLong> SelectorSLValue_ { get { return Items.FindAll<SignedLong>("0072007C").ToList(); } }
        public SignedShort SelectorSSValue { get { return Items.FindFirst<SignedShort>("0072007E") as SignedShort; } }
        public List<SignedShort> SelectorSSValue_ { get { return Items.FindAll<SignedShort>("0072007E").ToList(); } }
        public SequenceSelector SelectorCodeSequenceValue { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720080")); } }
        public List<SequenceSelector> SelectorCodeSequenceValue_ { get { return Items.FindAll<Sequence>("00720080").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfScreens { get { return Items.FindFirst<UnsignedShort>("00720100") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfScreens_ { get { return Items.FindAll<UnsignedShort>("00720100").ToList(); } }
        public SequenceSelector NominalScreenDefinitionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720102")); } }
        public List<SequenceSelector> NominalScreenDefinitionSequence_ { get { return Items.FindAll<Sequence>("00720102").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NumberOfVerticalPixels { get { return Items.FindFirst<UnsignedShort>("00720104") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfVerticalPixels_ { get { return Items.FindAll<UnsignedShort>("00720104").ToList(); } }
        public UnsignedShort NumberOfHorizontalPixels { get { return Items.FindFirst<UnsignedShort>("00720106") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfHorizontalPixels_ { get { return Items.FindAll<UnsignedShort>("00720106").ToList(); } }
        public FloatingPointDouble DisplayEnvironmentSpatialPosition { get { return Items.FindFirst<FloatingPointDouble>("00720108") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DisplayEnvironmentSpatialPosition_ { get { return Items.FindAll<FloatingPointDouble>("00720108").ToList(); } }
        public UnsignedShort ScreenMinimumGrayscaleBitDepth { get { return Items.FindFirst<UnsignedShort>("0072010A") as UnsignedShort; } }
        public List<UnsignedShort> ScreenMinimumGrayscaleBitDepth_ { get { return Items.FindAll<UnsignedShort>("0072010A").ToList(); } }
        public UnsignedShort ScreenMinimumColorBitDepth { get { return Items.FindFirst<UnsignedShort>("0072010C") as UnsignedShort; } }
        public List<UnsignedShort> ScreenMinimumColorBitDepth_ { get { return Items.FindAll<UnsignedShort>("0072010C").ToList(); } }
        public UnsignedShort ApplicationMaximumRepaintTime { get { return Items.FindFirst<UnsignedShort>("0072010E") as UnsignedShort; } }
        public List<UnsignedShort> ApplicationMaximumRepaintTime_ { get { return Items.FindAll<UnsignedShort>("0072010E").ToList(); } }
        public SequenceSelector DisplaySetsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720200")); } }
        public List<SequenceSelector> DisplaySetsSequence_ { get { return Items.FindAll<Sequence>("00720200").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort DisplaySetNumber { get { return Items.FindFirst<UnsignedShort>("00720202") as UnsignedShort; } }
        public List<UnsignedShort> DisplaySetNumber_ { get { return Items.FindAll<UnsignedShort>("00720202").ToList(); } }
        public LongString DisplaySetLabel { get { return Items.FindFirst<LongString>("00720203") as LongString; } }
        public List<LongString> DisplaySetLabel_ { get { return Items.FindAll<LongString>("00720203").ToList(); } }
        public UnsignedShort DisplaySetPresentationGroup { get { return Items.FindFirst<UnsignedShort>("00720204") as UnsignedShort; } }
        public List<UnsignedShort> DisplaySetPresentationGroup_ { get { return Items.FindAll<UnsignedShort>("00720204").ToList(); } }
        public LongString DisplaySetPresentationGroupDescription { get { return Items.FindFirst<LongString>("00720206") as LongString; } }
        public List<LongString> DisplaySetPresentationGroupDescription_ { get { return Items.FindAll<LongString>("00720206").ToList(); } }
        public CodeString PartialDataDisplayHandling { get { return Items.FindFirst<CodeString>("00720208") as CodeString; } }
        public List<CodeString> PartialDataDisplayHandling_ { get { return Items.FindAll<CodeString>("00720208").ToList(); } }
        public SequenceSelector SynchronizedScrollingSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720210")); } }
        public List<SequenceSelector> SynchronizedScrollingSequence_ { get { return Items.FindAll<Sequence>("00720210").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort DisplaySetScrollingGroup { get { return Items.FindFirst<UnsignedShort>("00720212") as UnsignedShort; } }
        public List<UnsignedShort> DisplaySetScrollingGroup_ { get { return Items.FindAll<UnsignedShort>("00720212").ToList(); } }
        public SequenceSelector NavigationIndicatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720214")); } }
        public List<SequenceSelector> NavigationIndicatorSequence_ { get { return Items.FindAll<Sequence>("00720214").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort NavigationDisplaySet { get { return Items.FindFirst<UnsignedShort>("00720216") as UnsignedShort; } }
        public List<UnsignedShort> NavigationDisplaySet_ { get { return Items.FindAll<UnsignedShort>("00720216").ToList(); } }
        public UnsignedShort ReferenceDisplaySets { get { return Items.FindFirst<UnsignedShort>("00720218") as UnsignedShort; } }
        public List<UnsignedShort> ReferenceDisplaySets_ { get { return Items.FindAll<UnsignedShort>("00720218").ToList(); } }
        public SequenceSelector ImageBoxesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720300")); } }
        public List<SequenceSelector> ImageBoxesSequence_ { get { return Items.FindAll<Sequence>("00720300").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ImageBoxNumber { get { return Items.FindFirst<UnsignedShort>("00720302") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxNumber_ { get { return Items.FindAll<UnsignedShort>("00720302").ToList(); } }
        public CodeString ImageBoxLayoutType { get { return Items.FindFirst<CodeString>("00720304") as CodeString; } }
        public List<CodeString> ImageBoxLayoutType_ { get { return Items.FindAll<CodeString>("00720304").ToList(); } }
        public UnsignedShort ImageBoxTileHorizontalDimension { get { return Items.FindFirst<UnsignedShort>("00720306") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxTileHorizontalDimension_ { get { return Items.FindAll<UnsignedShort>("00720306").ToList(); } }
        public UnsignedShort ImageBoxTileVerticalDimension { get { return Items.FindFirst<UnsignedShort>("00720308") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxTileVerticalDimension_ { get { return Items.FindAll<UnsignedShort>("00720308").ToList(); } }
        public CodeString ImageBoxScrollDirection { get { return Items.FindFirst<CodeString>("00720310") as CodeString; } }
        public List<CodeString> ImageBoxScrollDirection_ { get { return Items.FindAll<CodeString>("00720310").ToList(); } }
        public CodeString ImageBoxSmallScrollType { get { return Items.FindFirst<CodeString>("00720312") as CodeString; } }
        public List<CodeString> ImageBoxSmallScrollType_ { get { return Items.FindAll<CodeString>("00720312").ToList(); } }
        public UnsignedShort ImageBoxSmallScrollAmount { get { return Items.FindFirst<UnsignedShort>("00720314") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxSmallScrollAmount_ { get { return Items.FindAll<UnsignedShort>("00720314").ToList(); } }
        public CodeString ImageBoxLargeScrollType { get { return Items.FindFirst<CodeString>("00720316") as CodeString; } }
        public List<CodeString> ImageBoxLargeScrollType_ { get { return Items.FindAll<CodeString>("00720316").ToList(); } }
        public UnsignedShort ImageBoxLargeScrollAmount { get { return Items.FindFirst<UnsignedShort>("00720318") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxLargeScrollAmount_ { get { return Items.FindAll<UnsignedShort>("00720318").ToList(); } }
        public UnsignedShort ImageBoxOverlapPriority { get { return Items.FindFirst<UnsignedShort>("00720320") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxOverlapPriority_ { get { return Items.FindAll<UnsignedShort>("00720320").ToList(); } }
        public FloatingPointDouble CineRelativeToRealTime { get { return Items.FindFirst<FloatingPointDouble>("00720330") as FloatingPointDouble; } }
        public List<FloatingPointDouble> CineRelativeToRealTime_ { get { return Items.FindAll<FloatingPointDouble>("00720330").ToList(); } }
        public SequenceSelector FilterOperationsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720400")); } }
        public List<SequenceSelector> FilterOperationsSequence_ { get { return Items.FindAll<Sequence>("00720400").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FilterByCategory { get { return Items.FindFirst<CodeString>("00720402") as CodeString; } }
        public List<CodeString> FilterByCategory_ { get { return Items.FindAll<CodeString>("00720402").ToList(); } }
        public CodeString FilterByAttributePresence { get { return Items.FindFirst<CodeString>("00720404") as CodeString; } }
        public List<CodeString> FilterByAttributePresence_ { get { return Items.FindAll<CodeString>("00720404").ToList(); } }
        public CodeString FilterByOperator { get { return Items.FindFirst<CodeString>("00720406") as CodeString; } }
        public List<CodeString> FilterByOperator_ { get { return Items.FindAll<CodeString>("00720406").ToList(); } }
        public UnsignedShort StructuredDisplayBackgroundCIELabValue { get { return Items.FindFirst<UnsignedShort>("00720420") as UnsignedShort; } }
        public List<UnsignedShort> StructuredDisplayBackgroundCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00720420").ToList(); } }
        public UnsignedShort EmptyImageBoxCIELabValue { get { return Items.FindFirst<UnsignedShort>("00720421") as UnsignedShort; } }
        public List<UnsignedShort> EmptyImageBoxCIELabValue_ { get { return Items.FindAll<UnsignedShort>("00720421").ToList(); } }
        public SequenceSelector StructuredDisplayImageBoxSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720422")); } }
        public List<SequenceSelector> StructuredDisplayImageBoxSequence_ { get { return Items.FindAll<Sequence>("00720422").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector StructuredDisplayTextBoxSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720424")); } }
        public List<SequenceSelector> StructuredDisplayTextBoxSequence_ { get { return Items.FindAll<Sequence>("00720424").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedFirstFrameSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720427")); } }
        public List<SequenceSelector> ReferencedFirstFrameSequence_ { get { return Items.FindAll<Sequence>("00720427").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImageBoxSynchronizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720430")); } }
        public List<SequenceSelector> ImageBoxSynchronizationSequence_ { get { return Items.FindAll<Sequence>("00720430").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort SynchronizedImageBoxList { get { return Items.FindFirst<UnsignedShort>("00720432") as UnsignedShort; } }
        public List<UnsignedShort> SynchronizedImageBoxList_ { get { return Items.FindAll<UnsignedShort>("00720432").ToList(); } }
        public CodeString TypeOfSynchronization { get { return Items.FindFirst<CodeString>("00720434") as CodeString; } }
        public List<CodeString> TypeOfSynchronization_ { get { return Items.FindAll<CodeString>("00720434").ToList(); } }
        public CodeString BlendingOperationType { get { return Items.FindFirst<CodeString>("00720500") as CodeString; } }
        public List<CodeString> BlendingOperationType_ { get { return Items.FindAll<CodeString>("00720500").ToList(); } }
        public CodeString ReformattingOperationType { get { return Items.FindFirst<CodeString>("00720510") as CodeString; } }
        public List<CodeString> ReformattingOperationType_ { get { return Items.FindAll<CodeString>("00720510").ToList(); } }
        public FloatingPointDouble ReformattingThickness { get { return Items.FindFirst<FloatingPointDouble>("00720512") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReformattingThickness_ { get { return Items.FindAll<FloatingPointDouble>("00720512").ToList(); } }
        public FloatingPointDouble ReformattingInterval { get { return Items.FindFirst<FloatingPointDouble>("00720514") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ReformattingInterval_ { get { return Items.FindAll<FloatingPointDouble>("00720514").ToList(); } }
        public CodeString ReformattingOperationInitialViewDirection { get { return Items.FindFirst<CodeString>("00720516") as CodeString; } }
        public List<CodeString> ReformattingOperationInitialViewDirection_ { get { return Items.FindAll<CodeString>("00720516").ToList(); } }
        public CodeString ThreeDRenderingType { get { return Items.FindFirst<CodeString>("00720520") as CodeString; } }
        public List<CodeString> ThreeDRenderingType_ { get { return Items.FindAll<CodeString>("00720520").ToList(); } }
        public SequenceSelector SortingOperationsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720600")); } }
        public List<SequenceSelector> SortingOperationsSequence_ { get { return Items.FindAll<Sequence>("00720600").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SortByCategory { get { return Items.FindFirst<CodeString>("00720602") as CodeString; } }
        public List<CodeString> SortByCategory_ { get { return Items.FindAll<CodeString>("00720602").ToList(); } }
        public CodeString SortingDirection { get { return Items.FindFirst<CodeString>("00720604") as CodeString; } }
        public List<CodeString> SortingDirection_ { get { return Items.FindAll<CodeString>("00720604").ToList(); } }
        public CodeString DisplaySetPatientOrientation { get { return Items.FindFirst<CodeString>("00720700") as CodeString; } }
        public List<CodeString> DisplaySetPatientOrientation_ { get { return Items.FindAll<CodeString>("00720700").ToList(); } }
        public CodeString VOIType { get { return Items.FindFirst<CodeString>("00720702") as CodeString; } }
        public List<CodeString> VOIType_ { get { return Items.FindAll<CodeString>("00720702").ToList(); } }
        public CodeString PseudoColorType { get { return Items.FindFirst<CodeString>("00720704") as CodeString; } }
        public List<CodeString> PseudoColorType_ { get { return Items.FindAll<CodeString>("00720704").ToList(); } }
        public SequenceSelector PseudoColorPaletteInstanceReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00720705")); } }
        public List<SequenceSelector> PseudoColorPaletteInstanceReferenceSequence_ { get { return Items.FindAll<Sequence>("00720705").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ShowGrayscaleInverted { get { return Items.FindFirst<CodeString>("00720706") as CodeString; } }
        public List<CodeString> ShowGrayscaleInverted_ { get { return Items.FindAll<CodeString>("00720706").ToList(); } }
        public CodeString ShowImageTrueSizeFlag { get { return Items.FindFirst<CodeString>("00720710") as CodeString; } }
        public List<CodeString> ShowImageTrueSizeFlag_ { get { return Items.FindAll<CodeString>("00720710").ToList(); } }
        public CodeString ShowGraphicAnnotationFlag { get { return Items.FindFirst<CodeString>("00720712") as CodeString; } }
        public List<CodeString> ShowGraphicAnnotationFlag_ { get { return Items.FindAll<CodeString>("00720712").ToList(); } }
        public CodeString ShowPatientDemographicsFlag { get { return Items.FindFirst<CodeString>("00720714") as CodeString; } }
        public List<CodeString> ShowPatientDemographicsFlag_ { get { return Items.FindAll<CodeString>("00720714").ToList(); } }
        public CodeString ShowAcquisitionTechniquesFlag { get { return Items.FindFirst<CodeString>("00720716") as CodeString; } }
        public List<CodeString> ShowAcquisitionTechniquesFlag_ { get { return Items.FindAll<CodeString>("00720716").ToList(); } }
        public CodeString DisplaySetHorizontalJustification { get { return Items.FindFirst<CodeString>("00720717") as CodeString; } }
        public List<CodeString> DisplaySetHorizontalJustification_ { get { return Items.FindAll<CodeString>("00720717").ToList(); } }
        public CodeString DisplaySetVerticalJustification { get { return Items.FindFirst<CodeString>("00720718") as CodeString; } }
        public List<CodeString> DisplaySetVerticalJustification_ { get { return Items.FindAll<CodeString>("00720718").ToList(); } }
        public FloatingPointDouble ContinuationStartMeterset { get { return Items.FindFirst<FloatingPointDouble>("00740120") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContinuationStartMeterset_ { get { return Items.FindAll<FloatingPointDouble>("00740120").ToList(); } }
        public FloatingPointDouble ContinuationEndMeterset { get { return Items.FindFirst<FloatingPointDouble>("00740121") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ContinuationEndMeterset_ { get { return Items.FindAll<FloatingPointDouble>("00740121").ToList(); } }
        public CodeString ProcedureStepState { get { return Items.FindFirst<CodeString>("00741000") as CodeString; } }
        public List<CodeString> ProcedureStepState_ { get { return Items.FindAll<CodeString>("00741000").ToList(); } }
        public SequenceSelector ProcedureStepProgressInformationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741002")); } }
        public List<SequenceSelector> ProcedureStepProgressInformationSequence_ { get { return Items.FindAll<Sequence>("00741002").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString ProcedureStepProgress { get { return Items.FindFirst<DecimalString>("00741004") as DecimalString; } }
        public List<DecimalString> ProcedureStepProgress_ { get { return Items.FindAll<DecimalString>("00741004").ToList(); } }
        public ShortText ProcedureStepProgressDescription { get { return Items.FindFirst<ShortText>("00741006") as ShortText; } }
        public List<ShortText> ProcedureStepProgressDescription_ { get { return Items.FindAll<ShortText>("00741006").ToList(); } }
        public SequenceSelector ProcedureStepCommunicationsURISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741008")); } }
        public List<SequenceSelector> ProcedureStepCommunicationsURISequence_ { get { return Items.FindAll<Sequence>("00741008").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText ContactURI { get { return Items.FindFirst<ShortText>("0074100a") as ShortText; } }
        public List<ShortText> ContactURI_ { get { return Items.FindAll<ShortText>("0074100a").ToList(); } }
        public LongString ContactDisplayName { get { return Items.FindFirst<LongString>("0074100c") as LongString; } }
        public List<LongString> ContactDisplayName_ { get { return Items.FindAll<LongString>("0074100c").ToList(); } }
        public SequenceSelector ProcedureStepDiscontinuationReasonCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0074100e")); } }
        public List<SequenceSelector> ProcedureStepDiscontinuationReasonCodeSequence_ { get { return Items.FindAll<Sequence>("0074100e").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BeamTaskSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741020")); } }
        public List<SequenceSelector> BeamTaskSequence_ { get { return Items.FindAll<Sequence>("00741020").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString BeamTaskType { get { return Items.FindFirst<CodeString>("00741022") as CodeString; } }
        public List<CodeString> BeamTaskType_ { get { return Items.FindAll<CodeString>("00741022").ToList(); } }
        public IntegerString BeamOrderIndexTrialRetired { get { return Items.FindFirst<IntegerString>("00741024") as IntegerString; } }
        public List<IntegerString> BeamOrderIndexTrialRetired_ { get { return Items.FindAll<IntegerString>("00741024").ToList(); } }
        public FloatingPointDouble TableTopVerticalAdjustedPosition { get { return Items.FindFirst<FloatingPointDouble>("00741026") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopVerticalAdjustedPosition_ { get { return Items.FindAll<FloatingPointDouble>("00741026").ToList(); } }
        public FloatingPointDouble TableTopLongitudinalAdjustedPosition { get { return Items.FindFirst<FloatingPointDouble>("00741027") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopLongitudinalAdjustedPosition_ { get { return Items.FindAll<FloatingPointDouble>("00741027").ToList(); } }
        public FloatingPointDouble TableTopLateralAdjustedPosition { get { return Items.FindFirst<FloatingPointDouble>("00741028") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopLateralAdjustedPosition_ { get { return Items.FindAll<FloatingPointDouble>("00741028").ToList(); } }
        public FloatingPointDouble PatientSupportAdjustedAngle { get { return Items.FindFirst<FloatingPointDouble>("0074102A") as FloatingPointDouble; } }
        public List<FloatingPointDouble> PatientSupportAdjustedAngle_ { get { return Items.FindAll<FloatingPointDouble>("0074102A").ToList(); } }
        public FloatingPointDouble TableTopEccentricAdjustedAngle { get { return Items.FindFirst<FloatingPointDouble>("0074102B") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopEccentricAdjustedAngle_ { get { return Items.FindAll<FloatingPointDouble>("0074102B").ToList(); } }
        public FloatingPointDouble TableTopPitchAdjustedAngle { get { return Items.FindFirst<FloatingPointDouble>("0074102C") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopPitchAdjustedAngle_ { get { return Items.FindAll<FloatingPointDouble>("0074102C").ToList(); } }
        public FloatingPointDouble TableTopRollAdjustedAngle { get { return Items.FindFirst<FloatingPointDouble>("0074102D") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TableTopRollAdjustedAngle_ { get { return Items.FindAll<FloatingPointDouble>("0074102D").ToList(); } }
        public SequenceSelector DeliveryVerificationImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741030")); } }
        public List<SequenceSelector> DeliveryVerificationImageSequence_ { get { return Items.FindAll<Sequence>("00741030").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString VerificationImageTiming { get { return Items.FindFirst<CodeString>("00741032") as CodeString; } }
        public List<CodeString> VerificationImageTiming_ { get { return Items.FindAll<CodeString>("00741032").ToList(); } }
        public CodeString DoubleExposureFlag { get { return Items.FindFirst<CodeString>("00741034") as CodeString; } }
        public List<CodeString> DoubleExposureFlag_ { get { return Items.FindAll<CodeString>("00741034").ToList(); } }
        public CodeString DoubleExposureOrdering { get { return Items.FindFirst<CodeString>("00741036") as CodeString; } }
        public List<CodeString> DoubleExposureOrdering_ { get { return Items.FindAll<CodeString>("00741036").ToList(); } }
        public DecimalString DoubleExposureMetersetTrialRetired { get { return Items.FindFirst<DecimalString>("00741038") as DecimalString; } }
        public List<DecimalString> DoubleExposureMetersetTrialRetired_ { get { return Items.FindAll<DecimalString>("00741038").ToList(); } }
        public DecimalString DoubleExposureFieldDeltaTrialRetired { get { return Items.FindFirst<DecimalString>("0074103A") as DecimalString; } }
        public List<DecimalString> DoubleExposureFieldDeltaTrialRetired_ { get { return Items.FindAll<DecimalString>("0074103A").ToList(); } }
        public SequenceSelector RelatedReferenceRTImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741040")); } }
        public List<SequenceSelector> RelatedReferenceRTImageSequence_ { get { return Items.FindAll<Sequence>("00741040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector GeneralMachineVerificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741042")); } }
        public List<SequenceSelector> GeneralMachineVerificationSequence_ { get { return Items.FindAll<Sequence>("00741042").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ConventionalMachineVerificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741044")); } }
        public List<SequenceSelector> ConventionalMachineVerificationSequence_ { get { return Items.FindAll<Sequence>("00741044").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonMachineVerificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741046")); } }
        public List<SequenceSelector> IonMachineVerificationSequence_ { get { return Items.FindAll<Sequence>("00741046").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FailedAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741048")); } }
        public List<SequenceSelector> FailedAttributesSequence_ { get { return Items.FindAll<Sequence>("00741048").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OverriddenAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0074104A")); } }
        public List<SequenceSelector> OverriddenAttributesSequence_ { get { return Items.FindAll<Sequence>("0074104A").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ConventionalControlPointVerificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0074104C")); } }
        public List<SequenceSelector> ConventionalControlPointVerificationSequence_ { get { return Items.FindAll<Sequence>("0074104C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonControlPointVerificationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0074104E")); } }
        public List<SequenceSelector> IonControlPointVerificationSequence_ { get { return Items.FindAll<Sequence>("0074104E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AttributeOccurrenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741050")); } }
        public List<SequenceSelector> AttributeOccurrenceSequence_ { get { return Items.FindAll<Sequence>("00741050").Select(s => new SequenceSelector(s)).ToList(); } }
        public AttributeTag AttributeOccurrencePointer { get { return Items.FindFirst<AttributeTag>("00741052") as AttributeTag; } }
        public List<AttributeTag> AttributeOccurrencePointer_ { get { return Items.FindAll<AttributeTag>("00741052").ToList(); } }
        public UnsignedLong AttributeItemSelector { get { return Items.FindFirst<UnsignedLong>("00741054") as UnsignedLong; } }
        public List<UnsignedLong> AttributeItemSelector_ { get { return Items.FindAll<UnsignedLong>("00741054").ToList(); } }
        public LongString AttributeOccurrencePrivateCreator { get { return Items.FindFirst<LongString>("00741056") as LongString; } }
        public List<LongString> AttributeOccurrencePrivateCreator_ { get { return Items.FindAll<LongString>("00741056").ToList(); } }
        public IntegerString SelectorSequencePointerItems { get { return Items.FindFirst<IntegerString>("00741057") as IntegerString; } }
        public List<IntegerString> SelectorSequencePointerItems_ { get { return Items.FindAll<IntegerString>("00741057").ToList(); } }
        public CodeString ScheduledProcedureStepPriority { get { return Items.FindFirst<CodeString>("00741200") as CodeString; } }
        public List<CodeString> ScheduledProcedureStepPriority_ { get { return Items.FindAll<CodeString>("00741200").ToList(); } }
        public LongString WorklistLabel { get { return Items.FindFirst<LongString>("00741202") as LongString; } }
        public List<LongString> WorklistLabel_ { get { return Items.FindAll<LongString>("00741202").ToList(); } }
        public LongString ProcedureStepLabel { get { return Items.FindFirst<LongString>("00741204") as LongString; } }
        public List<LongString> ProcedureStepLabel_ { get { return Items.FindAll<LongString>("00741204").ToList(); } }
        public SequenceSelector ScheduledProcessingParametersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741210")); } }
        public List<SequenceSelector> ScheduledProcessingParametersSequence_ { get { return Items.FindAll<Sequence>("00741210").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerformedProcessingParametersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741212")); } }
        public List<SequenceSelector> PerformedProcessingParametersSequence_ { get { return Items.FindAll<Sequence>("00741212").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector UnifiedProcedureStepPerformedProcedureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741216")); } }
        public List<SequenceSelector> UnifiedProcedureStepPerformedProcedureSequence_ { get { return Items.FindAll<Sequence>("00741216").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RelatedProcedureStepSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741220")); } }
        public List<SequenceSelector> RelatedProcedureStepSequenceRetired_ { get { return Items.FindAll<Sequence>("00741220").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ProcedureStepRelationshipTypeRetired { get { return Items.FindFirst<LongString>("00741222") as LongString; } }
        public List<LongString> ProcedureStepRelationshipTypeRetired_ { get { return Items.FindAll<LongString>("00741222").ToList(); } }
        public SequenceSelector ReplacedProcedureStepSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00741224")); } }
        public List<SequenceSelector> ReplacedProcedureStepSequence_ { get { return Items.FindAll<Sequence>("00741224").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString DeletionLock { get { return Items.FindFirst<LongString>("00741230") as LongString; } }
        public List<LongString> DeletionLock_ { get { return Items.FindAll<LongString>("00741230").ToList(); } }
        public ApplicationEntity ReceivingAE { get { return Items.FindFirst<ApplicationEntity>("00741234") as ApplicationEntity; } }
        public List<ApplicationEntity> ReceivingAE_ { get { return Items.FindAll<ApplicationEntity>("00741234").ToList(); } }
        public ApplicationEntity RequestingAE { get { return Items.FindFirst<ApplicationEntity>("00741236") as ApplicationEntity; } }
        public List<ApplicationEntity> RequestingAE_ { get { return Items.FindAll<ApplicationEntity>("00741236").ToList(); } }
        public LongText ReasonForCancellation { get { return Items.FindFirst<LongText>("00741238") as LongText; } }
        public List<LongText> ReasonForCancellation_ { get { return Items.FindAll<LongText>("00741238").ToList(); } }
        public CodeString SCPStatus { get { return Items.FindFirst<CodeString>("00741242") as CodeString; } }
        public List<CodeString> SCPStatus_ { get { return Items.FindAll<CodeString>("00741242").ToList(); } }
        public CodeString SubscriptionListStatus { get { return Items.FindFirst<CodeString>("00741244") as CodeString; } }
        public List<CodeString> SubscriptionListStatus_ { get { return Items.FindAll<CodeString>("00741244").ToList(); } }
        public CodeString UnifiedProcedureStepListStatus { get { return Items.FindFirst<CodeString>("00741246") as CodeString; } }
        public List<CodeString> UnifiedProcedureStepListStatus_ { get { return Items.FindAll<CodeString>("00741246").ToList(); } }
        public UnsignedLong BeamOrderIndex { get { return Items.FindFirst<UnsignedLong>("00741324") as UnsignedLong; } }
        public List<UnsignedLong> BeamOrderIndex_ { get { return Items.FindAll<UnsignedLong>("00741324").ToList(); } }
        public FloatingPointDouble DoubleExposureMeterset { get { return Items.FindFirst<FloatingPointDouble>("00741338") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DoubleExposureMeterset_ { get { return Items.FindAll<FloatingPointDouble>("00741338").ToList(); } }
        public FloatingPointDouble DoubleExposureFieldDelta { get { return Items.FindFirst<FloatingPointDouble>("0074133A") as FloatingPointDouble; } }
        public List<FloatingPointDouble> DoubleExposureFieldDelta_ { get { return Items.FindAll<FloatingPointDouble>("0074133A").ToList(); } }
        public LongString ImplantAssemblyTemplateName { get { return Items.FindFirst<LongString>("00760001") as LongString; } }
        public List<LongString> ImplantAssemblyTemplateName_ { get { return Items.FindAll<LongString>("00760001").ToList(); } }
        public LongString ImplantAssemblyTemplateIssuer { get { return Items.FindFirst<LongString>("00760003") as LongString; } }
        public List<LongString> ImplantAssemblyTemplateIssuer_ { get { return Items.FindAll<LongString>("00760003").ToList(); } }
        public LongString ImplantAssemblyTemplateVersion { get { return Items.FindFirst<LongString>("00760006") as LongString; } }
        public List<LongString> ImplantAssemblyTemplateVersion_ { get { return Items.FindAll<LongString>("00760006").ToList(); } }
        public SequenceSelector ReplacedImplantAssemblyTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760008")); } }
        public List<SequenceSelector> ReplacedImplantAssemblyTemplateSequence_ { get { return Items.FindAll<Sequence>("00760008").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ImplantAssemblyTemplateType { get { return Items.FindFirst<CodeString>("0076000A") as CodeString; } }
        public List<CodeString> ImplantAssemblyTemplateType_ { get { return Items.FindAll<CodeString>("0076000A").ToList(); } }
        public SequenceSelector OriginalImplantAssemblyTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0076000C")); } }
        public List<SequenceSelector> OriginalImplantAssemblyTemplateSequence_ { get { return Items.FindAll<Sequence>("0076000C").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector DerivationImplantAssemblyTemplateSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0076000E")); } }
        public List<SequenceSelector> DerivationImplantAssemblyTemplateSequence_ { get { return Items.FindAll<Sequence>("0076000E").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImplantAssemblyTemplateTargetAnatomySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760010")); } }
        public List<SequenceSelector> ImplantAssemblyTemplateTargetAnatomySequence_ { get { return Items.FindAll<Sequence>("00760010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ProcedureTypeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760020")); } }
        public List<SequenceSelector> ProcedureTypeCodeSequence_ { get { return Items.FindAll<Sequence>("00760020").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SurgicalTechnique { get { return Items.FindFirst<LongString>("00760030") as LongString; } }
        public List<LongString> SurgicalTechnique_ { get { return Items.FindAll<LongString>("00760030").ToList(); } }
        public SequenceSelector ComponentTypesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760032")); } }
        public List<SequenceSelector> ComponentTypesSequence_ { get { return Items.FindAll<Sequence>("00760032").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ComponentTypeCodeSequence { get { return Items.FindFirst<CodeString>("00760034") as CodeString; } }
        public List<CodeString> ComponentTypeCodeSequence_ { get { return Items.FindAll<CodeString>("00760034").ToList(); } }
        public CodeString ExclusiveComponentType { get { return Items.FindFirst<CodeString>("00760036") as CodeString; } }
        public List<CodeString> ExclusiveComponentType_ { get { return Items.FindAll<CodeString>("00760036").ToList(); } }
        public CodeString MandatoryComponentType { get { return Items.FindFirst<CodeString>("00760038") as CodeString; } }
        public List<CodeString> MandatoryComponentType_ { get { return Items.FindAll<CodeString>("00760038").ToList(); } }
        public SequenceSelector ComponentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760040")); } }
        public List<SequenceSelector> ComponentSequence_ { get { return Items.FindAll<Sequence>("00760040").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ComponentID { get { return Items.FindFirst<UnsignedShort>("00760055") as UnsignedShort; } }
        public List<UnsignedShort> ComponentID_ { get { return Items.FindAll<UnsignedShort>("00760055").ToList(); } }
        public SequenceSelector ComponentAssemblySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00760060")); } }
        public List<SequenceSelector> ComponentAssemblySequence_ { get { return Items.FindAll<Sequence>("00760060").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort Component1ReferencedID { get { return Items.FindFirst<UnsignedShort>("00760070") as UnsignedShort; } }
        public List<UnsignedShort> Component1ReferencedID_ { get { return Items.FindAll<UnsignedShort>("00760070").ToList(); } }
        public UnsignedShort Component1ReferencedMatingFeatureSetID { get { return Items.FindFirst<UnsignedShort>("00760080") as UnsignedShort; } }
        public List<UnsignedShort> Component1ReferencedMatingFeatureSetID_ { get { return Items.FindAll<UnsignedShort>("00760080").ToList(); } }
        public UnsignedShort Component1ReferencedMatingFeatureID { get { return Items.FindFirst<UnsignedShort>("00760090") as UnsignedShort; } }
        public List<UnsignedShort> Component1ReferencedMatingFeatureID_ { get { return Items.FindAll<UnsignedShort>("00760090").ToList(); } }
        public UnsignedShort Component2ReferencedID { get { return Items.FindFirst<UnsignedShort>("007600A0") as UnsignedShort; } }
        public List<UnsignedShort> Component2ReferencedID_ { get { return Items.FindAll<UnsignedShort>("007600A0").ToList(); } }
        public UnsignedShort Component2ReferencedMatingFeatureSetID { get { return Items.FindFirst<UnsignedShort>("007600B0") as UnsignedShort; } }
        public List<UnsignedShort> Component2ReferencedMatingFeatureSetID_ { get { return Items.FindAll<UnsignedShort>("007600B0").ToList(); } }
        public UnsignedShort Component2ReferencedMatingFeatureID { get { return Items.FindFirst<UnsignedShort>("007600C0") as UnsignedShort; } }
        public List<UnsignedShort> Component2ReferencedMatingFeatureID_ { get { return Items.FindAll<UnsignedShort>("007600C0").ToList(); } }
        public LongString ImplantTemplateGroupName { get { return Items.FindFirst<LongString>("00780001") as LongString; } }
        public List<LongString> ImplantTemplateGroupName_ { get { return Items.FindAll<LongString>("00780001").ToList(); } }
        public ShortText ImplantTemplateGroupDescription { get { return Items.FindFirst<ShortText>("00780010") as ShortText; } }
        public List<ShortText> ImplantTemplateGroupDescription_ { get { return Items.FindAll<ShortText>("00780010").ToList(); } }
        public LongString ImplantTemplateGroupIssuer { get { return Items.FindFirst<LongString>("00780020") as LongString; } }
        public List<LongString> ImplantTemplateGroupIssuer_ { get { return Items.FindAll<LongString>("00780020").ToList(); } }
        public LongString ImplantTemplateGroupVersion { get { return Items.FindFirst<LongString>("00780024") as LongString; } }
        public List<LongString> ImplantTemplateGroupVersion_ { get { return Items.FindAll<LongString>("00780024").ToList(); } }
        public SequenceSelector ReplacedImplantTemplateGroupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00780026")); } }
        public List<SequenceSelector> ReplacedImplantTemplateGroupSequence_ { get { return Items.FindAll<Sequence>("00780026").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImplantTemplateGroupTargetAnatomySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00780028")); } }
        public List<SequenceSelector> ImplantTemplateGroupTargetAnatomySequence_ { get { return Items.FindAll<Sequence>("00780028").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImplantTemplateGroupMembersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("0078002A")); } }
        public List<SequenceSelector> ImplantTemplateGroupMembersSequence_ { get { return Items.FindAll<Sequence>("0078002A").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ImplantTemplateGroupMemberID { get { return Items.FindFirst<UnsignedShort>("0078002E") as UnsignedShort; } }
        public List<UnsignedShort> ImplantTemplateGroupMemberID_ { get { return Items.FindAll<UnsignedShort>("0078002E").ToList(); } }
        public FloatingPointDouble ThreeDImplantTemplateGroupMemberMatchingPoint { get { return Items.FindFirst<FloatingPointDouble>("00780050") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDImplantTemplateGroupMemberMatchingPoint_ { get { return Items.FindAll<FloatingPointDouble>("00780050").ToList(); } }
        public FloatingPointDouble ThreeDImplantTemplateGroupMemberMatchingAxes { get { return Items.FindFirst<FloatingPointDouble>("00780060") as FloatingPointDouble; } }
        public List<FloatingPointDouble> ThreeDImplantTemplateGroupMemberMatchingAxes_ { get { return Items.FindAll<FloatingPointDouble>("00780060").ToList(); } }
        public SequenceSelector ImplantTemplateGroupMemberMatching2DCoordinatesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00780070")); } }
        public List<SequenceSelector> ImplantTemplateGroupMemberMatching2DCoordinatesSequence_ { get { return Items.FindAll<Sequence>("00780070").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointDouble TwoDImplantTemplateGroupMemberMatchingPoint { get { return Items.FindFirst<FloatingPointDouble>("00780090") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDImplantTemplateGroupMemberMatchingPoint_ { get { return Items.FindAll<FloatingPointDouble>("00780090").ToList(); } }
        public FloatingPointDouble TwoDImplantTemplateGroupMemberMatchingAxes { get { return Items.FindFirst<FloatingPointDouble>("007800A0") as FloatingPointDouble; } }
        public List<FloatingPointDouble> TwoDImplantTemplateGroupMemberMatchingAxes_ { get { return Items.FindAll<FloatingPointDouble>("007800A0").ToList(); } }
        public SequenceSelector ImplantTemplateGroupVariationDimensionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("007800B0")); } }
        public List<SequenceSelector> ImplantTemplateGroupVariationDimensionSequence_ { get { return Items.FindAll<Sequence>("007800B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ImplantTemplateGroupVariationDimensionName { get { return Items.FindFirst<LongString>("007800B2") as LongString; } }
        public List<LongString> ImplantTemplateGroupVariationDimensionName_ { get { return Items.FindAll<LongString>("007800B2").ToList(); } }
        public SequenceSelector ImplantTemplateGroupVariationDimensionRankSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("007800B4")); } }
        public List<SequenceSelector> ImplantTemplateGroupVariationDimensionRankSequence_ { get { return Items.FindAll<Sequence>("007800B4").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReferencedImplantTemplateGroupMemberID { get { return Items.FindFirst<UnsignedShort>("007800B6") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedImplantTemplateGroupMemberID_ { get { return Items.FindAll<UnsignedShort>("007800B6").ToList(); } }
        public UnsignedShort ImplantTemplateGroupVariationDimensionRank { get { return Items.FindFirst<UnsignedShort>("007800B8") as UnsignedShort; } }
        public List<UnsignedShort> ImplantTemplateGroupVariationDimensionRank_ { get { return Items.FindAll<UnsignedShort>("007800B8").ToList(); } }
        public ShortString StorageMediaFileSetID { get { return Items.FindFirst<ShortString>("00880130") as ShortString; } }
        public List<ShortString> StorageMediaFileSetID_ { get { return Items.FindAll<ShortString>("00880130").ToList(); } }
        public UniqueIdentifier StorageMediaFileSetUID { get { return Items.FindFirst<UniqueIdentifier>("00880140") as UniqueIdentifier; } }
        public List<UniqueIdentifier> StorageMediaFileSetUID_ { get { return Items.FindAll<UniqueIdentifier>("00880140").ToList(); } }
        public SequenceSelector IconImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("00880200")); } }
        public List<SequenceSelector> IconImageSequence_ { get { return Items.FindAll<Sequence>("00880200").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString TopicTitleRetired { get { return Items.FindFirst<LongString>("00880904") as LongString; } }
        public List<LongString> TopicTitleRetired_ { get { return Items.FindAll<LongString>("00880904").ToList(); } }
        public ShortText TopicSubjectRetired { get { return Items.FindFirst<ShortText>("00880906") as ShortText; } }
        public List<ShortText> TopicSubjectRetired_ { get { return Items.FindAll<ShortText>("00880906").ToList(); } }
        public LongString TopicAuthorRetired { get { return Items.FindFirst<LongString>("00880910") as LongString; } }
        public List<LongString> TopicAuthorRetired_ { get { return Items.FindAll<LongString>("00880910").ToList(); } }
        public LongString TopicKeywordsRetired { get { return Items.FindFirst<LongString>("00880912") as LongString; } }
        public List<LongString> TopicKeywordsRetired_ { get { return Items.FindAll<LongString>("00880912").ToList(); } }
        public CodeString SOPInstanceStatus { get { return Items.FindFirst<CodeString>("01000410") as CodeString; } }
        public List<CodeString> SOPInstanceStatus_ { get { return Items.FindAll<CodeString>("01000410").ToList(); } }
        public EvilDICOM.Core.Element.DateTime SOPAuthorizationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("01000420") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> SOPAuthorizationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("01000420").ToList(); } }
        public LongText SOPAuthorizationComment { get { return Items.FindFirst<LongText>("01000424") as LongText; } }
        public List<LongText> SOPAuthorizationComment_ { get { return Items.FindAll<LongText>("01000424").ToList(); } }
        public LongString AuthorizationEquipmentCertificationNumber { get { return Items.FindFirst<LongString>("01000426") as LongString; } }
        public List<LongString> AuthorizationEquipmentCertificationNumber_ { get { return Items.FindAll<LongString>("01000426").ToList(); } }
        public UnsignedShort MACIDNumber { get { return Items.FindFirst<UnsignedShort>("04000005") as UnsignedShort; } }
        public List<UnsignedShort> MACIDNumber_ { get { return Items.FindAll<UnsignedShort>("04000005").ToList(); } }
        public UniqueIdentifier MACCalculationTransferSyntaxUID { get { return Items.FindFirst<UniqueIdentifier>("04000010") as UniqueIdentifier; } }
        public List<UniqueIdentifier> MACCalculationTransferSyntaxUID_ { get { return Items.FindAll<UniqueIdentifier>("04000010").ToList(); } }
        public CodeString MACAlgorithm { get { return Items.FindFirst<CodeString>("04000015") as CodeString; } }
        public List<CodeString> MACAlgorithm_ { get { return Items.FindAll<CodeString>("04000015").ToList(); } }
        public AttributeTag DataElementsSigned { get { return Items.FindFirst<AttributeTag>("04000020") as AttributeTag; } }
        public List<AttributeTag> DataElementsSigned_ { get { return Items.FindAll<AttributeTag>("04000020").ToList(); } }
        public UniqueIdentifier DigitalSignatureUID { get { return Items.FindFirst<UniqueIdentifier>("04000100") as UniqueIdentifier; } }
        public List<UniqueIdentifier> DigitalSignatureUID_ { get { return Items.FindAll<UniqueIdentifier>("04000100").ToList(); } }
        public EvilDICOM.Core.Element.DateTime DigitalSignatureDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("04000105") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> DigitalSignatureDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("04000105").ToList(); } }
        public CodeString CertificateType { get { return Items.FindFirst<CodeString>("04000110") as CodeString; } }
        public List<CodeString> CertificateType_ { get { return Items.FindAll<CodeString>("04000110").ToList(); } }
        public OtherByteString CertificateOfSigner { get { return Items.FindFirst<OtherByteString>("04000115") as OtherByteString; } }
        public List<OtherByteString> CertificateOfSigner_ { get { return Items.FindAll<OtherByteString>("04000115").ToList(); } }
        public OtherByteString Signature { get { return Items.FindFirst<OtherByteString>("04000120") as OtherByteString; } }
        public List<OtherByteString> Signature_ { get { return Items.FindAll<OtherByteString>("04000120").ToList(); } }
        public CodeString CertifiedTimestampType { get { return Items.FindFirst<CodeString>("04000305") as CodeString; } }
        public List<CodeString> CertifiedTimestampType_ { get { return Items.FindAll<CodeString>("04000305").ToList(); } }
        public OtherByteString CertifiedTimestamp { get { return Items.FindFirst<OtherByteString>("04000310") as OtherByteString; } }
        public List<OtherByteString> CertifiedTimestamp_ { get { return Items.FindAll<OtherByteString>("04000310").ToList(); } }
        public SequenceSelector DigitalSignaturePurposeCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000401")); } }
        public List<SequenceSelector> DigitalSignaturePurposeCodeSequence_ { get { return Items.FindAll<Sequence>("04000401").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedDigitalSignatureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000402")); } }
        public List<SequenceSelector> ReferencedDigitalSignatureSequence_ { get { return Items.FindAll<Sequence>("04000402").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedSOPInstanceMACSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000403")); } }
        public List<SequenceSelector> ReferencedSOPInstanceMACSequence_ { get { return Items.FindAll<Sequence>("04000403").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString MAC { get { return Items.FindFirst<OtherByteString>("04000404") as OtherByteString; } }
        public List<OtherByteString> MAC_ { get { return Items.FindAll<OtherByteString>("04000404").ToList(); } }
        public SequenceSelector EncryptedAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000500")); } }
        public List<SequenceSelector> EncryptedAttributesSequence_ { get { return Items.FindAll<Sequence>("04000500").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier EncryptedContentTransferSyntaxUID { get { return Items.FindFirst<UniqueIdentifier>("04000510") as UniqueIdentifier; } }
        public List<UniqueIdentifier> EncryptedContentTransferSyntaxUID_ { get { return Items.FindAll<UniqueIdentifier>("04000510").ToList(); } }
        public OtherByteString EncryptedContent { get { return Items.FindFirst<OtherByteString>("04000520") as OtherByteString; } }
        public List<OtherByteString> EncryptedContent_ { get { return Items.FindAll<OtherByteString>("04000520").ToList(); } }
        public SequenceSelector ModifiedAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000550")); } }
        public List<SequenceSelector> ModifiedAttributesSequence_ { get { return Items.FindAll<Sequence>("04000550").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OriginalAttributesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("04000561")); } }
        public List<SequenceSelector> OriginalAttributesSequence_ { get { return Items.FindAll<Sequence>("04000561").Select(s => new SequenceSelector(s)).ToList(); } }
        public EvilDICOM.Core.Element.DateTime AttributeModificationDateTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("04000562") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> AttributeModificationDateTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("04000562").ToList(); } }
        public LongString ModifyingSystem { get { return Items.FindFirst<LongString>("04000563") as LongString; } }
        public List<LongString> ModifyingSystem_ { get { return Items.FindAll<LongString>("04000563").ToList(); } }
        public LongString SourceOfPreviousValues { get { return Items.FindFirst<LongString>("04000564") as LongString; } }
        public List<LongString> SourceOfPreviousValues_ { get { return Items.FindAll<LongString>("04000564").ToList(); } }
        public CodeString ReasonForTheAttributeModification { get { return Items.FindFirst<CodeString>("04000565") as CodeString; } }
        public List<CodeString> ReasonForTheAttributeModification_ { get { return Items.FindAll<CodeString>("04000565").ToList(); } }
        public UnsignedShort EscapeTripletRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx0") as UnsignedShort; } }
        public List<UnsignedShort> EscapeTripletRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx0").ToList(); } }
        public UnsignedShort RunLengthTripletRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx1") as UnsignedShort; } }
        public List<UnsignedShort> RunLengthTripletRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx1").ToList(); } }
        public UnsignedShort HuffmanTableSizeRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx2") as UnsignedShort; } }
        public List<UnsignedShort> HuffmanTableSizeRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx2").ToList(); } }
        public UnsignedShort HuffmanTableTripletRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx3") as UnsignedShort; } }
        public List<UnsignedShort> HuffmanTableTripletRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx3").ToList(); } }
        public UnsignedShort ShiftTableSizeRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx4") as UnsignedShort; } }
        public List<UnsignedShort> ShiftTableSizeRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx4").ToList(); } }
        public UnsignedShort ShiftTableTripletRetired { get { return Items.FindFirst<UnsignedShort>("1000xxx5") as UnsignedShort; } }
        public List<UnsignedShort> ShiftTableTripletRetired_ { get { return Items.FindAll<UnsignedShort>("1000xxx5").ToList(); } }
        public UnsignedShort ZonalMapRetired { get { return Items.FindFirst<UnsignedShort>("1010xxxx") as UnsignedShort; } }
        public List<UnsignedShort> ZonalMapRetired_ { get { return Items.FindAll<UnsignedShort>("1010xxxx").ToList(); } }
        public IntegerString NumberOfCopies { get { return Items.FindFirst<IntegerString>("20000010") as IntegerString; } }
        public List<IntegerString> NumberOfCopies_ { get { return Items.FindAll<IntegerString>("20000010").ToList(); } }
        public SequenceSelector PrinterConfigurationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("2000001E")); } }
        public List<SequenceSelector> PrinterConfigurationSequence_ { get { return Items.FindAll<Sequence>("2000001E").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PrintPriority { get { return Items.FindFirst<CodeString>("20000020") as CodeString; } }
        public List<CodeString> PrintPriority_ { get { return Items.FindAll<CodeString>("20000020").ToList(); } }
        public CodeString MediumType { get { return Items.FindFirst<CodeString>("20000030") as CodeString; } }
        public List<CodeString> MediumType_ { get { return Items.FindAll<CodeString>("20000030").ToList(); } }
        public CodeString FilmDestination { get { return Items.FindFirst<CodeString>("20000040") as CodeString; } }
        public List<CodeString> FilmDestination_ { get { return Items.FindAll<CodeString>("20000040").ToList(); } }
        public LongString FilmSessionLabel { get { return Items.FindFirst<LongString>("20000050") as LongString; } }
        public List<LongString> FilmSessionLabel_ { get { return Items.FindAll<LongString>("20000050").ToList(); } }
        public IntegerString MemoryAllocation { get { return Items.FindFirst<IntegerString>("20000060") as IntegerString; } }
        public List<IntegerString> MemoryAllocation_ { get { return Items.FindAll<IntegerString>("20000060").ToList(); } }
        public IntegerString MaximumMemoryAllocation { get { return Items.FindFirst<IntegerString>("20000061") as IntegerString; } }
        public List<IntegerString> MaximumMemoryAllocation_ { get { return Items.FindAll<IntegerString>("20000061").ToList(); } }
        public CodeString ColorImagePrintingFlagRetired { get { return Items.FindFirst<CodeString>("20000062") as CodeString; } }
        public List<CodeString> ColorImagePrintingFlagRetired_ { get { return Items.FindAll<CodeString>("20000062").ToList(); } }
        public CodeString CollationFlagRetired { get { return Items.FindFirst<CodeString>("20000063") as CodeString; } }
        public List<CodeString> CollationFlagRetired_ { get { return Items.FindAll<CodeString>("20000063").ToList(); } }
        public CodeString AnnotationFlagRetired { get { return Items.FindFirst<CodeString>("20000065") as CodeString; } }
        public List<CodeString> AnnotationFlagRetired_ { get { return Items.FindAll<CodeString>("20000065").ToList(); } }
        public CodeString ImageOverlayFlagRetired { get { return Items.FindFirst<CodeString>("20000067") as CodeString; } }
        public List<CodeString> ImageOverlayFlagRetired_ { get { return Items.FindAll<CodeString>("20000067").ToList(); } }
        public CodeString PresentationLUTFlagRetired { get { return Items.FindFirst<CodeString>("20000069") as CodeString; } }
        public List<CodeString> PresentationLUTFlagRetired_ { get { return Items.FindAll<CodeString>("20000069").ToList(); } }
        public CodeString ImageBoxPresentationLUTFlagRetired { get { return Items.FindFirst<CodeString>("2000006A") as CodeString; } }
        public List<CodeString> ImageBoxPresentationLUTFlagRetired_ { get { return Items.FindAll<CodeString>("2000006A").ToList(); } }
        public UnsignedShort MemoryBitDepth { get { return Items.FindFirst<UnsignedShort>("200000A0") as UnsignedShort; } }
        public List<UnsignedShort> MemoryBitDepth_ { get { return Items.FindAll<UnsignedShort>("200000A0").ToList(); } }
        public UnsignedShort PrintingBitDepth { get { return Items.FindFirst<UnsignedShort>("200000A1") as UnsignedShort; } }
        public List<UnsignedShort> PrintingBitDepth_ { get { return Items.FindAll<UnsignedShort>("200000A1").ToList(); } }
        public SequenceSelector MediaInstalledSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("200000A2")); } }
        public List<SequenceSelector> MediaInstalledSequence_ { get { return Items.FindAll<Sequence>("200000A2").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OtherMediaAvailableSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("200000A4")); } }
        public List<SequenceSelector> OtherMediaAvailableSequence_ { get { return Items.FindAll<Sequence>("200000A4").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SupportedImageDisplayFormatsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("200000A8")); } }
        public List<SequenceSelector> SupportedImageDisplayFormatsSequence_ { get { return Items.FindAll<Sequence>("200000A8").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedFilmBoxSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20000500")); } }
        public List<SequenceSelector> ReferencedFilmBoxSequence_ { get { return Items.FindAll<Sequence>("20000500").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedStoredPrintSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20000510")); } }
        public List<SequenceSelector> ReferencedStoredPrintSequenceRetired_ { get { return Items.FindAll<Sequence>("20000510").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText ImageDisplayFormat { get { return Items.FindFirst<ShortText>("20100010") as ShortText; } }
        public List<ShortText> ImageDisplayFormat_ { get { return Items.FindAll<ShortText>("20100010").ToList(); } }
        public CodeString AnnotationDisplayFormatID { get { return Items.FindFirst<CodeString>("20100030") as CodeString; } }
        public List<CodeString> AnnotationDisplayFormatID_ { get { return Items.FindAll<CodeString>("20100030").ToList(); } }
        public CodeString FilmOrientation { get { return Items.FindFirst<CodeString>("20100040") as CodeString; } }
        public List<CodeString> FilmOrientation_ { get { return Items.FindAll<CodeString>("20100040").ToList(); } }
        public CodeString FilmSizeID { get { return Items.FindFirst<CodeString>("20100050") as CodeString; } }
        public List<CodeString> FilmSizeID_ { get { return Items.FindAll<CodeString>("20100050").ToList(); } }
        public CodeString PrinterResolutionID { get { return Items.FindFirst<CodeString>("20100052") as CodeString; } }
        public List<CodeString> PrinterResolutionID_ { get { return Items.FindAll<CodeString>("20100052").ToList(); } }
        public CodeString DefaultPrinterResolutionID { get { return Items.FindFirst<CodeString>("20100054") as CodeString; } }
        public List<CodeString> DefaultPrinterResolutionID_ { get { return Items.FindAll<CodeString>("20100054").ToList(); } }
        public CodeString MagnificationType { get { return Items.FindFirst<CodeString>("20100060") as CodeString; } }
        public List<CodeString> MagnificationType_ { get { return Items.FindAll<CodeString>("20100060").ToList(); } }
        public CodeString SmoothingType { get { return Items.FindFirst<CodeString>("20100080") as CodeString; } }
        public List<CodeString> SmoothingType_ { get { return Items.FindAll<CodeString>("20100080").ToList(); } }
        public CodeString DefaultMagnificationType { get { return Items.FindFirst<CodeString>("201000A6") as CodeString; } }
        public List<CodeString> DefaultMagnificationType_ { get { return Items.FindAll<CodeString>("201000A6").ToList(); } }
        public CodeString OtherMagnificationTypesAvailable { get { return Items.FindFirst<CodeString>("201000A7") as CodeString; } }
        public List<CodeString> OtherMagnificationTypesAvailable_ { get { return Items.FindAll<CodeString>("201000A7").ToList(); } }
        public CodeString DefaultSmoothingType { get { return Items.FindFirst<CodeString>("201000A8") as CodeString; } }
        public List<CodeString> DefaultSmoothingType_ { get { return Items.FindAll<CodeString>("201000A8").ToList(); } }
        public CodeString OtherSmoothingTypesAvailable { get { return Items.FindFirst<CodeString>("201000A9") as CodeString; } }
        public List<CodeString> OtherSmoothingTypesAvailable_ { get { return Items.FindAll<CodeString>("201000A9").ToList(); } }
        public CodeString BorderDensity { get { return Items.FindFirst<CodeString>("20100100") as CodeString; } }
        public List<CodeString> BorderDensity_ { get { return Items.FindAll<CodeString>("20100100").ToList(); } }
        public CodeString EmptyImageDensity { get { return Items.FindFirst<CodeString>("20100110") as CodeString; } }
        public List<CodeString> EmptyImageDensity_ { get { return Items.FindAll<CodeString>("20100110").ToList(); } }
        public UnsignedShort MinDensity { get { return Items.FindFirst<UnsignedShort>("20100120") as UnsignedShort; } }
        public List<UnsignedShort> MinDensity_ { get { return Items.FindAll<UnsignedShort>("20100120").ToList(); } }
        public UnsignedShort MaxDensity { get { return Items.FindFirst<UnsignedShort>("20100130") as UnsignedShort; } }
        public List<UnsignedShort> MaxDensity_ { get { return Items.FindAll<UnsignedShort>("20100130").ToList(); } }
        public CodeString Trim { get { return Items.FindFirst<CodeString>("20100140") as CodeString; } }
        public List<CodeString> Trim_ { get { return Items.FindAll<CodeString>("20100140").ToList(); } }
        public ShortText ConfigurationInformation { get { return Items.FindFirst<ShortText>("20100150") as ShortText; } }
        public List<ShortText> ConfigurationInformation_ { get { return Items.FindAll<ShortText>("20100150").ToList(); } }
        public LongText ConfigurationInformationDescription { get { return Items.FindFirst<LongText>("20100152") as LongText; } }
        public List<LongText> ConfigurationInformationDescription_ { get { return Items.FindAll<LongText>("20100152").ToList(); } }
        public IntegerString MaximumCollatedFilms { get { return Items.FindFirst<IntegerString>("20100154") as IntegerString; } }
        public List<IntegerString> MaximumCollatedFilms_ { get { return Items.FindAll<IntegerString>("20100154").ToList(); } }
        public UnsignedShort Illumination { get { return Items.FindFirst<UnsignedShort>("2010015E") as UnsignedShort; } }
        public List<UnsignedShort> Illumination_ { get { return Items.FindAll<UnsignedShort>("2010015E").ToList(); } }
        public UnsignedShort ReflectedAmbientLight { get { return Items.FindFirst<UnsignedShort>("20100160") as UnsignedShort; } }
        public List<UnsignedShort> ReflectedAmbientLight_ { get { return Items.FindAll<UnsignedShort>("20100160").ToList(); } }
        public DecimalString PrinterPixelSpacing { get { return Items.FindFirst<DecimalString>("20100376") as DecimalString; } }
        public List<DecimalString> PrinterPixelSpacing_ { get { return Items.FindAll<DecimalString>("20100376").ToList(); } }
        public SequenceSelector ReferencedFilmSessionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20100500")); } }
        public List<SequenceSelector> ReferencedFilmSessionSequence_ { get { return Items.FindAll<Sequence>("20100500").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedImageBoxSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20100510")); } }
        public List<SequenceSelector> ReferencedImageBoxSequence_ { get { return Items.FindAll<Sequence>("20100510").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedBasicAnnotationBoxSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20100520")); } }
        public List<SequenceSelector> ReferencedBasicAnnotationBoxSequence_ { get { return Items.FindAll<Sequence>("20100520").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ImageBoxPosition { get { return Items.FindFirst<UnsignedShort>("20200010") as UnsignedShort; } }
        public List<UnsignedShort> ImageBoxPosition_ { get { return Items.FindAll<UnsignedShort>("20200010").ToList(); } }
        public CodeString Polarity { get { return Items.FindFirst<CodeString>("20200020") as CodeString; } }
        public List<CodeString> Polarity_ { get { return Items.FindAll<CodeString>("20200020").ToList(); } }
        public DecimalString RequestedImageSize { get { return Items.FindFirst<DecimalString>("20200030") as DecimalString; } }
        public List<DecimalString> RequestedImageSize_ { get { return Items.FindAll<DecimalString>("20200030").ToList(); } }
        public CodeString RequestedDecimateCropBehavior { get { return Items.FindFirst<CodeString>("20200040") as CodeString; } }
        public List<CodeString> RequestedDecimateCropBehavior_ { get { return Items.FindAll<CodeString>("20200040").ToList(); } }
        public CodeString RequestedResolutionID { get { return Items.FindFirst<CodeString>("20200050") as CodeString; } }
        public List<CodeString> RequestedResolutionID_ { get { return Items.FindAll<CodeString>("20200050").ToList(); } }
        public CodeString RequestedImageSizeFlag { get { return Items.FindFirst<CodeString>("202000A0") as CodeString; } }
        public List<CodeString> RequestedImageSizeFlag_ { get { return Items.FindAll<CodeString>("202000A0").ToList(); } }
        public CodeString DecimateCropResult { get { return Items.FindFirst<CodeString>("202000A2") as CodeString; } }
        public List<CodeString> DecimateCropResult_ { get { return Items.FindAll<CodeString>("202000A2").ToList(); } }
        public SequenceSelector BasicGrayscaleImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20200110")); } }
        public List<SequenceSelector> BasicGrayscaleImageSequence_ { get { return Items.FindAll<Sequence>("20200110").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector BasicColorImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20200111")); } }
        public List<SequenceSelector> BasicColorImageSequence_ { get { return Items.FindAll<Sequence>("20200111").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedImageOverlayBoxSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20200130")); } }
        public List<SequenceSelector> ReferencedImageOverlayBoxSequenceRetired_ { get { return Items.FindAll<Sequence>("20200130").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedVOILUTBoxSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20200140")); } }
        public List<SequenceSelector> ReferencedVOILUTBoxSequenceRetired_ { get { return Items.FindAll<Sequence>("20200140").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort AnnotationPosition { get { return Items.FindFirst<UnsignedShort>("20300010") as UnsignedShort; } }
        public List<UnsignedShort> AnnotationPosition_ { get { return Items.FindAll<UnsignedShort>("20300010").ToList(); } }
        public LongString TextString { get { return Items.FindFirst<LongString>("20300020") as LongString; } }
        public List<LongString> TextString_ { get { return Items.FindAll<LongString>("20300020").ToList(); } }
        public SequenceSelector ReferencedOverlayPlaneSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20400010")); } }
        public List<SequenceSelector> ReferencedOverlayPlaneSequenceRetired_ { get { return Items.FindAll<Sequence>("20400010").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ReferencedOverlayPlaneGroupsRetired { get { return Items.FindFirst<UnsignedShort>("20400011") as UnsignedShort; } }
        public List<UnsignedShort> ReferencedOverlayPlaneGroupsRetired_ { get { return Items.FindAll<UnsignedShort>("20400011").ToList(); } }
        public SequenceSelector OverlayPixelDataSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20400020")); } }
        public List<SequenceSelector> OverlayPixelDataSequenceRetired_ { get { return Items.FindAll<Sequence>("20400020").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString OverlayMagnificationTypeRetired { get { return Items.FindFirst<CodeString>("20400060") as CodeString; } }
        public List<CodeString> OverlayMagnificationTypeRetired_ { get { return Items.FindAll<CodeString>("20400060").ToList(); } }
        public CodeString OverlaySmoothingTypeRetired { get { return Items.FindFirst<CodeString>("20400070") as CodeString; } }
        public List<CodeString> OverlaySmoothingTypeRetired_ { get { return Items.FindAll<CodeString>("20400070").ToList(); } }
        public CodeString OverlayOrImageMagnificationRetired { get { return Items.FindFirst<CodeString>("20400072") as CodeString; } }
        public List<CodeString> OverlayOrImageMagnificationRetired_ { get { return Items.FindAll<CodeString>("20400072").ToList(); } }
        public UnsignedShort MagnifyToNumberOfColumnsRetired { get { return Items.FindFirst<UnsignedShort>("20400074") as UnsignedShort; } }
        public List<UnsignedShort> MagnifyToNumberOfColumnsRetired_ { get { return Items.FindAll<UnsignedShort>("20400074").ToList(); } }
        public CodeString OverlayForegroundDensityRetired { get { return Items.FindFirst<CodeString>("20400080") as CodeString; } }
        public List<CodeString> OverlayForegroundDensityRetired_ { get { return Items.FindAll<CodeString>("20400080").ToList(); } }
        public CodeString OverlayBackgroundDensityRetired { get { return Items.FindFirst<CodeString>("20400082") as CodeString; } }
        public List<CodeString> OverlayBackgroundDensityRetired_ { get { return Items.FindAll<CodeString>("20400082").ToList(); } }
        public CodeString OverlayModeRetired { get { return Items.FindFirst<CodeString>("20400090") as CodeString; } }
        public List<CodeString> OverlayModeRetired_ { get { return Items.FindAll<CodeString>("20400090").ToList(); } }
        public CodeString ThresholdDensityRetired { get { return Items.FindFirst<CodeString>("20400100") as CodeString; } }
        public List<CodeString> ThresholdDensityRetired_ { get { return Items.FindAll<CodeString>("20400100").ToList(); } }
        public SequenceSelector ReferencedImageBoxSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("20400500")); } }
        public List<SequenceSelector> ReferencedImageBoxSequenceRetired_ { get { return Items.FindAll<Sequence>("20400500").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PresentationLUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20500010")); } }
        public List<SequenceSelector> PresentationLUTSequence_ { get { return Items.FindAll<Sequence>("20500010").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PresentationLUTShape { get { return Items.FindFirst<CodeString>("20500020") as CodeString; } }
        public List<CodeString> PresentationLUTShape_ { get { return Items.FindAll<CodeString>("20500020").ToList(); } }
        public SequenceSelector ReferencedPresentationLUTSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("20500500")); } }
        public List<SequenceSelector> ReferencedPresentationLUTSequence_ { get { return Items.FindAll<Sequence>("20500500").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString PrintJobIDRetired { get { return Items.FindFirst<ShortString>("21000010") as ShortString; } }
        public List<ShortString> PrintJobIDRetired_ { get { return Items.FindAll<ShortString>("21000010").ToList(); } }
        public CodeString ExecutionStatus { get { return Items.FindFirst<CodeString>("21000020") as CodeString; } }
        public List<CodeString> ExecutionStatus_ { get { return Items.FindAll<CodeString>("21000020").ToList(); } }
        public CodeString ExecutionStatusInfo { get { return Items.FindFirst<CodeString>("21000030") as CodeString; } }
        public List<CodeString> ExecutionStatusInfo_ { get { return Items.FindAll<CodeString>("21000030").ToList(); } }
        public Date CreationDate { get { return Items.FindFirst<Date>("21000040") as Date; } }
        public List<Date> CreationDate_ { get { return Items.FindAll<Date>("21000040").ToList(); } }
        public Time CreationTime { get { return Items.FindFirst<Time>("21000050") as Time; } }
        public List<Time> CreationTime_ { get { return Items.FindAll<Time>("21000050").ToList(); } }
        public ApplicationEntity Originator { get { return Items.FindFirst<ApplicationEntity>("21000070") as ApplicationEntity; } }
        public List<ApplicationEntity> Originator_ { get { return Items.FindAll<ApplicationEntity>("21000070").ToList(); } }
        public ApplicationEntity DestinationAERetired { get { return Items.FindFirst<ApplicationEntity>("21000140") as ApplicationEntity; } }
        public List<ApplicationEntity> DestinationAERetired_ { get { return Items.FindAll<ApplicationEntity>("21000140").ToList(); } }
        public ShortString OwnerID { get { return Items.FindFirst<ShortString>("21000160") as ShortString; } }
        public List<ShortString> OwnerID_ { get { return Items.FindAll<ShortString>("21000160").ToList(); } }
        public IntegerString NumberOfFilms { get { return Items.FindFirst<IntegerString>("21000170") as IntegerString; } }
        public List<IntegerString> NumberOfFilms_ { get { return Items.FindAll<IntegerString>("21000170").ToList(); } }
        public SequenceSelector ReferencedPrintJobSequencePullStoredPrintRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21000500")); } }
        public List<SequenceSelector> ReferencedPrintJobSequencePullStoredPrintRetired_ { get { return Items.FindAll<Sequence>("21000500").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PrinterStatus { get { return Items.FindFirst<CodeString>("21100010") as CodeString; } }
        public List<CodeString> PrinterStatus_ { get { return Items.FindAll<CodeString>("21100010").ToList(); } }
        public CodeString PrinterStatusInfo { get { return Items.FindFirst<CodeString>("21100020") as CodeString; } }
        public List<CodeString> PrinterStatusInfo_ { get { return Items.FindAll<CodeString>("21100020").ToList(); } }
        public LongString PrinterName { get { return Items.FindFirst<LongString>("21100030") as LongString; } }
        public List<LongString> PrinterName_ { get { return Items.FindAll<LongString>("21100030").ToList(); } }
        public ShortString PrintQueueIDRetired { get { return Items.FindFirst<ShortString>("21100099") as ShortString; } }
        public List<ShortString> PrintQueueIDRetired_ { get { return Items.FindAll<ShortString>("21100099").ToList(); } }
        public CodeString QueueStatusRetired { get { return Items.FindFirst<CodeString>("21200010") as CodeString; } }
        public List<CodeString> QueueStatusRetired_ { get { return Items.FindAll<CodeString>("21200010").ToList(); } }
        public SequenceSelector PrintJobDescriptionSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21200050")); } }
        public List<SequenceSelector> PrintJobDescriptionSequenceRetired_ { get { return Items.FindAll<Sequence>("21200050").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedPrintJobSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21200070")); } }
        public List<SequenceSelector> ReferencedPrintJobSequenceRetired_ { get { return Items.FindAll<Sequence>("21200070").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PrintManagementCapabilitiesSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300010")); } }
        public List<SequenceSelector> PrintManagementCapabilitiesSequenceRetired_ { get { return Items.FindAll<Sequence>("21300010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PrinterCharacteristicsSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300015")); } }
        public List<SequenceSelector> PrinterCharacteristicsSequenceRetired_ { get { return Items.FindAll<Sequence>("21300015").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector FilmBoxContentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300030")); } }
        public List<SequenceSelector> FilmBoxContentSequenceRetired_ { get { return Items.FindAll<Sequence>("21300030").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImageBoxContentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300040")); } }
        public List<SequenceSelector> ImageBoxContentSequenceRetired_ { get { return Items.FindAll<Sequence>("21300040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector AnnotationContentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300050")); } }
        public List<SequenceSelector> AnnotationContentSequenceRetired_ { get { return Items.FindAll<Sequence>("21300050").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ImageOverlayBoxContentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300060")); } }
        public List<SequenceSelector> ImageOverlayBoxContentSequenceRetired_ { get { return Items.FindAll<Sequence>("21300060").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PresentationLUTContentSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("21300080")); } }
        public List<SequenceSelector> PresentationLUTContentSequenceRetired_ { get { return Items.FindAll<Sequence>("21300080").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ProposedStudySequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("213000A0")); } }
        public List<SequenceSelector> ProposedStudySequenceRetired_ { get { return Items.FindAll<Sequence>("213000A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector OriginalImageSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("213000C0")); } }
        public List<SequenceSelector> OriginalImageSequenceRetired_ { get { return Items.FindAll<Sequence>("213000C0").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString LabelUsingInformationExtractedFromInstances { get { return Items.FindFirst<CodeString>("22000001") as CodeString; } }
        public List<CodeString> LabelUsingInformationExtractedFromInstances_ { get { return Items.FindAll<CodeString>("22000001").ToList(); } }
        public UnlimitedText LabelText { get { return Items.FindFirst<UnlimitedText>("22000002") as UnlimitedText; } }
        public List<UnlimitedText> LabelText_ { get { return Items.FindAll<UnlimitedText>("22000002").ToList(); } }
        public CodeString LabelStyleSelection { get { return Items.FindFirst<CodeString>("22000003") as CodeString; } }
        public List<CodeString> LabelStyleSelection_ { get { return Items.FindAll<CodeString>("22000003").ToList(); } }
        public LongText MediaDisposition { get { return Items.FindFirst<LongText>("22000004") as LongText; } }
        public List<LongText> MediaDisposition_ { get { return Items.FindAll<LongText>("22000004").ToList(); } }
        public LongText BarcodeValue { get { return Items.FindFirst<LongText>("22000005") as LongText; } }
        public List<LongText> BarcodeValue_ { get { return Items.FindAll<LongText>("22000005").ToList(); } }
        public CodeString BarcodeSymbology { get { return Items.FindFirst<CodeString>("22000006") as CodeString; } }
        public List<CodeString> BarcodeSymbology_ { get { return Items.FindAll<CodeString>("22000006").ToList(); } }
        public CodeString AllowMediaSplitting { get { return Items.FindFirst<CodeString>("22000007") as CodeString; } }
        public List<CodeString> AllowMediaSplitting_ { get { return Items.FindAll<CodeString>("22000007").ToList(); } }
        public CodeString IncludeNonDICOMObjects { get { return Items.FindFirst<CodeString>("22000008") as CodeString; } }
        public List<CodeString> IncludeNonDICOMObjects_ { get { return Items.FindAll<CodeString>("22000008").ToList(); } }
        public CodeString IncludeDisplayApplication { get { return Items.FindFirst<CodeString>("22000009") as CodeString; } }
        public List<CodeString> IncludeDisplayApplication_ { get { return Items.FindAll<CodeString>("22000009").ToList(); } }
        public CodeString PreserveCompositeInstancesAfterMediaCreation { get { return Items.FindFirst<CodeString>("2200000A") as CodeString; } }
        public List<CodeString> PreserveCompositeInstancesAfterMediaCreation_ { get { return Items.FindAll<CodeString>("2200000A").ToList(); } }
        public UnsignedShort TotalNumberOfPiecesOfMediaCreated { get { return Items.FindFirst<UnsignedShort>("2200000B") as UnsignedShort; } }
        public List<UnsignedShort> TotalNumberOfPiecesOfMediaCreated_ { get { return Items.FindAll<UnsignedShort>("2200000B").ToList(); } }
        public LongString RequestedMediaApplicationProfile { get { return Items.FindFirst<LongString>("2200000C") as LongString; } }
        public List<LongString> RequestedMediaApplicationProfile_ { get { return Items.FindAll<LongString>("2200000C").ToList(); } }
        public SequenceSelector ReferencedStorageMediaSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("2200000D")); } }
        public List<SequenceSelector> ReferencedStorageMediaSequence_ { get { return Items.FindAll<Sequence>("2200000D").Select(s => new SequenceSelector(s)).ToList(); } }
        public AttributeTag FailureAttributes { get { return Items.FindFirst<AttributeTag>("2200000E") as AttributeTag; } }
        public List<AttributeTag> FailureAttributes_ { get { return Items.FindAll<AttributeTag>("2200000E").ToList(); } }
        public CodeString AllowLossyCompression { get { return Items.FindFirst<CodeString>("2200000F") as CodeString; } }
        public List<CodeString> AllowLossyCompression_ { get { return Items.FindAll<CodeString>("2200000F").ToList(); } }
        public CodeString RequestPriority { get { return Items.FindFirst<CodeString>("22000020") as CodeString; } }
        public List<CodeString> RequestPriority_ { get { return Items.FindAll<CodeString>("22000020").ToList(); } }
        public ShortString RTImageLabel { get { return Items.FindFirst<ShortString>("30020002") as ShortString; } }
        public List<ShortString> RTImageLabel_ { get { return Items.FindAll<ShortString>("30020002").ToList(); } }
        public LongString RTImageName { get { return Items.FindFirst<LongString>("30020003") as LongString; } }
        public List<LongString> RTImageName_ { get { return Items.FindAll<LongString>("30020003").ToList(); } }
        public ShortText RTImageDescription { get { return Items.FindFirst<ShortText>("30020004") as ShortText; } }
        public List<ShortText> RTImageDescription_ { get { return Items.FindAll<ShortText>("30020004").ToList(); } }
        public CodeString ReportedValuesOrigin { get { return Items.FindFirst<CodeString>("3002000A") as CodeString; } }
        public List<CodeString> ReportedValuesOrigin_ { get { return Items.FindAll<CodeString>("3002000A").ToList(); } }
        public CodeString RTImagePlane { get { return Items.FindFirst<CodeString>("3002000C") as CodeString; } }
        public List<CodeString> RTImagePlane_ { get { return Items.FindAll<CodeString>("3002000C").ToList(); } }
        public DecimalString XRayImageReceptorTranslation { get { return Items.FindFirst<DecimalString>("3002000D") as DecimalString; } }
        public List<DecimalString> XRayImageReceptorTranslation_ { get { return Items.FindAll<DecimalString>("3002000D").ToList(); } }
        public DecimalString XRayImageReceptorAngle { get { return Items.FindFirst<DecimalString>("3002000E") as DecimalString; } }
        public List<DecimalString> XRayImageReceptorAngle_ { get { return Items.FindAll<DecimalString>("3002000E").ToList(); } }
        public DecimalString RTImageOrientation { get { return Items.FindFirst<DecimalString>("30020010") as DecimalString; } }
        public List<DecimalString> RTImageOrientation_ { get { return Items.FindAll<DecimalString>("30020010").ToList(); } }
        public DecimalString ImagePlanePixelSpacing { get { return Items.FindFirst<DecimalString>("30020011") as DecimalString; } }
        public List<DecimalString> ImagePlanePixelSpacing_ { get { return Items.FindAll<DecimalString>("30020011").ToList(); } }
        public DecimalString RTImagePosition { get { return Items.FindFirst<DecimalString>("30020012") as DecimalString; } }
        public List<DecimalString> RTImagePosition_ { get { return Items.FindAll<DecimalString>("30020012").ToList(); } }
        public ShortString RadiationMachineName { get { return Items.FindFirst<ShortString>("30020020") as ShortString; } }
        public List<ShortString> RadiationMachineName_ { get { return Items.FindAll<ShortString>("30020020").ToList(); } }
        public DecimalString RadiationMachineSAD { get { return Items.FindFirst<DecimalString>("30020022") as DecimalString; } }
        public List<DecimalString> RadiationMachineSAD_ { get { return Items.FindAll<DecimalString>("30020022").ToList(); } }
        public DecimalString RadiationMachineSSD { get { return Items.FindFirst<DecimalString>("30020024") as DecimalString; } }
        public List<DecimalString> RadiationMachineSSD_ { get { return Items.FindAll<DecimalString>("30020024").ToList(); } }
        public DecimalString RTImageSID { get { return Items.FindFirst<DecimalString>("30020026") as DecimalString; } }
        public List<DecimalString> RTImageSID_ { get { return Items.FindAll<DecimalString>("30020026").ToList(); } }
        public DecimalString SourceToReferenceObjectDistance { get { return Items.FindFirst<DecimalString>("30020028") as DecimalString; } }
        public List<DecimalString> SourceToReferenceObjectDistance_ { get { return Items.FindAll<DecimalString>("30020028").ToList(); } }
        public IntegerString FractionNumber { get { return Items.FindFirst<IntegerString>("30020029") as IntegerString; } }
        public List<IntegerString> FractionNumber_ { get { return Items.FindAll<IntegerString>("30020029").ToList(); } }
        public SequenceSelector ExposureSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30020030")); } }
        public List<SequenceSelector> ExposureSequence_ { get { return Items.FindAll<Sequence>("30020030").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString MetersetExposure { get { return Items.FindFirst<DecimalString>("30020032") as DecimalString; } }
        public List<DecimalString> MetersetExposure_ { get { return Items.FindAll<DecimalString>("30020032").ToList(); } }
        public DecimalString DiaphragmPosition { get { return Items.FindFirst<DecimalString>("30020034") as DecimalString; } }
        public List<DecimalString> DiaphragmPosition_ { get { return Items.FindAll<DecimalString>("30020034").ToList(); } }
        public SequenceSelector FluenceMapSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30020040")); } }
        public List<SequenceSelector> FluenceMapSequence_ { get { return Items.FindAll<Sequence>("30020040").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FluenceDataSource { get { return Items.FindFirst<CodeString>("30020041") as CodeString; } }
        public List<CodeString> FluenceDataSource_ { get { return Items.FindAll<CodeString>("30020041").ToList(); } }
        public DecimalString FluenceDataScale { get { return Items.FindFirst<DecimalString>("30020042") as DecimalString; } }
        public List<DecimalString> FluenceDataScale_ { get { return Items.FindAll<DecimalString>("30020042").ToList(); } }
        public SequenceSelector PrimaryFluenceModeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30020050")); } }
        public List<SequenceSelector> PrimaryFluenceModeSequence_ { get { return Items.FindAll<Sequence>("30020050").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FluenceMode { get { return Items.FindFirst<CodeString>("30020051") as CodeString; } }
        public List<CodeString> FluenceMode_ { get { return Items.FindAll<CodeString>("30020051").ToList(); } }
        public ShortString FluenceModeID { get { return Items.FindFirst<ShortString>("30020052") as ShortString; } }
        public List<ShortString> FluenceModeID_ { get { return Items.FindAll<ShortString>("30020052").ToList(); } }
        public CodeString DVHType { get { return Items.FindFirst<CodeString>("30040001") as CodeString; } }
        public List<CodeString> DVHType_ { get { return Items.FindAll<CodeString>("30040001").ToList(); } }
        public CodeString DoseUnits { get { return Items.FindFirst<CodeString>("30040002") as CodeString; } }
        public List<CodeString> DoseUnits_ { get { return Items.FindAll<CodeString>("30040002").ToList(); } }
        public CodeString DoseType { get { return Items.FindFirst<CodeString>("30040004") as CodeString; } }
        public List<CodeString> DoseType_ { get { return Items.FindAll<CodeString>("30040004").ToList(); } }
        public LongString DoseComment { get { return Items.FindFirst<LongString>("30040006") as LongString; } }
        public List<LongString> DoseComment_ { get { return Items.FindAll<LongString>("30040006").ToList(); } }
        public DecimalString NormalizationPoint { get { return Items.FindFirst<DecimalString>("30040008") as DecimalString; } }
        public List<DecimalString> NormalizationPoint_ { get { return Items.FindAll<DecimalString>("30040008").ToList(); } }
        public CodeString DoseSummationType { get { return Items.FindFirst<CodeString>("3004000A") as CodeString; } }
        public List<CodeString> DoseSummationType_ { get { return Items.FindAll<CodeString>("3004000A").ToList(); } }
        public DecimalString GridFrameOffsetVector { get { return Items.FindFirst<DecimalString>("3004000C") as DecimalString; } }
        public List<DecimalString> GridFrameOffsetVector_ { get { return Items.FindAll<DecimalString>("3004000C").ToList(); } }
        public DecimalString DoseGridScaling { get { return Items.FindFirst<DecimalString>("3004000E") as DecimalString; } }
        public List<DecimalString> DoseGridScaling_ { get { return Items.FindAll<DecimalString>("3004000E").ToList(); } }
        public SequenceSelector RTDoseROISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30040010")); } }
        public List<SequenceSelector> RTDoseROISequence_ { get { return Items.FindAll<Sequence>("30040010").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString DoseValue { get { return Items.FindFirst<DecimalString>("30040012") as DecimalString; } }
        public List<DecimalString> DoseValue_ { get { return Items.FindAll<DecimalString>("30040012").ToList(); } }
        public CodeString TissueHeterogeneityCorrection { get { return Items.FindFirst<CodeString>("30040014") as CodeString; } }
        public List<CodeString> TissueHeterogeneityCorrection_ { get { return Items.FindAll<CodeString>("30040014").ToList(); } }
        public DecimalString DVHNormalizationPoint { get { return Items.FindFirst<DecimalString>("30040040") as DecimalString; } }
        public List<DecimalString> DVHNormalizationPoint_ { get { return Items.FindAll<DecimalString>("30040040").ToList(); } }
        public DecimalString DVHNormalizationDoseValue { get { return Items.FindFirst<DecimalString>("30040042") as DecimalString; } }
        public List<DecimalString> DVHNormalizationDoseValue_ { get { return Items.FindAll<DecimalString>("30040042").ToList(); } }
        public SequenceSelector DVHSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30040050")); } }
        public List<SequenceSelector> DVHSequence_ { get { return Items.FindAll<Sequence>("30040050").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString DVHDoseScaling { get { return Items.FindFirst<DecimalString>("30040052") as DecimalString; } }
        public List<DecimalString> DVHDoseScaling_ { get { return Items.FindAll<DecimalString>("30040052").ToList(); } }
        public CodeString DVHVolumeUnits { get { return Items.FindFirst<CodeString>("30040054") as CodeString; } }
        public List<CodeString> DVHVolumeUnits_ { get { return Items.FindAll<CodeString>("30040054").ToList(); } }
        public IntegerString DVHNumberOfBins { get { return Items.FindFirst<IntegerString>("30040056") as IntegerString; } }
        public List<IntegerString> DVHNumberOfBins_ { get { return Items.FindAll<IntegerString>("30040056").ToList(); } }
        public DecimalString DVHData { get { return Items.FindFirst<DecimalString>("30040058") as DecimalString; } }
        public List<DecimalString> DVHData_ { get { return Items.FindAll<DecimalString>("30040058").ToList(); } }
        public SequenceSelector DVHReferencedROISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30040060")); } }
        public List<SequenceSelector> DVHReferencedROISequence_ { get { return Items.FindAll<Sequence>("30040060").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString DVHROIContributionType { get { return Items.FindFirst<CodeString>("30040062") as CodeString; } }
        public List<CodeString> DVHROIContributionType_ { get { return Items.FindAll<CodeString>("30040062").ToList(); } }
        public DecimalString DVHMinimumDose { get { return Items.FindFirst<DecimalString>("30040070") as DecimalString; } }
        public List<DecimalString> DVHMinimumDose_ { get { return Items.FindAll<DecimalString>("30040070").ToList(); } }
        public DecimalString DVHMaximumDose { get { return Items.FindFirst<DecimalString>("30040072") as DecimalString; } }
        public List<DecimalString> DVHMaximumDose_ { get { return Items.FindAll<DecimalString>("30040072").ToList(); } }
        public DecimalString DVHMeanDose { get { return Items.FindFirst<DecimalString>("30040074") as DecimalString; } }
        public List<DecimalString> DVHMeanDose_ { get { return Items.FindAll<DecimalString>("30040074").ToList(); } }
        public ShortString StructureSetLabel { get { return Items.FindFirst<ShortString>("30060002") as ShortString; } }
        public List<ShortString> StructureSetLabel_ { get { return Items.FindAll<ShortString>("30060002").ToList(); } }
        public LongString StructureSetName { get { return Items.FindFirst<LongString>("30060004") as LongString; } }
        public List<LongString> StructureSetName_ { get { return Items.FindAll<LongString>("30060004").ToList(); } }
        public ShortText StructureSetDescription { get { return Items.FindFirst<ShortText>("30060006") as ShortText; } }
        public List<ShortText> StructureSetDescription_ { get { return Items.FindAll<ShortText>("30060006").ToList(); } }
        public Date StructureSetDate { get { return Items.FindFirst<Date>("30060008") as Date; } }
        public List<Date> StructureSetDate_ { get { return Items.FindAll<Date>("30060008").ToList(); } }
        public Time StructureSetTime { get { return Items.FindFirst<Time>("30060009") as Time; } }
        public List<Time> StructureSetTime_ { get { return Items.FindAll<Time>("30060009").ToList(); } }
        public SequenceSelector ReferencedFrameOfReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060010")); } }
        public List<SequenceSelector> ReferencedFrameOfReferenceSequence_ { get { return Items.FindAll<Sequence>("30060010").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RTReferencedStudySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060012")); } }
        public List<SequenceSelector> RTReferencedStudySequence_ { get { return Items.FindAll<Sequence>("30060012").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RTReferencedSeriesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060014")); } }
        public List<SequenceSelector> RTReferencedSeriesSequence_ { get { return Items.FindAll<Sequence>("30060014").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContourImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060016")); } }
        public List<SequenceSelector> ContourImageSequence_ { get { return Items.FindAll<Sequence>("30060016").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector StructureSetROISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060020")); } }
        public List<SequenceSelector> StructureSetROISequence_ { get { return Items.FindAll<Sequence>("30060020").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ROINumber { get { return Items.FindFirst<IntegerString>("30060022") as IntegerString; } }
        public List<IntegerString> ROINumber_ { get { return Items.FindAll<IntegerString>("30060022").ToList(); } }
        public UniqueIdentifier ReferencedFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("30060024") as UniqueIdentifier; } }
        public List<UniqueIdentifier> ReferencedFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("30060024").ToList(); } }
        public LongString ROIName { get { return Items.FindFirst<LongString>("30060026") as LongString; } }
        public List<LongString> ROIName_ { get { return Items.FindAll<LongString>("30060026").ToList(); } }
        public ShortText ROIDescription { get { return Items.FindFirst<ShortText>("30060028") as ShortText; } }
        public List<ShortText> ROIDescription_ { get { return Items.FindAll<ShortText>("30060028").ToList(); } }
        public IntegerString ROIDisplayColor { get { return Items.FindFirst<IntegerString>("3006002A") as IntegerString; } }
        public List<IntegerString> ROIDisplayColor_ { get { return Items.FindAll<IntegerString>("3006002A").ToList(); } }
        public DecimalString ROIVolume { get { return Items.FindFirst<DecimalString>("3006002C") as DecimalString; } }
        public List<DecimalString> ROIVolume_ { get { return Items.FindAll<DecimalString>("3006002C").ToList(); } }
        public SequenceSelector RTRelatedROISequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060030")); } }
        public List<SequenceSelector> RTRelatedROISequence_ { get { return Items.FindAll<Sequence>("30060030").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString RTROIRelationship { get { return Items.FindFirst<CodeString>("30060033") as CodeString; } }
        public List<CodeString> RTROIRelationship_ { get { return Items.FindAll<CodeString>("30060033").ToList(); } }
        public CodeString ROIGenerationAlgorithm { get { return Items.FindFirst<CodeString>("30060036") as CodeString; } }
        public List<CodeString> ROIGenerationAlgorithm_ { get { return Items.FindAll<CodeString>("30060036").ToList(); } }
        public LongString ROIGenerationDescription { get { return Items.FindFirst<LongString>("30060038") as LongString; } }
        public List<LongString> ROIGenerationDescription_ { get { return Items.FindAll<LongString>("30060038").ToList(); } }
        public SequenceSelector ROIContourSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060039")); } }
        public List<SequenceSelector> ROIContourSequence_ { get { return Items.FindAll<Sequence>("30060039").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ContourSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060040")); } }
        public List<SequenceSelector> ContourSequence_ { get { return Items.FindAll<Sequence>("30060040").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ContourGeometricType { get { return Items.FindFirst<CodeString>("30060042") as CodeString; } }
        public List<CodeString> ContourGeometricType_ { get { return Items.FindAll<CodeString>("30060042").ToList(); } }
        public DecimalString ContourSlabThickness { get { return Items.FindFirst<DecimalString>("30060044") as DecimalString; } }
        public List<DecimalString> ContourSlabThickness_ { get { return Items.FindAll<DecimalString>("30060044").ToList(); } }
        public DecimalString ContourOffsetVector { get { return Items.FindFirst<DecimalString>("30060045") as DecimalString; } }
        public List<DecimalString> ContourOffsetVector_ { get { return Items.FindAll<DecimalString>("30060045").ToList(); } }
        public IntegerString NumberOfContourPoints { get { return Items.FindFirst<IntegerString>("30060046") as IntegerString; } }
        public List<IntegerString> NumberOfContourPoints_ { get { return Items.FindAll<IntegerString>("30060046").ToList(); } }
        public IntegerString ContourNumber { get { return Items.FindFirst<IntegerString>("30060048") as IntegerString; } }
        public List<IntegerString> ContourNumber_ { get { return Items.FindAll<IntegerString>("30060048").ToList(); } }
        public IntegerString AttachedContours { get { return Items.FindFirst<IntegerString>("30060049") as IntegerString; } }
        public List<IntegerString> AttachedContours_ { get { return Items.FindAll<IntegerString>("30060049").ToList(); } }
        public DecimalString ContourData { get { return Items.FindFirst<DecimalString>("30060050") as DecimalString; } }
        public List<DecimalString> ContourData_ { get { return Items.FindAll<DecimalString>("30060050").ToList(); } }
        public SequenceSelector RTROIObservationsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060080")); } }
        public List<SequenceSelector> RTROIObservationsSequence_ { get { return Items.FindAll<Sequence>("30060080").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ObservationNumber { get { return Items.FindFirst<IntegerString>("30060082") as IntegerString; } }
        public List<IntegerString> ObservationNumber_ { get { return Items.FindAll<IntegerString>("30060082").ToList(); } }
        public IntegerString ReferencedROINumber { get { return Items.FindFirst<IntegerString>("30060084") as IntegerString; } }
        public List<IntegerString> ReferencedROINumber_ { get { return Items.FindAll<IntegerString>("30060084").ToList(); } }
        public ShortString ROIObservationLabel { get { return Items.FindFirst<ShortString>("30060085") as ShortString; } }
        public List<ShortString> ROIObservationLabel_ { get { return Items.FindAll<ShortString>("30060085").ToList(); } }
        public SequenceSelector RTROIIdentificationCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30060086")); } }
        public List<SequenceSelector> RTROIIdentificationCodeSequence_ { get { return Items.FindAll<Sequence>("30060086").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText ROIObservationDescription { get { return Items.FindFirst<ShortText>("30060088") as ShortText; } }
        public List<ShortText> ROIObservationDescription_ { get { return Items.FindAll<ShortText>("30060088").ToList(); } }
        public SequenceSelector RelatedRTROIObservationsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300600A0")); } }
        public List<SequenceSelector> RelatedRTROIObservationsSequence_ { get { return Items.FindAll<Sequence>("300600A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString RTROIInterpretedType { get { return Items.FindFirst<CodeString>("300600A4") as CodeString; } }
        public List<CodeString> RTROIInterpretedType_ { get { return Items.FindAll<CodeString>("300600A4").ToList(); } }
        public PersonName ROIInterpreter { get { return Items.FindFirst<PersonName>("300600A6") as PersonName; } }
        public List<PersonName> ROIInterpreter_ { get { return Items.FindAll<PersonName>("300600A6").ToList(); } }
        public SequenceSelector ROIPhysicalPropertiesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300600B0")); } }
        public List<SequenceSelector> ROIPhysicalPropertiesSequence_ { get { return Items.FindAll<Sequence>("300600B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ROIPhysicalProperty { get { return Items.FindFirst<CodeString>("300600B2") as CodeString; } }
        public List<CodeString> ROIPhysicalProperty_ { get { return Items.FindAll<CodeString>("300600B2").ToList(); } }
        public DecimalString ROIPhysicalPropertyValue { get { return Items.FindFirst<DecimalString>("300600B4") as DecimalString; } }
        public List<DecimalString> ROIPhysicalPropertyValue_ { get { return Items.FindAll<DecimalString>("300600B4").ToList(); } }
        public SequenceSelector ROIElementalCompositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300600B6")); } }
        public List<SequenceSelector> ROIElementalCompositionSequence_ { get { return Items.FindAll<Sequence>("300600B6").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort ROIElementalCompositionAtomicNumber { get { return Items.FindFirst<UnsignedShort>("300600B7") as UnsignedShort; } }
        public List<UnsignedShort> ROIElementalCompositionAtomicNumber_ { get { return Items.FindAll<UnsignedShort>("300600B7").ToList(); } }
        public FloatingPointSingle ROIElementalCompositionAtomicMassFraction { get { return Items.FindFirst<FloatingPointSingle>("300600B8") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ROIElementalCompositionAtomicMassFraction_ { get { return Items.FindAll<FloatingPointSingle>("300600B8").ToList(); } }
        public SequenceSelector FrameOfReferenceRelationshipSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300600C0")); } }
        public List<SequenceSelector> FrameOfReferenceRelationshipSequence_ { get { return Items.FindAll<Sequence>("300600C0").Select(s => new SequenceSelector(s)).ToList(); } }
        public UniqueIdentifier RelatedFrameOfReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("300600C2") as UniqueIdentifier; } }
        public List<UniqueIdentifier> RelatedFrameOfReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("300600C2").ToList(); } }
        public CodeString FrameOfReferenceTransformationType { get { return Items.FindFirst<CodeString>("300600C4") as CodeString; } }
        public List<CodeString> FrameOfReferenceTransformationType_ { get { return Items.FindAll<CodeString>("300600C4").ToList(); } }
        public DecimalString FrameOfReferenceTransformationMatrix { get { return Items.FindFirst<DecimalString>("300600C6") as DecimalString; } }
        public List<DecimalString> FrameOfReferenceTransformationMatrix_ { get { return Items.FindAll<DecimalString>("300600C6").ToList(); } }
        public LongString FrameOfReferenceTransformationComment { get { return Items.FindFirst<LongString>("300600C8") as LongString; } }
        public List<LongString> FrameOfReferenceTransformationComment_ { get { return Items.FindAll<LongString>("300600C8").ToList(); } }
        public SequenceSelector MeasuredDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080010")); } }
        public List<SequenceSelector> MeasuredDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("30080010").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText MeasuredDoseDescription { get { return Items.FindFirst<ShortText>("30080012") as ShortText; } }
        public List<ShortText> MeasuredDoseDescription_ { get { return Items.FindAll<ShortText>("30080012").ToList(); } }
        public CodeString MeasuredDoseType { get { return Items.FindFirst<CodeString>("30080014") as CodeString; } }
        public List<CodeString> MeasuredDoseType_ { get { return Items.FindAll<CodeString>("30080014").ToList(); } }
        public DecimalString MeasuredDoseValue { get { return Items.FindFirst<DecimalString>("30080016") as DecimalString; } }
        public List<DecimalString> MeasuredDoseValue_ { get { return Items.FindAll<DecimalString>("30080016").ToList(); } }
        public SequenceSelector TreatmentSessionBeamSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080020")); } }
        public List<SequenceSelector> TreatmentSessionBeamSequence_ { get { return Items.FindAll<Sequence>("30080020").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TreatmentSessionIonBeamSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080021")); } }
        public List<SequenceSelector> TreatmentSessionIonBeamSequence_ { get { return Items.FindAll<Sequence>("30080021").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString CurrentFractionNumber { get { return Items.FindFirst<IntegerString>("30080022") as IntegerString; } }
        public List<IntegerString> CurrentFractionNumber_ { get { return Items.FindAll<IntegerString>("30080022").ToList(); } }
        public Date TreatmentControlPointDate { get { return Items.FindFirst<Date>("30080024") as Date; } }
        public List<Date> TreatmentControlPointDate_ { get { return Items.FindAll<Date>("30080024").ToList(); } }
        public Time TreatmentControlPointTime { get { return Items.FindFirst<Time>("30080025") as Time; } }
        public List<Time> TreatmentControlPointTime_ { get { return Items.FindAll<Time>("30080025").ToList(); } }
        public CodeString TreatmentTerminationStatus { get { return Items.FindFirst<CodeString>("3008002A") as CodeString; } }
        public List<CodeString> TreatmentTerminationStatus_ { get { return Items.FindAll<CodeString>("3008002A").ToList(); } }
        public ShortString TreatmentTerminationCode { get { return Items.FindFirst<ShortString>("3008002B") as ShortString; } }
        public List<ShortString> TreatmentTerminationCode_ { get { return Items.FindAll<ShortString>("3008002B").ToList(); } }
        public CodeString TreatmentVerificationStatus { get { return Items.FindFirst<CodeString>("3008002C") as CodeString; } }
        public List<CodeString> TreatmentVerificationStatus_ { get { return Items.FindAll<CodeString>("3008002C").ToList(); } }
        public SequenceSelector ReferencedTreatmentRecordSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080030")); } }
        public List<SequenceSelector> ReferencedTreatmentRecordSequence_ { get { return Items.FindAll<Sequence>("30080030").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString SpecifiedPrimaryMeterset { get { return Items.FindFirst<DecimalString>("30080032") as DecimalString; } }
        public List<DecimalString> SpecifiedPrimaryMeterset_ { get { return Items.FindAll<DecimalString>("30080032").ToList(); } }
        public DecimalString SpecifiedSecondaryMeterset { get { return Items.FindFirst<DecimalString>("30080033") as DecimalString; } }
        public List<DecimalString> SpecifiedSecondaryMeterset_ { get { return Items.FindAll<DecimalString>("30080033").ToList(); } }
        public DecimalString DeliveredPrimaryMeterset { get { return Items.FindFirst<DecimalString>("30080036") as DecimalString; } }
        public List<DecimalString> DeliveredPrimaryMeterset_ { get { return Items.FindAll<DecimalString>("30080036").ToList(); } }
        public DecimalString DeliveredSecondaryMeterset { get { return Items.FindFirst<DecimalString>("30080037") as DecimalString; } }
        public List<DecimalString> DeliveredSecondaryMeterset_ { get { return Items.FindAll<DecimalString>("30080037").ToList(); } }
        public DecimalString SpecifiedTreatmentTime { get { return Items.FindFirst<DecimalString>("3008003A") as DecimalString; } }
        public List<DecimalString> SpecifiedTreatmentTime_ { get { return Items.FindAll<DecimalString>("3008003A").ToList(); } }
        public DecimalString DeliveredTreatmentTime { get { return Items.FindFirst<DecimalString>("3008003B") as DecimalString; } }
        public List<DecimalString> DeliveredTreatmentTime_ { get { return Items.FindAll<DecimalString>("3008003B").ToList(); } }
        public SequenceSelector ControlPointDeliverySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080040")); } }
        public List<SequenceSelector> ControlPointDeliverySequence_ { get { return Items.FindAll<Sequence>("30080040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonControlPointDeliverySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080041")); } }
        public List<SequenceSelector> IonControlPointDeliverySequence_ { get { return Items.FindAll<Sequence>("30080041").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString SpecifiedMeterset { get { return Items.FindFirst<DecimalString>("30080042") as DecimalString; } }
        public List<DecimalString> SpecifiedMeterset_ { get { return Items.FindAll<DecimalString>("30080042").ToList(); } }
        public DecimalString DeliveredMeterset { get { return Items.FindFirst<DecimalString>("30080044") as DecimalString; } }
        public List<DecimalString> DeliveredMeterset_ { get { return Items.FindAll<DecimalString>("30080044").ToList(); } }
        public FloatingPointSingle MetersetRateSet { get { return Items.FindFirst<FloatingPointSingle>("30080045") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MetersetRateSet_ { get { return Items.FindAll<FloatingPointSingle>("30080045").ToList(); } }
        public FloatingPointSingle MetersetRateDelivered { get { return Items.FindFirst<FloatingPointSingle>("30080046") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MetersetRateDelivered_ { get { return Items.FindAll<FloatingPointSingle>("30080046").ToList(); } }
        public FloatingPointSingle ScanSpotMetersetsDelivered { get { return Items.FindFirst<FloatingPointSingle>("30080047") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ScanSpotMetersetsDelivered_ { get { return Items.FindAll<FloatingPointSingle>("30080047").ToList(); } }
        public DecimalString DoseRateDelivered { get { return Items.FindFirst<DecimalString>("30080048") as DecimalString; } }
        public List<DecimalString> DoseRateDelivered_ { get { return Items.FindAll<DecimalString>("30080048").ToList(); } }
        public SequenceSelector TreatmentSummaryCalculatedDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080050")); } }
        public List<SequenceSelector> TreatmentSummaryCalculatedDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("30080050").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString CumulativeDoseToDoseReference { get { return Items.FindFirst<DecimalString>("30080052") as DecimalString; } }
        public List<DecimalString> CumulativeDoseToDoseReference_ { get { return Items.FindAll<DecimalString>("30080052").ToList(); } }
        public Date FirstTreatmentDate { get { return Items.FindFirst<Date>("30080054") as Date; } }
        public List<Date> FirstTreatmentDate_ { get { return Items.FindAll<Date>("30080054").ToList(); } }
        public Date MostRecentTreatmentDate { get { return Items.FindFirst<Date>("30080056") as Date; } }
        public List<Date> MostRecentTreatmentDate_ { get { return Items.FindAll<Date>("30080056").ToList(); } }
        public IntegerString NumberOfFractionsDelivered { get { return Items.FindFirst<IntegerString>("3008005A") as IntegerString; } }
        public List<IntegerString> NumberOfFractionsDelivered_ { get { return Items.FindAll<IntegerString>("3008005A").ToList(); } }
        public SequenceSelector OverrideSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080060")); } }
        public List<SequenceSelector> OverrideSequence_ { get { return Items.FindAll<Sequence>("30080060").Select(s => new SequenceSelector(s)).ToList(); } }
        public AttributeTag ParameterSequencePointer { get { return Items.FindFirst<AttributeTag>("30080061") as AttributeTag; } }
        public List<AttributeTag> ParameterSequencePointer_ { get { return Items.FindAll<AttributeTag>("30080061").ToList(); } }
        public AttributeTag OverrideParameterPointer { get { return Items.FindFirst<AttributeTag>("30080062") as AttributeTag; } }
        public List<AttributeTag> OverrideParameterPointer_ { get { return Items.FindAll<AttributeTag>("30080062").ToList(); } }
        public IntegerString ParameterItemIndex { get { return Items.FindFirst<IntegerString>("30080063") as IntegerString; } }
        public List<IntegerString> ParameterItemIndex_ { get { return Items.FindAll<IntegerString>("30080063").ToList(); } }
        public IntegerString MeasuredDoseReferenceNumber { get { return Items.FindFirst<IntegerString>("30080064") as IntegerString; } }
        public List<IntegerString> MeasuredDoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("30080064").ToList(); } }
        public AttributeTag ParameterPointer { get { return Items.FindFirst<AttributeTag>("30080065") as AttributeTag; } }
        public List<AttributeTag> ParameterPointer_ { get { return Items.FindAll<AttributeTag>("30080065").ToList(); } }
        public ShortText OverrideReason { get { return Items.FindFirst<ShortText>("30080066") as ShortText; } }
        public List<ShortText> OverrideReason_ { get { return Items.FindAll<ShortText>("30080066").ToList(); } }
        public SequenceSelector CorrectedParameterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080068")); } }
        public List<SequenceSelector> CorrectedParameterSequence_ { get { return Items.FindAll<Sequence>("30080068").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle CorrectionValue { get { return Items.FindFirst<FloatingPointSingle>("3008006A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CorrectionValue_ { get { return Items.FindAll<FloatingPointSingle>("3008006A").ToList(); } }
        public SequenceSelector CalculatedDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080070")); } }
        public List<SequenceSelector> CalculatedDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("30080070").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString CalculatedDoseReferenceNumber { get { return Items.FindFirst<IntegerString>("30080072") as IntegerString; } }
        public List<IntegerString> CalculatedDoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("30080072").ToList(); } }
        public ShortText CalculatedDoseReferenceDescription { get { return Items.FindFirst<ShortText>("30080074") as ShortText; } }
        public List<ShortText> CalculatedDoseReferenceDescription_ { get { return Items.FindAll<ShortText>("30080074").ToList(); } }
        public DecimalString CalculatedDoseReferenceDoseValue { get { return Items.FindFirst<DecimalString>("30080076") as DecimalString; } }
        public List<DecimalString> CalculatedDoseReferenceDoseValue_ { get { return Items.FindAll<DecimalString>("30080076").ToList(); } }
        public DecimalString StartMeterset { get { return Items.FindFirst<DecimalString>("30080078") as DecimalString; } }
        public List<DecimalString> StartMeterset_ { get { return Items.FindAll<DecimalString>("30080078").ToList(); } }
        public DecimalString EndMeterset { get { return Items.FindFirst<DecimalString>("3008007A") as DecimalString; } }
        public List<DecimalString> EndMeterset_ { get { return Items.FindAll<DecimalString>("3008007A").ToList(); } }
        public SequenceSelector ReferencedMeasuredDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080080")); } }
        public List<SequenceSelector> ReferencedMeasuredDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("30080080").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedMeasuredDoseReferenceNumber { get { return Items.FindFirst<IntegerString>("30080082") as IntegerString; } }
        public List<IntegerString> ReferencedMeasuredDoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("30080082").ToList(); } }
        public SequenceSelector ReferencedCalculatedDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080090")); } }
        public List<SequenceSelector> ReferencedCalculatedDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("30080090").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedCalculatedDoseReferenceNumber { get { return Items.FindFirst<IntegerString>("30080092") as IntegerString; } }
        public List<IntegerString> ReferencedCalculatedDoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("30080092").ToList(); } }
        public SequenceSelector BeamLimitingDeviceLeafPairsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800A0")); } }
        public List<SequenceSelector> BeamLimitingDeviceLeafPairsSequence_ { get { return Items.FindAll<Sequence>("300800A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedWedgeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800B0")); } }
        public List<SequenceSelector> RecordedWedgeSequence_ { get { return Items.FindAll<Sequence>("300800B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedCompensatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800C0")); } }
        public List<SequenceSelector> RecordedCompensatorSequence_ { get { return Items.FindAll<Sequence>("300800C0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedBlockSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800D0")); } }
        public List<SequenceSelector> RecordedBlockSequence_ { get { return Items.FindAll<Sequence>("300800D0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector TreatmentSummaryMeasuredDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800E0")); } }
        public List<SequenceSelector> TreatmentSummaryMeasuredDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("300800E0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedSnoutSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800F0")); } }
        public List<SequenceSelector> RecordedSnoutSequence_ { get { return Items.FindAll<Sequence>("300800F0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedRangeShifterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800F2")); } }
        public List<SequenceSelector> RecordedRangeShifterSequence_ { get { return Items.FindAll<Sequence>("300800F2").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedLateralSpreadingDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800F4")); } }
        public List<SequenceSelector> RecordedLateralSpreadingDeviceSequence_ { get { return Items.FindAll<Sequence>("300800F4").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedRangeModulatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300800F6")); } }
        public List<SequenceSelector> RecordedRangeModulatorSequence_ { get { return Items.FindAll<Sequence>("300800F6").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector RecordedSourceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080100")); } }
        public List<SequenceSelector> RecordedSourceSequence_ { get { return Items.FindAll<Sequence>("30080100").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString SourceSerialNumber { get { return Items.FindFirst<LongString>("30080105") as LongString; } }
        public List<LongString> SourceSerialNumber_ { get { return Items.FindAll<LongString>("30080105").ToList(); } }
        public SequenceSelector TreatmentSessionApplicationSetupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080110")); } }
        public List<SequenceSelector> TreatmentSessionApplicationSetupSequence_ { get { return Items.FindAll<Sequence>("30080110").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ApplicationSetupCheck { get { return Items.FindFirst<CodeString>("30080116") as CodeString; } }
        public List<CodeString> ApplicationSetupCheck_ { get { return Items.FindAll<CodeString>("30080116").ToList(); } }
        public SequenceSelector RecordedBrachyAccessoryDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080120")); } }
        public List<SequenceSelector> RecordedBrachyAccessoryDeviceSequence_ { get { return Items.FindAll<Sequence>("30080120").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedBrachyAccessoryDeviceNumber { get { return Items.FindFirst<IntegerString>("30080122") as IntegerString; } }
        public List<IntegerString> ReferencedBrachyAccessoryDeviceNumber_ { get { return Items.FindAll<IntegerString>("30080122").ToList(); } }
        public SequenceSelector RecordedChannelSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080130")); } }
        public List<SequenceSelector> RecordedChannelSequence_ { get { return Items.FindAll<Sequence>("30080130").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString SpecifiedChannelTotalTime { get { return Items.FindFirst<DecimalString>("30080132") as DecimalString; } }
        public List<DecimalString> SpecifiedChannelTotalTime_ { get { return Items.FindAll<DecimalString>("30080132").ToList(); } }
        public DecimalString DeliveredChannelTotalTime { get { return Items.FindFirst<DecimalString>("30080134") as DecimalString; } }
        public List<DecimalString> DeliveredChannelTotalTime_ { get { return Items.FindAll<DecimalString>("30080134").ToList(); } }
        public IntegerString SpecifiedNumberOfPulses { get { return Items.FindFirst<IntegerString>("30080136") as IntegerString; } }
        public List<IntegerString> SpecifiedNumberOfPulses_ { get { return Items.FindAll<IntegerString>("30080136").ToList(); } }
        public IntegerString DeliveredNumberOfPulses { get { return Items.FindFirst<IntegerString>("30080138") as IntegerString; } }
        public List<IntegerString> DeliveredNumberOfPulses_ { get { return Items.FindAll<IntegerString>("30080138").ToList(); } }
        public DecimalString SpecifiedPulseRepetitionInterval { get { return Items.FindFirst<DecimalString>("3008013A") as DecimalString; } }
        public List<DecimalString> SpecifiedPulseRepetitionInterval_ { get { return Items.FindAll<DecimalString>("3008013A").ToList(); } }
        public DecimalString DeliveredPulseRepetitionInterval { get { return Items.FindFirst<DecimalString>("3008013C") as DecimalString; } }
        public List<DecimalString> DeliveredPulseRepetitionInterval_ { get { return Items.FindAll<DecimalString>("3008013C").ToList(); } }
        public SequenceSelector RecordedSourceApplicatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080140")); } }
        public List<SequenceSelector> RecordedSourceApplicatorSequence_ { get { return Items.FindAll<Sequence>("30080140").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedSourceApplicatorNumber { get { return Items.FindFirst<IntegerString>("30080142") as IntegerString; } }
        public List<IntegerString> ReferencedSourceApplicatorNumber_ { get { return Items.FindAll<IntegerString>("30080142").ToList(); } }
        public SequenceSelector RecordedChannelShieldSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080150")); } }
        public List<SequenceSelector> RecordedChannelShieldSequence_ { get { return Items.FindAll<Sequence>("30080150").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedChannelShieldNumber { get { return Items.FindFirst<IntegerString>("30080152") as IntegerString; } }
        public List<IntegerString> ReferencedChannelShieldNumber_ { get { return Items.FindAll<IntegerString>("30080152").ToList(); } }
        public SequenceSelector BrachyControlPointDeliveredSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080160")); } }
        public List<SequenceSelector> BrachyControlPointDeliveredSequence_ { get { return Items.FindAll<Sequence>("30080160").Select(s => new SequenceSelector(s)).ToList(); } }
        public Date SafePositionExitDate { get { return Items.FindFirst<Date>("30080162") as Date; } }
        public List<Date> SafePositionExitDate_ { get { return Items.FindAll<Date>("30080162").ToList(); } }
        public Time SafePositionExitTime { get { return Items.FindFirst<Time>("30080164") as Time; } }
        public List<Time> SafePositionExitTime_ { get { return Items.FindAll<Time>("30080164").ToList(); } }
        public Date SafePositionReturnDate { get { return Items.FindFirst<Date>("30080166") as Date; } }
        public List<Date> SafePositionReturnDate_ { get { return Items.FindAll<Date>("30080166").ToList(); } }
        public Time SafePositionReturnTime { get { return Items.FindFirst<Time>("30080168") as Time; } }
        public List<Time> SafePositionReturnTime_ { get { return Items.FindAll<Time>("30080168").ToList(); } }
        public CodeString CurrentTreatmentStatus { get { return Items.FindFirst<CodeString>("30080200") as CodeString; } }
        public List<CodeString> CurrentTreatmentStatus_ { get { return Items.FindAll<CodeString>("30080200").ToList(); } }
        public ShortText TreatmentStatusComment { get { return Items.FindFirst<ShortText>("30080202") as ShortText; } }
        public List<ShortText> TreatmentStatusComment_ { get { return Items.FindAll<ShortText>("30080202").ToList(); } }
        public SequenceSelector FractionGroupSummarySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080220")); } }
        public List<SequenceSelector> FractionGroupSummarySequence_ { get { return Items.FindAll<Sequence>("30080220").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedFractionNumber { get { return Items.FindFirst<IntegerString>("30080223") as IntegerString; } }
        public List<IntegerString> ReferencedFractionNumber_ { get { return Items.FindAll<IntegerString>("30080223").ToList(); } }
        public CodeString FractionGroupType { get { return Items.FindFirst<CodeString>("30080224") as CodeString; } }
        public List<CodeString> FractionGroupType_ { get { return Items.FindAll<CodeString>("30080224").ToList(); } }
        public CodeString BeamStopperPosition { get { return Items.FindFirst<CodeString>("30080230") as CodeString; } }
        public List<CodeString> BeamStopperPosition_ { get { return Items.FindAll<CodeString>("30080230").ToList(); } }
        public SequenceSelector FractionStatusSummarySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("30080240")); } }
        public List<SequenceSelector> FractionStatusSummarySequence_ { get { return Items.FindAll<Sequence>("30080240").Select(s => new SequenceSelector(s)).ToList(); } }
        public Date TreatmentDate { get { return Items.FindFirst<Date>("30080250") as Date; } }
        public List<Date> TreatmentDate_ { get { return Items.FindAll<Date>("30080250").ToList(); } }
        public Time TreatmentTime { get { return Items.FindFirst<Time>("30080251") as Time; } }
        public List<Time> TreatmentTime_ { get { return Items.FindAll<Time>("30080251").ToList(); } }
        public ShortString RTPlanLabel { get { return Items.FindFirst<ShortString>("300A0002") as ShortString; } }
        public List<ShortString> RTPlanLabel_ { get { return Items.FindAll<ShortString>("300A0002").ToList(); } }
        public LongString RTPlanName { get { return Items.FindFirst<LongString>("300A0003") as LongString; } }
        public List<LongString> RTPlanName_ { get { return Items.FindAll<LongString>("300A0003").ToList(); } }
        public ShortText RTPlanDescription { get { return Items.FindFirst<ShortText>("300A0004") as ShortText; } }
        public List<ShortText> RTPlanDescription_ { get { return Items.FindAll<ShortText>("300A0004").ToList(); } }
        public Date RTPlanDate { get { return Items.FindFirst<Date>("300A0006") as Date; } }
        public List<Date> RTPlanDate_ { get { return Items.FindAll<Date>("300A0006").ToList(); } }
        public Time RTPlanTime { get { return Items.FindFirst<Time>("300A0007") as Time; } }
        public List<Time> RTPlanTime_ { get { return Items.FindAll<Time>("300A0007").ToList(); } }
        public LongString TreatmentProtocols { get { return Items.FindFirst<LongString>("300A0009") as LongString; } }
        public List<LongString> TreatmentProtocols_ { get { return Items.FindAll<LongString>("300A0009").ToList(); } }
        public CodeString PlanIntent { get { return Items.FindFirst<CodeString>("300A000A") as CodeString; } }
        public List<CodeString> PlanIntent_ { get { return Items.FindAll<CodeString>("300A000A").ToList(); } }
        public LongString TreatmentSites { get { return Items.FindFirst<LongString>("300A000B") as LongString; } }
        public List<LongString> TreatmentSites_ { get { return Items.FindAll<LongString>("300A000B").ToList(); } }
        public CodeString RTPlanGeometry { get { return Items.FindFirst<CodeString>("300A000C") as CodeString; } }
        public List<CodeString> RTPlanGeometry_ { get { return Items.FindAll<CodeString>("300A000C").ToList(); } }
        public ShortText PrescriptionDescription { get { return Items.FindFirst<ShortText>("300A000E") as ShortText; } }
        public List<ShortText> PrescriptionDescription_ { get { return Items.FindAll<ShortText>("300A000E").ToList(); } }
        public SequenceSelector DoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0010")); } }
        public List<SequenceSelector> DoseReferenceSequence_ { get { return Items.FindAll<Sequence>("300A0010").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString DoseReferenceNumber { get { return Items.FindFirst<IntegerString>("300A0012") as IntegerString; } }
        public List<IntegerString> DoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("300A0012").ToList(); } }
        public UniqueIdentifier DoseReferenceUID { get { return Items.FindFirst<UniqueIdentifier>("300A0013") as UniqueIdentifier; } }
        public List<UniqueIdentifier> DoseReferenceUID_ { get { return Items.FindAll<UniqueIdentifier>("300A0013").ToList(); } }
        public CodeString DoseReferenceStructureType { get { return Items.FindFirst<CodeString>("300A0014") as CodeString; } }
        public List<CodeString> DoseReferenceStructureType_ { get { return Items.FindAll<CodeString>("300A0014").ToList(); } }
        public CodeString NominalBeamEnergyUnit { get { return Items.FindFirst<CodeString>("300A0015") as CodeString; } }
        public List<CodeString> NominalBeamEnergyUnit_ { get { return Items.FindAll<CodeString>("300A0015").ToList(); } }
        public LongString DoseReferenceDescription { get { return Items.FindFirst<LongString>("300A0016") as LongString; } }
        public List<LongString> DoseReferenceDescription_ { get { return Items.FindAll<LongString>("300A0016").ToList(); } }
        public DecimalString DoseReferencePointCoordinates { get { return Items.FindFirst<DecimalString>("300A0018") as DecimalString; } }
        public List<DecimalString> DoseReferencePointCoordinates_ { get { return Items.FindAll<DecimalString>("300A0018").ToList(); } }
        public DecimalString NominalPriorDose { get { return Items.FindFirst<DecimalString>("300A001A") as DecimalString; } }
        public List<DecimalString> NominalPriorDose_ { get { return Items.FindAll<DecimalString>("300A001A").ToList(); } }
        public CodeString DoseReferenceType { get { return Items.FindFirst<CodeString>("300A0020") as CodeString; } }
        public List<CodeString> DoseReferenceType_ { get { return Items.FindAll<CodeString>("300A0020").ToList(); } }
        public DecimalString ConstraintWeight { get { return Items.FindFirst<DecimalString>("300A0021") as DecimalString; } }
        public List<DecimalString> ConstraintWeight_ { get { return Items.FindAll<DecimalString>("300A0021").ToList(); } }
        public DecimalString DeliveryWarningDose { get { return Items.FindFirst<DecimalString>("300A0022") as DecimalString; } }
        public List<DecimalString> DeliveryWarningDose_ { get { return Items.FindAll<DecimalString>("300A0022").ToList(); } }
        public DecimalString DeliveryMaximumDose { get { return Items.FindFirst<DecimalString>("300A0023") as DecimalString; } }
        public List<DecimalString> DeliveryMaximumDose_ { get { return Items.FindAll<DecimalString>("300A0023").ToList(); } }
        public DecimalString TargetMinimumDose { get { return Items.FindFirst<DecimalString>("300A0025") as DecimalString; } }
        public List<DecimalString> TargetMinimumDose_ { get { return Items.FindAll<DecimalString>("300A0025").ToList(); } }
        public DecimalString TargetPrescriptionDose { get { return Items.FindFirst<DecimalString>("300A0026") as DecimalString; } }
        public List<DecimalString> TargetPrescriptionDose_ { get { return Items.FindAll<DecimalString>("300A0026").ToList(); } }
        public DecimalString TargetMaximumDose { get { return Items.FindFirst<DecimalString>("300A0027") as DecimalString; } }
        public List<DecimalString> TargetMaximumDose_ { get { return Items.FindAll<DecimalString>("300A0027").ToList(); } }
        public DecimalString TargetUnderdoseVolumeFraction { get { return Items.FindFirst<DecimalString>("300A0028") as DecimalString; } }
        public List<DecimalString> TargetUnderdoseVolumeFraction_ { get { return Items.FindAll<DecimalString>("300A0028").ToList(); } }
        public DecimalString OrganAtRiskFullVolumeDose { get { return Items.FindFirst<DecimalString>("300A002A") as DecimalString; } }
        public List<DecimalString> OrganAtRiskFullVolumeDose_ { get { return Items.FindAll<DecimalString>("300A002A").ToList(); } }
        public DecimalString OrganAtRiskLimitDose { get { return Items.FindFirst<DecimalString>("300A002B") as DecimalString; } }
        public List<DecimalString> OrganAtRiskLimitDose_ { get { return Items.FindAll<DecimalString>("300A002B").ToList(); } }
        public DecimalString OrganAtRiskMaximumDose { get { return Items.FindFirst<DecimalString>("300A002C") as DecimalString; } }
        public List<DecimalString> OrganAtRiskMaximumDose_ { get { return Items.FindAll<DecimalString>("300A002C").ToList(); } }
        public DecimalString OrganAtRiskOverdoseVolumeFraction { get { return Items.FindFirst<DecimalString>("300A002D") as DecimalString; } }
        public List<DecimalString> OrganAtRiskOverdoseVolumeFraction_ { get { return Items.FindAll<DecimalString>("300A002D").ToList(); } }
        public SequenceSelector ToleranceTableSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0040")); } }
        public List<SequenceSelector> ToleranceTableSequence_ { get { return Items.FindAll<Sequence>("300A0040").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ToleranceTableNumber { get { return Items.FindFirst<IntegerString>("300A0042") as IntegerString; } }
        public List<IntegerString> ToleranceTableNumber_ { get { return Items.FindAll<IntegerString>("300A0042").ToList(); } }
        public ShortString ToleranceTableLabel { get { return Items.FindFirst<ShortString>("300A0043") as ShortString; } }
        public List<ShortString> ToleranceTableLabel_ { get { return Items.FindAll<ShortString>("300A0043").ToList(); } }
        public DecimalString GantryAngleTolerance { get { return Items.FindFirst<DecimalString>("300A0044") as DecimalString; } }
        public List<DecimalString> GantryAngleTolerance_ { get { return Items.FindAll<DecimalString>("300A0044").ToList(); } }
        public DecimalString BeamLimitingDeviceAngleTolerance { get { return Items.FindFirst<DecimalString>("300A0046") as DecimalString; } }
        public List<DecimalString> BeamLimitingDeviceAngleTolerance_ { get { return Items.FindAll<DecimalString>("300A0046").ToList(); } }
        public SequenceSelector BeamLimitingDeviceToleranceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0048")); } }
        public List<SequenceSelector> BeamLimitingDeviceToleranceSequence_ { get { return Items.FindAll<Sequence>("300A0048").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString BeamLimitingDevicePositionTolerance { get { return Items.FindFirst<DecimalString>("300A004A") as DecimalString; } }
        public List<DecimalString> BeamLimitingDevicePositionTolerance_ { get { return Items.FindAll<DecimalString>("300A004A").ToList(); } }
        public FloatingPointSingle SnoutPositionTolerance { get { return Items.FindFirst<FloatingPointSingle>("300A004B") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SnoutPositionTolerance_ { get { return Items.FindAll<FloatingPointSingle>("300A004B").ToList(); } }
        public DecimalString PatientSupportAngleTolerance { get { return Items.FindFirst<DecimalString>("300A004C") as DecimalString; } }
        public List<DecimalString> PatientSupportAngleTolerance_ { get { return Items.FindAll<DecimalString>("300A004C").ToList(); } }
        public DecimalString TableTopEccentricAngleTolerance { get { return Items.FindFirst<DecimalString>("300A004E") as DecimalString; } }
        public List<DecimalString> TableTopEccentricAngleTolerance_ { get { return Items.FindAll<DecimalString>("300A004E").ToList(); } }
        public FloatingPointSingle TableTopPitchAngleTolerance { get { return Items.FindFirst<FloatingPointSingle>("300A004F") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableTopPitchAngleTolerance_ { get { return Items.FindAll<FloatingPointSingle>("300A004F").ToList(); } }
        public FloatingPointSingle TableTopRollAngleTolerance { get { return Items.FindFirst<FloatingPointSingle>("300A0050") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableTopRollAngleTolerance_ { get { return Items.FindAll<FloatingPointSingle>("300A0050").ToList(); } }
        public DecimalString TableTopVerticalPositionTolerance { get { return Items.FindFirst<DecimalString>("300A0051") as DecimalString; } }
        public List<DecimalString> TableTopVerticalPositionTolerance_ { get { return Items.FindAll<DecimalString>("300A0051").ToList(); } }
        public DecimalString TableTopLongitudinalPositionTolerance { get { return Items.FindFirst<DecimalString>("300A0052") as DecimalString; } }
        public List<DecimalString> TableTopLongitudinalPositionTolerance_ { get { return Items.FindAll<DecimalString>("300A0052").ToList(); } }
        public DecimalString TableTopLateralPositionTolerance { get { return Items.FindFirst<DecimalString>("300A0053") as DecimalString; } }
        public List<DecimalString> TableTopLateralPositionTolerance_ { get { return Items.FindAll<DecimalString>("300A0053").ToList(); } }
        public CodeString RTPlanRelationship { get { return Items.FindFirst<CodeString>("300A0055") as CodeString; } }
        public List<CodeString> RTPlanRelationship_ { get { return Items.FindAll<CodeString>("300A0055").ToList(); } }
        public SequenceSelector FractionGroupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0070")); } }
        public List<SequenceSelector> FractionGroupSequence_ { get { return Items.FindAll<Sequence>("300A0070").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString FractionGroupNumber { get { return Items.FindFirst<IntegerString>("300A0071") as IntegerString; } }
        public List<IntegerString> FractionGroupNumber_ { get { return Items.FindAll<IntegerString>("300A0071").ToList(); } }
        public LongString FractionGroupDescription { get { return Items.FindFirst<LongString>("300A0072") as LongString; } }
        public List<LongString> FractionGroupDescription_ { get { return Items.FindAll<LongString>("300A0072").ToList(); } }
        public IntegerString NumberOfFractionsPlanned { get { return Items.FindFirst<IntegerString>("300A0078") as IntegerString; } }
        public List<IntegerString> NumberOfFractionsPlanned_ { get { return Items.FindAll<IntegerString>("300A0078").ToList(); } }
        public IntegerString NumberOfFractionPatternDigitsPerDay { get { return Items.FindFirst<IntegerString>("300A0079") as IntegerString; } }
        public List<IntegerString> NumberOfFractionPatternDigitsPerDay_ { get { return Items.FindAll<IntegerString>("300A0079").ToList(); } }
        public IntegerString RepeatFractionCycleLength { get { return Items.FindFirst<IntegerString>("300A007A") as IntegerString; } }
        public List<IntegerString> RepeatFractionCycleLength_ { get { return Items.FindAll<IntegerString>("300A007A").ToList(); } }
        public LongText FractionPattern { get { return Items.FindFirst<LongText>("300A007B") as LongText; } }
        public List<LongText> FractionPattern_ { get { return Items.FindAll<LongText>("300A007B").ToList(); } }
        public IntegerString NumberOfBeams { get { return Items.FindFirst<IntegerString>("300A0080") as IntegerString; } }
        public List<IntegerString> NumberOfBeams_ { get { return Items.FindAll<IntegerString>("300A0080").ToList(); } }
        public DecimalString BeamDoseSpecificationPoint { get { return Items.FindFirst<DecimalString>("300A0082") as DecimalString; } }
        public List<DecimalString> BeamDoseSpecificationPoint_ { get { return Items.FindAll<DecimalString>("300A0082").ToList(); } }
        public DecimalString BeamDose { get { return Items.FindFirst<DecimalString>("300A0084") as DecimalString; } }
        public List<DecimalString> BeamDose_ { get { return Items.FindAll<DecimalString>("300A0084").ToList(); } }
        public DecimalString BeamMeterset { get { return Items.FindFirst<DecimalString>("300A0086") as DecimalString; } }
        public List<DecimalString> BeamMeterset_ { get { return Items.FindAll<DecimalString>("300A0086").ToList(); } }
        public FloatingPointSingle BeamDosePointDepth { get { return Items.FindFirst<FloatingPointSingle>("300A0088") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BeamDosePointDepth_ { get { return Items.FindAll<FloatingPointSingle>("300A0088").ToList(); } }
        public FloatingPointSingle BeamDosePointEquivalentDepth { get { return Items.FindFirst<FloatingPointSingle>("300A0089") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BeamDosePointEquivalentDepth_ { get { return Items.FindAll<FloatingPointSingle>("300A0089").ToList(); } }
        public FloatingPointSingle BeamDosePointSSD { get { return Items.FindFirst<FloatingPointSingle>("300A008A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BeamDosePointSSD_ { get { return Items.FindAll<FloatingPointSingle>("300A008A").ToList(); } }
        public IntegerString NumberOfBrachyApplicationSetups { get { return Items.FindFirst<IntegerString>("300A00A0") as IntegerString; } }
        public List<IntegerString> NumberOfBrachyApplicationSetups_ { get { return Items.FindAll<IntegerString>("300A00A0").ToList(); } }
        public DecimalString BrachyApplicationSetupDoseSpecificationPoint { get { return Items.FindFirst<DecimalString>("300A00A2") as DecimalString; } }
        public List<DecimalString> BrachyApplicationSetupDoseSpecificationPoint_ { get { return Items.FindAll<DecimalString>("300A00A2").ToList(); } }
        public DecimalString BrachyApplicationSetupDose { get { return Items.FindFirst<DecimalString>("300A00A4") as DecimalString; } }
        public List<DecimalString> BrachyApplicationSetupDose_ { get { return Items.FindAll<DecimalString>("300A00A4").ToList(); } }
        public SequenceSelector BeamSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00B0")); } }
        public List<SequenceSelector> BeamSequence_ { get { return Items.FindAll<Sequence>("300A00B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString TreatmentMachineName { get { return Items.FindFirst<ShortString>("300A00B2") as ShortString; } }
        public List<ShortString> TreatmentMachineName_ { get { return Items.FindAll<ShortString>("300A00B2").ToList(); } }
        public CodeString PrimaryDosimeterUnit { get { return Items.FindFirst<CodeString>("300A00B3") as CodeString; } }
        public List<CodeString> PrimaryDosimeterUnit_ { get { return Items.FindAll<CodeString>("300A00B3").ToList(); } }
        public DecimalString SourceAxisDistance { get { return Items.FindFirst<DecimalString>("300A00B4") as DecimalString; } }
        public List<DecimalString> SourceAxisDistance_ { get { return Items.FindAll<DecimalString>("300A00B4").ToList(); } }
        public SequenceSelector BeamLimitingDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00B6")); } }
        public List<SequenceSelector> BeamLimitingDeviceSequence_ { get { return Items.FindAll<Sequence>("300A00B6").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString RTBeamLimitingDeviceType { get { return Items.FindFirst<CodeString>("300A00B8") as CodeString; } }
        public List<CodeString> RTBeamLimitingDeviceType_ { get { return Items.FindAll<CodeString>("300A00B8").ToList(); } }
        public DecimalString SourceToBeamLimitingDeviceDistance { get { return Items.FindFirst<DecimalString>("300A00BA") as DecimalString; } }
        public List<DecimalString> SourceToBeamLimitingDeviceDistance_ { get { return Items.FindAll<DecimalString>("300A00BA").ToList(); } }
        public FloatingPointSingle IsocenterToBeamLimitingDeviceDistance { get { return Items.FindFirst<FloatingPointSingle>("300A00BB") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToBeamLimitingDeviceDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A00BB").ToList(); } }
        public IntegerString NumberOfLeafJawPairs { get { return Items.FindFirst<IntegerString>("300A00BC") as IntegerString; } }
        public List<IntegerString> NumberOfLeafJawPairs_ { get { return Items.FindAll<IntegerString>("300A00BC").ToList(); } }
        public DecimalString LeafPositionBoundaries { get { return Items.FindFirst<DecimalString>("300A00BE") as DecimalString; } }
        public List<DecimalString> LeafPositionBoundaries_ { get { return Items.FindAll<DecimalString>("300A00BE").ToList(); } }
        public IntegerString BeamNumber { get { return Items.FindFirst<IntegerString>("300A00C0") as IntegerString; } }
        public List<IntegerString> BeamNumber_ { get { return Items.FindAll<IntegerString>("300A00C0").ToList(); } }
        public LongString BeamName { get { return Items.FindFirst<LongString>("300A00C2") as LongString; } }
        public List<LongString> BeamName_ { get { return Items.FindAll<LongString>("300A00C2").ToList(); } }
        public ShortText BeamDescription { get { return Items.FindFirst<ShortText>("300A00C3") as ShortText; } }
        public List<ShortText> BeamDescription_ { get { return Items.FindAll<ShortText>("300A00C3").ToList(); } }
        public CodeString BeamType { get { return Items.FindFirst<CodeString>("300A00C4") as CodeString; } }
        public List<CodeString> BeamType_ { get { return Items.FindAll<CodeString>("300A00C4").ToList(); } }
        public CodeString RadiationType { get { return Items.FindFirst<CodeString>("300A00C6") as CodeString; } }
        public List<CodeString> RadiationType_ { get { return Items.FindAll<CodeString>("300A00C6").ToList(); } }
        public CodeString HighDoseTechniqueType { get { return Items.FindFirst<CodeString>("300A00C7") as CodeString; } }
        public List<CodeString> HighDoseTechniqueType_ { get { return Items.FindAll<CodeString>("300A00C7").ToList(); } }
        public IntegerString ReferenceImageNumber { get { return Items.FindFirst<IntegerString>("300A00C8") as IntegerString; } }
        public List<IntegerString> ReferenceImageNumber_ { get { return Items.FindAll<IntegerString>("300A00C8").ToList(); } }
        public SequenceSelector PlannedVerificationImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00CA")); } }
        public List<SequenceSelector> PlannedVerificationImageSequence_ { get { return Items.FindAll<Sequence>("300A00CA").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString ImagingDeviceSpecificAcquisitionParameters { get { return Items.FindFirst<LongString>("300A00CC") as LongString; } }
        public List<LongString> ImagingDeviceSpecificAcquisitionParameters_ { get { return Items.FindAll<LongString>("300A00CC").ToList(); } }
        public CodeString TreatmentDeliveryType { get { return Items.FindFirst<CodeString>("300A00CE") as CodeString; } }
        public List<CodeString> TreatmentDeliveryType_ { get { return Items.FindAll<CodeString>("300A00CE").ToList(); } }
        public IntegerString NumberOfWedges { get { return Items.FindFirst<IntegerString>("300A00D0") as IntegerString; } }
        public List<IntegerString> NumberOfWedges_ { get { return Items.FindAll<IntegerString>("300A00D0").ToList(); } }
        public SequenceSelector WedgeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00D1")); } }
        public List<SequenceSelector> WedgeSequence_ { get { return Items.FindAll<Sequence>("300A00D1").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString WedgeNumber { get { return Items.FindFirst<IntegerString>("300A00D2") as IntegerString; } }
        public List<IntegerString> WedgeNumber_ { get { return Items.FindAll<IntegerString>("300A00D2").ToList(); } }
        public CodeString WedgeType { get { return Items.FindFirst<CodeString>("300A00D3") as CodeString; } }
        public List<CodeString> WedgeType_ { get { return Items.FindAll<CodeString>("300A00D3").ToList(); } }
        public ShortString WedgeID { get { return Items.FindFirst<ShortString>("300A00D4") as ShortString; } }
        public List<ShortString> WedgeID_ { get { return Items.FindAll<ShortString>("300A00D4").ToList(); } }
        public IntegerString WedgeAngle { get { return Items.FindFirst<IntegerString>("300A00D5") as IntegerString; } }
        public List<IntegerString> WedgeAngle_ { get { return Items.FindAll<IntegerString>("300A00D5").ToList(); } }
        public DecimalString WedgeFactor { get { return Items.FindFirst<DecimalString>("300A00D6") as DecimalString; } }
        public List<DecimalString> WedgeFactor_ { get { return Items.FindAll<DecimalString>("300A00D6").ToList(); } }
        public FloatingPointSingle TotalWedgeTrayWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A00D7") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TotalWedgeTrayWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A00D7").ToList(); } }
        public DecimalString WedgeOrientation { get { return Items.FindFirst<DecimalString>("300A00D8") as DecimalString; } }
        public List<DecimalString> WedgeOrientation_ { get { return Items.FindAll<DecimalString>("300A00D8").ToList(); } }
        public FloatingPointSingle IsocenterToWedgeTrayDistance { get { return Items.FindFirst<FloatingPointSingle>("300A00D9") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToWedgeTrayDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A00D9").ToList(); } }
        public DecimalString SourceToWedgeTrayDistance { get { return Items.FindFirst<DecimalString>("300A00DA") as DecimalString; } }
        public List<DecimalString> SourceToWedgeTrayDistance_ { get { return Items.FindAll<DecimalString>("300A00DA").ToList(); } }
        public FloatingPointSingle WedgeThinEdgePosition { get { return Items.FindFirst<FloatingPointSingle>("300A00DB") as FloatingPointSingle; } }
        public List<FloatingPointSingle> WedgeThinEdgePosition_ { get { return Items.FindAll<FloatingPointSingle>("300A00DB").ToList(); } }
        public ShortString BolusID { get { return Items.FindFirst<ShortString>("300A00DC") as ShortString; } }
        public List<ShortString> BolusID_ { get { return Items.FindAll<ShortString>("300A00DC").ToList(); } }
        public ShortText BolusDescription { get { return Items.FindFirst<ShortText>("300A00DD") as ShortText; } }
        public List<ShortText> BolusDescription_ { get { return Items.FindAll<ShortText>("300A00DD").ToList(); } }
        public IntegerString NumberOfCompensators { get { return Items.FindFirst<IntegerString>("300A00E0") as IntegerString; } }
        public List<IntegerString> NumberOfCompensators_ { get { return Items.FindAll<IntegerString>("300A00E0").ToList(); } }
        public ShortString MaterialID { get { return Items.FindFirst<ShortString>("300A00E1") as ShortString; } }
        public List<ShortString> MaterialID_ { get { return Items.FindAll<ShortString>("300A00E1").ToList(); } }
        public DecimalString TotalCompensatorTrayFactor { get { return Items.FindFirst<DecimalString>("300A00E2") as DecimalString; } }
        public List<DecimalString> TotalCompensatorTrayFactor_ { get { return Items.FindAll<DecimalString>("300A00E2").ToList(); } }
        public SequenceSelector CompensatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00E3")); } }
        public List<SequenceSelector> CompensatorSequence_ { get { return Items.FindAll<Sequence>("300A00E3").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString CompensatorNumber { get { return Items.FindFirst<IntegerString>("300A00E4") as IntegerString; } }
        public List<IntegerString> CompensatorNumber_ { get { return Items.FindAll<IntegerString>("300A00E4").ToList(); } }
        public ShortString CompensatorID { get { return Items.FindFirst<ShortString>("300A00E5") as ShortString; } }
        public List<ShortString> CompensatorID_ { get { return Items.FindAll<ShortString>("300A00E5").ToList(); } }
        public DecimalString SourceToCompensatorTrayDistance { get { return Items.FindFirst<DecimalString>("300A00E6") as DecimalString; } }
        public List<DecimalString> SourceToCompensatorTrayDistance_ { get { return Items.FindAll<DecimalString>("300A00E6").ToList(); } }
        public IntegerString CompensatorRows { get { return Items.FindFirst<IntegerString>("300A00E7") as IntegerString; } }
        public List<IntegerString> CompensatorRows_ { get { return Items.FindAll<IntegerString>("300A00E7").ToList(); } }
        public IntegerString CompensatorColumns { get { return Items.FindFirst<IntegerString>("300A00E8") as IntegerString; } }
        public List<IntegerString> CompensatorColumns_ { get { return Items.FindAll<IntegerString>("300A00E8").ToList(); } }
        public DecimalString CompensatorPixelSpacing { get { return Items.FindFirst<DecimalString>("300A00E9") as DecimalString; } }
        public List<DecimalString> CompensatorPixelSpacing_ { get { return Items.FindAll<DecimalString>("300A00E9").ToList(); } }
        public DecimalString CompensatorPosition { get { return Items.FindFirst<DecimalString>("300A00EA") as DecimalString; } }
        public List<DecimalString> CompensatorPosition_ { get { return Items.FindAll<DecimalString>("300A00EA").ToList(); } }
        public DecimalString CompensatorTransmissionData { get { return Items.FindFirst<DecimalString>("300A00EB") as DecimalString; } }
        public List<DecimalString> CompensatorTransmissionData_ { get { return Items.FindAll<DecimalString>("300A00EB").ToList(); } }
        public DecimalString CompensatorThicknessData { get { return Items.FindFirst<DecimalString>("300A00EC") as DecimalString; } }
        public List<DecimalString> CompensatorThicknessData_ { get { return Items.FindAll<DecimalString>("300A00EC").ToList(); } }
        public IntegerString NumberOfBoli { get { return Items.FindFirst<IntegerString>("300A00ED") as IntegerString; } }
        public List<IntegerString> NumberOfBoli_ { get { return Items.FindAll<IntegerString>("300A00ED").ToList(); } }
        public CodeString CompensatorType { get { return Items.FindFirst<CodeString>("300A00EE") as CodeString; } }
        public List<CodeString> CompensatorType_ { get { return Items.FindAll<CodeString>("300A00EE").ToList(); } }
        public IntegerString NumberOfBlocks { get { return Items.FindFirst<IntegerString>("300A00F0") as IntegerString; } }
        public List<IntegerString> NumberOfBlocks_ { get { return Items.FindAll<IntegerString>("300A00F0").ToList(); } }
        public DecimalString TotalBlockTrayFactor { get { return Items.FindFirst<DecimalString>("300A00F2") as DecimalString; } }
        public List<DecimalString> TotalBlockTrayFactor_ { get { return Items.FindAll<DecimalString>("300A00F2").ToList(); } }
        public FloatingPointSingle TotalBlockTrayWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A00F3") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TotalBlockTrayWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A00F3").ToList(); } }
        public SequenceSelector BlockSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A00F4")); } }
        public List<SequenceSelector> BlockSequence_ { get { return Items.FindAll<Sequence>("300A00F4").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString BlockTrayID { get { return Items.FindFirst<ShortString>("300A00F5") as ShortString; } }
        public List<ShortString> BlockTrayID_ { get { return Items.FindAll<ShortString>("300A00F5").ToList(); } }
        public DecimalString SourceToBlockTrayDistance { get { return Items.FindFirst<DecimalString>("300A00F6") as DecimalString; } }
        public List<DecimalString> SourceToBlockTrayDistance_ { get { return Items.FindAll<DecimalString>("300A00F6").ToList(); } }
        public FloatingPointSingle IsocenterToBlockTrayDistance { get { return Items.FindFirst<FloatingPointSingle>("300A00F7") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToBlockTrayDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A00F7").ToList(); } }
        public CodeString BlockType { get { return Items.FindFirst<CodeString>("300A00F8") as CodeString; } }
        public List<CodeString> BlockType_ { get { return Items.FindAll<CodeString>("300A00F8").ToList(); } }
        public LongString AccessoryCode { get { return Items.FindFirst<LongString>("300A00F9") as LongString; } }
        public List<LongString> AccessoryCode_ { get { return Items.FindAll<LongString>("300A00F9").ToList(); } }
        public CodeString BlockDivergence { get { return Items.FindFirst<CodeString>("300A00FA") as CodeString; } }
        public List<CodeString> BlockDivergence_ { get { return Items.FindAll<CodeString>("300A00FA").ToList(); } }
        public CodeString BlockMountingPosition { get { return Items.FindFirst<CodeString>("300A00FB") as CodeString; } }
        public List<CodeString> BlockMountingPosition_ { get { return Items.FindAll<CodeString>("300A00FB").ToList(); } }
        public IntegerString BlockNumber { get { return Items.FindFirst<IntegerString>("300A00FC") as IntegerString; } }
        public List<IntegerString> BlockNumber_ { get { return Items.FindAll<IntegerString>("300A00FC").ToList(); } }
        public LongString BlockName { get { return Items.FindFirst<LongString>("300A00FE") as LongString; } }
        public List<LongString> BlockName_ { get { return Items.FindAll<LongString>("300A00FE").ToList(); } }
        public DecimalString BlockThickness { get { return Items.FindFirst<DecimalString>("300A0100") as DecimalString; } }
        public List<DecimalString> BlockThickness_ { get { return Items.FindAll<DecimalString>("300A0100").ToList(); } }
        public DecimalString BlockTransmission { get { return Items.FindFirst<DecimalString>("300A0102") as DecimalString; } }
        public List<DecimalString> BlockTransmission_ { get { return Items.FindAll<DecimalString>("300A0102").ToList(); } }
        public IntegerString BlockNumberOfPoints { get { return Items.FindFirst<IntegerString>("300A0104") as IntegerString; } }
        public List<IntegerString> BlockNumberOfPoints_ { get { return Items.FindAll<IntegerString>("300A0104").ToList(); } }
        public DecimalString BlockData { get { return Items.FindFirst<DecimalString>("300A0106") as DecimalString; } }
        public List<DecimalString> BlockData_ { get { return Items.FindAll<DecimalString>("300A0106").ToList(); } }
        public SequenceSelector ApplicatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0107")); } }
        public List<SequenceSelector> ApplicatorSequence_ { get { return Items.FindAll<Sequence>("300A0107").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString ApplicatorID { get { return Items.FindFirst<ShortString>("300A0108") as ShortString; } }
        public List<ShortString> ApplicatorID_ { get { return Items.FindAll<ShortString>("300A0108").ToList(); } }
        public CodeString ApplicatorType { get { return Items.FindFirst<CodeString>("300A0109") as CodeString; } }
        public List<CodeString> ApplicatorType_ { get { return Items.FindAll<CodeString>("300A0109").ToList(); } }
        public LongString ApplicatorDescription { get { return Items.FindFirst<LongString>("300A010A") as LongString; } }
        public List<LongString> ApplicatorDescription_ { get { return Items.FindAll<LongString>("300A010A").ToList(); } }
        public DecimalString CumulativeDoseReferenceCoefficient { get { return Items.FindFirst<DecimalString>("300A010C") as DecimalString; } }
        public List<DecimalString> CumulativeDoseReferenceCoefficient_ { get { return Items.FindAll<DecimalString>("300A010C").ToList(); } }
        public DecimalString FinalCumulativeMetersetWeight { get { return Items.FindFirst<DecimalString>("300A010E") as DecimalString; } }
        public List<DecimalString> FinalCumulativeMetersetWeight_ { get { return Items.FindAll<DecimalString>("300A010E").ToList(); } }
        public IntegerString NumberOfControlPoints { get { return Items.FindFirst<IntegerString>("300A0110") as IntegerString; } }
        public List<IntegerString> NumberOfControlPoints_ { get { return Items.FindAll<IntegerString>("300A0110").ToList(); } }
        public SequenceSelector ControlPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0111")); } }
        public List<SequenceSelector> ControlPointSequence_ { get { return Items.FindAll<Sequence>("300A0111").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ControlPointIndex { get { return Items.FindFirst<IntegerString>("300A0112") as IntegerString; } }
        public List<IntegerString> ControlPointIndex_ { get { return Items.FindAll<IntegerString>("300A0112").ToList(); } }
        public DecimalString NominalBeamEnergy { get { return Items.FindFirst<DecimalString>("300A0114") as DecimalString; } }
        public List<DecimalString> NominalBeamEnergy_ { get { return Items.FindAll<DecimalString>("300A0114").ToList(); } }
        public DecimalString DoseRateSet { get { return Items.FindFirst<DecimalString>("300A0115") as DecimalString; } }
        public List<DecimalString> DoseRateSet_ { get { return Items.FindAll<DecimalString>("300A0115").ToList(); } }
        public SequenceSelector WedgePositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0116")); } }
        public List<SequenceSelector> WedgePositionSequence_ { get { return Items.FindAll<Sequence>("300A0116").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString WedgePosition { get { return Items.FindFirst<CodeString>("300A0118") as CodeString; } }
        public List<CodeString> WedgePosition_ { get { return Items.FindAll<CodeString>("300A0118").ToList(); } }
        public SequenceSelector BeamLimitingDevicePositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A011A")); } }
        public List<SequenceSelector> BeamLimitingDevicePositionSequence_ { get { return Items.FindAll<Sequence>("300A011A").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString LeafJawPositions { get { return Items.FindFirst<DecimalString>("300A011C") as DecimalString; } }
        public List<DecimalString> LeafJawPositions_ { get { return Items.FindAll<DecimalString>("300A011C").ToList(); } }
        public DecimalString GantryAngle { get { return Items.FindFirst<DecimalString>("300A011E") as DecimalString; } }
        public List<DecimalString> GantryAngle_ { get { return Items.FindAll<DecimalString>("300A011E").ToList(); } }
        public CodeString GantryRotationDirection { get { return Items.FindFirst<CodeString>("300A011F") as CodeString; } }
        public List<CodeString> GantryRotationDirection_ { get { return Items.FindAll<CodeString>("300A011F").ToList(); } }
        public DecimalString BeamLimitingDeviceAngle { get { return Items.FindFirst<DecimalString>("300A0120") as DecimalString; } }
        public List<DecimalString> BeamLimitingDeviceAngle_ { get { return Items.FindAll<DecimalString>("300A0120").ToList(); } }
        public CodeString BeamLimitingDeviceRotationDirection { get { return Items.FindFirst<CodeString>("300A0121") as CodeString; } }
        public List<CodeString> BeamLimitingDeviceRotationDirection_ { get { return Items.FindAll<CodeString>("300A0121").ToList(); } }
        public DecimalString PatientSupportAngle { get { return Items.FindFirst<DecimalString>("300A0122") as DecimalString; } }
        public List<DecimalString> PatientSupportAngle_ { get { return Items.FindAll<DecimalString>("300A0122").ToList(); } }
        public CodeString PatientSupportRotationDirection { get { return Items.FindFirst<CodeString>("300A0123") as CodeString; } }
        public List<CodeString> PatientSupportRotationDirection_ { get { return Items.FindAll<CodeString>("300A0123").ToList(); } }
        public DecimalString TableTopEccentricAxisDistance { get { return Items.FindFirst<DecimalString>("300A0124") as DecimalString; } }
        public List<DecimalString> TableTopEccentricAxisDistance_ { get { return Items.FindAll<DecimalString>("300A0124").ToList(); } }
        public DecimalString TableTopEccentricAngle { get { return Items.FindFirst<DecimalString>("300A0125") as DecimalString; } }
        public List<DecimalString> TableTopEccentricAngle_ { get { return Items.FindAll<DecimalString>("300A0125").ToList(); } }
        public CodeString TableTopEccentricRotationDirection { get { return Items.FindFirst<CodeString>("300A0126") as CodeString; } }
        public List<CodeString> TableTopEccentricRotationDirection_ { get { return Items.FindAll<CodeString>("300A0126").ToList(); } }
        public DecimalString TableTopVerticalPosition { get { return Items.FindFirst<DecimalString>("300A0128") as DecimalString; } }
        public List<DecimalString> TableTopVerticalPosition_ { get { return Items.FindAll<DecimalString>("300A0128").ToList(); } }
        public DecimalString TableTopLongitudinalPosition { get { return Items.FindFirst<DecimalString>("300A0129") as DecimalString; } }
        public List<DecimalString> TableTopLongitudinalPosition_ { get { return Items.FindAll<DecimalString>("300A0129").ToList(); } }
        public DecimalString TableTopLateralPosition { get { return Items.FindFirst<DecimalString>("300A012A") as DecimalString; } }
        public List<DecimalString> TableTopLateralPosition_ { get { return Items.FindAll<DecimalString>("300A012A").ToList(); } }
        public DecimalString IsocenterPosition { get { return Items.FindFirst<DecimalString>("300A012C") as DecimalString; } }
        public List<DecimalString> IsocenterPosition_ { get { return Items.FindAll<DecimalString>("300A012C").ToList(); } }
        public DecimalString SurfaceEntryPoint { get { return Items.FindFirst<DecimalString>("300A012E") as DecimalString; } }
        public List<DecimalString> SurfaceEntryPoint_ { get { return Items.FindAll<DecimalString>("300A012E").ToList(); } }
        public DecimalString SourceToSurfaceDistance { get { return Items.FindFirst<DecimalString>("300A0130") as DecimalString; } }
        public List<DecimalString> SourceToSurfaceDistance_ { get { return Items.FindAll<DecimalString>("300A0130").ToList(); } }
        public DecimalString CumulativeMetersetWeight { get { return Items.FindFirst<DecimalString>("300A0134") as DecimalString; } }
        public List<DecimalString> CumulativeMetersetWeight_ { get { return Items.FindAll<DecimalString>("300A0134").ToList(); } }
        public FloatingPointSingle TableTopPitchAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0140") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableTopPitchAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0140").ToList(); } }
        public CodeString TableTopPitchRotationDirection { get { return Items.FindFirst<CodeString>("300A0142") as CodeString; } }
        public List<CodeString> TableTopPitchRotationDirection_ { get { return Items.FindAll<CodeString>("300A0142").ToList(); } }
        public FloatingPointSingle TableTopRollAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0144") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TableTopRollAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0144").ToList(); } }
        public CodeString TableTopRollRotationDirection { get { return Items.FindFirst<CodeString>("300A0146") as CodeString; } }
        public List<CodeString> TableTopRollRotationDirection_ { get { return Items.FindAll<CodeString>("300A0146").ToList(); } }
        public FloatingPointSingle HeadFixationAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0148") as FloatingPointSingle; } }
        public List<FloatingPointSingle> HeadFixationAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0148").ToList(); } }
        public FloatingPointSingle GantryPitchAngle { get { return Items.FindFirst<FloatingPointSingle>("300A014A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GantryPitchAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A014A").ToList(); } }
        public CodeString GantryPitchRotationDirection { get { return Items.FindFirst<CodeString>("300A014C") as CodeString; } }
        public List<CodeString> GantryPitchRotationDirection_ { get { return Items.FindAll<CodeString>("300A014C").ToList(); } }
        public FloatingPointSingle GantryPitchAngleTolerance { get { return Items.FindFirst<FloatingPointSingle>("300A014E") as FloatingPointSingle; } }
        public List<FloatingPointSingle> GantryPitchAngleTolerance_ { get { return Items.FindAll<FloatingPointSingle>("300A014E").ToList(); } }
        public SequenceSelector PatientSetupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0180")); } }
        public List<SequenceSelector> PatientSetupSequence_ { get { return Items.FindAll<Sequence>("300A0180").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString PatientSetupNumber { get { return Items.FindFirst<IntegerString>("300A0182") as IntegerString; } }
        public List<IntegerString> PatientSetupNumber_ { get { return Items.FindAll<IntegerString>("300A0182").ToList(); } }
        public LongString PatientSetupLabel { get { return Items.FindFirst<LongString>("300A0183") as LongString; } }
        public List<LongString> PatientSetupLabel_ { get { return Items.FindAll<LongString>("300A0183").ToList(); } }
        public LongString PatientAdditionalPosition { get { return Items.FindFirst<LongString>("300A0184") as LongString; } }
        public List<LongString> PatientAdditionalPosition_ { get { return Items.FindAll<LongString>("300A0184").ToList(); } }
        public SequenceSelector FixationDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0190")); } }
        public List<SequenceSelector> FixationDeviceSequence_ { get { return Items.FindAll<Sequence>("300A0190").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString FixationDeviceType { get { return Items.FindFirst<CodeString>("300A0192") as CodeString; } }
        public List<CodeString> FixationDeviceType_ { get { return Items.FindAll<CodeString>("300A0192").ToList(); } }
        public ShortString FixationDeviceLabel { get { return Items.FindFirst<ShortString>("300A0194") as ShortString; } }
        public List<ShortString> FixationDeviceLabel_ { get { return Items.FindAll<ShortString>("300A0194").ToList(); } }
        public ShortText FixationDeviceDescription { get { return Items.FindFirst<ShortText>("300A0196") as ShortText; } }
        public List<ShortText> FixationDeviceDescription_ { get { return Items.FindAll<ShortText>("300A0196").ToList(); } }
        public ShortString FixationDevicePosition { get { return Items.FindFirst<ShortString>("300A0198") as ShortString; } }
        public List<ShortString> FixationDevicePosition_ { get { return Items.FindAll<ShortString>("300A0198").ToList(); } }
        public FloatingPointSingle FixationDevicePitchAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0199") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FixationDevicePitchAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0199").ToList(); } }
        public FloatingPointSingle FixationDeviceRollAngle { get { return Items.FindFirst<FloatingPointSingle>("300A019A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FixationDeviceRollAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A019A").ToList(); } }
        public SequenceSelector ShieldingDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A01A0")); } }
        public List<SequenceSelector> ShieldingDeviceSequence_ { get { return Items.FindAll<Sequence>("300A01A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ShieldingDeviceType { get { return Items.FindFirst<CodeString>("300A01A2") as CodeString; } }
        public List<CodeString> ShieldingDeviceType_ { get { return Items.FindAll<CodeString>("300A01A2").ToList(); } }
        public ShortString ShieldingDeviceLabel { get { return Items.FindFirst<ShortString>("300A01A4") as ShortString; } }
        public List<ShortString> ShieldingDeviceLabel_ { get { return Items.FindAll<ShortString>("300A01A4").ToList(); } }
        public ShortText ShieldingDeviceDescription { get { return Items.FindFirst<ShortText>("300A01A6") as ShortText; } }
        public List<ShortText> ShieldingDeviceDescription_ { get { return Items.FindAll<ShortText>("300A01A6").ToList(); } }
        public ShortString ShieldingDevicePosition { get { return Items.FindFirst<ShortString>("300A01A8") as ShortString; } }
        public List<ShortString> ShieldingDevicePosition_ { get { return Items.FindAll<ShortString>("300A01A8").ToList(); } }
        public CodeString SetupTechnique { get { return Items.FindFirst<CodeString>("300A01B0") as CodeString; } }
        public List<CodeString> SetupTechnique_ { get { return Items.FindAll<CodeString>("300A01B0").ToList(); } }
        public ShortText SetupTechniqueDescription { get { return Items.FindFirst<ShortText>("300A01B2") as ShortText; } }
        public List<ShortText> SetupTechniqueDescription_ { get { return Items.FindAll<ShortText>("300A01B2").ToList(); } }
        public SequenceSelector SetupDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A01B4")); } }
        public List<SequenceSelector> SetupDeviceSequence_ { get { return Items.FindAll<Sequence>("300A01B4").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString SetupDeviceType { get { return Items.FindFirst<CodeString>("300A01B6") as CodeString; } }
        public List<CodeString> SetupDeviceType_ { get { return Items.FindAll<CodeString>("300A01B6").ToList(); } }
        public ShortString SetupDeviceLabel { get { return Items.FindFirst<ShortString>("300A01B8") as ShortString; } }
        public List<ShortString> SetupDeviceLabel_ { get { return Items.FindAll<ShortString>("300A01B8").ToList(); } }
        public ShortText SetupDeviceDescription { get { return Items.FindFirst<ShortText>("300A01BA") as ShortText; } }
        public List<ShortText> SetupDeviceDescription_ { get { return Items.FindAll<ShortText>("300A01BA").ToList(); } }
        public DecimalString SetupDeviceParameter { get { return Items.FindFirst<DecimalString>("300A01BC") as DecimalString; } }
        public List<DecimalString> SetupDeviceParameter_ { get { return Items.FindAll<DecimalString>("300A01BC").ToList(); } }
        public ShortText SetupReferenceDescription { get { return Items.FindFirst<ShortText>("300A01D0") as ShortText; } }
        public List<ShortText> SetupReferenceDescription_ { get { return Items.FindAll<ShortText>("300A01D0").ToList(); } }
        public DecimalString TableTopVerticalSetupDisplacement { get { return Items.FindFirst<DecimalString>("300A01D2") as DecimalString; } }
        public List<DecimalString> TableTopVerticalSetupDisplacement_ { get { return Items.FindAll<DecimalString>("300A01D2").ToList(); } }
        public DecimalString TableTopLongitudinalSetupDisplacement { get { return Items.FindFirst<DecimalString>("300A01D4") as DecimalString; } }
        public List<DecimalString> TableTopLongitudinalSetupDisplacement_ { get { return Items.FindAll<DecimalString>("300A01D4").ToList(); } }
        public DecimalString TableTopLateralSetupDisplacement { get { return Items.FindFirst<DecimalString>("300A01D6") as DecimalString; } }
        public List<DecimalString> TableTopLateralSetupDisplacement_ { get { return Items.FindAll<DecimalString>("300A01D6").ToList(); } }
        public CodeString BrachyTreatmentTechnique { get { return Items.FindFirst<CodeString>("300A0200") as CodeString; } }
        public List<CodeString> BrachyTreatmentTechnique_ { get { return Items.FindAll<CodeString>("300A0200").ToList(); } }
        public CodeString BrachyTreatmentType { get { return Items.FindFirst<CodeString>("300A0202") as CodeString; } }
        public List<CodeString> BrachyTreatmentType_ { get { return Items.FindAll<CodeString>("300A0202").ToList(); } }
        public SequenceSelector TreatmentMachineSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0206")); } }
        public List<SequenceSelector> TreatmentMachineSequence_ { get { return Items.FindAll<Sequence>("300A0206").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector SourceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0210")); } }
        public List<SequenceSelector> SourceSequence_ { get { return Items.FindAll<Sequence>("300A0210").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString SourceNumber { get { return Items.FindFirst<IntegerString>("300A0212") as IntegerString; } }
        public List<IntegerString> SourceNumber_ { get { return Items.FindAll<IntegerString>("300A0212").ToList(); } }
        public CodeString SourceType { get { return Items.FindFirst<CodeString>("300A0214") as CodeString; } }
        public List<CodeString> SourceType_ { get { return Items.FindAll<CodeString>("300A0214").ToList(); } }
        public LongString SourceManufacturer { get { return Items.FindFirst<LongString>("300A0216") as LongString; } }
        public List<LongString> SourceManufacturer_ { get { return Items.FindAll<LongString>("300A0216").ToList(); } }
        public DecimalString ActiveSourceDiameter { get { return Items.FindFirst<DecimalString>("300A0218") as DecimalString; } }
        public List<DecimalString> ActiveSourceDiameter_ { get { return Items.FindAll<DecimalString>("300A0218").ToList(); } }
        public DecimalString ActiveSourceLength { get { return Items.FindFirst<DecimalString>("300A021A") as DecimalString; } }
        public List<DecimalString> ActiveSourceLength_ { get { return Items.FindAll<DecimalString>("300A021A").ToList(); } }
        public DecimalString SourceEncapsulationNominalThickness { get { return Items.FindFirst<DecimalString>("300A0222") as DecimalString; } }
        public List<DecimalString> SourceEncapsulationNominalThickness_ { get { return Items.FindAll<DecimalString>("300A0222").ToList(); } }
        public DecimalString SourceEncapsulationNominalTransmission { get { return Items.FindFirst<DecimalString>("300A0224") as DecimalString; } }
        public List<DecimalString> SourceEncapsulationNominalTransmission_ { get { return Items.FindAll<DecimalString>("300A0224").ToList(); } }
        public LongString SourceIsotopeName { get { return Items.FindFirst<LongString>("300A0226") as LongString; } }
        public List<LongString> SourceIsotopeName_ { get { return Items.FindAll<LongString>("300A0226").ToList(); } }
        public DecimalString SourceIsotopeHalfLife { get { return Items.FindFirst<DecimalString>("300A0228") as DecimalString; } }
        public List<DecimalString> SourceIsotopeHalfLife_ { get { return Items.FindAll<DecimalString>("300A0228").ToList(); } }
        public CodeString SourceStrengthUnits { get { return Items.FindFirst<CodeString>("300A0229") as CodeString; } }
        public List<CodeString> SourceStrengthUnits_ { get { return Items.FindAll<CodeString>("300A0229").ToList(); } }
        public DecimalString ReferenceAirKermaRate { get { return Items.FindFirst<DecimalString>("300A022A") as DecimalString; } }
        public List<DecimalString> ReferenceAirKermaRate_ { get { return Items.FindAll<DecimalString>("300A022A").ToList(); } }
        public DecimalString SourceStrength { get { return Items.FindFirst<DecimalString>("300A022B") as DecimalString; } }
        public List<DecimalString> SourceStrength_ { get { return Items.FindAll<DecimalString>("300A022B").ToList(); } }
        public Date SourceStrengthReferenceDate { get { return Items.FindFirst<Date>("300A022C") as Date; } }
        public List<Date> SourceStrengthReferenceDate_ { get { return Items.FindAll<Date>("300A022C").ToList(); } }
        public Time SourceStrengthReferenceTime { get { return Items.FindFirst<Time>("300A022E") as Time; } }
        public List<Time> SourceStrengthReferenceTime_ { get { return Items.FindAll<Time>("300A022E").ToList(); } }
        public SequenceSelector ApplicationSetupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0230")); } }
        public List<SequenceSelector> ApplicationSetupSequence_ { get { return Items.FindAll<Sequence>("300A0230").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ApplicationSetupType { get { return Items.FindFirst<CodeString>("300A0232") as CodeString; } }
        public List<CodeString> ApplicationSetupType_ { get { return Items.FindAll<CodeString>("300A0232").ToList(); } }
        public IntegerString ApplicationSetupNumber { get { return Items.FindFirst<IntegerString>("300A0234") as IntegerString; } }
        public List<IntegerString> ApplicationSetupNumber_ { get { return Items.FindAll<IntegerString>("300A0234").ToList(); } }
        public LongString ApplicationSetupName { get { return Items.FindFirst<LongString>("300A0236") as LongString; } }
        public List<LongString> ApplicationSetupName_ { get { return Items.FindAll<LongString>("300A0236").ToList(); } }
        public LongString ApplicationSetupManufacturer { get { return Items.FindFirst<LongString>("300A0238") as LongString; } }
        public List<LongString> ApplicationSetupManufacturer_ { get { return Items.FindAll<LongString>("300A0238").ToList(); } }
        public IntegerString TemplateNumber { get { return Items.FindFirst<IntegerString>("300A0240") as IntegerString; } }
        public List<IntegerString> TemplateNumber_ { get { return Items.FindAll<IntegerString>("300A0240").ToList(); } }
        public ShortString TemplateType { get { return Items.FindFirst<ShortString>("300A0242") as ShortString; } }
        public List<ShortString> TemplateType_ { get { return Items.FindAll<ShortString>("300A0242").ToList(); } }
        public LongString TemplateName { get { return Items.FindFirst<LongString>("300A0244") as LongString; } }
        public List<LongString> TemplateName_ { get { return Items.FindAll<LongString>("300A0244").ToList(); } }
        public DecimalString TotalReferenceAirKerma { get { return Items.FindFirst<DecimalString>("300A0250") as DecimalString; } }
        public List<DecimalString> TotalReferenceAirKerma_ { get { return Items.FindAll<DecimalString>("300A0250").ToList(); } }
        public SequenceSelector BrachyAccessoryDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0260")); } }
        public List<SequenceSelector> BrachyAccessoryDeviceSequence_ { get { return Items.FindAll<Sequence>("300A0260").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString BrachyAccessoryDeviceNumber { get { return Items.FindFirst<IntegerString>("300A0262") as IntegerString; } }
        public List<IntegerString> BrachyAccessoryDeviceNumber_ { get { return Items.FindAll<IntegerString>("300A0262").ToList(); } }
        public ShortString BrachyAccessoryDeviceID { get { return Items.FindFirst<ShortString>("300A0263") as ShortString; } }
        public List<ShortString> BrachyAccessoryDeviceID_ { get { return Items.FindAll<ShortString>("300A0263").ToList(); } }
        public CodeString BrachyAccessoryDeviceType { get { return Items.FindFirst<CodeString>("300A0264") as CodeString; } }
        public List<CodeString> BrachyAccessoryDeviceType_ { get { return Items.FindAll<CodeString>("300A0264").ToList(); } }
        public LongString BrachyAccessoryDeviceName { get { return Items.FindFirst<LongString>("300A0266") as LongString; } }
        public List<LongString> BrachyAccessoryDeviceName_ { get { return Items.FindAll<LongString>("300A0266").ToList(); } }
        public DecimalString BrachyAccessoryDeviceNominalThickness { get { return Items.FindFirst<DecimalString>("300A026A") as DecimalString; } }
        public List<DecimalString> BrachyAccessoryDeviceNominalThickness_ { get { return Items.FindAll<DecimalString>("300A026A").ToList(); } }
        public DecimalString BrachyAccessoryDeviceNominalTransmission { get { return Items.FindFirst<DecimalString>("300A026C") as DecimalString; } }
        public List<DecimalString> BrachyAccessoryDeviceNominalTransmission_ { get { return Items.FindAll<DecimalString>("300A026C").ToList(); } }
        public SequenceSelector ChannelSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0280")); } }
        public List<SequenceSelector> ChannelSequence_ { get { return Items.FindAll<Sequence>("300A0280").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ChannelNumber { get { return Items.FindFirst<IntegerString>("300A0282") as IntegerString; } }
        public List<IntegerString> ChannelNumber_ { get { return Items.FindAll<IntegerString>("300A0282").ToList(); } }
        public DecimalString ChannelLength { get { return Items.FindFirst<DecimalString>("300A0284") as DecimalString; } }
        public List<DecimalString> ChannelLength_ { get { return Items.FindAll<DecimalString>("300A0284").ToList(); } }
        public DecimalString ChannelTotalTime { get { return Items.FindFirst<DecimalString>("300A0286") as DecimalString; } }
        public List<DecimalString> ChannelTotalTime_ { get { return Items.FindAll<DecimalString>("300A0286").ToList(); } }
        public CodeString SourceMovementType { get { return Items.FindFirst<CodeString>("300A0288") as CodeString; } }
        public List<CodeString> SourceMovementType_ { get { return Items.FindAll<CodeString>("300A0288").ToList(); } }
        public IntegerString NumberOfPulses { get { return Items.FindFirst<IntegerString>("300A028A") as IntegerString; } }
        public List<IntegerString> NumberOfPulses_ { get { return Items.FindAll<IntegerString>("300A028A").ToList(); } }
        public DecimalString PulseRepetitionInterval { get { return Items.FindFirst<DecimalString>("300A028C") as DecimalString; } }
        public List<DecimalString> PulseRepetitionInterval_ { get { return Items.FindAll<DecimalString>("300A028C").ToList(); } }
        public IntegerString SourceApplicatorNumber { get { return Items.FindFirst<IntegerString>("300A0290") as IntegerString; } }
        public List<IntegerString> SourceApplicatorNumber_ { get { return Items.FindAll<IntegerString>("300A0290").ToList(); } }
        public ShortString SourceApplicatorID { get { return Items.FindFirst<ShortString>("300A0291") as ShortString; } }
        public List<ShortString> SourceApplicatorID_ { get { return Items.FindAll<ShortString>("300A0291").ToList(); } }
        public CodeString SourceApplicatorType { get { return Items.FindFirst<CodeString>("300A0292") as CodeString; } }
        public List<CodeString> SourceApplicatorType_ { get { return Items.FindAll<CodeString>("300A0292").ToList(); } }
        public LongString SourceApplicatorName { get { return Items.FindFirst<LongString>("300A0294") as LongString; } }
        public List<LongString> SourceApplicatorName_ { get { return Items.FindAll<LongString>("300A0294").ToList(); } }
        public DecimalString SourceApplicatorLength { get { return Items.FindFirst<DecimalString>("300A0296") as DecimalString; } }
        public List<DecimalString> SourceApplicatorLength_ { get { return Items.FindAll<DecimalString>("300A0296").ToList(); } }
        public LongString SourceApplicatorManufacturer { get { return Items.FindFirst<LongString>("300A0298") as LongString; } }
        public List<LongString> SourceApplicatorManufacturer_ { get { return Items.FindAll<LongString>("300A0298").ToList(); } }
        public DecimalString SourceApplicatorWallNominalThickness { get { return Items.FindFirst<DecimalString>("300A029C") as DecimalString; } }
        public List<DecimalString> SourceApplicatorWallNominalThickness_ { get { return Items.FindAll<DecimalString>("300A029C").ToList(); } }
        public DecimalString SourceApplicatorWallNominalTransmission { get { return Items.FindFirst<DecimalString>("300A029E") as DecimalString; } }
        public List<DecimalString> SourceApplicatorWallNominalTransmission_ { get { return Items.FindAll<DecimalString>("300A029E").ToList(); } }
        public DecimalString SourceApplicatorStepSize { get { return Items.FindFirst<DecimalString>("300A02A0") as DecimalString; } }
        public List<DecimalString> SourceApplicatorStepSize_ { get { return Items.FindAll<DecimalString>("300A02A0").ToList(); } }
        public IntegerString TransferTubeNumber { get { return Items.FindFirst<IntegerString>("300A02A2") as IntegerString; } }
        public List<IntegerString> TransferTubeNumber_ { get { return Items.FindAll<IntegerString>("300A02A2").ToList(); } }
        public DecimalString TransferTubeLength { get { return Items.FindFirst<DecimalString>("300A02A4") as DecimalString; } }
        public List<DecimalString> TransferTubeLength_ { get { return Items.FindAll<DecimalString>("300A02A4").ToList(); } }
        public SequenceSelector ChannelShieldSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A02B0")); } }
        public List<SequenceSelector> ChannelShieldSequence_ { get { return Items.FindAll<Sequence>("300A02B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ChannelShieldNumber { get { return Items.FindFirst<IntegerString>("300A02B2") as IntegerString; } }
        public List<IntegerString> ChannelShieldNumber_ { get { return Items.FindAll<IntegerString>("300A02B2").ToList(); } }
        public ShortString ChannelShieldID { get { return Items.FindFirst<ShortString>("300A02B3") as ShortString; } }
        public List<ShortString> ChannelShieldID_ { get { return Items.FindAll<ShortString>("300A02B3").ToList(); } }
        public LongString ChannelShieldName { get { return Items.FindFirst<LongString>("300A02B4") as LongString; } }
        public List<LongString> ChannelShieldName_ { get { return Items.FindAll<LongString>("300A02B4").ToList(); } }
        public DecimalString ChannelShieldNominalThickness { get { return Items.FindFirst<DecimalString>("300A02B8") as DecimalString; } }
        public List<DecimalString> ChannelShieldNominalThickness_ { get { return Items.FindAll<DecimalString>("300A02B8").ToList(); } }
        public DecimalString ChannelShieldNominalTransmission { get { return Items.FindFirst<DecimalString>("300A02BA") as DecimalString; } }
        public List<DecimalString> ChannelShieldNominalTransmission_ { get { return Items.FindAll<DecimalString>("300A02BA").ToList(); } }
        public DecimalString FinalCumulativeTimeWeight { get { return Items.FindFirst<DecimalString>("300A02C8") as DecimalString; } }
        public List<DecimalString> FinalCumulativeTimeWeight_ { get { return Items.FindAll<DecimalString>("300A02C8").ToList(); } }
        public SequenceSelector BrachyControlPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A02D0")); } }
        public List<SequenceSelector> BrachyControlPointSequence_ { get { return Items.FindAll<Sequence>("300A02D0").Select(s => new SequenceSelector(s)).ToList(); } }
        public DecimalString ControlPointRelativePosition { get { return Items.FindFirst<DecimalString>("300A02D2") as DecimalString; } }
        public List<DecimalString> ControlPointRelativePosition_ { get { return Items.FindAll<DecimalString>("300A02D2").ToList(); } }
        public DecimalString ControlPoint3DPosition { get { return Items.FindFirst<DecimalString>("300A02D4") as DecimalString; } }
        public List<DecimalString> ControlPoint3DPosition_ { get { return Items.FindAll<DecimalString>("300A02D4").ToList(); } }
        public DecimalString CumulativeTimeWeight { get { return Items.FindFirst<DecimalString>("300A02D6") as DecimalString; } }
        public List<DecimalString> CumulativeTimeWeight_ { get { return Items.FindAll<DecimalString>("300A02D6").ToList(); } }
        public CodeString CompensatorDivergence { get { return Items.FindFirst<CodeString>("300A02E0") as CodeString; } }
        public List<CodeString> CompensatorDivergence_ { get { return Items.FindAll<CodeString>("300A02E0").ToList(); } }
        public CodeString CompensatorMountingPosition { get { return Items.FindFirst<CodeString>("300A02E1") as CodeString; } }
        public List<CodeString> CompensatorMountingPosition_ { get { return Items.FindAll<CodeString>("300A02E1").ToList(); } }
        public DecimalString SourceToCompensatorDistance { get { return Items.FindFirst<DecimalString>("300A02E2") as DecimalString; } }
        public List<DecimalString> SourceToCompensatorDistance_ { get { return Items.FindAll<DecimalString>("300A02E2").ToList(); } }
        public FloatingPointSingle TotalCompensatorTrayWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A02E3") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TotalCompensatorTrayWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A02E3").ToList(); } }
        public FloatingPointSingle IsocenterToCompensatorTrayDistance { get { return Items.FindFirst<FloatingPointSingle>("300A02E4") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToCompensatorTrayDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A02E4").ToList(); } }
        public FloatingPointSingle CompensatorColumnOffset { get { return Items.FindFirst<FloatingPointSingle>("300A02E5") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CompensatorColumnOffset_ { get { return Items.FindAll<FloatingPointSingle>("300A02E5").ToList(); } }
        public FloatingPointSingle IsocenterToCompensatorDistances { get { return Items.FindFirst<FloatingPointSingle>("300A02E6") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToCompensatorDistances_ { get { return Items.FindAll<FloatingPointSingle>("300A02E6").ToList(); } }
        public FloatingPointSingle CompensatorRelativeStoppingPowerRatio { get { return Items.FindFirst<FloatingPointSingle>("300A02E7") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CompensatorRelativeStoppingPowerRatio_ { get { return Items.FindAll<FloatingPointSingle>("300A02E7").ToList(); } }
        public FloatingPointSingle CompensatorMillingToolDiameter { get { return Items.FindFirst<FloatingPointSingle>("300A02E8") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CompensatorMillingToolDiameter_ { get { return Items.FindAll<FloatingPointSingle>("300A02E8").ToList(); } }
        public SequenceSelector IonRangeCompensatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A02EA")); } }
        public List<SequenceSelector> IonRangeCompensatorSequence_ { get { return Items.FindAll<Sequence>("300A02EA").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongText CompensatorDescription { get { return Items.FindFirst<LongText>("300A02EB") as LongText; } }
        public List<LongText> CompensatorDescription_ { get { return Items.FindAll<LongText>("300A02EB").ToList(); } }
        public IntegerString RadiationMassNumber { get { return Items.FindFirst<IntegerString>("300A0302") as IntegerString; } }
        public List<IntegerString> RadiationMassNumber_ { get { return Items.FindAll<IntegerString>("300A0302").ToList(); } }
        public IntegerString RadiationAtomicNumber { get { return Items.FindFirst<IntegerString>("300A0304") as IntegerString; } }
        public List<IntegerString> RadiationAtomicNumber_ { get { return Items.FindAll<IntegerString>("300A0304").ToList(); } }
        public SignedShort RadiationChargeState { get { return Items.FindFirst<SignedShort>("300A0306") as SignedShort; } }
        public List<SignedShort> RadiationChargeState_ { get { return Items.FindAll<SignedShort>("300A0306").ToList(); } }
        public CodeString ScanMode { get { return Items.FindFirst<CodeString>("300A0308") as CodeString; } }
        public List<CodeString> ScanMode_ { get { return Items.FindAll<CodeString>("300A0308").ToList(); } }
        public FloatingPointSingle VirtualSourceAxisDistances { get { return Items.FindFirst<FloatingPointSingle>("300A030A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VirtualSourceAxisDistances_ { get { return Items.FindAll<FloatingPointSingle>("300A030A").ToList(); } }
        public SequenceSelector SnoutSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A030C")); } }
        public List<SequenceSelector> SnoutSequence_ { get { return Items.FindAll<Sequence>("300A030C").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle SnoutPosition { get { return Items.FindFirst<FloatingPointSingle>("300A030D") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SnoutPosition_ { get { return Items.FindAll<FloatingPointSingle>("300A030D").ToList(); } }
        public ShortString SnoutID { get { return Items.FindFirst<ShortString>("300A030F") as ShortString; } }
        public List<ShortString> SnoutID_ { get { return Items.FindAll<ShortString>("300A030F").ToList(); } }
        public IntegerString NumberOfRangeShifters { get { return Items.FindFirst<IntegerString>("300A0312") as IntegerString; } }
        public List<IntegerString> NumberOfRangeShifters_ { get { return Items.FindAll<IntegerString>("300A0312").ToList(); } }
        public SequenceSelector RangeShifterSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0314")); } }
        public List<SequenceSelector> RangeShifterSequence_ { get { return Items.FindAll<Sequence>("300A0314").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString RangeShifterNumber { get { return Items.FindFirst<IntegerString>("300A0316") as IntegerString; } }
        public List<IntegerString> RangeShifterNumber_ { get { return Items.FindAll<IntegerString>("300A0316").ToList(); } }
        public ShortString RangeShifterID { get { return Items.FindFirst<ShortString>("300A0318") as ShortString; } }
        public List<ShortString> RangeShifterID_ { get { return Items.FindAll<ShortString>("300A0318").ToList(); } }
        public CodeString RangeShifterType { get { return Items.FindFirst<CodeString>("300A0320") as CodeString; } }
        public List<CodeString> RangeShifterType_ { get { return Items.FindAll<CodeString>("300A0320").ToList(); } }
        public LongString RangeShifterDescription { get { return Items.FindFirst<LongString>("300A0322") as LongString; } }
        public List<LongString> RangeShifterDescription_ { get { return Items.FindAll<LongString>("300A0322").ToList(); } }
        public IntegerString NumberOfLateralSpreadingDevices { get { return Items.FindFirst<IntegerString>("300A0330") as IntegerString; } }
        public List<IntegerString> NumberOfLateralSpreadingDevices_ { get { return Items.FindAll<IntegerString>("300A0330").ToList(); } }
        public SequenceSelector LateralSpreadingDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0332")); } }
        public List<SequenceSelector> LateralSpreadingDeviceSequence_ { get { return Items.FindAll<Sequence>("300A0332").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString LateralSpreadingDeviceNumber { get { return Items.FindFirst<IntegerString>("300A0334") as IntegerString; } }
        public List<IntegerString> LateralSpreadingDeviceNumber_ { get { return Items.FindAll<IntegerString>("300A0334").ToList(); } }
        public ShortString LateralSpreadingDeviceID { get { return Items.FindFirst<ShortString>("300A0336") as ShortString; } }
        public List<ShortString> LateralSpreadingDeviceID_ { get { return Items.FindAll<ShortString>("300A0336").ToList(); } }
        public CodeString LateralSpreadingDeviceType { get { return Items.FindFirst<CodeString>("300A0338") as CodeString; } }
        public List<CodeString> LateralSpreadingDeviceType_ { get { return Items.FindAll<CodeString>("300A0338").ToList(); } }
        public LongString LateralSpreadingDeviceDescription { get { return Items.FindFirst<LongString>("300A033A") as LongString; } }
        public List<LongString> LateralSpreadingDeviceDescription_ { get { return Items.FindAll<LongString>("300A033A").ToList(); } }
        public FloatingPointSingle LateralSpreadingDeviceWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A033C") as FloatingPointSingle; } }
        public List<FloatingPointSingle> LateralSpreadingDeviceWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A033C").ToList(); } }
        public IntegerString NumberOfRangeModulators { get { return Items.FindFirst<IntegerString>("300A0340") as IntegerString; } }
        public List<IntegerString> NumberOfRangeModulators_ { get { return Items.FindAll<IntegerString>("300A0340").ToList(); } }
        public SequenceSelector RangeModulatorSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0342")); } }
        public List<SequenceSelector> RangeModulatorSequence_ { get { return Items.FindAll<Sequence>("300A0342").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString RangeModulatorNumber { get { return Items.FindFirst<IntegerString>("300A0344") as IntegerString; } }
        public List<IntegerString> RangeModulatorNumber_ { get { return Items.FindAll<IntegerString>("300A0344").ToList(); } }
        public ShortString RangeModulatorID { get { return Items.FindFirst<ShortString>("300A0346") as ShortString; } }
        public List<ShortString> RangeModulatorID_ { get { return Items.FindAll<ShortString>("300A0346").ToList(); } }
        public CodeString RangeModulatorType { get { return Items.FindFirst<CodeString>("300A0348") as CodeString; } }
        public List<CodeString> RangeModulatorType_ { get { return Items.FindAll<CodeString>("300A0348").ToList(); } }
        public LongString RangeModulatorDescription { get { return Items.FindFirst<LongString>("300A034A") as LongString; } }
        public List<LongString> RangeModulatorDescription_ { get { return Items.FindAll<LongString>("300A034A").ToList(); } }
        public ShortString BeamCurrentModulationID { get { return Items.FindFirst<ShortString>("300A034C") as ShortString; } }
        public List<ShortString> BeamCurrentModulationID_ { get { return Items.FindAll<ShortString>("300A034C").ToList(); } }
        public CodeString PatientSupportType { get { return Items.FindFirst<CodeString>("300A0350") as CodeString; } }
        public List<CodeString> PatientSupportType_ { get { return Items.FindAll<CodeString>("300A0350").ToList(); } }
        public ShortString PatientSupportID { get { return Items.FindFirst<ShortString>("300A0352") as ShortString; } }
        public List<ShortString> PatientSupportID_ { get { return Items.FindAll<ShortString>("300A0352").ToList(); } }
        public LongString PatientSupportAccessoryCode { get { return Items.FindFirst<LongString>("300A0354") as LongString; } }
        public List<LongString> PatientSupportAccessoryCode_ { get { return Items.FindAll<LongString>("300A0354").ToList(); } }
        public FloatingPointSingle FixationLightAzimuthalAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0356") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FixationLightAzimuthalAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0356").ToList(); } }
        public FloatingPointSingle FixationLightPolarAngle { get { return Items.FindFirst<FloatingPointSingle>("300A0358") as FloatingPointSingle; } }
        public List<FloatingPointSingle> FixationLightPolarAngle_ { get { return Items.FindAll<FloatingPointSingle>("300A0358").ToList(); } }
        public FloatingPointSingle MetersetRate { get { return Items.FindFirst<FloatingPointSingle>("300A035A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> MetersetRate_ { get { return Items.FindAll<FloatingPointSingle>("300A035A").ToList(); } }
        public SequenceSelector RangeShifterSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0360")); } }
        public List<SequenceSelector> RangeShifterSettingsSequence_ { get { return Items.FindAll<Sequence>("300A0360").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString RangeShifterSetting { get { return Items.FindFirst<LongString>("300A0362") as LongString; } }
        public List<LongString> RangeShifterSetting_ { get { return Items.FindAll<LongString>("300A0362").ToList(); } }
        public FloatingPointSingle IsocenterToRangeShifterDistance { get { return Items.FindFirst<FloatingPointSingle>("300A0364") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToRangeShifterDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A0364").ToList(); } }
        public FloatingPointSingle RangeShifterWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A0366") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RangeShifterWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A0366").ToList(); } }
        public SequenceSelector LateralSpreadingDeviceSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0370")); } }
        public List<SequenceSelector> LateralSpreadingDeviceSettingsSequence_ { get { return Items.FindAll<Sequence>("300A0370").Select(s => new SequenceSelector(s)).ToList(); } }
        public LongString LateralSpreadingDeviceSetting { get { return Items.FindFirst<LongString>("300A0372") as LongString; } }
        public List<LongString> LateralSpreadingDeviceSetting_ { get { return Items.FindAll<LongString>("300A0372").ToList(); } }
        public FloatingPointSingle IsocenterToLateralSpreadingDeviceDistance { get { return Items.FindFirst<FloatingPointSingle>("300A0374") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToLateralSpreadingDeviceDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A0374").ToList(); } }
        public SequenceSelector RangeModulatorSettingsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0380")); } }
        public List<SequenceSelector> RangeModulatorSettingsSequence_ { get { return Items.FindAll<Sequence>("300A0380").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle RangeModulatorGatingStartValue { get { return Items.FindFirst<FloatingPointSingle>("300A0382") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RangeModulatorGatingStartValue_ { get { return Items.FindAll<FloatingPointSingle>("300A0382").ToList(); } }
        public FloatingPointSingle RangeModulatorGatingStopValue { get { return Items.FindFirst<FloatingPointSingle>("300A0384") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RangeModulatorGatingStopValue_ { get { return Items.FindAll<FloatingPointSingle>("300A0384").ToList(); } }
        public FloatingPointSingle RangeModulatorGatingStartWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A0386") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RangeModulatorGatingStartWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A0386").ToList(); } }
        public FloatingPointSingle RangeModulatorGatingStopWaterEquivalentThickness { get { return Items.FindFirst<FloatingPointSingle>("300A0388") as FloatingPointSingle; } }
        public List<FloatingPointSingle> RangeModulatorGatingStopWaterEquivalentThickness_ { get { return Items.FindAll<FloatingPointSingle>("300A0388").ToList(); } }
        public FloatingPointSingle IsocenterToRangeModulatorDistance { get { return Items.FindFirst<FloatingPointSingle>("300A038A") as FloatingPointSingle; } }
        public List<FloatingPointSingle> IsocenterToRangeModulatorDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A038A").ToList(); } }
        public ShortString ScanSpotTuneID { get { return Items.FindFirst<ShortString>("300A0390") as ShortString; } }
        public List<ShortString> ScanSpotTuneID_ { get { return Items.FindAll<ShortString>("300A0390").ToList(); } }
        public IntegerString NumberOfScanSpotPositions { get { return Items.FindFirst<IntegerString>("300A0392") as IntegerString; } }
        public List<IntegerString> NumberOfScanSpotPositions_ { get { return Items.FindAll<IntegerString>("300A0392").ToList(); } }
        public FloatingPointSingle ScanSpotPositionMap { get { return Items.FindFirst<FloatingPointSingle>("300A0394") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ScanSpotPositionMap_ { get { return Items.FindAll<FloatingPointSingle>("300A0394").ToList(); } }
        public FloatingPointSingle ScanSpotMetersetWeights { get { return Items.FindFirst<FloatingPointSingle>("300A0396") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ScanSpotMetersetWeights_ { get { return Items.FindAll<FloatingPointSingle>("300A0396").ToList(); } }
        public FloatingPointSingle ScanningSpotSize { get { return Items.FindFirst<FloatingPointSingle>("300A0398") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ScanningSpotSize_ { get { return Items.FindAll<FloatingPointSingle>("300A0398").ToList(); } }
        public IntegerString NumberOfPaintings { get { return Items.FindFirst<IntegerString>("300A039A") as IntegerString; } }
        public List<IntegerString> NumberOfPaintings_ { get { return Items.FindAll<IntegerString>("300A039A").ToList(); } }
        public SequenceSelector IonToleranceTableSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03A0")); } }
        public List<SequenceSelector> IonToleranceTableSequence_ { get { return Items.FindAll<Sequence>("300A03A0").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonBeamSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03A2")); } }
        public List<SequenceSelector> IonBeamSequence_ { get { return Items.FindAll<Sequence>("300A03A2").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonBeamLimitingDeviceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03A4")); } }
        public List<SequenceSelector> IonBeamLimitingDeviceSequence_ { get { return Items.FindAll<Sequence>("300A03A4").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonBlockSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03A6")); } }
        public List<SequenceSelector> IonBlockSequence_ { get { return Items.FindAll<Sequence>("300A03A6").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonControlPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03A8")); } }
        public List<SequenceSelector> IonControlPointSequence_ { get { return Items.FindAll<Sequence>("300A03A8").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonWedgeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03AA")); } }
        public List<SequenceSelector> IonWedgeSequence_ { get { return Items.FindAll<Sequence>("300A03AA").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector IonWedgePositionSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A03AC")); } }
        public List<SequenceSelector> IonWedgePositionSequence_ { get { return Items.FindAll<Sequence>("300A03AC").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedSetupImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0401")); } }
        public List<SequenceSelector> ReferencedSetupImageSequence_ { get { return Items.FindAll<Sequence>("300A0401").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortText SetupImageComment { get { return Items.FindFirst<ShortText>("300A0402") as ShortText; } }
        public List<ShortText> SetupImageComment_ { get { return Items.FindAll<ShortText>("300A0402").ToList(); } }
        public SequenceSelector MotionSynchronizationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0410")); } }
        public List<SequenceSelector> MotionSynchronizationSequence_ { get { return Items.FindAll<Sequence>("300A0410").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle ControlPointOrientation { get { return Items.FindFirst<FloatingPointSingle>("300A0412") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ControlPointOrientation_ { get { return Items.FindAll<FloatingPointSingle>("300A0412").ToList(); } }
        public SequenceSelector GeneralAccessorySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0420")); } }
        public List<SequenceSelector> GeneralAccessorySequence_ { get { return Items.FindAll<Sequence>("300A0420").Select(s => new SequenceSelector(s)).ToList(); } }
        public ShortString GeneralAccessoryID { get { return Items.FindFirst<ShortString>("300A0421") as ShortString; } }
        public List<ShortString> GeneralAccessoryID_ { get { return Items.FindAll<ShortString>("300A0421").ToList(); } }
        public ShortText GeneralAccessoryDescription { get { return Items.FindFirst<ShortText>("300A0422") as ShortText; } }
        public List<ShortText> GeneralAccessoryDescription_ { get { return Items.FindAll<ShortText>("300A0422").ToList(); } }
        public CodeString GeneralAccessoryType { get { return Items.FindFirst<CodeString>("300A0423") as CodeString; } }
        public List<CodeString> GeneralAccessoryType_ { get { return Items.FindAll<CodeString>("300A0423").ToList(); } }
        public IntegerString GeneralAccessoryNumber { get { return Items.FindFirst<IntegerString>("300A0424") as IntegerString; } }
        public List<IntegerString> GeneralAccessoryNumber_ { get { return Items.FindAll<IntegerString>("300A0424").ToList(); } }
        public SequenceSelector ApplicatorGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300A0431")); } }
        public List<SequenceSelector> ApplicatorGeometrySequence_ { get { return Items.FindAll<Sequence>("300A0431").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ApplicatorApertureShape { get { return Items.FindFirst<CodeString>("300A0432") as CodeString; } }
        public List<CodeString> ApplicatorApertureShape_ { get { return Items.FindAll<CodeString>("300A0432").ToList(); } }
        public FloatingPointSingle ApplicatorOpening { get { return Items.FindFirst<FloatingPointSingle>("300A0433") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ApplicatorOpening_ { get { return Items.FindAll<FloatingPointSingle>("300A0433").ToList(); } }
        public FloatingPointSingle ApplicatorOpeningX { get { return Items.FindFirst<FloatingPointSingle>("300A0434") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ApplicatorOpeningX_ { get { return Items.FindAll<FloatingPointSingle>("300A0434").ToList(); } }
        public FloatingPointSingle ApplicatorOpeningY { get { return Items.FindFirst<FloatingPointSingle>("300A0435") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ApplicatorOpeningY_ { get { return Items.FindAll<FloatingPointSingle>("300A0435").ToList(); } }
        public FloatingPointSingle SourceToApplicatorMountingPositionDistance { get { return Items.FindFirst<FloatingPointSingle>("300A0436") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SourceToApplicatorMountingPositionDistance_ { get { return Items.FindAll<FloatingPointSingle>("300A0436").ToList(); } }
        public SequenceSelector ReferencedRTPlanSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0002")); } }
        public List<SequenceSelector> ReferencedRTPlanSequence_ { get { return Items.FindAll<Sequence>("300C0002").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedBeamSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0004")); } }
        public List<SequenceSelector> ReferencedBeamSequence_ { get { return Items.FindAll<Sequence>("300C0004").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedBeamNumber { get { return Items.FindFirst<IntegerString>("300C0006") as IntegerString; } }
        public List<IntegerString> ReferencedBeamNumber_ { get { return Items.FindAll<IntegerString>("300C0006").ToList(); } }
        public IntegerString ReferencedReferenceImageNumber { get { return Items.FindFirst<IntegerString>("300C0007") as IntegerString; } }
        public List<IntegerString> ReferencedReferenceImageNumber_ { get { return Items.FindAll<IntegerString>("300C0007").ToList(); } }
        public DecimalString StartCumulativeMetersetWeight { get { return Items.FindFirst<DecimalString>("300C0008") as DecimalString; } }
        public List<DecimalString> StartCumulativeMetersetWeight_ { get { return Items.FindAll<DecimalString>("300C0008").ToList(); } }
        public DecimalString EndCumulativeMetersetWeight { get { return Items.FindFirst<DecimalString>("300C0009") as DecimalString; } }
        public List<DecimalString> EndCumulativeMetersetWeight_ { get { return Items.FindAll<DecimalString>("300C0009").ToList(); } }
        public SequenceSelector ReferencedBrachyApplicationSetupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C000A")); } }
        public List<SequenceSelector> ReferencedBrachyApplicationSetupSequence_ { get { return Items.FindAll<Sequence>("300C000A").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedBrachyApplicationSetupNumber { get { return Items.FindFirst<IntegerString>("300C000C") as IntegerString; } }
        public List<IntegerString> ReferencedBrachyApplicationSetupNumber_ { get { return Items.FindAll<IntegerString>("300C000C").ToList(); } }
        public IntegerString ReferencedSourceNumber { get { return Items.FindFirst<IntegerString>("300C000E") as IntegerString; } }
        public List<IntegerString> ReferencedSourceNumber_ { get { return Items.FindAll<IntegerString>("300C000E").ToList(); } }
        public SequenceSelector ReferencedFractionGroupSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0020")); } }
        public List<SequenceSelector> ReferencedFractionGroupSequence_ { get { return Items.FindAll<Sequence>("300C0020").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedFractionGroupNumber { get { return Items.FindFirst<IntegerString>("300C0022") as IntegerString; } }
        public List<IntegerString> ReferencedFractionGroupNumber_ { get { return Items.FindAll<IntegerString>("300C0022").ToList(); } }
        public SequenceSelector ReferencedVerificationImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0040")); } }
        public List<SequenceSelector> ReferencedVerificationImageSequence_ { get { return Items.FindAll<Sequence>("300C0040").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedReferenceImageSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0042")); } }
        public List<SequenceSelector> ReferencedReferenceImageSequence_ { get { return Items.FindAll<Sequence>("300C0042").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0050")); } }
        public List<SequenceSelector> ReferencedDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("300C0050").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedDoseReferenceNumber { get { return Items.FindFirst<IntegerString>("300C0051") as IntegerString; } }
        public List<IntegerString> ReferencedDoseReferenceNumber_ { get { return Items.FindAll<IntegerString>("300C0051").ToList(); } }
        public SequenceSelector BrachyReferencedDoseReferenceSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0055")); } }
        public List<SequenceSelector> BrachyReferencedDoseReferenceSequence_ { get { return Items.FindAll<Sequence>("300C0055").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ReferencedStructureSetSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0060")); } }
        public List<SequenceSelector> ReferencedStructureSetSequence_ { get { return Items.FindAll<Sequence>("300C0060").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedPatientSetupNumber { get { return Items.FindFirst<IntegerString>("300C006A") as IntegerString; } }
        public List<IntegerString> ReferencedPatientSetupNumber_ { get { return Items.FindAll<IntegerString>("300C006A").ToList(); } }
        public SequenceSelector ReferencedDoseSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C0080")); } }
        public List<SequenceSelector> ReferencedDoseSequence_ { get { return Items.FindAll<Sequence>("300C0080").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedToleranceTableNumber { get { return Items.FindFirst<IntegerString>("300C00A0") as IntegerString; } }
        public List<IntegerString> ReferencedToleranceTableNumber_ { get { return Items.FindAll<IntegerString>("300C00A0").ToList(); } }
        public SequenceSelector ReferencedBolusSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C00B0")); } }
        public List<SequenceSelector> ReferencedBolusSequence_ { get { return Items.FindAll<Sequence>("300C00B0").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedWedgeNumber { get { return Items.FindFirst<IntegerString>("300C00C0") as IntegerString; } }
        public List<IntegerString> ReferencedWedgeNumber_ { get { return Items.FindAll<IntegerString>("300C00C0").ToList(); } }
        public IntegerString ReferencedCompensatorNumber { get { return Items.FindFirst<IntegerString>("300C00D0") as IntegerString; } }
        public List<IntegerString> ReferencedCompensatorNumber_ { get { return Items.FindAll<IntegerString>("300C00D0").ToList(); } }
        public IntegerString ReferencedBlockNumber { get { return Items.FindFirst<IntegerString>("300C00E0") as IntegerString; } }
        public List<IntegerString> ReferencedBlockNumber_ { get { return Items.FindAll<IntegerString>("300C00E0").ToList(); } }
        public IntegerString ReferencedControlPointIndex { get { return Items.FindFirst<IntegerString>("300C00F0") as IntegerString; } }
        public List<IntegerString> ReferencedControlPointIndex_ { get { return Items.FindAll<IntegerString>("300C00F0").ToList(); } }
        public SequenceSelector ReferencedControlPointSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("300C00F2")); } }
        public List<SequenceSelector> ReferencedControlPointSequence_ { get { return Items.FindAll<Sequence>("300C00F2").Select(s => new SequenceSelector(s)).ToList(); } }
        public IntegerString ReferencedStartControlPointIndex { get { return Items.FindFirst<IntegerString>("300C00F4") as IntegerString; } }
        public List<IntegerString> ReferencedStartControlPointIndex_ { get { return Items.FindAll<IntegerString>("300C00F4").ToList(); } }
        public IntegerString ReferencedStopControlPointIndex { get { return Items.FindFirst<IntegerString>("300C00F6") as IntegerString; } }
        public List<IntegerString> ReferencedStopControlPointIndex_ { get { return Items.FindAll<IntegerString>("300C00F6").ToList(); } }
        public IntegerString ReferencedRangeShifterNumber { get { return Items.FindFirst<IntegerString>("300C0100") as IntegerString; } }
        public List<IntegerString> ReferencedRangeShifterNumber_ { get { return Items.FindAll<IntegerString>("300C0100").ToList(); } }
        public IntegerString ReferencedLateralSpreadingDeviceNumber { get { return Items.FindFirst<IntegerString>("300C0102") as IntegerString; } }
        public List<IntegerString> ReferencedLateralSpreadingDeviceNumber_ { get { return Items.FindAll<IntegerString>("300C0102").ToList(); } }
        public IntegerString ReferencedRangeModulatorNumber { get { return Items.FindFirst<IntegerString>("300C0104") as IntegerString; } }
        public List<IntegerString> ReferencedRangeModulatorNumber_ { get { return Items.FindAll<IntegerString>("300C0104").ToList(); } }
        public CodeString ApprovalStatus { get { return Items.FindFirst<CodeString>("300E0002") as CodeString; } }
        public List<CodeString> ApprovalStatus_ { get { return Items.FindAll<CodeString>("300E0002").ToList(); } }
        public Date ReviewDate { get { return Items.FindFirst<Date>("300E0004") as Date; } }
        public List<Date> ReviewDate_ { get { return Items.FindAll<Date>("300E0004").ToList(); } }
        public Time ReviewTime { get { return Items.FindFirst<Time>("300E0005") as Time; } }
        public List<Time> ReviewTime_ { get { return Items.FindAll<Time>("300E0005").ToList(); } }
        public PersonName ReviewerName { get { return Items.FindFirst<PersonName>("300E0008") as PersonName; } }
        public List<PersonName> ReviewerName_ { get { return Items.FindAll<PersonName>("300E0008").ToList(); } }
        public LongText ArbitraryRetired { get { return Items.FindFirst<LongText>("40000010") as LongText; } }
        public List<LongText> ArbitraryRetired_ { get { return Items.FindAll<LongText>("40000010").ToList(); } }
        public LongText TextCommentsRetired { get { return Items.FindFirst<LongText>("40004000") as LongText; } }
        public List<LongText> TextCommentsRetired_ { get { return Items.FindAll<LongText>("40004000").ToList(); } }
        public ShortString ResultsIDRetired { get { return Items.FindFirst<ShortString>("40080040") as ShortString; } }
        public List<ShortString> ResultsIDRetired_ { get { return Items.FindAll<ShortString>("40080040").ToList(); } }
        public LongString ResultsIDIssuerRetired { get { return Items.FindFirst<LongString>("40080042") as LongString; } }
        public List<LongString> ResultsIDIssuerRetired_ { get { return Items.FindAll<LongString>("40080042").ToList(); } }
        public SequenceSelector ReferencedInterpretationSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("40080050")); } }
        public List<SequenceSelector> ReferencedInterpretationSequenceRetired_ { get { return Items.FindAll<Sequence>("40080050").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ReportProductionStatusTrialRetired { get { return Items.FindFirst<CodeString>("400800FF") as CodeString; } }
        public List<CodeString> ReportProductionStatusTrialRetired_ { get { return Items.FindAll<CodeString>("400800FF").ToList(); } }
        public Date InterpretationRecordedDateRetired { get { return Items.FindFirst<Date>("40080100") as Date; } }
        public List<Date> InterpretationRecordedDateRetired_ { get { return Items.FindAll<Date>("40080100").ToList(); } }
        public Time InterpretationRecordedTimeRetired { get { return Items.FindFirst<Time>("40080101") as Time; } }
        public List<Time> InterpretationRecordedTimeRetired_ { get { return Items.FindAll<Time>("40080101").ToList(); } }
        public PersonName InterpretationRecorderRetired { get { return Items.FindFirst<PersonName>("40080102") as PersonName; } }
        public List<PersonName> InterpretationRecorderRetired_ { get { return Items.FindAll<PersonName>("40080102").ToList(); } }
        public LongString ReferenceToRecordedSoundRetired { get { return Items.FindFirst<LongString>("40080103") as LongString; } }
        public List<LongString> ReferenceToRecordedSoundRetired_ { get { return Items.FindAll<LongString>("40080103").ToList(); } }
        public Date InterpretationTranscriptionDateRetired { get { return Items.FindFirst<Date>("40080108") as Date; } }
        public List<Date> InterpretationTranscriptionDateRetired_ { get { return Items.FindAll<Date>("40080108").ToList(); } }
        public Time InterpretationTranscriptionTimeRetired { get { return Items.FindFirst<Time>("40080109") as Time; } }
        public List<Time> InterpretationTranscriptionTimeRetired_ { get { return Items.FindAll<Time>("40080109").ToList(); } }
        public PersonName InterpretationTranscriberRetired { get { return Items.FindFirst<PersonName>("4008010A") as PersonName; } }
        public List<PersonName> InterpretationTranscriberRetired_ { get { return Items.FindAll<PersonName>("4008010A").ToList(); } }
        public ShortText InterpretationTextRetired { get { return Items.FindFirst<ShortText>("4008010B") as ShortText; } }
        public List<ShortText> InterpretationTextRetired_ { get { return Items.FindAll<ShortText>("4008010B").ToList(); } }
        public PersonName InterpretationAuthorRetired { get { return Items.FindFirst<PersonName>("4008010C") as PersonName; } }
        public List<PersonName> InterpretationAuthorRetired_ { get { return Items.FindAll<PersonName>("4008010C").ToList(); } }
        public SequenceSelector InterpretationApproverSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("40080111")); } }
        public List<SequenceSelector> InterpretationApproverSequenceRetired_ { get { return Items.FindAll<Sequence>("40080111").Select(s => new SequenceSelector(s)).ToList(); } }
        public Date InterpretationApprovalDateRetired { get { return Items.FindFirst<Date>("40080112") as Date; } }
        public List<Date> InterpretationApprovalDateRetired_ { get { return Items.FindAll<Date>("40080112").ToList(); } }
        public Time InterpretationApprovalTimeRetired { get { return Items.FindFirst<Time>("40080113") as Time; } }
        public List<Time> InterpretationApprovalTimeRetired_ { get { return Items.FindAll<Time>("40080113").ToList(); } }
        public PersonName PhysicianApprovingInterpretationRetired { get { return Items.FindFirst<PersonName>("40080114") as PersonName; } }
        public List<PersonName> PhysicianApprovingInterpretationRetired_ { get { return Items.FindAll<PersonName>("40080114").ToList(); } }
        public LongText InterpretationDiagnosisDescriptionRetired { get { return Items.FindFirst<LongText>("40080115") as LongText; } }
        public List<LongText> InterpretationDiagnosisDescriptionRetired_ { get { return Items.FindAll<LongText>("40080115").ToList(); } }
        public SequenceSelector InterpretationDiagnosisCodeSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("40080117")); } }
        public List<SequenceSelector> InterpretationDiagnosisCodeSequenceRetired_ { get { return Items.FindAll<Sequence>("40080117").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ResultsDistributionListSequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("40080118")); } }
        public List<SequenceSelector> ResultsDistributionListSequenceRetired_ { get { return Items.FindAll<Sequence>("40080118").Select(s => new SequenceSelector(s)).ToList(); } }
        public PersonName DistributionNameRetired { get { return Items.FindFirst<PersonName>("40080119") as PersonName; } }
        public List<PersonName> DistributionNameRetired_ { get { return Items.FindAll<PersonName>("40080119").ToList(); } }
        public LongString DistributionAddressRetired { get { return Items.FindFirst<LongString>("4008011A") as LongString; } }
        public List<LongString> DistributionAddressRetired_ { get { return Items.FindAll<LongString>("4008011A").ToList(); } }
        public ShortString InterpretationIDRetired { get { return Items.FindFirst<ShortString>("40080200") as ShortString; } }
        public List<ShortString> InterpretationIDRetired_ { get { return Items.FindAll<ShortString>("40080200").ToList(); } }
        public LongString InterpretationIDIssuerRetired { get { return Items.FindFirst<LongString>("40080202") as LongString; } }
        public List<LongString> InterpretationIDIssuerRetired_ { get { return Items.FindAll<LongString>("40080202").ToList(); } }
        public CodeString InterpretationTypeIDRetired { get { return Items.FindFirst<CodeString>("40080210") as CodeString; } }
        public List<CodeString> InterpretationTypeIDRetired_ { get { return Items.FindAll<CodeString>("40080210").ToList(); } }
        public CodeString InterpretationStatusIDRetired { get { return Items.FindFirst<CodeString>("40080212") as CodeString; } }
        public List<CodeString> InterpretationStatusIDRetired_ { get { return Items.FindAll<CodeString>("40080212").ToList(); } }
        public ShortText ImpressionsRetired { get { return Items.FindFirst<ShortText>("40080300") as ShortText; } }
        public List<ShortText> ImpressionsRetired_ { get { return Items.FindAll<ShortText>("40080300").ToList(); } }
        public ShortText ResultsCommentsRetired { get { return Items.FindFirst<ShortText>("40084000") as ShortText; } }
        public List<ShortText> ResultsCommentsRetired_ { get { return Items.FindAll<ShortText>("40084000").ToList(); } }
        public CodeString LowEnergyDetectors { get { return Items.FindFirst<CodeString>("40100001") as CodeString; } }
        public List<CodeString> LowEnergyDetectors_ { get { return Items.FindAll<CodeString>("40100001").ToList(); } }
        public CodeString HighEnergyDetectors { get { return Items.FindFirst<CodeString>("40100002") as CodeString; } }
        public List<CodeString> HighEnergyDetectors_ { get { return Items.FindAll<CodeString>("40100002").ToList(); } }
        public SequenceSelector DetectorGeometrySequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40100004")); } }
        public List<SequenceSelector> DetectorGeometrySequence_ { get { return Items.FindAll<Sequence>("40100004").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ThreatROIVoxelSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101001")); } }
        public List<SequenceSelector> ThreatROIVoxelSequence_ { get { return Items.FindAll<Sequence>("40101001").Select(s => new SequenceSelector(s)).ToList(); } }
        public FloatingPointSingle ThreatROIBase { get { return Items.FindFirst<FloatingPointSingle>("40101004") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ThreatROIBase_ { get { return Items.FindAll<FloatingPointSingle>("40101004").ToList(); } }
        public FloatingPointSingle ThreatROIExtents { get { return Items.FindFirst<FloatingPointSingle>("40101005") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ThreatROIExtents_ { get { return Items.FindAll<FloatingPointSingle>("40101005").ToList(); } }
        public OtherByteString ThreatROIBitmap { get { return Items.FindFirst<OtherByteString>("40101006") as OtherByteString; } }
        public List<OtherByteString> ThreatROIBitmap_ { get { return Items.FindAll<OtherByteString>("40101006").ToList(); } }
        public ShortString RouteSegmentID { get { return Items.FindFirst<ShortString>("40101007") as ShortString; } }
        public List<ShortString> RouteSegmentID_ { get { return Items.FindAll<ShortString>("40101007").ToList(); } }
        public CodeString GantryType { get { return Items.FindFirst<CodeString>("40101008") as CodeString; } }
        public List<CodeString> GantryType_ { get { return Items.FindAll<CodeString>("40101008").ToList(); } }
        public CodeString OOIOwnerType { get { return Items.FindFirst<CodeString>("40101009") as CodeString; } }
        public List<CodeString> OOIOwnerType_ { get { return Items.FindAll<CodeString>("40101009").ToList(); } }
        public SequenceSelector RouteSegmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("4010100A")); } }
        public List<SequenceSelector> RouteSegmentSequence_ { get { return Items.FindAll<Sequence>("4010100A").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort PotentialThreatObjectID { get { return Items.FindFirst<UnsignedShort>("40101010") as UnsignedShort; } }
        public List<UnsignedShort> PotentialThreatObjectID_ { get { return Items.FindAll<UnsignedShort>("40101010").ToList(); } }
        public SequenceSelector ThreatSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101011")); } }
        public List<SequenceSelector> ThreatSequence_ { get { return Items.FindAll<Sequence>("40101011").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ThreatCategory { get { return Items.FindFirst<CodeString>("40101012") as CodeString; } }
        public List<CodeString> ThreatCategory_ { get { return Items.FindAll<CodeString>("40101012").ToList(); } }
        public LongText ThreatCategoryDescription { get { return Items.FindFirst<LongText>("40101013") as LongText; } }
        public List<LongText> ThreatCategoryDescription_ { get { return Items.FindAll<LongText>("40101013").ToList(); } }
        public CodeString ATDAbilityAssessment { get { return Items.FindFirst<CodeString>("40101014") as CodeString; } }
        public List<CodeString> ATDAbilityAssessment_ { get { return Items.FindAll<CodeString>("40101014").ToList(); } }
        public CodeString ATDAssessmentFlag { get { return Items.FindFirst<CodeString>("40101015") as CodeString; } }
        public List<CodeString> ATDAssessmentFlag_ { get { return Items.FindAll<CodeString>("40101015").ToList(); } }
        public FloatingPointSingle ATDAssessmentProbability { get { return Items.FindFirst<FloatingPointSingle>("40101016") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ATDAssessmentProbability_ { get { return Items.FindAll<FloatingPointSingle>("40101016").ToList(); } }
        public FloatingPointSingle Mass { get { return Items.FindFirst<FloatingPointSingle>("40101017") as FloatingPointSingle; } }
        public List<FloatingPointSingle> Mass_ { get { return Items.FindAll<FloatingPointSingle>("40101017").ToList(); } }
        public FloatingPointSingle Density { get { return Items.FindFirst<FloatingPointSingle>("40101018") as FloatingPointSingle; } }
        public List<FloatingPointSingle> Density_ { get { return Items.FindAll<FloatingPointSingle>("40101018").ToList(); } }
        public FloatingPointSingle ZEffective { get { return Items.FindFirst<FloatingPointSingle>("40101019") as FloatingPointSingle; } }
        public List<FloatingPointSingle> ZEffective_ { get { return Items.FindAll<FloatingPointSingle>("40101019").ToList(); } }
        public ShortString BoardingPassID { get { return Items.FindFirst<ShortString>("4010101A") as ShortString; } }
        public List<ShortString> BoardingPassID_ { get { return Items.FindAll<ShortString>("4010101A").ToList(); } }
        public FloatingPointSingle CenterOfMass { get { return Items.FindFirst<FloatingPointSingle>("4010101B") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CenterOfMass_ { get { return Items.FindAll<FloatingPointSingle>("4010101B").ToList(); } }
        public FloatingPointSingle CenterOfPTO { get { return Items.FindFirst<FloatingPointSingle>("4010101C") as FloatingPointSingle; } }
        public List<FloatingPointSingle> CenterOfPTO_ { get { return Items.FindAll<FloatingPointSingle>("4010101C").ToList(); } }
        public FloatingPointSingle BoundingPolygon { get { return Items.FindFirst<FloatingPointSingle>("4010101D") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BoundingPolygon_ { get { return Items.FindAll<FloatingPointSingle>("4010101D").ToList(); } }
        public ShortString RouteSegmentStartLocationID { get { return Items.FindFirst<ShortString>("4010101E") as ShortString; } }
        public List<ShortString> RouteSegmentStartLocationID_ { get { return Items.FindAll<ShortString>("4010101E").ToList(); } }
        public ShortString RouteSegmentEndLocationID { get { return Items.FindFirst<ShortString>("4010101F") as ShortString; } }
        public List<ShortString> RouteSegmentEndLocationID_ { get { return Items.FindAll<ShortString>("4010101F").ToList(); } }
        public CodeString RouteSegmentLocationIDType { get { return Items.FindFirst<CodeString>("40101020") as CodeString; } }
        public List<CodeString> RouteSegmentLocationIDType_ { get { return Items.FindAll<CodeString>("40101020").ToList(); } }
        public CodeString AbortReason { get { return Items.FindFirst<CodeString>("40101021") as CodeString; } }
        public List<CodeString> AbortReason_ { get { return Items.FindAll<CodeString>("40101021").ToList(); } }
        public FloatingPointSingle VolumeOfPTO { get { return Items.FindFirst<FloatingPointSingle>("40101023") as FloatingPointSingle; } }
        public List<FloatingPointSingle> VolumeOfPTO_ { get { return Items.FindAll<FloatingPointSingle>("40101023").ToList(); } }
        public CodeString AbortFlag { get { return Items.FindFirst<CodeString>("40101024") as CodeString; } }
        public List<CodeString> AbortFlag_ { get { return Items.FindAll<CodeString>("40101024").ToList(); } }
        public EvilDICOM.Core.Element.DateTime RouteSegmentStartTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("40101025") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> RouteSegmentStartTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("40101025").ToList(); } }
        public EvilDICOM.Core.Element.DateTime RouteSegmentEndTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("40101026") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> RouteSegmentEndTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("40101026").ToList(); } }
        public CodeString TDRType { get { return Items.FindFirst<CodeString>("40101027") as CodeString; } }
        public List<CodeString> TDRType_ { get { return Items.FindAll<CodeString>("40101027").ToList(); } }
        public CodeString InternationalRouteSegment { get { return Items.FindFirst<CodeString>("40101028") as CodeString; } }
        public List<CodeString> InternationalRouteSegment_ { get { return Items.FindAll<CodeString>("40101028").ToList(); } }
        public LongString ThreatDetectionAlgorithmandVersion { get { return Items.FindFirst<LongString>("40101029") as LongString; } }
        public List<LongString> ThreatDetectionAlgorithmandVersion_ { get { return Items.FindAll<LongString>("40101029").ToList(); } }
        public ShortString AssignedLocation { get { return Items.FindFirst<ShortString>("4010102A") as ShortString; } }
        public List<ShortString> AssignedLocation_ { get { return Items.FindAll<ShortString>("4010102A").ToList(); } }
        public EvilDICOM.Core.Element.DateTime AlarmDecisionTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("4010102B") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> AlarmDecisionTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("4010102B").ToList(); } }
        public CodeString AlarmDecision { get { return Items.FindFirst<CodeString>("40101031") as CodeString; } }
        public List<CodeString> AlarmDecision_ { get { return Items.FindAll<CodeString>("40101031").ToList(); } }
        public UnsignedShort NumberOfTotalObjects { get { return Items.FindFirst<UnsignedShort>("40101033") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfTotalObjects_ { get { return Items.FindAll<UnsignedShort>("40101033").ToList(); } }
        public UnsignedShort NumberOfAlarmObjects { get { return Items.FindFirst<UnsignedShort>("40101034") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfAlarmObjects_ { get { return Items.FindAll<UnsignedShort>("40101034").ToList(); } }
        public SequenceSelector PTORepresentationSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101037")); } }
        public List<SequenceSelector> PTORepresentationSequence_ { get { return Items.FindAll<Sequence>("40101037").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector ATDAssessmentSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101038")); } }
        public List<SequenceSelector> ATDAssessmentSequence_ { get { return Items.FindAll<Sequence>("40101038").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString TIPType { get { return Items.FindFirst<CodeString>("40101039") as CodeString; } }
        public List<CodeString> TIPType_ { get { return Items.FindAll<CodeString>("40101039").ToList(); } }
        public CodeString DICOSVersion { get { return Items.FindFirst<CodeString>("4010103A") as CodeString; } }
        public List<CodeString> DICOSVersion_ { get { return Items.FindAll<CodeString>("4010103A").ToList(); } }
        public EvilDICOM.Core.Element.DateTime OOIOwnerCreationTime { get { return Items.FindFirst<EvilDICOM.Core.Element.DateTime>("40101041") as EvilDICOM.Core.Element.DateTime; } }
        public List<EvilDICOM.Core.Element.DateTime> OOIOwnerCreationTime_ { get { return Items.FindAll<EvilDICOM.Core.Element.DateTime>("40101041").ToList(); } }
        public CodeString OOIType { get { return Items.FindFirst<CodeString>("40101042") as CodeString; } }
        public List<CodeString> OOIType_ { get { return Items.FindAll<CodeString>("40101042").ToList(); } }
        public FloatingPointSingle OOISize { get { return Items.FindFirst<FloatingPointSingle>("40101043") as FloatingPointSingle; } }
        public List<FloatingPointSingle> OOISize_ { get { return Items.FindAll<FloatingPointSingle>("40101043").ToList(); } }
        public CodeString AcquisitionStatus { get { return Items.FindFirst<CodeString>("40101044") as CodeString; } }
        public List<CodeString> AcquisitionStatus_ { get { return Items.FindAll<CodeString>("40101044").ToList(); } }
        public SequenceSelector BasisMaterialsCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101045")); } }
        public List<SequenceSelector> BasisMaterialsCodeSequence_ { get { return Items.FindAll<Sequence>("40101045").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString PhantomType { get { return Items.FindFirst<CodeString>("40101046") as CodeString; } }
        public List<CodeString> PhantomType_ { get { return Items.FindAll<CodeString>("40101046").ToList(); } }
        public SequenceSelector OOIOwnerSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101047")); } }
        public List<SequenceSelector> OOIOwnerSequence_ { get { return Items.FindAll<Sequence>("40101047").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString ScanType { get { return Items.FindFirst<CodeString>("40101048") as CodeString; } }
        public List<CodeString> ScanType_ { get { return Items.FindAll<CodeString>("40101048").ToList(); } }
        public LongString ItineraryID { get { return Items.FindFirst<LongString>("40101051") as LongString; } }
        public List<LongString> ItineraryID_ { get { return Items.FindAll<LongString>("40101051").ToList(); } }
        public ShortString ItineraryIDType { get { return Items.FindFirst<ShortString>("40101052") as ShortString; } }
        public List<ShortString> ItineraryIDType_ { get { return Items.FindAll<ShortString>("40101052").ToList(); } }
        public LongString ItineraryIDAssigningAuthority { get { return Items.FindFirst<LongString>("40101053") as LongString; } }
        public List<LongString> ItineraryIDAssigningAuthority_ { get { return Items.FindAll<LongString>("40101053").ToList(); } }
        public ShortString RouteID { get { return Items.FindFirst<ShortString>("40101054") as ShortString; } }
        public List<ShortString> RouteID_ { get { return Items.FindAll<ShortString>("40101054").ToList(); } }
        public ShortString RouteIDAssigningAuthority { get { return Items.FindFirst<ShortString>("40101055") as ShortString; } }
        public List<ShortString> RouteIDAssigningAuthority_ { get { return Items.FindAll<ShortString>("40101055").ToList(); } }
        public CodeString InboundArrivalType { get { return Items.FindFirst<CodeString>("40101056") as CodeString; } }
        public List<CodeString> InboundArrivalType_ { get { return Items.FindAll<CodeString>("40101056").ToList(); } }
        public ShortString CarrierID { get { return Items.FindFirst<ShortString>("40101058") as ShortString; } }
        public List<ShortString> CarrierID_ { get { return Items.FindAll<ShortString>("40101058").ToList(); } }
        public CodeString CarrierIDAssigningAuthority { get { return Items.FindFirst<CodeString>("40101059") as CodeString; } }
        public List<CodeString> CarrierIDAssigningAuthority_ { get { return Items.FindAll<CodeString>("40101059").ToList(); } }
        public FloatingPointSingle SourceOrientation { get { return Items.FindFirst<FloatingPointSingle>("40101060") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SourceOrientation_ { get { return Items.FindAll<FloatingPointSingle>("40101060").ToList(); } }
        public FloatingPointSingle SourcePosition { get { return Items.FindFirst<FloatingPointSingle>("40101061") as FloatingPointSingle; } }
        public List<FloatingPointSingle> SourcePosition_ { get { return Items.FindAll<FloatingPointSingle>("40101061").ToList(); } }
        public FloatingPointSingle BeltHeight { get { return Items.FindFirst<FloatingPointSingle>("40101062") as FloatingPointSingle; } }
        public List<FloatingPointSingle> BeltHeight_ { get { return Items.FindAll<FloatingPointSingle>("40101062").ToList(); } }
        public SequenceSelector AlgorithmRoutingCodeSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("40101064")); } }
        public List<SequenceSelector> AlgorithmRoutingCodeSequence_ { get { return Items.FindAll<Sequence>("40101064").Select(s => new SequenceSelector(s)).ToList(); } }
        public CodeString TransportClassification { get { return Items.FindFirst<CodeString>("40101067") as CodeString; } }
        public List<CodeString> TransportClassification_ { get { return Items.FindAll<CodeString>("40101067").ToList(); } }
        public LongText OOITypeDescriptor { get { return Items.FindFirst<LongText>("40101068") as LongText; } }
        public List<LongText> OOITypeDescriptor_ { get { return Items.FindAll<LongText>("40101068").ToList(); } }
        public FloatingPointSingle TotalProcessingTime { get { return Items.FindFirst<FloatingPointSingle>("40101069") as FloatingPointSingle; } }
        public List<FloatingPointSingle> TotalProcessingTime_ { get { return Items.FindAll<FloatingPointSingle>("40101069").ToList(); } }
        public OtherByteString DetectorCalibrationData { get { return Items.FindFirst<OtherByteString>("4010106C") as OtherByteString; } }
        public List<OtherByteString> DetectorCalibrationData_ { get { return Items.FindAll<OtherByteString>("4010106C").ToList(); } }
        public SequenceSelector MACParametersSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("4FFE0001")); } }
        public List<SequenceSelector> MACParametersSequence_ { get { return Items.FindAll<Sequence>("4FFE0001").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort CurveDimensionsRetired { get { return Items.FindFirst<UnsignedShort>("50xx0005") as UnsignedShort; } }
        public List<UnsignedShort> CurveDimensionsRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0005").ToList(); } }
        public UnsignedShort NumberOfPointsRetired { get { return Items.FindFirst<UnsignedShort>("50xx0010") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfPointsRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0010").ToList(); } }
        public CodeString TypeOfDataRetired { get { return Items.FindFirst<CodeString>("50xx0020") as CodeString; } }
        public List<CodeString> TypeOfDataRetired_ { get { return Items.FindAll<CodeString>("50xx0020").ToList(); } }
        public LongString CurveDescriptionRetired { get { return Items.FindFirst<LongString>("50xx0022") as LongString; } }
        public List<LongString> CurveDescriptionRetired_ { get { return Items.FindAll<LongString>("50xx0022").ToList(); } }
        public ShortString AxisUnitsRetired { get { return Items.FindFirst<ShortString>("50xx0030") as ShortString; } }
        public List<ShortString> AxisUnitsRetired_ { get { return Items.FindAll<ShortString>("50xx0030").ToList(); } }
        public ShortString AxisLabelsRetired { get { return Items.FindFirst<ShortString>("50xx0040") as ShortString; } }
        public List<ShortString> AxisLabelsRetired_ { get { return Items.FindAll<ShortString>("50xx0040").ToList(); } }
        public UnsignedShort DataValueRepresentationRetired { get { return Items.FindFirst<UnsignedShort>("50xx0103") as UnsignedShort; } }
        public List<UnsignedShort> DataValueRepresentationRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0103").ToList(); } }
        public UnsignedShort MinimumCoordinateValueRetired { get { return Items.FindFirst<UnsignedShort>("50xx0104") as UnsignedShort; } }
        public List<UnsignedShort> MinimumCoordinateValueRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0104").ToList(); } }
        public UnsignedShort MaximumCoordinateValueRetired { get { return Items.FindFirst<UnsignedShort>("50xx0105") as UnsignedShort; } }
        public List<UnsignedShort> MaximumCoordinateValueRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0105").ToList(); } }
        public ShortString CurveRangeRetired { get { return Items.FindFirst<ShortString>("50xx0106") as ShortString; } }
        public List<ShortString> CurveRangeRetired_ { get { return Items.FindAll<ShortString>("50xx0106").ToList(); } }
        public UnsignedShort CurveDataDescriptorRetired { get { return Items.FindFirst<UnsignedShort>("50xx0110") as UnsignedShort; } }
        public List<UnsignedShort> CurveDataDescriptorRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0110").ToList(); } }
        public UnsignedShort CoordinateStartValueRetired { get { return Items.FindFirst<UnsignedShort>("50xx0112") as UnsignedShort; } }
        public List<UnsignedShort> CoordinateStartValueRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0112").ToList(); } }
        public UnsignedShort CoordinateStepValueRetired { get { return Items.FindFirst<UnsignedShort>("50xx0114") as UnsignedShort; } }
        public List<UnsignedShort> CoordinateStepValueRetired_ { get { return Items.FindAll<UnsignedShort>("50xx0114").ToList(); } }
        public CodeString CurveActivationLayerRetired { get { return Items.FindFirst<CodeString>("50xx1001") as CodeString; } }
        public List<CodeString> CurveActivationLayerRetired_ { get { return Items.FindAll<CodeString>("50xx1001").ToList(); } }
        public UnsignedShort AudioTypeRetired { get { return Items.FindFirst<UnsignedShort>("50xx2000") as UnsignedShort; } }
        public List<UnsignedShort> AudioTypeRetired_ { get { return Items.FindAll<UnsignedShort>("50xx2000").ToList(); } }
        public UnsignedShort AudioSampleFormatRetired { get { return Items.FindFirst<UnsignedShort>("50xx2002") as UnsignedShort; } }
        public List<UnsignedShort> AudioSampleFormatRetired_ { get { return Items.FindAll<UnsignedShort>("50xx2002").ToList(); } }
        public UnsignedShort NumberOfChannelsRetired { get { return Items.FindFirst<UnsignedShort>("50xx2004") as UnsignedShort; } }
        public List<UnsignedShort> NumberOfChannelsRetired_ { get { return Items.FindAll<UnsignedShort>("50xx2004").ToList(); } }
        public UnsignedLong NumberOfSamplesRetired { get { return Items.FindFirst<UnsignedLong>("50xx2006") as UnsignedLong; } }
        public List<UnsignedLong> NumberOfSamplesRetired_ { get { return Items.FindAll<UnsignedLong>("50xx2006").ToList(); } }
        public UnsignedLong SampleRateRetired { get { return Items.FindFirst<UnsignedLong>("50xx2008") as UnsignedLong; } }
        public List<UnsignedLong> SampleRateRetired_ { get { return Items.FindAll<UnsignedLong>("50xx2008").ToList(); } }
        public UnsignedLong TotalTimeRetired { get { return Items.FindFirst<UnsignedLong>("50xx200A") as UnsignedLong; } }
        public List<UnsignedLong> TotalTimeRetired_ { get { return Items.FindAll<UnsignedLong>("50xx200A").ToList(); } }
        public OtherWordString AudioSampleDataRetired { get { return Items.FindFirst<OtherWordString>("50xx200C") as OtherWordString; } }
        public List<OtherWordString> AudioSampleDataRetired_ { get { return Items.FindAll<OtherWordString>("50xx200C").ToList(); } }
        public LongText AudioCommentsRetired { get { return Items.FindFirst<LongText>("50xx200E") as LongText; } }
        public List<LongText> AudioCommentsRetired_ { get { return Items.FindAll<LongText>("50xx200E").ToList(); } }
        public LongString CurveLabelRetired { get { return Items.FindFirst<LongString>("50xx2500") as LongString; } }
        public List<LongString> CurveLabelRetired_ { get { return Items.FindAll<LongString>("50xx2500").ToList(); } }
        public SequenceSelector CurveReferencedOverlaySequenceRetired { get { return new SequenceSelector(Items.FindFirst<Sequence>("50xx2600")); } }
        public List<SequenceSelector> CurveReferencedOverlaySequenceRetired_ { get { return Items.FindAll<Sequence>("50xx2600").Select(s => new SequenceSelector(s)).ToList(); } }
        public UnsignedShort CurveReferencedOverlayGroupRetired { get { return Items.FindFirst<UnsignedShort>("50xx2610") as UnsignedShort; } }
        public List<UnsignedShort> CurveReferencedOverlayGroupRetired_ { get { return Items.FindAll<UnsignedShort>("50xx2610").ToList(); } }
        public OtherWordString CurveDataRetired { get { return Items.FindFirst<OtherWordString>("50xx3000") as OtherWordString; } }
        public List<OtherWordString> CurveDataRetired_ { get { return Items.FindAll<OtherWordString>("50xx3000").ToList(); } }
        public SequenceSelector SharedFunctionalGroupsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("52009229")); } }
        public List<SequenceSelector> SharedFunctionalGroupsSequence_ { get { return Items.FindAll<Sequence>("52009229").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector PerFrameFunctionalGroupsSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("52009230")); } }
        public List<SequenceSelector> PerFrameFunctionalGroupsSequence_ { get { return Items.FindAll<Sequence>("52009230").Select(s => new SequenceSelector(s)).ToList(); } }
        public SequenceSelector WaveformSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("54000100")); } }
        public List<SequenceSelector> WaveformSequence_ { get { return Items.FindAll<Sequence>("54000100").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString ChannelMinimumValue { get { return Items.FindFirst<OtherByteString>("54000110") as OtherByteString; } }
        public List<OtherByteString> ChannelMinimumValue_ { get { return Items.FindAll<OtherByteString>("54000110").ToList(); } }
        public OtherByteString ChannelMaximumValue { get { return Items.FindFirst<OtherByteString>("54000112") as OtherByteString; } }
        public List<OtherByteString> ChannelMaximumValue_ { get { return Items.FindAll<OtherByteString>("54000112").ToList(); } }
        public UnsignedShort WaveformBitsAllocated { get { return Items.FindFirst<UnsignedShort>("54001004") as UnsignedShort; } }
        public List<UnsignedShort> WaveformBitsAllocated_ { get { return Items.FindAll<UnsignedShort>("54001004").ToList(); } }
        public CodeString WaveformSampleInterpretation { get { return Items.FindFirst<CodeString>("54001006") as CodeString; } }
        public List<CodeString> WaveformSampleInterpretation_ { get { return Items.FindAll<CodeString>("54001006").ToList(); } }
        public OtherByteString WaveformPaddingValue { get { return Items.FindFirst<OtherByteString>("5400100A") as OtherByteString; } }
        public List<OtherByteString> WaveformPaddingValue_ { get { return Items.FindAll<OtherByteString>("5400100A").ToList(); } }
        public OtherByteString WaveformData { get { return Items.FindFirst<OtherByteString>("54001010") as OtherByteString; } }
        public List<OtherByteString> WaveformData_ { get { return Items.FindAll<OtherByteString>("54001010").ToList(); } }
        public OtherFloatString FirstOrderPhaseCorrectionAngle { get { return Items.FindFirst<OtherFloatString>("56000010") as OtherFloatString; } }
        public List<OtherFloatString> FirstOrderPhaseCorrectionAngle_ { get { return Items.FindAll<OtherFloatString>("56000010").ToList(); } }
        public OtherFloatString SpectroscopyData { get { return Items.FindFirst<OtherFloatString>("56000020") as OtherFloatString; } }
        public List<OtherFloatString> SpectroscopyData_ { get { return Items.FindAll<OtherFloatString>("56000020").ToList(); } }
        public UnsignedShort OverlayRows { get { return Items.FindFirst<UnsignedShort>("60xx0010") as UnsignedShort; } }
        public List<UnsignedShort> OverlayRows_ { get { return Items.FindAll<UnsignedShort>("60xx0010").ToList(); } }
        public UnsignedShort OverlayColumns { get { return Items.FindFirst<UnsignedShort>("60xx0011") as UnsignedShort; } }
        public List<UnsignedShort> OverlayColumns_ { get { return Items.FindAll<UnsignedShort>("60xx0011").ToList(); } }
        public UnsignedShort OverlayPlanesRetired { get { return Items.FindFirst<UnsignedShort>("60xx0012") as UnsignedShort; } }
        public List<UnsignedShort> OverlayPlanesRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0012").ToList(); } }
        public IntegerString NumberOfFramesInOverlay { get { return Items.FindFirst<IntegerString>("60xx0015") as IntegerString; } }
        public List<IntegerString> NumberOfFramesInOverlay_ { get { return Items.FindAll<IntegerString>("60xx0015").ToList(); } }
        public LongString OverlayDescription { get { return Items.FindFirst<LongString>("60xx0022") as LongString; } }
        public List<LongString> OverlayDescription_ { get { return Items.FindAll<LongString>("60xx0022").ToList(); } }
        public CodeString OverlayType { get { return Items.FindFirst<CodeString>("60xx0040") as CodeString; } }
        public List<CodeString> OverlayType_ { get { return Items.FindAll<CodeString>("60xx0040").ToList(); } }
        public LongString OverlaySubtype { get { return Items.FindFirst<LongString>("60xx0045") as LongString; } }
        public List<LongString> OverlaySubtype_ { get { return Items.FindAll<LongString>("60xx0045").ToList(); } }
        public SignedShort OverlayOrigin { get { return Items.FindFirst<SignedShort>("60xx0050") as SignedShort; } }
        public List<SignedShort> OverlayOrigin_ { get { return Items.FindAll<SignedShort>("60xx0050").ToList(); } }
        public UnsignedShort ImageFrameOrigin { get { return Items.FindFirst<UnsignedShort>("60xx0051") as UnsignedShort; } }
        public List<UnsignedShort> ImageFrameOrigin_ { get { return Items.FindAll<UnsignedShort>("60xx0051").ToList(); } }
        public UnsignedShort OverlayPlaneOriginRetired { get { return Items.FindFirst<UnsignedShort>("60xx0052") as UnsignedShort; } }
        public List<UnsignedShort> OverlayPlaneOriginRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0052").ToList(); } }
        public CodeString OverlayCompressionCodeRetired { get { return Items.FindFirst<CodeString>("60xx0060") as CodeString; } }
        public List<CodeString> OverlayCompressionCodeRetired_ { get { return Items.FindAll<CodeString>("60xx0060").ToList(); } }
        public ShortString OverlayCompressionOriginatorRetired { get { return Items.FindFirst<ShortString>("60xx0061") as ShortString; } }
        public List<ShortString> OverlayCompressionOriginatorRetired_ { get { return Items.FindAll<ShortString>("60xx0061").ToList(); } }
        public ShortString OverlayCompressionLabelRetired { get { return Items.FindFirst<ShortString>("60xx0062") as ShortString; } }
        public List<ShortString> OverlayCompressionLabelRetired_ { get { return Items.FindAll<ShortString>("60xx0062").ToList(); } }
        public CodeString OverlayCompressionDescriptionRetired { get { return Items.FindFirst<CodeString>("60xx0063") as CodeString; } }
        public List<CodeString> OverlayCompressionDescriptionRetired_ { get { return Items.FindAll<CodeString>("60xx0063").ToList(); } }
        public AttributeTag OverlayCompressionStepPointersRetired { get { return Items.FindFirst<AttributeTag>("60xx0066") as AttributeTag; } }
        public List<AttributeTag> OverlayCompressionStepPointersRetired_ { get { return Items.FindAll<AttributeTag>("60xx0066").ToList(); } }
        public UnsignedShort OverlayRepeatIntervalRetired { get { return Items.FindFirst<UnsignedShort>("60xx0068") as UnsignedShort; } }
        public List<UnsignedShort> OverlayRepeatIntervalRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0068").ToList(); } }
        public UnsignedShort OverlayBitsGroupedRetired { get { return Items.FindFirst<UnsignedShort>("60xx0069") as UnsignedShort; } }
        public List<UnsignedShort> OverlayBitsGroupedRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0069").ToList(); } }
        public UnsignedShort OverlayBitsAllocated { get { return Items.FindFirst<UnsignedShort>("60xx0100") as UnsignedShort; } }
        public List<UnsignedShort> OverlayBitsAllocated_ { get { return Items.FindAll<UnsignedShort>("60xx0100").ToList(); } }
        public UnsignedShort OverlayBitPosition { get { return Items.FindFirst<UnsignedShort>("60xx0102") as UnsignedShort; } }
        public List<UnsignedShort> OverlayBitPosition_ { get { return Items.FindAll<UnsignedShort>("60xx0102").ToList(); } }
        public CodeString OverlayFormatRetired { get { return Items.FindFirst<CodeString>("60xx0110") as CodeString; } }
        public List<CodeString> OverlayFormatRetired_ { get { return Items.FindAll<CodeString>("60xx0110").ToList(); } }
        public UnsignedShort OverlayLocationRetired { get { return Items.FindFirst<UnsignedShort>("60xx0200") as UnsignedShort; } }
        public List<UnsignedShort> OverlayLocationRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0200").ToList(); } }
        public CodeString OverlayCodeLabelRetired { get { return Items.FindFirst<CodeString>("60xx0800") as CodeString; } }
        public List<CodeString> OverlayCodeLabelRetired_ { get { return Items.FindAll<CodeString>("60xx0800").ToList(); } }
        public UnsignedShort OverlayNumberOfTablesRetired { get { return Items.FindFirst<UnsignedShort>("60xx0802") as UnsignedShort; } }
        public List<UnsignedShort> OverlayNumberOfTablesRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0802").ToList(); } }
        public AttributeTag OverlayCodeTableLocationRetired { get { return Items.FindFirst<AttributeTag>("60xx0803") as AttributeTag; } }
        public List<AttributeTag> OverlayCodeTableLocationRetired_ { get { return Items.FindAll<AttributeTag>("60xx0803").ToList(); } }
        public UnsignedShort OverlayBitsForCodeWordRetired { get { return Items.FindFirst<UnsignedShort>("60xx0804") as UnsignedShort; } }
        public List<UnsignedShort> OverlayBitsForCodeWordRetired_ { get { return Items.FindAll<UnsignedShort>("60xx0804").ToList(); } }
        public CodeString OverlayActivationLayer { get { return Items.FindFirst<CodeString>("60xx1001") as CodeString; } }
        public List<CodeString> OverlayActivationLayer_ { get { return Items.FindAll<CodeString>("60xx1001").ToList(); } }
        public UnsignedShort OverlayDescriptorGrayRetired { get { return Items.FindFirst<UnsignedShort>("60xx1100") as UnsignedShort; } }
        public List<UnsignedShort> OverlayDescriptorGrayRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1100").ToList(); } }
        public UnsignedShort OverlayDescriptorRedRetired { get { return Items.FindFirst<UnsignedShort>("60xx1101") as UnsignedShort; } }
        public List<UnsignedShort> OverlayDescriptorRedRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1101").ToList(); } }
        public UnsignedShort OverlayDescriptorGreenRetired { get { return Items.FindFirst<UnsignedShort>("60xx1102") as UnsignedShort; } }
        public List<UnsignedShort> OverlayDescriptorGreenRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1102").ToList(); } }
        public UnsignedShort OverlayDescriptorBlueRetired { get { return Items.FindFirst<UnsignedShort>("60xx1103") as UnsignedShort; } }
        public List<UnsignedShort> OverlayDescriptorBlueRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1103").ToList(); } }
        public UnsignedShort OverlaysGrayRetired { get { return Items.FindFirst<UnsignedShort>("60xx1200") as UnsignedShort; } }
        public List<UnsignedShort> OverlaysGrayRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1200").ToList(); } }
        public UnsignedShort OverlaysRedRetired { get { return Items.FindFirst<UnsignedShort>("60xx1201") as UnsignedShort; } }
        public List<UnsignedShort> OverlaysRedRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1201").ToList(); } }
        public UnsignedShort OverlaysGreenRetired { get { return Items.FindFirst<UnsignedShort>("60xx1202") as UnsignedShort; } }
        public List<UnsignedShort> OverlaysGreenRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1202").ToList(); } }
        public UnsignedShort OverlaysBlueRetired { get { return Items.FindFirst<UnsignedShort>("60xx1203") as UnsignedShort; } }
        public List<UnsignedShort> OverlaysBlueRetired_ { get { return Items.FindAll<UnsignedShort>("60xx1203").ToList(); } }
        public IntegerString ROIArea { get { return Items.FindFirst<IntegerString>("60xx1301") as IntegerString; } }
        public List<IntegerString> ROIArea_ { get { return Items.FindAll<IntegerString>("60xx1301").ToList(); } }
        public DecimalString ROIMean { get { return Items.FindFirst<DecimalString>("60xx1302") as DecimalString; } }
        public List<DecimalString> ROIMean_ { get { return Items.FindAll<DecimalString>("60xx1302").ToList(); } }
        public DecimalString ROIStandardDeviation { get { return Items.FindFirst<DecimalString>("60xx1303") as DecimalString; } }
        public List<DecimalString> ROIStandardDeviation_ { get { return Items.FindAll<DecimalString>("60xx1303").ToList(); } }
        public LongString OverlayLabel { get { return Items.FindFirst<LongString>("60xx1500") as LongString; } }
        public List<LongString> OverlayLabel_ { get { return Items.FindAll<LongString>("60xx1500").ToList(); } }
        public OtherByteString OverlayData { get { return Items.FindFirst<OtherByteString>("60xx3000") as OtherByteString; } }
        public List<OtherByteString> OverlayData_ { get { return Items.FindAll<OtherByteString>("60xx3000").ToList(); } }
        public LongText OverlayCommentsRetired { get { return Items.FindFirst<LongText>("60xx4000") as LongText; } }
        public List<LongText> OverlayCommentsRetired_ { get { return Items.FindAll<LongText>("60xx4000").ToList(); } }
        public OtherWordString PixelData { get { return Items.FindFirst<OtherWordString>("7FE00010") as OtherWordString; } }
        public List<OtherWordString> PixelData_ { get { return Items.FindAll<OtherWordString>("7FE00010").ToList(); } }
        public OtherWordString CoefficientsSDVNRetired { get { return Items.FindFirst<OtherWordString>("7FE00020") as OtherWordString; } }
        public List<OtherWordString> CoefficientsSDVNRetired_ { get { return Items.FindAll<OtherWordString>("7FE00020").ToList(); } }
        public OtherWordString CoefficientsSDHNRetired { get { return Items.FindFirst<OtherWordString>("7FE00030") as OtherWordString; } }
        public List<OtherWordString> CoefficientsSDHNRetired_ { get { return Items.FindAll<OtherWordString>("7FE00030").ToList(); } }
        public OtherWordString CoefficientsSDDNRetired { get { return Items.FindFirst<OtherWordString>("7FE00040") as OtherWordString; } }
        public List<OtherWordString> CoefficientsSDDNRetired_ { get { return Items.FindAll<OtherWordString>("7FE00040").ToList(); } }
        public OtherWordString VariablePixelDataRetired { get { return Items.FindFirst<OtherWordString>("7Fxx0010") as OtherWordString; } }
        public List<OtherWordString> VariablePixelDataRetired_ { get { return Items.FindAll<OtherWordString>("7Fxx0010").ToList(); } }
        public UnsignedShort VariableNextDataGroupRetired { get { return Items.FindFirst<UnsignedShort>("7Fxx0011") as UnsignedShort; } }
        public List<UnsignedShort> VariableNextDataGroupRetired_ { get { return Items.FindAll<UnsignedShort>("7Fxx0011").ToList(); } }
        public OtherWordString VariableCoefficientsSDVNRetired { get { return Items.FindFirst<OtherWordString>("7Fxx0020") as OtherWordString; } }
        public List<OtherWordString> VariableCoefficientsSDVNRetired_ { get { return Items.FindAll<OtherWordString>("7Fxx0020").ToList(); } }
        public OtherWordString VariableCoefficientsSDHNRetired { get { return Items.FindFirst<OtherWordString>("7Fxx0030") as OtherWordString; } }
        public List<OtherWordString> VariableCoefficientsSDHNRetired_ { get { return Items.FindAll<OtherWordString>("7Fxx0030").ToList(); } }
        public OtherWordString VariableCoefficientsSDDNRetired { get { return Items.FindFirst<OtherWordString>("7Fxx0040") as OtherWordString; } }
        public List<OtherWordString> VariableCoefficientsSDDNRetired_ { get { return Items.FindAll<OtherWordString>("7Fxx0040").ToList(); } }
        public SequenceSelector DigitalSignaturesSequence { get { return new SequenceSelector(Items.FindFirst<Sequence>("FFFAFFFA")); } }
        public List<SequenceSelector> DigitalSignaturesSequence_ { get { return Items.FindAll<Sequence>("FFFAFFFA").Select(s => new SequenceSelector(s)).ToList(); } }
        public OtherByteString DataSetTrailingPadding { get { return Items.FindFirst<OtherByteString>("FFFCFFFC") as OtherByteString; } }
        public List<OtherByteString> DataSetTrailingPadding_ { get { return Items.FindAll<OtherByteString>("FFFCFFFC").ToList(); } }

        #endregion
    }
}
