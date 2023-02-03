using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.Logging;

namespace MediaElementTest;

public class MyApp : Application
{
    public MyApp()
    {
        MainPage = new ContentPage
        {
            Content = new MediaElement
            {
                Source = MediaSource.FromUri("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"),
                HeightRequest = 300,
                WidthRequest = 400,
                ShouldAutoPlay = true
            }
        };
    }
}

public class MyApp_Workaround : Application
{
    public MyApp_Workaround()
    {
        // A workaround is to delay the creation of the MediaElement until the page is loaded.
        
        var contentPage = new ContentPage();
        contentPage.Loaded += (sender, args) =>
            contentPage.Content = new MediaElement
            {
                Source = MediaSource.FromUri(
                    "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"),
                HeightRequest = 300,
                WidthRequest = 400,
                ShouldAutoPlay = true
            };
        
        MainPage = contentPage;
    }
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<MyApp>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}