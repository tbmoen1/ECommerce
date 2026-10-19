using OrderService.Domain.Entities;
using OrderService.Infrastructure.Contexts;

namespace OrderService.Application.Features.Orders.Mutations.CreateOrder;

[MutationType]
public class CreateOrderMutation
{
    public async Task<CreateOrderMutationPayload> CreateOrder(
        CreateOrderMutationInput input,
        [Service] DomainContext context)
    {
        var order = new Order(
            long.MaxValue, 
            input.UnitId,
            input.Quantity);
        
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        
        return new CreateOrderMutationPayload(order);
    }
}