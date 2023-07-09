using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Doctor;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class DoctorController : Controller
{
    private IDoctorService _doctorService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public DoctorController(
        IDoctorService doctorService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(DoctorRequest model)
    {
        _doctorService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _doctorService.GetAll();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var doctor = _doctorService.GetById(id);
        return Ok(doctor);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDoctor(int id, UpdateDoctor model)
    {
        _doctorService.UpdateDoctor(id, model);
        return Ok(new { message = "Doctor updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(int id)
    {
        _doctorService.DeleteDoctor(id);
        return Ok(new { message = "Doctor deleted successfully" });
    }
}
