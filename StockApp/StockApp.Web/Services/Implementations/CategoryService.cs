using System.Net.Http.Json;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;
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
            return PagedResult<List<CategoryDto>?>.Failure(new Error("500", "Ocorreu um erro inesperado ao obter as categorias."));
            
        return result.IsFailure 
            ? PagedResult<List<CategoryDto>?>.Failure(result.Error) 
            : result;
    }

    public async Task<Result<CategoryDto?>> CreateCategoryAsync(CreateCategoryDto request, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/categories", request, cancellationToken);
        
         var readResult = await result.Content.ReadFromJsonAsync<Result<CategoryDto?>>(cancellationToken: cancellationToken);

         if (readResult is null)
            return Result.Failure<CategoryDto?>(new Error("400", "Ocorreu um erro inesperado ao criar a nova categoria."));
        
        return readResult.IsFailure 
            ? Result.Failure<CategoryDto?>(readResult.Error) 
            : readResult;
    }
}