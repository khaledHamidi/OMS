using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Core.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public Double Price { get; set; }


        [ForeignKey("Companys")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
