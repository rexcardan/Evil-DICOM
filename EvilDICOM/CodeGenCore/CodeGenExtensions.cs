using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class CodeGenExtensions
    {
        public static SyntaxNode AddImports(this SyntaxNode node)
            => G.CompilationUnit(ImportHelper.CommonImports.Concat(new[] { node }));

        public static SyntaxNode AddNamespace(this SyntaxNode node, string ns)
            => G.NamespaceDeclaration(ns, node);

        public static void WriteOut(this SyntaxNode node, string fileName, bool includeGenerationMessage = true)
        {
            var code = node.NormalizeWhitespace().ToFullString();

            if (includeGenerationMessage)
                code = code.Insert(0, "// THIS CODE WAS GENERATED AUTOMATICALLY\n\n");

            File.WriteAllText(fileName, code);
        }
    }
}
