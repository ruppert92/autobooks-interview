using AutoMapper;
using GroceryStore.API.Models;
using GroceryStore.BLL.DTOs;
using GroceryStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint to get all customers of the grocery store
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>All customers of the grocery store</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<CustomerDTO>), 200)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var customers = await _customersService.GetAll(cancellationToken);
            return Ok(customers);
        }

        /// <summary>
        /// Endpoint to get customer by id
        /// </summary>
        /// <param name="customerId">Customers Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Customer if found, or Not Found response</returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _customersService.GetById(customerId, cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// Endpoint to add a customer to the grocery store
        /// </summary>
        /// <param name="addCustomerRequest">New customers info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>201 response with new customer's info</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(CustomerDTO), 201)]
        public async Task<IActionResult> Post(AddCustomerRequest addCustomerRequest, CancellationToken cancellationToken = default)
        {
            var customerDto = _mapper.Map<CustomerDTO>(addCustomerRequest);
            var customer = await _customersService.Add(customerDto, cancellationToken);
            return CreatedAtAction(nameof(Get), new { customerId = customer.Id }, customer);
        }

        /// <summary>
        /// Endpoint to update a customer
        /// </summary>
        /// <param name="customerId">Customer to update's Id</param>
        /// <param name="customerDto">Updated customer info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>204 response, or 400 if Ids don't match</returns>
        [HttpPut("{customerId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Put(int customerId, CustomerDTO customerDto, CancellationToken cancellationToken = default)
        {
            if(customerId != customerDto.Id)
            {
                return BadRequest("Id does not match");
            }
            await _customersService.Update(customerDto, cancellationToken);
            return NoContent();
        }
    }
}
