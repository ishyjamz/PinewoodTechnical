using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PinewoodTechBack.Shared.Dtos;
using PinewoodTechBack.Shared.Interfaces;
using PinewoodTechBack.Shared.Models;
using Microsoft.Extensions.Logging;

namespace PinewoodTechBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper,
            ILogger<CustomerController> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCustomers()
        {
            try
            {
                var customerList = _mapper.Map<List<CustomerDto>>(_customerRepository.GetCustomers());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(customerList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                if (!_customerRepository.CustomerExists(id))
                    return NotFound($"Customer with id {id} not found.");

                var customer = _mapper.Map<CustomerDto>(_customerRepository.GetCustomer(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the customer with ID {Id}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    _logger.LogWarning("Received null CustomerDto in AddCustomer.");
                    return BadRequest("CustomerDto cannot be null.");
                }

                if (_customerRepository.CustomerExists(customerDto.Id))
                {
                    ModelState.AddModelError("", "Customer with the name " + customerDto.Name + " already exists.");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerMap = _mapper.Map<Customer>(customerDto);

                if (!_customerRepository.AddCustomer(customerMap))
                {
                    ModelState.AddModelError("", "Could not save customer.");
                    return StatusCode(500, ModelState);
                }

                return StatusCode(201, $"Successfully added customer: {customerMap.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new customer.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDto customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    _logger.LogWarning("Received null CustomerDto in UpdateCustomer.");
                    return BadRequest("CustomerDto cannot be null.");
                }

                if (customerId != customerDto.Id)
                {
                    _logger.LogWarning("Customer ID mismatch in UpdateCustomer.");
                    return BadRequest("Customer ID mismatch.");
                }

                if (!_customerRepository.CustomerExists(customerId))
                {
                    return NotFound($"Customer with id {customerId} not found.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerMap = _mapper.Map<Customer>(customerDto);

                if (!_customerRepository.UpdateCustomer(customerMap))
                {
                    ModelState.AddModelError("", "Could not update customer.");
                    return StatusCode(500, ModelState);
                }

                return Ok($"Customer with id {customerId} successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the customer with ID {Id}.", customerId);
                return StatusCode(500, "Internal server error.");
            }
        }

        // DELETE handler: Deletes the customer
        [HttpDelete("{customerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                if (!_customerRepository.CustomerExists(customerId))
                {
                    return NotFound($"Customer with id {customerId} not found.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_customerRepository.DeleteCustomer(customerId))
                {
                    ModelState.AddModelError("", "Could not delete customer.");
                    return StatusCode(500, ModelState);
                }

                return Ok($"Customer with id {customerId} successfully deleted.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the customer with ID {Id}.", customerId);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
