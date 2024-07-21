namespace DictionaryApp.Models
{
    public class WordResponseStorageModel : WordResponseModel
    {
        public DateTime StorageDate { get; set; }
        public string WordEntry { get; set; } = string.Empty;

        public WordResponseStorageModel() {}

        public WordResponseStorageModel(Meta meta, List<string> shortDef) 
            : base(meta, shortDef) { }
    }
}
