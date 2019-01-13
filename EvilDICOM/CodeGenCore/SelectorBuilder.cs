using Microsoft.CodeAnalysis;
using static EvilDICOM.CodeGenerator.GeneratorInstance;

namespace EvilDICOM.CodeGenerator
{
    public static class SelectorBuilder
    {
        public static (SyntaxNode[] getStatements, SyntaxNode[] setStatements) GeneratePropertyStatements(this DictionaryData entry, string className)
        {
            var getStatements = new[]
            {
                G.ReturnStatement(G.CastExpression(G.IdentifierName(className),
                    G.InvocationExpression(G.IdentifierName("_dicom.FindFirst"), G.Argument(RefKind.None, G.LiteralExpression(entry.Id)))))
            };

            var setStatements = new[]
            {
                G.InvocationExpression(G.IdentifierName("_dicom.ReplaceOrAdd"), G.Argument(RefKind.None, G.IdentifierName("value")))
            };

            return (getStatements, setStatements);
        }

        public static SyntaxNode[] GenerateSequencePropertyStatements(this DictionaryData entry, string className)
        {
            var getStatements = new[]
            {
                G.ReturnStatement(G.CastExpression(G.IdentifierName(className),
                    G.InvocationExpression(G.IdentifierName($"Items.FindFirst<{className}>"), G.Argument(RefKind.None, G.LiteralExpression(entry.Id)))))
            };

            return getStatements;
        }
    }
}
