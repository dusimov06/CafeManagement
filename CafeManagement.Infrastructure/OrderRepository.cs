using CafeManagement.Domain;
using CafeManagement.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;
    
    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await  _dbContext.Orders.Include(o => o.Items).ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await  _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}