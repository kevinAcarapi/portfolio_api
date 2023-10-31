using api_portafolio.entities.users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class AboutMeController : ControllerBase
{
    private readonly DataContext dataContext;

    public AboutMeController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<AboutMe>>> Get()
    {
        List<AboutMe> aboutUs = new List<AboutMe>();

        if(this.dataContext != null && this.dataContext.AboutUs != null)
        {
            aboutUs = await this.dataContext.AboutUs.ToListAsync();   
        }
        return Ok(aboutUs);
    }
}
