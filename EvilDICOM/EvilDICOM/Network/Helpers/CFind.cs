#region

using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.Helpers
{
    public class CFind
    {
        public static CFindRequest CreateStudyQuery(string patientId)
        {
            var iod = new CFindStudyIOD();
            iod.PatientId = patientId;
            return new CFindRequest(iod, Root.STUDY);
        }

        public static CFindRequest CreateSeriesQuery(string studyUid)
        {
            var iod = new CFindSeriesIOD();
            iod.StudyInstanceUID = studyUid;
            return new CFindRequest(iod, Root.STUDY);
        }

        public static CFindRequest CreateImageQuery(string seriesUid)
        {
            var iod = new CFindImageIOD();
            iod.SeriesInstanceUID = seriesUid;
            return new CFindRequest(iod, Root.STUDY);
        }
    }
}