namespace  ToDo.Models
{
public class Attachment
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; } 
    public string FileType { get; set; } 
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }
    
    // Task Relationship
    
    public int TaskId { get; set; }
    public Task Task { get; set; }
}
    
}

