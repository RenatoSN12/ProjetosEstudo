using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = ToDo.Models.Task;

namespace ToDo.Data.Mappings;

public class TaskMap : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Task");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder.Property(x=> x.Title)
            .HasMaxLength(100)
            .HasColumnType("VARCHAR")
            .HasColumnName("Title")
            .IsRequired();

        builder.Property(x => x.Done)
            .HasColumnType("BOOLEAN")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.TaskPriority)
            .HasColumnType("INT")
            .IsRequired();
        
        builder.Property(x => x.DueDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.Property(x => x.LastModificationDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.Property(x => x.CompletionDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.HasOne(x => x.AssignedUser)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.AssignedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.CreationDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.HasIndex(x => x.Title)
            .HasDatabaseName("IX_Task_Title");
        builder.HasIndex(x => x.Done)
            .HasDatabaseName("IX_Task_Done");
        
        builder.HasMany(x=>x.Attachments)
            .WithOne(x=>x.Task)
            .HasForeignKey(x=>x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.AssignedUser)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.AssignedUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(t => t.Tags)
            .WithMany() 
            .UsingEntity(j => j.ToTable("TaskTags"));
    }
    
}