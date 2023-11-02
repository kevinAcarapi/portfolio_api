using api_portafolio.Entities.Skills.SoftSkills;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class SoftSkillController : ControllerBase
{
    private readonly DataContext dataContext;

    public SoftSkillController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<SoftSkill>>> Get()
    {
        List<SoftSkill> softSkills = new List<SoftSkill>();

        if(this.dataContext != null && this.dataContext.Softskills != null)
        {
            softSkills = await this.dataContext.Softskills.ToListAsync();
        }
        return Ok(softSkills);
    }
}