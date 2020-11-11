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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }
        [SwaggerOperation(
             Summary = "List all Tags",
             Description = "List of Tags",
             OperationId = "ListAllTag",
           Tags = new[] { "Tags" })]
        [SwaggerResponse(200, "List of Tags", typeof(IEnumerable<TagResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TagResource>), 200)]
        public async Task<IEnumerable<TagResource>> GetAllAsync()
        {
            var tags = await _tagService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Post a Tag",
             Description = "Post of Tag",
             OperationId = "PostTag",
           Tags = new[] { "Tags" })]
        [SwaggerResponse(200, "Post of Tag", typeof(TagResource))]
        [HttpPost]
        [ProducesResponseType(typeof(TagResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTagResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetMessages());
            }
            var tag = _mapper.Map<SaveTagResource, Tag>(resource);
            var result = await _tagService.SaveAsync(tag);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource);
            return Ok(tagResource);
        }

        [SwaggerOperation(
            Summary = "Put a Tag",
             Description = "Put of Tag",
             OperationId = "PutTag",
           Tags = new[] { "Tags" })]
        [SwaggerResponse(200, "Put of Tag", typeof(TagResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TagResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTagResource resource)
        {

            var tag = _mapper.Map<SaveTagResource, Tag>(resource);
            var result = await _tagService.UpdateAsync(id, tag);

            if (!result.Success)
                return BadRequest(result.Message);

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource);
            return Ok(tagResource);
        }

        [SwaggerOperation(
            Summary = "Delete a Tag",
             Description = "Delete of Tag",
             OperationId = "DeleteTag",
           Tags = new[] { "Tags" })]
        [SwaggerResponse(200, "Delete of Tag", typeof(TagResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TagResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _tagService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource);
            return Ok(tagResource);
        }
    }
}
