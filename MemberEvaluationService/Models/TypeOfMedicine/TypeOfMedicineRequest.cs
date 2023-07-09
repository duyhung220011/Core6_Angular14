using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.TypeOfMedicine
{
    public class TypeOfMedicineRequest
    {

        public string TypeOfMedicineId { get; set; }
        public string TypeOfMedicineName { get; set; }
        public Boolean Status { get; set; }
    }
}
