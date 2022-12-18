using AutoMapper;
using OMS.Core.DTOs.Products;
using OMS.Core.Entities;

namespace OMS.Service.Mapping.Products;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
    }
}
