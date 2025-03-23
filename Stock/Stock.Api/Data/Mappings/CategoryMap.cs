using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models;

namespace Stock.Api.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("SMALLINT");
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.HasIndex(o => new { o.UserId, o.Id });
    }
}