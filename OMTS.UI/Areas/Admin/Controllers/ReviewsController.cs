using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ReviewsController : Controller
    {
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IGenericRepository<Movie> _movieRepository;
        private readonly IGenericRepository<Customer> _customerRepository;

        public ReviewsController(IGenericRepository<Review> reviewRepository,
            IGenericRepository<Movie> movieRepository,
            IGenericRepository<Customer> customerRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewRepository.GetAll();
            List<ReviewVM> list = new();
            foreach (var review in reviews)
            {
                var movie = await _movieRepository.Get(review.MovieId);
                var customer = await _customerRepository.Get(review.CustomerId);
                list.Add(new ReviewVM
                {
                    Id = review.Id,
                    MovieId = review.MovieId,
                    Movie = movie,
                    CustomerId = review.CustomerId,
                    Customer=customer,
                    Rating= review.Rating,
                    Comment=review.Comment,
                });
            }
            return View(list);
        }
        public async Task<IActionResult> Delete(int id)
        {
			var review= await _reviewRepository.Get(id);
			var movie = await _movieRepository.Get(review.MovieId);
			var customer = await _customerRepository.Get(review.CustomerId);
            ReviewVM reviewVM = new()
            {
				Id = review.Id,
				MovieId = review.MovieId,
				Movie = movie,
				CustomerId = review.CustomerId,
				Customer = customer,
				Rating = review.Rating,
				Comment = review.Comment,
			};
            return View(reviewVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ReviewVM model)
        {
            _reviewRepository.Delete(model.Id);
            await _reviewRepository.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
