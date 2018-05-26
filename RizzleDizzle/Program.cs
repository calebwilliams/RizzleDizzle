using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDBService.Services;
using RizzleDizzle.Config.Settings;
using RizzleDizzle.Interfaces;
using RizzleDizzle.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RizzleDizzle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            await provider.GetService<App>().Run();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            services.AddLogging();

            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/Config/")
                .AddJsonFile("app-settings.json", false)
                .Build();
            services.AddOptions();

            services.Configure<RuneScapeSettings>(configuration.GetSection("RuneScapeSettings"));
            services.Configure<MongoDBService.Config.MongoDBSettings>(configuration.GetSection("MongoDBSettings"));

            services.AddTransient<MongoDBService.Interfaces.IMongoDBService, MongoDBService.Services.MongoDBService>();
            services.AddTransient<IRuneScapeKeyboardService, RuneScapeKeyboardService>();
            services.AddTransient<IRuneScapeStoryTimeService, RuneScapeStoryTimeService>();
            services.AddTransient<App>();
        }
    }

}
