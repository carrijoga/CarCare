using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Domain.Entities;

namespace MyBlazorApp.Infrastructure;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> User { get; set; }
}
