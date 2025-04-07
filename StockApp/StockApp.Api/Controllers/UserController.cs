using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.UseCases.Authentication.GetUserInfo;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var query = new GetUserInfoQuery();
        var result = await sender.Send(query);
            
        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);
            
        return Ok(result);
    }
}