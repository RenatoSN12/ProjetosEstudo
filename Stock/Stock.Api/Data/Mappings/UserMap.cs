using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models;

namespace Stock.Api.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Username).IsRequired().HasMaxLength(50).HasColumnType("NVARCHAR");
        builder.Property(x=> x.Email).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
        builder.Property(x=> x.IsActive).IsRequired().HasColumnType("SMALLINT");
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255).HasColumnType("NVARCHAR");
        
        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}