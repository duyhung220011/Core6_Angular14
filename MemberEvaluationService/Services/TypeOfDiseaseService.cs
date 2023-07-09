using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.TypeofDisease;

namespace MemberEvaluationService.Services
{
    public interface ITypeofDiseaseService
    {
        IEnumerable<TypeofDisease> GetAll();
        TypeofDisease GetById(int id);
        void AddDept(TypeofDiseaseRequest model);
        void UpdateTypeofDisease(int id, UpdateTypeofDisease model);
        void DeleteTypeofDisease(int id);
    }

    public class TypeofDiseaseservice : ITypeofDiseaseService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public TypeofDiseaseservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<TypeofDisease> GetAll()
        {
            return _context.TypeofDiseases;
        }

        public TypeofDisease GetById(int id)
        {
            return getTypeofDisease(id);
        }

        public void AddDept(TypeofDiseaseRequest model)
        {
            // validate
            if (_context.TypeofDiseases.Any(x => x.TypeofDiseaseId == model.TypeofDiseaseId))
                throw new AppException("TypeofDiseaseName '" + model.TypeofDiseaseId + "' is already taken");

            // map model to new user object
            var TypeofDisease = _mapper.Map<TypeofDisease>(model);

            // save user
            _context.TypeofDiseases.Add(TypeofDisease);
            _context.SaveChanges();
        }

        private TypeofDisease getTypeofDisease(int id)
        {
            var TypeofDisease = _context.TypeofDiseases.Find(id);
            if (TypeofDisease == null) throw new KeyNotFoundException("TypeofDisease not found");
            return TypeofDisease;
        }

        public void UpdateTypeofDisease(int id, UpdateTypeofDisease model)
        {
            var TypeofDisease = getTypeofDisease(id);
            // validate
            if (model.TypeofDiseaseId != TypeofDisease.TypeofDiseaseId && _context.TypeofDiseases.Any(x => x.TypeofDiseaseId == model.TypeofDiseaseId))
                throw new AppException("UserName '" + model.TypeofDiseaseId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, TypeofDisease);
            _context.TypeofDiseases.Update(TypeofDisease);
            _context.SaveChanges();
        }

        public void DeleteTypeofDisease(int id)
        {
            var TypeofDisease = getTypeofDisease(id);
            _context.TypeofDiseases.Remove(TypeofDisease);
            _context.SaveChanges();
        }
    }
}
