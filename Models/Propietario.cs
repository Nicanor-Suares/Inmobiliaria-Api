using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Api.Models;
public class Propietario
{
	[Display(Name = "Código Propietario")]
  [Key]
	public int idPropietario { get; set; }
	public string Nombre { get; set; } = "";
	public string Apellido { get; set; } = "";
	public string Dni { get; set; } = "";
	public int Telefono { get; set; }
  public string Email { get; set; } = "";
  public string Password { get; set; } = "";
	public string Avatar { get; set; } = "";

	public override string ToString()
	{
		return $"{Nombre} {Apellido}";
	}


}