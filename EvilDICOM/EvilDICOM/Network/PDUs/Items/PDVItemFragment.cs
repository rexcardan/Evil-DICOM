namespace EvilDICOM.Network.PDUs.Items
{
    public class PDVItemFragment
    {
        public bool IsCommandObject { get; set; }
        public bool IsLastItem { get; set; }
        public byte[] Data { get; set; }
    }
}