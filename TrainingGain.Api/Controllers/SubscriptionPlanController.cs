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

        [HttpGet]
        public async Task<IEnumerable<SubscriptionPlanResource>> GetAllAsync()
        {
            var subscriptionPlans = await _subscriptionPlanService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SubscriptionPlan>,IEnumerable<SubscriptionPlanResource>>(subscriptionPlans);
            return resources;
        }

        [HttpPost]
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
    }
}
