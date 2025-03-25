using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class ItemStockMap : IEntityTypeConfiguration<ItemStock>
{
    public void Configure(EntityTypeBuilder<ItemStock> builder)
    {
        builder.ToTable("ItemStock");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MinimumStockLevel).HasColumnType("INT");
        builder.Property(x => x.MaximumStockLevel).HasColumnType("INT");
        builder.Property(x => x.Quantity).HasColumnType("INT");
        builder.Property(x=> x.LastUpdatedDate).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(80);

        builder.HasOne(x => x.Item).WithMany().HasForeignKey(x => x.ItemId);
        builder.HasOne(x=> x.Location).WithMany().HasForeignKey(i => i.ItemId);;
        
        builder.HasIndex(x=> new {x.LocationId,x.ItemId}).IsUnique();
    }
}