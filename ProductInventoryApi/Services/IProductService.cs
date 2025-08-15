using System;
using ProductInventoryApi.DTOs;
using ProductInventoryApi.Models.DTOs;

namespace ProductInventoryApi.Middleware;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(
        string? search = null,
        string? sortBy = null,
        bool sortDesc = false,
        int pageNumber = 1,
        int pageSize = 10
    );
    
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<bool> UpdateAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);
}
