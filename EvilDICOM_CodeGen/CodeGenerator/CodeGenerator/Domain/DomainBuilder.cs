using EvilDICOM.Core;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Domain
{
    public class DomainBuilder
    {
        public static List<SyntaxNode> BuildFromFile(string filePath, string className)
        {
            //This object plus all descendant sequence objects
            List<SyntaxNode> allNodes = new List<SyntaxNode>();

            var dcm = DICOMObject.Read(filePath);
            List<SyntaxNode> props = new List<SyntaxNode>();

            foreach (var el in dcm.Elements)
            {
                //Create property
                props.Add(GenerateProperty(el));
                //If sequence - Create class
                if(el is Sequence)
                {
                    allNodes.Add(BuildFromSequence(el as Sequence));
                }
            }
            var g = GeneratorBuilder.Instance.Generator;
            var node = g.ClassDeclaration(className,
                    null,
                    Accessibility.Public,
                    DeclarationModifiers.None, g.IdentifierName("DICOMObjectWrapper"), null, props);

            allNodes.Add(node);
            return allNodes;
        }

        public static SyntaxNode BuildFromSequence(Sequence seq)
        {
            throw new NotImplementedException();
        }

        public static SyntaxNode GenerateProperty(IDICOMElement el)
        {
            var g = GeneratorBuilder.Instance.Generator;
            var entry = DICOMDictionary.Instance.Entries.FirstOrDefault(e => e.Id == el.Tag.CompleteID);
            var (cName, parameter) = EntryParser.Parse(g, entry);
            var (propGetStatements, propSetStatements) = DomainBuilder.GeneratePropertyStatements(g, cName, entry);
            var sel = g.PropertyDeclaration(entry.Keyword, g.IdentifierName(cName), Accessibility.Public,
                DeclarationModifiers.None, propGetStatements, propSetStatements);
            var test = sel.ToString();
            return sel;
        }

        private static (SyntaxNode[], SyntaxNode[]) GeneratePropertyStatements(SyntaxGenerator g, string cName, DictionaryData entry)
        {
            var template = $"var el = DCM.GetSelector().{entry.Keyword}; return el != null ? el.Data : default({entry.;";
            var propGetStatements = new SyntaxNode[]
           {
                //get { return _dicom.FindFirst("00000000") as UnsignedLong;
                //}
                //set { _dicom.ReplaceOrAdd(value); }
                
                g.ReturnStatement(g.CastExpression(g.IdentifierName(cName), g.InvocationExpression(g.IdentifierName("_dicom.FindFirst"),
                    new SyntaxNode[]{g.Argument(RefKind.None, g.LiteralExpression(entry.Id))})))
           };

            var propSetStatements = new SyntaxNode[]
            {
                //get { return _dicom.FindFirst("00000000") as UnsignedLong;
                //}
                //set { _dicom.ReplaceOrAdd(value); }
                g.InvocationExpression(g.IdentifierName("_dicom.ReplaceOrAdd"),
                    new SyntaxNode[] {g.Argument(RefKind.None, g.IdentifierName("value"))})
            };
            return (propGetStatements, propSetStatements);
        }
    }
}
