using Inmobiliaria_Api.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options){}
  public DbSet<Propietario> Propietario { get; set; } = null!;
 	public DbSet<Inmueble> Inmueble { get; set; } = null!;
  public DbSet<TipoInmueble> Tipo_Inmueble { get; set; } = null!;
  public DbSet<Contrato> Contrato { get; set; } = null!;
  public DbSet<Inquilino> Inquilino { get; set; } = null!;
  public DbSet<Pago> Pago { get; set; } = null!;

}