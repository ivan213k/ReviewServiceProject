using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class ReviewSessionConfiguration : IEntityTypeConfiguration<ReviewSession>
    {
        public void Configure(EntityTypeBuilder<ReviewSession> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.PersonUnderReview).IsRequired();
            builder.Property(p => p.ReviewMaster).IsRequired();

            builder.HasMany(p => p.ReviewEvaluations);
        }
    }
}
