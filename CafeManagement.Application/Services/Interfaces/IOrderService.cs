using CafeManagement.Domain;

namespace CafeManagement.Application.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
}