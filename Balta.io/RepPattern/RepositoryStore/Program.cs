using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repositories;
using RepositoryStore.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IProductRepository,ProductRepository>();

var app = builder.Build();

app.MapGet("v1/products", async (IProductRepository productRepository, CancellationToken token, int skip = 0, int take = 25) =>
    Results.Ok(await productRepository.GetAllAsync(skip, take, token)));

app.MapGet("v1/products/{id:int}", async (IProductRepository productRepository, int id, CancellationToken token) =>
    Results.Ok(await productRepository.GetByIdAsync(id, token)));

app.MapPut("v1/products/", async (IProductRepository productRepository, Product product, CancellationToken token) =>
    Results.Ok(await productRepository.UpdateAsync(product, token)));

app.MapPost("v1/products/", async (IProductRepository productRepository, Product product, CancellationToken token) =>
    Results.Ok(await productRepository.CreateAsync(product, token)));

app.MapDelete("v1/products/{id:int}", async (IProductRepository productRepository, int id, CancellationToken token) =>
{
    var product = await productRepository.GetByIdAsync(id);
    return product == null 
        ? Results.NotFound() 
        : Results.Ok(await productRepository.DeleteAsync(product, token));
});

app.Run();