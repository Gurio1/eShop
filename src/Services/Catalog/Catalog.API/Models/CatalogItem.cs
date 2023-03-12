namespace Catalog.API.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = null!;
    public string PictureUri { get; set; } = null!;

    public int CatalogItemTypeId { get; set; }
    public CatalogItemType CatalogItemType { get; set; } = null!;

    public int CatalogItemBrandId { get; set; }
    public CatalogItemBrand CatalogItemBrand { get; set; } = null!;
    
}