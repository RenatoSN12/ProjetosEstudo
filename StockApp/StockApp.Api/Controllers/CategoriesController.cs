using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.UseCases.Categories.Commands;
using StockApp.Application.UseCases.Categories.Queries;
using StockApp.Domain.DTOs.Requests.Categories;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userId = HttpContext.GetUserEmail();
        var query = new GetAllCategoriesQuery(userId, pageNumber, pageSize);
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new CreateCategoryCommand(userId, request.Title, request.Description);
        
        var result = await sender.Send(command);
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}