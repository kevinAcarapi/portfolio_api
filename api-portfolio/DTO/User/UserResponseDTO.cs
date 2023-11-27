namespace api_portafolio.DTO.User;
public class UserResponseDTO
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
    public long? ProjectId {get;set;}
    public long? BlogId {get;set;}
    public long?  TecnologyId {get;set;}
    public long? SoftSkillId {get;set;}
}