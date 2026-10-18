namespace OrderService.Domain.Entities;

public class Order
{
    public long Id { get; set; }

    public Order(long id)
    {
        Id = id;
    }
}