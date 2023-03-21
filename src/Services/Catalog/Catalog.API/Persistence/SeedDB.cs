using Catalog.API.Models;

namespace Catalog.API.Persistence;

using Microsoft.EntityFrameworkCore;

public class SeedDB
{
    private readonly CatalogContext _context;

    public SeedDB(CatalogContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (await _context.CatalogItems.AnyAsync())
        {
            return;   // DB has been seeded already
        }

        var catalogItemTypes = new[]
        {
            new CatalogItemType { Name = "Type 1" },
            new CatalogItemType { Name = "Type 2" },
            new CatalogItemType { Name = "Type 3" },
        };

        _context.CatalogItemTypes.AddRange(catalogItemTypes);
        await _context.SaveChangesAsync();

        var catalogItemBrands = new[]
        {
            new CatalogItemBrand { Name = "Brand 1" },
            new CatalogItemBrand { Name = "Brand 2" },
            new CatalogItemBrand { Name = "Brand 3" },
        };

        _context.CatalogItemBrands.AddRange(catalogItemBrands);
        await _context.SaveChangesAsync();

        var catalogItems = new[]
        {
            new CatalogItem
            {
                Name = "Item 1",
                Description = "Description 1",
                Price = 100.00m,
                PictureFileName = "1.jpg",
                CatalogItemTypeId = catalogItemTypes[0].Id,
                CatalogItemBrandId = catalogItemBrands[0].Id,
            },
            new CatalogItem
            {
                Name = "Item 2",
                Description = "Description 2",
                Price = 200.00m,
                PictureFileName = "2.jpg",
                CatalogItemTypeId = catalogItemTypes[1].Id,
                CatalogItemBrandId = catalogItemBrands[1].Id,
            },
            new CatalogItem
            {
                Name = "Item 3",
                Description = "Description 3",
                Price = 300.00m,
                PictureFileName = "3.jpg",
                CatalogItemTypeId = catalogItemTypes[2].Id,
                CatalogItemBrandId = catalogItemBrands[2].Id,
            },
        };

        _context.CatalogItems.AddRange(catalogItems);
        await _context.SaveChangesAsync();
    }
}
