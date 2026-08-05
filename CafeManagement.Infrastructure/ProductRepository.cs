using Microsoft.EntityFrameworkCore;
using CafeManagement.Application.Repositories;
using CafeManagement.Domain;

namespace CafeManagement.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
    {
        _context = context;
        
    }


    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await  _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<bool> ExistsAsync(string name)
    {
        return _context.Products.AnyAsync(p => p.Name == name);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
           return;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}