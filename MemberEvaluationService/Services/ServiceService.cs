using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Service;

namespace MemberEvaluationService.Services
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAll();
        Service GetById(int id);
        void AddDept(ServiceRequest model);
        void UpdateService(int id, UpdateService model);
        void DeleteService(int id);
    }

    public class Serviceservice : IServiceService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public Serviceservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Service> GetAll()
        {
            return _context.Services;
        }

        public Service GetById(int id)
        {
            return getService(id);
        }

        public void AddDept(ServiceRequest model)
        {
            // validate
            if (_context.Services.Any(x => x.ServiceId == model.ServiceId))
                throw new AppException("ServiceName '" + model.ServiceId + "' is already taken");

            // map model to new user object
            var Service = _mapper.Map<Service>(model);

            // save user
            _context.Services.Add(Service);
            _context.SaveChanges();
        }

        private Service getService(int id)
        {
            var Service = _context.Services.Find(id);
            if (Service == null) throw new KeyNotFoundException("Service not found");
            return Service;
        }

        public void UpdateService(int id, UpdateService model)
        {
            var Service = getService(id);
            // validate
            if (model.ServiceId != Service.ServiceId && _context.Services.Any(x => x.ServiceId == model.ServiceId))
                throw new AppException("UserName '" + model.ServiceId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, Service);
            _context.Services.Update(Service);
            _context.SaveChanges();
        }

        public void DeleteService(int id)
        {
            var Service = getService(id);
            _context.Services.Remove(Service);
            _context.SaveChanges();
        }
    }
}
