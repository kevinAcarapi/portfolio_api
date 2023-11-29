namespace api_portafolio.DTO.Tecnology;

public class TechnologyDTOResponse
{
    private long id = 0;
    public long Id 
    {
        get
        {
            return this.id;       
        }
        set
        {
            this.id = value;
        }
    }

    private string description = "";
    public string Description
    {
        get
        {
            return this.description;       
        }
        set
        {
            this.description = value;
        }
    }

    private IFormFile? image;
    public IFormFile? Image 
    {
        get 
        {
            return this.image;
        }
        set
        {
            this.image = value;
        } 
    }

    private string? urlImage;
    public string? UrlImage
    {
        get 
        {
            return this.urlImage;
        }
        set
        {
            this.urlImage = value;
        } 
    }
    private long? userId;
    public long? UserId{
        get
        {
            return this.userId;
        }
        set
        {
            this.userId = value;
        }
    }
}