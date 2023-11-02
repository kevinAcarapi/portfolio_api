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

    private string soft_skill ="";
    public string Soft_skill
    {
        get
        {
            return this.soft_skill;       
        }
        set
        {
            this.soft_skill = value;
        }
    }
}