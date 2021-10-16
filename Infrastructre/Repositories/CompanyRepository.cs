using Domain.Companies;

namespace Infrastructure.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>
        , ICompanyRepository
    {
        public CompanyRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}