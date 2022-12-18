using OMS.Core.Entities;
using OMS.Core.Repositories;

namespace OMS.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
