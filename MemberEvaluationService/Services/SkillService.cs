namespace MemberEvaluationService.Services;

using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Entities;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Skills;

public interface ISkillInterface
    {
        IEnumerable<Skill> GetAll();
        Skill GetById(int id);
        void AddSkill(SkillRequest model);
        void UpdateSkill(int id, UpdateSkillRequest model);
        void DeleteSkill(int id);
    }


public class SkillService : ISkillInterface
{
    private readonly DataContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public SkillService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public void AddSkill(SkillRequest model)
    {
        // validate
        if (_context.Skills.Any(x => x.SkillId == model.SkillId))
            throw new AppException("RoleName '" + model.SkillId + "' is already taken");

        // map model to new user object
  
        var skill = _mapper.Map<Skill>(model);

            _context.Add(skill);
            _context.SaveChanges();
    }

    public void DeleteSkill(int id)
    {
        var skill = getSkill(id);
        _context.Skills.Remove(skill);
        _context.SaveChanges();
    }

    public IEnumerable<Skill> GetAll()
    {
        return _context.Skills;
    }

    public Skill GetById(int id)
    {
        return getSkill(id);
    }

    public void UpdateSkill(int id, UpdateSkillRequest model)
    {
        var skill = getSkill(id);

        // validate
        if (model.SkillId != skill.SkillId && _context.Skills.Any(x => x.SkillId == model.SkillId))
            throw new AppException("SkillName '" + model.SkillId + "' is already taken");


        // copy model to user and save
        _mapper.Map(model, skill);
        _context.Skills.Update(skill);
        _context.SaveChanges();
    }
    private Skill getSkill(int id)
    {
        var skill = _context.Skills.Find(id);
        if (skill == null) throw new KeyNotFoundException("Skilll not found");
        return skill;
    }

}

