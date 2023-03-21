using System.Net;
using AutoMapper;
using Catalog.API.Models;
using Catalog.API.Persistence;
using Catalog.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController:ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly CatalogContext _catalogContext;
    private readonly IMapper _mapper;

    public CatalogController(ILogger<CatalogController> logger,CatalogContext catalogContext,IMapper mapper)
    {
        _logger = logger;
        _catalogContext = catalogContext;
        _mapper = mapper;
    }
    [HttpGet]
    [Route("items")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PaginatedItems<CatalogItem>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsAsync([FromServices] IWebHostEnvironment env,[FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

        var itemsOnPage = await _catalogContext.CatalogItems
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToArrayAsync();

        foreach (var item in itemsOnPage)
        {
            item.PictureUri = $"http://localhost:5178/images/{item.PictureFileName}";
        }

        var viewModel = new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        return Ok(viewModel);
    }

    [HttpGet]
    [Route("item/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return null;
    }
    
    [HttpPost]
    [Route("items")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateItemAsync([FromBody]ItemViewModel item)
    {
        var product = _mapper.Map<CatalogItem>(item);

        await _catalogContext.CatalogItems.AddAsync(product);
        await _catalogContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, null);
    }
}