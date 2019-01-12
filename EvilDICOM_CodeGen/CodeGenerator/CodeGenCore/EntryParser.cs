using System;
using System.Diagnostics;
using System.Reflection;
using EvilDICOM.Core;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class EntryParser
    {
        private static readonly Assembly EvilDicomAssembly;

        static EntryParser()
        {
            EvilDicomAssembly = Assembly.LoadFrom("EvilDICOM");
        }

        public static (string className, SyntaxNode node) Parse(this SyntaxGenerator g, DictionaryData entry)
        {
            if (entry.VR.StartsWith("See"))
                return (null, null);

            //var vr = entry.VR[0..2];
            var vr = entry.VR.Substring(0, 2);

            var className = VRDictionary.GetVRFromAbbreviation(vr).ToString();

            var i = (IDICOMElement)EvilDicomAssembly.CreateInstance($"EvilDICOM.Core.Element.{className}");

            if (className == "DateTime")
                className = "Element.DateTime";

            var dataType = i.DatType;

            // hack to make sure we are in right namespace - EvilDICOM has DateTime VR and so does system
            var dataTypeName = dataType.Name.StartsWith("Nullable") 
                ? "System.DateTime?" 
                : dataType.Name;

            // initialize strings as empty string instead of null
            var parameter = g.ParameterDeclaration("data", g.IdentifierName($"params {dataTypeName}[]"));

            return (className, parameter);
        }
    }
}
