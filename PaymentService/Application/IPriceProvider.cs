using PaymentService.Infrastructure.Prices.Stripe.Models;

namespace PaymentService.Application;

// Abstraction layer to support several price providers later
public interface IPriceProvider
{
    public Task<PriceDto?> CreatePrice(CreatePriceDto dto);
}
