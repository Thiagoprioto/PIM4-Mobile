// Todos os 'usings' devem estar no topo, um por linha.
using CommunityToolkit.Maui;
using PIM4App.Services;
using PIM4App.ViewModels;
using PIM4App.Views;
using System.Net.Http;
using Microsoft.Extensions.Http;

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


        //conexão com o n8n
        builder.Services.AddHttpClient();

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
        builder.Services.AddTransient<TecnicoDashboardViewModel>();
        builder.Services.AddTransient<DetalheTecnicoViewModel>();

        // --- Views (Transient = 1 nova a cada vez) ---
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ChamadosPage>();
        builder.Services.AddTransient<NovoChamadoPage>();
        builder.Services.AddTransient<FaqPage>();
        builder.Services.AddTransient<DetalheChamadoPage>();
        builder.Services.AddTransient<PerfilPage>();
        builder.Services.AddTransient<TecnicoDashboardPage>();
        builder.Services.AddTransient<DetalheTecnicoPage>();

        return builder.Build();
    }
}