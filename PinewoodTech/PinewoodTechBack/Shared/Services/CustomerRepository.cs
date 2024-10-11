using PinewoodTechBack.Shared.Interfaces;
using PinewoodTechBack.Shared.Models;
namespace PinewoodTechBack.Shared.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;
    public CustomerRepository(CustomerDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public bool AddCustomer(Customer customer)
    {
        if (customer != null)
        {
            _dbContext.Customers.Add(customer);
        }

        return this.Save();
    }

    public bool UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Customer GetCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public List<Customer> GetCustomers()
    {
        return _dbContext.Customers.ToList();
    }

    public bool CustomerExists(int id)
    {
        if(this._dbContext.Customers.Any(c => c.Id == id))
        {
            return true;
        }

        return false;
    }

    private bool Save()
    {
        var save = _dbContext.SaveChanges();
        return save > 0 ? true : false;
    }
    
}