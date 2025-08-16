using System;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductInventoryApi.DTOs;
using ProductInventoryApi.Models.DTOs;

namespace ProductInventoryApi.Tests.Integration;

public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/Products");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task PostProduct_CreatesNewProduct()
    {
        // Arrange
        var newProduct = new CreateProductDto
        {
            Name = "Monitor",
            Price = 199.99M,
            StockQuantity = 10,
            CategoryId = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Products", newProduct);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(created);
        Assert.Equal("Monitor", created!.Name);
    }

    [Fact]
    public async Task DeleteProduct_RemovesProduct()
    {
        // Arrange
        var product = new CreateProductDto
        {
            Name = "Headphones",
            Price = 49.99M,
            StockQuantity = 5,
            CategoryId = 1
        };

        // Act
        var createResponse = await _client.PostAsJsonAsync("/api/Products", product);
        var created = await createResponse.Content.ReadFromJsonAsync<ProductDto>();

        var deleteResponse = await _client.DeleteAsync($"/api/products/{created!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
