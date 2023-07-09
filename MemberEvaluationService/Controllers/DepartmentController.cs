namespace MemberEvaluationService.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Department;
using MemberEvaluationService.Services;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    private IDepartmentService _departmentService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public DepartmentsController(
        IDepartmentService departmentService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _departmentService = departmentService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(DepartmentRequest model)
    {
        _departmentService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var departments = _departmentService.GetAll();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var department = _departmentService.GetById(id);
        return Ok(department);
    }

    [HttpPut("{id}")]
    //public IActionResult UpdateDepartment(int id, DepartmentUpdate model)
    //{
    //    _departmentService.UpdateDepartment(id, model);
    //    return Ok(new { message = "Department updated successfully" });
    //}

    [HttpDelete("{id}")]
    public IActionResult DeleteDepartment(int id)
    {
        _departmentService.DeleteDepartment(id);
        return Ok(new { message = "Department deleted successfully" });
    }
}
