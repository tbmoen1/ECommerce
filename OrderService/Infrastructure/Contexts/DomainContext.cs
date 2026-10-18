using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Contexts;

public class DomainContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}