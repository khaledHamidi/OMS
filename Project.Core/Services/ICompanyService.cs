using OMS.Core.DTOs.Companys;
using OMS.Core.Entities;

namespace OMS.Core.Services;
public interface ICompanyService : IGenericService<Company>
{
    public Task<Company> UpdateTimeAsync(CompanyUpdateTimeDto company);

}

