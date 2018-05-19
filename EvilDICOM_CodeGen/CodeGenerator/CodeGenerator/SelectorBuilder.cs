using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis;

namespace CodeGenerator
{
    public class SelectorBuilder
    {
        internal static (SyntaxNode[],SyntaxNode[]) GeneratePropertyStatements(SyntaxGenerator g, string cName, DictionaryData entry)
        {
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

        internal static SyntaxNode[] GenerateSequencePropertyStatements(SyntaxGenerator g, string cName, DictionaryData entry)
        {
            var propGetStatements = new SyntaxNode[]
            {
                //get { return _dicom.FindFirst("00000000") as UnsignedLong;
                //}
                //set { _dicom.ReplaceOrAdd(value); }

                g.ReturnStatement(g.CastExpression(g.IdentifierName(cName), g.InvocationExpression(g.IdentifierName($"Items.FindFirst<{cName}>"),
                    new SyntaxNode[]{g.Argument(RefKind.None, g.LiteralExpression(entry.Id))})))
            };

            return propGetStatements;
        }
    }
}
