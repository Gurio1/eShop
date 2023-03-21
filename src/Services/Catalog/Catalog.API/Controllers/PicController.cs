using System.Net;
using System.Net.Mime;
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
    
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(MediaTypeNames.Image),(int)HttpStatusCode.OK)]
    [HttpGet("images/{filename}")]
    public IActionResult GetImage(string filename,[FromServices] IWebHostEnvironment env)
    {
        var imagePath = Path.Combine(env.ContentRootPath, "Pics", filename);

        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound();
        }

        var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        
        string imageFileExtension = Path.GetExtension(imagePath);

        return new FileStreamResult(fileStream, GetImageMimeTypeFromImageFileExtension(imageFileExtension));
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