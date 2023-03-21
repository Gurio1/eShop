namespace Catalog.API.ViewModels;

public class ItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = null!;

    public int CatalogItemTypeId { get; set; }

    public int CatalogItemBrandId { get; set; }
}