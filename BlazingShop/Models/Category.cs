using System.ComponentModel.DataAnnotations;

namespace BlazingShop.Models;

public class Category
{
    [Key]
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be no more than 50 characters")]
    public string Title { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}