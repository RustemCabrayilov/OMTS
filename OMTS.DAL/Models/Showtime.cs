using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
    public class Showtime : BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
