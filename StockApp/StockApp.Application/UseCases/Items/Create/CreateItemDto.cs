namespace StockApp.Application.UseCases.Items.Create;

public class CreateItemDto
{
    public string CustomId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
}