using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Orders.Mutations.CreateOrder;

public class CreateOrderMutationPayload(Order order)
{
    public Order Order { get; set; } = order;
}