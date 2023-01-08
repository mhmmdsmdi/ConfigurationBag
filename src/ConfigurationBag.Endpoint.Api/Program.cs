using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Common;
using ConfigurationBag.Core.Common.Extensions;
using ConfigurationBag.Core.Common.Mapping;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Domain.Models;
using ConfigurationBag.EndPoint.Api.Extensions;
using ConfigurationBag.Infrastructure.Data.SqlServer;
using ConfigurationBag.Infrastructure.Data.SqlServer.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Core;
using Serilog.Events;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
    {
        var commonAssembly = typeof(Assert).Assembly;
        var domainAssembly = typeof(Configuration).Assembly;
        var serviceAssembly = typeof(ConfigurationService).Assembly;
        var repositoryAssembly = typeof(IRepository<>).Assembly;

        containerBuilder.AddServices(typeof(Repository<>), typeof(IRepository<>),
            commonAssembly, domainAssembly, serviceAssembly, repositoryAssembly);
    }));

var levelSwitch = new LoggingLevelSwitch
{
    MinimumLevel = LogEventLevel.Information
};
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(DotNetEnv.Env.GetString("SEQ_URL"),
        apiKey: DotNetEnv.Env.GetString("SEQ_API_KEY"),
        controlLevelSwitch: levelSwitch)
    .MinimumLevel.ControlledBy(levelSwitch)
    .CreateLogger();
Serilog.Debugging.SelfLog.Enable(Console.Error);

try
{
    /* Configure Services */

    builder.Host.UseSerilog();

    /* Base Database */
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var dbUser = Environment.GetEnvironmentVariable("DB_USER");
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
    builder.Services.AddApplicationDbContext(dbHost, dbName, dbUser, dbPassword);

    builder.Services.InitializeAutoMapper(typeof(Configuration).Assembly);
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    builder.Services.AddValidatorsFromAssemblyContaining<ConfigurationInsertValidator>();
    builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApiSwagger();

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    var app = builder.Build();

    /* Configure the HTTP request pipeline. */

    app.UseSerilogRequestLogging();

    app.UseExceptionHandlingMiddleware();

    app.UseApiSwagger();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}