using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
    public class Review : BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Rating { get; set; } /*1-5*/
        public string Comment { get; set; }
    }
}
