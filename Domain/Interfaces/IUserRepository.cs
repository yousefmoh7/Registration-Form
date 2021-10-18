using Domain.Entities.Users;

namespace Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}