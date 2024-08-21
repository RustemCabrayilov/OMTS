using OMTS.DAL.Models;

namespace OMTS.UI.Models
{
	public class PaymentVM:BaseEntity
	{
		public int TicketId { get; set; }
		public string TicketName { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentMethod { get; set; }
	}
}
