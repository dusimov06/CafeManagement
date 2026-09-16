using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Presentation.Menu;

public class UserMenu
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    
    public UserMenu(IProductService productService, 
                    ICartService cartService, 
                    ICategoryService categoryService)
    {
        _productService = productService;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=====USER MENU====="); 
            Console.WriteLine("1. Show Menu");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Pay");
            Console.WriteLine("0. Exit");
            
            Console.Write("Choose: ");
            var choise = Console.ReadLine();

            try
            {
                switch (choise)
                {
                    case "1":
                        await ShowMenuAsync();
                        break;
                    case "2":
                        await ViewCartAsync();
                        break;
                    case "3":
                        await PayAsync();
                        break;
                    case "0":
                        Console.WriteLine("goodbye !");
                        return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }
    }
    private async Task ShowMenuAsync()
    {
     while (true)
        {
            var selectedCategory = await SelectCategoryAsync();
            if (selectedCategory == null)
                return;
            
            var selectedProduct = await SelectProductAsync(selectedCategory);
            if (selectedProduct == null)
                continue;
            
            Console.WriteLine("Amount: ");
            
            if(!int.TryParse(Console.ReadLine(),out int quantity))
                throw new Exception("Invalid choice");
            
            await _cartService.AddToCartAsync(selectedProduct.Id, quantity);
        }
    }

    private async Task<Category?> SelectCategoryAsync()
    {
        var category = await _categoryService.GetAllAsync();

        foreach (var categoryItem in category)
        {
            Console.WriteLine($"{categoryItem.Id} - {categoryItem.Name}");
        }
        Console.WriteLine("0 – Back");
        Console.Write("choose: ");
            
        if(!int.TryParse(Console.ReadLine(), out int categoryChoice))
            throw new Exception("Invalid choice");

        if (categoryChoice == 0)
            return null;
            
        var selectedCategory = category.Find(i =>i.Id == categoryChoice);
            
        if(selectedCategory == null)
            throw new Exception("Category not found");
            
        return selectedCategory;
        
    }

    private async Task<Product?> SelectProductAsync(Category category)
    {
        var products = await _productService.GetAllAsync();

        var categoryProducts = products.Where(i => i.CategoryId == category.Id ).ToList();

        var index = 1;
                
        foreach (var product in categoryProducts)
        {
            Console.WriteLine($"{index} | {product.Name} | {product.Price}");
            index++;
        }
        Console.WriteLine("0 – Back");
            
        Console.Write("choose product: ");
            
        if(!int.TryParse(Console.ReadLine(), out int productChoice))
            throw new Exception("Invalid choice");
            
        if (productChoice == 0)
            return null;

        var selectedProduct = categoryProducts.ElementAtOrDefault(productChoice - 1);
            
        if (selectedProduct == null)
            throw new Exception("Product not found");
        
        return selectedProduct;
    }
    private async Task ViewCartAsync()
    {
        await ShowCartAsync();
    }

    private async Task PayAsync()
    {
        await ShowCartAsync();

        Console.WriteLine();
        Console.WriteLine("Do you want to pay? y/n");

        if (Console.ReadLine()?.ToLower() == "y")
        {
            await _cartService.CheckoutAsync();
            Console.WriteLine("Payment successful");
        }
        else
        {
            Console.WriteLine("Payment cancelled."); 
        }
    }
    private async Task<decimal> ShowCartAsync()
    {
        var cart = await _cartService.GetCartAsync();
        var products = await _productService.GetAllAsync();

        if (cart.Count == 0)
            throw new Exception("Cart is empty.");

        Console.WriteLine("\n===== CART ITEMS =====");
        Console.WriteLine(new string('-', 50));

        foreach (var item in cart)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);

            Console.WriteLine(
                $"{item.Id,-5}. {product?.Name,-15} | " +
                $"{item.Price,-10} x {item.Quantity,-5} = {item.Price * item.Quantity}"
            );
        }

        var total = _cartService.GetCartTotal();

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"Total price: {total}");

        return total;
    }
}