namespace api_portafolio.DTO.BlogsDTO;
public class BlogResponseDTO
{
    public long Id { get; set; }
    public IFormFile? Imagen { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public string? Enlace { get; set; }
}