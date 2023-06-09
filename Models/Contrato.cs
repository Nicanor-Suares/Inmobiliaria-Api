namespace Inmobiliaria_Api.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


	public class Contrato
	{
		[Key]
		public int idContrato { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }

		[Column("idInmueble")]
		public int InmuebleId { get; set; }
		public Inmueble InmuebleContrato { get; set; }
    
		[Column("idInquilino")]
		public int InquilinoId { get; set; }
		public Inquilino InquilinoContrato { get; set; }
		public bool Activo { get; set; }

  }