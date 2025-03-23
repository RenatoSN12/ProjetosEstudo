using Stock.Domain.Models;
using Stock.Domain.Requests;
using Stock.Domain.Responses;

namespace Stock.Domain.Handlers;

public interface IItemHandler
{
    Task<Response<Item?>> CreateAsync(CreateItemRequest request);

}