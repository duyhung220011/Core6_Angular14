using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace MemberEvaluationService.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string UserRole { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string LockoutEnd { get; set; }
        public string LockoutEnable { get; set; }
    }
}
