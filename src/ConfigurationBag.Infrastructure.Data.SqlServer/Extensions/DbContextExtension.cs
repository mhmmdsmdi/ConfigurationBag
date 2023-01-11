using ConfigurationBag.Core.Common;
using ConfigurationBag.Infrastructure.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Extensions;

public static class DbContextExtension
{
    public static void AddApplicationDbContext(this IServiceCollection services, string dbHost, string dbName, string dbUser, string dbPassword)
    {
        Assert.NotNull(dbHost, nameof(dbHost));
        Assert.NotNull(dbName, nameof(dbName));
        Assert.NotNull(dbUser, nameof(dbUser));
        Assert.NotNull(dbPassword, nameof(dbPassword));

        var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID={dbUser};Password={dbPassword};TrustServerCertificate=True;";

        /* Migration */
        try
        {
            Console.WriteLine($"{DateTime.Now} - Migration Started.");
            var dataWarehouseDbContext = new ApplicationDbContext(connectionString);

            var pendingMigrations = dataWarehouseDbContext.Database.GetPendingMigrations().ToList();
            Console.WriteLine($"{DateTime.Now} - Pending Migration : {pendingMigrations.Count}");
            if (pendingMigrations.Any())
            {
                dataWarehouseDbContext.Database.Migrate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{DateTime.Now} - Error :");
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine($"{DateTime.Now} - Migration Ended.");
        }

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("ApplicationDbContextHistory", Schemas.Base));
            opt.EnableSensitiveDataLogging();
        });
    }
}