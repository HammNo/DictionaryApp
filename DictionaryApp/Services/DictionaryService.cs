using DictionaryApp.Extensions;
using DictionaryApp.Models;
using RestSharp;
using System.Text.Json;

namespace DictionaryApp.Services
{
    public class DictionaryService
    {
        private const string DictionaryAPIUri = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
        private const string APIKey = "a653454b-83d8-4522-97ec-e5e5a3e8b441";
        private const int StorageTimeInMinutes = 10;
        private readonly JsonFileService _jsonFileService;

        public DictionaryService(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public async Task<List<WordResponseStorageModel>?> RetrieveWordResponsesAsync(string requestedWord, bool extensiveSearch = false)
        {
            List<WordResponseStorageModel>? wordResponses 
                = await RetrieveWordResponsesFromJSON(requestedWord);

            if (wordResponses != null)
                return wordResponses;

            wordResponses = (await GetWordResponsesFromAPI(requestedWord))?
                            .Select(wr => new WordResponseStorageModel().FromBase(wr)).ToList();

            if (wordResponses != null && wordResponses.Count > 0)
                await WriteWordResponsesInJSON(wordResponses, requestedWord);

            return wordResponses;
        }

        private async Task<List<WordResponseStorageModel>?> RetrieveWordResponsesFromJSON(string requestedWord)
        {
            List<WordResponseStorageModel>? wordResponses = null;

            if (!_jsonFileService.CreateJSONFile())
            {
                List<WordResponseStorageModel>? wordResponsesFileCollection 
                    = await _jsonFileService.RetrieveWordResponsesFromStorage();

                if (wordResponsesFileCollection != null)
                    wordResponses = await ReadWordFromJSON(requestedWord, wordResponsesFileCollection);
            }

            return wordResponses;
        }

        private async Task<List<WordResponseStorageModel>?> ReadWordFromJSON(string requestedWord, List<WordResponseStorageModel> wordResponsesFileCollection)
        {
            List<WordResponseStorageModel>? wordResponses = null;

            List<WordResponseStorageModel>? wordResponsesFileCollectionCopy = new(wordResponsesFileCollection);

            bool foundOutOfDateWordResponses = false;
            foreach (var wordOccurence in wordResponsesFileCollection)
            {
                if (wordOccurence?.StorageDate.AddMinutes(StorageTimeInMinutes) < DateTime.Now)
                {
                    wordResponsesFileCollectionCopy.Remove(wordOccurence);
                    foundOutOfDateWordResponses = true;
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

            if (foundOutOfDateWordResponses)
            {
                string jsonString = JsonSerializer.Serialize(wordResponsesFileCollectionCopy);

                await _jsonFileService.WriteWordResponses(jsonString);
            }
            
            return wordResponses;
        }


        private async Task<List<WordResponseModel>?> GetWordResponsesFromAPI(string requestedWord)
        {
            List<WordResponseModel>? wordResponses = null;

            try
            {
                RestClientOptions options = new RestClientOptions(DictionaryAPIUri);
                RestClient client = new(options);
                wordResponses = await client.GetAsync<List<WordResponseModel>?>($"{requestedWord}?key={APIKey}");
            }
            catch (Exception) {}

            return wordResponses;
        }

        private async Task WriteWordResponsesInJSON(List<WordResponseStorageModel> wordResponses, string requestedWord)
        {
            List<WordResponseStorageModel>? wordResponsesFileCollection = 
                await _jsonFileService.RetrieveWordResponsesFromStorage(); 

            wordResponsesFileCollection ??= new List<WordResponseStorageModel>();

            wordResponses.ForEach((wr) =>
            {
                wr.StorageDate = DateTime.Now;
                wr.WordEntry = requestedWord.ToLower();
            });

            wordResponses.ForEach(wr => wordResponsesFileCollection.Add(wr));

            string jsonString = JsonSerializer.Serialize(wordResponsesFileCollection);

            await _jsonFileService.WriteWordResponses(jsonString);
        }
    }
}
