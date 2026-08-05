using CafeManagement.Domain;

namespace CafeManagement.Application.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<Category?> GetByNameAsync(string name);
    Task<bool>  ExistsAsync(string name);
    Task<Category?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
