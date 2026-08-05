using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    
    public  OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }
    
    public async Task<Order> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid order id");

        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            throw new ArgumentException("Order not found");

        return order;
    }
}
