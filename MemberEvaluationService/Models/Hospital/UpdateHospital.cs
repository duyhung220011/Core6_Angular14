namespace MemberEvaluationService.Models.Hospital;

using System.ComponentModel.DataAnnotations;

public class UpdateHospital
{

    public string HospitalId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Boolean Status { get; set; }
}