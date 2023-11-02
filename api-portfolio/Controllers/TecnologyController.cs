using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class TechnologyController : ControllerBase
{
    private readonly DataContext dataContext;

    public TechnologyController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Technology>>> Get()
    {
        List<Technology> technologies = new List<Technology>();

        if(this.dataContext != null && this.dataContext.Technologies != null)
        {
            technologies = await this.dataContext.Technologies.ToListAsync();
        }
        return Ok(technologies);
    }
}