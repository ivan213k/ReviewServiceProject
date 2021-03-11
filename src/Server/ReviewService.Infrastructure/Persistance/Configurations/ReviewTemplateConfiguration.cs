using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Domain.Entites;
using System;

namespace ReviewService.Infrastructure.Persistance.Configurations
{
    class ReviewTemplateConfiguration : IEntityTypeConfiguration<ReviewTemplate>
    {
        public void Configure(EntityTypeBuilder<ReviewTemplate> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(r => r.Areas).WithMany(a => a.ReviewTemplates);
        }
    }
}
