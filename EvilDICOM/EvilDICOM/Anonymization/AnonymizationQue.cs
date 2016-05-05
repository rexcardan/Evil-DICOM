using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Anonymization.Anonymizers;

namespace EvilDICOM.Anonymization
{
    public class AnonymizationQueue
    {
        public AnonymizationQueue()
        {
            Queue = new List<IAnonymizer>();
        }

        public static AnonymizationQueue BuildQueue(AnonymizationSettings settings, IEnumerable<string> dcmFiles)
        {
            var anonQue = new AnonymizationQueue();

            if (settings.DoAnonymizeStudyIDs || settings.DoAnonymizeUIDs)
            {
                StudyIdAnonymizer studyAnon = settings.DoAnonymizeStudyIDs ? new StudyIdAnonymizer() : null;
                UIDAnonymizer uidAnon = settings.DoAnonymizeUIDs ? new UIDAnonymizer() : null;
                foreach (var file in dcmFiles)
                {
                    var ob = DICOMObject.Read(file);
                    if (uidAnon != null) uidAnon.AddDICOMObject(ob);
                    if (studyAnon != null) studyAnon.AddDICOMObject(ob);
                }
                if (studyAnon != null) { studyAnon.FinalizeDictionary(); anonQue.Queue.Add(studyAnon); }
                if (uidAnon != null) { anonQue.Queue.Add(uidAnon); }
            }
            if (settings.DoAnonymizeNames) { anonQue.Queue.Add(new NameAnonymizer()); }
            if (settings.DoRemovePrivateTags) { anonQue.Queue.Add(new PrivateTagAnonymizer()); }
            if (settings.DoDICOMProfile) { anonQue.Queue.Add(new ProfileAnonymizer()); }
            anonQue.Queue.Add(new PatientIdAnonymizer(settings.FirstName, settings.LastName, settings.Id));
            anonQue.Queue.Add(new DateAnonymizer(settings.DateSettings));
            return anonQue;
        }

        public async static Task<AnonymizationQueue> BuildQueueAsync(AnonymizationSettings settings, IEnumerable<string> dcmFiles)
        {
            return await Task.Run(() => BuildQueue(settings, dcmFiles));
        }

        /// <summary>
        /// Builds a standard anonymization que with defaul parameters
        /// </summary>
        /// <param name="dcmFiles">a collection of file paths to anonymize</param>
        /// <returns>the default queue</returns>
        public static AnonymizationQueue GetDefault(IEnumerable<string> dcmFiles)
        {
            return BuildQueue(AnonymizationSettings.Default, dcmFiles);
        }

        /// <summary>
        /// Builds a standard anonymization que with defaul parameters
        /// </summary>
        /// <param name="dcmFiles">a collection of file paths to anonymize</param>
        /// <returns>the default queue</returns>
        public async static Task<AnonymizationQueue> GetDefaultAsync(IEnumerable<string> dcmFiles)
        {
            return await BuildQueueAsync(AnonymizationSettings.Default, dcmFiles);
        }

        public void Anonymize(DICOMObject dcm)
        {
            int i = 1;
            foreach (var anony in Queue)
            {
                anony.Anonymize(dcm);
                RaiseProgressUpdated(CalculateProgress(i, Queue.Count + 1) / 100);
                i++;
            }
        }

        public List<IAnonymizer> Queue { get; set; }

        private double CalculateProgress(int i, int totalOperations)
        {
            return (int)((double)i / (double)(totalOperations) * 100);
        }

        //PROGRESS REPORTING (for UI)
        public delegate void ProgressUpdatedHandler(double progress);

        public event ProgressUpdatedHandler ProgressUpdated;

        public void RaiseProgressUpdated(double progress)
        {
            if (ProgressUpdated != null)
            {
                ProgressUpdated(progress);
            }
        }
    }
}
