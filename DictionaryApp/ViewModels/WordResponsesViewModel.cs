using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using DictionaryApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DictionaryApp.ViewModels
{
    public partial class WordResponsesViewModel : ObservableObject
    {
        public ObservableCollection<WordResponseModel> ModifiedWordResponsesList { get; set; } = [];
        private readonly ObservableCollection<WordResponseModel> _originalResponsesList = [];
        private readonly DictionaryService _dictionaryService;
        private const int DefinitionMaxSize = 50;

        public WordResponsesViewModel(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public async Task SearchWord(string word)
        {
            var responses = await _dictionaryService.RetrieveWordResponsesAsync(word);
            _originalResponsesList.Clear();
            responses?.ForEach(wr => _originalResponsesList.Add(wr));
            ModifiedWordResponsesList.Clear();
            _originalResponsesList.ToList()
                .ForEach(wr => ModifiedWordResponsesList.Add(ProcessWordResponse(wr)));
        }

        private WordResponseModel ProcessWordResponse(WordResponseModel wordResponse)
        {
            WordResponseModel wordResponseCopy = wordResponse.DeepCopy();

            if (wordResponseCopy.Meta.Id.Contains(':'))
                wordResponseCopy.Meta.Id = wordResponseCopy.Meta.Id.Split(':')[0];

            for (short i = 1; i <= wordResponseCopy.ShortDef.Count; i++)
            {
                wordResponseCopy.ShortDef[i - 1] = $"{i}. {wordResponseCopy.ShortDef[i - 1]}";
            };

            return wordResponseCopy;
        }
    }
}
