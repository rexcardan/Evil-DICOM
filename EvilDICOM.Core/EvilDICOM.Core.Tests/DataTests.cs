using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.Helpers;
using E=EvilDICOM.Core.Element;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void WorkingWithSingleValuesTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);

            //The patient's name IDICOMElement
            var pName = dcm.FindFirst(TagHelper.PATIENT_NAME);

            //The patient's name strong typed for better data access
            var strongName = dcm.FindFirst(TagHelper.PATIENT_NAME) as PersonName;
            var firstName = strongName.FirstName;
            var lastName = strongName.LastName;

            //You can manipulate this way also
            strongName.FirstName = "Fred";
            strongName.LastName = "Flinstone";

            //Generic casting
            var genericName = dcm.FindFirst(TagHelper.PATIENT_NAME) as AbstractElement<string>;
            var genValue = genericName.Data; // returns Flinstone^Fred
        }

        [TestMethod]
        public void WorkingWithMultiValuesTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);

            //Study time is a dateTime value
            var studyTime = dcm.FindFirst(TagHelper.STUDY_TIME) as Time;
            var time = studyTime.Data; // data of type T (in this case system.datetime)

            //Patient position holds double values
            var positionEl = dcm.FindFirst(TagHelper.IMAGE_POSITION_PATIENT);
            var position = positionEl as DecimalString;
            //Patient position contains an array of double values {X,Y,Z}
            var xyz = position.Data_; //Data as List<T> (in this case List<double>)
            var x = xyz[0];
            var y = xyz[1];
            var z = xyz[2];
        }
    }
}
