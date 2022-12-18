using OMS.Core.Entities;

namespace OMS.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}