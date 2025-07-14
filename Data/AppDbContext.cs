using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
namespace TodoApi.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  public DbSet<User> Users => Set<User>();
  public DbSet<TodoTask> Tasks => Set<TodoTask>();

// Aqu iesta la relacion entre user y todotask
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TodoTask>()
        .HasOne(t => t.User)
        .WithMany(t => t.Tasks)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}