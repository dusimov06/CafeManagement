using CafeManagement.Application.Repositories;
using CafeManagement.Application.Services.Interfaces;
using CafeManagement.Domain;

namespace CafeManagement.Application.Services;

public class CartService : ICartService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly List<OrderItem> _orderItems = new();

    public CartService(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task AddToCartAsync(int productId, int quantity)
    {
        if(productId <= 0)
            throw new ArgumentException("Invalid product id");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        var existingProduct = await _productRepository.GetProductByIdAsync(productId);

        if (existingProduct == null)
            throw new ArgumentException("Product not found");
        
        if(quantity > existingProduct.Quantity)
            throw new ArgumentException("Quantity exceeds quantity");
        
        var item = _orderItems.Find(i => i.ProductId == productId);

        if (item != null)
        {
            if (item.Quantity + quantity > existingProduct.Quantity) 
                throw new ArgumentException("Quantity exceeds quantity");
            item.Quantity += quantity;
        }
        else
        {
            var orderItem = new OrderItem
            {
                ProductId = productId,
                Quantity = quantity,
                Price =  existingProduct.Price
            };
            _orderItems.Add(orderItem);
        }
    }
    
    public Task<List<OrderItem>> GetCartAsync()
    {
        return Task.FromResult(_orderItems);
    }
    
    public Task ClearCartAsync()
    {
        _orderItems.Clear();
        return Task.CompletedTask;
    }

    public async Task<Order> CheckoutAsync()
    {
        if (_orderItems.Count == 0)
            throw  new ArgumentException("No order items found");

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Items = _orderItems.ToList()
        };

        foreach (var item in _orderItems)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);

            if (product == null)
                throw new ArgumentException("Product not found");
            
            product.Quantity -= item.Quantity;
            
            await _productRepository.UpdateAsync(product);
        }
        
        await _orderRepository.AddAsync(order);

        _orderItems.Clear();


        return order;
    }
}