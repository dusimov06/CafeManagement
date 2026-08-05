using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> AddAsync(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty");
        
        if(await _categoryRepository.ExistsAsync(category.Name))
            throw new ArgumentException($"Category '{category.Name}' already exists");
        
        return await _categoryRepository.AddAsync(category);
    }

    public async Task<Category> UpdateAsync(Category category)
    {        
        if(category.Id <= 0)
            throw new ArgumentException("Invalid category id");
        
        var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
        
        if(existingCategory == null)
            throw new ArgumentException("Category not found");
        
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty");
        existingCategory.Name = category.Name;
        
        return await _categoryRepository.UpdateAsync(existingCategory);
    }

    public async Task DeleteAsync(int id)
    {
        if(id <= 0)
            throw new ArgumentException("Invalid category id");
        
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        
        if(existingCategory == null)
            throw new ArgumentException("Category not found");

        await _categoryRepository.DeleteAsync(id);
    }
}
