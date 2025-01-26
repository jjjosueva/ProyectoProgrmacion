using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProyectoProgrmacion.Services;
using ProyectoProgrmacion.Servicios;
using ProyectoProgrmacion.ViewModels;
using ProyectoProgrmacion.Views;

namespace ProyectoProgrmacion
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //  Registrar Servicios
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PiezaService>();  //  Nuevo servicio para piezas
            builder.Services.AddSingleton<PagoService>();
            builder.Services.AddSingleton<PedidoService>();

            //  Registrar ViewModels
            builder.Services.AddTransient<DetallesPedidosViewModel>();
            builder.Services.AddTransient<EncabezadoPedidosViewModel>();
            builder.Services.AddTransient<PiezasViewModel>();
            builder.Services.AddTransient<SistemaPagoViewModel>();
            builder.Services.AddSingleton<HomeViewModel>(); // Solo una vez como Singleton si es compartido

            // Registrar Páginas
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginPage>(); //  Se mantuvo como Singleton para rendimiento
            builder.Services.AddTransient<DetallesPedidosPage>();
            builder.Services.AddTransient<EncabezadoPedidosPage>();
            builder.Services.AddTransient<PiezasPage>();
            builder.Services.AddTransient<SistemaPagoPage>();
            builder.Services.AddSingleton<Home>(); //  Como Singleton porque puede ser página principal

            return builder.Build();
        }
    }
}
