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

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        List<User> users = new List<User>();

        if(this.dataContext != null && this.dataContext.Users != null)
        {
            users = await this.dataContext.Users.ToListAsync();   
        }
        return Ok(users);
    }
}