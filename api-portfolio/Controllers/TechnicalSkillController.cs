using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.TechnicalSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.Tecnology;
namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnologyController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public TecnologyController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<Technology>> GetTecnologyByUser(long id)
    {
        User user =await this.dataContext.Users
            .Include(user => user.Technologies)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        List<TechnologyDTOResponse> technologyDTOResponses = new List<TechnologyDTOResponse>();

        foreach(Technology technology in user.Technologies){
            technologyDTOResponses.Add(new TechnologyDTOResponse{
                Id = technology.Id,
                Description = technology.Description
            });
        }
        return Ok(technologyDTOResponses);
    }
}