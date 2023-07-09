using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Address;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private IAddressService _addressService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public AddressController(
        IAddressService addressService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _addressService = addressService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(AddressRequest model)
    {
        _addressService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var addresses = _addressService.GetAll();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var address = _addressService.GetById(id);
        return Ok(address);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, UpdateAddress model)
    {
        _addressService.UpdateAddress(id, model);
        return Ok(new { message = "Address updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        _addressService.DeleteAddress(id);
        return Ok(new { message = "Address deleted successfully" });
    }
}
