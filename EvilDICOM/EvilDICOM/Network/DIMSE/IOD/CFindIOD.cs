using System.Runtime.Serialization;
using EvilDICOM.Core.Element;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using EvilDICOM.Core.Interfaces;
using System.Collections.Generic;
using EvilDICOM.Core;

namespace EvilDICOM.Network.DIMSE.IOD
{
    /// <summary>
    /// Class CFindIOD.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.DIMSE.IOD.AbstractDIMSEIOD" />
    public class CFindIOD : AbstractDIMSEIOD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CFindIOD"/> class.
        /// </summary>
        /// <param name="level">The level.</param>
        public CFindIOD(QueryLevel level)
        {
            QueryLevel = level;
            SOPInstanceUID = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CFindIOD"/> class.
        /// </summary>
        /// <param name="dcm">The DCM.</param>
        public CFindIOD(DICOMObject dcm) : base(dcm) { }

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
                return (QueryLevel) S.Enum.Parse(typeof (QueryLevel), _sel.QueryRetrieveLevel.Data);
            }
            set { _sel.Forge(DF.QueryRetrieveLevel).Data = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the name of the patients.
        /// </summary>
        /// <value>The name of the patients.</value>
        public PersonName PatientsName
        {
            get { return _sel.PatientName != null ? _sel.PatientName : null; }
            set { _sel.PatientName = value; }
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
        /// Gets or sets the study date.
        /// </summary>
        /// <value>The study date.</value>
        public S.DateTime? StudyDate
        {
            get { return _sel.StudyDate != null ? _sel.StudyDate.Data : null; }
            set
            {
                _sel.Forge(DF.StudyDate).Data = value;
            }
        }

        /// <summary>
        /// Gets or sets the study time.
        /// </summary>
        /// <value>The study time.</value>
        public S.DateTime? StudyTime
        {
            get { return _sel.StudyTime != null ? _sel.StudyTime.Data : null; }
            set { _sel.Forge(DF.StudyTime).Data = value; }
        }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber
        {
            get { return _sel.AccessionNumber != null ? _sel.AccessionNumber.Data : null; }
            set { _sel.Forge(DF.AccessionNumber).Data = value; }
        }

        /// <summary>
        /// Gets or sets the study identifier.
        /// </summary>
        /// <value>The study identifier.</value>
        public string StudyId
        {
            get { return _sel.StudyID != null ? _sel.StudyID.Data : null; }
            set { _sel.Forge(DF.StudyID).Data = value; }
        }

        /// <summary>
        /// Gets or sets the study description.
        /// </summary>
        /// <value>The study description.</value>
        public string StudyDescription
        {
            get { return _sel.StudyDescription != null ? _sel.StudyDescription.Data : null; }
            set { _sel.Forge(DF.StudyDescription).Data = value; }
        }

        /// <summary>
        /// Gets or sets the name of the referring physician.
        /// </summary>
        /// <value>The name of the referring physician.</value>
        public string ReferringPhysicianName
        {
            get { return _sel.ReferringPhysicianName != null ? _sel.ReferringPhysicianName.Data : null; }
            set { _sel.Forge(DF.ReferringPhysicianName).Data = value; }
        }

        /// <summary>
        /// Gets or sets the name of physicians reading study.
        /// </summary>
        /// <value>The name of physicians reading study.</value>
        public string NameOfPhysiciansReadingStudy
        {
            get { return _sel.NameOfPhysiciansReadingStudy != null ? _sel.NameOfPhysiciansReadingStudy.Data : null; }
            set { _sel.Forge(DF.NameOfPhysiciansReadingStudy).Data = value; }
        }

        /// <summary>
        /// Gets or sets the modalities in study.
        /// </summary>
        /// <value>The modalities in study.</value>
        public string ModalitiesInStudy
        {
            get { return _sel.ModalitiesInStudy != null ? _sel.ModalitiesInStudy.Data : null; }
            set { _sel.Forge(DF.ModalitiesInStudy).Data = value; }
        }

        /// <summary>
        /// Gets or sets the patient birth date.
        /// </summary>
        /// <value>The patient birth date.</value>
        public S.DateTime? PatientBirthDate
        {
            get { return _sel.PatientBirthDate != null ? _sel.PatientBirthDate.Data : null; }
            set { _sel.Forge(DF.PatientBirthDate).Data = value; }
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
        /// Gets or sets the series instance uid.
        /// </summary>
        /// <value>The series instance uid.</value>
        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID).Data = value; }
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
    }
}