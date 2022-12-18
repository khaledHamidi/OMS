using OMS.Core.Entities;
using OMS.Core.Repositories;

namespace OMS.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
