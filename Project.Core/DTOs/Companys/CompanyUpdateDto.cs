namespace OMS.Core.DTOs.Companys;

public class CompanyUpdateDto
{
    public int Id { get; set; }
    public string Name { set; get; }
    public bool Status { set; get; }
    public DateTime StartDate { set; get; }
    public DateTime EndDate { set; get; }

}