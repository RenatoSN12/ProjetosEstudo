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
            .HasColumnType("NVARCHAR")
            .HasColumnName("Title")
            .IsRequired();

        builder.Property(x => x.Done)
            .HasColumnType("bit")
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasIndex(x => x.Title)
            .HasDatabaseName("IX_Task_Title");
        builder.HasIndex(x => x.Done)
            .HasDatabaseName("IX_Task_Done");
    }
    
}