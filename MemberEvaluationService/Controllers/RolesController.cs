namespace MemberEvaluationService.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Role;
using MemberEvaluationService.Services;
using MemberEvaluationService.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private IRoleService _roleService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public RolesController(
        IRoleService roleService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _roleService = roleService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddRole(RoleRequest model)
    {
        _roleService.AddRole(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _roleService.GetAll();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var role = _roleService.GetById(id);
        return Ok(role);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRole(int id, RoleUpdate model)
    {
        _roleService.UpdateRole(id, model);
        return Ok(new { message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        _roleService.DeleteRole(id);
        return Ok(new { message = "Role deleted successfully" });
    }
}
