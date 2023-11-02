namespace api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog.Projects_technologiesCatalog;

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
    private List<Project_technologyCatalog>? projects_technologiesCatalog;
    public List<Project_technologyCatalog>? Projects_TechnologiesCatalogs 
    {
        get
        {
            return this.projects_technologiesCatalog;       
        }
        set
        {
            this.projects_technologiesCatalog = value;
        }
    }
}