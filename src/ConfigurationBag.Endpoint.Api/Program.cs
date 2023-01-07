using ConfigurationBag.EndPoint.Api.Extensions;
using ConfigurationBag.Infrastructure.Data.SqlServer.Extensions;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

/* Configure Services */

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiSwagger();

/* Base Database */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
builder.Services.AddApplicationDbContext(dbHost, dbName, dbUser, dbPassword);

var app = builder.Build();

/* Configure the HTTP request pipeline. */

app.UseExceptionHandlingMiddleware();

app.UseApiSwagger();

app.UseAuthorization();

app.MapControllers();

app.Run();