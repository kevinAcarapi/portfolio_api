namespace api_portafolio.Entities.Common;

public class Image
{
    private long id = 0;
    public long Id 
    { 
        get
        {
            return this.id;
        } set
        {
            this.id = value;
        } 
    }

    private string path = "";
    public string Path 
    { 
        get
        {
            return this.path;
        } set
        {
            this.path = value;
        } 
    }

    private string url = "";
    public string Url 
    { 
        get
        {
            return this.url;
        } set
        {
            this.url = value;
        } 
    }

    private DateTime? uploadDate;
    public DateTime? UploadDate 
    { 
        get
        {
            return this.uploadDate;
        } set
        {
            this.uploadDate = value;
        } 
    }
}