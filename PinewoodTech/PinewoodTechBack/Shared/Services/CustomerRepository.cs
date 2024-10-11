using PinewoodTechBack.Shared.Interfaces;
using PinewoodTechBack.Shared.Models;
using Microsoft.Extensions.Logging;

namespace PinewoodTechBack.Shared.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(CustomerDbContext dbContext, ILogger<CustomerRepository> logger)
    {
        this._dbContext = dbContext;
        this._logger = logger;
    }

    public bool AddCustomer(Customer customer)
    {
        if (customer == null)
        {
            _logger.LogWarning("Attempted to add a null customer.");
            return false;
        }

        try
        {
            _dbContext.Customers.Add(customer);
            return Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a new customer.");
            return false;
        }
    }

    public bool UpdateCustomer(Customer customer)
    {
        if (customer == null)
        {
            _logger.LogWarning("Attempted to update a null customer.");
            return false;
        }

        try
        {
            _dbContext.Customers.Update(customer);
            return Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating the customer with ID {Id}.", customer.Id);
            return false;
        }
    }

    public Customer GetCustomer(int id)
    {
        try
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {Id} not found.", id);
                throw new InvalidOperationException($"Customer with ID {id} not found.");
            }

            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving the customer with ID {Id}.", id);
            throw;
        }
    }

    public List<Customer> GetCustomers()
    {
        try
        {
            return _dbContext.Customers.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving the customer list.");
            return new List<Customer>();  // Return an empty list on failure
        }
    }

    public bool CustomerExists(int id)
    {
        try
        {
            return _dbContext.Customers.Any(c => c.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if customer with ID {Id} exists.", id);
            return false;
        }
    }
    
    public bool DeleteCustomer(int customerId)
    {
        var customer = GetCustomer(customerId);
        
        try
        {
            _dbContext.Customers.Remove(customer);
            return Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting the customer with ID {Id}.", customer.Id);
            return false;
        }
    }

    private bool Save()
    {
        try
        {
            return _dbContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while saving changes to the database.");
            return false;
        }
    }
}
