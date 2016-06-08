using System.Text;

namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class AsyncOperations.
    /// </summary>
    public class AsyncOperations
    {
        /// <summary>
        /// Gets or sets the maximum invoke operations.
        /// </summary>
        /// <value>The maximum invoke operations.</value>
        public int MaxInvokeOperations { get; set; }
        /// <summary>
        /// Gets or sets the maximum perform operations.
        /// </summary>
        /// <value>The maximum perform operations.</value>
        public int MaxPerformOperations { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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