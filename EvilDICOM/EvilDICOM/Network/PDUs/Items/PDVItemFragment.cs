namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class PDVItemFragment.
    /// </summary>
    public class PDVItemFragment
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is command object.
        /// </summary>
        /// <value><c>true</c> if this instance is command object; otherwise, <c>false</c>.</value>
        public bool IsCommandObject { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is last item.
        /// </summary>
        /// <value><c>true</c> if this instance is last item; otherwise, <c>false</c>.</value>
        public bool IsLastItem { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data { get; set; }
    }
}