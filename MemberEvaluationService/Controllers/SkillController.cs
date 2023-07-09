using AutoMapper;
using MemberEvaluationService.Authorization;
using MemberEvaluationService.Helpers;
using MemberEvaluationService.Models.Role;
using MemberEvaluationService.Models.Skills;
using MemberEvaluationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MemberEvaluationService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillInterface _skillInterface;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public SkillController( ISkillInterface skillInterface, IMapper mapper, IOptions<AppSettings> appSettings )
        {
        _skillInterface = skillInterface;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        }

        [HttpPost("add")]
        public IActionResult AddSkill(SkillRequest model)
            {
                _skillInterface.AddSkill(model);
                return Ok(new { message = "Skill added succesfull"});
            }
        [HttpGet]
        public IActionResult GetAll()
        {
            var skill = _skillInterface.GetAll();
            return Ok(skill);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skill = _skillInterface.GetById(id);
            return Ok(skill);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSkill(int id, UpdateSkillRequest model)
        {
            _skillInterface.UpdateSkill(id, model);
            return Ok(new { message = "Skill updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {
            _skillInterface.DeleteSkill(id);
            return Ok(new { message = "skill deleted successfully" });
        }
    }
}
