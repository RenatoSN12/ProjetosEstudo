using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;
using Task = System.Threading.Tasks.Task;

namespace ToDo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    public readonly AppDbContext _context; 
    public TagController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        return Ok(await _context.Tags.ToListAsync());
    }
    
    [HttpPost]
    public async Task<ActionResult<Tag>> PostTag(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(PostTag), new { id = tag.Id }, tag);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Tag>> DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            return NotFound();
        
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
}