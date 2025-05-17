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
                .SetBasePath(Directory.GetCurrentDirectory()) // Путь к exe при запуске
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string ConnectionString =>
            Configuration.GetConnectionString("DefaultConnection");
    }
}
