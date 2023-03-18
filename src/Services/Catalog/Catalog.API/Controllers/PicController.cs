using System.Net;
using Catalog.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[ApiController]
public class PicController: ControllerBase
{
    private readonly CatalogContext _catalogContext;

    public PicController(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }
    
    [HttpGet]
    [Route("Catalog/items/{catalogItemId:int}/pic")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPicture(int catalogItemId)
    {
        if (catalogItemId <= 0)
        {
            return BadRequest();
        }

        var item = await _catalogContext.CatalogItems
            .FirstOrDefaultAsync(ci => ci.Id == catalogItemId);

        if (item!= null)
        {
            string filePath = Path.Combine("pics", item.PictureFileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string imageFileExtension = Path.GetExtension(item.PictureFileName);
            string mimeType = GetImageMimeTypeFromImageFileExtension(imageFileExtension);
                
            return File(fileStream, mimeType);
        }

        return NotFound();
    }
    
    
    private string GetImageMimeTypeFromImageFileExtension(string extension)
    {
        string mimetype = extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream",
        };
        return mimetype;
    }
}