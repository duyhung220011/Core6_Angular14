namespace MemberEvaluationService.Models.Users;

using System.ComponentModel.DataAnnotations;

public class AddUserRequest 
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Department { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserRole { get; set; }
}