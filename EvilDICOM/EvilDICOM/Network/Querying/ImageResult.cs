using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Querying
{
    public class ImageResult : SeriesResult
    {
        public string SopInstanceUid { get; set; }
    }
}
