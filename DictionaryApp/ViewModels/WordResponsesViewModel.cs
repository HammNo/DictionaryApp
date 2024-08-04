using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using DictionaryApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DictionaryApp.ViewModels
{
    public partial class WordResponsesViewModel : ObservableObject
    {
        public ObservableCollection<WordResponseModel> ModifiedWordResponsesList { get; set; } = [];
        private readonly ObservableCollection<WordResponseModel> _originalResponsesList = [];
        private readonly DictionaryService _dictionaryService;
        private const int DefinitionMaxSize = 10;

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

            if (wordResponseCopy.ShortDef != null && wordResponseCopy.ShortDef.Count > 0
                && wordResponseCopy.ShortDef[0].Length >= DefinitionMaxSize)
                wordResponseCopy.ShortDef[0] = $"{wordResponseCopy.ShortDef[0][..DefinitionMaxSize]} [...]";
            return wordResponseCopy;
        }

        public ICommand ExpandWordCommand => new Command<WordResponseModel>((item) =>
        {
            item.Fl = "test";
        });
    }
}
