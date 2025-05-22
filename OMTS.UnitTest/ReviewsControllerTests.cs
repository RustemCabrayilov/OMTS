using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Areas.Admin.Controllers;
using OMTS.UI.Models;


namespace OMTS.UnitTest
{
    [TestFixture]
    public class ReviewsControllerTests
    {
        public Mock<IGenericRepository<Review>> _mockRepo;
        private ReviewsController _reviewsController;
        private Mock<IGenericRepository<Review>> _mockReviewRepo;
        private Mock<IGenericRepository<Movie>> _mockMovieRepo;
        private Mock<IGenericRepository<Customer>> _mockCustomerRepo;


        [SetUp]
        public void Setup()
        {
            _mockReviewRepo = new Mock<IGenericRepository<Review>>();
            _mockMovieRepo = new Mock<IGenericRepository<Movie>>();
            _mockCustomerRepo = new Mock<IGenericRepository<Customer>>();

           var _reviewsController = new ReviewsController(
                _mockReviewRepo.Object,
                _mockMovieRepo.Object,
                _mockCustomerRepo.Object
            );

            var context = new DefaultHttpContext();
            context.Request.Headers["Cookie"] = "customer_id=1";

            _reviewsController.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
        }


        [Test]
        public async Task Delete_ValidCustomer_DeletesReview()
        {
            var review = new Review { Id = 123, CustomerId = 1 };

            _mockRepo.Setup(r => r.Get(123)).ReturnsAsync(review);

            var result = await _reviewsController.Delete(123) as RedirectToActionResult;

            _mockRepo.Verify(r => r.Delete(123), Times.Once);
            Assert.That(result?.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Delete_InvalidCustomer_DoesNotDeleteReview()
        {
            var review = new Review { Id = 123, CustomerId = 99 };

            _mockRepo.Setup(r => r.Get(123)).ReturnsAsync(review);

            var result = await _reviewsController.Delete(123) as RedirectToActionResult;

            _mockRepo.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
            Assert.That(result?.ActionName, Is.EqualTo("Index"));
        }
    }
}