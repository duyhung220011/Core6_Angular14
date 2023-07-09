using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Employee;

namespace MemberEvaluationService.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void AddDept(EmployeeRequest model);
        void UpdateEmployee(int id, UpdateEmployee model);
        void DeleteEmployee(int id);
    }

    public class Employeeservice : IEmployeeService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public Employeeservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public Employee GetById(int id)
        {
            return getEmployee(id);
        }

        public void AddDept(EmployeeRequest model)
        {
            // validate
            if (_context.Employees.Any(x => x.EmployeeId == model.EmployeeId))
                throw new AppException("EmployeeName '" + model.EmployeeId + "' is already taken");

            // map model to new user object
            var Employee = _mapper.Map<Employee>(model);

            // save user
            _context.Employees.Add(Employee);
            _context.SaveChanges();
        }

        private Employee getEmployee(int id)
        {
            var Employee = _context.Employees.Find(id);
            if (Employee == null) throw new KeyNotFoundException("Employee not found");
            return Employee;
        }

        public void UpdateEmployee(int id, UpdateEmployee model)
        {
            var Employee = getEmployee(id);
            // validate
            if (model.EmployeeId != Employee.EmployeeId && _context.Employees.Any(x => x.EmployeeId == model.EmployeeId))
                throw new AppException("UserName '" + model.EmployeeId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, Employee);
            _context.Employees.Update(Employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var Employee = getEmployee(id);
            _context.Employees.Remove(Employee);
            _context.SaveChanges();
        }
    }
}
