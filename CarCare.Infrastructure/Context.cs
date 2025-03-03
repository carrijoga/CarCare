using Microsoft.EntityFrameworkCore;
using CarCare.Domain.Entities.Users;
using CarCare.Domain.Entities.Persons;

namespace CarCare.Infrastructure;

public class Context : DbContext
{
    public Context() => Database.SetCommandTimeout(12000); 
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    #region Models

    public DbSet<User> Users { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<PersonType> PersonType { get; set; }
    public DbSet<PersonAddress> PersonAddress { get; set; }
    
    #endregion
}
