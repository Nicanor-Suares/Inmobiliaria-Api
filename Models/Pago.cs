using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Api.Models;
	public class Pago
	{
		[Key]
		public int IdPago { get; set; }

		[Column("idContrato")]
		[ForeignKey("Contrato")]
		public int IdContrato { get; set; }
		public DateTime FechaPago { get; set; }
		public int Monto { get; set; }
		public int nroPago { get; set; }
  }
