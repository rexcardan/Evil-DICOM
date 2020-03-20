using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Domain
{
    public class DomainWriter
    {
        public static void WriteObjects(List<CompilationUnitSyntax> objs, string outputFolder)
        {
            foreach(var obj in objs)
            {
                var code = obj
                   .NormalizeWhitespace()
                   .ToFullString();

                var classType = (obj.DescendantNodes().OfType<ClassDeclarationSyntax>().Cast<ClassDeclarationSyntax>().First()).Identifier.ToString();
                var output = Path.Combine(outputFolder, $"{classType}.cs");
                File.WriteAllText(output, code);
            }
        }

        private static string GetClass(SyntaxList<MemberDeclarationSyntax> members)
        {
            var cd = members.FirstOrDefault(m => m.Kind() == SyntaxKind.ClassDeclaration);
            if (cd == null)
            {
                return string.Empty;
            }
            return cd.ToString();
        }
    }
}
