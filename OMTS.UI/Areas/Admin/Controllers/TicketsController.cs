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
		public TicketsController(IGenericRepository<Ticket> ticketRepository,
			IGenericRepository<Movie> movieRepository,
			IGenericRepository<Customer> customerRepository,
			IGenericRepository<Showtime> showtimeRepository)
		{
			_ticketRepository = ticketRepository;
			_movieRepository = movieRepository;
			_customerRepository = customerRepository;
			_showtimeRepository = showtimeRepository;
		}

		public async Task<IActionResult> Index()
		{
			var tickets = await _ticketRepository.GetAll();
			List<TicketVM> list = new();
			foreach (var ticket in tickets)
			{
				var customer = await _customerRepository.Get(ticket.CustomerId);
				var movie = await _movieRepository.Get(ticket.MovieId);
				var showtime = await _showtimeRepository.Get(ticket.ShowtimeId);
				list.Add(new TicketVM
				{
					Id = ticket.Id,
					CustomerId = customer?.Id,
					CustomerName = customer?.Name,
					MovieId = movie.Id,
					MovieName = movie.Title,
					StartTime = showtime.StartTime,
					EndTime = showtime.EndTime,
					/*SeatNumber = ticket.SeatNumber,*/
					Price = ticket.Price,
				});
			}
			return View(list);
		}
		public async Task<IActionResult> Create(int? customerId, int? showtimeId, int? movieId)
		{
			
			var movies= await _movieRepository.GetAll();
			var showtimes=await _showtimeRepository.GetAll();
			TicketVM ticketVM = new();
			ticketVM.CustomerId = customerId ?? 0;
			ticketVM.ShowtimeId = showtimeId ?? 0;
			ticketVM.MovieId = movieId ?? 0;
			ticketVM.Movies = movies.ToList();
			ticketVM.Showtimes = showtimes.ToList();
			return View(ticketVM);
		}
		[HttpPost]
		public async Task<IActionResult> Create(TicketVM model)
		{
			Ticket ticket = new();
			ticket.MovieId = model.MovieId;
			/*ticket.SeatNumber = model.SeatNumber;*/
			ticket.CustomerId = model.CustomerId;
			ticket.ShowtimeId = model.ShowtimeId;
			ticket.Price = model.Price;
			await _ticketRepository.Add(ticket);
			await _ticketRepository.SaveAsync();
			return RedirectToAction("Index");
		}

	}
}
