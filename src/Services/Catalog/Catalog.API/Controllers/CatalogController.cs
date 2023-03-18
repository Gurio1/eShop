using System.Net;
using Catalog.API.Models;
using Catalog.API.Persistence;
using Catalog.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CatalogController:ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly CatalogContext _catalogContext;

    public CatalogController(ILogger<CatalogController> logger,CatalogContext catalogContext)
    {
        _logger = logger;
        _catalogContext = catalogContext;
    }
    [HttpGet]
    [Route("items")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PaginatedItems<CatalogItem>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

        var itemsOnPage = await _catalogContext.CatalogItems
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToArrayAsync();

        //Need to decide what to do with ImageUrl
        //Pic controller(working not correct) is one of the solutions,but i need time
        
        var viewModel = new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        return Ok(viewModel);
    }
}