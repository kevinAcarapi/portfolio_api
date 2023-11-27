using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Projects;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.ProjectDTO;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.DTO.Tecnology;
using api_portafolio.Entities.Common;

namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public ProjectController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<ProjectResponseDTO>>> GetProjectsByUser(long id)
    {
        User? user = await dataContext.Users
            .Include(user => user.Projects)
            .ThenInclude(project => project.TechnologiesByProject)
            .ThenInclude(technologyByProject => technologyByProject.Technology)
            .Include(user => user.Projects)
            .ThenInclude(project => project.Image)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }

        List<ProjectResponseDTO> projectResponseDTOs = new List<ProjectResponseDTO>();

        foreach (Project project in user.Projects)
        {
            List<TechnologyDTOResponse> technologyDTOResponses = new List<TechnologyDTOResponse>();

            foreach (TechnologyByProject technologyByProject in project.TechnologiesByProject)
            {
                technologyDTOResponses.Add(new TechnologyDTOResponse
                {
                    Id = technologyByProject.Technology.Id,
                    Description = technologyByProject.Technology.Description
                });
            }

            projectResponseDTOs.Add(new ProjectResponseDTO
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Enlace = project.Enlace,
                urlImage = "/Image/" + (project.Image != null ? project.Image.Id.ToString() : "")
            });
        }

        return Ok(projectResponseDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] ProjectResponseDTO projectResponseDTO)
    {
        if (projectResponseDTO.Imagen == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "projectPhotos", projectResponseDTO.Imagen.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await projectResponseDTO.Imagen.CopyToAsync(stream);
        };

        Project project = new Project
        {
            Id = projectResponseDTO.Id,
            Description = projectResponseDTO.Description,
            Enlace = projectResponseDTO.Enlace,
            Image = new api_portafolio.Entities.Common.Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
            Title = projectResponseDTO.Title,
        };

        await this.dataContext.Projects.AddAsync(project);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponseDTO>> Put(
    [FromRoute] long id,
    [FromForm] ProjectRequestDTO projectRequetsDTO)
    {
        if (this.dataContext != null && this.dataContext.Projects != null)
        {
            Project dbproject = await this.dataContext.Projects.FindAsync(id);
            if (dbproject == null)
            {
                return NotFound("Proyecto no encontrado");
            };

            var newImage = projectRequetsDTO.Imagen != null ? await SaveImage(projectRequetsDTO.Imagen) : dbproject.Image;

            if (newImage != null)
            {
                dbproject.Image = newImage;
            }

            dbproject.Title = projectRequetsDTO.Title ?? dbproject.Title;
            dbproject.Description = projectRequetsDTO.Description ?? dbproject.Description;
            dbproject.Enlace = projectRequetsDTO.Enlace ?? dbproject.Enlace;

            await this.dataContext.SaveChangesAsync();
        }

        return Ok(projectRequetsDTO);
    }

    private async Task<Image> SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "projectPhotos", imageFile.FileName);

        string imagePath = Path.Combine("Archivos", "projectPhotos", path);

        
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            Image image = new Image{
                Path = imagePath,
                UploadDate = DateTime.Now,
                Url = ""
            };

            // Devuelve la ruta de la imagen
            return image;
        
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        Project dbProject = await this.dataContext.Projects
            .Include(project => project.Image)
            .Include(project => project.TechnologiesByProject)
            .ThenInclude(technologyByProject => technologyByProject.Technology)
            .Where(project => project.Id == id).FirstOrDefaultAsync();

        if (dbProject == null)
        {
            return NotFound("Proyecto no encontrado");
        }
        if (dbProject.Image != null)
        {
            this.dataContext.Images.RemoveRange(dbProject.Image);
        }

        this.dataContext.TechnologiesByProject.RemoveRange(dbProject.TechnologiesByProject);
        this.dataContext.Projects.Remove(dbProject);
        await this.dataContext.SaveChangesAsync();
        return Ok(dbProject);
    }
}