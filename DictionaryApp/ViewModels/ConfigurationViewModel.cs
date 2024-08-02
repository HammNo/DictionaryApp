using CommunityToolkit.Mvvm.ComponentModel;
using DictionaryApp.Models;
using DictionaryApp.Services;

namespace DictionaryApp.ViewModels
{
    public partial class ConfigurationViewModel : ObservableObject
    {
        [ObservableProperty]
        public ConfigurationModel configurationModel = new();

        private readonly JsonFileService _jsonFileService;

        public ConfigurationViewModel(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;

            MapInternalConfiguration();
        }

        public bool HasChanged()
        {
            if (ConfigurationModel.APIKey != _jsonFileService.Configuration.APIKey
                || ConfigurationModel.StorageTime != _jsonFileService.Configuration.StorageTime
                || ConfigurationModel.WordResponsesFileName != _jsonFileService.Configuration.WordResponsesFileName)
                return true;
           else 
                return false;
        }

        public async Task<bool> WriteChanges()
        {
            ConfigurationModel? configuration = null;

            if (HasChanged())
            {
                configuration = await _jsonFileService.WriteNewConfiguration(ConfigurationModel);

                MapInternalConfiguration();
            }

            if (configuration != null)
                return true;
            return false;
        }

        private void MapInternalConfiguration()
        {
            ConfigurationModel = new ConfigurationModel
            {
                APIKey = _jsonFileService.Configuration.APIKey,
                StorageTime = _jsonFileService.Configuration.StorageTime,
                WordResponsesFileName = _jsonFileService.Configuration.WordResponsesFileName
            };
        }
    }
}
