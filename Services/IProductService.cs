using System;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(int? categoryId = null);

    Task<Product?> GetByIdAsync(int id);

    Task<Product> CreateAsync(Product product);

    Task<bool> UpdateAsync(int id, Product product);

    Task<bool> DeleteAsync(int id);
}
