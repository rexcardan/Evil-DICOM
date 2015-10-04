using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.Helpers;
using E=EvilDICOM.Core.Element;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;
using System.Globalization;
using System.Threading;

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

        [TestMethod]
        public void StringDataComposer_ComposeDateTime_HasCorrectSeparator()
        {
            var dateTime = new System.DateTime(621671364610101010, DateTimeKind.Unspecified);

            var currentThread = Thread.CurrentThread;
            var currentCulture = currentThread.CurrentCulture;

            currentThread.CurrentCulture = CreateNonDotSeparatorCulture();

            try
            {
                var actual = StringDataComposer.ComposeDateTime(dateTime);
                var expected = "19710101010101.010101";
                Assert.AreEqual(expected, actual);
            }
            finally
            {
                currentThread.CurrentCulture = currentCulture;
            }
        }

        [TestMethod]
        public void StringDataComposer_ComposeDecimalString_HasCorrectSeparator()
        {
            var data = new[] { -2.5, 5, 7.5 };

            var currentThread = Thread.CurrentThread;
            var currentCulture = currentThread.CurrentCulture;

            currentThread.CurrentCulture = CreateNonDotSeparatorCulture();

            try
            {
                var actual = StringDataComposer.ComposeDecimalString(data);
                var expected = @"-2.5\5\7.5";
                Assert.AreEqual(expected, actual);
            }
            finally
            {
                currentThread.CurrentCulture = currentCulture;
            }
        }

        private static CultureInfo CreateNonDotSeparatorCulture()
        {
            var nonDotSeparatorCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            nonDotSeparatorCulture.NumberFormat.NumberGroupSeparator = "@";
            nonDotSeparatorCulture.NumberFormat.NumberDecimalSeparator = "*";
            return nonDotSeparatorCulture;
        }
    }
}
