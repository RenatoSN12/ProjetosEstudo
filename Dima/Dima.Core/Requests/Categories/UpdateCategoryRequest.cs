using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    [Required(ErrorMessage = "O id é obrigatório.")]
    public long Id { get; set; }
    [Required(ErrorMessage = "Titulo é obrigatório.")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;
}