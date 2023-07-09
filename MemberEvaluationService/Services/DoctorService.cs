using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Doctor;

namespace MemberEvaluationService.Services;


public interface IDoctorService
{
    IEnumerable<Doctor> GetAll();
    Doctor GetById(int id);
    void AddDept(DoctorRequest model);
    void UpdateDoctor(int id, UpdateDoctor model);
    void DeleteDoctor(int id);
}

public class Doctorservice : IDoctorService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public Doctorservice(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }


    public IEnumerable<Doctor> GetAll()
    {
        return _context.Doctors;
    }

    public Doctor GetById(int id)
    {
        return getDoctor(id);
    }

    public void AddDept(DoctorRequest model)
    {
        // validate
        if (_context.Doctors.Any(x => x.DoctorId == model.DoctorId))
            throw new AppException("DoctorName '" + model.DoctorId + "' is already taken");

        // map model to new user object
        var Doctor = _mapper.Map<Doctor>(model);

        // save user
        _context.Doctors.Add(Doctor);
        _context.SaveChanges();
    }

    private Doctor getDoctor(int id)
    {
        var Doctor = _context.Doctors.Find(id);
        if (Doctor == null) throw new KeyNotFoundException("Doctor not found");
        return Doctor;
    }

    public void UpdateDoctor(int id, UpdateDoctor model)
    {
        var Doctor = getDoctor(id);
        // validate
        if (model.DoctorId != Doctor.DoctorId && _context.Doctors.Any(x => x.DoctorId == model.DoctorId))
            throw new AppException("UserName '" + model.DoctorId + "' is already taken");

        // copy model to user and save
        _mapper.Map(model, Doctor);
        _context.Doctors.Update(Doctor);
        _context.SaveChanges();
    }

    public void DeleteDoctor(int id)
    {
        var Doctor = getDoctor(id);
        _context.Doctors.Remove(Doctor);
        _context.SaveChanges();
    }
}

