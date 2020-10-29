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
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService; 
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all subscriptions",
           Description = "List of subscriptions",
           OperationId = "ListSubscriptions",
           Tags = new[] { "Subscriptions" })]
        [SwaggerResponse(200, "List of subscriptions", typeof(IEnumerable<SubscriptionResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionResource>), 200)]
        public async Task<IEnumerable<SubscriptionResource>> GetAllAsync()
        {
            var subscriptions = await _subscriptionService.ListAsyn();
            var resources = _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionResource>>(subscriptions);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a subscription",
                Description = "Post of subscription",
                OperationId = "PostSubscription",
                Tags = new[] { "Subscriptions" })]
        [SwaggerResponse(200, "Post of subscription", typeof(SubscriptionResource))]
        [HttpPost]
        [ProducesResponseType(typeof(SubscriptionResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var subscriptions = _mapper.Map<SaveSubscriptionResource, Subscription>(resource);
            var result = await _subscriptionService.AssignSubscriptionAsync(subscriptions.CustomerId,subscriptions.SubscriptionPlanId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionsResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscriptionsResource);
        }

        [SwaggerOperation(
              Summary = "Delete a subscription",
              Description = "Delete of subscription",
              OperationId = "DeleteSubscription",
              Tags = new[] { "Subscriptions" })]
        [SwaggerResponse(200, "Delete of subscription", typeof(SubscriptionResource))]
        [HttpDelete("customer/{customerid}/subscriptionplan/{subscriptionplanid}")]
        [ProducesResponseType(typeof(SubscriptionResource), 200)]
        public async Task<IActionResult> DeleteAsync(int customerid,int subscriptionplanid)
        {

            var result = await _subscriptionService.UnassignSubscriptionAsync(customerid, subscriptionplanid);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionsResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscriptionsResource);
        }

    }
}
