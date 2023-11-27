namespace api_portafolio.Entities.Skills.SoftSkills;

using api_portafolio.Entities.Common;

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

    private Image? image;
    public Image? Image
    {
        get
        {
            return this.image;
        }
        set
        {
            this.image = value;
        }
    }

}