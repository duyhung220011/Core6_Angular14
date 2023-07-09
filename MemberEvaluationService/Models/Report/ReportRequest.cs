namespace MemberEvaluationService.Models.Report;

using System.ComponentModel.DataAnnotations;

public class ReportRequest
{

    [Required]
    public string ReportId { get; set; }

    [Required]
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Level { get; set; }
    public string Grade { get; set; }
    public string Reason { get; set; }
    public string Comment { get; set; }
}