namespace StockApp.Domain.DTOs.Requests.Categories;

public sealed class CreateCategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
