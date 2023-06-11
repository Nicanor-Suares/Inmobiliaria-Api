namespace Inmobiliaria_Api.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
	[Key]
	public int idInmueble { get; set; }
	public bool Estado { get; set; }
	public int Ambientes { get; set; }
	public double Precio { get; set; }
	[ForeignKey(nameof(Propietario))]
	public int PropietarioId { get; set; }
	public Propietario PropietarioInmueble { get; set; }
	public string Direccion { get; set; }
	public string Uso { get; set; }
	public string Imagen { get; set; }
	public int idTipo { get; set; }
	[ForeignKey(nameof(idTipo))]
	public TipoInmueble Tipo_Inmueble { get; set; }

}