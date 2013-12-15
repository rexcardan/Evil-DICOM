using System.Collections.Generic;
using System.ComponentModel;

namespace EvilDICOM.Facade
{
    public class MedicalCase
    {
        List<Study> _studiesList = new List<Study>(); 
        List<Series> _seriesList = new List<Series>(); 
        Patient _patient = new Patient();

        public MedicalCase()
        {
        }

        public MedicalCase(Patient patient)
        {
            _patient = patient;
        }
    }
}