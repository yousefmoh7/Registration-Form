using API.DTOs.Compaines;
using API.DTOs.Companies;
using Domain.Companies;
using Domain.Interfaces;
using Infrastructre.ValidatorExtentions;
using Infrastructre.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructre.Services.Compaines
{
    public interface ICompanyService
    {
        public Task<AddCompanyResponse> AddNewCompany(AddCompanyRequest model);
        public Task<CompanyInfo> UpdateCompany(UpdateCompanyRequest model, int id);
        public Task<CompanyInfo> GetCompany(int id);
        public Task DeleteCompany(int id);
        public Task<List<CompanyInfo>> SearchAsync(GetCompanyRequest request);

    }
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AddCompanyResponse> AddNewCompany(AddCompanyRequest model)
        {
            var validator = new CompanyValidator();
            await validator.ValidateAndThrow(model);

            var company = new Company(model.Name, model.Address, model.Description);

            var repository = UnitOfWork.AsyncRepository<Company>();
            await repository.AddAsync(company);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddCompanyResponse()
            {
                Id = company.Id,
                CompanyName = company.Name
            };

            return response;
        }

        public async Task DeleteCompany(int id)
        {
            var repository = UnitOfWork.AsyncRepository<Company>();
            var entity = await repository.GetAsyncById(id);
            await repository.DeleteAsync(entity);
            await UnitOfWork.SaveChangesAsync();

        }

        public async Task<CompanyInfo> GetCompany(int id)
        {
            var repository = UnitOfWork.AsyncRepository<Company>();
            var entity = await repository.GetAsyncById(id);
            return new CompanyInfo()
            {
                Name = entity.Name,
                Id = entity.Id,
                Address = entity.Address,
                Description = entity.Description
            };
        }

        public async Task<List<CompanyInfo>> SearchAsync(GetCompanyRequest request)
        {
            var repository = UnitOfWork.AsyncRepository<Company>();

            var companies =
             await repository
             .ListAsync(_ => _.Name.Contains(request.Search));

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
            var repository = UnitOfWork.AsyncRepository<Company>();
            var company = await repository.GetAsyncById(id);
            company.Update(model.Name, model.Address, model.Description);
            await repository.UpdateAsync(company);
            await UnitOfWork.SaveChangesAsync();

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