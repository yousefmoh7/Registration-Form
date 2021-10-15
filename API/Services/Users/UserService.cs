using API.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using System.Threading.Tasks;

namespace API.Services.Users
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AddUserResponse> AddNewAsync(AddUserRequest model)
        {
            
        var user = new User(model.Name
                , model.Email
                , model.Address
                , model.Password
                , model.CompanyId.Value);

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