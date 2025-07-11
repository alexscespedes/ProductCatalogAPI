using System;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Category> CreateAsync(Category category)
    {
        _context.categories.Add(category);
        await _context.SaveChangesAsync();
        return category; 
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.categories.FindAsync(id);
    }
}
