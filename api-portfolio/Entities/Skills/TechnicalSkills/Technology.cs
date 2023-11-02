namespace api_portafolio.Entities.Skills.TechnicalSkills;

public class Technology 
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
    private string technical_skill = "";
    public string Technical_skill
    {
        get
        {
            return this.technical_skill;       
        }
        set
        {
            this.technical_skill = value;
        }
    }
}