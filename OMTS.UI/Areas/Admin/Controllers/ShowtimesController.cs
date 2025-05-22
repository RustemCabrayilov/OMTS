using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class ShowtimesController : Controller
	{
		private readonly IGenericRepository<Showtime> _showtimeRepository;
		private readonly IGenericRepository<Movie> _movieRepository;
		private readonly IGenericRepository<CinemaHall> _cinemaHallRepository;

		public ShowtimesController(IGenericRepository<Showtime> showtimeRepository,
			IGenericRepository<Movie> movieRepository,
			IGenericRepository<CinemaHall> cinemaHallRepository)
		{
			_showtimeRepository = showtimeRepository;
			_movieRepository = movieRepository;
			_cinemaHallRepository = cinemaHallRepository;
		}

		public async Task<IActionResult> Index()
		{
			var showtimes = await _showtimeRepository.GetAll();
			List<ShowtimeVM> list = new();
			foreach (var showtime in showtimes)
			{
				var movie = await _movieRepository.Get(showtime.MovieId);
				var cinemaHall = await _cinemaHallRepository.Get(showtime.CinemaHallId);
				list.Add(new ShowtimeVM
				{
					Id = showtime.Id,
					MovieId = showtime.MovieId,
					MovieTitle = movie.Title,
					CinemaHallId = showtime.CinemaHallId,
					CinemaHallName = cinemaHall.Name,
					StartTime = showtime.StartTime,
					EndTime = showtime.EndTime,
				});
			}
			return View(list);
		}
		public async Task<IActionResult> Create(int? movieId, int? cinemaHallId)
		{
			var cinemaHalls = await _cinemaHallRepository.GetAll();
			var movies = await _movieRepository.GetAll();
			ShowtimeVM showtimeVM = new ShowtimeVM();
			showtimeVM.MovieId = movieId ?? 0;
			showtimeVM.CinemaHallId = cinemaHallId ?? 0;
			showtimeVM.CinemaHalls = cinemaHalls.ToList();
			showtimeVM.Movies = movies.ToList();
			return View(showtimeVM);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ShowtimeVM model)
		{
			Showtime showtime = new();
			showtime.MovieId = model.MovieId;
			showtime.CinemaHallId = model.CinemaHallId;
			showtime.StartTime = model.StartTime;
			showtime.EndTime = model.EndTime;
			await _showtimeRepository.Add(showtime);
			await _showtimeRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Edit(int id)
		{
			var showtime = await _showtimeRepository.Get(id);
			var movies = await _movieRepository.GetAll();
			var cinemaHalls = await _cinemaHallRepository.GetAll();
			ShowtimeVM showtimeVM = new();
			showtimeVM.MovieId = showtime.MovieId;
			showtimeVM.CinemaHallId = showtime.CinemaHallId;
			showtimeVM.StartTime = showtime.StartTime;
			showtimeVM.EndTime = showtime.EndTime;
			showtimeVM.Movies = movies.ToList();
			showtimeVM.CinemaHalls = cinemaHalls.ToList();
			return View(showtimeVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(ShowtimeVM model, int id)
		{
			/*Movie movie = new();*/
			var showtime = await _showtimeRepository.Get(id);
			showtime.MovieId = model.MovieId;
			showtime.CinemaHallId = model.CinemaHallId;
			showtime.StartTime = model.StartTime;
			showtime.EndTime = model.EndTime;
			_showtimeRepository.Update(showtime);
			await _movieRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var showtime = await _showtimeRepository.Get(id);
			ShowtimeVM showtimeVM = new();
			showtimeVM.Id = id;
			showtimeVM.MovieId = showtime.MovieId;
			showtimeVM.CinemaHallId = showtime.CinemaHallId;
			showtimeVM.StartTime = showtime.StartTime;
			showtimeVM.EndTime = showtime.EndTime;
			return View(showtimeVM);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(ShowtimeVM model)
		{
			_showtimeRepository.Delete(model.Id);
			await _showtimeRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
