using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Api.Models;
public class TipoInmueble
{
	[Key]
	public int idTipo { get; set; }
	public string tipo { get; set; }
}