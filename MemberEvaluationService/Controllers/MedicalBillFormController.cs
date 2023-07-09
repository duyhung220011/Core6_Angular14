using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.MedicalBillForm;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class MedicalBillFormController : Controller
{
    private IMedicalBillFormService _medicalBillFormService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public MedicalBillFormController(
        IMedicalBillFormService medicalBillFormService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _medicalBillFormService = medicalBillFormService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(MedicalBillFormRequest model)
    {
        _medicalBillFormService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var medicalBillForms = _medicalBillFormService.GetAll();
        return Ok(medicalBillForms);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var medicalBillForm = _medicalBillFormService.GetById(id);
        return Ok(medicalBillForm);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMedicalBillForm(int id, UpdateMedicalBillForm model)
    {
        _medicalBillFormService.UpdateMedicalBillForm(id, model);
        return Ok(new { message = "MedicalBillForm updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMedicalBillForm(int id)
    {
        _medicalBillFormService.DeleteMedicalBillForm(id);
        return Ok(new { message = "MedicalBillForm deleted successfully" });
    }
}
