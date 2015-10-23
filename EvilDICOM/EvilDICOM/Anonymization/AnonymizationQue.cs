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
    public class AnonymizationQue
    {
        public AnonymizationQue()
        {
            Que = new List<IAnonymizer>();
        }

        public static AnonymizationQue BuildQue(AnonymizationSettings settings, IEnumerable<string> dcmFiles)
        {
            var anonQue = new AnonymizationQue();

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
                if (studyAnon != null) { studyAnon.FinalizeDictionary(); anonQue.Que.Add(studyAnon); }
                if (uidAnon != null) { anonQue.Que.Add(uidAnon); }
            }
            if (settings.DoAnonymizeNames) { anonQue.Que.Add(new NameAnonymizer()); }
            if (settings.DoRemovePrivateTags) { anonQue.Que.Add(new PrivateTagAnonymizer()); }
            if (settings.DoDICOMProfile) { anonQue.Que.Add(new ProfileAnonymizer()); }
            anonQue.Que.Add(new PatientIdAnonymizer(settings.FirstName, settings.LastName, settings.Id));
            anonQue.Que.Add(new DateAnonymizer(settings.DateSettings));
            return anonQue;
        }

        public AnonymizationQue Default(IEnumerable<string> dcmFiles)
        {
            return BuildQue(AnonymizationSettings.Default, dcmFiles);
        }

        public void Anonymize(DICOMObject dcm)
        {
            int i = 1;
            foreach (var anony in Que)
            {
                anony.Anonymize(dcm);
                RaiseProgressUpdated(CalculateProgress(i, Que.Count + 1) / 100);
                i++;
            }
        }

        public List<IAnonymizer> Que { get; set; }

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
