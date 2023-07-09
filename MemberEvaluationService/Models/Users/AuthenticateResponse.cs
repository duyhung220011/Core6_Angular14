namespace MemberEvaluationService.Models.Users;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
}