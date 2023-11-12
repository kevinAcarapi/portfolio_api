using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.DTO.User;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public UserController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<User>> GetUser(long id)
    // {
    //     User? dbUser = await this.dataContext.Users.Include(dbUser => dbUser.TechnologiesByUser).ToListAsync(id);
    //     if(dbUser == null)
    //     {
    //         return NotFound("Usuario no encontrado");
    //     }

    //     return Ok(dbUser);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(long id)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        List<User> lista = new List<User>();
        
        User user = await this.dataContext.Users
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if(user == null){
            return NotFound("Usuario no encontrado");
        }
        UserResponseDTO userResponseDTO = new UserResponseDTO{
            Apellido = user.Apellido,
            Nombre = user.Nombre,
            Description = user.Description,
            ProfilePhoto = user.ProfilePhoto,
        };
        
        return Ok(userResponseDTO);
    }

    // [HttpPost]
    // public async Task<ActionResult<User>> Post([FromBody])
}