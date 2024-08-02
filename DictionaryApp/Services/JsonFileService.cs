using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using System.Text;
using System.Text.Json;

namespace DictionaryApp.Services
{
    public partial class JsonFileService : ObservableObject
    {
        public string WordResponsesFileFullPath { get; set; } = string.Empty;
        private const string DefaultWordResponsesFileName = "WordsCache.json";
        private const short DefaultStorageTime = 10;

        private const string DefaultAPIKey = "a653454b-83d8-4522-97ec-e5e5a3e8b441";

        public string ConfigurationFileFullPath { get; set; } = string.Empty;
        private const string ConfigurationFileName = "Configuration.json";

        [ObservableProperty]
        public ConfigurationModel configuration = new();

        public JsonFileService()
        {
            ConfigurationFileFullPath = Path.Combine(FileSystem.Current.AppDataDirectory, ConfigurationFileName);

            _ = Initialize();
        }

        private async Task Initialize()
        {
            using (FileStream stream = File.Open(ConfigurationFileFullPath, FileMode.OpenOrCreate))
            {
                Configuration = await RetrieveConfigurationFromStorage(stream) ?? await InitializeConfiguration(stream);

                WordResponsesFileFullPath
                    = Path.Combine(FileSystem.Current.CacheDirectory, Configuration.WordResponsesFileName);
            }
        }

        private async Task<ConfigurationModel> InitializeConfiguration(FileStream stream)
        {
            ConfigurationModel configuration = new ConfigurationModel
            {
                WordResponsesFileName = DefaultWordResponsesFileName,
                StorageTime = DefaultStorageTime,
                APIKey = DefaultAPIKey
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

        public async Task<ConfigurationModel?> WriteNewConfiguration(ConfigurationModel newConfiguration)
        {
            try
            {
                using (FileStream stream = File.Open(ConfigurationFileFullPath, FileMode.OpenOrCreate))
                {
                    ClearJSONFile(stream);

                    string jsonString = JsonSerializer.Serialize(newConfiguration);

                    await WriteJsonData(jsonString, stream);

                    Configuration = newConfiguration;

                    WordResponsesFileFullPath
                        = Path.Combine(FileSystem.Current.CacheDirectory, Configuration.WordResponsesFileName);
                }

                return Configuration;
            }
            catch (Exception)
            {
                return null;
            }
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
