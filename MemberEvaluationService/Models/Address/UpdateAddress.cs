namespace MemberEvaluationService.Models.Address;

using System.ComponentModel.DataAnnotations;

public class UpdateAddress
{
    public string AddressId { get; set; }
    public string AddressName { get; set; }
    public Boolean Status { get; set; }
}