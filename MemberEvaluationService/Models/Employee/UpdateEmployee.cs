namespace MemberEvaluationService.Models.Employee;

using System.ComponentModel.DataAnnotations;

public class UpdateEmployee
{

    public string EmployeeId { get; set; }
    public string Name { get; set; }
    public string Birth { get; set; }
    public int Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Position { get; set; }
    public string DepartmentId { get; set; }
    public string UserId { get; set; }
    public string RoleId { get; set; }
    public Boolean Status { get; set; }
}