using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Persistence.EntityConfigurations;

public class CatalogItemTypeEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItemType>
{
    public void Configure(EntityTypeBuilder<CatalogItemType> builder)
    {
        builder.ToTable("ItemTypes");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}