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

    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
                Summary = "List all sessions",
                Description = "List of Sessions",
                OperationId = "ListAllSessions",
                Tags = new[] { "Sessions" })]
        [SwaggerResponse(200, "List of Sessions", typeof(IEnumerable<SessionResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllAsync()
        {
            var sessions = await _sessionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }

        [SwaggerOperation(
                Summary = "List all sessions by tittle",
                Description = "List of Session by tittle",
                OperationId = "ListAllSessionByTittle",
                Tags = new[] { "Sessions" })]
        [SwaggerResponse(200, "List of Session by tittle", typeof(IEnumerable<SessionResource>))]
        [HttpGet("{tittle}")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllAsyncByTittle(string tittle)
        {
            var sessions = await _sessionService.ListAsyncByTittle(tittle);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a session",
                Description = "Post of Session",
                OperationId = "PostSession",
                Tags = new[] { "Sessions" })]
        [SwaggerResponse(200, "Post of Session", typeof(SessionResource))]
        [HttpPost]
        [ProducesResponseType(typeof(SessionResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.SaveAsync(session);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
                Summary = "Put a session",
                Description = "Put of Session",
                OperationId = "PutSession",
                Tags = new[] { "Sessions" })]
        [SwaggerResponse(200, "Put of Session", typeof(SessionResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSessionResource resource)
        {

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(id, session);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
              Summary = "Delete a session",
              Description = "Delete of Session",
              OperationId = "DeleteSession",
              Tags = new[] { "Sessions" })]
        [SwaggerResponse(200, "Delete of Session", typeof(SessionResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _sessionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(userResource);
        }
    }
}
