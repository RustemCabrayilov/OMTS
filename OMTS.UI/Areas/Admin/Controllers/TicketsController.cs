using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class TicketsController : Controller
	{
		private readonly IGenericRepository<Ticket> _ticketRepository;
		private readonly IGenericRepository<Movie> _movieRepository;
		private readonly IGenericRepository<Customer> _customerRepository;
		private readonly IGenericRepository<Showtime> _showtimeRepository;
		private readonly IGenericRepository<Seat> _seatRepository;
		public TicketsController(IGenericRepository<Ticket> ticketRepository,
			IGenericRepository<Movie> movieRepository,
			IGenericRepository<Customer> customerRepository,
			IGenericRepository<Showtime> showtimeRepository,
			IGenericRepository<Seat> seatRepository)
		{
			_ticketRepository = ticketRepository;
			_movieRepository = movieRepository;
			_customerRepository = customerRepository;
			_showtimeRepository = showtimeRepository;
			_seatRepository = seatRepository;
		}

		public async Task<IActionResult> Index()
		{
			var tickets = await _ticketRepository.GetAll();
			List<TicketVM> list = new();
			foreach (var ticket in tickets)
			{
				var customer = await _customerRepository.Get(ticket.CustomerId);
				var seat = await _seatRepository.Get(ticket.SeatId);
				var movie = await _movieRepository.Get(ticket.MovieId);
				var showtime = await _showtimeRepository.Get(ticket.ShowtimeId);
				list.Add(new TicketVM
				{
					Id = ticket.Id,
					CustomerId = customer?.Id,
					CustomerName = customer?.UserName,
					MovieId = movie.Id,
					MovieName = movie.Title,
					StartTime = showtime.StartTime,
					EndTime = showtime.EndTime,
					SeatNumber = seat.SeatNo,
					Price = ticket.Price,
					IsPaid = ticket.IsPaid,
				});
			}
			return View(list);
		}
		public async Task<IActionResult> Edit(int id)
		{
			var ticket = await _ticketRepository.Get(id);
			var customer = await _customerRepository.Get(ticket.CustomerId);
			var seat = await _seatRepository.Get(ticket.SeatId);
			var movie = await _movieRepository.Get(ticket.MovieId);
			var showtime = await _showtimeRepository.Get(ticket.ShowtimeId);
			var movies = await _movieRepository.GetAll();
			var seats = await _seatRepository.GetAll();
			var showtimes = await _showtimeRepository.GetAll();
			TicketVM ticketVM = new TicketVM
			{
				Id = ticket.Id,
				CustomerId = customer?.Id,
				CustomerName = customer?.UserName,
				MovieId = movie.Id,
				MovieName = movie.Title,
				StartTime = showtime.StartTime,
				EndTime = showtime.EndTime,
				SeatNumber = seat.SeatNo,
				Price = ticket.Price,
				Movies = movies.ToList(),
				SeatNumbers = seats.ToList(),
				Showtimes =showtimes.ToList(),
				IsPaid=ticket.IsPaid,
			};
			return View(ticketVM);
		}
		public async Task<IActionResult> Delete(int id)
		{
			var ticket = await _ticketRepository.Get(id);
			var customer = await _customerRepository.Get(ticket.CustomerId);
			var seat = await _seatRepository.Get(ticket.SeatId);
			var movie = await _movieRepository.Get(ticket.MovieId);
			var showtime = await _showtimeRepository.Get(ticket.ShowtimeId);
			TicketVM ticketVM = new TicketVM
			{
				Id = ticket.Id,
				CustomerId = customer?.Id,
				CustomerName = customer?.UserName,
				MovieId = movie.Id,
				MovieName = movie.Title,
				StartTime = showtime.StartTime,
				EndTime = showtime.EndTime,
				SeatNumber = seat.SeatNo,
				Price = ticket.Price,
				IsPaid=ticket.IsPaid,
			};
			return View(ticketVM);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(TicketVM model)
		{
			_ticketRepository.Delete(model.Id);
			await _ticketRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
