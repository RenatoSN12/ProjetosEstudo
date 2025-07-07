using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context, [FromServices] MemoryCache cache)
        {
            try
            {
                var categories = cache.GetOrCreate("CategoriesCache",x=> {
                    x.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return context.Categories.
                    AsNoTracking().
                    Include(x=>x.Posts).
                    ToList();
                });
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500,new ResultViewModel<List<Category>>("05EX04 - Falha interna no servidor."));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor."));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Posts = [],
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("Não foi possível adicionar a categoria."));
            } 
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor."));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model, [FromRoute] int id)
        {
            try
            {
                var categoryFromDb = await context.Categories.FirstOrDefaultAsync(context => context.Id == id);

                if (categoryFromDb == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));

                categoryFromDb.Name = model.Name;
                categoryFromDb.Slug = model.Slug;

                context.Categories.Update(categoryFromDb);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Category>(categoryFromDb));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("Não foi possível alterar a categoria."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor."));
            }

        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext context,[FromRoute] int id)
        {
            try
            {
                var categoryFromDb = await context.Categories.FirstOrDefaultAsync(context => context.Id == id);

                if (categoryFromDb == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada."));

                context.Categories.Remove(categoryFromDb);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Category>(categoryFromDb));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("Não foi possível excluir a categoria."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor."));
            }
        }

    }
}
