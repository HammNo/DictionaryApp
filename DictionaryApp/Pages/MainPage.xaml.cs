using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using DictionaryApp.Contents;
using DictionaryApp.Models;
using DictionaryApp.Services;
using System.Collections.ObjectModel;

namespace DictionaryApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly DictionaryService _dictionaryService;
        private readonly ObservableCollection<WordResponseModel> LocalWordResponses = [];
        private const int DefinitionMaxSize = 80;
        public MainPage(DictionaryService dictionaryService)
        {
            InitializeComponent();
            _dictionaryService = dictionaryService;
            BindingContext = this;
            WordsCollectionView.ItemsSource = LocalWordResponses;
            ClearImageButton.IsVisible = false;
        }

        public async void OnCompleted(object sender, EventArgs e)
        {
            LocalWordResponses.Clear();
            if (!string.IsNullOrEmpty(Word.Text))
            {
                var popup = new SpinnerPopup();
                this.ShowPopup(popup);
                var result = await _dictionaryService.RetrieveWordResponsesAsync(Word.Text);
                result?.ForEach(wr => LocalWordResponses.Add(ProcessWordResponse(wr)));
                ClearImageButton.IsVisible = true;
                popup.Close();
            }
            else
                ClearImageButton.IsVisible = false;

            MatchingEntryFoundShowSnackbar();

            ManageWordResponsesCount();

            Word.IsEnabled = false;
            Word.IsEnabled = true;
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

        private void ManageWordResponsesCount()
        {
            short resultsNumber = (short)LocalWordResponses.Count;
            if (resultsNumber == 0)
                ResultsCountLabel.Text = "No result";
            else
                ResultsCountLabel.Text = $"{resultsNumber} result{((resultsNumber > 1) ? 's' : null)}";
        }

        private async void OnClearButtonClicked(object sender, EventArgs args)
        {
            Word.Text = string.Empty;
            LocalWordResponses.Clear();
            ManageWordResponsesCount();
            ClearImageButton.IsVisible = false;

            var snackbar = Snackbar.Make("Search cleared", actionButtonText: string.Empty,
                                    duration: TimeSpan.FromSeconds(3), visualOptions: new SnackbarOptions
                                    {
                                        TextColor = Colors.Black,
                                        CornerRadius = 30,
                                        BackgroundColor = Colors.Salmon
                                    });
            await snackbar.Show();
        }

        private async void MatchingEntryFoundShowSnackbar()
        {
            short wordResponsesCount = (short)LocalWordResponses.Count;

            string message = string.Empty;
            Color backgroundColor = default!;
            if (wordResponsesCount == 0)
            {
                message = "No matching entry found";
                backgroundColor = Colors.Salmon;
            }
            else
            {
                message = $"{wordResponsesCount} {((wordResponsesCount > 1) ? "entries were" : "entry was")} found";
                backgroundColor = Colors.DarkSeaGreen;
            }

            ISnackbar snackbar = Snackbar.Make(message, actionButtonText: string.Empty,
                        duration: TimeSpan.FromSeconds(3), visualOptions: new SnackbarOptions
                        {
                            TextColor = Colors.Black,
                            CornerRadius = 30,
                            BackgroundColor = backgroundColor
                        });

            await snackbar.Show();
        }
    }
}
