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
using api_portafolio.Entities.Projects;
using api_portafolio.DTO.ProjectDTO;
using api_portafolio.Entities.Blogs;
using api_portafolio.Entities.Common;

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
            Profesion = user.Profesion,
        };

        return Ok(userResponseDTO);
    }

    // Metodo Post
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] UserResponseDTO userResponseDTO)
    {
        if (userResponseDTO.Image == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "profilePhotos", userResponseDTO.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await userResponseDTO.Image.CopyToAsync(stream);
        };

        User user = new User
        {
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
        if (userResponseDTO.ProjectId.HasValue)
        {
            var existingProject = await this.dataContext.Projects.FindAsync(userResponseDTO.ProjectId.Value);
            if (existingProject == null)
            {
                return BadRequest("Proyecto no encontrado");
            }
            user.Projects = new List<Project> { existingProject };
        }

        if (userResponseDTO.BlogId.HasValue)
        {
            var existingBlog = await this.dataContext.Blogs.FindAsync(userResponseDTO.BlogId.Value);
            if (existingBlog == null)
            {
                return BadRequest("Blog no encontrado");
            }
            user.Blogs = new List<Blog> { existingBlog };
        }

        if (userResponseDTO.TecnologyId.HasValue)
        {
            var existingTecnology = await this.dataContext.Technologies.FindAsync(userResponseDTO.TecnologyId.Value);
            if (existingTecnology == null)
            {
                return BadRequest("Tecnology no encontrado");
            }
            user.Technologies = new List<Technology> { existingTecnology };
        }

        if (userResponseDTO.SoftSkillId.HasValue)
        {
            var existingSoftSkill = await this.dataContext.SoftSkills.FindAsync(userResponseDTO.SoftSkillId.Value);
            if (existingSoftSkill == null)
            {
                return BadRequest("SoftSkill no encontrado");
            }
            user.SoftSkills = new List<SoftSkill> { existingSoftSkill };
        }

        await this.dataContext.Users.AddAsync(user);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    // Metodo Put
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDTO>> Put(
        [FromRoute] long id,
        [FromForm] UserRequestDTO userRequestDTO)
    {
        User? dbUser = await this.dataContext.Users.FindAsync(id);
        if (dbUser == null)
        {
            return NotFound("Usuario no encontrado");
        }

        dbUser.Nombre = userRequestDTO.Nombre ?? dbUser.Nombre;
        dbUser.Apellido = userRequestDTO.Apellido ?? dbUser.Apellido;
        dbUser.Curriculum = userRequestDTO.Curriculum ?? dbUser.Curriculum;
        dbUser.Profesion = userRequestDTO.Profesion ?? dbUser.Profesion;
        dbUser.Description = userRequestDTO.Description ?? dbUser.Description;
        dbUser.Gmail = userRequestDTO.Gmail ?? dbUser.Gmail;

        var newImage = userRequestDTO.Image != null ? await SaveImage(userRequestDTO.Image) : dbUser.Image;

        if (newImage != null)
        {
            dbUser.Image = newImage;
        }


        if (userRequestDTO.ProjectId.HasValue)
        {
            var existingProject = await this.dataContext.Projects.FindAsync(userRequestDTO.ProjectId.Value);
            if (existingProject == null)
            {
                return BadRequest("Proyecto no encontrado");
            }
            dbUser.Projects = new List<Project> { existingProject };
        }

        if (userRequestDTO.BlogId.HasValue)
        {
            var existingBlog = await this.dataContext.Blogs.FindAsync(userRequestDTO.BlogId.Value);
            if (existingBlog == null)
            {
                return BadRequest("Blog no encontrado");
            }
            dbUser.Blogs = new List<Blog> { existingBlog };
        }

        if (userRequestDTO.TecnologyId.HasValue)
        {
            var existingTecnology = await this.dataContext.Technologies.FindAsync(userRequestDTO.TecnologyId.Value);
            if (existingTecnology == null)
            {
                return BadRequest("Tecnology no encontrado");
            }
            dbUser.Technologies = new List<Technology> { existingTecnology };
        }

        if (userRequestDTO.SoftSkillId.HasValue)
        {
            var existingSoftSkill = await this.dataContext.SoftSkills.FindAsync(userRequestDTO.SoftSkillId.Value);
            if (existingSoftSkill == null)
            {
                return BadRequest("SoftSkill no encontrado");
            }
            dbUser.SoftSkills = new List<SoftSkill> { existingSoftSkill };
        }

        
        await this.dataContext.SaveChangesAsync();

        return Ok(userRequestDTO);
    }

    private async Task<Image> SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "profilePhotos", imageFile.FileName);

        string imagePath = Path.Combine("Archivos", "profilePhotos", path);

        
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            Image image = new Image{
                Path = imagePath,
                UploadDate = DateTime.Now,
                Url = ""
            };

            return image;
        
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
        if (dbUser.Image != null)
        {
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