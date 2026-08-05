using CafeManagement.Application.Repositories;
using CafeManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Infrastructure;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public Task<Category?> GetByNameAsync(string name)
    {
        return _context.Categories.FirstOrDefaultAsync(p => p.Name == name);
    }

    public Task<bool> ExistsAsync(string name)
    {
        return _context.Categories.AnyAsync(c => c.Name == name);
    }

    public Task<Category?> GetByIdAsync(int id)
    {
        return _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}