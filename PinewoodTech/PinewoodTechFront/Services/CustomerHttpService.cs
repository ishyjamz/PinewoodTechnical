using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;

namespace PinewoodTechFront.Services;

public class CustomerHttpService : ICustomerHttpService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "http://localhost:5134/";

    // Constructor injection of HttpClient
    public CustomerHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CustomerApi");
    }

    // Get all customers
    public async Task<List<Customer>?> GetCustomersAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl + "api/Customer");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Customer>>(jsonString);
    }

    // Get a specific customer
    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var response = await _httpClient.GetAsync(_baseUrl + $"api/Customer/{id}");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Customer>(responseBody);
    }

    // Add a customer
    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(customer),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(_baseUrl + "api/Customer", content);
        response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;
    }

    // Update a customer
    public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            throw new InvalidOperationException("Customer ID mismatch.");
        }
        
        var content = new StringContent(
            JsonConvert.SerializeObject(customer),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync(_baseUrl + $"api/Customer/{id}", content);
        response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;
    }
}
