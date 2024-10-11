using Microsoft.AspNetCore.Mvc.RazorPages;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;

namespace PinewoodTechFront.Pages.Views;

public class Customers : PageModel
{
    private readonly ICustomerHttpService _apiService;
    
    public List<Customer>? CustomerList { get; private set; }
    
    public Customers(ICustomerHttpService apiService)
    {
        this._apiService = apiService;
    }
    
    public async Task OnGetAsync()
    {
        var customerList = await _apiService.GetCustomersAsync();
        
        if (customerList != null)
        {
            CustomerList = customerList;
        }
    }
}