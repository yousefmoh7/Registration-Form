using Domain.Entities.Users;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructre.Repositories
{
    public class UserRepository : RepositoryBase<User>
        , IUserRepository
    {
        public UserRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}