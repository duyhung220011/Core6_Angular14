using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.Skills
{
    public class SkillRequest
    {
        [Required]
        public string SkillId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public string Grade { get; set; }
    }
}
