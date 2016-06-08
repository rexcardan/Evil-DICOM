namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class PDVItem.
    /// </summary>
    public class PDVItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDVItem"/> class.
        /// </summary>
        public PDVItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDVItem"/> class.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <param name="frag">The frag.</param>
        public PDVItem(int contextId, PDVItemFragment frag)
        {
            PresentationContextID = contextId;
            Fragment = frag;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDVItem"/> class.
        /// </summary>
        /// <param name="contextId">The context identifier.</param>
        /// <param name="isCommand">if set to <c>true</c> [is command].</param>
        /// <param name="isLast">if set to <c>true</c> [is last].</param>
        /// <param name="data">The data.</param>
        public PDVItem(int contextId, bool isCommand, bool isLast, byte[] data)
        {
            PresentationContextID = contextId;
            Fragment = new PDVItemFragment
            {
                Data = data,
                IsCommandObject = isCommand,
                IsLastItem = isLast
            };
        }

        /// <summary>
        /// Gets or sets the presentation context identifier.
        /// </summary>
        /// <value>The presentation context identifier.</value>
        public int PresentationContextID { get; set; }
        /// <summary>
        /// Gets or sets the fragment.
        /// </summary>
        /// <value>The fragment.</value>
        public PDVItemFragment Fragment { get; set; }
    }
}