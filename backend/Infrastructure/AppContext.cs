using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppContext(DbContextOptions<AppContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Metric> Metrics { get; set; }
    public DbSet<MetricType> MetricTypes { get; set; }
}