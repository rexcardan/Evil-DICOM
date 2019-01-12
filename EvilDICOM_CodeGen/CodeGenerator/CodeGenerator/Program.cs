using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System.IO;
using EvilDICOM.Core.Dictionaries;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //SOPClassUIDBuilder.BuildSOPClassUIDs(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\SOPClassUID.cs");
            //SOPClassUIDBuilder.BuildSOPClassDictionary(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\SOPClassDictionary.cs");
            //SOPClassUIDBuilder.BuildSOPClass(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Enums\SOPClass.cs");
            var g = GeneratorBuilder.Instance.Generator;

            //DICOMDefinitionLoader.UpdateCurrentFromWeb();
            var dictionary = DICOMDefinitionLoader.LoadCurrentDictionary().ToList();

            // Get Anonymization tags
            var anonTags = DICOMDefinitionLoader.LoadAnonymizationTags().ToList();

            var forgeNodes = new List<SyntaxNode>();
            var tags = new List<SyntaxNode>();
            var selectors = new List<SyntaxNode>();
            var seqSelectors = new List<SyntaxNode>();
            var anonymizationNodes = new List<SyntaxNode>();

            List<SyntaxNode> anonProfile = new List<SyntaxNode>();
            anonProfile.Add(g.AssignmentStatement(g.IdentifierName("var profile"), g.IdentifierName("new List<IDICOMElement>()")));
            foreach (var atag in anonTags)
            {
                var action = "AnonymizeAction.REMOVE_ELEMENT";
                switch (atag.Metadata)
                {
                    case "D": action = "AnonymizeAction.DUMMY_DATA"; break;
                    case "Z":
                    case "Z/D": action = "AnonymizeAction.NULL_DATA"; break;
                    case "X/Z":
                    case "X/D":
                    case "X/Z/D":
                    case "X": action = "AnonymizeAction.REMOVE_ELEMENT"; break;
                    case "K":
                    case "C": action = "AnonymizeAction.CLEAN"; break;
                    case "X/Z/U*":
                    case "U": action = "AnonymizeAction.CLEAN"; break;
                }
                atag.VR = dictionary.FirstOrDefault(d => d.Id == atag.Id)?.VR;
                var entry = g.IdentifierName($"yield return new ConfidentialElement(){{Id=\"{atag.Id}\", ElementName=\"{atag.Name}\", VR=VR.{VRDictionary.GetVRFromAbbreviation(atag.VR)}, Action = {action}}} )");
                anonProfile.Add(entry);
            }
            anonProfile.Add(g.ReturnStatement(g.IdentifierName("profile")));

            var method = g.MethodDeclaration("GenerateProfileElements", null, null, g.IdentifierName("List<IDICOMElement>"), Accessibility.Public, DeclarationModifiers.Static, anonProfile);

            var anonNode = g.ClassDeclaration("AnonStub",
                   null,
                   Accessibility.Public,
                   DeclarationModifiers.Static, null, null, new SyntaxNode[] { method });
            var anonMethod = g.CompilationUnit(anonNode).NormalizeWhitespace().ToString();



            foreach (var entry in dictionary.Where(d => !string.IsNullOrEmpty(d.Keyword)))
            {
                //Build a tag no matter what
                tags.Add(TagBuilder.Generate(g, entry));

                var (cName, parameter) = EntryParser.Parse(g, entry);

                if (cName != null)
                {
                    //SELECTOR
                    var (propGetStatements, propSetStatements) = SelectorBuilder.GeneratePropertyStatements(g, cName, entry);
                    var sel = g.PropertyDeclaration(entry.Keyword, g.IdentifierName(cName), Accessibility.Public,
                        DeclarationModifiers.None, propGetStatements, propSetStatements);
                    selectors.Add(sel);
                    seqSelectors.Add(g.PropertyDeclaration(entry.Keyword, g.IdentifierName(cName), Accessibility.Public,
                       DeclarationModifiers.ReadOnly, SelectorBuilder.GenerateSequencePropertyStatements(g, cName, entry), null));

                    // return _dicom.FindAll("00000000").Select(d => d as UnsignedLong).ToList();
                    var returnMany = g.ReturnStatement(g.InvocationExpression(
                        g.IdentifierName($"_dicom.FindAll(\"{entry.Id}\").Select(d => d as {cName}).ToList")));
                    var returnManySeq = g.ReturnStatement(g.InvocationExpression(
                        g.IdentifierName($"Items.FindAll<{cName}>(\"{entry.Id}\").ToList")));
                    selectors.Add(g.PropertyDeclaration(entry.Keyword + "_", g.IdentifierName($"List<{cName}>"),
                        Accessibility.Public, DeclarationModifiers.ReadOnly, new SyntaxNode[] { returnMany }));
                    var selSeqProp = g.PropertyDeclaration(entry.Keyword + "_", g.IdentifierName($"List<{cName}>"),
                         Accessibility.Public, DeclarationModifiers.ReadOnly, new SyntaxNode[] { returnManySeq});

                    seqSelectors.Add(selSeqProp);
                    //FORGE
                    var methStatements = new SyntaxNode[]
                     {
                        // return new UnsignedLong { Tag = new Tag("00000001") };
                        g.AssignmentStatement(g.IdentifierName("var element"),
                            g.ObjectCreationExpression(g.IdentifierName(cName))),
                        g.AssignmentStatement(g.IdentifierName("element.Tag"),
                            g.ObjectCreationExpression(g.IdentifierName("Tag"),
                                g.Argument(RefKind.None, g.LiteralExpression(entry.Id)))),
                        g.AssignmentStatement(g.IdentifierName("element.Data_"), g.IdentifierName("data?.ToList()")),
                        g.ReturnStatement(g.IdentifierName("element"))
                     };

                    var m = g.MethodDeclaration(entry.Keyword, new SyntaxNode[] { parameter }, null, g.IdentifierName(cName), Accessibility.Public,
                         DeclarationModifiers.Static, methStatements);
                    forgeNodes.Add(m);

                }
            }



            var forgeNode = g.ClassDeclaration("DICOMForge",
                    null,
                    Accessibility.Public,
                    DeclarationModifiers.Static, null, null, forgeNodes);
            var namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core", forgeNode);
            forgeNode = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
    })).NormalizeWhitespace();
            //public UnsignedLong CommandGroupLength
            //{
            //    get { return _dicom.FindFirst("00000000") as UnsignedLong; }
            //set { _dicom.ReplaceOrAdd(value); }
            //}

            var tagHelperNode = g.ClassDeclaration("TagHelper",
                    null,
                    Accessibility.Public,
                    DeclarationModifiers.Static, null, null, tags);
            namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Helpers", tagHelperNode);
            tagHelperNode = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
})).NormalizeWhitespace();

            var selectorNode = g.ClassDeclaration("DICOMSelector",
                null,
                Accessibility.Public,
                DeclarationModifiers.Partial, null, null, selectors);
            namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Selection", selectorNode);
            selectorNode = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
            })).NormalizeWhitespace();

            var selectorSeqNode = g.ClassDeclaration("SequenceSelector",
              null,
              Accessibility.Public,
              DeclarationModifiers.Partial, g.IdentifierName("AbstractElement<DICOMSelector>"), null, seqSelectors);
            namespaceDeclaration = g.NamespaceDeclaration("EvilDICOM.Core.Selection", selectorSeqNode);
            selectorSeqNode = g.CompilationUnit(UsingsHelper.GetUsings().Concat(new SyntaxNode[] { namespaceDeclaration
            })).NormalizeWhitespace();

            //File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\DICOMForge.cs", forgeNode.NormalizeWhitespace().ToString());
            //File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\TagHelper.cs", tagHelperNode.NormalizeWhitespace().ToString());
            File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Selection\DICOMSelectorProperties.cs", selectorNode.NormalizeWhitespace().ToString());
            File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Selection\SequenceSelectorProperties.cs", selectorSeqNode.NormalizeWhitespace().ToString());
        }

    }
}
