using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DictionaryApp.Models
{
    public partial class WordResponseModel : ObservableObject
    {
        public Meta Meta { get; set; } = default!;

        public List<string> ShortDef { get; set; } = default!;

        [ObservableProperty]
        public string fl = string.Empty;

        public WordResponseModel() {}

        public WordResponseModel(Meta meta, List<string> shortDef)
        {
            Meta = meta;
            ShortDef = shortDef;
        }


        public WordResponseModel DeepCopy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<WordResponseModel>(serializedObject);
        }
    }

    public class Meta
    {
        public string Id { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
        public string Sort { get; set; } = string.Empty;
        public string Src { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public List<string> Stems { get; set; } = default!;
        public bool Offensive { get; set; }
    }
}
