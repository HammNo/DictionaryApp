namespace DictionaryApp.Models
{
    public class ConfigurationModel
    {
        public string WordResponsesFileName { get; set; } = string.Empty;
        public string APIKey { get; set; } = string.Empty;
        public short StorageTime { get; set; }
    }
}
