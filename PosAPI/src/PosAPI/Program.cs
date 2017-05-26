using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("hosting.json", optional: false)
            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()                
                .UseSetting("detailedErrors", "true")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .Build();

            host.Run();
        }
    }
}
