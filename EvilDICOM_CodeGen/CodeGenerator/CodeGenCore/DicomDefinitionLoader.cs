using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CodeGenCore
{
    public static class DicomDefinitionLoader
    {
        private const string Part6Path = @"C:\Users\Tirth\Programming\Evil-DICOM\EvilDICOM_CodeGen\CodeGenerator\CodeGenCore\Resources\part06.xml";
        private const string Part7Path = @"C:\Users\Tirth\Programming\Evil-DICOM\EvilDICOM_CodeGen\CodeGenerator\CodeGenCore\Resources\part07.xml";
        private const string Part4Path = @"C:\Users\Tirth\Programming\Evil-DICOM\EvilDICOM_CodeGen\CodeGenerator\CodeGenCore\Resources\part04.xml";
        private const string Part15Path = @"C:\Users\Tirth\Programming\Evil-DICOM\EvilDICOM_CodeGen\CodeGenerator\CodeGenCore\Resources\part15.xml";

        private static readonly XNamespace XmlNs = "http://www.w3.org/XML/1998/namespace";
        private static readonly XNamespace DocNs = "http://docbook.org/ns/docbook";

        private static IEnumerable<List<string>> GetTableData(string docPath, string tableId)
        {
            var doc = XDocument.Load(docPath);

            // Standard SOP Classes Table
            var table = doc.Descendants(DocNs + "table").SingleOrDefault(t => t.Attribute(XmlNs + "id")?.Value == tableId);
            if (table == null)
            {
                Console.WriteLine($"Couldn't get table {tableId} from {docPath}");
                yield break;
            }

            var rows = table.Descendants(DocNs + "tbody").First().Descendants(DocNs + "tr");
            foreach (var row in rows)
                yield return row.Elements(DocNs + "td").Select(e => e.Value).ToList();
        }

        public static IEnumerable<DictionaryData> LoadCurrentSopClasses()
        {
            foreach (var rowData in GetTableData(Part4Path, "table_B.5-1"))
            {
                var id = rowData[1];
                var name = rowData[0];

                var keyword = name
                    .Replace(" ", "")
                    .Replace("-", "_")
                    .Replace("/", "") // for XA/XR
                    .Replace("12_", "_12_"); // for 12-lead ECG

                yield return new DictionaryData { Id = id, Name = name, Keyword = keyword };
            }
        }

        public static IEnumerable<DictionaryData> LoadAnonymizationTags()
        {
            foreach (var rowData in GetTableData(Part15Path, "table_E.1-1"))
            {
                var id = rowData[1];
                var name = rowData[0];
                var profileTaskKey = rowData[4];

                var keyword = name
                    .Replace(" ", "")
                    .Replace("-", "_")
                    .Replace("/", "") // for XA/XR
                    .Replace("12_", "_12_"); // for 12-lead ECG

                var tag = CleanId(id);

                yield return new DictionaryData { Id = tag, Name = name, Keyword = keyword, Metadata = profileTaskKey };
            }
        }

        private static string CleanId(string id) 
            => id
                .Replace("(", "")
                .Replace(")", "")
                .Replace(",", "");

        public static IEnumerable<DictionaryData> LoadCurrentDictionary()
        {
            var data = GetTableData(Part6Path, "table_7-1") // file meta
                .Concat(GetTableData(Part6Path, "table_8-1")) // directory elements
                .Concat(GetTableData(Part6Path, "table_6-1")) // data elements
                .Concat(GetTableData(Part7Path, "table_E.1-1")); // command elements

            foreach (var rowData in data)
            {
                var id = CleanId(rowData[0]);

                var name = rowData[1];
                var keyword = rowData[2];
                var vr = rowData[3];
                var vm = rowData[4];

                if (!string.IsNullOrEmpty(vr))
                    yield return new DictionaryData
                    {
                        Id = id,
                        Name = name,
                        Keyword = keyword,
                        VR = vr,
                        VM = vm
                    };
            }
        }
    }
}