using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace CodeGenerator
{
    public class UsingsHelper
    {
        public static SyntaxNode[] GetUsings()
        {
            var g = GeneratorBuilder.Instance.Generator;

            var usings = new SyntaxNode[]
            {
                g.NamespaceImportDeclaration("System"),
                g.NamespaceImportDeclaration("System.Collections"),
                g.NamespaceImportDeclaration("System.Collections.Generic"),
                g.NamespaceImportDeclaration("EvilDICOM.Core.Enums"),
                g.NamespaceImportDeclaration("System.Linq"),
                g.NamespaceImportDeclaration("EvilDICOM.Core.Element"),
            };
            return usings;
        }
    }
}
