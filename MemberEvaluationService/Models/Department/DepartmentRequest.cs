namespace MemberEvaluationService.Models.Department;

using System.ComponentModel.DataAnnotations;

public class DepartmentRequest
{

    [Required]
    public string DepartmentId { get; set; }

    [Required]
    public string DepartmentName { get; set; }
}