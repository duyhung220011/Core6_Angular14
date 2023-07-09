using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Symptom;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class SymptomController : Controller
{
    private ISymptomService _symptomService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public SymptomController(
        ISymptomService symptomService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _symptomService = symptomService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(SymptomRequest model)
    {
        _symptomService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var symptoms = _symptomService.GetAll();
        return Ok(symptoms);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var symptom = _symptomService.GetById(id);
        return Ok(symptom);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSymptom(int id, UpdateSymptom model)
    {
        _symptomService.UpdateSymptom(id, model);
        return Ok(new { message = "Symptom updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSymptom(int id)
    {
        _symptomService.DeleteSymptom(id);
        return Ok(new { message = "Symptom deleted successfully" });
    }
}
