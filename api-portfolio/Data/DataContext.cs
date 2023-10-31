using Microsoft.EntityFrameworkCore;
using api_portafolio.entities;
using api_portafolio.entities.users;
namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<AboutMe>? AboutUs { get; set; }

    public DbSet<AboutMe>? Technologies { get; set; }
}