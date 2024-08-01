using DictionaryApp.ViewModels;

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
}