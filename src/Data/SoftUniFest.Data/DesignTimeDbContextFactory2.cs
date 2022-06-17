namespace SoftUniFest.Data
{
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeDbContextFactory2 : IDesignTimeDbContextFactory<ApplicationDbContext2>
    {
        public ApplicationDbContext2 CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext2>();
            var connectionString = configuration.GetConnectionString("SecondConnection");
            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext2(builder.Options);
        }
    }
}
