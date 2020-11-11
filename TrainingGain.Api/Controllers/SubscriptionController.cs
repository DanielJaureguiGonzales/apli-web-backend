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
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        private readonly ICustomerService _customerService; 
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper, ISubscriptionPlanService subscriptionPlanService, ICustomerService customerService)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
            _subscriptionPlanService = subscriptionPlanService;
            _customerService = customerService;
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
            var subscriptions = await _subscriptionService.ListAsync();
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
            var result = await _subscriptionService.AssignSubscriptionAsync(subscriptions.CustomerId,subscriptions.SubscriptionPlanId,subscriptions.StartDate,subscriptions.ExpiryDate);

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
        [HttpDelete]
        [ProducesResponseType(typeof(SubscriptionResource), 200)]
        public async Task<IActionResult> DeleteAsync([FromBody] SaveSubscriptionResource resource)
        {

            var result = await _subscriptionService.UnassignSubscriptionAsync(resource.CustomerId, resource.SubscriptionPlanId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionsResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscriptionsResource);
        }

        [SwaggerOperation(
            Summary = "List subscriptionPlans by customer",
            Description = "List of sessions for an specific customer",
            OperationId = "ListSubscriptionPlansBycustomer",
            Tags = new[] { "Customers" })]
        [HttpGet("customers/{customerId}")]
        public async Task<IEnumerable<SubscriptionPlanResource>> GetAllByCustomerIdAsync(int customerId)
        {
            var subscriptionPlans = await _subscriptionPlanService.ListByCustomerIdAsync(customerId);
            var resources = _mapper.Map<IEnumerable<SubscriptionPlan>, IEnumerable<SubscriptionPlanResource>>(subscriptionPlans);
            return resources;
        }
        [SwaggerOperation(
            Summary = "List customers by subscriptionPlan",
            Description = "List of customers for an specific subscriptionPlan",
            OperationId = "ListCustomersBysubscriptionPlan",
          Tags = new[] { "SubscriptionPlans" })]
        [HttpGet("plans/{subscriptionPlanId}")]
        public async Task<IEnumerable<CustomerResource>> GetAllBySubscriptionPlanIdAsync(int subscriptionPlanId)
        {
            var customers = await _customerService.ListBySubscriptionPlanId(subscriptionPlanId);
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }

    }
}
