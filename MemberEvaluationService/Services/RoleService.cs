namespace MemberEvaluationService.Services;

using AutoMapper;
using BCrypt.Net;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Role;

public interface IRoleService
{
    IEnumerable<Role> GetAll();
    Role GetById(int id);
    void AddRole(RoleRequest model);
    void UpdateRole(int id, RoleUpdate model);
    void DeleteRole(int id);
}

public class RoleService : IRoleService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public RoleService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public IEnumerable<Role> GetAll()
    {
        return _context.Roles;
    }

    public Role GetById(int id)
    {
        return getRole(id);
    }

    public void AddRole(RoleRequest model)
    {
        // validate
        if (_context.Roles.Any(x => x.RoleId == model.RoleId))
            throw new AppException("RoleName '" + model.RoleId + "' is already taken");

        // map model to new user object
        var role = _mapper.Map<Role>(model);

        // save user
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void UpdateRole(int id, RoleUpdate model)
    {
        var role = getRole(id);

        // validate
        if (model.RoleId != role.RoleId && _context.Roles.Any(x => x.RoleId == model.RoleId))
            throw new AppException("RoleName '" + model.RoleId + "' is already taken");


        // copy model to user and save
        _mapper.Map(model, role);
        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    public void DeleteRole(int id)
    {
        var role = getRole(id);
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }
    private Role getRole(int id)
    {
        var role = _context.Roles.Find(id);
        if (role == null) throw new KeyNotFoundException("Role not found");
        return role;
    }

}
