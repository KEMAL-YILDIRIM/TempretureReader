using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using ConsoleApp.Helpers;
using ConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp.Configuration
{
    public static class AutofacSetup
    {
        internal static IServiceProvider RegisterServices(IServiceProvider serviceProvider)
        {
            var collection = new ServiceCollection();
            var builder = new ContainerBuilder();

            builder.RegisterType<AmbientTemperatureProcessor>().As<IAmbientTemperatureProcessor>();
            builder.RegisterType<TyreTemperatureProcessor>().As<ITyreTemperatureProcessor>();
            builder.RegisterType<FileReader>().As<IFileReader>();
            builder.RegisterType<CalculateTempratures>().As<ICalculateTempratures>();

            builder.Populate(collection);

            var appContainer = builder.Build();

            serviceProvider = new AutofacServiceProvider(appContainer);
            return serviceProvider;
        }

        internal static void DisposeServices(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
