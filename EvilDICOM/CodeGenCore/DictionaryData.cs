namespace EvilDICOM.CodeGenerator
{
    public class DictionaryData
    {
        public string Id { get; set; }
        public string Name { get; internal set; }
        public string Keyword { get; internal set; }
        public string VR { get; internal set; }
        public string VM { get; internal set; }
        public string Metadata { get; internal set; }
    }
}
