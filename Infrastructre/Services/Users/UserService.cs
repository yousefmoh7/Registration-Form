using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructre.Services.Users
{
    public interface IUserService
    {
        public Task<AddUserResponse> AddNewUser(AddUserRequest model);
        public Task<List<UserInfoDTO>> SearchAsync(GetUserRequest request);
        public Task<UserInfoDTO> GetUser(int id);
        public Task<UserInfoDTO> UpdateUser(UpdateUserRequest model, int id);
        public Task DeleteUser(int id);


    }

    public class UserService : BaseService, IUserService
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
            //var validator = new UserValidator();
            //validator.ValidateAndThrow(model);
            var repository = UnitOfWork.AsyncRepository<User>();
            if (await repository.IsExistAsync(x => x.Name == model.Name))
                throw new BadHttpRequestException("User Name Already Used");

            await repository.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddUserResponse()
            {
                Id = user.Id,
                UserName = user.Name
            };

            return response;
        }

        public async Task DeleteUser(int id)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var entity = await repository.GetAsyncById(id);
            await repository.DeleteAsync(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<UserInfoDTO> GetUser(int id)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var user = await repository.GetAsyncById(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return new UserInfoDTO()
            {
                Name = user.Name,
                Id = user.Id,
                Address = user.Address,
                CompanyId = user.CompanyId,
                Email = user.Email
            };
        }

        public async Task<List<UserInfoDTO>> SearchAsync(GetUserRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var users = await repository
                .ListAsync(_ => _.Name.Contains(request.Search));

            var userDTOs = users.Select(_ => new UserInfoDTO()
            {
                Address = _.Address,
                CompanyId = _.CompanyId,
                Email = _.Email,
                Name = _.Name,
                Id = _.Id,
            })
            .ToList();

            return userDTOs;
        }

        public async Task<UserInfoDTO> UpdateUser(UpdateUserRequest model, int id)
        {
            var repository = UnitOfWork.AsyncRepository<User>();
            var user = await repository.GetAsyncById(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Update(model.Name, model.Address, model.Email, model.Password, model.CompanyId);
            await repository.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();

            return new UserInfoDTO
            {
                Name = user.Name,
                CompanyId = user.CompanyId,
                Email = user.Email,
                Address = user.Address
            };
        }
    }
}