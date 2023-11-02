using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.Users_SoftSkills;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Cards;
using api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.Skills.Users_TechnicalSkills;

namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Technology>? Technologies { get; set; }
    public DbSet<SoftSkill>? SoftSkills{ get; set; }
    public DbSet<SoftSkillByUser>? SoftSkillByUser {get;set;}
    public DbSet<Card>? Cards{ get; set; }
    public DbSet<Project>? Projects{ get; set; }
    public DbSet<TechnologyByProject>? TechnologiesByProject { get; set; }
    public DbSet<TechnologyByUser>? TechnologiesByUser { get; set; }
}