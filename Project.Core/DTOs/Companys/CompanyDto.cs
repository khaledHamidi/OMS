using OMS.Core.Entities;

namespace OMS.Core.DTOs.Companys;

public class CompanyDto : BaseDto
{
    public string Name { set; get; }
    public bool Status { set; get; }
    public DateTime StartDate { set; get; }
    public DateTime EndDate { set; get; }


    public virtual ICollection<Product> Products { get; set; }

}

