using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;
using PinewoodTechFront.Services;

namespace PinewoodTechFront.Pages;

public class AddCustomer : PageModel
{
    private readonly ICustomerHttpService _apiService;
    
    [BindProperty]
    public Customer Customer { get; set; }

    public AddCustomer(ICustomerHttpService apiService)
    {
        _apiService = apiService;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        await _apiService.AddCustomerAsync(customer);
        return RedirectToPage("/Views/Customers");
    }
}