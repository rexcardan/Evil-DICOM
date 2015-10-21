using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Querying
{
    public class SeriesResult : StudyResult
    {
        public string SeriesUid { get; set; }
        public string Modality { get; set; }
    }
}
