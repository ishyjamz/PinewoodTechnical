using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;

namespace PinewoodTechFront.Pages.Views;

public class EditCustomer : PageModel
{
    private readonly ICustomerHttpService _apiService;

    [BindProperty]
    public Customer Customer { get; set; }
    
    public EditCustomer(ICustomerHttpService apiService)
    {
        _apiService = apiService;
    }

    // GET handler: Fetches the customer data by id
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Customer = await _apiService.GetCustomerAsync(id);

        if (Customer == null)
            return NotFound();

        return Page();
    }

    // POST handler: Updates the customer
    public async Task<IActionResult> OnPostAsync(int id)
    {
        // Check if the ModelState is valid
        if (!ModelState.IsValid)
        {
            return Page();  // Return the form if validation fails
        }

        var customerDto = new Customer()
        {
            Id = Customer.Id,
            Name = Customer.Name,
            Email = Customer.Email
        };
        
        // Call the API to update the customer
        var success = await _apiService.UpdateCustomerAsync(id, customerDto);
        
        if (!success) 
        {
            ModelState.AddModelError("", "Failed to update customer. Please try again.");
            return Page();  // Re-display the page with error message
        }

        // Redirect to the customer list page after successful update
        return RedirectToPage("/Views/Customers");
    }
}