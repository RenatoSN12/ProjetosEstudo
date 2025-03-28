using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.Fullname, name =>
        {
            name.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("FirstName")
                .HasColumnType("NVARCHAR");

            name.Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("LastName")
                .HasColumnType("NVARCHAR");
        });
        
        builder.Property(x=> x.Email).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
        builder.Property(x=> x.IsActive).IsRequired().HasColumnType("SMALLINT");
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255).HasColumnType("NVARCHAR");
        
        builder.HasIndex(x => x.Email).IsUnique();
    }
}