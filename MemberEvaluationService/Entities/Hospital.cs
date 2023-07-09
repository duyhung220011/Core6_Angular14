namespace MemberEvaluationService.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public string HospitalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Boolean Status { get; set; }
    }
}
