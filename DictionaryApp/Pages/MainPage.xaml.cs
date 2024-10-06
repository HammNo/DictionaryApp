using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using DictionaryApp.Contents;
using DictionaryApp.ViewModels;

namespace DictionaryApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly WordResponsesViewModel _wordResponsesViewModel;

        private IReadOnlyList<IVisualTreeElement>? rootViews;

        public MainPage(WordResponsesViewModel wordResponsesViewModel)
        {
            InitializeComponent();

            BindingContext = wordResponsesViewModel;
            _wordResponsesViewModel = wordResponsesViewModel;
            ClearImageButton.IsVisible = false;

            EnsureCloseExpander();
        }

        public async void OnWordEntryCompleted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Word.Text))
            {
                var popup = new SpinnerPopup();
                this.ShowPopup(popup);
                await _wordResponsesViewModel.SearchWord(Word.Text);
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

        private void ManageWordResponsesCount()
        {
            short resultsNumber = (short)_wordResponsesViewModel.ModifiedWordResponsesList.Count;
            if (resultsNumber == 0)
                ResultsCountLabel.Text = "No result";
            else
                ResultsCountLabel.Text = $"{resultsNumber} result{((resultsNumber > 1) ? 's' : null)}";
        }

        private async void OnClearButtonClicked(object sender, EventArgs args)
        {
            Word.Text = string.Empty;
            _wordResponsesViewModel.ModifiedWordResponsesList.Clear();
            ResultsCountLabel.Text = string.Empty;
            ClearImageButton.IsVisible = false;

            //Problème avec affichage Snackbar sous Windows
            if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
            {
                var snackbar = Snackbar.Make("Search cleared", actionButtonText: string.Empty,
                                        duration: TimeSpan.FromSeconds(3), visualOptions: new SnackbarOptions
                                        {
                                            TextColor = Colors.Black,
                                            CornerRadius = 30,
                                            BackgroundColor = Colors.DarkSeaGreen
                                        });
                await snackbar.Show();
            }
        }

        private async void MatchingEntryFoundShowSnackbar()
        {
            short wordResponsesCount = (short)_wordResponsesViewModel.ModifiedWordResponsesList.Count;

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

            //Problème avec affichage Snackbar sous Windows
            if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
            {
                ISnackbar snackbar = Snackbar.Make(message, actionButtonText: string.Empty,
                                                duration: TimeSpan.FromSeconds(3), 
                                                visualOptions: new SnackbarOptions
                                                {
                                                    TextColor = Colors.Black,
                                                    CornerRadius = 30,
                                                    BackgroundColor = backgroundColor
                                                });

                await snackbar.Show();
            }

        }

        private void OnPlayPauseButtonClicked(object sender, EventArgs args)
        {
            var button = sender as Button;
            var player = button?.BindingContext as MediaElement;
            player?.Play();
            Task.Delay((int)player?.Duration.TotalMilliseconds);
            player.SeekTo(TimeSpan.Zero);
        }

        private void EnsureCloseExpander()
        {
            WordsCollectionView.ChildAdded += (sender, e) =>
            {
                var borderElement = e.Element as Border;

                if (borderElement != null)
                {
                    var children = borderElement.GetVisualTreeDescendants();

                    foreach (var child in children)
                    {
                        if (child is Expander expander)
                            expander.IsExpanded = false;
                    }
                }
            };
        }
    }
}
