using ConsoleApp.Configuration;
using ConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            _serviceProvider = AutofacSetup.RegisterServices(_serviceProvider);

            var calculateTempratures = _serviceProvider.GetService<ICalculateTempratures>();

            try
            {
                var result = calculateTempratures.Execute();
            }
            catch (Exception e)
            {
                // todo: It should be written in a log file and keep continue to program based on exception type.
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine("Please restart the program.");
            }

            Console.ReadKey();
            AutofacSetup.DisposeServices(_serviceProvider);
        }
    }
}
