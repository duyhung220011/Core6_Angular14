namespace MemberEvaluationService.Services;

using AutoMapper;
using BCrypt.Net;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Report;

public interface IReportService
{
    IEnumerable<Report> GetAll();
    Report GetById(int id);
    void AddDept(ReportRequest model);
    void UpdateReport(int id, ReportUpdate model);
    void DeleteReport(int id);
}

public class ReportService : IReportService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public ReportService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public IEnumerable<Report> GetAll()
    {
        return _context.Reports;
    }

    public Report GetById(int id)
    {
        return getReport(id);
    }

    public void AddDept(ReportRequest model)
    {
        // validate
        if (_context.Reports.Any(x => x.ReportId == model.ReportId))
            throw new AppException("Tilte '" + model.ReportId + "' is already taken");

        // map model to new user object
        var report = _mapper.Map<Report>(model);

        // save user
        _context.Reports.Add(report);
        _context.SaveChanges();
    }

    public void UpdateReport(int id, ReportUpdate model)
    {
        var report = getReport(id);
        // validate
        if (model.ReportId != report.ReportId && _context.Reports.Any(x => x.ReportId == model.ReportId))
            throw new AppException("UserName '" + model.ReportId + "' is already taken");

        // copy model to user and save
        _mapper.Map(model, report);
        _context.Reports.Update(report);
        _context.SaveChanges();
    }

    public void DeleteReport(int id)
    {
        var report = getReport(id);
        _context.Reports.Remove(report);
        _context.SaveChanges();
    }

    private Report getReport(int id)
    {
        var report = _context.Reports.Find(id);
        if (report == null) throw new KeyNotFoundException("Report not found");
        return report;
    }

}
