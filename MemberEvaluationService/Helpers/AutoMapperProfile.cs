namespace WebApi.Helpers;

using AutoMapper;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Models.Skills;
using MemberEvaluationService.Models.Department;
using MemberEvaluationService.Models.Users;
using MemberEvaluationService.Models.Role;
using MemberEvaluationService.Models.Report;
using MemberEvaluationService.Models.Address;
using MemberEvaluationService.Models.Customer;
using MemberEvaluationService.Models.Doctor;
using MemberEvaluationService.Models.Employee;
using MemberEvaluationService.Models.Hospital;
using MemberEvaluationService.Models.MedicalBillForm;
using MemberEvaluationService.Models.Medicine;
using MemberEvaluationService.Models.RegistrationForm;
using MemberEvaluationService.Models.Service;
using MemberEvaluationService.Models.Symptom;
using MemberEvaluationService.Models.TypeofDisease;
using MemberEvaluationService.Models.TypeOfMedicine;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

        CreateMap<AddUserRequest, User>();

        CreateMap<QueryUserRequest, User>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
        // AddressRequest
        CreateMap<AddressRequest, Address>();

        // AddressRequest -> Address
        CreateMap<UpdateAddress, Address>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // CustomerRequest
        CreateMap<CustomerRequest, Customer>();

        // CustomerRequest -> Customer
        CreateMap<UpdateCustomer, Customer >()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // CustomerRequest
        CreateMap<DoctorRequest, Doctor>();

        // CustomerRequest -> Customer
        CreateMap<UpdateDoctor, Doctor>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // CustomerRequest
        CreateMap<EmployeeRequest, Employee>();

        // CustomerRequest -> Customer
        CreateMap<UpdateEmployee, Employee>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // HospitalRequest
        CreateMap<HospitalRequest, Hospital>();

        // HospitalRequest -> Hospital
        CreateMap<UpdateHospital, Hospital>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // MedicalBillFormRequest
        CreateMap<MedicalBillFormRequest, MedicalBillForm>();

        // MedicalBillFormRequest -> Customer
        CreateMap<UpdateMedicalBillForm, MedicalBillForm>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // MedicineRequest
        CreateMap<MedicineRequest, Medicine>();

        // MedicineRequest -> Medicine
        CreateMap<UpdateMedicine, Medicine>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // RegistrationFormRequest
        CreateMap<RegistrationFormRequest, RegistrationForm>();

        // RegistrationFormRequest -> RegistrationForm
        CreateMap<UpdateRegistrationForm, RegistrationForm>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));


        // ServiceRequest
        CreateMap<ServiceRequest, Service>();

        // RegistrationFormRequest -> RegistrationForm
        CreateMap<UpdateService, Service>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // ServiceRequest
        CreateMap<SymptomRequest, Symptom>();

        // SymptomRequest -> Symptom
        CreateMap<UpdateSymptom, Symptom>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));


        // TypeofDiseaseRequest
        CreateMap<TypeofDiseaseRequest, TypeofDisease>();

        // TypeofDiseaseRequest -> Symptom
        CreateMap<UpdateTypeofDisease, TypeofDisease>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // TypeOfMedicineRequest
        CreateMap<TypeOfMedicineRequest, TypeOfMedicine>();

        // TypeOfMedicineRequest -> TypeOfMedicine
        CreateMap<UpdateTypeOfMedicine, TypeOfMedicine>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));


        CreateMap<SkillRequest, Skill>();

        // DepartmentRequest
        CreateMap<DepartmentRequest, Department>();

        // DepartmentRequest -> Department
        CreateMap<DepartmentUpdate, Department>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));



        //create Role
        CreateMap<RoleRequest, Role>();

        // RoleRequest -> Role
        CreateMap<RoleUpdate, Role>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        //create Role
        CreateMap<SkillRequest, Skill>();

        // RoleRequest -> Role
        CreateMap<UpdateSkillRequest, Skill>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
        //create Report
        CreateMap<ReportRequest, Report>();

        // RoleRequest -> Role
        CreateMap<ReportUpdate, Report>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}