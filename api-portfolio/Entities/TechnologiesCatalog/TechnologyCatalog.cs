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
}