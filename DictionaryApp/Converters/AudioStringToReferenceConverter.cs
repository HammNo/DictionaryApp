using System.Globalization;

namespace DictionaryApp.Converters
{
    public class AudioStringToReferenceConverter : IValueConverter
    {
        const string BaseURL = "https://media.merriam-webster.com/audio/prons/en/us/mp3";
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null && value is string audioString)
                return $"{BaseURL}/{audioString[0]}/{value}.mp3";

            return null;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
