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
    public class EquipamentSessionController : ControllerBase
    {
        private readonly IEquipamentSessionService _equipamentSessionService;
        private readonly IEquipamentService _equipamentService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public EquipamentSessionController(IEquipamentSessionService equipamentSessionService, IEquipamentService equipamentService, ISessionService sessionService, IMapper mapper)
        {
            _equipamentSessionService = equipamentSessionService;
            _equipamentService = equipamentService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all EquipamentSessions",
           Description = "List of EquipamentSessions",
           OperationId = "ListEquipamentSessions",
           Tags = new[] { "EquipamentSessions" })]
        [SwaggerResponse(200, "List of EquipamentSessions", typeof(IEnumerable<EquipamentSessionResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EquipamentSessionResource>), 200)]
        public async Task<IEnumerable<EquipamentSessionResource>> GetAllAsync()
        {
            var equipamentSessions = await _equipamentSessionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<EquipamentSession>, IEnumerable<EquipamentSessionResource>>(equipamentSessions);
            return resources;
        }

        [SwaggerOperation(
             Summary = "Post a EquipamentSession",
             Description = "Post of EquipamentSession",
             OperationId = "PostSubscription",
          Tags = new[] { "EquipamentSessions" })]
        [SwaggerResponse(200, "Post of EquipamentSession", typeof(SubscriptionResource))]
        [HttpPost]
        [ProducesResponseType(typeof(SubscriptionResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveEquipamentSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var equipamentSession = _mapper.Map<SaveEquipamentSessionResource, EquipamentSession>(resource);
            var result = await _equipamentSessionService.AssignEquipamentSessionAsync(equipamentSession.EquipamentId, equipamentSession.SessionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var equipamentSessionResource = _mapper.Map<EquipamentSession, EquipamentSessionResource>(result.Resource);
            return Ok(equipamentSessionResource);
        }

        [SwaggerOperation(
             Summary = "Delete a EquipamentSession",
             Description = "Delete of EquipamentSession",
             OperationId = "DeleteEquipamentSession",
          Tags = new[] { "EquipamentSessions" })]
        [SwaggerResponse(200, "Delete of EquipamentSession", typeof(EquipamentSessionResource))]
        [HttpDelete]
        [ProducesResponseType(typeof(EquipamentSessionResource), 200)]
        public async Task<IActionResult> DeleteAsync([FromBody] SaveEquipamentSessionResource resource)
        {
            var result = await _equipamentSessionService.UnassignEquipamentSessionAsync(resource.EquipamentId, resource.SessionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionsResource = _mapper.Map<EquipamentSession, EquipamentSessionResource>(result.Resource);
            return Ok(subscriptionsResource);
        }

        [SwaggerOperation(
            Summary = "List Equipaments by Session",
            Description = "List of Equipaments for an specific Session",
            OperationId = "ListEquipamentsBySession",
           Tags = new[] { "Sessions" })]
        [HttpGet("sessions/{sessionId}")]
        public async Task<IEnumerable<EquipamentResource>> GetAllBySessionIdAsync(int sessionId)
        {
            var equipaments = await _equipamentService.ListBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<Equipament>, IEnumerable<EquipamentResource>>(equipaments);
            return resources;
        }
        [SwaggerOperation(
            Summary = "List Session by Equipaments",
            Description = "List of Session for an specific Equipaments",
            OperationId = "ListSessionByEquipaments",
          Tags = new[] { "Equipaments" })]
        [HttpGet("equipaments/{equipamentId}")]
        public async Task<IEnumerable<SessionResource>> GetAllByEquipamentIdAsync(int equipamentId)
        {
            var sessions = await _sessionService.ListByEquipamentIdAsync(equipamentId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }
    }
}
