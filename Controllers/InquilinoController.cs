using System.Security.Claims;
using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class InquilinoController : ControllerBase
{
	private readonly DataContext _context;
	private readonly IConfiguration _config;

	public InquilinoController(DataContext context, IConfiguration config)
	{
		_context = context;
		_config = config;
	}

	[HttpGet("Obtener/{inmueble_id}")]
	[Authorize]
	public IActionResult ObtenerPorInmueble(int inmueble_id){
		try
			{
				int.TryParse(User.FindFirstValue("Id"), out int userId);
				var usuario = User.Identity != null ? _context.Propietario.Find(userId) : null;

				if (usuario == null) return NotFound();

				var inmueble = _context.Inmueble.Find(inmueble_id);
				if (inmueble == null) return NotFound();

				if (usuario.idPropietario != inmueble.PropietarioId) return Unauthorized();

				var inquilino = _context.Contrato
					.Include(c => c.InquilinoContrato)
					.Where(c => c.InmuebleId == inmueble_id)
					.Select(c => c.InquilinoContrato)
					.FirstOrDefault();

					if(inquilino == null) return NotFound();

				return Ok(inquilino);
			} catch (Exception e) {
				return BadRequest(e.Message);
			}
	}
}