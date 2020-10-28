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
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly IMapper _mapper;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService, IMapper mapper)
        {
            _subscriptionPlanService = subscriptionPlanService;
            _mapper = mapper;
        }

        [SwaggerOperation(
               Summary = "List all subscriptionPlans",
               Description = "List of SubscriptionPlans",
               OperationId = "ListAllSubscriptionPlans",
               Tags = new[] { "SubscriptionPlans" })]
        [SwaggerResponse(200, "List of SubscriptionPlan", typeof(IEnumerable<SubscriptionPlanResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionPlanResource>), 200)]
        public async Task<IEnumerable<SubscriptionPlanResource>> GetAllAsync()
        {
            var subscriptionPlans = await _subscriptionPlanService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SubscriptionPlan>,IEnumerable<SubscriptionPlanResource>>(subscriptionPlans);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a subscriptionPlan",
                Description = "Post of SubscriptionPlan",
                OperationId = "PostSubscriptionPlan",
                Tags = new[] { "SubscriptionPlans" })]
        [SwaggerResponse(200, "Post of Specialists", typeof(SpecialistResource))]
        [HttpPost]
        [ProducesResponseType(typeof(SpecialistResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionPlanResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetMessages());
            }
            var subscriptionPlan = _mapper.Map<SaveSubscriptionPlanResource, SubscriptionPlan>(resource);
            var result = await _subscriptionPlanService.SaveAsync(subscriptionPlan);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var subscriptionPlanResource = _mapper.Map<SubscriptionPlan, SubscriptionPlanResource>(result.Resource);
            return Ok(subscriptionPlanResource);
        }
   
        [SwaggerOperation(
                Summary = "Put a subscriptionPlan",
                Description = "Put of SubscriptionPlan",
                OperationId = "PutSubscriptionPlan",
                Tags = new[] { "SubscriptionPlans" })]
        [SwaggerResponse(200, "Put of SubscriptionPlan", typeof(SubscriptionPlanResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SubscriptionPlanResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubscriptionPlanResource resource)
        {

            var subscriptionPlan = _mapper.Map<SaveSubscriptionPlanResource, SubscriptionPlan>(resource);
            var result = await _subscriptionPlanService.UpdateAsync(id, subscriptionPlan);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionPlanResource = _mapper.Map<SubscriptionPlan, SubscriptionPlanResource>(result.Resource);    
            return Ok(subscriptionPlanResource);
        }
      

        [SwaggerOperation(
              Summary = "Delete a subscriptionPlan",
              Description = "Delete of SubscriptionPlan",
              OperationId = "DeleteSubscriptionPlan",
              Tags = new[] { "SubscriptionPlans" })]
        [SwaggerResponse(200, "Delete of SubscriptionPlan", typeof(SubscriptionPlanResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SubscriptionPlanResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _subscriptionPlanService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<SubscriptionPlan, SubscriptionPlanResource>(result.Resource);
            return Ok(userResource);
        }
    }
}
