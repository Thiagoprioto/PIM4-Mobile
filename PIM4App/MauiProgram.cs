// Todos os 'usings' devem estar no topo, um por linha.
using CommunityToolkit.Maui;
using PIM4App.Services;
using PIM4App.ViewModels;
using PIM4App.Views;

namespace PIM4App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Esta linha está correta
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // =================================================================
        // REGISTRO DE TODOS OS NOSSOS ARQUIVOS
        // (Esta parte já estava correta no seu código)
        // =================================================================

        // --- Serviços (Singleton = 1 por app) ---
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IChamadoService, ChamadoService>();
        builder.Services.AddSingleton<IFaqService, FaqService>();

        // --- ViewModels (Transient = 1 novo a cada vez) ---
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<ChamadosViewModel>();
        builder.Services.AddTransient<NovoChamadoViewModel>();
        builder.Services.AddTransient<FaqViewModel>();
        builder.Services.AddTransient<DetalheChamadoViewModel>();
        builder.Services.AddTransient<PerfilViewModel>();

        // --- Views (Transient = 1 nova a cada vez) ---
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ChamadosPage>();
        builder.Services.AddTransient<NovoChamadoPage>();
        builder.Services.AddTransient<FaqPage>();
        builder.Services.AddTransient<DetalheChamadoPage>();
        builder.Services.AddTransient<PerfilPage>();

        return builder.Build();
    }
}