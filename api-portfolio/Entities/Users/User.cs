namespace api_portafolio.Entities.Users;
using api_portafolio.Entities.Projects;
using api_portafolio.Entities.Skills.SoftSkills;

using api_portafolio.Entities.Blogs;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Common;

public class User
{
    private long id;
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

    private List<SoftSkill>? softSkills;
    public List<SoftSkill>? SoftSkills 
    {
        get
        {
            return this.softSkills;       
        }
        set
        {
            this.softSkills = value;
        }
    }
    
    private List<Blog>? blogs;
    public List<Blog>? Blogs 
    {
        get
        {
            return this.blogs;       
        }
        set
        {
            this.blogs = value;
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