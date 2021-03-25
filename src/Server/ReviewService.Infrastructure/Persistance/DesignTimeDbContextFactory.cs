using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ReviewService.Infrastructure.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReviewServiceDbContext>
    {
        public ReviewServiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReviewServiceDbContext>();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("dbsettings.json");
            var config = builder.Build();
            
            string connectionString = config.GetConnectionString("LocalConnection");
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new ReviewServiceDbContext(optionsBuilder.Options);
        }
    }
}
