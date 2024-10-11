using PinewoodTechFront.Models;

namespace PinewoodTechFront.Interfaces;

public interface ICustomerHttpService
{
    public Task<List<Customer>?> GetCustomersAsync();
    public Task<Customer?> GetCustomerAsync(int id);
    
    public Task<bool> AddCustomerAsync(Customer customer);
    
    public Task<bool> UpdateCustomerAsync(int id,Customer customer);
}