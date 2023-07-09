using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Medicine;

namespace MemberEvaluationService.Services
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> GetAll();
        Medicine GetById(int id);
        void AddDept(MedicineRequest model);
        void UpdateMedicine(int id, UpdateMedicine model);
        void DeleteMedicine(int id);
    }

    public class Medicineservice : IMedicineService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public Medicineservice(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        public IEnumerable<Medicine> GetAll()
        {
            return _context.Medicines;
        }

        public Medicine GetById(int id)
        {
            return getMedicine(id);
        }

        public void AddDept(MedicineRequest model)
        {
            // validate
            if (_context.Medicines.Any(x => x.MedicineId == model.MedicineId))
                throw new AppException("MedicineName '" + model.MedicineId + "' is already taken");

            // map model to new user object
            var Medicine = _mapper.Map<Medicine>(model);

            // save user
            _context.Medicines.Add(Medicine);
            _context.SaveChanges();
        }

        private Medicine getMedicine(int id)
        {
            var Medicine = _context.Medicines.Find(id);
            if (Medicine == null) throw new KeyNotFoundException("Medicine not found");
            return Medicine;
        }

        public void UpdateMedicine(int id, UpdateMedicine model)
        {
            var Medicine = getMedicine(id);
            // validate
            if (model.MedicineId != Medicine.MedicineId && _context.Medicines.Any(x => x.MedicineId == model.MedicineId))
                throw new AppException("UserName '" + model.MedicineId + "' is already taken");

            // copy model to user and save
            _mapper.Map(model, Medicine);
            _context.Medicines.Update(Medicine);
            _context.SaveChanges();
        }

        public void DeleteMedicine(int id)
        {
            var Medicine = getMedicine(id);
            _context.Medicines.Remove(Medicine);
            _context.SaveChanges();
        }
    }
}
