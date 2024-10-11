using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;

namespace PinewoodTechFront.Pages.Views;

public class EditCustomer : PageModel
{
    private readonly ICustomerHttpService _apiService;
    public Customer Customer { get; set; }
    
    public EditCustomer(ICustomerHttpService apiService)
    {
        _apiService = apiService;
    }
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Customer = await _apiService.GetCustomerAsync(id);

        if (Customer == null)
            return NotFound();
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        await _apiService.UpdateCustomerAsync(id, Customer);
        return RedirectToPage("/Views/Customers");
    }
}