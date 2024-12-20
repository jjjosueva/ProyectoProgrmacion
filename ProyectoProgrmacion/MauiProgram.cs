﻿using Microsoft.Extensions.DependencyInjection;
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

            // Registrar servicios
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PiezaService>();
            builder.Services.AddSingleton<PagoService>();
            builder.Services.AddSingleton<PedidoService>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<Home>();




            // Registrar ViewModels
            builder.Services.AddTransient<DetallesPedidosViewModel>();
            builder.Services.AddTransient<EncabezadoPedidosViewModel>();
            builder.Services.AddTransient<PiezasViewModel>();
            builder.Services.AddTransient<SistemaPagoViewModel>();
            builder.Services.AddTransient<HomeViewModel>();

            // Registrar páginas
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DetallesPedidosPage>();
            builder.Services.AddTransient<EncabezadoPedidosPage>();
            builder.Services.AddTransient<PiezasPage>();
            builder.Services.AddTransient<SistemaPagoPage>();
            builder.Services.AddTransient<Home>();

            return builder.Build();
        }
    }
}
