namespace MemberEvaluationService.Models.Users;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public string Password { get; set; }
}