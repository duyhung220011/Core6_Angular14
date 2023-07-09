using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.RegistrationForm;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class RegistrationFormController : Controller
{
    private IRegistrationFormService _registrationFormService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public RegistrationFormController(
        IRegistrationFormService registrationFormService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _registrationFormService = registrationFormService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(RegistrationFormRequest model)
    {
        _registrationFormService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var registrationForms = _registrationFormService.GetAll();
        return Ok(registrationForms);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var registrationForm = _registrationFormService.GetById(id);
        return Ok(registrationForm);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRegistrationForm(int id, UpdateRegistrationForm model)
    {
        _registrationFormService.UpdateRegistrationForm(id, model);
        return Ok(new { message = "RegistrationForm updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRegistrationForm(int id)
    {
        _registrationFormService.DeleteRegistrationForm(id);
        return Ok(new { message = "RegistrationForm deleted successfully" });
    }
}
