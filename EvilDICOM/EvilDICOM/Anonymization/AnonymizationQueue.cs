#region

using System.Collections.Generic;
using EvilDICOM.Anonymization.Anonymizers;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Core;

#endregion

namespace EvilDICOM.Anonymization
{
    /// <summary>
    /// This class is for building a stack of anonymization options and then executing
    /// </summary>
    public class AnonymizationQueue
    {
        //PROGRESS REPORTING (for UI)
        public delegate void ProgressUpdatedHandler(double progress);

        public AnonymizationQueue()
        {
            Queue = new List<IAnonymizer>();
        }

        /// <summary>
        /// The current list of anonymization settings
        /// </summary>
        public List<IAnonymizer> Queue { get; set; }

        public static AnonymizationQueue BuildQueue(AnonymizationSettings settings, IEnumerable<string> dcmFiles)
        {
            var anonQue = new AnonymizationQueue();

            if (settings.DoAnonymizeStudyIDs || settings.DoAnonymizeUIDs)
            {
                var studyAnon = settings.DoAnonymizeStudyIDs ? new StudyIdAnonymizer() : null;
                var uidAnon = settings.DoAnonymizeUIDs ? new UIDAnonymizer() : null;
                foreach (var file in dcmFiles)
                {
                    var ob = DICOMObject.Read(file);
                    if (uidAnon != null) uidAnon.AddDICOMObject(ob);
                    if (studyAnon != null) studyAnon.AddDICOMObject(ob);
                }
                if (studyAnon != null)
                {
                    studyAnon.FinalizeDictionary();
                    anonQue.Queue.Add(studyAnon);
                }
                if (uidAnon != null) anonQue.Queue.Add(uidAnon);
            }
            if (settings.DoAnonymizeNames) anonQue.Queue.Add(new NameAnonymizer());
            if (settings.DoRemovePrivateTags) anonQue.Queue.Add(new PrivateTagAnonymizer());
            if (settings.DoDICOMProfile) anonQue.Queue.Add(new ProfileAnonymizer());
            anonQue.Queue.Add(new PatientIdAnonymizer(settings.FirstName, settings.LastName, settings.Id));
            anonQue.Queue.Add(new DateAnonymizer(settings.DateSettings));
            return anonQue;
        }

        /// <summary>
        /// Initializes an anonymization queue from a list of DICOM files that need to be anonymized
        /// </summary>
        /// <param name="dcmFiles"></param>
        /// <returns>the anonymization queue with default options</returns>
        public AnonymizationQueue Default(IEnumerable<string> dcmFiles)
        {
            return BuildQueue(AnonymizationSettings.Default, dcmFiles);
        }

        /// <summary>
        /// This method needs to be called on each DICOM object (file) to be anonymized with the set options
        /// </summary>
        /// <param name="dcm">the DICOM object (file) to be anonymized</param>
        public void Anonymize(DICOMObject dcm)
        {
            var i = 1;
            foreach (var anony in Queue)
            {
                anony.Anonymize(dcm);
                RaiseProgressUpdated(CalculateProgress(i, Queue.Count + 1) / 100);
                i++;
            }
        }

        /// <summary>
        /// Calculates a value for a progress update
        /// </summary>
        /// <param name="i"></param>
        /// <param name="totalOperations"></param>
        /// <returns></returns>
        private double CalculateProgress(int i, int totalOperations)
        {
            return (int) (i / (double) totalOperations * 100);
        }

        /// <summary>
        /// Reports a double value of the current progress from 0 to 100
        /// </summary>
        public event ProgressUpdatedHandler ProgressUpdated;

        public void RaiseProgressUpdated(double progress)
        {
            if (ProgressUpdated != null)
                ProgressUpdated(progress);
        }
    }
}