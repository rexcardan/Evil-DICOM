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
    /// <summary>
    /// Class AnonymizationQueue.
    /// </summary>
    public class AnonymizationQueue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymizationQueue"/> class.
        /// </summary>
        public AnonymizationQueue()
        {
            Queue = new List<IAnonymizer>();
        }

        /// <summary>
        /// Builds the queue.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="dcmFiles">The DCM files.</param>
        /// <returns>AnonymizationQueue.</returns>
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

        /// <summary>
        /// build queue as an asynchronous operation.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="dcmFiles">The DCM files.</param>
        /// <returns>Task&lt;AnonymizationQueue&gt;.</returns>
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

        /// <summary>
        /// Anonymizes the specified DCM.
        /// </summary>
        /// <param name="dcm">The DCM.</param>
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

        /// <summary>
        /// Gets or sets the queue.
        /// </summary>
        /// <value>The queue.</value>
        public List<IAnonymizer> Queue { get; set; }

        /// <summary>
        /// Calculates the progress.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="totalOperations">The total operations.</param>
        /// <returns>System.Double.</returns>
        private double CalculateProgress(int i, int totalOperations)
        {
            return (int)((double)i / (double)(totalOperations) * 100);
        }

        //PROGRESS REPORTING (for UI)
        /// <summary>
        /// Delegate ProgressUpdatedHandler
        /// </summary>
        /// <param name="progress">The progress.</param>
        public delegate void ProgressUpdatedHandler(double progress);

        /// <summary>
        /// Occurs when [progress updated].
        /// </summary>
        public event ProgressUpdatedHandler ProgressUpdated;

        /// <summary>
        /// Raises the progress updated.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void RaiseProgressUpdated(double progress)
        {
            if (ProgressUpdated != null)
            {
                ProgressUpdated(progress);
            }
        }
    }
}
