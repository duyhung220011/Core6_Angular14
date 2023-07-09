using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.TypeOfMedicine
{
    public class UpdateTypeOfMedicine
    {

        public string TypeOfMedicineId { get; set; }
        public string TypeOfMedicineName { get; set; }
        public Boolean Status { get; set; }
    }
}
