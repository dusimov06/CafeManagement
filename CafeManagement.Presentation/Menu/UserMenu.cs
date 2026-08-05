using CafeManagement.Application.Services.Interfaces;

namespace CafeManagement.Presentation.Menu;

public class UserMenu
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    
    public UserMenu(IProductService productService, ICartService cartService, ICategoryService categoryService)
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
            Console.WriteLine("4. Exit");
            
            Console.Write("Choose: ");
            var choise = Console.ReadLine();

            switch (choise)
            {
                case "Show Menu":
                    while (true)
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
                            break;
                        
                        var selectedCategory = category.Find(i =>i.Id == categoryChoice);
                        
                        if(selectedCategory == null)
                            throw new Exception("Category not found");
                        
                        var products = await _productService.GetAllAsync();

                        var categoryProducts = products.Where(i => i.CategoryId == selectedCategory.Id ).ToList();

                        foreach (var product in categoryProducts)
                        {
                            Console.WriteLine($"{product.Id} | {product.Name} | {product.Price}");
                        }
                        Console.WriteLine("0 – Back");
                        Console.Write("choose product: ");
                        
                        if(!int.TryParse(Console.ReadLine(), out int productChoice))
                            throw new Exception("Invalid choice");
                        
                        if (productChoice == 0)
                            continue;

                        var selectedProduct = categoryProducts.FirstOrDefault(i => i.Id == productChoice);
                        
                        if (selectedProduct == null)
                            throw new Exception("Product not found");
                        
                        Console.WriteLine("Amount: ");
                        
                        if(!int.TryParse(Console.ReadLine(),out int quantity))
                            throw new Exception("Invalid choice");
                        
                        await _cartService.AddToCartAsync(productChoice, quantity);
                    }
                   break;
                case "View Cart":
                    Console.WriteLine("\n===== CART ITEMS =====");
                    Console.WriteLine(new string('-',10));
                    
                    var cart = await _cartService.GetCartAsync();
                    var prod = await _productService.GetAllAsync();
                    
                    if(cart.Count <= 0)
                        throw new Exception("Cart is empty.");

                    foreach (var item in cart)
                    {
                        var product = prod.FirstOrDefault(i => i.Id == item.ProductId);
                        Console.WriteLine($"{item.Id,-5}. {product?.Name,-10} | {item.Price,-10} x {item.Quantity,-10} = {item.Price*item.Quantity,-5}");
                    }
                    
                    Console.WriteLine(new string('-', 10));
                    
                    var total = cart.Sum(i => i.Price*i.Quantity);
                    Console.WriteLine("Total price: " + total);
                    break;
                case "Pay":
                    Console.WriteLine("\n===== CART ITEMS =====");
                    Console.WriteLine(new string('-',10));
                    
                    var cartPay = await _cartService.GetCartAsync();
                    var prodPay = await _productService.GetAllAsync();
                    
                    if(cartPay.Count <= 0)
                        throw new Exception("Cart is empty.");

                    foreach (var item in cartPay)
                    {
                        var product = prodPay.FirstOrDefault(i => i.Id == item.ProductId);
                        Console.WriteLine($"{item.Id,-5}. {product?.Name,-10} | {item.Price,-10} x {item.Quantity,-10} = {item.Price*item.Quantity,-5}");
                    }
                    
                    Console.WriteLine(new string('-', 10));
                    
                    var totalPay = cartPay.Sum(i => i.Price*i.Quantity);
                    Console.WriteLine("Total price: " + totalPay);
                    
                    Console.WriteLine(new string('-', 10));
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
                   break;
                case "Exit":
                    Console.WriteLine("good bay");
                    return;
            }
        }
    }
}