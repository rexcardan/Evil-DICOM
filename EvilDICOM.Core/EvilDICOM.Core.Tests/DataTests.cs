using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;
using System.Linq;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void TestMethod1()
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
            var genValue = genericName.Data.SingleValue; // returns Flinstone^Fred
        }

         [TestMethod]
        public void TestMethod2()
        {
             var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);

             //Patient's age is a single string value
             var age = dcm.FindFirst(TagHelper.PATIENT_AGE) as AgeString;
             var actualAge = age.Data.SingleValue; // data of type T (in this case string)

             //Patient position holds double values
             var position = dcm.FindFirst(TagHelper.PATIENT_POSITION) as AbstractElement<double>;

             //Patient position contains an array of double values {X,Y,Z}
             var xyz = position.Data.MultipicityValue; //Data as List<T> (in this case List<double>)
             var x = xyz[0];
             var y = xyz[1];
             var z = xyz[2];
        }
    }

   
    }
}
