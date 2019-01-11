using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class SopClassUidBuilder
    {
        public static void BuildSopClassUids(string filePath)
        {
            var g = GeneratorBuilder.Instance.Generator;

            var sopFields = DicomDefinitionLoader.LoadCurrentSopClasses()
                .Select(sopClass => g.FieldDeclaration(
                    sopClass.Keyword,
                    g.IdentifierName(nameof(String)),
                    Accessibility.Public,
                    DeclarationModifiers.Const,
                    g.LiteralExpression(sopClass.Id)));

            var sopClassNode = g.ClassDeclaration(nameof(SOPClassUID),
                null,
                Accessibility.Public,
                DeclarationModifiers.Partial,
                null,
                null,
                sopFields);

            var namespaceDeclaration = g.NamespaceDeclaration(typeof(SOPClassUID).Namespace, sopClassNode);

            var finalNode = g.CompilationUnit(ImportHelper.CommonImports.Concat(new[] { namespaceDeclaration }));

            File.WriteAllText(filePath, finalNode.NormalizeWhitespace().ToFullString());
        }

        public static void BuildSopClassEnum(string filePath)
        {
            var g = GeneratorBuilder.Instance.Generator;

            var sopEnumMembers = DicomDefinitionLoader.LoadCurrentSopClasses()
                .Select(sopClass => g.EnumMember(sopClass.Keyword))
                .Append(g.EnumMember("Unknown"));

            var sopClassEnum = g.EnumDeclaration(nameof(SOPClass),
                Accessibility.Public,
                DeclarationModifiers.None,
                sopEnumMembers);

            var namespaceDeclaration = g.NamespaceDeclaration(typeof(SOPClass).Namespace, sopClassEnum);

            var finalNode = g.CompilationUnit(namespaceDeclaration);

            File.WriteAllText(filePath, finalNode.NormalizeWhitespace().ToFullString());
        }

        // TODO: SOP class dictionary
    }
}