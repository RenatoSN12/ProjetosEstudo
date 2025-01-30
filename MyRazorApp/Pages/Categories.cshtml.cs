using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorApp.Pages;

public class Categories : PageModel
{
    public List<Category> CategoryList { get; set; } = new();
    
    public void OnGet(int skip = 0, int take = 25)
    {
        var tempList = new List<Category>();
        
        for (var i = 0; i < 100; i++)
            tempList.Add(new Category(i, "Category " + i, i*100M));
        
        CategoryList = tempList
            .Skip(skip)
            .Take(take)
            .ToList();
    }
    
    public record Category(
        int Id,
        string Title,
        decimal Price
    );
}