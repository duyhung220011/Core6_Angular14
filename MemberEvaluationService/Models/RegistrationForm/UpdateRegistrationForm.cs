namespace MemberEvaluationService.Models.RegistrationForm;

using System.ComponentModel.DataAnnotations;

public class UpdateRegistrationForm
{

    public string RegistrationFormId { get; set; }
    public string Name { get; set; }
    public string Birth { get; set; }
    public int Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Passport { get; set; }
    public string AddressId { get; set; }
    public string HospitalId { get; set; }
    public string DepartmentId { get; set; }
    public int DoctorId { get; set; }
    public string Note { get; set; }
    public DateTime Create_at { get; set; }
    public DateTime Update_at { get; set; }
    public Boolean Status { get; set; }
}