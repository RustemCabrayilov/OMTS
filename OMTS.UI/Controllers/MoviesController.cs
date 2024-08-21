using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IGenericRepository<Movie> _movieRepository;

        public MoviesController(IGenericRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index(int customerId)
        {
            var movies = await _movieRepository.GetAll();
            List<MovieVM> list = new List<MovieVM>();
            foreach (var movie in movies)
            {
                list.Add(new MovieVM
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Duration = movie.Duration,
                    Director = movie.Director,
                    ReleaseDate = movie.ReleaseDate,
                    CustomerId=customerId 

                });
            }
            return View(list);
        }
        public async Task<IActionResult> Details(int customerId,int movieId)
        {

            return RedirectToAction("Index", "Showtimes", new {customerId=customerId,movieId=movieId});
        }
    }
}
