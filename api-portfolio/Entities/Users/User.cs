namespace api_portafolio.Entities.Users;
using api_portafolio.Entities.TechnicalSkills;

public class User
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
    private string profile_photo = "";
    public string Profile_photo 
    { 
        get
        {
            return this.profile_photo;
        } 
        set
        {
            this.profile_photo = value;
        }
    }
    
    

    private List<Technology>? technologies; //cambiar a tipo objeto
    public List<Technology>? Technologies 
    {
        get
        {
            return this.technologies;       
        }
        set
        {
            this.technologies = value;
        }
    }


    
}
