using Domain.Companies;
using Domain.DTOs.Users;
using Domain.Interfaces;
using Domain.Users;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using Infrastructre.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public class UserService : IUserService
    {
        public IAsyncRepository<User> _userRepository;
        public IAsyncRepository<Company> _companyRepository;
        private readonly IValidator<AddUserRequest> _userAddValidator;
        private readonly IValidator<UpdateUserRequest> _userUpdateValidator;
        private readonly IValidator<UserBaseRequest> _userGetValidator;


        public UserService(IAsyncRepository<User> userRepository,
            IAsyncRepository<Company> companyRepository,
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

        public async Task<AddUserResponse> AddNewUser(AddUserRequest model)
        {
            // validate company exists
            await _userAddValidator.ValidateAndThrowEx(model);
            var user = new User(model.Name
                , model.Email
                , model.Address
                , model.Password
                , model.CompanyId);
      
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var response = new AddUserResponse()
            {
                Id = user.Id,
                UserName = user.Name
            };

            return response;
        }

        public async Task DeleteUser(int id)
        {
            var entity = await _userRepository.GetAsyncById(id);
            await _userGetValidator.ValidateAndThrowEx(new UserBaseRequest { Id = id });

            await _userRepository.DeleteAsync(entity);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserInfoDTO> GetUser(int id)
        {
            await _userGetValidator.ValidateAndThrowEx(new UserBaseRequest { Id = id });

            var user = await _userRepository.GetAsyncById(id);
           

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

            //var users = await _userRepository
            //    .ListAsync(_ => 0request.Search _.Name.Contains(request.Search));
            var users = await _userRepository.ListAsync();
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
            model.Id = id;
            await _userUpdateValidator.ValidateAndThrowEx(model);
            
            var user = await _userRepository.GetAsyncById(id);
            user.Update(model.Name, model.Address, model.Email, model.Password, model.CompanyId);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

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