using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;

namespace api_portafolio.Entities.Skills.Users_SoftSkills;

public class User_SoftSkill{
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
    private SoftSkill? softSkills;
    public SoftSkill? SoftSkills{
        get
        {
            return this.softSkills;
        }
        set
        {
            this.softSkills = value;
        }
    }
}