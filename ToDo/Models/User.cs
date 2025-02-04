namespace ToDo.Models;

public class User
{
    public int Id { get; set; } 
    public string Name { get; set; } 

    // Task Relationship
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}