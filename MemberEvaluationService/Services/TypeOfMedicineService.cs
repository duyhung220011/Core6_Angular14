using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.TypeOfMedicine;

namespace MemberEvaluationService.Services
{
    public interface ITypeOfMedicineService
    {
        IEnumerable<TypeOfMedicine> GetAll();
        TypeOfMedicine GetById(int id);
        void AddDept(TypeOfMedicineRequest model);
        void UpdateTypeOfMedicine(int id, UpdateTypeOfMedicine model);
        void DeleteTypeOfMedicine(int id);
    }

    public class TypeOfMedicineservice : ITypeOfMedicineService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public TypeOfMedicineservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<TypeOfMedicine> GetAll()
        {
            return _context.TypeOfMedicines;
        }

        public TypeOfMedicine GetById(int id)
        {
            return getTypeOfMedicine(id);
        }

        public void AddDept(TypeOfMedicineRequest model)
        {
            // validate
            if (_context.TypeOfMedicines.Any(x => x.TypeOfMedicineId == model.TypeOfMedicineId))
                throw new AppException("TypeOfMedicineName '" + model.TypeOfMedicineId + "' is already taken");

            // map model to new user object
            var TypeOfMedicine = _mapper.Map<TypeOfMedicine>(model);

            // save user
            _context.TypeOfMedicines.Add(TypeOfMedicine);
            _context.SaveChanges();
        }

        private TypeOfMedicine getTypeOfMedicine(int id)
        {
            var TypeOfMedicine = _context.TypeOfMedicines.Find(id);
            if (TypeOfMedicine == null) throw new KeyNotFoundException("TypeOfMedicine not found");
            return TypeOfMedicine;
        }

        public void UpdateTypeOfMedicine(int id, UpdateTypeOfMedicine model)
        {
            var TypeOfMedicine = getTypeOfMedicine(id);
            // validate
            if (model.TypeOfMedicineId != TypeOfMedicine.TypeOfMedicineId && _context.TypeOfMedicines.Any(x => x.TypeOfMedicineId == model.TypeOfMedicineId))
                throw new AppException("UserName '" + model.TypeOfMedicineId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, TypeOfMedicine);
            _context.TypeOfMedicines.Update(TypeOfMedicine);
            _context.SaveChanges();
        }

        public void DeleteTypeOfMedicine(int id)
        {
            var TypeOfMedicine = getTypeOfMedicine(id);
            _context.TypeOfMedicines.Remove(TypeOfMedicine);
            _context.SaveChanges();
        }
    }
}
