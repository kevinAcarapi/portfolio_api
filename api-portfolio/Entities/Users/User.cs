namespace api_portafolio.Entities.Users;
using api_portafolio.Entities.TechnicalSkills;
using api_portafolio.Entities.Cards;

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
}
