using OMS.Core.Entities;

namespace OMS.Core.DTOs.Products;

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Stock { get; set; }
    public Double Price { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }



}