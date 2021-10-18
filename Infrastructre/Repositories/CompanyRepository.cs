using Domain.Entities.Companies;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructre.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>
        , ICompanyRepository
    {
        public CompanyRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}