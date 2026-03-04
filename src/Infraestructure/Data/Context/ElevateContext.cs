using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Context;

public class ElevateContext : DbContext
{
    public ElevateContext(DbContextOptions<ElevateContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}