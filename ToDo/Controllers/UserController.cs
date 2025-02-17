using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public readonly AppDbContext _context;
    
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(PostUser), new { id = user.Id }, user);
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUsers()
    {
        return Ok(await _context.Users.ToListAsync());
    }
    
}