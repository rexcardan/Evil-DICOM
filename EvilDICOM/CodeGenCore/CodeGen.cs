using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Selection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class CodeGen
    {
        public static void GenerateStuff()
        {
            var tags = new List<SyntaxNode>();
            var selectors = new List<SyntaxNode>();
            var seqSelectors = new List<SyntaxNode>();
            var forgeNodes = new List<SyntaxNode>();

            foreach (var entry in DicomDefinitionLoader.LoadCurrentDictionary().Where(d => !string.IsNullOrEmpty(d.Keyword)))
            {
                tags.Add(entry.GenerateTag());

                var (className, node) = entry.Parse();

                if (className == null)
                    continue;

                // selector
                var (propGetStatements, propSetStatements) = entry.GeneratePropertyStatements(className);

                var selector = G.PropertyDeclaration(entry.Keyword,
                    G.IdentifierName(className),
                    Accessibility.Public,
                    DeclarationModifiers.None,
                    propGetStatements,
                    propSetStatements);

                selectors.Add(selector);

                var propGetManyStatements = G.ReturnStatement(G.InvocationExpression(
                    G.IdentifierName($"_dicom.FindAll(\"{entry.Id}\").Select(d => d as {className}).ToList")));

                var manySelector = G.PropertyDeclaration(entry.Keyword + "_", G.IdentifierName($"List<{className}>"),
                    Accessibility.Public, DeclarationModifiers.ReadOnly, new[] { propGetManyStatements });

                selectors.Add(manySelector);

                var seqPropGetStatements = entry.GenerateSequencePropertyStatements(className);

                var seqSelector = G.PropertyDeclaration(entry.Keyword,
                    G.IdentifierName(className),
                    Accessibility.Public,
                    DeclarationModifiers.ReadOnly,
                    seqPropGetStatements);

                seqSelectors.Add(seqSelector);

                var propGetManySeqStatements = G.ReturnStatement(G.InvocationExpression(
                    G.IdentifierName($"Items.FindAll<{className}>(\"{entry.Id}\").ToList")));

                var manySeqSelector = G.PropertyDeclaration(entry.Keyword + "_", G.IdentifierName($"List<{className}>"),
                    Accessibility.Public, DeclarationModifiers.ReadOnly, new[] { propGetManySeqStatements });

                seqSelectors.Add(manySeqSelector);

                // forge
                var forgeStatements = new[] 
                {
                    G.AssignmentStatement(G.IdentifierName("var element"), G.ObjectCreationExpression(G.IdentifierName(className))),
                    G.AssignmentStatement(G.IdentifierName("element.Tag"), G.ObjectCreationExpression(G.IdentifierName("Tag"), G.Argument(RefKind.None, G.LiteralExpression(entry.Id)))),
                    G.AssignmentStatement(G.IdentifierName("element.Data_"), G.IdentifierName("data?.ToList()")),
                    G.ReturnStatement(G.IdentifierName("element"))
                };

                var forgeMethod = G.MethodDeclaration(entry.Keyword, new[] { node }, 
                    null, 
                    G.IdentifierName(className), 
                    Accessibility.Public,
                    DeclarationModifiers.Static, 
                    forgeStatements);

                forgeNodes.Add(forgeMethod);
            }

            CodeGenHelper.PublicStaticClassFull(typeof(DICOMForge), forgeNodes)
                .WriteOut("DICOMForge.cs");

            CodeGenHelper.PublicStaticClassFull(typeof(TagHelper), tags)
                .WriteOut("TagHelper.cs");

            CodeGenHelper.PublicPartialClassFull(typeof(DICOMSelector), selectors)
                .WriteOut("DICOMSelectorProperties.cs");

            G.ClassDeclaration(typeof(SequenceSelector).Name,
                    null,
                    Accessibility.Public,
                    DeclarationModifiers.Partial,
                    G.IdentifierName("AbstractElement<DICOMSelector>"),
                    null,
                    seqSelectors)
                .AddNamespace(typeof(SequenceSelector).Namespace)
                .AddImports()
                .WriteOut("SequenceSelectorProperties.cs");
        }

        // TODO
        public static void GenerateAnonStuff()
        {
            var dictionary = DicomDefinitionLoader.LoadCurrentDictionary().ToList();

            var anonProfile = new List<SyntaxNode>
            {
                G.AssignmentStatement(G.IdentifierName("var profile"), G.IdentifierName("new List<IDICOMElement>()"))
            };

            foreach (var anonTag in DicomDefinitionLoader.LoadAnonymizationTags())
            {
                var action = "AnonymizeAction.REMOVE_ELEMENT";

                switch (anonTag.Metadata)
                {
                    case "D":
                        action = "AnonymizeAction.DUMMY_DATA";
                        break;
                    case "Z":
                    case "Z/D":
                        action = "AnonymizeAction.NULL_DATA";
                        break;
                    case "X/Z":
                    case "X/D":
                    case "X/Z/D":
                    case "X":
                        action = "AnonymizeAction.REMOVE_ELEMENT";
                        break;
                    case "K":
                    case "C":
                        action = "AnonymizeAction.CLEAN";
                        break;
                    case "X/Z/U*":
                    case "U":
                        action = "AnonymizeAction.CLEAN";
                        break;
                }

                anonTag.VR = dictionary.FirstOrDefault(d => d.Id == anonTag.Id)?.VR;

                var entry = G.IdentifierName(
                    $"yield return new ConfidentialElement(){{Id=\"{anonTag.Id}\", ElementName=\"{anonTag.Name}\", VR=VR.{VRDictionary.GetVRFromAbbreviation(anonTag.VR)}, Action = {action}}} )");

                anonProfile.Add(entry);
            }

            anonProfile.Add(G.ReturnStatement(G.IdentifierName("profile")));

            var method = G.MethodDeclaration("GenerateProfileElements", 
                null, 
                null, 
                G.IdentifierName("List<IDICOMElement>"), 
                Accessibility.Public, 
                DeclarationModifiers.Static, 
                anonProfile);

            var anonClass = CodeGenHelper.PublicStaticClass("AnonStub", new[] { method });

            var anonMethod = G.CompilationUnit(anonClass).NormalizeWhitespace().ToString();
        }
    }
}
