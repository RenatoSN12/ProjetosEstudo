using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Category title is required")]
    [MinLength(3, ErrorMessage = "Category title must be at least 3 characters")]
    [MaxLength(60, ErrorMessage = "Category title must be less than 60 characters")]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}