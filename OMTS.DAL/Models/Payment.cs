using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Models
{
    public class Payment:BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public decimal  Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
