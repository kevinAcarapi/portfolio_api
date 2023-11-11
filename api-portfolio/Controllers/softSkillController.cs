using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.SoftSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.SoftSkill;
namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class SoftSkillController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public SoftSkillController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<SoftSkill>> GetSoftSkillByUser(long id)
    {
        User user =await this.dataContext.Users
            .Include(user => user.SoftSkills)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        List<SoftSkillResponseDTO> softSkillResponseDTOs = new List<SoftSkillResponseDTO>();

        foreach(SoftSkill softSkill in user.SoftSkills){
            softSkillResponseDTOs.Add(new SoftSkillResponseDTO{
                Id = softSkill.Id,
                Description = softSkill.Description
            });
        }
        return Ok(softSkillResponseDTOs);
    }
}