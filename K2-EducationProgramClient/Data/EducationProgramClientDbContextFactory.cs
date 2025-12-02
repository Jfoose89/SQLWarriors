using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace K2_EducationProgramClient.Data
{
    public class EducationProgramClientDbContextFactory
        : IDesignTimeDbContextFactory<EducationProgramClientDbContext>
    {
        public EducationProgramClientDbContext CreateDbContext(string[] args)
        {
            // Load configuration (just like Program.cs)
            var basePath = AppContext.BaseDirectory;
            basePath = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<EducationProgramClientDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EducationProgramClientDbContext(optionsBuilder.Options);
        }
    }
}
