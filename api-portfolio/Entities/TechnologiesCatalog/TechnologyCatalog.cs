using api_portafolio.Entities.TechnologiesCatalog.Projects_technologiesCatalog;

namespace api_portafolio.Entities.TechnologiesCatalog;

public class TechnologyCatalog{
    private long id = 0;
    public long Id{
        get
        {
            return this.id;
        }
        set{
            this.id = value;
        }
    }

    private string project_technology = "";
    public string Project_technology{
        get
        {
            return this.project_technology;
        }
        set{
            this.project_technology = value;
        }
    }
    private List<Project_technologyCatalog>? projects_technologiesCatalog; //cambiar a tipo objeto
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