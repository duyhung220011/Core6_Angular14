namespace MemberEvaluationService.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Report;
using MemberEvaluationService.Services;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private IReportService _reportService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public ReportsController(
        IReportService reportService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _reportService = reportService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(ReportRequest model)
    {
        _reportService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var reports = _reportService.GetAll();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var report = _reportService.GetById(id);
        return Ok(report);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReport(int id, ReportUpdate model)
    {
        _reportService.UpdateReport(id, model);
        return Ok(new { message = "Report updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReport(int id)
    {
        _reportService.DeleteReport(id);
        return Ok(new { message = "Department deleted successfully" });
    }
}
