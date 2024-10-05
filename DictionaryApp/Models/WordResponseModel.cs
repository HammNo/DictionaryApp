using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace DictionaryApp.Models
{
    public partial class WordResponseModel : ObservableObject
    {
        public Meta Meta { get; set; } = default!;

        public List<string> ShortDef { get; set; } = default!;

        public HeadwordInformation Hwi { get; set; } = default!;

        [ObservableProperty]
        public string fl = string.Empty;

        public WordResponseModel() {}

        public WordResponseModel(Meta meta, List<string> shortDef, HeadwordInformation hwi)
        {
            Meta = meta;
            ShortDef = shortDef;
            Hwi = hwi;
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

    public class HeadwordInformation
    {
        public string Hw { get; set; } = string.Empty;
        public List<Pronunciation> Prs { get; set; } = default!;

    }

    public class Pronunciation
    {
        public string Mw { get; set; } = string.Empty;
        public Sound? Sound { get; set; } = default!;    
    }

    public class Sound
    {
        public string Audio { get; set; } = string.Empty;
        public string Ref { get; set; } = string.Empty;
        public string Stat { get; set; } = string.Empty;
    }
}
