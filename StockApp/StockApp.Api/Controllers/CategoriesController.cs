using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.UseCases.Categories.Queries;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userEmail = HttpContext.GetUserEmail();
        var query = new GetAllCategoriesQuery(userEmail, pageNumber, pageSize);
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
    
    // [HttpGet]
    // public async Task<IActionResult> GetById(int id)
    // {
    //     var userEmail = HttpContext.GetUserEmail();
    //     var query = new GetAllCategoriesQuery(userEmail);
    //     var result = await sender.Send(query);
    //
    //     return result.IsSuccess
    //         ? Ok(result)
    //         : BadRequest(result);
    // }
}