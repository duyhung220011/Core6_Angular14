namespace MemberEvaluationService.Services;

using AutoMapper;
using BCrypt.Net;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Department;

public interface IDepartmentService
{
    IEnumerable<Department> GetAll();
    Department GetById(int id);
    void AddDept(DepartmentRequest model);
    void UpdateDepartment(int id, DepartmentRequest model);
    void DeleteDepartment(int id);
}

public class DepartmentService : IDepartmentService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public DepartmentService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }


    public IEnumerable<Department> GetAll()
    {
        return _context.Departments;
    }

    public Department GetById(int id)
    {
        return getDepartment(id);
    }
    
    public void AddDept(DepartmentRequest model)
    {
       // validate
            if (_context.Departments.Any(x => x.DepartmentId == model.DepartmentId))
            throw new AppException("DepartmentName '" + model.DepartmentId + "' is already taken");

        // map model to new user object
        var department = _mapper.Map<Department>(model);

        // save user
        _context.Departments.Add(department);
        _context.SaveChanges();
    }

    public void UpdateDepartment(int id, DepartmentRequest model)
    {
        var department = getDepartment(id);
        // validate
        if (model.DepartmentId != department.DepartmentId && _context.Departments.Any(x => x.DepartmentId == model.DepartmentId))
            throw new AppException("UserName '" + model.DepartmentId + "' is already taken");

        // copy model to user and save
        _mapper.Map(model, department);
        _context.Departments.Update(department);
        _context.SaveChanges();
    }

    public void DeleteDepartment(int id)
    {
        var department = getDepartment(id);
        _context.Departments.Remove(department);
        _context.SaveChanges();
    }
    private Department getDepartment(int id)
    {
        var department = _context.Departments.Find(id);
        if (department == null) throw new KeyNotFoundException("Department not found");
        return department;
    }
    
}
