using System.ComponentModel.DataAnnotations;

namespace BlazingShop.Models;

public class Product
{
    [Key]
    [Required(ErrorMessage="Id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage="Title is required")]
    [MaxLength(150, ErrorMessage = "Title length cannot be more than 150 characters")]
    [MinLength(5, ErrorMessage = "Title length cannot be less than 5 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage="Price is required")]
    [DataType(DataType.Currency)]
    [Range(1,9999, ErrorMessage = "Price must be between 1 and 9999")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage="Category is required")]
    [Range(1, 9999, ErrorMessage = "Category must be between 1 and 9999")]
    public int CategoryId { get; set; }
    public Category Cate { get; set; } = null!;
}