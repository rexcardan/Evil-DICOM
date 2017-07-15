using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenerator
{
    public class TagBuilder
    {
        internal static SyntaxNode Generate(SyntaxGenerator g, DictionaryData entry)
        {
            //TAGHELPER
            var t = g.FieldDeclaration(entry.Keyword, g.IdentifierName("Tag"), Accessibility.Public,
                DeclarationModifiers.Static, g.ObjectCreationExpression(g.IdentifierName("Tag"),
                    new SyntaxNode[] { g.Argument(RefKind.None, g.LiteralExpression(entry.Id)) }));
            return t;
        }
    }
}
