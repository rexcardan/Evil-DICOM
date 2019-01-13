using System.Linq;
using Microsoft.CodeAnalysis;

namespace EvilDICOM.CodeGenerator
{
    public static class ImportHelper
    {
        public static readonly SyntaxNode[] CommonImports;

        static ImportHelper()
        {
            var g = GeneratorBuilder.Instance.Generator;

            CommonImports = new[]
            {
                "System",
                "System.Collections",
                "System.Collections.Generic",
                "System.Linq",
                "EvilDICOM.Core.Element",
                "EvilDICOM.Core.Enums"
            }.Select(ns => g.NamespaceImportDeclaration(ns)).ToArray();
        }
    }
}
