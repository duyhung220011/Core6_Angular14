using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.TypeofDisease
{
    public class TypeofDiseaseRequest
    {
        public string TypeofDiseaseId { get; set; }
        public string TypeofDiseaseName { get; set; }
        public Boolean Status { get; set; }
    }
}
