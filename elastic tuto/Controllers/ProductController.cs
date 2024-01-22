using elastic_tuto.Services;
using ElasticDotnet.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISearchService _searchService;

    public ProductsController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts([FromQuery(Name = "q")] string searchQuery)
    {
        var response = await _searchService.SearchAsync(searchQuery);

        if (response.IsValid)
        {
            return Ok(response.Documents);
        }
        else { return BadRequest(); }
    }


}
