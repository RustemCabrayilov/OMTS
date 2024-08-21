using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class MoviesController : Controller
	{
		private readonly IGenericRepository<Movie> _movieRepository;
		private readonly IGenericRepository<Ticket> _ticketRepository;
		private readonly IGenericRepository<Review> _reviewRepository;

		public MoviesController(IGenericRepository<Movie> movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public async Task<IActionResult> Index()
		{
			var movies = await _movieRepository.GetAll();
			List<MovieVM> list = new List<MovieVM>();
			foreach (var movie in movies)
			{
				list.Add(new MovieVM
				{
					Id = movie.Id,
					Title = movie.Title,
					Genre = movie.Genre,
					Duration = movie.Duration,
					Director = movie.Director,
					ReleaseDate = movie.ReleaseDate
				});
			}
			return View(list);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(MovieVM model)
		{
			Movie movie = new();
			movie.Title = model.Title;
			movie.Genre = model.Genre;
			movie.Director = model.Director;
			movie.ReleaseDate = model.ReleaseDate;
			movie.Duration = model.Duration;
			await _movieRepository.Add(movie);
			await _movieRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
