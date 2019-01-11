using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace CodeGenCore
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("yo");

            var g = GeneratorBuilder.Instance.Generator;

            var thing = DicomDefinitionLoader.LoadCurrentSopClasses();
            Trace.WriteLine($"{thing.Take(20).JsonStr()}");

            Console.WriteLine("^");
            Console.ReadKey();
        }

        public static string JsonStr(this object obj, Formatting formatting = Formatting.Indented)
            => JsonConvert.SerializeObject(obj, formatting, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
