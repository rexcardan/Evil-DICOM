using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class SelectorBuilder
    {
        public static (SyntaxNode[] getStatements, SyntaxNode[] setStatements) GeneratePropertyStatements(this SyntaxGenerator g, string className, DictionaryData entry)
        {
            var getStatements = new[]
            {
                g.ReturnStatement(g.CastExpression(g.IdentifierName(className),
                    g.InvocationExpression(g.IdentifierName("_dicom.FindFirst"), g.Argument(RefKind.None, g.LiteralExpression(entry.Id)))))
            };

            var setStatements = new[]
            {
                g.InvocationExpression(g.IdentifierName("_dicom.ReplaceOrAdd"), g.Argument(RefKind.None, g.IdentifierName("value")))
            };

            return (getStatements, setStatements);
        }

        public static SyntaxNode[] GenerateSequencePropertyStatements(this SyntaxGenerator g, string className, DictionaryData entry)
        {
            var getStatements = new[]
            {
                g.ReturnStatement(g.CastExpression(g.IdentifierName(className),
                    g.InvocationExpression(g.IdentifierName($"Items.FindFirst<{className}>"), g.Argument(RefKind.None, g.LiteralExpression(entry.Id)))))
            };

            return getStatements;
        }
    }
}
