namespace ToDo.Models;
public class Task
{
    public enum Priority
    {
        Low = 1,
        Medium = 2, 
        High = 3
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public Priority TaskPriority { get; set; }
    public bool Done { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public DateTime CompletionDate { get; set; }
    
    // Attachment Relationship
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    
    // User Relationship
    public int AssignedUserId { get; set; }
    public User AssignedUser { get; set; }
    
    // Tag Relationship
    public ICollection<Tag> Tags { get; set; }  = new List<Tag>();

}