using OMS.Core.Entities;

namespace OMS.Core.Services;
public interface IUserService : IGenericService<User>
{
    Task<User> GetUserByUserName(string email);
}
