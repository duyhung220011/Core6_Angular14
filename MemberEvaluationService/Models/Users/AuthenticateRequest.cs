namespace MemberEvaluationService.Models.Users;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Password { get; set; }
}