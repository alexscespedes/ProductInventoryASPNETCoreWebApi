using System;

namespace ProductInventoryApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Product> products { get; set; } = new List<Product>();
}
