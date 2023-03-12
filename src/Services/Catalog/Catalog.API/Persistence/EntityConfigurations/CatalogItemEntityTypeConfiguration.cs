using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Persistence.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Price)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(150);


        builder.Property(c => c.PictureFileName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Ignore(c => c.PictureUri);

        builder.HasOne(c => c.CatalogItemType)
            .WithMany()
            .HasForeignKey(c => c.CatalogItemTypeId);

        builder.HasOne(c => c.CatalogItemBrand)
            .WithMany()
            .HasForeignKey(c => c.CatalogItemBrandId);


    }
}