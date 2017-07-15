using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System.IO;

namespace CodeGenerator
{
    public class SOPClassUIDBuilder
    {
        internal static void BuildSOPClassUIDs(string filePath)
        {
            var g = GeneratorBuilder.Instance.Generator;
            var sopClass = DICOMDefinitionLoader.LoadCurrentSOPClasses();
            List<SyntaxNode> sopFields = new List<SyntaxNode>();
            foreach (var sop in sopClass)
            {
                var name = sop.Keyword;
                //public static string VERIFICATION = "1.2.840.10008.1.1";
                var f = g.FieldDeclaration(name, g.IdentifierName("String"), Accessibility.Public,
                    DeclarationModifiers.Const, g.LiteralExpression(sop.Id));
                sopFields.Add(f);
            }

            //SOPClassUID
            var sopNode = g.ClassDeclaration("SOPClassUID",
                null,
                Accessibility.Public,
                DeclarationModifiers.Partial, null, null, sopFields);
            var namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Helpers", sopNode);
            sopNode = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
            })).NormalizeWhitespace();

            File.WriteAllText(filePath, sopNode.NormalizeWhitespace().ToString());
        }

        internal static void BuildSOPClass(string filePath)
        {
            var g = GeneratorBuilder.Instance.Generator;
            var sopClass = DICOMDefinitionLoader.LoadCurrentSOPClasses();

            List<SyntaxNode> enumMembers = new List<SyntaxNode>();
            foreach (var sop in sopClass)
            {
                var name = sop.Keyword;

                enumMembers.Add(g.EnumMember(sop.Keyword));
            }
            enumMembers.Add(g.EnumMember("Unknown"));

            //SOPClass enum
            var sopclassNode = g.EnumDeclaration("SOPClass", Accessibility.Public, DeclarationModifiers.None,
                enumMembers);
            var namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Enums", sopclassNode);
            sopclassNode = g.CompilationUnit(new SyntaxNode[] { namespaceDeclaration
            }).NormalizeWhitespace();
            File.WriteAllText(filePath, sopclassNode.NormalizeWhitespace().ToString());
        }

        internal static void BuildSOPClassDictionary(string filePath)
        {
            var g = GeneratorBuilder.Instance.Generator;
            var type = g.IdentifierName("Dictionary<string, SOPClass>");

            var sopClass = DICOMDefinitionLoader.LoadCurrentSOPClasses();
            List<SyntaxNode> methodStatements = new List<SyntaxNode>();
            methodStatements.Add(g.AssignmentStatement(g.IdentifierName("var dict"), g.ObjectCreationExpression(type)));

            foreach (var sop in sopClass)
            {
                methodStatements.Add(g.InvocationExpression(g.IdentifierName("dict.Add"),
                    g.Argument(RefKind.None, g.IdentifierName($"SOPClassUID.{sop.Keyword}")),
                    g.Argument(RefKind.None, g.IdentifierName($"SOPClass.{sop.Keyword}"))));
            }

            methodStatements.Add(g.ReturnStatement(g.IdentifierName("dict")));

            //SOPClass enum
            var method = g.MethodDeclaration("Initialize", null, null, type, Accessibility.Internal,
                DeclarationModifiers.Static, methodStatements);
            var cls = g.ClassDeclaration("SOPClassDictionary", null, Accessibility.Internal,
                DeclarationModifiers.None, null, null, new SyntaxNode[] {method});
            var namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Helpers", cls);
            cls = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
            })).NormalizeWhitespace();
            File.WriteAllText(filePath, cls.NormalizeWhitespace().ToString());
        }
    }
}
