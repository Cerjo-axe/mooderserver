using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.HasIndex(p=>p.Email).IsUnique();
        builder.Property(p=>p.UserName).IsRequired().HasMaxLength(150);
    }
}
