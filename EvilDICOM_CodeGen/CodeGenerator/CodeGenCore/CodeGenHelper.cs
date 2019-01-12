using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class CodeGenHelper
    {
        public static SyntaxNode PublicStaticClassFull(Type type, IEnumerable<SyntaxNode> members)
            => PublicStaticClass(type.Name, members)
                .AddNamespace(type.Namespace)
                .AddImports();

        public static SyntaxNode PublicPartialClassFull(Type type, IEnumerable<SyntaxNode> members)
            => PublicPartialClass(type.Name, members)
                .AddNamespace(type.Namespace)
                .AddImports();

        public static SyntaxNode PublicStaticClass(string className, IEnumerable<SyntaxNode> members)
            => GeneratorBuilder.Instance.Generator.ClassDeclaration(className,
                null,
                Accessibility.Public,
                DeclarationModifiers.Static,
                null,
                null,
                members);

        public static SyntaxNode PublicPartialClass(string className, IEnumerable<SyntaxNode> members)
            => GeneratorBuilder.Instance.Generator.ClassDeclaration(className,
                null,
                Accessibility.Public,
                DeclarationModifiers.Partial,
                null,
                null,
                members);
    }
}
