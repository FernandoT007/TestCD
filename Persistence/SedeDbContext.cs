using Microsoft.EntityFrameworkCore;
using SedesFunction.Models;

namespace SedesFunction.Persistence;

public class SedeDbContext : DbContext
{
    public SedeDbContext(DbContextOptions<SedeDbContext> options) : base(options)
    {
    }
    
    public DbSet<Sede> Sedes { get; set; }
}