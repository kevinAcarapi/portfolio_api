using Microsoft.EntityFrameworkCore;
using api_portafolio.Entities;
using api_portafolio.Entities.Users;
namespace api_portfolio.Data.DataContext;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<AboutMe>? AboutUs { get; set; }

    public DbSet<AboutMe>? Technologies { get; set; }
}