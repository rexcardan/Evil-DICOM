using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenerator
{
    public class GeneratorBuilder
    {
        private static GeneratorBuilder _instance;

        public SyntaxGenerator Generator { get; }

        public static GeneratorBuilder Instance => _instance ?? (_instance = new GeneratorBuilder());

        private GeneratorBuilder()
        {
            var ws = new AdhocWorkspace();
            Generator = SyntaxGenerator.GetGenerator(ws, LanguageNames.CSharp);
        }
    }
}
