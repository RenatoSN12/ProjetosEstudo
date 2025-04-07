using Microsoft.EntityFrameworkCore;
using StockApp.Api.Common.Extensions;
using StockApp.Domain;
using StockApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.Services.AddApplicationServices();
builder.Services.AddCrossOrigin();
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(Configuration.ConnectionString, b
        => b.MigrationsAssembly("StockApp.Api"));
});

// builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAuthenticationSetup();
builder.Services.AddValidation();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
app.UseAppConfiguration();

app.Run();
