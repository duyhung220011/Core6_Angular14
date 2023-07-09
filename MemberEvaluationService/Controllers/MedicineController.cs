using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Medicine;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class MedicineController : Controller
{
    private IMedicineService _medicineService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public MedicineController(
        IMedicineService medicineService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _medicineService = medicineService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(MedicineRequest model)
    {
        _medicineService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var medicines = _medicineService.GetAll();
        return Ok(medicines);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var medicine = _medicineService.GetById(id);
        return Ok(medicine);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMedicine(int id, UpdateMedicine model)
    {
        _medicineService.UpdateMedicine(id, model);
        return Ok(new { message = "Medicine updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMedicine(int id)
    {
        _medicineService.DeleteMedicine(id);
        return Ok(new { message = "Medicine deleted successfully" });
    }
}
