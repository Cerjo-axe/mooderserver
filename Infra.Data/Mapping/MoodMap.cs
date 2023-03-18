using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class MoodMap : IEntityTypeConfiguration<MoodDay>
{
    public void Configure(EntityTypeBuilder<MoodDay> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(p=>p.Date).IsRequired();
        builder.Property(p=>p.Mood).IsRequired();
        builder.Property(p=>p.Genre).IsRequired();
        builder.Property(p=>p.Description).HasMaxLength(150);
        builder.HasOne(p=>p.User).WithMany(p=>p.MoodDays).HasForeignKey(p=>p.UserId).IsRequired();
    }
}
