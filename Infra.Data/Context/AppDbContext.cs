using Domain.Entity;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<MoodDay> MoodDays {get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);
        mb.Entity<MoodDay>(new MoodMap().Configure);
        mb.Entity<User>(new UserMap().Configure);
    }

}
