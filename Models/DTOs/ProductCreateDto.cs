using System;
using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApi.Models.DTOs;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
