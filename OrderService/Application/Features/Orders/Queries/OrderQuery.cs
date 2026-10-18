using OrderService.Domain.Entities;
using OrderService.Infrastructure.Contexts;

namespace OrderService.Application.Features.Orders.Queries;

[QueryType]
public class OrderQuery
{
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> Orders([Service] DomainContext context)
    {
        return context.Orders;
    }   
}