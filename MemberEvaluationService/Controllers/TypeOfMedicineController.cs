using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.TypeOfMedicine;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class TypeOfMedicineController : Controller
{
    private ITypeOfMedicineService _typeOfMedicineService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public TypeOfMedicineController(
        ITypeOfMedicineService typeOfMedicineService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _typeOfMedicineService = typeOfMedicineService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(TypeOfMedicineRequest model)
    {
        _typeOfMedicineService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var typeOfMedicines = _typeOfMedicineService.GetAll();
        return Ok(typeOfMedicines);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var typeOfMedicine = _typeOfMedicineService.GetById(id);
        return Ok(typeOfMedicine);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTypeOfMedicine(int id, UpdateTypeOfMedicine model)
    {
        _typeOfMedicineService.UpdateTypeOfMedicine(id, model);
        return Ok(new { message = "TypeOfMedicine updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTypeOfMedicine(int id)
    {
        _typeOfMedicineService.DeleteTypeOfMedicine(id);
        return Ok(new { message = "TypeOfMedicine deleted successfully" });
    }
}
