namespace MemberEvaluationService.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string MedicineId { get; set; }
        public string TypeMedicineId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Price { get; set; }
        public Boolean Status { get; set; }
    }
}
