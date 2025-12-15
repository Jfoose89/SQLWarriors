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
            //db.Database.EnsureCreated();

            Console.WriteLine("Database created successfully.");

            // Optional: Test querying some data
            var students = db.Students.ToList();
            Console.WriteLine($"Number of students: {students.Count}");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName} - {s.Email}");
            }

            //var MainMenu = new MainMenu();
            //MainMenu.db = db;

            //MainMenu.Run();
        }
    }
}
