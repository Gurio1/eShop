using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Persistence.EntityConfigurations;

public class CatalogItemBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItemBrand>
{
    public void Configure(EntityTypeBuilder<CatalogItemBrand> builder)
    {
        builder.ToTable("ItemBrands");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}