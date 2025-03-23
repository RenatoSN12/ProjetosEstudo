using System.ComponentModel.DataAnnotations;
using Stock.Domain.Models;

namespace Stock.Domain.Requests;

public class CreateItemRequest(string customId) : Request
{
    [Required(ErrorMessage = "Informe um código customizado para o item.")]
    public string CustomId { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe um título para o item.")]
    [MaxLength(20,ErrorMessage = "O título do item deve possuir no máximo 20 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A descrição do item deve possuir no máximo 500 caracteres.")]
    public string Description { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public long CategoryId { get; set; }
}