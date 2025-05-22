using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
	public class Seat : BaseEntity
	{
		public int SeatNo { get; set; }
		public bool IsBooked { get; set; }
		public int CinemaHallId { get; set; }
		public CinemaHall CinemaHall { get; set; }
	}
}
