using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DaysContext(DbContextOptions<DaysContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}