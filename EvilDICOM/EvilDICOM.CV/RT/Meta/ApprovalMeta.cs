using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class ApprovalMeta
    {
        public string ApprovedBy { get; set; }
        public string ApprovalType { get; set; }
        public DateTime ApprovedOn { get; set; }
    }
}
