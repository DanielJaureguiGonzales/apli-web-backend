using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test
{
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUsersReturnsEmptyCollection()
        {
           
            var mockUserRepository = GetDefaultICategoryRepositoryInstance();
            mockUserRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<User>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(
                mockUserRepository.Object,
                mockUnitOfWork.Object);
            
            List<User> result =(List<User>) await service.ListAsync();
            int usersCount = result.Count;
           
            usersCount.Should().Equals(0);

        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserNotFoundResponse()
        {
            var mockUserRepository = GetDefaultICategoryRepositoryInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId)).Returns(Task.FromResult<User>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(
              mockUserRepository.Object,
              mockUnitOfWork.Object);

            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;

            message.Should().Be("User not found");

        }

        private  Mock<IUserRepository> GetDefaultICategoryRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private  Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}