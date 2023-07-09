namespace MemberEvaluationService.Models.Report;

public class ReportUpdate
{
    public string ReportId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Level { get; set; }
    public string Grade { get; set; }
    public string Reason { get; set; }
    public string Comment { get; set; }
    
}