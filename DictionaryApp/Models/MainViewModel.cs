using CommunityToolkit.Mvvm.ComponentModel;

namespace DictionaryApp.Models
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string inpuText;
    }
}
