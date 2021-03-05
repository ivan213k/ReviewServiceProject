using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class EvaluationPointConfiguration : IEntityTypeConfiguration<EvaluationPoint>
    {
        public void Configure(EntityTypeBuilder<EvaluationPoint> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
