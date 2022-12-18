using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Filters;
using OMS.Common;
using OMS.Core.DTOs;
using OMS.Core.DTOs.Products;
using OMS.Core.Entities;
using OMS.Core.Services;

namespace OMS.API.Controllers
{
    //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    // [ApiController]
    [Route("api/products")]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICacheService _cacheService;

        public ProductsController(
            IMapper mapper,
            IProductService productService,
            ICacheService cacheService)
        {
            _mapper = mapper;
            _productService = productService;
            _cacheService = cacheService;
        }

        // GET api/products
        [AllowAnonymous]
        //[CustomAuthorize(Authorities = new[] { Constants.RoleGuest })]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cachedProducts = _cacheService.GetData<List<ProductDto>>(CacheKeys.Product);
            if (cachedProducts is null)
            {
                var products = _productService.GetAllAsync();
                var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
                _cacheService.SetData(CacheKeys.Product, productDtos);
                return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(StatusCodes.Status200OK, productDtos));
            }
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(StatusCodes.Status200OK, cachedProducts));
        }

        // GET api/products/id
        [HttpGet("{id}")]
        //  [CustomAuthorize(Authorities = new[] { Constants.RoleGuest })]
        [ServiceFilter(typeof(NotFoundIdFilter<Product>))]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK, productDto));
        }

        // POST api/products
        [HttpPost]
        //[CustomAuthorize(Authorities = new[] { Constants.RoleAdmin })]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var productCreated = await _productService.AddAsync(_mapper.Map<Product>(productCreateDto));
            var newProduct = _mapper.Map<ProductDto>(productCreated);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created, newProduct));
        }

        // PUT api/products
        [HttpPut]
        [ServiceFilter(typeof(NotFoundUpdateFilter<Product>))]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }

        // DELETE api/products/id
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundIdFilter<Product>))]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }
    }
}