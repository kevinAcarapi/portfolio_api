using api_portafolio.Entities.Skills.TechnicalSkills;

namespace api_portafolio.Entities.TechnologiesCatalog;

public class TechnologyByProject{
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

    private Technology technology;
    public Technology Technology
    {
        get
        {
            return this.technology;
        }
        set{
            this.technology = value;
        }
    }
}