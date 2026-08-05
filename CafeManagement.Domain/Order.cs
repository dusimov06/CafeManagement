using System;

namespace CafeManagement.Domain;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
