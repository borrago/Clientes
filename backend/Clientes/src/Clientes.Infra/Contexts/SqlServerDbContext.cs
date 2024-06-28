using Clientes.Domain.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clientes.Infra.Contexts;

public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : SqlServerContextBase(options)
{
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}