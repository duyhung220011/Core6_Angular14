using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Employee;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class EmployeeController : Controller
{
    private IEmployeeService _employeeService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public EmployeeController(
        IEmployeeService employeeService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _employeeService = employeeService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(EmployeeRequest model)
    {
        _employeeService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeeService.GetAll();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var employee = _employeeService.GetById(id);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, UpdateEmployee model)
    {
        _employeeService.UpdateEmployee(id, model);
        return Ok(new { message = "Employee updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        _employeeService.DeleteEmployee(id);
        return Ok(new { message = "Employee deleted successfully" });
    }
}
