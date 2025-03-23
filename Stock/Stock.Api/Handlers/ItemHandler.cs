using Stock.Api.Data;
using Stock.Domain.Handlers;
using Stock.Domain.Models;
using Stock.Domain.Requests;
using Stock.Domain.Responses;

namespace Stock.Api.Handlers;

public class ItemHandler(AppDbContext context) : IItemHandler
{
    public async Task<Response<Item?>> CreateAsync(CreateItemRequest request)
    {
        try
        {
            var item = new Item
            {
                Title = request.Title,
                CustomId = request.CustomId,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return new Response<Item?>(item, 201, "Item criado com sucesso!" );
        }
        catch
        {
            return new Response<Item?>(null, 500, "Ocorreu um erro ao criar o item.");
        }
    }
}