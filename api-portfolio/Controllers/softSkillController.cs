using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.SoftSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.SoftSkill;
using api_portafolio.DTO.PaginatedDTO;
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
            .ThenInclude(softSkill => softSkill.Image)
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
                Description = softSkill.Description,
                UrlImage = "/Image/" + (softSkill.Image != null ? softSkill.Image.Id.ToString() : "")
            });
        }
        return Ok(softSkillResponseDTOs);
    }

    [HttpGet("type/{id}")]
    public async Task<ActionResult<List<DTOListResponse>>> Get(
        [FromRoute] long id,
        [FromQuery] DTOList dtoList)
    {
        // Primer cambio.
        var query = this.dataContext.SoftSkills.AsQueryable();

        if (!string.IsNullOrEmpty(dtoList.Query))
        {
            query = query.Where(softSkills => softSkills.Description.Contains(dtoList.Query));
        }

        if (!string.IsNullOrEmpty(dtoList.OrderBy))
        {
            query = query.OrderBy(softSkills => softSkills.Description);
        }

        int page = dtoList.Page != null ? dtoList.Page.Value : 1;
        int pageSize = dtoList.PageSize != null ? dtoList.PageSize.Value : 10;

        var count = await query.CountAsync();

        var softSkills = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<Object> dtos = new List<Object>();

        foreach (SoftSkill softSkill in softSkills)
        {
            dtos.Add(new SoftSkillResponseDTO
            {
                Description = softSkill.Description,
                Id = softSkill.Id,
                UrlImage = "/Image/" + (softSkill.Image != null ? softSkill.Image.Id.ToString() : "")
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
            Image = new Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },

        };

        User? dbUser = await this.dataContext.Users.FindAsync(softSkillResponseDTO.UserId);
        if (dbUser == null)
        {
            return NotFound("Usuario no encontrado");
        }

        if (softSkillResponseDTO.UserId.HasValue)
        {
            var existingUser = await this.dataContext.Users.FindAsync(softSkillResponseDTO.UserId.Value);
            if (existingUser == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            dbUser.SoftSkills = new List<SoftSkill> { softSkill };
        }

        await this.dataContext.SoftSkills.AddAsync(softSkill);

        await this.dataContext.SaveChangesAsync();

        return Ok(softSkillResponseDTO);
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