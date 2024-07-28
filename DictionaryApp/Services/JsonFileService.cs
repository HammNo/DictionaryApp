using DictionaryApp.Models;
using System.Text;
using System.Text.Json;

namespace DictionaryApp.Services
{
    public class JsonFileService
    {
        public string WordResponsesFileFullPath { get; set; } = string.Empty;
        private const string DefaultWordResponsesFileName = "WordsCache.json";
        private const short DefaultStorageTime = 10;

        public string ConfigurationFileFullPath { get; set; } = string.Empty;
        private const string ConfigurationFileName = "Configuration.json";

        private ConfigurationModel? _configuration = new();

        public JsonFileService()
        {
            ConfigurationFileFullPath = Path.Combine(FileSystem.Current.CacheDirectory, ConfigurationFileName);

            _ = Initialize();
        }

        private async Task Initialize()
        {
            using (FileStream stream = File.Open(ConfigurationFileFullPath, FileMode.OpenOrCreate))
            {
                _configuration = await RetrieveConfigurationFromStorage(stream);

                _configuration ??= await InitializeConfiguration(stream);

                WordResponsesFileFullPath
                    = Path.Combine(FileSystem.Current.CacheDirectory, _configuration.WordResponsesFileName);
            }
        }

        private async Task<ConfigurationModel> InitializeConfiguration(FileStream stream)
        {
            ConfigurationModel configuration = new ConfigurationModel
            {
                WordResponsesFileName = DefaultWordResponsesFileName,
                StorageTime = DefaultStorageTime,
            };

            string jsonString = JsonSerializer.Serialize(configuration);

            await WriteJsonData(jsonString, stream);

            return configuration;
        }

        public async Task<ConfigurationModel?> RetrieveConfigurationFromStorage(FileStream stream)
        {
            ConfigurationModel? configuration = null;

            try
            {
                configuration
                    = await JsonSerializer.DeserializeAsync<ConfigurationModel>(stream);
            }
            catch (Exception)
            {
                ClearJSONFile(stream);
            }

            return configuration;
        }

        public void ClearJSONFile(FileStream stream)
        {
            try
            {
                stream.SetLength(0);
            }
            catch (Exception) { }
        }

        public async Task<List<WordResponseStorageModel>?> RetrieveWordResponsesFromStorage(FileStream stream)
        {
            List<WordResponseStorageModel>? wordResponsesFileCollection = null;

            try
            {
                wordResponsesFileCollection
                    = await JsonSerializer.DeserializeAsync<List<WordResponseStorageModel>?>(stream);
            }
            catch (Exception)
            {
                ClearJSONFile(stream);
            }

            return wordResponsesFileCollection;
        }


        public async Task WriteJsonData(string jsonString, FileStream stream) => await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonString));
    }
}
