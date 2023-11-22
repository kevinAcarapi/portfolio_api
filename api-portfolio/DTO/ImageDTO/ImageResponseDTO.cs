namespace api_portafolio.DTO.Common;

public class ImageResponseDTO
{
    public long Id {get; set;}

    public string? Path {get; set;}

    public string? Url {get; set;}
    public DateTime? UploadDate {get; set; }
}