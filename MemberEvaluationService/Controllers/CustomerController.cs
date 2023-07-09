using AutoMapper;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Customer;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers;


[ApiController]
[Route("[controller]")]
public class CustomerController : Controller
{
    private ICustomerService _customerService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public CustomerController(
        ICustomerService customerService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _customerService = customerService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }


    [HttpPost("add")]
    public IActionResult AddDept(CustomerRequest model)
    {
        _customerService.AddDept(model);
        return Ok(new { message = "Add successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var customer = _customerService.GetById(id);
        return Ok(customer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, UpdateCustomer model)
    {
        _customerService.UpdateCustomer(id, model);
        return Ok(new { message = "Customer updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        _customerService.DeleteCustomer(id);
        return Ok(new { message = "Customer deleted successfully" });
    }
}
