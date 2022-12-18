using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Filters;
using OMS.Common;
using OMS.Core.DTOs;
using OMS.Core.DTOs.Orders;
using OMS.Core.Entities;
using OMS.Core.Services;

namespace OMS.API.Controllers
{
    //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [ApiController]
    [Route("api/orders")]
    public class OrdersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly ICacheService _cacheService;

        public OrdersController(
            IMapper mapper,
            IOrderService orderService,
            ICacheService cacheService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _cacheService = cacheService;
        }

        // GET api/orders
        [AllowAnonymous]
        //[CustomAuthorize(Authorities = new[] { Constants.RoleGuest })]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cachedOrders = _cacheService.GetData<List<OrderDto>>(CacheKeys.Order);
            if (cachedOrders is null)
            {
                var orders = await _orderService.GetAllAsync();
                var orderDtos = _mapper.Map<List<OrderDto>>(orders.ToList());
                _cacheService.SetData(CacheKeys.Order, orderDtos);
                return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(StatusCodes.Status200OK, orderDtos));
            }
            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(StatusCodes.Status200OK, cachedOrders));
        }

        // GET api/orders/id
        [HttpGet("{id}")]
        //  [CustomAuthorize(Authorities = new[] { Constants.RoleGuest })]
        [ServiceFilter(typeof(NotFoundIdFilter<Order>))]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            var orderDto = _mapper.Map<OrderDto>(order);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(StatusCodes.Status200OK, orderDto));
        }

        // POST api/orders
        [HttpPost]
        //  [CustomAuthorize(Authorities = new[] { Constants.RoleCompany })]
        public async Task<IActionResult> Create(OrderCreateDto orderCreateDto)
        {
            var orderCreated = await _orderService.AddAsync(_mapper.Map<Order>(orderCreateDto));
            var newOrder = _mapper.Map<OrderDto>(orderCreated);
            return CreateActionResult(CustomResponseDto<OrderDto>.Success(StatusCodes.Status201Created, newOrder));
        }

        // PUT api/orders
        [HttpPut]
        [ServiceFilter(typeof(NotFoundUpdateFilter<Order>))]
        public async Task<IActionResult> Update(OrderUpdateDto orderUpdateDto)
        {
            await _orderService.UpdateAsync(_mapper.Map<Order>(orderUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }

        // DELETE api/orders/id
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundIdFilter<Order>))]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _orderService.GetByIdAsync(id);
            await _orderService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent));
        }
    }
}