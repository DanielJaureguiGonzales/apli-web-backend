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
    public class EquipamentController : ControllerBase
    {
        private readonly IEquipamentService _equipamentService;
        private readonly IMapper _mapper;

        public EquipamentController(IEquipamentService equipamentService, IMapper mapper)
        {
            _equipamentService = equipamentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Equipaments",
             Description = "List of Equipaments",
             OperationId = "ListAllEquipaments",
           Tags = new[] { "Equipaments" })]
        [SwaggerResponse(200, "List of Equipaments", typeof(IEnumerable<EquipamentResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EquipamentResource>), 200)]
        public async Task<IEnumerable<EquipamentResource>> GetAllAsync()
        {
            var equipaments = await _equipamentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Equipament>, IEnumerable<EquipamentResource>>(equipaments);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Post a Equipament",
             Description = "Post of Equipament",
             OperationId = "PostEquipament",
           Tags = new[] { "Equipaments" })]
        [SwaggerResponse(200, "Post of Equipament", typeof(EquipamentResource))]
        [HttpPost]
        [ProducesResponseType(typeof(EquipamentResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveEquipamentResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetMessages());
            }
            var equipament = _mapper.Map<SaveEquipamentResource, Equipament>(resource);
            var result = await _equipamentService.SaveAsync(equipament);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var equipamentResource = _mapper.Map<Equipament, EquipamentResource>(result.Resource);
            return Ok(equipamentResource);
        }

        [SwaggerOperation(
            Summary = "Put a Equipament",
             Description = "Put of Equipament",
             OperationId = "PutEquipament",
           Tags = new[] { "Equipaments" })]
        [SwaggerResponse(200, "Put of Equipament", typeof(EquipamentResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EquipamentResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEquipamentResource resource)
        {

            var equipament = _mapper.Map<SaveEquipamentResource, Equipament>(resource);
            var result = await _equipamentService.UpdateAsync(id, equipament);

            if (!result.Success)
                return BadRequest(result.Message);

            var equipamentResource = _mapper.Map<Equipament, EquipamentResource>(result.Resource);
            return Ok(equipamentResource);
        }

        [SwaggerOperation(
            Summary = "Delete a Equipament",
             Description = "Delete of Equipament",
             OperationId = "DeleteEquipament",
           Tags = new[] { "Equipaments" })]
        [SwaggerResponse(200, "Delete of Equipament", typeof(EquipamentResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EquipamentResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _equipamentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var equipamentResource = _mapper.Map< Equipament, EquipamentResource>(result.Resource);
            return Ok(equipamentResource);
        }
    }
}
