using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IEnumerable<CustomerResource>> GetAllAsync()
        {
            var customers = await _customerService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }


        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCustomerResource resource)
        {

            var customers = _mapper.Map<SaveCustomerResource, Customer>(resource);
            var result = await _customerService.UpdateAsync(id, customers);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Customer, CustomerResource>(result.Resource);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
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
