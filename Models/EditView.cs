namespace Inmobiliaria_Api.Models;

public class EditView
{
	public int idPropietario { get; set; }
	public string Dni { get; set; } = "";
	public string Nombre { get; set; } = "";
	public string Apellido { get; set; } = "";
	public string Email { get; set; } = "";
	public string Telefono { get; set; } = "";
  public string Password { get; set; } = "";
}