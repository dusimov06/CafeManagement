using CafeManagement.Domain;

namespace CafeManagement.Application.Services.Interfaces;

public interface ICartService
{
    Task AddToCartAsync(int productId, int quantity);
    Task<List<OrderItem>> GetCartAsync();
    Task ClearCartAsync();
    Task<Order> CheckoutAsync();
    decimal GetCartTotal();
}