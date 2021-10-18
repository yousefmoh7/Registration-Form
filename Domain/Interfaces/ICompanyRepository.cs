using Domain.Entities.Companies;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
    }
}