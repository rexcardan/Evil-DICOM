using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class DICOMDictionary
    {
        private static DICOMDictionary instance;

        private DICOMDictionary()
        {
            Entries = DICOMDefinitionLoader.LoadCurrentDictionary().ToList();
        }

        public DICOMDictionary Generator { get; set; }

        public static DICOMDictionary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DICOMDictionary();
                }
                return instance;
            }
        }

        public List<DictionaryData> Entries { get; }
    }
}
