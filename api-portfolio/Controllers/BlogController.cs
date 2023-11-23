using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Blogs;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.BlogsDTO;
namespace api_portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    [NotNull]
    private readonly DataContext dataContext;

    public BlogController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<Blog>>> GetBlogByUser(long id)
    {
        User? user =await this.dataContext.Users
            .Include(user => user.Blogs)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }

        List<BlogResponseDTO> blogResponseDTOs = new List<BlogResponseDTO>();

        foreach(Blog blog in user.Blogs){
            blogResponseDTOs.Add(new BlogResponseDTO{
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Enlace = blog.Enlace
            });
        }
        return Ok(blogResponseDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] BlogResponseDTO blogResponseDTO)
    {
        if(blogResponseDTO.Imagen == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos","blogPhotos", blogResponseDTO.Imagen.FileName);

        using(var stream = new FileStream(path, FileMode.Create))
        {
            await blogResponseDTO.Imagen.CopyToAsync(stream);
        };

        Blog blog = new Blog{
            Id = blogResponseDTO.Id,
            Title = blogResponseDTO.Title,
            Description = blogResponseDTO.Description,
            Enlace = blogResponseDTO.Enlace,
            Imagen = new api_portafolio.Entities.Common.Image 
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
        };

        await this.dataContext.Blogs.AddAsync(blog);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Blog>> Put(
        [FromRoute] long id,
        [FromForm] Blog blog)
    {
        if (this.dataContext != null && this.dataContext.Blogs != null)
        {
            Blog dbblog = await this.dataContext.Blogs.FindAsync(id);
            if(dbblog == null){
                return NotFound("Blog no encontrado");
            };

            dbblog.Id = blog.Id;
            dbblog.Description = blog.Description;
            dbblog.Enlace = blog.Enlace;
            dbblog.Title = blog.Title;
            await this.dataContext.SaveChangesAsync();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        Blog dbBlog = await this.dataContext.Blogs
            .Include(blog => blog.Imagen)
            .Where(blog => blog.Id == id).FirstOrDefaultAsync();

        if (dbBlog == null)
        {
            return NotFound("Proyecto no encontrado");
        }
        if (dbBlog.Imagen != null)
        {
            this.dataContext.Images.RemoveRange(dbBlog.Imagen);
        }

        this.dataContext.Blogs.Remove(dbBlog);
        await this.dataContext.SaveChangesAsync();
        return Ok(dbBlog);
    }
}