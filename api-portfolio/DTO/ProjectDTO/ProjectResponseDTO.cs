using api_portafolio.DTO.Tecnology;

namespace api_portafolio.DTO.ProjectDTO;

public class ProjectResponseDTO
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
    private string imagen = "";
    public string Imagen 
    {
        get 
        {
            return this.imagen;
        }
        set
        {
            this.imagen = value;
        } 
    }
    private string title = "";
    public string Title 
    {
        get 
        {
            return this.title;
        }
        set
        {
            this.title = value;
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
    private string enlace = "";
    public string Enlace 
    {
        get 
        {
            return this.enlace;
        }
        set
        {
            this.enlace = value;
        } 
    }

    private List<TechnologyDTOResponse>? technologies;
    public List<TechnologyDTOResponse>? Technologies 
    {
        get
        {
            return this.technologies;       
        }
        set
        {
            this.technologies = value;
        }
    }
}