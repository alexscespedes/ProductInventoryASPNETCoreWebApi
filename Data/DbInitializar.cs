using System;
using ProductInventoryApi.Models;

namespace ProductInventoryApi.Data;

public class DbInitializar
{
    public static void Seed(AppDbContext context)
    {
        if (context.Categories.Any() || context.Products.Any())
            return;

        var categories = new List<Category>
        {
            new Category{ Name = "Electronics" },
            new Category{ Name = "Books" },
            new Category{ Name = "Clothing" }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();

        var products = new List<Product>
        {
            new Product{Name = "Laptop", Description = "15-inch display", Price = 1200.50m, StockQuantity = 10, CategoryId = 1},
            new Product{Name = "Smartphone", Description = "5G enabled", Price = 800.00m, StockQuantity = 25, CategoryId = 1},
            new Product{Name = "Novel", Description = "Bestselling fiction", Price = 15.99m, StockQuantity = 100, CategoryId = 2},
            new Product{Name = "T-Shirt", Description = "Cotton, size M", Price = 12.00m, StockQuantity = 50, CategoryId = 3}
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
