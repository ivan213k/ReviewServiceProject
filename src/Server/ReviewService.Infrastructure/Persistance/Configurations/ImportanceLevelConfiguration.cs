using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class ImportanceLevelConfiguration : IEntityTypeConfiguration<ImportanceLevel>
    {
        public void Configure(EntityTypeBuilder<ImportanceLevel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
