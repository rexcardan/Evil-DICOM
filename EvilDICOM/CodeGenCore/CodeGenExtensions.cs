using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace EvilDICOM.CodeGenerator
{
    public static class CodeGenExtensions
    {
        public static SyntaxNode AddImports(this SyntaxNode node)
            => GeneratorBuilder.Instance.Generator.CompilationUnit(ImportHelper.CommonImports.Concat(new[] { node }));

        public static SyntaxNode AddNamespace(this SyntaxNode node, string ns)
            => GeneratorBuilder.Instance.Generator.NamespaceDeclaration(ns, node);

        public static void WriteClass(this SyntaxNode node, string fileName)
            => File.WriteAllText(fileName, node.NormalizeWhitespace().ToFullString());
    }
}
