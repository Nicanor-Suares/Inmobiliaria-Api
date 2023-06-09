using System.Security.Claims;
using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class InmuebleController : ControllerBase {
	private readonly DataContext _context;
	private readonly IConfiguration _config;
	public InmuebleController(DataContext context, IConfiguration config) {
		_context = context;
		_config = config;
	}

//http://localhost:5200/Inmueble
	[HttpGet]
	[Authorize]
	public IActionResult GetInmuebles(){
		try {
			int.TryParse(User.FindFirstValue("Id"), out int id);
			var usuario = User.Identity != null ? _context.Propietario.Find(id) : null;

			if(usuario == null) return NotFound();

			return Ok(_context.Inmueble
					.Include(i => i.PropietarioInmueble)
					.Include(i => i.Tipo_Inmueble)
					.Where(e => e.PropietarioInmueble.idPropietario == usuario.idPropietario));
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

	//http://localhost:5200/Inmueble/Alquilados
	[HttpGet("Alquilados")]
	[Authorize]
	public IActionResult GetInmueblesAlquilados(){
		try {
			int.TryParse(User.FindFirstValue("Id"), out int id);
			var usuario = User.Identity != null ? _context.Propietario.Find(id) : null;

			if(usuario == null) return NotFound();

			var fechaActual = DateTime.Today;

			var inmuebles = _context.Contrato
				.Include(c => c.InmuebleContrato)
					.ThenInclude(i => i.PropietarioInmueble)
					.Where(c => c.InmuebleContrato.PropietarioInmueble.idPropietario == usuario.idPropietario)
					.Where(c => c.Activo && c.FechaInicio <= fechaActual && c.FechaFin >= fechaActual)
					.Select(c => c.InmuebleContrato)
					.ToList();

			return Ok(inmuebles);		
		} 
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

}