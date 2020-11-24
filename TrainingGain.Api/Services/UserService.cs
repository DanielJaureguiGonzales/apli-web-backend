using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Settings;

namespace TrainingGain.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingCategory = await _userRepository.FindById(id);

            if (existingCategory == null)
                return new UserResponse("User not found");

            try
            {
                _userRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("user not found");

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Description = user.Description;
            existingUser.Birth = user.Birth;
            existingUser.Address = user.Address;
            existingUser.Phone = user.Phone;
            existingUser.Age = user.Age;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Country = user.Country;
            existingUser.Gender = user.Gender;
           
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating user: {ex.Message}");
            }
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var users = _userRepository.GetAll();
            var user = users.SingleOrDefault(x =>
                  x.Email == request.Email &&
                  x.Password == request.Password
                );
            if (user == null)
            {
                return null;
            }
            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
