using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Unity.Microsoft.DependencyInjection;

namespace Deviant.KleinHotelAmersfoort.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUnityServiceProvider()
                .UseStartup<Startup>();
    }
}
