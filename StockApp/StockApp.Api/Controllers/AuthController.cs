using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.UseCases.Authentication.GetUserInfo;
using StockApp.Application.UseCases.Authentication.Login;
using StockApp.Application.UseCases.Authentication.Logout;
using StockApp.Application.UseCases.Authentication.Register;
using StockApp.Domain.DTOs.Responses;

namespace StockApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController(ISender sender) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await sender.Send(command);

            if (!result.IsSuccess || result.Value is null)
                return Unauthorized(result.Error);

            await SignInUserAsync(result.Value);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await sender.Send(command);
            
            if (!result.IsSuccess || result.Value is null)
                return BadRequest(result.Error);
            
            await SignInUserAsync(result.Value);
            
            return Ok(result);
        }
        
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var command = new LogoutCommand(); // ou receba do corpo, se necessário
            await sender.Send(command);
            await HttpContext.SignOutAsync("Cookies");
            return NoContent();
        }
        
        private async Task SignInUserAsync(UserDto userDto)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, $"{userDto.Firstname} {userDto.Lastname}"),
                new(ClaimTypes.Email, userDto.Email)
            };

            var identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
            {
                IsPersistent = true
            });
        }

    }
}