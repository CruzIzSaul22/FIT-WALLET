using FITFINAL.Services;
using FITFINAL.ViewModels;
using FITFINAL.Views;

namespace FITFINAL
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ❌ NO registrar DataService (es Singleton manual)
            // ❌ NO registrar ViewModels
            // ❌ NO registrar Pages

            return builder.Build();
        }
    }
}