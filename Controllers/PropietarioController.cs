using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PropietarioController : ControllerBase
{
  private readonly DataContext _context;
  private readonly IConfiguration _configuration;

  public PropietarioController(DataContext context, IConfiguration configuration){
    _context = context;
    _configuration = configuration;
  }

// GET: Propietario/
  [HttpGet]
  public ActionResult<IEnumerable<Propietario>> Get(){
    return _context.Propietario;
  }

// GET: Propietario/:id
  [HttpGet("{id}")]
  public IActionResult Get(int id){
    var propietario = _context.Propietario.Find(id);
    if(propietario == null){
      return NotFound();
    } 
    return Ok(propietario);
  }
}