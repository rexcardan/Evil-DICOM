using System;
using EvilDICOM.Network.Enums;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    /// <summary>
    /// Class CMoveIOD.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.DIMSE.IOD.AbstractDIMSEIOD" />
    public class CMoveIOD : AbstractDIMSEIOD
    {
        /// <summary>
        /// Gets or sets the query level.
        /// </summary>
        /// <value>The query level.</value>
        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.QueryRetrieveLevel == null)
                {
                    _sel.QueryRetrieveLevel.Data = QueryLevel.PATIENT.ToString();
                }
                return (QueryLevel) Enum.Parse(typeof (QueryLevel), _sel.QueryRetrieveLevel.Data);
            }
            set { _sel.Forge(DF.QueryRetrieveLevel).Data = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the specific character seet.
        /// </summary>
        /// <value>The specific character seet.</value>
        public string SpecificCharacterSeet
        {
            get { return _sel.SpecificCharacterSet != null ? _sel.SpecificCharacterSet.Data : null; }
            set { _sel.Forge(DF.SpecificCharacterSet).Data = value; }
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        public string PatientId
        {
            get { return _sel.PatientID != null ? _sel.PatientID.Data : null; }
            set { _sel.Forge(DF.PatientID).Data = value; }
        }

        /// <summary>
        /// Gets or sets the study instance uid.
        /// </summary>
        /// <value>The study instance uid.</value>
        public string StudyInstanceUID
        {
            get { return _sel.StudyInstanceUID != null ? _sel.StudyInstanceUID.Data : null; }
            set { _sel.Forge(DF.StudyInstanceUID).Data = value; }
        }

        /// <summary>
        /// Gets or sets the sop instance uid.
        /// </summary>
        /// <value>The sop instance uid.</value>
        public string SOPInstanceUID
        {
            get { return _sel.SOPInstanceUID != null ? _sel.SOPInstanceUID.Data : null; }
            set { _sel.Forge(DF.SOPInstanceUID).Data = value; }
        }

        /// <summary>
        /// Gets or sets the series instance uid.
        /// </summary>
        /// <value>The series instance uid.</value>
        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID).Data = value; }
        }
    }
}