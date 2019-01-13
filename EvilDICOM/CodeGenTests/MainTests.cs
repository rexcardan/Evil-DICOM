using System.Threading.Tasks;
using EvilDICOM.CodeGenerator;
using Xunit;
using Xunit.Abstractions;

namespace EvilDICOM.CodeGeneratorTests
{
    public class MainTest
    {
        private readonly ITestOutputHelper _output;

        public MainTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestStuff()
        {

        }

        [Fact]
        public async Task TestDownloadDefinitions()
        {
            await DicomDefinitionLoader.DownloadCurrentDefinitions();
        }

        [Fact]
        public void TestSopClassBuilder()
        {
            SopClassUidBuilder.BuildSopClassUids();
        }

        [Fact]
        public void TestSopEnumBuilder()
        {
            SopClassUidBuilder.BuildSopClassEnum();
        }

        [Fact]
        public void TestSopClassDictionary()
        {
            SopClassUidBuilder.BuildSopClassDictionary();
        }

        [Fact]
        public void TestElementBuilder()
        {
            CodeGen.GenerateStuff();
        }

        [Fact]
        public void TestAnonStuff()
        {
            CodeGen.GenerateAnonStuff();
        }
    }
}
