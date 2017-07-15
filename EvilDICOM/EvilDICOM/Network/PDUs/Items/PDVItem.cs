namespace EvilDICOM.Network.PDUs.Items
{
    public class PDVItem
    {
        public PDVItem()
        {
        }

        public PDVItem(int contextId, PDVItemFragment frag)
        {
            PresentationContextID = contextId;
            Fragment = frag;
        }

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

        public int PresentationContextID { get; set; }
        public PDVItemFragment Fragment { get; set; }
    }
}