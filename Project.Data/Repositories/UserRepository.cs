using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.Repositories;

namespace OMS.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(b => b.UserName.Equals(userName));
        }
    }
}
