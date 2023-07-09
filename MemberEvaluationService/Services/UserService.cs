namespace MemberEvaluationService.Services;

using AutoMapper;
using BCrypt.Net;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Users;
using MemberEvaluationService.Models.Pagination;
using System;
using MemberEvaluationService.Repository;

public interface IUserService : IRepositoryBase<User>
{
    IEnumerable<User> GetAll();
    PageList<User> GetUsers(QueryUserRequest parameters);
    PageList<User> GetUsersByCondition(QueryUserRequest parameters);
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    User GetById(int id);
    void Register(RegisterRequest model);
    void AddUser(AddUserRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class UserService : RepositoryBase<User>, IUserService
{
    private readonly DataContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IJwtUtils jwtUtils, IMapper mapper) : base(context)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public UserService(DataContext dataContext) : base(dataContext)
    {
    }

    IEnumerable<User> IUserService.GetAll()
    {
        return _context.Users;
    }

    public PageList<User> GetUsers(QueryUserRequest parameters)
    {
        return PageList<User>.ToPagedList(FindAll().OrderBy(on => on.FullName),
                parameters.PageNumber,
                parameters.PageSize);
    }

    public PageList<User> GetUsersByCondition(QueryUserRequest parameters)
    {
        var users = FindByCondition(a => a.FullName.Equals(parameters));

        SearchByName(ref users, parameters.FullName);

        return PageList<User>.ToPagedList(users.OrderBy(on => on.FullName),
                parameters.PageNumber,
                parameters.PageSize);
    }

    private void SearchByName(ref IQueryable<User> users, string fullName)
    {
        if (!users.Any() || string.IsNullOrWhiteSpace(fullName))
            return;

        users = users.Where(o => o.FullName.ToLower().Contains(fullName.Trim().ToLower()));
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.UserId == model.UserId);

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            throw new AppException("UserName or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Register(RegisterRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.UserId == model.UserId))
            throw new AppException("UserName '" + model.UserId + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void AddUser(AddUserRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.UserId == model.UserId))
            throw new AppException("UserName '" + model.UserId + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // validate
        if (model.UserId != user.UserId && _context.Users.Any(x => x.UserId == model.UserId))
            throw new AppException("UserName '" + model.UserId + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private User getUser(int id)
    {
      
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
    
}