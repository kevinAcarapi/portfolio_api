using api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog;

namespace api_portafolio.Entities.TechnologiesCatalog.Projects_technologiesCatalog;

public class Project_technologyCatalog{
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
    private Project? projects;
    public Project? Projects{
        get
        {
            return this.projects;
        }
        set
        {
            this.projects = value;
        }
    }

    private TechnologyCatalog? technologiesCatalog;
    public TechnologyCatalog? TechnologiesCatalog{
        get
        {
            return this.technologiesCatalog;
        }
        set
        {
            this.technologiesCatalog = value;
        }
    }
}