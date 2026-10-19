namespace OrderService.Application.Features.Orders.Mutations.CreateOrder;

public class CreateOrderMutationInput(long unitId, int quantity)
{
    public long UnitId { get; set; } = unitId;
    public int Quantity { get; set; } =  quantity;
}   