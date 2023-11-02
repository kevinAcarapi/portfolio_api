using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.TechnicalSkills;

namespace api_portafolio.Entities.Skills.Users_TechnicalSkills;

public class TechnologyByUser
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

    private User? user;
    public User? User
    {
        get
        {
            return this.user;
        }
        set
        {
            this.user = value;
        }
    }

    private Technology? technology;
    public Technology? Technology
    {
        get
        {
            return this.technology;
        }
        set
        {
            this.technology = value;
        }
    }
}