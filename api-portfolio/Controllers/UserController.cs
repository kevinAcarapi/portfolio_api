using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portafolio.Entities.Users;
using api_portafolio.DTO.Tecnology;
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

    //Metodo Get

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

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }
        UserResponseDTO userResponseDTO = new UserResponseDTO
        {
            Id = user.Id,
            Apellido = user.Apellido,
            Nombre = user.Nombre,
            Description = user.Description,
            Curriculum = user.Curriculum,
            Gmail = user.Gmail,
            Profesion = user.Profesion
        };

        return Ok(userResponseDTO);
    }

    // Metodo Post
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] UserResponseDTO userResponseDTO)
    {
        if(userResponseDTO.Image == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "profilePhotos", userResponseDTO.Image.FileName);

        using(var stream = new FileStream(path, FileMode.Create))
        {
            await userResponseDTO.Image.CopyToAsync(stream);
        };

        User user = new User{
            Id = userResponseDTO.Id,
            Apellido = userResponseDTO.Apellido,
            Nombre = userResponseDTO.Nombre,
            Description = userResponseDTO.Description,
            Curriculum = userResponseDTO.Curriculum,
            Gmail = userResponseDTO.Gmail,
            Profesion = userResponseDTO.Profesion,
            Image = new api_portafolio.Entities.Common.Image 
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
        };

        await this.dataContext.Users.AddAsync(user);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    // Metodo Put
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDTO>> Put(
        [FromRoute] long id,
        [FromForm] UserResponseDTO userResponseDTO)
    {
        User? dbUser = await this.dataContext.Users.FindAsync(id);
        if(dbUser == null)
        {
            return NotFound("Usuario no encontrado");
        }
        
        dbUser.Nombre = userResponseDTO.Nombre;
        dbUser.Apellido = userResponseDTO.Apellido;
        dbUser.Curriculum = userResponseDTO.Curriculum;
        dbUser.Profesion = userResponseDTO.Profesion;
        dbUser.Description = userResponseDTO.Description;
        dbUser.Gmail = userResponseDTO.Gmail;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "profilePhotos", userResponseDTO.Image.FileName);

        using(var stream = new FileStream(path, FileMode.Create))
        {
            await userResponseDTO.Image.CopyToAsync(stream);
        };

        dbUser.Image = new api_portafolio.Entities.Common.Image 
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            };

        await this.dataContext.SaveChangesAsync();

        return Ok(dbUser);
    }   

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        if (this.dataContext == null || this.dataContext.Users == null)
        {
            return StatusCode(500, "Error interno del servidor");
        }

        User dbUser = await this.dataContext.Users
            .Include(user => user.Image)
            .Include(user => user.Projects)
            .Include(user => user.SoftSkills)
            .Include(user => user.Technologies)
            .Include(user => user.Blogs)
            .Where(user => user.Id == id).FirstOrDefaultAsync();

        if (dbUser == null)
        {
            return NotFound("Usuario no encontrado");
        }
        if (dbUser.Image != null){
            this.dataContext.Images.RemoveRange(dbUser.Image);
        }
        
        this.dataContext.Projects.RemoveRange(dbUser.Projects);
        
        this.dataContext.Technologies.RemoveRange(dbUser.Technologies);
        this.dataContext.SoftSkills.RemoveRange(dbUser.SoftSkills);
        this.dataContext.Blogs.RemoveRange(dbUser.Blogs);

        this.dataContext.Users.Remove(dbUser);
        await this.dataContext.SaveChangesAsync();
        return Ok(dbUser);
    }

}