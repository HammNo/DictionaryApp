using DictionaryApp.Models;

namespace DictionaryApp.Extensions
{
    public static class WordResponseStorageModelExtensions
    {
        public static WordResponseStorageModel FromBase(this WordResponseStorageModel model, WordResponseModel wordResponse)
        {
            return new WordResponseStorageModel
            {
                Meta = wordResponse.Meta,
                ShortDef = wordResponse.ShortDef,
                Fl = wordResponse.Fl
            };
        } 
    }
}
