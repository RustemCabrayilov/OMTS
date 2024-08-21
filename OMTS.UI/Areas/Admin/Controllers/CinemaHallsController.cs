using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class CinemaHallsController : Controller
	{
		private readonly IGenericRepository<CinemaHall> _cinemaHallRepository;

		public CinemaHallsController(IGenericRepository<CinemaHall> cinemaHallRepository)
		{
			_cinemaHallRepository = cinemaHallRepository;
		}
		public async Task<IActionResult> Index()
		{
			var cinemaHalls = await _cinemaHallRepository.GetAll();
			List<CinemaHallVM> list = new();
			foreach (var cinemaHall in cinemaHalls)
			{
				list.Add(new CinemaHallVM
				{
					Id = cinemaHall.Id,
					Name = cinemaHall.Name,
					Location = cinemaHall.Location,
				});
			}
			return View(list);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CinemaHallVM model)
		{
			CinemaHall cinemaHall = new();
			cinemaHall.Name=model.Name;
			cinemaHall.Capacity=model.Capacity;
			cinemaHall.Location=model.Location;
			await _cinemaHallRepository.Add(cinemaHall);
			await _cinemaHallRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
