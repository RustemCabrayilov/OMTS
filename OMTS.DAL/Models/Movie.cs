using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
