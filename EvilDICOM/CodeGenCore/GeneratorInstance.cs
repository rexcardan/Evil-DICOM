using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace EvilDICOM.CodeGenerator
{
    public static class GeneratorInstance
    {
        public static SyntaxGenerator G { get; }

        // TODO: workspace disposal?
        static GeneratorInstance()
            => G = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
    }
}