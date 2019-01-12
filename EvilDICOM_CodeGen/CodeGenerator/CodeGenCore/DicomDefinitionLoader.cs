using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeGenCore
{
    public static class DicomDefinitionLoader
    {
        private const string DefinitionDir = @"C:\Users\Tirth\Downloads\dicom_nema";

        private static readonly XNamespace XmlNs = "http://www.w3.org/XML/1998/namespace";
        private static readonly XNamespace DocNs = "http://docbook.org/ns/docbook";

        public static async Task DownloadCurrentDefinitions()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(@"http://dicom.nema.org/medical/dicom/current/source/docbook/") })
                foreach (var part in new[] { 4, 6, 7, 15 })
                {
                    var partName = $"part{part:D2}";
                    await client.DownloadFile($"{partName}/{partName}.xml", Path.Combine(DefinitionDir, $"{partName}.xml"));
                }
        }

        internal static async Task<string> DownloadFile(this HttpClient client, string address, string fileName)
        {
            var file = new FileInfo(fileName);
            if (file.Exists)
                file.Delete();

            using (var contentStream = await client.GetStreamAsync(address))
            using (var fileStream = file.OpenWrite())
                await contentStream.CopyToAsync(fileStream);

            return file.FullName;
        }

        private static IEnumerable<List<string>> GetTableData(int partNum, string tableId)
        {
            var doc = XDocument.Load(Path.Combine(DefinitionDir, $"part{partNum:D2}.xml"));

            // Standard SOP Classes Table
            var table = doc.Descendants(DocNs + "table").SingleOrDefault(t => t.Attribute(XmlNs + "id")?.Value == tableId);
            if (table == null)
            {
                Console.WriteLine($"Couldn't get table {tableId} from {partNum}");
                yield break;
            }

            var rows = table.Descendants(DocNs + "tbody").First().Descendants(DocNs + "tr");
            foreach (var row in rows)
                yield return row.Elements(DocNs + "td").Select(e => e.Value).ToList();
        }

        public static IEnumerable<DictionaryData> LoadCurrentSopClasses()
        {
            foreach (var rowData in GetTableData(4, "table_B.5-1"))
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
            foreach (var rowData in GetTableData(15, "table_E.1-1"))
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
            var data = GetTableData(6, "table_7-1") // file meta
                .Concat(GetTableData(6, "table_8-1")) // directory elements
                .Concat(GetTableData(6, "table_6-1")) // data elements
                .Concat(GetTableData(7, "table_E.1-1")); // command elements

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