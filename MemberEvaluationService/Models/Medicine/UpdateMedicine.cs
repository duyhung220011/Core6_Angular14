namespace MemberEvaluationService.Models.Medicine;

using System.ComponentModel.DataAnnotations;

public class UpdateMedicine
{

    public string MedicineId { get; set; }
    public string TypeMedicineId { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Price { get; set; }
    public Boolean Status { get; set; }
}