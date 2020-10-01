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

        [HttpGet]
        public async Task<IEnumerable<SpecialistResource>> GetAllAsync()
        {
            var specialists = await _specialistService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Specialist>, IEnumerable<SpecialistResource>>(specialists);
            return resources;
        }


        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSpecialistResource resource)
        {

            var specialist = _mapper.Map<SaveSpecialistResource, Specialist>(resource);
            var result = await _specialistService.UpdateAsync(id, specialist);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
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
