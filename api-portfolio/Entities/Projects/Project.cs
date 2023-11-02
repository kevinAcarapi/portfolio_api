using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.Cards;

namespace api_portafolio.Entities.Projects;

public class Project : Card
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