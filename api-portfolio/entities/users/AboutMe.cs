namespace api_portafolio.entities.users;

public class AboutMe
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
    private string about_me = "";
    public string About_me 
    {
        get 
        {
            return this.about_me;
        }
        set
        {
            this.about_me = value;
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
