using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Domain.Entities;

public class Order(long id, long productId, int quantity)
{
    public long Id { get; init; } = id;
    public long UnitId { get; init; } = productId;
    public int Quantity { get; init; } = quantity;
    public OrderState State { get; private set; } = OrderState.Pending;
    public CanceledReasons? CanceledReason { get; set; }

    public void SetOrderState(OrderState state)
    {
        if (state == OrderState.Canceled)
            throw new DomainException("Use SetCanceledOrderState instead");
        State = state;
    }
    
    public void SetCanceledOrderState(CanceledReasons reason)
    {
        State = OrderState.Canceled;
        CanceledReason = reason;
    }
}