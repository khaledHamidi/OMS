namespace OMS.Core.DTOs.Products;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public int Stock { get; set; }
    public Double Price { get; set; }
    public int CompanyId { get; set; }


}