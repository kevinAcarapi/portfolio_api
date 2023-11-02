using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portafolio.Entities.Skills.TechnicalSkills;
namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Technology>? Technologies { get; set; }
    public DbSet<SoftSkill>? Softskills{ get; set; }
}