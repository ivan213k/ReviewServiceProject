using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewService.Domain.Entites;
using ReviewService.Infrastructure.Identity;
using ReviewService.Infrastructure.Persistance.Configurations;

namespace ReviewService.Infrastructure.Persistance
{
    public class ReviewServiceDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ReviewSession> ReviewSessions { get; set; }
        public virtual DbSet<ReviewEvaluation> ReviewEvaluations { get; set; }
        public virtual DbSet<ReviewTemplate> ReviewTemplates { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaItem> AreaItems { get; set; }
        public virtual DbSet<EvaluationPointsTemplate> EvaluationPointsTemplates { get; set; }
        public virtual DbSet<EvaluationPoint> EvaluationPoints { get; set; }
        public virtual DbSet<ImportanceLevel> ImportanceLevels { get; set; }
        public ReviewServiceDbContext(DbContextOptions<ReviewServiceDbContext> options)
            : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new AreaItemConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationPointConfiguration());
            modelBuilder.ApplyConfiguration(new ImportanceLevelConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationPointsTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEvaluationConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewSessionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
