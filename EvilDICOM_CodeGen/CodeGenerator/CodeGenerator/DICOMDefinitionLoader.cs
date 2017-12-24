using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace CodeGenerator
{
    internal class DICOMDefinitionLoader
    {
        private static string part6Path = @"..\..\Resources\part06.xml";
        private static string part7Path = @"..\..\Resources\part07.xml";
        private static string part4Path = @"..\..\Resources\part04.xml";
        private static string part15Path = @"..\..\Resources\part15.xml";

        internal static void UpdateCurrentFromWeb()
        {
            //Online XML File
            var currentDictionary = @"http://dicom.nema.org/medical/dicom/current/source/docbook/part06/part06.xml";

            using (var client = new WebClient())
            {
                client.DownloadFile(currentDictionary, part6Path);
            }
        }

        public static IEnumerable<DictionaryData> LoadCurrentSOPClasses()
        {
            XNamespace ns = "http://docbook.org/ns/docbook";
            var xdoc = XDocument.Load(part4Path);
            //Get table ( <table frame="box" label="6-1" rules="all" xml:id="table_6-1">)
            var table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "B.5-1");
            var elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var id = data[1];
                var name = data[0];
                var keyword = data[0]
                    .Replace(" ", "")
                    .Replace("Study Root Query/Retrieve Information Model - ","C_")
                    .Replace("-","_")
                    .Replace("/","")
                    .Replace("12_", "_12_");
                yield return new DictionaryData() { Id = id, Name = name, Keyword = keyword };
            }
        }

        internal static IEnumerable<DictionaryData> LoadAnonymizationTags()
        {
            XNamespace ns = "http://docbook.org/ns/docbook";
            var xdoc = XDocument.Load(part15Path);
            //Get table ( <table frame="box" label="E.1-1" rules="all" xml:id="table_6-1">)
            var table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "E.1-1");
            var elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var name = data[0];
                var profileTaskKey = data[4];
                var keyword = data[0]
                   .Replace(" ", "")
                   .Replace("Study Root Query/Retrieve Information Model - ", "C_")
                   .Replace("-", "_")
                   .Replace("/", "")
                   .Replace("12_", "_12_");
                var tag = data[1].Replace("(", "").Replace(")", "").Replace(",", "");
                yield return new DictionaryData() { Id = tag, Name = name, Keyword = keyword, Metadata=profileTaskKey };
            }
        }

        public static IEnumerable<DictionaryData> LoadCurrentDictionary()
        {
            XNamespace ns = "http://docbook.org/ns/docbook";
            var xdoc = XDocument.Load(part6Path);
            //Get table ( <table frame="box" label="6-1" rules="all" xml:id="table_6-1">)
            var table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "7-1");
            var elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var id = data[0].Replace("(", "").Replace(")", "").Replace(",", "");
                var name = data[1];
                var keyword = data[2];
                var vr = data[3];
                var vm = data[4];
                if (!string.IsNullOrEmpty(vr))
                    yield return new DictionaryData() { Id = id, Name = name, Keyword = keyword, VR = vr, VM = vm };
            }

            table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "8-1");
            elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var id = data[0].Replace("(", "").Replace(")", "").Replace(",", "");
                var name = data[1];
                var keyword = data[2];
                var vr = data[3];
                var vm = data[4];
                if (!string.IsNullOrEmpty(vr))
                    yield return new DictionaryData() { Id = id, Name = name, Keyword = keyword, VR = vr, VM = vm };
            }

            table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "6-1");
            elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var id = data[0].Replace("(", "").Replace(")", "").Replace(",", "");
                var name = data[1];
                var keyword = data[2];
                var vr = data[3];
                var vm = data[4];
                if (!string.IsNullOrEmpty(vr))
                    yield return new DictionaryData() { Id = id, Name = name, Keyword = keyword, VR = vr, VM = vm };
            }

            //PART 7 
            xdoc = XDocument.Load(part7Path);
            //Get table ( <table frame="box" label="6-1" rules="all" xml:id="table_6-1">)
            table = xdoc.Descendants(ns + "table").First(el => el.Attribute("label").Value == "E.1-1");
            elements = table.Descendants(ns + "tbody").First().Descendants(ns + "tr");
            foreach (var el in elements)
            {
                var data = el.Elements(ns + "td").Select(e => e.Value).ToList();
                var id = data[0].Replace("(", "").Replace(")", "").Replace(",", "");
                var name = data[1];
                var keyword = data[2];
                var vr = data[3];
                var vm = data[4];
                if (!string.IsNullOrEmpty(vr))
                    yield return new DictionaryData() { Id = id, Name = name, Keyword = keyword, VR = vr, VM = vm };
            }

        }
    }
}