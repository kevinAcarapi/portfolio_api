namespace api_portafolio.Entities.Cards;

public class Card 
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
    private string imagen = "";
    public string Imagen 
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