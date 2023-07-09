using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.RegistrationForm;

namespace MemberEvaluationService.Services
{
    public interface IRegistrationFormService
    {
        IEnumerable<RegistrationForm> GetAll();
        RegistrationForm GetById(int id);
        void AddDept(RegistrationFormRequest model);
        void UpdateRegistrationForm(int id, UpdateRegistrationForm model);
        void DeleteRegistrationForm(int id);
    }

    public class RegistrationFormservice : IRegistrationFormService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public RegistrationFormservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<RegistrationForm> GetAll()
        {
            return _context.RegistrationForms;
        }

        public RegistrationForm GetById(int id)
        {
            return getRegistrationForm(id);
        }

        public void AddDept(RegistrationFormRequest model)
        {
            // validate
            if (_context.RegistrationForms.Any(x => x.RegistrationFormId == model.RegistrationFormId))
                throw new AppException("RegistrationFormName '" + model.RegistrationFormId + "' is already taken");

            // map model to new user object
            var RegistrationForm = _mapper.Map<RegistrationForm>(model);

            // save user
            _context.RegistrationForms.Add(RegistrationForm);
            _context.SaveChanges();
        }

        private RegistrationForm getRegistrationForm(int id)
        {
            var RegistrationForm = _context.RegistrationForms.Find(id);
            if (RegistrationForm == null) throw new KeyNotFoundException("RegistrationForm not found");
            return RegistrationForm;
        }

        public void UpdateRegistrationForm(int id, UpdateRegistrationForm model)
        {
            var RegistrationForm = getRegistrationForm(id);
            // validate
            if (model.RegistrationFormId != RegistrationForm.RegistrationFormId && _context.RegistrationForms.Any(x => x.RegistrationFormId == model.RegistrationFormId))
                throw new AppException("UserName '" + model.RegistrationFormId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, RegistrationForm);
            _context.RegistrationForms.Update(RegistrationForm);
            _context.SaveChanges();
        }

        public void DeleteRegistrationForm(int id)
        {
            var RegistrationForm = getRegistrationForm(id);
            _context.RegistrationForms.Remove(RegistrationForm);
            _context.SaveChanges();
        }
    }
}
