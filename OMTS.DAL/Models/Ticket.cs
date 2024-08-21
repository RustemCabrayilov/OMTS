using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
	public class Ticket : BaseEntity
	{
		public int MovieId { get; set; }
		public Movie Movie { get; set; }
		public int? CustomerId { get; set; }
		public Customer? Customer { get; set; }
		public int? ShowtimeId { get; set; }
		public Showtime? Showtime { get; set; }
		public int SeatId { get; set; }
        public Seat Seat { get; set; }
        public decimal Price { get; set; }

	}
}
