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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly ICustomerService _customerService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public HistoryController(IHistoryService historyService, IMapper mapper, ICustomerService customerService, ISessionService sessionService)
        {
            _historyService = historyService;
            _mapper = mapper;
            _customerService = customerService;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IEnumerable<HistoryResource>> GetAllAsync()
        {
            var histories = await _historyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<History>, IEnumerable<HistoryResource>>(histories);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var history = _mapper.Map<SaveHistoryResource, History>(resource);
            var result = await _historyService.AssignHistoryAsync(history.CustomerId, history.SessionId,history.Watched);

            if (!result.Success)
                return BadRequest(result.Message);

            var historyResource = _mapper.Map<History, HistoryResource>(result.Resource);
            return Ok(historyResource);
        }

        [SwaggerOperation(
           Tags = new[] { "Customers" })]
        [HttpGet("customers/{customerId}")]
        public async Task<IEnumerable<SessionResource>> GetAllByCustomerIdAsync(int customerId)
        {
            var sessions = await _sessionService.ListByCustomerIdAsync(customerId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }

        [SwaggerOperation(
          Tags = new[] { "Sessions" })]
        [HttpGet("sessions/{sessionId}")]
        public async Task<IEnumerable<CustomerResource>> GetAllBySessionIdAsync(int sessionId)  
        {
            var customers = await _customerService.ListBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }

    }
}
