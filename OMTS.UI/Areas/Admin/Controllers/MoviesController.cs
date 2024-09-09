using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NToastNotify;
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
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IFileProvider _fileProvider;
		private readonly IToastNotification _toastNotification;
		public MoviesController(IGenericRepository<Movie> movieRepository, IWebHostEnvironment webHostEnvironment, IFileProvider fileProvider, IToastNotification toastNotification)
		{
			_movieRepository = movieRepository;
			_webHostEnvironment = webHostEnvironment;
			_fileProvider = fileProvider;
			_toastNotification = toastNotification;
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
					Thumbnail = movie.Thumbnail,
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
			movie.Thumbnail = UploadImage(model.Image);
			await _movieRepository.Add(movie);
			await _movieRepository.SaveAsync();
			_toastNotification.AddErrorToastMessage("sj xkjs");
			return RedirectToAction("Index");
		}
		public string UploadImage(IFormFile file)
		{
			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			string localPath = _webHostEnvironment.WebRootPath;
			string filePath = Path.Combine(localPath, "Images", fileName);
			using (var localFile = System.IO.File.OpenWrite(filePath))
			using (var uploadedFile = file.OpenReadStream())
			{
				uploadedFile.CopyTo(localFile);
			}
			return fileName;
		}
		public async Task<IActionResult> Edit(int id)
		{
			var movie = await _movieRepository.Get(id);
			MovieVM movieVM = new MovieVM();
			movieVM.Title = movie.Title;
			movieVM.Genre = movie.Genre;
			movieVM.Director = movie.Director;
			movieVM.Duration = movie.Duration;
			movieVM.Thumbnail = movie.Thumbnail;
			movieVM.ReleaseDate = movie.ReleaseDate;
			return View(movieVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(MovieVM model, int id)
		{
			/*Movie movie = new();*/ 
			var movie = await _movieRepository.Get(id);
			movie.Title = model.Title;
			movie.Genre = model.Genre;
			movie.Director = model.Director;
			movie.Duration = model.Duration;
			movie.ReleaseDate = model.ReleaseDate;

			if (model.Image != null)
			{
				DeleteImage(movie.Thumbnail);
				movie.Thumbnail = UploadImage(model.Image);
			}
			_movieRepository.Update(movie);
			await _movieRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public void DeleteImage(string fileName)
		{
			string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
		}
		public async Task<ActionResult> Delete(int id)
		{
			var movie = await _movieRepository.Get(id);
			MovieVM movieVM = new MovieVM
			{
				Id = movie.Id,
				Title = movie.Title,
				Genre = movie.Genre,
				Director = movie.Director,
				Duration = movie.Duration,
				Thumbnail = movie.Thumbnail,
				ReleaseDate = movie.ReleaseDate
			};

			return View(movieVM);
		}
		[HttpPost]
		public async Task<ActionResult> Delete(MovieVM model)
		{

			_movieRepository.Delete(model.Id);
			await _movieRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
