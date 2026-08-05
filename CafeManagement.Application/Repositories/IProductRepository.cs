using CafeManagement.Domain;

namespace CafeManagement.Application.Repositories;

public interface IProductRepository 
{
    Task<Product> GetProductByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> ExistsAsync(string name);
    Task DeleteAsync(int id);
}
