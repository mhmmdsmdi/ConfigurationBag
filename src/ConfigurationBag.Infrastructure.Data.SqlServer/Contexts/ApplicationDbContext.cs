using ConfigurationBag.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString = "";

    public virtual DbSet<Collection> Collections { get; set; } = null!;
    public virtual DbSet<FeatureFlag> FeatureFlags { get; set; } = null!;
    public virtual DbSet<Configuration> Configurations { get; set; } = null!;
    public virtual DbSet<Property> Properties { get; set; } = null!;
    public virtual DbSet<Value> Values { get; set; } = null!;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

/// <summary>
/// DesignTimeFactory for EF Migration, use your full connection string,
/// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
/// </summary>
public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        return new ApplicationDbContext("Data Source=../IoTGateway/iotgateway.db");
    }
}