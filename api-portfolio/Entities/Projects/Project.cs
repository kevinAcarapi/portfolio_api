using api_portafolio.Entities.TechnologiesCatalog;

namespace api_portafolio.Entities.Projects;

public class Project
{
    private long id;
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
    private List<TechnologyByProject>? technologiesByProject;
    public List<TechnologyByProject>? TechnologiesByProject 
    {
        get
        {
            return this.technologiesByProject;       
        }
        set
        {
            this.technologiesByProject = value;
        }
    }
}