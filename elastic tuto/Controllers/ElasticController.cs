using ElasticDotnet.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public SearchController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        //var response = await _elasticClient.SearchAsync<Product>(s => s
        //   .Query(q => q.QueryString(d => d.Query(query)))
        //);
        var response = await _elasticClient.PingAsync();
        return Ok(response.IsValid);
    }
}

