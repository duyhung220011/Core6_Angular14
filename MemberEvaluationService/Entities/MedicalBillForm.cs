namespace MemberEvaluationService.Entities
{
    public class MedicalBillForm
    {
        public int Id { get; set; }
        public string MedicalBillFormId { get; set; }
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
        public String DoctorId { get; set; }
        public string Note { get; set; }
        public string ServiceId { get; set; }
        public string TypeOfMedicineId { get; set; }
        public String MedicineId { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public Boolean Status { get; set; }
    }
}
