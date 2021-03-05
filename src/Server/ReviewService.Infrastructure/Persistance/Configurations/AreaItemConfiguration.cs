using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class AreaItemConfiguration : IEntityTypeConfiguration<AreaItem>
    {
        public void Configure(EntityTypeBuilder<AreaItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
