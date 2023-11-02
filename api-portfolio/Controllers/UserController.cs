using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Skills.Users_TechnicalSkills;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(long id)
    {
        User user = await this.dataContext.Users
            .FindAsync(id);
        
        return Ok(user);
    }
}