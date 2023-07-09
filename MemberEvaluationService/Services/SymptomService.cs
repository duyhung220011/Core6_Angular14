using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Symptom;

namespace MemberEvaluationService.Services
{
    public interface ISymptomService
    {
        IEnumerable<Symptom> GetAll();
        Symptom GetById(int id);
        void AddDept(SymptomRequest model);
        void UpdateSymptom(int id, UpdateSymptom model);
        void DeleteSymptom(int id);
    }

    public class Symptomservice : ISymptomService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public Symptomservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Symptom> GetAll()
        {
            return _context.Symptoms;
        }

        public Symptom GetById(int id)
        {
            return getSymptom(id);
        }

        public void AddDept(SymptomRequest model)
        {
            // validate
            if (_context.Symptoms.Any(x => x.SymptomId == model.SymptomId))
                throw new AppException("SymptomName '" + model.SymptomId + "' is already taken");

            // map model to new user object
            var Symptom = _mapper.Map<Symptom>(model);

            // save user
            _context.Symptoms.Add(Symptom);
            _context.SaveChanges();
        }

        private Symptom getSymptom(int id)
        {
            var Symptom = _context.Symptoms.Find(id);
            if (Symptom == null) throw new KeyNotFoundException("Symptom not found");
            return Symptom;
        }

        public void UpdateSymptom(int id, UpdateSymptom model)
        {
            var Symptom = getSymptom(id);
            // validate
            if (model.SymptomId != Symptom.SymptomId && _context.Symptoms.Any(x => x.SymptomId == model.SymptomId))
                throw new AppException("UserName '" + model.SymptomId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, Symptom);
            _context.Symptoms.Update(Symptom);
            _context.SaveChanges();
        }

        public void DeleteSymptom(int id)
        {
            var Symptom = getSymptom(id);
            _context.Symptoms.Remove(Symptom);
            _context.SaveChanges();
        }
    }
}
