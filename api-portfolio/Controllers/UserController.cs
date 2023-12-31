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
using api_portafolio.DTO.PaginatedDTO;
using api_portafolio.Entities.TechnologiesCatalog;

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
        User user = await this.dataContext.Users
            .Include(user => user.Image)
            .Include(user => user.Projects)
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
            urlImage = "/Image/" + (user.Image != null ? user.Image.Id.ToString() : "")
        };
        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }
        return Ok(userResponseDTO);
    }

    [HttpGet]
    public async Task<ActionResult<List<DTOListResponse>>> Get(
        [FromQuery] DTOList dtoList)
    {
        // Primer cambio.
        var query = this.dataContext.Users.AsQueryable();

        if (!string.IsNullOrEmpty(dtoList.Query))
        {
            query = query.Where(user => user.Nombre.Contains(dtoList.Query));
        }

        if (!string.IsNullOrEmpty(dtoList.OrderBy))
        {
            query = query.OrderBy(user => user.Nombre);
        }

        int page = dtoList.Page != null ? dtoList.Page.Value : 1;
        int pageSize = dtoList.PageSize != null ? dtoList.PageSize.Value : 10;

        var count = await query.CountAsync();

        var users = await query
            .Include(user => user.Image)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<Object> dtos = new List<Object>();

        foreach (User user in users)
        {
            dtos.Add(new UserResponseDTO
            {
                Id = user.Id,
                Apellido = user.Apellido,
                Nombre = user.Nombre,
                Description = user.Description,
                Gmail = user.Gmail,
                Profesion = user.Profesion,
                urlImage = "/Image/" + (user.Image != null ? user.Image.Id.ToString() : "")
            });
        }

        int pageCount = (count / pageSize) + 1;

        return Ok(new DTOListResponse
        {
            HasNextPage = (page + 1) <= pageCount,
            HasPrevPage = page > 1,
            List = dtos,
            NextPage = (page + 1) <= pageCount ? page + 1 : page,
            Page = page,
            PageSize = pageSize,
            PrevPage = page > 1 ? page - 1 : 1,
            TotalCount = count,
            TotalPage = pageCount
        });
    }

    // Metodo Post
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] UserRequestDTO userRequestDTO)
    {
        if (userRequestDTO.Image == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "profilePhotos", userRequestDTO.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await userRequestDTO.Image.CopyToAsync(stream);
        };

        User user = new User
        {
            Id = userRequestDTO.Id,
            Apellido = userRequestDTO.Apellido,
            Nombre = userRequestDTO.Nombre,
            Description = userRequestDTO.Description,
            Curriculum = userRequestDTO.Curriculum,
            Gmail = userRequestDTO.Gmail,
            Profesion = userRequestDTO.Profesion,
            Image = new Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },

        };

        if (userRequestDTO.ProjectId != null && userRequestDTO.ProjectId.Count > 0)
        {
            user.Projects = new List<Project> { };
            foreach (long item in userRequestDTO.ProjectId)
            {
                var existingProject = await this.dataContext.Projects.FindAsync(item);
                if (existingProject == null)
                {
                    return BadRequest("Proyecto no encontrado");
                }
                user.Projects.Add(existingProject);
            }
        }

        if (userRequestDTO.BlogId != null && userRequestDTO.BlogId.Count > 0)
        {
            user.Blogs = new List<Blog> { };
            foreach (long item in userRequestDTO.BlogId)
            {
                var existingBlog = await this.dataContext.Blogs.FindAsync(item);
                if (existingBlog == null)
                {
                    return BadRequest("Blog no encontrado");
                }
                user.Blogs.Add(existingBlog);
            }
        }

        if (userRequestDTO.TecnologyId != null && userRequestDTO.TecnologyId.Count > 0)
        {
            user.Technologies = new List<Technology> { };
            foreach (long item in userRequestDTO.TecnologyId)
            {
                var existingTecnology = await this.dataContext.Technologies.FindAsync(item);
                if (existingTecnology == null)
                {
                    return BadRequest("Tecnologia no encontrada");
                }
                user.Technologies.Add(existingTecnology);
            }
        }

        if (userRequestDTO.SoftSkillId != null && userRequestDTO.SoftSkillId.Count > 0)
        {
            user.SoftSkills = new List<SoftSkill> { };
            foreach (long item in userRequestDTO.SoftSkillId)
            {
                var existingSoftSkill = await this.dataContext.SoftSkills.FindAsync(item);
                if (existingSoftSkill == null)
                {
                    return BadRequest("SoftSkill no encontrado");
                }
                user.SoftSkills.Add(existingSoftSkill);
            }
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


        if (userRequestDTO.ProjectId != null && userRequestDTO.ProjectId.Count > 0)
        {
            dbUser.Projects = new List<Project> { };
            foreach (long item in userRequestDTO.ProjectId)
            {
                var existingProject = await this.dataContext.Projects.FindAsync(item);
                if (existingProject == null)
                {
                    return BadRequest("Proyecto no encontrado");
                }
                dbUser.Projects.Add(existingProject);
            }
        }

        if (userRequestDTO.BlogId != null && userRequestDTO.BlogId.Count > 0)
        {
            dbUser.Blogs = new List<Blog> { };
            foreach (long item in userRequestDTO.BlogId)
            {
                var existingBlog = await this.dataContext.Blogs.FindAsync(item);
                if (existingBlog == null)
                {
                    return BadRequest("Blog no encontrado");
                }

                dbUser.Blogs.Add(existingBlog);
            }
        }

        if (userRequestDTO.TecnologyId != null && userRequestDTO.TecnologyId.Count > 0)
        {
            dbUser.Technologies = new List<Technology> { };
            foreach (long item in userRequestDTO.TecnologyId)
            {
                var existingTecnology = await this.dataContext.Technologies.FindAsync(item);
                if (existingTecnology == null)
                {
                    return BadRequest("Tecnologia no encontrada");
                }
                dbUser.Technologies.Add(existingTecnology);
            }
        }

        if (userRequestDTO.SoftSkillId != null && userRequestDTO.SoftSkillId.Count > 0)
        {
            dbUser.SoftSkills = new List<SoftSkill> { };
            foreach (long item in userRequestDTO.SoftSkillId)
            {
                var existingSoftSkill = await this.dataContext.SoftSkills.FindAsync(item);
                if (existingSoftSkill == null)
                {
                    return BadRequest("SoftSkill no encontrado");
                }
                dbUser.SoftSkills.Add(existingSoftSkill);
            }
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

        Image image = new Image
        {
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