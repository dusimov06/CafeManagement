using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Infrastructure;
using CafeManagement.Presentation.Menu;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// repositories
services.AddDbContext<AppDbContext>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
// services
services.AddScoped<ICartService, CartService>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<ICategoryService, CategoryService>();
// menu
services.AddScoped<UserMenu>();
services.AddScoped<AdminMenu>();

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

var userMenu = provider.GetRequiredService<UserMenu>();
var adminMenu = provider.GetRequiredService<AdminMenu>();


while (true)
{
    Console.WriteLine("===== CAFE MANAGEMENT =====");
    Console.WriteLine("1. User");
    Console.WriteLine("2. Admin");
    Console.WriteLine("3. Exit");

    Console.Write("Choose: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await userMenu.RunAsync();
            break;

        case "2":
            await adminMenu.RunAsync();
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

