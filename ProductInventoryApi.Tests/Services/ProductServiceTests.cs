using System;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApi.Data;
using ProductInventoryApi.Models;
using ProductInventoryApi.Models.DTOs;
using ProductInventoryApi.Services;

namespace ProductInventoryApi.Tests.Services;

public class ProductServiceTests
{
    private AppDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Categories.AddRange(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Accesories" }
        );

        context.Products.AddRange(
            new Product { Id = 1, Name = "Laptop", Price = 999.99M, StockQuantity = 5, CategoryId = 1 },
            new Product { Id = 2, Name = "Mouse", Price = 25.50M, StockQuantity = 15, CategoryId = 2 }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetAllProducts_ReturnsCorrectCount()
    {
        // Arrange
        var context = GetDbContextWithData();
        var service = new ProductService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddProduct_SavesToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Categories.Add(new Category { Id = 1, Name = "Electronics" });

        var service = new ProductService(context);

        var newProduct = new CreateProductDto
        {
            Name = "Keyboard",
            Price = 45.00M,
            StockQuantity = 10,
            CategoryId = 1
        };

        // Act
        await service.CreateAsync(newProduct);
        var products = await service.GetAllAsync();

        Assert.Single(products);
        Assert.Equal("Keyboard", products.First().Name);
    }

    [Fact]
    public async Task DeleteProduct_RemovesFromDatabase()
    {
        // Arrange
        var context = GetDbContextWithData();
        var service = new ProductService(context);

        // Act
        var deleted = await service.DeleteAsync(1);
        var products = await service.GetAllAsync();

        Assert.True(deleted);
        Assert.Single(products);
    }
}
