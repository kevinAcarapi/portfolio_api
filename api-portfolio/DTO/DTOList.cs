namespace api_portafolio.DTO;

public class DTOList
{
    public int? Page { get; set; }
    
    public int? PageSize { get; set; }

    public string? Query { get; set; }

    public string? OrderBy { get; set; }
}