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
    public class TagSessionController : ControllerBase
    {
        private readonly ITagSessionService _tagSessionService;
        private readonly ITagService _tagService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public TagSessionController(ITagSessionService tagSessionService, ITagService tagService, ISessionService sessionService, IMapper mapper)
        {
            _tagSessionService = tagSessionService;
            _tagService = tagService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all TagSessions",
           Description = "List of TagSessions",
           OperationId = "ListTagSessions",
          Tags = new[] { "TagSessions" })]
        [SwaggerResponse(200, "List of TagSessions", typeof(IEnumerable<TagSessionResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TagSessionResource>), 200)]
        public async Task<IEnumerable<TagSessionResource>> GetAllAsync()
        {
            var tagSessions = await _tagSessionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<TagSession>, IEnumerable<TagSessionResource>>(tagSessions);
            return resources;
        }

        [SwaggerOperation(
                Summary = "Post a TagSession",
                Description = "Post of TagSession",
                OperationId = "PostTagSession",
                Tags = new[] { "TagSessions" })]
        [SwaggerResponse(200, "Post of TagSession", typeof(TagSessionResource))]
        [HttpPost]
        [ProducesResponseType(typeof(TagSessionResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTagSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var tagSession = _mapper.Map<SaveTagSessionResource, TagSession>(resource);
            var result = await _tagSessionService.AssignTagSessionAsync(tagSession.TagId, tagSession.SessionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var tagSessionResource = _mapper.Map<TagSession, TagSessionResource>(result.Resource);
            return Ok(tagSessionResource);
        } 
        [SwaggerOperation(
             Summary = "List Tags by Session",
            Description = "List of Tags for an specific Session",
            OperationId = "ListTagsBySession",
          Tags = new[] { "Sessions" })]
        [HttpGet("sessions/{sessionId}")]
        public async Task<IEnumerable<TagResource>> GetAllBySessionIdAsync(int sessionId)
        {
            var tags = await _tagService.ListBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
            return resources;
        }
        [SwaggerOperation(
            Summary = "List Sessions by Tag",
            Description = "List of Tags for an specific Session",
            OperationId = "ListTagsBySession",
          Tags = new[] { "Tags" })]
        [HttpGet("tags/{tagId}")]
        public async Task<IEnumerable<SessionResource>> GetAllByTagIdAsync(int tagId)   
        {
            var sessions = await _sessionService.ListByEquipamentIdAsync(tagId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }
    }
}
