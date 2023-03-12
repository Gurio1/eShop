using System.Reflection;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Persistence;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CatalogItem> CatalogItems { get; set; } = null!;
    public DbSet<CatalogItemBrand> CatalogItemBrands { get; set; } = null!;
    public DbSet<CatalogItemType> CatalogItemTypes { get; set; } = null!;
}