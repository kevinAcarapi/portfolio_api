namespace api_portafolio.Entities.Skills.SoftSkills;

public class SoftSkill
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

    private string description ="";
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