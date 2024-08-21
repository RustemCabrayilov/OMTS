using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Controllers
{
	public class ReviewsController : Controller
	{
		private readonly IGenericRepository<Review> _reviewRepository;

		public ReviewsController(IGenericRepository<Review> reviewRepository)
		{
			_reviewRepository = reviewRepository;
		}

		
		public IActionResult Create(int? customerId, int? movieId)
		{
			ReviewVM reviewVM = new();
			reviewVM.CustomerId = customerId ?? 0;
			reviewVM.MovieId = movieId ?? 0;
			return View(reviewVM);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ReviewVM model)
		{
			Review review = new Review();
			review.MovieId = model.MovieId;
			review.CustomerId = model.CustomerId;
			review.Rating = model.Rating;
			review.Comment = model.Comment;
			await _reviewRepository.Add(review);
			await _reviewRepository.SaveAsync();
			return RedirectToAction("Index", "Movies");
		}
	}
}
