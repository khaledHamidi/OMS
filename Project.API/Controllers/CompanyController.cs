using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Filters;
using OMS.Common;
using OMS.Core.DTOs;
using OMS.Core.DTOs.Companys;
using OMS.Core.Entities;
using OMS.Core.Services;

namespace OMS.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/companys")]
    public class CompanysController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly ICacheService _cacheService;

        public CompanysController(
            IMapper mapper,
            ICompanyService companyService,
            ICacheService cacheService)
        {
            _mapper = mapper;
            _companyService = companyService;
            _cacheService = cacheService;
        }

        // GET api/companys
        [AllowAnonymous]
        //[CustomAuthorize(Authorities = new[] { Constants.RoleGuest })]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cachedCompanys = _cacheService.GetData<List<CompanyDto>>(CacheKeys.Company);
            if (cachedCompanys is null)
            {
                var companys = await _companyService.GetAllAsync();
                var companyDtos = _mapper.Map<List<CompanyDto>>(companys.ToList());
                _cacheService.SetData(CacheKeys.Company, companyDtos);
                return CreateActionResult(CustomResponseDto<List<CompanyDto>>.Success(StatusCodes.Status200OK, companyDtos));
            }
            return CreateActionResult(CustomResponseDto<List<CompanyDto>>.Success(StatusCodes.Status200OK, cachedCompanys));
        }

        // GET api/companys/id
        [HttpGet("{id}")]
        //  [CustomAuthorize(Authorities = new[] { Constants.RoleCompany })]
        [ServiceFilter(typeof(NotFoundIdFilter<Company>))]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return CreateActionResult(CustomResponseDto<CompanyDto>.Success(StatusCodes.Status200OK, companyDto));
        }

        // POST api/companys
        [HttpPost]
        // [CustomAuthorize(Authorities = new[] { Constants.RoleAdmin })]
        public async Task<IActionResult> Create(CompanyCreateDto companyCreateDto)
        {
            var companyCreated = await _companyService.AddAsync(_mapper.Map<Company>(companyCreateDto));
            var newCompany = _mapper.Map<CompanyDto>(companyCreated);
            return CreateActionResult(CustomResponseDto<CompanyDto>.Success(StatusCodes.Status201Created, newCompany));
        }

        // PUT api/companys
        [HttpPut]
        [ServiceFilter(typeof(NotFoundUpdateFilter<Company>))]
        public async Task<IActionResult> Update(CompanyUpdateDto companyUpdateDto)
        {
            await _companyService.UpdateAsync(_mapper.Map<Company>(companyUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }

        // PUT api/companys/UpdateTime
        [HttpPut("UpdateTime")]
        [ServiceFilter(typeof(NotFoundUpdateFilter<Company>))]
        public async Task<IActionResult> TimeUpdate(CompanyUpdateTimeDto companyUpdateDto)
        {
            await _companyService.UpdateTimeAsync((companyUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }



        // DELETE api/companys/id
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundIdFilter<Company>))]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _companyService.GetByIdAsync(id);
            await _companyService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }
    }
}