using EvilDICOM.CV.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class StructureSetMeta
    {
        public StructureSetMeta()
        {
        }
        public Dictionary<string, StructureMeta> Structures { get; internal set; } = new Dictionary<string, StructureMeta>();
    }
}
