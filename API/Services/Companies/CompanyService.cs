using API.DTOs.Compaines;
using API.DTOs.Users;
using Domain.Departments;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace API.Services.Compaines
{
    public interface ICompanyService
    {
        public Task<AddCompanyResponse> AddNewCompany(AddCompanyRequest model);
    }
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AddCompanyResponse> AddNewCompany(AddCompanyRequest model)
        {

            var company = new Company(model.Name,model.Address,model.Description);

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

    }
}