using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null) return false;

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int? categoryId = null)
    {
        var query = _context.products.Include(p => p.Category).AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        return await query.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(P => P.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        var exists = await _context.products.AnyAsync(p => p.Id == id);
        if (!exists) return false;

        product.Id = id;
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
