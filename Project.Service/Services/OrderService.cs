using AutoMapper;
using OMS.Core.Entities;
using OMS.Core.Repositories;
using OMS.Core.Services;
using OMS.Core.UnitOfWorks;

namespace OMS.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
    }
}
