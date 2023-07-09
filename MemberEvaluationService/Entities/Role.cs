using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace MemberEvaluationService.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
