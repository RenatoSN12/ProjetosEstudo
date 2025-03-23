using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models;

namespace Stock.Api.Data.Mappings;

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