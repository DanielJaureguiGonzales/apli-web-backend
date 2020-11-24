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

    public class SpecialistsController : ControllerBase
    {
        private readonly ISpecialistService _specialistService;
        private readonly IMapper _mapper;

        public SpecialistsController(ISpecialistService specialistService, IMapper mapper)
        {
            _specialistService = specialistService;
            _mapper = mapper;
        }
        [SwaggerOperation(
                Summary = "List all specialists",    
                Description ="List of Specialists",
                OperationId ="ListAllSpecialists",
                Tags = new [] {"Specialists"})]
        [SwaggerResponse(200,"List of Specialists",typeof(IEnumerable<SpecialistResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpecialistResource>), 200)]
        public async Task<IEnumerable<SpecialistResource>> GetAllAsync()
        {
            var specialists = await _specialistService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Specialist>, IEnumerable<SpecialistResource>>(specialists);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a specialist",
                Description = "Post of Specialist",
                OperationId = "PostSpecialist",
                Tags = new[] { "Specialists" })]
        [SwaggerResponse(200, "Post of Specialists", typeof(SpecialistResource))]
        [HttpPost]
        [ProducesResponseType(typeof(SpecialistResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSpecialistResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var specialist = _mapper.Map<SaveSpecialistResource, Specialist>(resource);
            var result = await _specialistService.SaveAsync(specialist);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
               Summary = "Put a specialist",
               Description = "Put of Specialist",
               OperationId = "PutSpecialist",
               Tags = new[] { "Specialists" })]
        [SwaggerResponse(200, "Put of Specialist", typeof(SpecialistResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SpecialistResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSpecialistResource resource)
        {

            var specialist = _mapper.Map<SaveSpecialistResource, Specialist>(resource);
            var result = await _specialistService.UpdateAsync(id, specialist);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
              Summary = "Delete a specialist",
              Description = "Delete of Specialist",
              OperationId = "DeleteSpecialist",
              Tags = new[] { "Specialists" })]
        [SwaggerResponse(200, "Delete of Specialist", typeof(SpecialistResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SpecialistResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _specialistService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
            return Ok(userResource);
        }

    }
}
