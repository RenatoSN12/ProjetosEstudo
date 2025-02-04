using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Models;

namespace ToDo.Data.Mappings;

public class AttachmentMap : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachment");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder.Property(x => x.FileName)
            .HasMaxLength(255)
            .HasColumnType("VARCHAR")
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasMaxLength(500)
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder.Property(x => x.FileType)
            .HasMaxLength(100)
            .HasColumnType("VARCHAR")
            .IsRequired();
        
        builder.Property(x => x.FileSize)
            .HasColumnType("BIGINT")
            .IsRequired();
        
        builder.Property(x => x.UploadedAt)
            .HasColumnType("TIMESTAMP")
            .IsRequired();
        
        builder.HasOne(x => x.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}