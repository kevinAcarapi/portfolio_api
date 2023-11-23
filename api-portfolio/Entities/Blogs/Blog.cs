using api_portafolio.Entities.Common;
namespace api_portafolio.Entities.Blogs;
public class Blog
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
    private Image? imagen;
    public Image? Imagen 
    {
        get 
        {
            return this.imagen;
        }
        set
        {
            this.imagen = value;
        } 
    }
    private string title = "";
    public string Title 
    {
        get 
        {
            return this.title;
        }
        set
        {
            this.title = value;
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
    private string enlace = "";
    public string Enlace 
    {
        get 
        {
            return this.enlace;
        }
        set
        {
            this.enlace = value;
        } 
    }
}