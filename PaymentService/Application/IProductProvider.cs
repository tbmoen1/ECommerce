using PaymentService.Infrastructure.Products.Stripe.Models;

namespace PaymentService.Application;

// Abstraction layer to support several product providers later
public interface IProductProvider
{
    public Task<ProductDto?> CreateProduct(CreateProductDto dto);
}
