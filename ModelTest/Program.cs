using Microsoft.EntityFrameworkCore;
using K2_EducationProgramClient.Data;   // your DbContext namespace

class Program
{
    static void Main()
    {
        Console.WriteLine("Testing EF model...");

        var options = new DbContextOptionsBuilder<EducationProgramClientDbContext>()
            .UseInMemoryDatabase("TestDb")
            .EnableSensitiveDataLogging()
            .Options;

        try
        {
            using var db = new EducationProgramClientDbContext(options);

            // This line executes OnModelCreating and applies seed data.
            db.Database.EnsureCreated();

            Console.WriteLine("✔ EF model created successfully. No errors.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ EF model FAILED:");
            Console.WriteLine(ex.ToString());
        }
    }
}
