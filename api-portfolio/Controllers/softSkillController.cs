using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.SoftSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.SoftSkill;
namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class SoftSkillController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public SoftSkillController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<User>> GetSoftSkillByUser(long id)
    {
        User user = await this.dataContext.Users
            .Include(user => user.SoftSkills)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }

        List<SoftSkillResponseDTO> softSkillResponseDTOs = new List<SoftSkillResponseDTO>();

        foreach (SoftSkill softSkill in user.SoftSkills)
        {
            softSkillResponseDTOs.Add(new SoftSkillResponseDTO
            {
                Id = softSkill.Id,
                Description = softSkill.Description
            });
        }
        return Ok(softSkillResponseDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SoftSkillResponseDTO softSkillResponseDTO)
    {
        if (softSkillResponseDTO.Image == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "softSkillsPhotos", softSkillResponseDTO.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await softSkillResponseDTO.Image.CopyToAsync(stream);
        };

        SoftSkill softSkill = new SoftSkill
        {
            Id = softSkillResponseDTO.Id,
            Description = softSkillResponseDTO.Description,
            Image = new api_portafolio.Entities.Common.Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
        };

        await this.dataContext.SoftSkills.AddAsync(softSkill);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SoftSkillResponseDTO>> Put(
    [FromRoute] long id,
    [FromForm] SoftSkillResponseDTO softSkillResponseDTO)
    {
        SoftSkill? dbSoftSkill = await this.dataContext.SoftSkills.FindAsync(id);
        if (dbSoftSkill == null)
        {
            return NotFound("Habilidad no encontrada");
        }

        dbSoftSkill.Id = softSkillResponseDTO.Id;
        dbSoftSkill.Description = softSkillResponseDTO.Description;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "softSkillsPhotos", softSkillResponseDTO.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await softSkillResponseDTO.Image.CopyToAsync(stream);
        };

        dbSoftSkill.Image = new api_portafolio.Entities.Common.Image
        {
            Path = path,
            UploadDate = DateTime.Now,
            Url = ""
        };

        await this.dataContext.SaveChangesAsync();

        return Ok(dbSoftSkill);
    }



}