using Microsoft.EntityFrameworkCore;
using OMS.Core.DTOs.Companys;
using OMS.Core.Entities;
using OMS.Core.Repositories;

namespace OMS.Data.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {

        public CompanyRepository(DatabaseContext context) : base(context)
        {

        }


        public async Task<Company> UpdateTimeAsync1(CompanyUpdateTimeDto company)
        {
            var entity = _context.Companys.Find(company.Id);

            if (entity != null)
            {
                var referenceDate = new DateTime(2001, 1, 1);
                referenceDate += company.StartTime.ToTimeSpan();

                entity.StartDate = referenceDate;
                _ = _context.SaveChangesAsync();
                return await _context.Companys.FirstOrDefaultAsync(b => b.Id.Equals(company.Id));
            }

            return null;
        }



    }



}
