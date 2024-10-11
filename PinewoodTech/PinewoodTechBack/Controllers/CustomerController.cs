using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PinewoodTechBack.Shared.Dtos;
using PinewoodTechBack.Shared.Interfaces;
using PinewoodTechBack.Shared.Models;

namespace PinewoodTechBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public IActionResult GetCustomers()
    {
        var customerList = _mapper.Map<List<CustomerDto>>(_customerRepository.GetCustomers());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(customerList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public IActionResult GetCustomer(int id)
    {
        if (!_customerRepository.CustomerExists(id))
            return NotFound();
        
        var customer = _mapper.Map<CustomerDto>(_customerRepository.GetCustomer(id));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddCustomer([FromBody] CustomerDto customerDto)
    {
        if (customerDto == null)
        {
            return BadRequest(ModelState);
        }
        
        if (_customerRepository.CustomerExists(customerDto.Id))
        {
            ModelState.AddModelError("", "Customer with the name " 
                                         + customerDto.Name + " already exists");
            return StatusCode(422, ModelState);
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var customerMap = _mapper.Map<Customer>(customerDto);

        if (!_customerRepository.AddCustomer(customerMap))
        {
            ModelState.AddModelError("", "Did not save");
            return StatusCode(422, ModelState);
        }

        return Ok("Successfully added customer named: " + customerMap.Name);
    }

    [HttpPut("{customerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDto customerDto)
    {
        if (customerDto == null)
            return BadRequest(ModelState);

        if (customerId != customerDto.Id)
            return BadRequest(ModelState);

        if (!_customerRepository.CustomerExists(customerId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var customerMap = _mapper.Map<Customer>(customerDto);

        if (!_customerRepository.UpdateCustomer(customerMap))
        {
            ModelState.AddModelError("", "Problem Saving...");
            return StatusCode(500, ModelState);
        }

        return Ok("Customer updated");
    }
}