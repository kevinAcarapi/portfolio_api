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
                Imagen = project.Imagen,
                Description = project.Description,
                Technologies = technologyDTOResponses
            });
        }
        return Ok(projectResponseDTOs);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Post([FromBody] Project project)
    {
        if (this.dataContext != null && this.dataContext.Projects != null)
        {
            await this.dataContext.Projects.AddAsync(project);

            await this.dataContext.SaveChangesAsync();
        }
        return Ok(project);
    }

    // [HttpPut("{id}")]
    // public async Task<ActionResult<Project>> Put(
    //     [FromRoute] long id,
    //     [FromBody] Project project)
    // {
    //     if (this.dataContext != null && this.dataContext.Projects != null)
    //     {
    //         User user =await this.dataContext.Users
    //         .Include(user => user.Projects)
    //         .ThenInclude(project => project.TechnologiesByProject)
    //         .ThenInclude(technologyByProject => technologyByProject.Technology)
    //         .Where(user => user.Id == id)
    //         .FirstOrDefaultAsync();

    //         if(user == null){
    //             return NotFound("Usuario no encontrado")
    //         }

    //         Project proj = user.Projects.FirstOrDefault(proj => p.Id == project.Id);

    //         await this.dataContext.SaveChangesAsync();
    //     }

    //     return Ok(project);
    // }
}