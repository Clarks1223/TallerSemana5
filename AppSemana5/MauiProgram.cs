using AppSemana5.Repositories;
using AppSemana5.Utils;
using AppSemana5.Views;
using Microsoft.Extensions.Logging;

namespace AppSemana5
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

            string dbPath = FileAccessHelper.GetLocalFilePath("uisrael.db");
            builder.Services.AddSingleton<EstudianteRepository>(
                s => ActivatorUtilities.CreateInstance<EstudianteRepository>(s, dbPath));
            builder.Services.AddTransient<vEstudiante>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
