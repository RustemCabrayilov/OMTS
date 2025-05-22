using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMTS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Data
{
	public class OMTSDbContext : IdentityDbContext
	{
		public OMTSDbContext(DbContextOptions<OMTSDbContext> opts) : base(opts)
		{

		}
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Showtime> Showtimes { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<CinemaHall> CinemaHalls { get; set; }
		public DbSet<Seat> Seats { get; set; }
	}
}
