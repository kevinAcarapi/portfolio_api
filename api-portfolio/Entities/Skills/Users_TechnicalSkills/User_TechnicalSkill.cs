using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.TechnicalSkills;

namespace api_portafolio.Entities.Skills.Users_TechnicalSkills;

public class User_TechnicalSkill{
    private long id = 0;
    public long Id{
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }
    }
    private User? users;
    public User? Users{
        get
        {
            return this.users;
        }
        set
        {
            this.users = value;
        }
    }

    private Technology? technologys;
    public Technology? Technologys{
        get
        {
            return this.technologys;
        }
        set
        {
            this.technologys = value;
        }
    }
}