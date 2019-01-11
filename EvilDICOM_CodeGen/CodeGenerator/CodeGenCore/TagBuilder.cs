using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class TagBuilder
    {
        public static SyntaxNode GenerateTag(this SyntaxGenerator g, DictionaryData entry)
        {
            return g.FieldDeclaration(entry.Keyword,
                g.IdentifierName("Tag"),
                Accessibility.Public,
                DeclarationModifiers.Static,
                g.ObjectCreationExpression(g.IdentifierName("Tag"), g.Argument(RefKind.None, g.LiteralExpression(entry.Id))));
        }
    }
}
