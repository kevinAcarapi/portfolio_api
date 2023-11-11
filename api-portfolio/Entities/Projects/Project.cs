using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.Cards;

namespace api_portafolio.Entities.Projects;

public class Project : Card
{
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