using AutoMapper;
using OMS.Core.DTOs.Companys;
using OMS.Core.Entities;

namespace OMS.Service.Mapping.Companys;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>().ReverseMap();
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();
        CreateMap<CompanyUpdateTimeDto, Company>();
    }
}
