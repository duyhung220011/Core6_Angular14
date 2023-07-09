namespace MemberEvaluationService.Services;
using AutoMapper;
using BCrypt.Net;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Address;
public interface IAddressService
{
    IEnumerable<Address> GetAll();
    Address GetById(int id);
    void AddDept(AddressRequest model);
    void UpdateAddress(int id, UpdateAddress model);
    void DeleteAddress(int id);
}

public class AddressService : IAddressService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public AddressService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }


    public IEnumerable<Address> GetAll()
    {
        return _context.Addresses;
    }

    public Address GetById(int id)
    {
        return getAddress(id);
    }

    public void AddDept(AddressRequest model)
    {
        // validate
        if (_context.Addresses.Any(x => x.AddressId == model.AddressId))
            throw new AppException("AddressName '" + model.AddressId + "' is already taken");

        // map model to new user object
        var address = _mapper.Map<Address>(model);

        // save user
        _context.Addresses.Add(address);
        _context.SaveChanges();
    }

    private Address getAddress(int id)
    {
        var address = _context.Addresses.Find(id);
        if (address == null) throw new KeyNotFoundException("Address not found");
        return address;
    }

    public void UpdateAddress(int id, UpdateAddress model)
    {
        var address = getAddress(id);
        // validate
        if (model.AddressId != address.AddressId && _context.Addresses.Any(x => x.AddressId == model.AddressId))
            throw new AppException("UserName '" + model.AddressId + "' is already taken");

        // copy model to user and save
        _mapper.Map(model, address);
        _context.Addresses.Update(address);
        _context.SaveChanges();
    }

    public void DeleteAddress(int id)
    {
        var address = getAddress(id);
        _context.Addresses.Remove(address);
        _context.SaveChanges();
    }
}
