using System.Linq;
using Microsoft.CodeAnalysis;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class ImportHelper
    {
        public static readonly SyntaxNode[] CommonImports;

        static ImportHelper()
        {
            CommonImports = new[]
            {
                "System",
                "System.Collections",
                "System.Collections.Generic",
                "System.Linq",
                "EvilDICOM.Core.Element",
                "EvilDICOM.Core.Enums"
            }.Select(ns => G.NamespaceImportDeclaration(ns)).ToArray();
        }
    }
}
