using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class MoodMap : IEntityTypeConfiguration<MoodDay>
{
    public void Configure(EntityTypeBuilder<MoodDay> builder)
    {
        builder.HasKey(prop => prop.Id);
    }
}
