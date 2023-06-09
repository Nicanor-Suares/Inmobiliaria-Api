using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Api.Models;
public class PropietarioView
{
	[Display(Name = "Código Propietario")]
  [Key]
	public int idPropietario { get; set; }
	public string Nombre { get; set; } = "";
	public string Apellido { get; set; } = "";
	public string Dni { get; set; } = "";
	public int? Telefono { get; set; }
  public string Email { get; set; } = "";
  public string Password { get; set; } = "";
	public string Avatar { get; set; } = "";

  public PropietarioView(){}

  public PropietarioView(Propietario propietario){
    this.idPropietario = propietario.idPropietario;
    this.Nombre = propietario.Nombre;
    this.Apellido = propietario.Apellido;
    this.Dni = propietario.Dni;
    this.Telefono = propietario.Telefono;
    this.Email = propietario.Email;
    this.Password = propietario.Password;
    this.Avatar = propietario.Avatar;
  }

}