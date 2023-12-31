namespace api_portafolio.DTO.ProjectDTO;

public class ProjectRequestDTO
{
    public long Id {get; set;}
    public IFormFile? Imagen {get; set;}
    public string? urlImage {get; set;}
    public string? Title {get;set;}
    public string? Description {get; set;}
    public string? Enlace {get;set;}    
    public long? TechnologyId { get; set; }
    public long? UserId {get;set;}
    
}