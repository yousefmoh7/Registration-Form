using API.Controllers;
using Domain.DTOs.Users;
using Infrastructre.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests
{
    public class UserTest
    {
        #region Property  
        public Mock<IUserService> userService = new();
        public Mock<ILogger<UsersController>> logger = new();

        #endregion

        [Fact]
        public async void GetUserbyId()
        {
            var userInfo = new UserInfo
            {
                Id = 1,
                Name = "name"
            };
            userService.Setup(p => p.GetUser(1)).ReturnsAsync(userInfo);
            UsersController controller = new(logger.Object, userService.Object);
            var result = await controller.GetById(1);
            var okResult = result as OkObjectResult;
            var user = okResult.Value as UserInfo;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(user.Name, userInfo.Name);
        }

        [Fact]
        public async void AddUser()
        {
            var addUser = new AddUserRequest
            {
               
                Name = "name"
            };
            var userInfo = new UserInfo
            {
                Id = 1,
                Name = "name"
            };
            userService.Setup(p => p.AddNewUser(addUser)).ReturnsAsync(userInfo);
            UsersController controller = new(logger.Object, userService.Object);
            var result = await controller.Add(addUser);
            var okResult = result as OkObjectResult;
            var user = okResult.Value as UserInfo;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(addUser.Name, user.Name);
        }
    }
}
