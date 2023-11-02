using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext dataContext;

    public UserController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{idType}")]
    public async Task<ActionResult<List<User>>> Get(long idType)
    {
        List<User> users = new List<User>();

        if (this.dataContext != null && this.dataContext.Users != null && this.dataContext.Technologies != null)
        {
            Technology? technology = await this.dataContext.Technologies.FindAsync(idType);
            users = await this.dataContext.Users.Where(user => user.Id == idType).ToListAsync();
        }
        return Ok(users);
    }
}