using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class ElevateContext(DbContextOptions<ElevateContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}