using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReviewService.Domain.Entites;
using ReviewService.Infrastructure.Persistance.Configurations;
using System.IO;

namespace ReviewService.Infrastructure.Persistance
{
    public class ReviewServiceDbContext : DbContext
    {
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaItem> AreaItems { get; set; }
        public virtual DbSet<EvaluationPoint> EvaluationPoints { get; set; }
        public virtual DbSet<ImportanceLevel> ImportanceLevels { get; set; }
        public ReviewServiceDbContext()
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new AreaItemConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationPointConfiguration());
            modelBuilder.ApplyConfiguration(new ImportanceLevelConfiguration());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("dbsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("LocalConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
