using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using DictionaryApp.Services;

namespace DictionaryApp.ViewModels
{
    public partial class ConfigurationViewModel : ObservableObject
    {
        [ObservableProperty]
        public ConfigurationModel configurationModel;

        private readonly JsonFileService _jsonFileService;

        public ConfigurationViewModel(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;

            configurationModel = new ConfigurationModel
            {
                APIKey = _jsonFileService.Configuration.APIKey,
                StorageTime = _jsonFileService.Configuration.StorageTime,
                WordResponsesFileName = _jsonFileService.Configuration.WordResponsesFileName
            };
        }

        private bool HasChanged()
        {
            if (ConfigurationModel.APIKey != _jsonFileService.Configuration.APIKey
                || ConfigurationModel.StorageTime != _jsonFileService.Configuration.StorageTime
                || ConfigurationModel.WordResponsesFileName != _jsonFileService.Configuration.WordResponsesFileName)
                return true;
            return false;
        }
    }
}
