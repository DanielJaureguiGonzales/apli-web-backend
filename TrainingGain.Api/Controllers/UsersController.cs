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
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Extensions;
using TrainingGain.Api.Resources;

namespace TrainingGain.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary ="List all users",
            Description ="List of Users",
            OperationId ="ListAllUsers",
            Tags = new[] {"Users"})]
        [SwaggerResponse(200,"List of Users",typeof(IEnumerable<UserResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>),200)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        [SwaggerOperation(
              Summary = "Post a user",
              Description = "Post of User",
              OperationId = "PostUser",
              Tags = new[] { "Users" })]
        [SwaggerResponse(200, "Post of User", typeof(UserResource))]
        [HttpPost]
        [ProducesResponseType(typeof(UserResource), 200)]
        public async Task<IActionResult>PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
              Summary = "Put a user",
              Description = "Put of User",
              OperationId = "PutUser",
              Tags = new[] { "Users" })]
        [SwaggerResponse(200, "Put of User", typeof(UserResource))]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveUserResource resource)
        {
           
            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.UpdateAsync(id,user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
              Summary = "Delete a user",
              Description = "Delete of User",
              OperationId = "DeleteUser",
              Tags = new[] { "Users" })]
        [SwaggerResponse(200, "Delete of User", typeof(UserResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }
    }
}
