using Domain.DTOs.Users;
using Domain.Interfaces;
using FluentValidation;
using Infrastructre.Services.Users;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class UserServiceTests
    {
        #region Property  
        public Mock<IUserRepository> mockUserRepository = new();
        public Mock<ICompanyRepository> mockCompanyRepository = new();
        public Mock<IValidator<AddUserRequest>> mockUserAddValidator = new();
        public Mock<IValidator<UpdateUserRequest>> mockUserUpdateValidator = new();
        public Mock<IValidator<UserBaseRequest>> mockGetValidator = new();

        UserService userService;

        #endregion

        public UserServiceTests()
        {
            userService = new UserService(mockUserRepository.Object,
                mockCompanyRepository.Object,
                mockUserUpdateValidator.Object,
                mockUserAddValidator.Object,
                mockGetValidator.Object
                );

        }

        [Fact]
        public async void Test_GetUserbyId_That_Not_Exist_Throw_NotFoundExcpetion()
        {
            //

        }

        [Fact]
        public async void Test_AddUser_WithEmailAlreadyTaken_ShouldThrowExcpetion()
        {
            //


        }

    }
}
