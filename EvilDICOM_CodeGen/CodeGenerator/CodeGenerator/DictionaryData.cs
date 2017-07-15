using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class DictionaryData
    {
        public string Id { get; set; }
        public string Name { get; internal set; }
        public string Keyword { get; internal set; }
        public string VR { get; internal set; }
        public string VM { get; internal set; }
    }
}
