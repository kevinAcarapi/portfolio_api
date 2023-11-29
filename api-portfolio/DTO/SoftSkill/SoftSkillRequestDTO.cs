namespace api_portafolio.DTO.SoftSkill;

public class SoftSkillRequestDTO
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

    private IFormFile? image;
    public IFormFile? Image 
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