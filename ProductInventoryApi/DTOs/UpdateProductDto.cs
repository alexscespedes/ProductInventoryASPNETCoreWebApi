using System;
using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApi.DTOs;

public class UpdateProductDto
{
    [Required, StringLength(100)]
    public string Name { get; set; } = null!;
    
    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
