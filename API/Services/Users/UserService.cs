using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System.Threading.Tasks;

namespace API.Services.Users
{
    public interface IUserService
    {
        public Task<AddUserResponse> AddNewUser(AddUserRequest model);
    }

    public class UserService : BaseService,IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

       

        public async Task<AddUserResponse> AddNewUser(AddUserRequest model)
        {
            var user = new User(model.Name
                , model.Email
                , model.Address
                , model.Password
                , model.CompanyId);

            var repository = UnitOfWork.AsyncRepository<User>();
            await repository.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddUserResponse()
            {
                Id = user.Id,
                UserName = user.Name
            };

            return response;
        }
    }
}