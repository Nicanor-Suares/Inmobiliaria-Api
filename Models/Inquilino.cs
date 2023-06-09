using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Api.Models;
public class Inquilino
{
	[Key]
	public int idInquilino { get; set; }
	public string Nombre { get; set; } = "";
	public string Apellido { get; set; } = "";
	public int Dni { get; set; }
	public string Telefono { get; set; } = "";
}
