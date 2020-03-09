using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class CalculationOptionMeta
    {
        public string Particle { get; set; }
        public string AlgorithmType { get; set; }
        public string AlgorithmName { get; set; }

        public Dictionary<string, string> Options = new Dictionary<string, string>();
    }
}
