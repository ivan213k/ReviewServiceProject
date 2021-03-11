using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class EvaluationPointItemConfiguration : IEntityTypeConfiguration<EvaluationPointItem>
    {
        public void Configure(EntityTypeBuilder<EvaluationPointItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
