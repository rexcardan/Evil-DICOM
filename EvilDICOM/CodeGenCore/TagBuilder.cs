using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class TagBuilder
    {
        public static SyntaxNode GenerateTag(this DictionaryData entry)
        {
            return G.FieldDeclaration(entry.Keyword,
                G.IdentifierName("Tag"),
                Accessibility.Public,
                DeclarationModifiers.Static,
                G.ObjectCreationExpression(G.IdentifierName("Tag"), G.Argument(RefKind.None, G.LiteralExpression(entry.Id))));
        }
    }
}
