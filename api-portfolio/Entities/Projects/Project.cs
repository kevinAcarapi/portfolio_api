namespace api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog;

public class Project{
    private long id = 0;
    public long Id{
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }
    }
    private string technology_used = "";
    public string  Technology_used{
        get
        {
            return this.technology_used;
        }
        set
        {
            this.technology_used = value;
        }
    }
    private List<TechnologyCatalog>? technologiesCatalogs;
    public List<TechnologyCatalog>? TechnologiesCatalogs 
    {
        get
        {
            return this.technologiesCatalogs;       
        }
        set
        {
            this.technologiesCatalogs = value;
        }
    }  
}