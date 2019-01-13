using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class SopClassUidBuilder
    {
        public static void BuildSopClassUids()
        {
            var sopFields = DicomDefinitionLoader.LoadCurrentSopClasses()
                .Select(sopClass => G.FieldDeclaration(
                    sopClass.Keyword,
                    G.IdentifierName(nameof(String)),
                    Accessibility.Public,
                    DeclarationModifiers.Const,
                    G.LiteralExpression(sopClass.Id)));

            CodeGenHelper.PublicStaticClassFull(typeof(SOPClassUID), sopFields)
                .WriteOut("SOPClassUID.cs");
        }

        public static void BuildSopClassEnum()
        {
            var sopEnumMembers = DicomDefinitionLoader.LoadCurrentSopClasses()
                .Select(sopClass => G.EnumMember(sopClass.Keyword))
                .Append(G.EnumMember("Unknown"));

            G.EnumDeclaration(nameof(SOPClass),
                    Accessibility.Public,
                    DeclarationModifiers.None,
                    sopEnumMembers)
                .AddNamespace(typeof(SOPClass).Namespace)
                .WriteOut("SOPClass.cs");
        }

        public static void BuildSopClassDictionary()
        {
            var type = G.IdentifierName($"Dictionary<string, {nameof(SOPClass)}>");

            var methodStatements = new List<SyntaxNode>
            {
                G.AssignmentStatement(G.IdentifierName("var dict"), G.ObjectCreationExpression(type))
            };

            methodStatements.AddRange(DicomDefinitionLoader.LoadCurrentSopClasses().Select(
                sopClass => G.InvocationExpression(G.IdentifierName("dict.Add"),
                    G.Argument(RefKind.None, G.IdentifierName($"{nameof(SOPClassUID)}.{sopClass.Keyword}")),
                    G.Argument(RefKind.None, G.IdentifierName($"{nameof(SOPClass)}.{sopClass.Keyword}")))));

            methodStatements.Add(G.ReturnStatement(G.IdentifierName("dict")));

            var method = G.MethodDeclaration("Initialize",
                null,
                null,
                type,
                Accessibility.Internal,
                DeclarationModifiers.Static,
                methodStatements);

            G.ClassDeclaration("SOPClassDictionary",
                    null,
                    Accessibility.Internal,
                    DeclarationModifiers.None,
                    null,
                    null,
                    new[] { method })
                .AddNamespace(typeof(SOPClassHelper).Namespace)
                .AddImports()
                .WriteOut("SOPClassDictionary.cs");
        }
    }
}