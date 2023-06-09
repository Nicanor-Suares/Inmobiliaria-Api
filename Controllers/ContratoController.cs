using System.Security.Claims;
using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContratoController : ControllerBase
{
  private readonly DataContext _context;
  private readonly IConfiguration _config;

  public ContratoController(DataContext context, IConfiguration config)
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

      if(usuario == null) return NotFound();

      var inmueble = _context.Inmueble.Find(inmueble_id);
      if(inmueble == null) return NotFound();

      if(usuario.idPropietario != inmueble.PropietarioId) return Unauthorized();

      var contrato = _context.Contrato
        .Include(i => i.InquilinoContrato)
        .FirstOrDefault(i => i.InmuebleId == inmueble_id);

      if(contrato == null) return NotFound();

      return Ok(contrato);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}