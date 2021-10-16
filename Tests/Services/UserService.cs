using API.Controllers;
using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using Infrastructre.Services.Users;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests
{
    public class UserServiceTest
    {
        #region Property  
        public Mock<ILogger<UsersController>> logger = new();
        public Mock<IAsyncRepository<User>> userRepository = new();
        public Mock<IUnitOfWork> unitOfWork = new();
        #endregion

       [Fact]
        public async void GetUserbyId()
        {
            var userInfo = new AddUserResponse
            {
                Id = 1,
                UserName="name"
            };
            var user = new User("name", "email", "address", "password", 1);
            userRepository.Setup(p => p.GetAsyncById(1)).ReturnsAsync(user);
            unitOfWork.Setup(x => x.AsyncRepository<User>()).Returns(userRepository.Object);
            UserService userService = new(unitOfWork.Object);
            var result = await userService.GetUser(1);
           
       


        }
    }
}
