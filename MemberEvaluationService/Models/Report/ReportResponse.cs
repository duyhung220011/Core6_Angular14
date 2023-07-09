namespace MemberEvaluationService.Models.Report;

public class ReportResponse
{
    public int Id { get; set; }
    public string ReportId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Level { get; set; }
    public string Grade { get; set; }
    public string Reason { get; set; }
    public string Comment { get; set; }
    public DateTime Create_at { get; set; }
    public DateTime Update_at { get; set; }

}