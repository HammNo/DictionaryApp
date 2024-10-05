using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using DictionaryApp.Services;
using DictionaryApp.Pages;
using DictionaryApp.ViewModels;

namespace DictionaryApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddSingleton<DictionaryService>();
            builder.Services.AddSingleton<JsonFileService>();
            builder.Services.AddSingleton<WordResponsesViewModel>();
            builder.Services.AddSingleton<ConfigurationViewModel>();

            return builder.Build();
        }
    }
}
