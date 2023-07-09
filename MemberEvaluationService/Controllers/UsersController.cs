namespace MemberEvaluationService.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Users;
using MemberEvaluationService.Services;
using Microsoft.EntityFrameworkCore;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Models.Pagination;
using System.Text.Json;
using Newtonsoft.Json;
using MemberEvaluationService.Repository;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext context;
    private readonly IUserService _userService;

    public UsersController(
        DataContext context,
        IUserService userService)
    {
        this.context = context;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("add")]
    public IActionResult AddUser(AddUserRequest model)
    {
        _userService.AddUser(model);
        return Ok(new { message = "Add user successful" });
    }

    // GET: api/Users
    [HttpGet("page")]
    public IActionResult GetUsers([FromQuery] QueryUserRequest parameters)
    {
        var users = _userService.GetUsers(parameters);

        var metadata = new
        {
            users.TotalCount,
            users.PageSize,
            users.CurrentPage,
            users.TotalPages,
            users.HasNext,
            users.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(users);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var skill = _userService.GetAll();
        return Ok(skill);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}