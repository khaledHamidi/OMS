using OMS.Core.Entities;

namespace OMS.Core.DTOs.Orders;

public class OrderUpdateDto
{
    public int Id { get; set; }

    public string? ClientName { get; set; }



    public int ProductId { get; set; }
    public Product Product { get; set; }


}