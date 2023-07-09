using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace MemberEvaluationService.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
