namespace MemberEvaluationService.Models.Users;

public class UpdateRequest
{
    public string FullName { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public string UserRole { get; set; }

}