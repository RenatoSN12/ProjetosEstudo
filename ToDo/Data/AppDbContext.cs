using Microsoft.EntityFrameworkCore;
using ToDo.Data.Mappings;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
        modelBuilder.ApplyConfiguration(new AttachmentMap());
        modelBuilder.ApplyConfiguration(new TagMap());
    }
}