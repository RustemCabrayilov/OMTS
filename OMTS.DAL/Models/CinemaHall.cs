using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
    public class CinemaHall : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        List<Seat> Seats { get; set; }
    }
}
