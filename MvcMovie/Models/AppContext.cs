using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
public class ApplicationContext : DbContext
{
    public DbSet<GeoTask> GeoTasks { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }    
}