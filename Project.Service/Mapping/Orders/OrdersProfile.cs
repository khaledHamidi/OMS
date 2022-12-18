using AutoMapper;
using OMS.Core.DTOs.Orders;
using OMS.Core.Entities;

namespace OMS.Service.Mapping.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
    }
}
