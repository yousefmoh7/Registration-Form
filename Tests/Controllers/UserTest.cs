using API.Controllers;
using API.DTOs.Users;
using Infrastructre.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
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
            var userInfo = new UserInfoDTO
            {
                Id = 1,
                Name="name"
            };
            userService.Setup(p => p.GetUser(1)).ReturnsAsync(userInfo);
            UsersController controller = new(logger.Object, userService.Object);
            var result = await controller.GetById(1);
            var okResult = result as OkObjectResult;
            var user = okResult.Value as UserInfoDTO;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(user.Name, userInfo.Name);


        }
    }
}
