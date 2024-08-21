using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Controllers
{
	public class PaymentsController : Controller
	{
		private readonly IGenericRepository<Ticket> _ticketRepository;
		private readonly IGenericRepository<Payment> _paymentRepository;

		public PaymentsController(IGenericRepository<Ticket> ticketRepository,
			IGenericRepository<Payment> paymentRepository)
		{
			_ticketRepository = ticketRepository;
			_paymentRepository = paymentRepository;
		}

		public async Task<IActionResult> Index()
		{
			var payments = await _paymentRepository.GetAll();
			List<PaymentVM> list = new();
            foreach (var payment in payments)
            {
				list.Add(new PaymentVM
				{
					Id = payment.Id,
					TicketId = payment.TicketId,
					PaymentDate=payment.PaymentDate,
					PaymentMethod=payment.PaymentMethod,
				});
            }
            return View(list);
		}
		public IActionResult Create(int? customerId, int? showtimeId, int? movieId)
		{
			TicketVM ticketVM = new();
			ticketVM.CustomerId = customerId ?? 0;
			ticketVM.ShowtimeId = showtimeId ?? 0;
			ticketVM.MovieId = movieId ?? 0;
			return View(ticketVM);
		}
		[HttpPost]
		public async Task<IActionResult> Create(TicketVM model)
		{
			Ticket ticket = new();
			ticket.MovieId = model.MovieId;
			ticket.CustomerId = model.CustomerId;
			ticket.ShowtimeId = model.ShowtimeId;
	/*		ticket.SeatNumber = model.SeatNumber;*/
			ticket.Price = model.Price;
			await _ticketRepository.Add(ticket);
			await _ticketRepository.SaveAsync();
			return RedirectToAction("Index", "Movies");
		}
	}
}
