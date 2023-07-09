using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Service;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class ServiceController : Controller
{
    private IServiceService _serviceService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public ServiceController(
        IServiceService serviceService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _serviceService = serviceService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(ServiceRequest model)
    {
        _serviceService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var services = _serviceService.GetAll();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var service = _serviceService.GetById(id);
        return Ok(service);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateService(int id, UpdateService model)
    {
        _serviceService.UpdateService(id, model);
        return Ok(new { message = "Service updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteService(int id)
    {
        _serviceService.DeleteService(id);
        return Ok(new { message = "Service deleted successfully" });
    }
}
