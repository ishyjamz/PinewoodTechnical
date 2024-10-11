using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PinewoodTechFront.Interfaces;
using PinewoodTechFront.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PinewoodTechFront.Services;

public class CustomerHttpService : ICustomerHttpService
{
    private readonly HttpClient _httpClient;
    private string _baseUrl = "http://localhost:5134/";

    // Constructor injection of HttpClient
    public CustomerHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CustomerApi");
    }

    // Get all customers
    public async Task<List<Customer>> GetCustomersAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl + "api/Customer");
        response.EnsureSuccessStatusCode();
    
        var jsonString = await response.Content.ReadAsStringAsync();
        var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonString);
    
        return customers;
    }


    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var response = _httpClient.GetAsync(_baseUrl + $"api/Customer/{id}");
        response.Result.EnsureSuccessStatusCode();
        
        var responseBody = await response.Result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Customer>(responseBody);
    }

    // Add a customer
    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(customer),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(_baseUrl + "api/Customer", content);
        response.EnsureSuccessStatusCode();
        
        return response.IsSuccessStatusCode;
    }
    
    // Update a customer
    public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(customer),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync($"api/customers/{id}", content);
        response.EnsureSuccessStatusCode();

        return response.IsSuccessStatusCode;

    }

    // Delete a customer
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/customers/{id}");
        return response.IsSuccessStatusCode;
    }
}
