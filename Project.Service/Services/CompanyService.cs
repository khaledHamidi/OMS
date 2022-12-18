using AutoMapper;
using OMS.Core.DTOs.Companys;
using OMS.Core.Entities;
using OMS.Core.Repositories;
using OMS.Core.Services;
using OMS.Core.UnitOfWorks;

namespace OMS.Service.Services
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(IGenericRepository<Company> repository, IUnitOfWork unitOfWork, IMapper mapper, ICompanyRepository companyRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<Company> UpdateTimeAsync(CompanyUpdateTimeDto company)
        {
            var s1 = new DateTime(2001, 1, 1);
            s1 += company.StartTime.ToTimeSpan();

            var s2 = new DateTime(2001, 1, 1);
            s2 += company.EndTime.ToTimeSpan();

            Company x = await _companyRepository.GetByIdAsync(company.Id);
            x.StartDate = s1;
            x.EndDate = s2;


            return x;


        }



    }
}
