using CafeManagement.Domain;

namespace CafeManagement.Application.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> AddAsync(Order order);
    Task<Order> GetByIdAsync(int id);
}
