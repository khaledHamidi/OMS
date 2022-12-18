namespace OMS.Core.DTOs.Companys;

public class CompanyUpdateTimeDto
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

}