using Microsoft.EntityFrameworkCore;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    // use sql server db in production and sqlite db in development
    services.AddDbContext<DataContext>();
    
    services.AddSwaggerGen();
    services.AddCors();
    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddMvc();

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAddressService, AddressService>();
    services.AddScoped<ISkillInterface, SkillService>();
    services.AddScoped<IDepartmentService, DepartmentService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IReportService, ReportService>();
}

var app = builder.Build();

// generated JSON document and Swagger UI

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();

// migrate any database changes on startup (includes initial db creation)
/*using (var scope = app.Services.CreateScope())
{
var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
dataContext.Database.Migrate();
}*/

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
//app.Run();
app.Run("http://localhost:4000");