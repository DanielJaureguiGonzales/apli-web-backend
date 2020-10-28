using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Extensions;
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
   
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [SwaggerOperation(
                Summary = "List all customers",
                Description = "List of Customers",
                OperationId = "ListAllCustomers",
                Tags = new[] { "Customers" })]
        [SwaggerResponse(200, "List of Customers", typeof(IEnumerable<CustomerResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResource>), 200)]
        public async Task<IEnumerable<CustomerResource>> GetAllAsync()
        {
            var customers = await _customerService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a customer",
                Description = "Post of Customer",
                OperationId = "PostCustomer",
                Tags = new[] { "Customers" })]
        [SwaggerResponse(200, "Post of Customer", typeof(CustomerResource))]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var customers = _mapper.Map<SaveCustomerResource, Customer>(resource);
            var result = await _customerService.SaveAsync(customers);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Customer, CustomerResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
               Summary = "Put a customer",
               Description = "Put of Customer",
               OperationId = "PutCustomer",
               Tags = new[] { "Customers" })]
        [SwaggerResponse(200, "Put of Customer", typeof(CustomerResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCustomerResource resource)
        {

            var customers = _mapper.Map<SaveCustomerResource, Customer>(resource);
            var result = await _customerService.UpdateAsync(id, customers);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Customer, CustomerResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
               Summary = "Delete a customer",
               Description = "Delete of Customer",
               OperationId = "DeleteCustomer",
               Tags = new[] { "Customers" })]
        [SwaggerResponse(200, "Delete of Customer", typeof(CustomerResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _customerService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Customer, CustomerResource>(result.Resource);
            return Ok(userResource);
        }

    }
}
