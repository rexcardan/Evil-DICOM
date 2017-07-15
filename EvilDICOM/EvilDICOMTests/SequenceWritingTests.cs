using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.Element;

namespace EvilDICOMTests
{
    [TestClass]
    public class SequenceWritingTests
    {
        [TestMethod]
        public void WriteSequence()
        {
            var seq = new Sequence() {Tag = new Tag("00000000")};
        }
    }
}
