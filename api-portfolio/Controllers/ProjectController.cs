using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Projects;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.ProjectDTO;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.DTO.Tecnology;
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
        User user =await this.dataContext.Users
            .Include(user => user.Projects)
            .ThenInclude(project => project.TechnologiesByProject)
            .ThenInclude(technologyByProject => technologyByProject.Technology)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        List<ProjectResponseDTO> projectResponseDTOs = new List<ProjectResponseDTO>();

        foreach(Project project in user.Projects){
            List<TechnologyDTOResponse> technologyDTOResponses = new List<TechnologyDTOResponse>();

            foreach (TechnologyByProject technologyByProject in project.TechnologiesByProject){
                technologyDTOResponses.Add(new TechnologyDTOResponse{
                    Id = technologyByProject.Technology.Id,
                    Description = technologyByProject.Technology.Description
                });
            } 
            projectResponseDTOs.Add(new ProjectResponseDTO{
                Id = project.Id,
                // Imagen = project.Imagen,
                Description = project.Description,
                Technologies = technologyDTOResponses
            });
        }
        return Ok(projectResponseDTOs);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post([FromForm] ProjectResponseDTO projectResponseDTO)
    {
        if(projectResponseDTO.Imagen == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos\\projectPhotos", projectResponseDTO.Imagen.FileName);

        using(var stream = new FileStream(path, FileMode.Create))
        {
            await projectResponseDTO.Imagen.CopyToAsync(stream);
        };

        Project project = new Project{
            Id = projectResponseDTO.Id,
            Description = projectResponseDTO.Description,
            Enlace = projectResponseDTO.Enlace,
            // Imagen = projectResponseDTO.Imagen,
            Title = projectResponseDTO.Title,
        };
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Project>> Put(
        [FromRoute] long id,
        [FromForm] Project project)
    {
        if (this.dataContext != null && this.dataContext.Projects != null)
        {
            Project dbproject = await this.dataContext.Projects.FindAsync(id);
            if(dbproject == null){
                return NotFound("Usuario no encontrado");
            };

            dbproject.Id = project.Id;
            dbproject.Description = project.Description;
            dbproject.Enlace = project.Enlace;
            // dbproject.Imagen = project.Imagen;
            dbproject.Title = project.Title;

            await this.dataContext.SaveChangesAsync();
        }

        return Ok(project);
    }
}