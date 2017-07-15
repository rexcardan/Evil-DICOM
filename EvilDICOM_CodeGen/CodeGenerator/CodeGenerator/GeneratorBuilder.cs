using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenerator
{
    public class GeneratorBuilder
    {
        private static GeneratorBuilder instance;

        private GeneratorBuilder()
        {
            var ws = new AdhocWorkspace();
            Generator = SyntaxGenerator.GetGenerator(ws, LanguageNames.CSharp);
        }

        public SyntaxGenerator Generator { get; set; }

        public static GeneratorBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeneratorBuilder();
                }
                return instance;
            }
        }
    }
}
