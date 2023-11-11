namespace api_portafolio.DTO.Tecnology;

public class TechnologyDTOResponse
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
}