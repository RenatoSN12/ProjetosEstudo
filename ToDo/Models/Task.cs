namespace ToDo.Models;

public class Task
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
    
    public int Priority { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public string AssignedTo { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public List<string> Tags { get; set; }
    
    public DateTime LastModificationDate { get; set; }
    
    public DateTime CompletionDate { get; set; }
    
}