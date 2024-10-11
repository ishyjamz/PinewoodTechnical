using Microsoft.EntityFrameworkCore;
using PinewoodTechBack.Shared.Models;

namespace PinewoodTechBack;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}
