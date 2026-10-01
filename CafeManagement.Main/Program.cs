using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Infrastructure;
using CafeManagement.Presentation;
using CafeManagement.Presentation.Menu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

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
services.AddScoped<IAdminAccessService>(_ => new AdminAccessService(configuration["AdminAccessCode"]!));
// menu
services.AddScoped<UserMenu>();
services.AddScoped<AdminMenu>();
services.AddTransient<CursorMenu>();

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
}

var userMenu = provider.GetRequiredService<UserMenu>();

await userMenu.RunAsync();

