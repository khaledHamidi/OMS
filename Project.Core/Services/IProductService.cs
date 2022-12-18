using OMS.Core.Entities;

namespace OMS.Core.Services;
public interface IProductService : IGenericService<Product>
{
    public new List<Product> GetAllAsync();


}

