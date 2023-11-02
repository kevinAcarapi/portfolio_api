namespace api_portafolio.Entities.Users;
using api_portafolio.Entities.Projects;
using api_portafolio.Entities.Skills.Users_SoftSkills;
using api_portafolio.Entities.Cards;
using api_portafolio.Entities.Skills.Users_TechnicalSkills;

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
    private string nombre = "";
    public string Nombre 
    {
        get 
        {
            return this.nombre;
        }
        set
        {
            this.nombre = value;
        } 
    }
    private string apellido = "";
    public string Apellido 
    {
        get 
        {
            return this.apellido;
        }
        set
        {
            this.apellido = value;
        } 
    }
    private string profesion = "";
    public string Profesion 
    {
        get 
        {
            return this.profesion;
        }
        set
        {
            this.profesion = value;
        } 
    }
    private string gmail = "";
    public string Gmail 
    {
        get 
        {
            return this.gmail;
        }
        set
        {
            this.gmail = value;
        } 
    }
    private string curriculum = "";
    public string Curriculum 
    {
        get 
        {
            return this.curriculum;
        }
        set
        {
            this.curriculum = value;
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
    
    

    private List<User_TechnicalSkill>? user_technicalSkills; //cambiar a tipo objeto
    public List<User_TechnicalSkill>? User_TechnicalSkills 
    {
        get
        {
            return this.user_technicalSkills;       
        }
        set
        {
            this.user_technicalSkills = value;
        }
    }
    private List<User_SoftSkill>? user_SoftSkills;
    public List<User_SoftSkill>? User_SoftSkills 
    {
        get
        {
            return this.user_SoftSkills;       
        }
        set
        {
            this.user_SoftSkills = value;
        }
    }
    private List<Card>? cards;
    public List<Card>? Cards 
    {
        get
        {
            return this.cards;       
        }
        set
        {
            this.cards = value;
        }
    }
    private List<Project>? projects;
    public List<Project>? Projects 
    {
        get
        {
            return this.projects;       
        }
        set
        {
            this.projects = value;
        }
    }    
}
