using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.Repositories;
using OMS.Core.Services;
using OMS.Core.UnitOfWorks;

namespace OMS.Service.Services;

public class UserService : Service<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(repository, unitOfWork)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        var user = await _userRepository.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        return user;
    }

}
