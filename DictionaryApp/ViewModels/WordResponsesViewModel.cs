using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using DictionaryApp.Services;
using System.Collections.ObjectModel;

namespace DictionaryApp.ViewModels
{
    public partial class WordResponsesViewModel : ObservableObject
    {
        public ObservableCollection<WordResponseModel> ModifiedWordResponsesList { get; set; } = [];
        public ObservableCollection<WordResponseModel> OriginalResponsesList { get; set; } = [];
        private readonly DictionaryService _dictionaryService;
        private const int DefinitionMaxSize = 80;

        public WordResponsesViewModel(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task SearchWord(string word)
        {
            var responses = await _dictionaryService.RetrieveWordResponsesAsync(word);
            OriginalResponsesList.Clear();
            responses?.ForEach(wr => OriginalResponsesList.Add(wr));
            ModifiedWordResponsesList.Clear();
            OriginalResponsesList.ToList().ForEach(wr => ModifiedWordResponsesList.Add(ProcessWordResponse(wr)));
        }

        private WordResponseModel ProcessWordResponse(WordResponseModel wordResponse)
        {
            WordResponseModel wordResponseCopy = (WordResponseModel)wordResponse.Clone();

            if (wordResponseCopy.Meta.Id.Contains(':'))
                wordResponseCopy.Meta.Id = wordResponseCopy.Meta.Id.Split(':')[0];

            if (wordResponseCopy.ShortDef != null && wordResponseCopy.ShortDef.Count > 0
                && wordResponseCopy.ShortDef[0].Length >= DefinitionMaxSize)
                wordResponseCopy.ShortDef[0] = $"{wordResponseCopy.ShortDef[0][..DefinitionMaxSize]} [...]";
            return wordResponseCopy;
        }
    }
}
