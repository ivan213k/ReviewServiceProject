using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class EvaluationPointsTemplateConfiguration : IEntityTypeConfiguration<EvaluationPointsTemplate>
    {
        public void Configure(EntityTypeBuilder<EvaluationPointsTemplate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(e => e.ReviewTemplates);
        }
    }
}
