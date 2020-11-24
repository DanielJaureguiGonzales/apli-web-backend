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
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/specialists/{specialistId}/sessions")]
    public class SpecialistSessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SpecialistSessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary ="List sessions by specialist",
            Description ="List of sessions for an specific specialist",
            OperationId ="ListSessionsBySpecialist",
            Tags = new[] { "Specialists" })]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>),200)]
        public async Task<IEnumerable<SessionResource>> GetAllBySpecialistIdAsync(int specialistId)
        {
            var sessions = await _sessionService.ListBySpecialistIdAsync(specialistId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }
    }
}
