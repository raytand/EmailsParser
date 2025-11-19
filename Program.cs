using Microsoft.Extensions.DependencyInjection;
using EmailsParser.Interfaces;

namespace EmailsParser
{
    internal class Program
    { 
        static async Task Main()
        {
            var services = new ServiceCollection();
            services.AddTransient<IWritingService, WritingService>();
            services.AddTransient<IParsingService, ParsingService>();
            services.AddTransient<IInputService,InputService>();
            services.AddTransient<Controller>();

            var serviceProvider = services.BuildServiceProvider();
            var controller = serviceProvider.GetService<Controller>();

            await controller.ControlAsync();
        }
    }
}