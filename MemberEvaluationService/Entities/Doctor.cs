namespace MemberEvaluationService.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Degree { get; set; }
        public string DepartmentId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public Boolean Status { get; set; }
    }
}
