namespace DictionaryApp.Models
{
    public class WordResponseModel : ICloneable
    {
        public Meta Meta { get; set; } = default!;
        public List<string> ShortDef { get; set; } = default!;
        public string Fl { get; set; } = string.Empty;

        public WordResponseModel() {}

        public WordResponseModel(Meta meta, List<string> shortDef)
        {
            Meta = meta;
            ShortDef = shortDef;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Meta
    {
        public string Id { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
        public string Sort { get; set; } = string.Empty;
        public string Src { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public List<string> Stems { get; set; } = default!;
        public bool Offensive { get; set; }
    }
}
