using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class LocationMap : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(255).HasColumnType("VARCHAR");
        builder.Property(x=>x.IsActive).IsRequired().HasColumnType("SMALLINT");
        builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(80);
    }
}