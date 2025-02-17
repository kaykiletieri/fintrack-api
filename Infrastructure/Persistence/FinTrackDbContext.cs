using Microsoft.EntityFrameworkCore;

namespace FinTrackAPI.Infrastructure.Persistence;

public class FinTrackDbContext : DbContext
{
    public FinTrackDbContext(DbContextOptions<FinTrackDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
    }
}
