using Domain.DTOs.Users;
using Domain.Entities.Users;
using Domain.Interfaces;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructre.Services.Users
{
    public interface IUserService
    {
        public Task<UserInfo> AddNewUser(AddUserRequest model);
        public Task<List<UserInfo>> GetAllUsersAsync();
        public Task<UserInfo> GetUser(int id);
        public Task<UserInfo> UpdateUser(UpdateUserRequest model, int id);
        public Task DeleteUser(int id);

    }

    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public ICompanyRepository _companyRepository;
        private readonly IValidator<AddUserRequest> _userAddValidator;
        private readonly IValidator<UpdateUserRequest> _userUpdateValidator;
        private readonly IValidator<UserBaseRequest> _userGetValidator;


        public UserService(IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IValidator<UpdateUserRequest> userUpdateValidator,
            IValidator<AddUserRequest> userAddValidator,
            IValidator<UserBaseRequest> userGetValidator
            )
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _userAddValidator = userAddValidator;
            _userUpdateValidator = userUpdateValidator;
            _userGetValidator = userGetValidator;
        }

        public async Task<UserInfo> AddNewUser(AddUserRequest model)
        {
            await _userAddValidator.ValidateAndThrowException(model);
            var user = new User(model.Name
                , model.Email
                , model.Address
                , model.Password
                , model.CompanyId);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var response = new UserInfo()
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                CompanyId = user.CompanyId,
                Email = user.Email
            };

            return response;
        }

        public async Task DeleteUser(int id)
        {
            var entity = await _userRepository.GetAsyncById(id);
            await _userGetValidator.ValidateAndThrowException(new UserBaseRequest { Id = id });

            await _userRepository.DeleteAsync(entity);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserInfo> GetUser(int id)
        {
            await _userGetValidator.ValidateAndThrowException(new UserBaseRequest { Id = id });

            var user = await _userRepository.GetAsyncById(id);


            return new UserInfo()
            {
                Name = user.Name,
                Id = user.Id,
                Address = user.Address,
                CompanyId = user.CompanyId,
                Email = user.Email
            };
        }

        public async Task<List<UserInfo>> GetAllUsersAsync()
        {

            var users = await _userRepository.ListAsync();
            var userDTOs = users.Select(_ => new UserInfo()
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

        public async Task<UserInfo> UpdateUser(UpdateUserRequest model, int id)
        {
            model.Id = id;
            await _userUpdateValidator.ValidateAndThrowException(model);

            var user = await _userRepository.GetAsyncById(id);
            user.Update(model.Name, model.Address, model.Email, model.Password, model.CompanyId);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserInfo
            {
                Name = user.Name,
                Id=user.Id,
                CompanyId = user.CompanyId,
                Email = user.Email,
                Address = user.Address
            };
        }
    }
}