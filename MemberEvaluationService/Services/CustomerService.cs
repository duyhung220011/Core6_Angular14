using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Customer;

namespace MemberEvaluationService.Services;


public interface ICustomerService
{
    IEnumerable<Customer> GetAll();
    Customer GetById(int id);
    void AddDept(CustomerRequest model);
    void UpdateCustomer(int id, UpdateCustomer model);
    void DeleteCustomer(int id);
}

public class CustomerService : ICustomerService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public CustomerService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }


    public IEnumerable<Customer> GetAll()
    {
        return _context.Customers;
    }

    public Customer GetById(int id)
    {
        return getCustomer(id);
    }

    public void AddDept(CustomerRequest model)
    {
        // validate
        if (_context.Customers.Any(x => x.CustomerId == model.CustomerId))
            throw new AppException("CustomerName '" + model.CustomerId + "' is already taken");

        // map model to new user object
        var Customer = _mapper.Map<Customer>(model);

        // save user
        _context.Customers.Add(Customer);
        _context.SaveChanges();
    }

    private Customer getCustomer(int id)
    {
        var Customer = _context.Customers.Find(id);
        if (Customer == null) throw new KeyNotFoundException("Customer not found");
        return Customer;
    }

    public void UpdateCustomer(int id, UpdateCustomer model)
    {
        var Customer = getCustomer(id);
        // validate
        if (model.CustomerId != Customer.CustomerId && _context.Customers.Any(x => x.CustomerId == model.CustomerId))
            throw new AppException("UserName '" + model.CustomerId + "' is already taken");

        // copy model to user and save
        _mapper.Map(model, Customer);
        _context.Customers.Update(Customer);
        _context.SaveChanges();
    }

    public void DeleteCustomer(int id)
    {
        var Customer = getCustomer(id);
        _context.Customers.Remove(Customer);
        _context.SaveChanges();
    }
}
