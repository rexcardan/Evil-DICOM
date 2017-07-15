using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SOPClassUIDBuilder.BuildSOPClassUIDs(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\SOPClassUID.cs");
            SOPClassUIDBuilder.BuildSOPClassDictionary(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\SOPClassDictionary.cs");
            SOPClassUIDBuilder.BuildSOPClass(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Enums\SOPClass.cs");
            var g = GeneratorBuilder.Instance.Generator;

            //DICOMDefinitionLoader.UpdateCurrentFromWeb();
            var dictionary = DICOMDefinitionLoader.LoadCurrentDictionary();

            var forgeNodes = new List<SyntaxNode>();
            var tags = new List<SyntaxNode>();
            var selectors = new List<SyntaxNode>();

            //DICOMForge


            foreach (var entry in dictionary.Where(d => !string.IsNullOrEmpty(d.Keyword)))
            {
                //Build a tag no matter what
                tags.Add(TagBuilder.Generate(g, entry));

                var (cName, parameter, listParameter) = EntryParser.Parse(g, entry);

                if (cName != null)
                {
                    //SELECTOR
                    var (propGetStatements, propSetStatements) = SelectorBuilder.GeneratePropertyStatements(g, cName, entry);
                    var sel = g.PropertyDeclaration(entry.Keyword, g.IdentifierName(cName), Accessibility.Public,
                        DeclarationModifiers.None, propGetStatements, propSetStatements);
                    selectors.Add(sel);

                    // return _dicom.FindAll("00000000").Select(d => d as UnsignedLong).ToList();
                    var returnMany = g.ReturnStatement(g.InvocationExpression(
                        g.IdentifierName($"_dicom.FindAll(\"{entry.Id}\").Select(d => d as {cName}).ToList")));

                    selectors.Add(g.PropertyDeclaration(entry.Keyword + "_", g.IdentifierName($"List<{cName}>"),
                        Accessibility.Public, DeclarationModifiers.None, new SyntaxNode[] { returnMany }));


                    //FORGE
                    var methStatements = new SyntaxNode[]
                    {
                        // return new UnsignedLong { Tag = new Tag("00000001") };
                        g.AssignmentStatement(g.IdentifierName("var element"),
                            g.ObjectCreationExpression(g.IdentifierName(cName))),
                        g.AssignmentStatement(g.IdentifierName("element.Tag"),
                            g.ObjectCreationExpression(g.IdentifierName("Tag"),
                                g.Argument(RefKind.None, g.LiteralExpression(entry.Id)))),
                        g.AssignmentStatement(g.IdentifierName("element.Data"), g.IdentifierName("data")),
                        g.ReturnStatement(g.IdentifierName("element"))
                    };

                    var m = g.MethodDeclaration(entry.Keyword, new SyntaxNode[] { parameter }, null, g.IdentifierName(cName), Accessibility.Public,
                         DeclarationModifiers.Static, methStatements);
                    forgeNodes.Add(m);
                    m = g.MethodDeclaration(entry.Keyword, new SyntaxNode[] { listParameter }, null, g.IdentifierName(cName), Accessibility.Public,
                        DeclarationModifiers.Static, methStatements);
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

            File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\DICOMForge.cs", forgeNode.NormalizeWhitespace().ToString());
            File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Helpers\TagHelper.cs", tagHelperNode.NormalizeWhitespace().ToString());
            File.WriteAllText(@"D:\OneDrive\Cardan.Code\Git\Evil-DICOM\EvilDICOM\EvilDICOM\Core\Selection\DICOMSelectorProperties.cs", selectorNode.NormalizeWhitespace().ToString());
        }

    }
}
