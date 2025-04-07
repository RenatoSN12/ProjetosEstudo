using System.Net.Http.Json;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Services.Implementations;

public class CategoryService(IHttpClientFactory httpClientFactory) : ICategoryService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<PagedResult<List<CategoryDto>?>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<PagedResult<List<CategoryDto>?>>("/api/categories", cancellationToken);
        
        if (result is null)
            return PagedResult<List<CategoryDto>?>.Failure(new Error("400", "Ocorreu um erro inesperado ao obter as categorias."));
            
        return result.IsFailure 
            ? PagedResult<List<CategoryDto>?>.Failure(result.Error) 
            : result;
    }
}