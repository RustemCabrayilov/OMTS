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
					Capacity= cinemaHall.Capacity,
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
			cinemaHall.Name = model.Name;
			cinemaHall.Capacity = model.Capacity;
			cinemaHall.Location = model.Location;
			await _cinemaHallRepository.Add(cinemaHall);
			await _cinemaHallRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Edit(int id)
		{
			var cinemaHall = await _cinemaHallRepository.Get(id);
			CinemaHallVM cinemaHallVM = new();
			cinemaHallVM.Name = cinemaHall.Name;
			cinemaHallVM.Location = cinemaHall.Location;
			cinemaHallVM.Capacity = cinemaHall.Capacity;

			return View(cinemaHallVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(CinemaHallVM model, int id)
		{
			/*CinemaHall ch = new();*/
			var cinemaHall = await _cinemaHallRepository.Get(id);
			cinemaHall.Name = model.Name;
			cinemaHall.Location = model.Location;
			cinemaHall.Capacity = model.Capacity;
			_cinemaHallRepository.Update(cinemaHall);
			await _cinemaHallRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var cinemaHall = await _cinemaHallRepository.Get(id);
			CinemaHallVM cinemaHallVM = new();
			cinemaHallVM.Id = id;
			cinemaHall.Name = cinemaHall.Name;
			cinemaHallVM.Capacity = cinemaHall.Capacity;
			cinemaHallVM.Location = cinemaHall.Location;
			return View(cinemaHallVM);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(CinemaHallVM model)
		{
			_cinemaHallRepository.Delete(model.Id);
			await _cinemaHallRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
