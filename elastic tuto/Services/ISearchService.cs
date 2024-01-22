using ElasticDotnet.Domain.Models;
using Nest;

namespace elastic_tuto.Services;

public interface ISearchService
{
    public Task<ISearchResponse<Product>> SearchAsync(string query);
}
