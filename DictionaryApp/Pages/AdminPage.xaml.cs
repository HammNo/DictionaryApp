using CommunityToolkit.Maui.Core;
using DictionaryApp.ViewModels;
using CommunityToolkit.Maui.Alerts;

namespace DictionaryApp.Pages;

public partial class AdminPage : ContentPage
{
    private readonly ConfigurationViewModel _configurationViewModel;

    public AdminPage(ConfigurationViewModel configurationViewModel)
	{
		InitializeComponent();
        _configurationViewModel = configurationViewModel;
        BindingContext = _configurationViewModel;
	}

    private void HasInputChanged(object sender, TextChangedEventArgs e) => HasInputChanged();

    private async void ButtonClicked_EventHandler(object sender, EventArgs e)
    {
        EntriesKeyboardReset();

        await _configurationViewModel.WriteChanges();

        Color backgroundColor = Colors.DarkSeaGreen;
        string message = "Configuration changed";

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

        HasInputChanged();
    }

    private void HasInputChanged()
    {
        if (_configurationViewModel.HasChanged())
            ConfirmButton.IsEnabled = true;
        else
            ConfirmButton.IsEnabled = false;
    }

    private void EntriesKeyboardReset()
    {
        StorageTime.IsEnabled = false;
        APIKey.IsEnabled = false;
        WordResponsesFileName.IsEnabled = false;
        StorageTime.IsEnabled = true;
        APIKey.IsEnabled = true;
        WordResponsesFileName.IsEnabled = true;
    }
}