using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Database;

namespace TodoApp.Contexts;

public sealed class DatabaseContext : IdentityDbContext<User> 
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    public DbSet<Todo> Todos { get; set; }
}