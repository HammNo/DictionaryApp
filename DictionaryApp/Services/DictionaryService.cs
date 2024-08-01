using DictionaryApp.Extensions;
using DictionaryApp.Models;
using RestSharp;
using System.Text.Json;

namespace DictionaryApp.Services
{
    public class DictionaryService
    {
        private const string DictionaryAPIUri = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
        private readonly JsonFileService _jsonFileService;

        public DictionaryService(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public async Task<List<WordResponseStorageModel>?> RetrieveWordResponsesAsync(string requestedWord, bool extensiveSearch = false)
        {
            using (FileStream stream = File.Open(_jsonFileService.WordResponsesFileFullPath, FileMode.OpenOrCreate))
            {
                List<WordResponseStorageModel>? wordResponsesFileCollection;
                List<WordResponseStorageModel>? wordResponses;

                (wordResponses, wordResponsesFileCollection) = await RetrieveWordResponsesFromJSON(requestedWord, stream);

                if (wordResponses != null)
                    return wordResponses;

                wordResponses = (await GetWordResponsesFromAPI(requestedWord))?
                                .Select(wr => new WordResponseStorageModel().FromBase(wr)).ToList();

                if (wordResponses != null && wordResponses.Count > 0)
                    await WriteWordResponsesInJSON(wordResponses, requestedWord, wordResponsesFileCollection, stream);

                return wordResponses;
            }
        }

        private async Task<(List<WordResponseStorageModel>?, List<WordResponseStorageModel>?)> RetrieveWordResponsesFromJSON(string requestedWord, FileStream stream)
        {
            List<WordResponseStorageModel>? wordResponses = null;

            List<WordResponseStorageModel>? wordResponsesFileCollection
                = await _jsonFileService.RetrieveWordResponsesFromStorage(stream);

            if (wordResponsesFileCollection != null)
                (wordResponses, wordResponsesFileCollection) = await ReadWordFromJSON(requestedWord, wordResponsesFileCollection, stream);

            return (wordResponses, wordResponsesFileCollection);
        }

        private async Task<(List<WordResponseStorageModel>?, List<WordResponseStorageModel>?)> ReadWordFromJSON(string requestedWord, List<WordResponseStorageModel> wordResponsesFileCollection, FileStream stream)
        {
            List<WordResponseStorageModel>? wordResponses = null;

            List<WordResponseStorageModel>? wordResponsesFileCollectionCopy = new(wordResponsesFileCollection);

            foreach (var wordOccurence in wordResponsesFileCollection)
            {
                if (wordOccurence?.StorageDate.AddMinutes(_jsonFileService.Configuration.StorageTime) < DateTime.Now)
                {
                    wordResponsesFileCollectionCopy.Remove(wordOccurence);
                }
                else
                {
                    if (wordOccurence != null)
                    {
                        if (wordOccurence.WordEntry.Equals(requestedWord, StringComparison.CurrentCultureIgnoreCase))
                        {
                            wordResponses ??= new List<WordResponseStorageModel>();
                            wordResponses?.Add(wordOccurence);
                        }
                    }
                }
            }
            
            return (wordResponses, wordResponsesFileCollectionCopy);
        }


        private async Task<List<WordResponseModel>?> GetWordResponsesFromAPI(string requestedWord)
        {
            List<WordResponseModel>? wordResponses = null;

            try
            {
                RestClientOptions options = new RestClientOptions(DictionaryAPIUri);
                RestClient client = new(options);
                wordResponses = await client.GetAsync<List<WordResponseModel>?>($"{requestedWord}?key={_jsonFileService.Configuration.APIKey}");
            }
            catch (Exception) {}

            return wordResponses;
        }

        private async Task WriteWordResponsesInJSON(List<WordResponseStorageModel> wordResponses, string requestedWord, List<WordResponseStorageModel>? wordResponsesFileCollection, FileStream stream)
        {
            wordResponsesFileCollection ??= new List<WordResponseStorageModel>();

            wordResponses.ForEach((wr) =>
            {
                wr.StorageDate = DateTime.Now;
                wr.WordEntry = requestedWord.ToLower();
            });

            wordResponses.ForEach(wr => wordResponsesFileCollection.Add(wr));

            string jsonString = JsonSerializer.Serialize(wordResponsesFileCollection);

            _jsonFileService.ClearJSONFile(stream);

            await _jsonFileService.WriteJsonData(jsonString, stream);
        }
    }
}
