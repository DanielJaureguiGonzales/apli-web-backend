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

        [HttpGet]
        public async Task<IEnumerable<SessionResource>> GetAllAsync()
        {
            var sessions = await _sessionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }

        [HttpGet("{tittle}")]
        public async Task<IEnumerable<SessionResource>> GetAllAsyncByTittle(string tittle)
        {
            var sessions = await _sessionService.ListAsyncByTittle(tittle);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }


        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSessionResource resource)
        {

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(id, session);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
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
