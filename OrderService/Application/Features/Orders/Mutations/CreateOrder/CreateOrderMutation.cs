using ECommerce.IdGeneration;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Contexts;

namespace OrderService.Application.Features.Orders.Mutations.CreateOrder;

[MutationType]
public class CreateOrderMutation
{
    public async Task<CreateOrderMutationPayload> CreateOrder(
        CreateOrderMutationInput input,
        [Service] IInventoryRepository inventoryService,
        [Service] DomainContext context,
        [Service] IIdGenerator idGenerator)
    {
        var order = new Order(
            idGenerator.NextId(), 
            input.UnitId,
            input.Quantity);
        
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        try
        {
            await inventoryService.ReserveUnitAsync(input.UnitId, input.Quantity);
        }
        catch (HttpRequestException e)
        {
            throw new GraphQLException(e.Message, e);
        }
        
        return new CreateOrderMutationPayload(order);
    }
}