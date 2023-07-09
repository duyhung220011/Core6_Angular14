using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Hospital;

namespace MemberEvaluationService.Services
{
    public interface IHospitalService
    {
        IEnumerable<Hospital> GetAll();
        Hospital GetById(int id);
        void AddDept(HospitalRequest model);
        void UpdateHospital(int id, UpdateHospital model);
        void DeleteHospital(int id);
    }

    public class Hospitalservice : IHospitalService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public Hospitalservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Hospital> GetAll()
        {
            return _context.Hospitals;
        }

        public Hospital GetById(int id)
        {
            return getHospital(id);
        }

        public void AddDept(HospitalRequest model)
        {
            // validate
            if (_context.Hospitals.Any(x => x.HospitalId == model.HospitalId))
                throw new AppException("HospitalName '" + model.HospitalId + "' is already taken");

            // map model to new user object
            var Hospital = _mapper.Map<Hospital>(model);

            // save user
            _context.Hospitals.Add(Hospital);
            _context.SaveChanges();
        }

        private Hospital getHospital(int id)
        {
            var Hospital = _context.Hospitals.Find(id);
            if (Hospital == null) throw new KeyNotFoundException("Hospital not found");
            return Hospital;
        }

        public void UpdateHospital(int id, UpdateHospital model)
        {
            var Hospital = getHospital(id);
            // validate
            if (model.HospitalId != Hospital.HospitalId && _context.Hospitals.Any(x => x.HospitalId == model.HospitalId))
                throw new AppException("UserName '" + model.HospitalId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, Hospital);
            _context.Hospitals.Update(Hospital);
            _context.SaveChanges();
        }

        public void DeleteHospital(int id)
        {
            var Hospital = getHospital(id);
            _context.Hospitals.Remove(Hospital);
            _context.SaveChanges();
        }
    }
}
