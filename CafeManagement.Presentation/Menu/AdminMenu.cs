using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Presentation.Menu;

public class AdminMenu
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public AdminMenu(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("===== ADMIN MENU =====");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. View Products");
            Console.WriteLine("5. Add Category");
            Console.WriteLine("6. Update Category");
            Console.WriteLine("7. Delete Category");
            Console.WriteLine("8. Exit");
            
            Console.Write("choose:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "Add Product":
                    Console.WriteLine("===== ADD NEW PRODUCT =====");
                    string? name;
                    string? category;
                    int price;
                    int quantity;
                    while (true)
                    {
                        Console.Write("Name:");
                        name = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Name cannot be empty");
                            continue;
                        }

                        break;
                    }

                    while (true)
                    {
                        Console.WriteLine("Category:");

                        var categories = await _categoryService.GetAllAsync();

                        foreach (var item in categories)
                        {
                            Console.WriteLine($"{item.Id} - {item.Name}");
                        }

                        Console.Write("Choose category id: ");

                        if (!int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            Console.WriteLine("Category id must be a number.");
                            continue;
                        }

                        var selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);

                        if (selectedCategory == null)
                        {
                            Console.WriteLine("Category not found.");
                            continue;
                        }

                        category = selectedCategory.Name;
                        break;
                    }

                    while (true)
                    {
                        Console.Write("Price:");

                        if (!int.TryParse(Console.ReadLine(), out price))
                        {
                            Console.WriteLine("Price must be a number.");
                            continue;
                        }

                        break;
                    }

                    while (true)
                    {
                        Console.Write("Quantity:");
                        if (!int.TryParse(Console.ReadLine(), out quantity))
                        {
                            Console.WriteLine("Quantity must be a number.");
                            continue;
                        }

                        break;
                    }

                    var product = new Product
                    {
                        Name = name,
                        Price = price,
                        Quantity = quantity,
                        CategoryName = category
                    };

                    while (true)
                    {
                        Console.WriteLine("[S] Save");
                        Console.WriteLine("[C] Cancel");
                        var confirmation = Console.ReadLine()?.ToLower();
                        if (string.IsNullOrWhiteSpace(confirmation))
                        {
                            Console.WriteLine("Choose one");
                            continue;
                        }
                        else if (confirmation == "c")
                        {
                            break;
                        }
                        if (confirmation == "s")
                        {
                            await _productService.AddAsync(product);
                            Console.WriteLine("Product added successfully.");
                            break;
                        }
                    }

                    break;
                case "Update Product":
                    Console.WriteLine("===== UPDATE PRODUCT =====");
                    Console.WriteLine("Id:");
                    
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Product id must be a number.");
                        continue;
                    }
                    
                    string? nameUpdate;
                    string? categoryUpdate;
                    int priceUpdate;
                    int quantityUpdate;
                    
                    while (true)
                    {
                        Console.Write("Name:");
                        nameUpdate = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nameUpdate))
                        {
                            Console.WriteLine("Name cannot be empty");
                            continue;
                        }

                        break;
                    }

                    while (true)
                    {
                        Console.WriteLine("Category:");

                        var categories = await _categoryService.GetAllAsync();

                        foreach (var item in categories)
                        {
                            Console.WriteLine($"{item.Id} - {item.Name}");
                        }

                        Console.Write("Choose category id: ");

                        if (!int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            Console.WriteLine("Category id must be a number.");
                            continue;
                        }

                        var selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);

                        if (selectedCategory == null)
                        {
                            Console.WriteLine("Category not found.");
                            continue;
                        }

                        categoryUpdate = selectedCategory.Name;
                        break;
                    }

                    while (true)
                    {
                        Console.Write("Price:");

                        if (!int.TryParse(Console.ReadLine(), out priceUpdate))
                        {
                            Console.WriteLine("Price must be a number.");
                            continue;
                        }

                        break;
                    }

                    while (true)
                    {
                        Console.Write("Quantity:");
                        if (!int.TryParse(Console.ReadLine(), out quantityUpdate))
                        {
                            Console.WriteLine("Quantity must be a number.");
                            continue;
                        }

                        break;
                    }

                    var productUpdate = new Product
                    {
                        Id = id,
                        Name = nameUpdate,
                        Price = priceUpdate,
                        Quantity = quantityUpdate,
                        CategoryName = categoryUpdate
                    };

                    while (true)
                    {
                        Console.WriteLine("[S] Save");
                        Console.WriteLine("[C] Cancel");
                        var confirmation = Console.ReadLine()?.ToLower();
                        if (string.IsNullOrWhiteSpace(confirmation))
                        {
                            Console.WriteLine("Choose one");
                            continue;
                        }
                        else if (confirmation == "c")
                        {
                            break;
                        }
                        if (confirmation == "s")
                        {
                            await _productService.UpdateAsync(productUpdate);
                            Console.WriteLine("Product updated successfully.");
                            break;
                        }
                    }
                    break;
                case "Delete Product":
                    Console.WriteLine("===== DELETING PRODUCT =====");
                    while (true)
                    {
                        Console.Write("Write product Id: ");

                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Product id must be a number.");
                            continue;
                        }
                        
                       while (true)
                        {
                            Console.WriteLine("[D] Delete");
                            Console.WriteLine("[C] Cancel");
                            Console.Write("To delete type:");
                            var deleteProdChoice = Console.ReadLine()?.ToLower();
                        
                            if (deleteProdChoice == "d")
                            {
                                await _productService.DeleteAsync(productId);
                                Console.WriteLine("Product deleted successfully.");
                                break;
                            }
                            else if (deleteProdChoice == "c")
                            {
                                Console.WriteLine("Deleting canceled.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                                continue;
                            }
                        }
                        
                        break;
                    }
                    break;
                case "View Products":
                    Console.WriteLine("===== ALL PRODUCTS =====");
                    Console.WriteLine(new string('-', 70));
                    Console.WriteLine($"{"id",-5}|{"Name",-10} | {"Category",-15} | {"Price",-10} | {"Quantity",-10} |");
                    Console.WriteLine(new string('-', 70));
                    var products = await _productService.GetAllAsync();
                    var catView = await _categoryService.GetAllAsync();
                    
                    if (!products.Any())
                    {
                        Console.WriteLine("No products found.");
                        break;
                    }
                    foreach (var prod in products)
                    {
                        var categ = catView.FirstOrDefault(c => c.Id == prod.CategoryId);
                        Console.WriteLine($"{prod.Id,-5} | {prod.Name,-10} | {categ?.Name,-15} | {prod.Price,-10} | {prod.Quantity,-10} |");
                    }
                    Console.WriteLine(new string('-', 70));
                    break;
                case "Add Category":
                    Console.WriteLine("===== ADD CATEGORY =====");
                    while (true)
                    {
                        Console.Write("Write category name: ");
                        var categoryName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(categoryName))
                        {
                            Console.WriteLine("Name cannot be empty.");
                            continue;
                        }
                        
                        while (true)
                        {
                            Console.WriteLine("[S] Save");
                            Console.WriteLine("[C] Cancel");
                            Console.Write("To add type:");
                            var addCatChoice = Console.ReadLine()?.ToLower();
                        
                            if (addCatChoice == "s")
                            {
                                var categoryAdd = new Category
                                {
                                    Name = categoryName
                                };
                                await _categoryService.AddAsync(categoryAdd);
                                Console.WriteLine("Category added successfully.");
                                break;
                            }
                            else if (addCatChoice == "c")
                            {
                                Console.WriteLine("Adding canceled.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                                continue;
                            }
                            
                        }
                            
                        break;
                    }
                    break;
                case "Update Category":
                    Console.WriteLine("===== UPDATE CATEGORY =====");
                    while (true)
                    {
                        Console.Write("Write category id: ");
                        if (!int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            Console.WriteLine("Category id must be a number.");
                            continue;
                        }
                        
                        Console.Write("Write category name: ");
                        var categoryName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(categoryName))
                        {
                            Console.WriteLine("Name cannot be empty.");
                            continue;
                        }

                        while (true)
                        {
                            Console.WriteLine("[S] Save");
                            Console.WriteLine("[C] Cancel");
                            Console.Write("To update type:");
                            var updateCatChoice = Console.ReadLine()?.ToLower();
                            if (updateCatChoice == "s")
                            {
                                var updateCat = new Category
                                {
                                    Id = categoryId,
                                    Name = categoryName
                                };
                                await _categoryService.UpdateAsync(updateCat);
                                Console.WriteLine("Category updated successfully.");
                                break;
                            }
                            else if (updateCatChoice == "c")
                            {
                                Console.WriteLine("Updating canceled.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                                continue;
                            }
                        }
                        break;
                    }
                    break;
                case "Delete Category":
                    Console.WriteLine("===== DELETING CATEGORY =====");
                    while (true)
                    {
                        Console.Write("Write category Id: ");

                        if (!int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            Console.WriteLine("Category id must be a number.");
                            continue;
                        }

                        while (true)
                        {
                            Console.WriteLine("[D] Delete");
                            Console.WriteLine("[C] Cancel");
                            Console.Write("To delete type:");
                            var deleteCatChoice = Console.ReadLine()?.ToLower();
                        
                            if (deleteCatChoice == "d")
                            {
                                await _categoryService.DeleteAsync(categoryId);
                                Console.WriteLine("Category deleted successfully.");
                                break;
                            }
                            else if (deleteCatChoice == "c")
                            {
                                Console.WriteLine("Deleting canceled.");
                                break;
                            }
                            else 
                            {
                                Console.WriteLine("Invalid choice.");
                                continue;
                            }
                        }
                        
                        break;
                    }
                    break;
                case "Exit":
                    Console.WriteLine("===== EXIT =====");
                    Console.WriteLine("Goodbye!");
                    return;
            }
        }
    }
}