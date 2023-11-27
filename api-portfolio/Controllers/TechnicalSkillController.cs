using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.TechnicalSkills;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.Tecnology;
namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnologyController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public TecnologyController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<Technology>> GetTecnologyByUser(long id)
    {
        User user =await this.dataContext.Users
            .Include(user => user.Technologies)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }

        List<TechnologyDTOResponse> technologyDTOResponse = new List<TechnologyDTOResponse>();

        foreach (Technology technology in user.Technologies)
        {
            technologyDTOResponse.Add(new TechnologyDTOResponse
            {
                Id = technology.Id,
                Description = technology.Description
            });
        }
        return Ok(technologyDTOResponse);    

    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] TechnologyDTOResponse technologyDTOResponse)
    {
         if (technologyDTOResponse.Image == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "technologyPhotos", technologyDTOResponse.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await technologyDTOResponse.Image.CopyToAsync(stream);
        };

        Technology technology = new Technology
        {
            Id = technologyDTOResponse.Id,
            Description = technologyDTOResponse.Description,
            Image = new api_portafolio.Entities.Common.Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
        };

        await this.dataContext.Technologies.AddAsync(technology);
        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TechnologyDTOResponse>> Put(
        [FromRoute] long id,
        [FromForm] TechnologyDTOResponse technologyDTOResponse)
    {
        Technology? dbTechnology = await this.dataContext.Technologies.FindAsync(id);
        if (dbTechnology == null)
        {
            return NotFound("Tecnología no encontrada");
        }

        dbTechnology.Id = technologyDTOResponse.Id;
        dbTechnology.Description = technologyDTOResponse.Description;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "technologyPhotos", technologyDTOResponse.Image.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await technologyDTOResponse.Image.CopyToAsync(stream);
        };

        dbTechnology.Image = new api_portafolio.Entities.Common.Image
        {
            Path = path,
            UploadDate = DateTime.Now,
            Url = ""
        };

        await this.dataContext.SaveChangesAsync();

        return Ok(dbTechnology);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)       
    {
        Technology? dbTechnology = await this.dataContext.Technologies
            .Include(technology => technology.Image)
            .Where(technology => technology.Id == id).FirstOrDefaultAsync();
       
        if (dbTechnology == null)
        {
            return NotFound("Tecnología no encontrada");
        }

        if (dbTechnology.Image != null)
        {
            this.dataContext.Images.RemoveRange(dbTechnology.Image);
        }

        this.dataContext.Technologies.Remove(dbTechnology);
        await this.dataContext.SaveChangesAsync();

        return Ok();
    }        
}
