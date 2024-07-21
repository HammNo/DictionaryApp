using DictionaryApp.Models;
using System.Text.Json;

namespace DictionaryApp.Services
{
    public class JsonFileService
    {

        private readonly string JSONFileFullPath = string.Empty;
        private const string JSONFileName = "WordsCache.json";

        public JsonFileService()
        {
            JSONFileFullPath = Path.Combine(FileSystem.Current.CacheDirectory, JSONFileName);
        }

        public bool CreateJSONFile(bool deleteIfExists = false)
        {
            if (!File.Exists(JSONFileFullPath))
            {
                File.Create(JSONFileFullPath);
                return true;
            }
            if (File.Exists(JSONFileFullPath) && deleteIfExists)
            {
                bool couldDelete = false;
                while (!couldDelete)
                {
                    try
                    {
                        File.Delete(JSONFileFullPath);
                        couldDelete = true;
                    }
                    catch (Exception) { }
                }
                File.Create(JSONFileFullPath);
                return true;
            }
            return false;
        }

        public async Task<List<WordResponseStorageModel>?> RetrieveWordResponsesFromStorage()
        {
            List<WordResponseStorageModel>? wordResponsesFileCollection = null;

            using (FileStream stream = File.Open(JSONFileFullPath, FileMode.Open))
            {
                try
                {
                    wordResponsesFileCollection 
                        = await JsonSerializer.DeserializeAsync<List<WordResponseStorageModel>?>(stream);
                }
                catch (Exception)
                {
                    stream.Close();
                    CreateJSONFile(true);
                }
            }

            return wordResponsesFileCollection;
        }

        public async Task WriteWordResponses(string jsonString)
        {
            await File.WriteAllTextAsync(JSONFileFullPath, jsonString);
        }
    }
}
