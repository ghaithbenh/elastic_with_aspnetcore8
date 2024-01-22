using ElasticDotnet.Domain.Models;
using Nest;

namespace elastic_tuto.Services;

public class SearchService : ISearchService
{
    private readonly IElasticClient _elasticClient;

    public SearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<ISearchResponse<Product>> SearchAsync(string query)
    {
        var searchDescriptor = new SearchDescriptor<Product>()
            .Index("french_products")
            .Size(10);

        if (string.IsNullOrEmpty(query))
        {
            searchDescriptor = searchDescriptor.Query(q => q.MatchAll());
        }
        else
        {
            // Adjust this query according to your needs, for example, using QueryString or Match
            searchDescriptor = searchDescriptor.Query(q => q.QueryString(qs => qs.Query(query)));
        }

        var searchResponse = await _elasticClient.SearchAsync<Product>(searchDescriptor);

        return searchResponse;
    }
}
