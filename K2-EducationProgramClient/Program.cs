using K2_EducationProgramClient.Data;
using K2_EducationProgramClient.Models;
using K2_EducationProgramClient.Models.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace K2_EducationProgramClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var appRoot = Directory.GetParent(exePath).Parent.Parent.FullName;
            Console.WriteLine(appRoot);

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appRoot)  // AppContext.BaseDirectory points to the runtime folder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup DI container
            var services = new ServiceCollection();

            services.AddDbContext<EducationProgramClientDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationProgramDB")));

            var provider = services.BuildServiceProvider();

            // Resolve DbContext
            using var db = provider.GetRequiredService<EducationProgramClientDbContext>();

            // Ensure DB is created
            db.Database.EnsureCreated();

            //var MainMenu = new MainMenu();
            //MainMenu.db = db;

            //MainMenu.Run();
        }
    }
}
