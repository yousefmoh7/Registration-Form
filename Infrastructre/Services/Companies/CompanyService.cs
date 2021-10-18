using Domain.DTOs.Compaines;
using Domain.DTOs.Companies;
using Domain.Entities.Companies;
using Domain.Interfaces;
using FluentValidation;
using Infrastructre.ValidatorExtentions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructre.Services.Companies
{
    public interface ICompanyService
    {
        public Task<CompanyInfo> AddNewCompany(AddCompanyRequest model);
        public Task<CompanyInfo> UpdateCompany(UpdateCompanyRequest model, int id);
        public Task<CompanyInfo> GetCompany(int id);
        public Task DeleteCompany(int id);
        public Task<List<CompanyInfo>> GetAllCompaniesAsync();

    }
    public class CompanyService : ICompanyService
    {
        public ICompanyRepository _companyRepository;
        private readonly IValidator<AddCompanyRequest> _companyAddValidator;
        private readonly IValidator<UpdateCompanyRequest> _companyUpdateValidator;
        private readonly IValidator<CompanyBaseRequest> _companyGetValidator;
        private readonly IValidator<DeleteCompanyRequest> _companyDeleteValidator;


        public CompanyService(
            ICompanyRepository companyRepository,
            IValidator<UpdateCompanyRequest> companyUpdateValidator,
            IValidator<AddCompanyRequest> companyAddValidator,
            IValidator<CompanyBaseRequest> companyGetValidator,
            IValidator<DeleteCompanyRequest> companyDeleteValidator
            )
        {
            _companyRepository = companyRepository;
            _companyAddValidator = companyAddValidator;
            _companyUpdateValidator = companyUpdateValidator;
            _companyGetValidator = companyGetValidator;
            _companyDeleteValidator = companyDeleteValidator;

        }

        public async Task<CompanyInfo> AddNewCompany(AddCompanyRequest model)
        {

            await _companyAddValidator.ValidateAndThrowEx(model);
            var company = new Company(model.Name, model.Address, model.Description);

            await _companyRepository.AddAsync(company);
            await _companyRepository.SaveChangesAsync();

            var response = new CompanyInfo()
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Description = company.Description
            };

            return response;
        }

        public async Task DeleteCompany(int id)
        {
            await _companyDeleteValidator.ValidateAndThrowEx(new DeleteCompanyRequest { Id = id });
            var entity = await _companyRepository.GetAsyncById(id);
            await _companyRepository.DeleteAsync(entity);
            await _companyRepository.SaveChangesAsync();

        }

        public async Task<CompanyInfo> GetCompany(int id)
        {
            await _companyGetValidator.ValidateAndThrowEx(new CompanyBaseRequest { Id = id });

            var entity = await _companyRepository.GetAsyncById(id);
            return new CompanyInfo()
            {
                Name = entity.Name,
                Id = entity.Id,
                Address = entity.Address,
                Description = entity.Description
            };
        }

        public async Task<List<CompanyInfo>> GetAllCompaniesAsync()
        {

            var companies =
             await _companyRepository
             .ListAsync();

            var companyDTOs = companies.Select(_ => new CompanyInfo
            {
                Address = _.Address,
                Name = _.Name,
                Description = _.Description,
                Id = _.Id,
            })
            .ToList();

            return companyDTOs;
        }

        public async Task<CompanyInfo> UpdateCompany(UpdateCompanyRequest model, int id)
        {
            model.Id = id;
            await _companyUpdateValidator.ValidateAndThrowEx(model);
            var company = await _companyRepository.GetAsyncById(id);
            //if (await _companyRepository.GetAsyncById(id) == null)
            //    throw new KeyNotFoundException("");
            company.Update(model.Name, model.Address, model.Description);
            await _companyRepository.UpdateAsync(company);
            await _companyRepository.SaveChangesAsync();

            return new CompanyInfo
            {
                Name = company.Name,
                Id = company.Id,
                Address = company.Address,
                Description = company.Description
            };
        }
    }
}