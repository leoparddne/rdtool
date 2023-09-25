using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RDTool.Server.WebAPI.Helper;

namespace RDTool.Server.WebAPI
{
    public class Program
    {
        private static string ip;
        private static string port;

        public static void Main(string[] args)
        {
            ip = AppSettingsHelper.GetSetting("Publish", "ApplicationIP");
            port = AppSettingsHelper.GetSetting("Publish", "ApplicationPort");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                   .UseUrls($"http://{ip}:{port}")
                   .UseStartup<Startup>()
                   ;
                })
                .UseWindowsService();
    }
}
