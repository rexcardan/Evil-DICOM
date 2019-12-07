#region

using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindStudyIOD : CFindRequestIOD
    {
        public CFindStudyIOD()
        {
            QueryLevel = QueryLevel.STUDY;
            StudyDate = null;
            AccessionNumber = string.Empty;
            StudyId = string.Empty;
            StudyInstanceUID = string.Empty;
        }

        public CFindStudyIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public S.DateTime? StudyDate
        {
            get { return _sel.StudyDate != null ? _sel.StudyDate.Data : null; }
            set { _sel.Forge(DF.StudyDate(value)); }
        }

        public S.DateTime? StudyTime
        {
            get { return _sel.StudyTime != null ? _sel.StudyTime.Data : null; }
            set { _sel.Forge(DF.StudyTime(value)); }
        }

        public string AccessionNumber
        {
            get { return _sel.AccessionNumber != null ? _sel.AccessionNumber.Data : null; }
            set { _sel.Forge(DF.AccessionNumber(value)); }
        }

        public string StudyId
        {
            get { return _sel.StudyID != null ? _sel.StudyID.Data : null; }
            set { _sel.Forge(DF.StudyID(value)); }
        }

        public string StudyInstanceUID
        {
            get { return _sel.StudyInstanceUID != null ? _sel.StudyInstanceUID.Data : null; }
            set { _sel.Forge(DF.StudyInstanceUID(value)); }
        }
    }
}