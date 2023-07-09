using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.Symptom
{
    public class UpdateSymptom
    {
        public string SymptomId { get; set; }
        public string SymptomName { get; set; }
        public Boolean Status { get; set; }
    }
}
