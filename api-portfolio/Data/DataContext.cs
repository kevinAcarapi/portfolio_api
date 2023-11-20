using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.Skills.SoftSkills;
using api_portafolio.Entities.Skills.TechnicalSkills;
using api_portafolio.Entities.Projects;
using api_portafolio.Entities.TechnologiesCatalog;
using api_portafolio.Entities.Blogs;
using System.Diagnostics.CodeAnalysis;
using api_portafolio.Entities.Common;

namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    [NotNull]
    public DbSet<User>? Users { get; set; }
    public DbSet<Technology>? Technologies { get; set; }
    public DbSet<SoftSkill>? SoftSkills{ get; set; }
    public DbSet<Image>? Images {get; set;}
    public DbSet<Project>? Projects{ get; set; }
    public DbSet<Blog>? Blogs { get; set; }
    public DbSet<TechnologyByProject>? TechnologiesByProject { get; set; }
}