using PaymentService.Infrastructure.Payments.Stripe.Models;

namespace PaymentService.Application;

// Abstraction layer to support several payment providers later
public interface IPaymentProvider
{
    public Task<PaymentIntentDto?> CreatePaymentIntentAsync(CreatePaymentIntentDto dto);
}