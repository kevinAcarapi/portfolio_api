using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.SoftSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.SoftSkill;
using api_portafolio.Entities.Common;
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
    [FromForm] SoftSkillRequestDTO softSkillRequestDTO)
    {
        SoftSkill? dbSoftSkill = await this.dataContext.SoftSkills.FindAsync(id);
        if (dbSoftSkill == null)
        {
            return NotFound("Habilidad no encontrada");
        }

        dbSoftSkill.Description = softSkillRequestDTO.Description ?? dbSoftSkill.Description;

        var newImage = softSkillRequestDTO.Image != null ? await SaveImage(softSkillRequestDTO.Image) : dbSoftSkill.Image;

        if (newImage != null)
        {
            dbSoftSkill.Image = newImage;
        }

        await this.dataContext.SaveChangesAsync();

        return Ok(dbSoftSkill);
    }

    private async Task<Image> SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "softSkillsPhotos", imageFile.FileName);

        string imagePath = Path.Combine("Archivos", "softSkillsPhotos", path);


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
        SoftSkill? dbSoftSkill = await this.dataContext.SoftSkills
        .Include(softSkill => softSkill.Image)
        .Where(softSkill => softSkill.Id == id).FirstOrDefaultAsync();

        if (dbSoftSkill == null)
        {
            return NotFound("Tecnología no encontrada");
        }

        if (dbSoftSkill.Image != null)
        {
            this.dataContext.Images.RemoveRange(dbSoftSkill.Image);
        }

        this.dataContext.SoftSkills.Remove(dbSoftSkill);
        await this.dataContext.SaveChangesAsync();

        return Ok();
    }



}