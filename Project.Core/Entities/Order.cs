using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Core.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {

        public string? ClientName { get; set; }



        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Product Product { get; set; }



        //  public int CompanyId { get; set; }
    }

}
