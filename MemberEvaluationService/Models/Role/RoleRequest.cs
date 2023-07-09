namespace MemberEvaluationService.Models.Role;

using System.ComponentModel.DataAnnotations;

public class RoleRequest
{

    [Required]
    public string RoleId { get; set; }

    [Required]
    public string RoleName { get; set; }
}