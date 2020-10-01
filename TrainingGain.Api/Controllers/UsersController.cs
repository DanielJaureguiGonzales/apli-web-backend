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
        [SwaggerResponse(200,"OK",typeof(IEnumerable<UserResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>),200)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Find user by id",
            Description = "A specific user is returned",
            OperationId ="GetUser",
            Tags = new[] { "Users" })]
        [SwaggerResponse(200, "OK", typeof(UserResource))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userResource = _mapper.Map<User, UserResource>(user.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
           Summary = "User creation",
           Description = "A user is created in the system",
           OperationId ="CreateUser",
           Tags = new[] { "Users" })]
        [SwaggerResponse(200, "OK", typeof(SaveUserResource))]
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
          Summary = "Updating of user data",
          Description = "The specific user data is updated",
          OperationId = "UpdateUser",
          Tags = new[] { "Users" })]
        [SwaggerResponse(200, "OK", typeof(SaveUserResource))]
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
          Summary = "Delete User",
          Description = "The specific user data is deleted",
          OperationId = "DeleteUser",
          Tags = new[] { "Users" })]
        [SwaggerResponse(200, "OK", typeof(SaveUserResource))]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserResource),200)]
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
