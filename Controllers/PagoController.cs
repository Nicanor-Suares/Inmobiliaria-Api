using System.Security.Claims;
using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class PagoController : ControllerBase
{
  private readonly DataContext _context;
  private readonly IConfiguration _config;

  public PagoController(DataContext context, IConfiguration config)
  {
    _context = context;
    _config = config;
  }

  [HttpGet("Obtener/{contrato_id}")]
  [Authorize]
  public IActionResult ObtenerPorContrato(int contrato_id)
  {
    try
    {
      int.TryParse(User.FindFirstValue("Id"), out int userId);
      var usuario = User.Identity != null ? _context.Propietario.Find(userId) : null;
      if(usuario == null) return NotFound();
      var contrato = _context.Contrato.Include(c => c.InmuebleContrato).FirstOrDefault(c => c.idContrato == contrato_id);
      if(contrato == null) return NotFound();

      var pagos = _context.Pago.Where(p => p.IdContrato == contrato_id);

      return Ok(pagos);
    } catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}