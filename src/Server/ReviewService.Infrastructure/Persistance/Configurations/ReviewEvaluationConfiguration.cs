using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class ReviewEvaluationConfiguration : IEntityTypeConfiguration<ReviewEvaluation>
    {
        public void Configure(EntityTypeBuilder<ReviewEvaluation> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Reviewer).IsRequired();
        }
    }
}
