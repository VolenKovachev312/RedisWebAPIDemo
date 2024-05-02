using Microsoft.EntityFrameworkCore;
using RedisWebAPIDemo.Models;

namespace RedisWebAPIDemo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Driver> Drivers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}