using System.Text;

namespace EvilDICOM.Network.PDUs.Items
{
    public class AsyncOperations
    {
        public int MaxInvokeOperations { get; set; }
        public int MaxPerformOperations { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("ASYNC OPS");
            sb.AppendLine("-------------");
            sb.AppendLine(string.Format("Max Invoke Ops : {0}", MaxInvokeOperations));
            sb.AppendLine(string.Format("Max Perform Ops : {0}", MaxPerformOperations));
            return sb.ToString();
        }
    }
}