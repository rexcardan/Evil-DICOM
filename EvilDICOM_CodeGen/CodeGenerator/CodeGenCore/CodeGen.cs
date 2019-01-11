using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Selection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class CodeGen
    {
        public static void GenerateStuff()
        {
            var g = GeneratorBuilder.Instance.Generator;

            var tags = new List<SyntaxNode>();
            var selectors = new List<SyntaxNode>();
            var seqSelectors = new List<SyntaxNode>();
            var forgeNodes = new List<SyntaxNode>();

            foreach (var entry in DicomDefinitionLoader.LoadCurrentDictionary().Where(d => !string.IsNullOrEmpty(d.Keyword)))
            {
                tags.Add(g.GenerateTag(entry));

                var (className, node) = g.Parse(entry);

                if (className == null)
                    continue;

                // selector
                var (propGetStatements, propSetStatements) = g.GeneratePropertyStatements(className, entry);

                var selector = g.PropertyDeclaration(entry.Keyword,
                    g.IdentifierName(className),
                    Accessibility.Public,
                    DeclarationModifiers.None,
                    propGetStatements,
                    propSetStatements);

                selectors.Add(selector);

                var propGetManyStatements = g.ReturnStatement(g.InvocationExpression(
                    g.IdentifierName($"_dicom.FindAll(\"{entry.Id}\").Select(d => d as {className}).ToList")));

                var manySelector = g.PropertyDeclaration(entry.Keyword + "_", g.IdentifierName($"List<{className}>"),
                    Accessibility.Public, DeclarationModifiers.ReadOnly, new[] { propGetManyStatements });

                selectors.Add(manySelector);

                var seqPropGetStatements = g.GenerateSequencePropertyStatements(className, entry);

                var seqSelector = g.PropertyDeclaration(entry.Keyword,
                    g.IdentifierName(className),
                    Accessibility.Public,
                    DeclarationModifiers.ReadOnly,
                    seqPropGetStatements);

                seqSelectors.Add(seqSelector);

                var propGetManySeqStatements = g.ReturnStatement(g.InvocationExpression(
                    g.IdentifierName($"Items.FindAll<{className}>(\"{entry.Id}\").ToList")));

                var manySeqSelector = g.PropertyDeclaration(entry.Keyword + "_", g.IdentifierName($"List<{className}>"),
                    Accessibility.Public, DeclarationModifiers.ReadOnly, new[] { propGetManySeqStatements });

                seqSelectors.Add(manySeqSelector);

                // forge
            }

            PublicStaticClassFull(typeof(TagHelper), tags)
                .WriteClass("TagHelper.cs");

            PublicPartialClassFull(typeof(DICOMSelector), selectors)
                .WriteClass("DICOMSelectorProperties.cs");

            GeneratorBuilder.Instance.Generator.ClassDeclaration(typeof(SequenceSelector).Name,
                    null,
                    Accessibility.Public,
                    DeclarationModifiers.Partial,
                    GeneratorBuilder.Instance.Generator.IdentifierName("AbstractElement<DICOMSelector>"),
                    null,
                    seqSelectors)
                .AddNamespace(typeof(SequenceSelector).Namespace)
                .AddImports()
                .WriteClass("SequenceSelectorProperties.cs");
        }

        public static void WriteClass(this SyntaxNode node, string fileName)
            => File.WriteAllText(fileName, node.NormalizeWhitespace().ToFullString());

        public static SyntaxNode AddImports(this SyntaxNode node)
            => GeneratorBuilder.Instance.Generator.CompilationUnit(ImportHelper.CommonImports.Concat(new[] { node }));

        public static SyntaxNode AddNamespace(this SyntaxNode node, string ns)
            => GeneratorBuilder.Instance.Generator.NamespaceDeclaration(ns, node);

        public static SyntaxNode PublicStaticClassFull(Type type, IEnumerable<SyntaxNode> members)
            => PublicStaticClass(type.Name, members)
                .AddNamespace(type.Namespace)
                .AddImports();

        public static SyntaxNode PublicPartialClassFull(Type type, IEnumerable<SyntaxNode> members)
            => PublicPartialClass(type.Name, members)
                .AddNamespace(type.Namespace)
                .AddImports();

        public static SyntaxNode PublicStaticClass(string className, IEnumerable<SyntaxNode> members)
            => GeneratorBuilder.Instance.Generator.ClassDeclaration(className,
                null,
                Accessibility.Public,
                DeclarationModifiers.Static,
                null,
                null,
                members);

        public static SyntaxNode PublicPartialClass(string className, IEnumerable<SyntaxNode> members)
            => GeneratorBuilder.Instance.Generator.ClassDeclaration(className,
                null,
                Accessibility.Public,
                DeclarationModifiers.Partial,
                null,
                null,
                members);
    }
}
