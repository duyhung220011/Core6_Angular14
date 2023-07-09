using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Hospital;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class HospitalController : Controller
{
    private IHospitalService _hospitalService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public HospitalController(
        IHospitalService hospitalService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _hospitalService = hospitalService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(HospitalRequest model)
    {
        _hospitalService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var hospitals = _hospitalService.GetAll();
        return Ok(hospitals);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var hospital = _hospitalService.GetById(id);
        return Ok(hospital);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateHospital(int id, UpdateHospital model)
    {
        _hospitalService.UpdateHospital(id, model);
        return Ok(new { message = "Hospital updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteHospital(int id)
    {
        _hospitalService.DeleteHospital(id);
        return Ok(new { message = "Hospital deleted successfully" });
    }
}
