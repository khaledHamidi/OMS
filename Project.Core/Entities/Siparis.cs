using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Core.Entities
{
    [Table("Sipariss")]
    public class Siparis : BaseEntity
    {


        public string ClientName { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }





    }
}
