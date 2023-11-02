using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Skills.Users_SoftSkills;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Cards;
using api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.TechnologiesCatalog.Projects_technologiesCatalog;
namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Technology>? Technologies { get; set; }
    public DbSet<SoftSkill>? Softskills{ get; set; }
    public DbSet<User_SoftSkill>? Users_SoftSkills{get;set;}
    public DbSet<Card>? Cards{ get; set; }
    public DbSet<Project>? Projects{ get; set; }
    public DbSet<TechnologyCatalog>? technologiesCatalog{ get; set; }
    public DbSet<Project_technologyCatalog>? Projects_TechnologiesCatalog{ get; set; }
}