using Inmobiliaria_Api.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options){}
  public DbSet<Propietario> Propietario { get; set; } = null!;
}