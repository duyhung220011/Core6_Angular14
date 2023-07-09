using System.ComponentModel.DataAnnotations;

namespace MemberEvaluationService.Models.Service
{
    public class ServiceRequest
    {

        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Boolean Status { get; set; }
    }
}
