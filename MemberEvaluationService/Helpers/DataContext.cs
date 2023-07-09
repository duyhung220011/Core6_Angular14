namespace MemberEvaluationService.Helpers;

using Microsoft.EntityFrameworkCore;
using MemberEvaluationService.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<MedicalBillForm> MedicalBillForms { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<RegistrationForm> RegistrationForms { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Symptom> Symptoms { get; set; }
    public DbSet<TypeofDisease> TypeofDiseases { get; set; }
    public DbSet<TypeOfMedicine> TypeOfMedicines { get; set; }
}