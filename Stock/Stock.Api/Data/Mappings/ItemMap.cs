using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models;

namespace Stock.Api.Data.Mappings;

public class ItemMap : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.CustomId).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(x=> x.Title).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(80);
        builder.Property(x => x.Description).IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(500);
        builder.Property(x => x.Price).HasColumnType("MONEY");
        builder.Property(x => x.IsActive).IsRequired().HasColumnType("SMALLINT");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UpdatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(80);
        
        builder.HasOne(x => x.Category).WithMany();
        builder.HasMany(x => x.Inventories).WithOne(i => i.Item);
        
        builder.HasIndex(x => x.CustomId).IsUnique();
    }
}