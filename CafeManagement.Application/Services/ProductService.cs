using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty");
        
        if (product.Price <= 0)
            throw new ArgumentException("Product price cannot be zero or negative");
        
        if (product.Quantity <= 0)
            throw new ArgumentException("Product quantity cannot be zero or negative");
        
        if(await _productRepository.ExistsAsync(product.Name))
            throw new ArgumentException($"Product '{product.Name}' already exists");
        
        if (string.IsNullOrWhiteSpace(product.CategoryName))
            throw new ArgumentException("Category name cannot be empty");
        
        var category = await _categoryRepository.GetByNameAsync(product.CategoryName);
        if (category == null)
            throw new ArgumentException("Category not found");
        product.CategoryId = category.Id;
        
        
        return await _productRepository.AddAsync(product);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        if(product.Id <= 0)
            throw new ArgumentException("Invalid product id");

        var existingProduct = await _productRepository.GetProductByIdAsync(product.Id);
        
        if(existingProduct == null)
            throw new ArgumentException("Product not found");
        
        if(string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty");
        
        existingProduct.Name = product.Name;
        
        if(product.Price <= 0)
            throw new ArgumentException("Product price cannot be zero or negative");
        existingProduct.Price = product.Price;
        
        if(product.Quantity <= 0)
            throw new ArgumentException("Product quantity cannot be zero or negative");
        existingProduct.Quantity = product.Quantity;
        
        if (string.IsNullOrWhiteSpace(product.CategoryName))
            throw new ArgumentException("Category name cannot be empty");
        
        var category = await _categoryRepository.GetByNameAsync(product.CategoryName);
        
        if (category == null)
            throw new ArgumentException("Category not found");
        existingProduct.CategoryName = product.CategoryName;
        existingProduct.CategoryId = category.Id;
        
        return await _productRepository.UpdateAsync(existingProduct);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid product id");
        
        var existingProduct = await _productRepository.GetProductByIdAsync(id);
        
        if(existingProduct == null)
            throw new ArgumentException("Product not found");

        await _productRepository.DeleteAsync(id);
    }
}
