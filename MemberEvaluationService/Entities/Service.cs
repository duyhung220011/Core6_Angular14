namespace MemberEvaluationService.Entities
{
    public class Service
    {
        public int Id { get; set; }

        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Boolean Status { get; set; }
    }
}
