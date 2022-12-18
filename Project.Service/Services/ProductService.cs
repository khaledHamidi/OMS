using AutoMapper;
using OMS.Core.Entities;
using OMS.Core.Repositories;
using OMS.Core.Services;
using OMS.Core.UnitOfWorks;

namespace OMS.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _croductRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository croductRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _croductRepository = croductRepository;
        }


        List<Product> IProductService.GetAllAsync()
        {
            var x = _croductRepository.Where(product => product.Company.Status == true);
            return x.ToList();
        }
    }
}
