using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.TypeofDisease;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class TypeOfDiseaseController : Controller
{
    private ITypeofDiseaseService _typeOfDiseaseService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public TypeOfDiseaseController(
        ITypeofDiseaseService typeOfDiseaseService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _typeOfDiseaseService = typeOfDiseaseService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(TypeofDiseaseRequest model)
    {
        _typeOfDiseaseService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var typeOfDiseases = _typeOfDiseaseService.GetAll();
        return Ok(typeOfDiseases);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var typeOfDisease = _typeOfDiseaseService.GetById(id);
        return Ok(typeOfDisease);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTypeofDisease(int id, UpdateTypeofDisease model)
    {
        _typeOfDiseaseService.UpdateTypeofDisease(id, model);
        return Ok(new { message = "TypeofDisease updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTypeofDisease(int id)
    {
        _typeOfDiseaseService.DeleteTypeofDisease(id);
        return Ok(new { message = "TypeofDisease deleted successfully" });
    }
}

