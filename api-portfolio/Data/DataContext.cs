using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities;
using api_portafolio.Entities.Users;
using api_portafolio.Entities.TechnicalSkills;
namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }

    public DbSet<User>? Technologies { get; set; }
}