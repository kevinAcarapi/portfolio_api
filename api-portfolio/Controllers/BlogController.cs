using api_portafolio.Entities.Users;
using api_portfolio.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Blogs;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.DTO.BlogsDTO;
using api_portafolio.DTO.PaginatedDTO;
using api_portafolio.Entities.Common;
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
        User? user = await this.dataContext.Users
            .Include(user => user.Blogs)
            .ThenInclude(blog => blog.Imagen)
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("Usuario no encontrado");
        }

        List<BlogResponseDTO> blogResponseDTOs = new List<BlogResponseDTO>();

        foreach (Blog blog in user.Blogs)
        {
            blogResponseDTOs.Add(new BlogResponseDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Enlace = blog.Enlace,
                UrlImage = "/Image/" + (blog.Imagen != null ? blog.Imagen.Id.ToString() : "")
            });
        }
        return Ok(blogResponseDTOs);
    }

    [HttpGet]
    public async Task<ActionResult<List<DTOListResponse>>> Get(
        [FromQuery] DTOList dtoList)
    {
        // Primer cambio.
        var query = this.dataContext.Blogs.AsQueryable();

        if (!string.IsNullOrEmpty(dtoList.Query))
        {
            query = query.Where(blogs => blogs.Title.Contains(dtoList.Query));
        }

        if (!string.IsNullOrEmpty(dtoList.OrderBy))
        {
            query = query.OrderBy(blogs => blogs.Title);
        }

        int page = dtoList.Page != null ? dtoList.Page.Value : 1;
        int pageSize = dtoList.PageSize != null ? dtoList.PageSize.Value : 10;

        var count = await query.CountAsync();

        var blogs = await query
            .Include(blog => blog.Imagen)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<Object> dtos = new List<Object>();

        foreach (Blog blog in blogs)
        {
            dtos.Add(new BlogResponseDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Enlace = blog.Enlace,
                UrlImage = "/Image/" + (blog.Imagen != null ? blog.Imagen.Id.ToString() : "")
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
    public async Task<ActionResult> Post([FromForm] BlogRequestDTO blogRequestDTO)
    {
        if (blogRequestDTO.Imagen == null)
        {
            return BadRequest("Archivo no encontrado");
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "blogPhotos", blogRequestDTO.Imagen.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await blogRequestDTO.Imagen.CopyToAsync(stream);
        };

        Blog blog = new Blog
        {
            Id = blogRequestDTO.Id,
            Title = blogRequestDTO.Title,
            Description = blogRequestDTO.Description,
            Enlace = blogRequestDTO.Enlace,
            Imagen = new Image
            {
                Path = path,
                UploadDate = DateTime.Now,
                Url = ""
            },
        };

        User? dbUser = await this.dataContext.Users.FindAsync(blogRequestDTO.UserId);
        if (dbUser == null)
        {
            return NotFound("Usuario no encontrado");
        }

        if (blogRequestDTO.UserId.HasValue)
        {
            var existingUser = await this.dataContext.Users.FindAsync(blogRequestDTO.UserId.Value);
            if (existingUser == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            dbUser.Blogs = new List<Blog> { blog };
        }

        await this.dataContext.Blogs.AddAsync(blog);

        await this.dataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Blog>> Put(
        [FromRoute] long id,
        [FromForm] BlogRequestDTO blogRequestDTO)
    {
        if (this.dataContext != null && this.dataContext.Blogs != null)
        {
            Blog dbblog = await this.dataContext.Blogs.FindAsync(id);
            if (dbblog == null)
            {
                return NotFound("Blog no encontrado");
            };

            dbblog.Description = blogRequestDTO.Description ?? dbblog.Description;
            dbblog.Enlace = blogRequestDTO.Enlace ?? dbblog.Enlace;
            dbblog.Title = blogRequestDTO.Title ?? dbblog.Title;

            var newImage = blogRequestDTO.Imagen != null ? await SaveImage(blogRequestDTO.Imagen) : dbblog.Imagen;

            if (newImage != null)
            {
                dbblog.Imagen = newImage;
            }

            await this.dataContext.SaveChangesAsync();
        }

        return Ok(blogRequestDTO);
    }

    private async Task<Image> SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", "blogPhotos", imageFile.FileName);

        string imagePath = Path.Combine("Archivos", "blogPhotos", path);


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

        // Devuelve la ruta de la imagen
        return image;

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