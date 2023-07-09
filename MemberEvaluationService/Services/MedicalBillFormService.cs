using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.MedicalBillForm;

namespace MemberEvaluationService.Services
{
    public interface IMedicalBillFormService
    {
        IEnumerable<MedicalBillForm> GetAll();
        MedicalBillForm GetById(int id);
        void AddDept(MedicalBillFormRequest model);
        void UpdateMedicalBillForm(int id, UpdateMedicalBillForm model);
        void DeleteMedicalBillForm(int id);
    }

    public class MedicalBillFormservice : IMedicalBillFormService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MedicalBillFormservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<MedicalBillForm> GetAll()
        {
            return _context.MedicalBillForms;
        }

        public MedicalBillForm GetById(int id)
        {
            return getMedicalBillForm(id);
        }

        public void AddDept(MedicalBillFormRequest model)
        {
            // validate
            if (_context.MedicalBillForms.Any(x => x.MedicalBillFormId == model.MedicalBillFormId))
                throw new AppException("MedicalBillFormName '" + model.MedicalBillFormId + "' is already taken");

            // map model to new user object
            var MedicalBillForm = _mapper.Map<MedicalBillForm>(model);

            // save user
            _context.MedicalBillForms.Add(MedicalBillForm);
            _context.SaveChanges();
        }

        private MedicalBillForm getMedicalBillForm(int id)
        {
            var MedicalBillForm = _context.MedicalBillForms.Find(id);
            if (MedicalBillForm == null) throw new KeyNotFoundException("MedicalBillForm not found");
            return MedicalBillForm;
        }

        public void UpdateMedicalBillForm(int id, UpdateMedicalBillForm model)
        {
            var MedicalBillForm = getMedicalBillForm(id);
            // validate
            if (model.MedicalBillFormId != MedicalBillForm.MedicalBillFormId && _context.MedicalBillForms.Any(x => x.MedicalBillFormId == model.MedicalBillFormId))
                throw new AppException("UserName '" + model.MedicalBillFormId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, MedicalBillForm);
            _context.MedicalBillForms.Update(MedicalBillForm);
            _context.SaveChanges();
        }

        public void DeleteMedicalBillForm(int id)
        {
            var MedicalBillForm = getMedicalBillForm(id);
            _context.MedicalBillForms.Remove(MedicalBillForm);
            _context.SaveChanges();
        }
    }
}
