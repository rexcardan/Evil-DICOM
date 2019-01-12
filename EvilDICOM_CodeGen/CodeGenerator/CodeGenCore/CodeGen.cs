using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
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
                var forgeStatements = new[] 
                {
                    g.AssignmentStatement(g.IdentifierName("var element"), g.ObjectCreationExpression(g.IdentifierName(className))),
                    g.AssignmentStatement(g.IdentifierName("element.Tag"), g.ObjectCreationExpression(g.IdentifierName("Tag"), g.Argument(RefKind.None, g.LiteralExpression(entry.Id)))),
                    g.AssignmentStatement(g.IdentifierName("element.Data_"), g.IdentifierName("data?.ToList()")),
                    g.ReturnStatement(g.IdentifierName("element"))
                };

                var forgeMethod = g.MethodDeclaration(entry.Keyword, new[] { node }, 
                    null, 
                    g.IdentifierName(className), 
                    Accessibility.Public,
                    DeclarationModifiers.Static, 
                    forgeStatements);

                forgeNodes.Add(forgeMethod);
            }

            CodeGenHelper.PublicStaticClassFull(typeof(DICOMForge), forgeNodes)
                .WriteClass("DICOMForge.cs");

            CodeGenHelper.PublicStaticClassFull(typeof(TagHelper), tags)
                .WriteClass("TagHelper.cs");

            CodeGenHelper.PublicPartialClassFull(typeof(DICOMSelector), selectors)
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
    }
}
