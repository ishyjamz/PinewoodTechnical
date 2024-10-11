using PinewoodTechBack.Shared.Models;

namespace PinewoodTechBack.Shared.Interfaces;

public interface ICustomerRepository
{
    public bool AddCustomer(Customer customer);
    
    public bool UpdateCustomer(Customer customer);
    
    public Customer GetCustomer(int id);
    
    public List<Customer> GetCustomers();

    public bool CustomerExists(int id);
    
    public bool DeleteCustomer(int customerId);
}