using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    public readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Task>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();
        
        return task;
    }

    [HttpPost]
    public async Task<ActionResult<Task>> PostTask(Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTask(int id, Task task)
    {
        if (id != task.Id)
            return BadRequest();
        
        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == id))
             return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Task>> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();
        
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}