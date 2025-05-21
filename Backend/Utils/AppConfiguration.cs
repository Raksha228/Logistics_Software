using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Backend.Utils
{
    public static class AppConfiguration
    {
        public static IConfigurationRoot Configuration { get; }
        static AppConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        // URL вашего API вместо строки подключения
        public static string ApiBaseUrl =>
            Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000";
    }
}