using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class SeatController : Controller
	{
		private readonly IGenericRepository<Seat> _seatRepository;
		private readonly IGenericRepository<CinemaHall> _cinemaHallRepository;

		public SeatController(IGenericRepository<Seat> seatRepository,
			IGenericRepository<CinemaHall> cinemaHallRepository)
		{
			_seatRepository = seatRepository;
			_cinemaHallRepository = cinemaHallRepository;
		}
		public async Task<IActionResult> Index()
		{
			var seats = await _seatRepository.GetAll();
			List<SeatVM> list = new();
			foreach (var seat in seats)
			{
				var cinemaHall = await _cinemaHallRepository.Get(seat.CinemaHallId);
				list.Add(new SeatVM
				{
					Id = seat.Id,
					SeatNo = seat.SeatNo,
					CinemaHallId = seat.CinemaHallId,
					CinemaHallName = cinemaHall.Name,
				});
			}
			return View(list);
		}
		public async Task<IActionResult> Create(int? cinemaHallId)
		{
			var cinemaHalls = await _cinemaHallRepository.GetAll();
			SeatVM seatVM = new();
			seatVM.CinemaHallId = cinemaHallId ?? 0;
			seatVM.CinemaHalls = cinemaHalls.ToList();
			return View(seatVM);
		}
		[HttpPost]
		public async Task<IActionResult> Create(SeatVM model)
		{
			Seat seat = new();
			seat.CinemaHallId = model.CinemaHallId;
			seat.SeatNo= model.SeatNo;
			seat.IsBooked = false;
			await _seatRepository.Add(seat);
			await _seatRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
