using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Core.Entities
{
    [Table("Products")]

    public class Company : BaseEntity
    {
        public string Name { set; get; }
        public bool Status { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
