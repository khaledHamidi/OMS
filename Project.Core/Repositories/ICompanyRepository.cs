using OMS.Core.Entities;

namespace OMS.Core.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public async Task<Company> UpdateTimeAsync1(Company company)
        {
            return null;
        }

    }


}
