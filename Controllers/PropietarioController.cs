using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Inmobiliaria_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Inmobiliaria_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PropietarioController : ControllerBase
{
	private string hashSalt = "";
	private readonly DataContext _context;
	private readonly IConfiguration _configuration;

	public PropietarioController(DataContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
		hashSalt = _configuration["Salt"] ?? "";
	}

	// GET: Propietario/
	[HttpGet]
	public ActionResult<IEnumerable<Propietario>> Get()
	{
		return _context.Propietario;
	}

	// GET: Propietario/:id
	[HttpGet("{id}")]
	public IActionResult Get(int id)
	{
		var propietario = _context.Propietario.Find(id);
		if (propietario == null)
		{
			return NotFound();
		}
		return Ok(propietario);
	}

	//http://localhost:5200/Propietario/Login
	[HttpPost("Login")]
	public IActionResult Login(LoginView loginView)
	{
		var propietario = _context.Propietario.FirstOrDefault(x => x.Email == loginView.Email);
		if (propietario == null)
		{
			return NotFound();
		}

		String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
			password: loginView.Password,
			salt: System.Text.Encoding.ASCII.GetBytes(hashSalt),
			prf: KeyDerivationPrf.HMACSHA1,
			iterationCount: 10000,
			numBytesRequested: 256 / 8
		));

		if (hashed != propietario.Password)
		{
			return BadRequest("Contraseña incorrecta");
		}

		string secretKey = _configuration["TokenAuthentication:SecretKey"] ?? throw new ArgumentNullException(nameof(secretKey));
		var securityKey = secretKey != null ? new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)) : null;
		var credenciales = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, propietario.Email),
			new Claim("Id", propietario.idPropietario.ToString())
		};

		var token = new JwtSecurityToken(
			issuer: _configuration["TokenAuthentication:Issuer"],
			audience: _configuration["TokenAuthentication:Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(60),
			signingCredentials: credenciales
		);

		return Ok(new JwtSecurityTokenHandler().WriteToken(token));
	}

	//http://localhost:5200/Propietario/Perfil
	[HttpGet ("Perfil")]
	[Authorize]
	public ActionResult<Propietario> GetPerfil()
	{
	var propietario = User.Identity != null
			? _context.Propietario
				.Where(x => x.Email == User.Identity.Name)
				.Select(x => new PropietarioView(x))
				.FirstOrDefault()
			: null;

		if (propietario == null)
		{
			return NotFound();
		}

		return Ok(propietario);
	}

	//http://localhost:5200/Propietario/Edit
	[HttpPost("Edit")]
	[Authorize]
	public IActionResult Edit(EditView propietario){
		try
		{
			int.TryParse(User.FindFirstValue("Id"), out int id);
			var propietarioDB = User.Identity != null ? _context.Propietario.Find(id) : null;

			if (propietarioDB == null)
			{
				return NotFound();
			}

			if(propietario.idPropietario != propietarioDB.idPropietario)
			{
				return BadRequest("No se puede editar el propietario");
			}

			if(
				string.IsNullOrEmpty(propietario.Nombre) ||
				string.IsNullOrEmpty(propietario.Apellido) ||
				string.IsNullOrEmpty(propietario.Dni) || 
				string.IsNullOrEmpty(propietario.Email) ||
				string.IsNullOrEmpty(propietario.Telefono) ||
				string.IsNullOrEmpty(propietario.Password)
			) {
				return BadRequest("Los campos no pueden estar vacíos");
			}

			propietarioDB.Nombre = propietario.Nombre;
			propietarioDB.Apellido = propietario.Apellido;
			propietarioDB.Dni = propietario.Dni;
			propietarioDB.Email = propietario.Email;
			propietarioDB.Telefono = propietario.Telefono;

			_context.Propietario.Update(propietarioDB);
			_context.SaveChanges();

			return Ok(propietario);
		}


		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

}