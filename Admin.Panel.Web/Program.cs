using System.IO;
using Admin.Panel.Web.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Admin.Panel.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var mt =
                "{Timestamp: HH:mm:ss} [{SourceContext}] [{Level:w3}] {UserName} - {Message: lj}  {Exception} {NewLine} ";

            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Information()
                        .Enrich.With(services.GetRequiredService<UserNameEnricher>())
                        .WriteTo.RollingFile($"{path}\\Logs\\Log.txt", outputTemplate: mt))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}