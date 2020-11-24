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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ICustomerService _customerService;
        private readonly ISpecialistService _specialistService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper, ICustomerService customerService, ISpecialistService specialistService)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _customerService = customerService;
            _specialistService = specialistService;
        }
        [SwaggerOperation(
           Summary = "List all Reviews",
           Description = "List of Reviews",
           OperationId = "ListReviews",
           Tags = new[] { "Reviews" })]
        [SwaggerResponse(200, "List of Reviews", typeof(IEnumerable<ReviewResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReviewResource>), 200)]
        public async Task<IEnumerable<ReviewResource>> GetAllAsync()
        {
            var reviews = await _reviewService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewResource>>(reviews);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Post a Review",
                Description = "Post of Review",
                OperationId = "PostReview",
           Tags = new[] { "Reviews" })]
        [SwaggerResponse(200, "Post of Review", typeof(ReviewResource))]
        [HttpPost]
        [ProducesResponseType(typeof(ReviewResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveReviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var review = _mapper.Map<SaveReviewResource, Review>(resource); 
            var result = await _reviewService.AssignReviewAsync(review.CustomerId, review.SpecialistId, review.Description, review.Rank);

            if (!result.Success)
                return BadRequest(result.Message);

            var historyResource = _mapper.Map<Review, ReviewResource>(result.Resource);
            return Ok(historyResource);
        }
        [SwaggerOperation(
            Summary = "List Specialist by customer",
            Description = "List of Specialist for an specific customer",
            OperationId = "ListSpecialistByCustomer",
           Tags = new[] { "Customers" })]
        [HttpGet("customers/{customerId}")]
        public async Task<IEnumerable<SpecialistResource>> GetAllByCustomerIdAsync(int customerId)
        {
            var specialists = await _specialistService.ListByCustomerIdAsync(customerId);
            var resources = _mapper.Map<IEnumerable<Specialist>, IEnumerable<SpecialistResource>>(specialists);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List Customer by Specialist",
            Description = "List of Customer for an specific Specialist",
            OperationId = "ListCustomerBySpecialist",
          Tags = new[] { "Specialists" })]  
        [HttpGet("specialists/{specialistId}")]
        public async Task<IEnumerable<CustomerResource>> GetAllBySpecialistIdAsync(int specialistId)    
        {
            var customers = await _customerService.ListBySpecialistIdAsync(specialistId);
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }
    }
}
