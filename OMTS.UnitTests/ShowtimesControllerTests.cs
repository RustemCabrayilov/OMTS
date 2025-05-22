using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Areas.Admin.Controllers;
using OMTS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.UnitTest
{
    [TestFixture]
    public class ShowtimesControllerTests
    {
        private Mock<IGenericRepository<Showtime>> _mockShowtimeRepo;
        private Mock<IGenericRepository<Movie>> _mockMovieRepo;
        private Mock<IGenericRepository<CinemaHall>> _mockCinemaHallRepo;

        private ShowtimesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockShowtimeRepo = new Mock<IGenericRepository<Showtime>>();
            _mockMovieRepo = new Mock<IGenericRepository<Movie>>();
            _mockCinemaHallRepo = new Mock<IGenericRepository<CinemaHall>>();

            _controller = new ShowtimesController(
                _mockShowtimeRepo.Object,
                _mockMovieRepo.Object,
                _mockCinemaHallRepo.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_WhenCalled_ReturnsViewResultWithShowtimeVMList()
        {
            // Arrange
            int movieId = 1;
            int customerId = 42;

            var showtimes = new List<Showtime>
        {
            new Showtime { Id = 1, MovieId = movieId, CinemaHallId = 5, StartTime = System.DateTime.Now, EndTime = System.DateTime.Now.AddHours(2) }
        }.AsQueryable();

            var movie = new Movie { Id = movieId, Title = "Test Movie" };
            var cinemaHall = new CinemaHall { Id = 5, Name = "Main Hall" };

            _mockShowtimeRepo.Setup(r => r.GetAll()).ReturnsAsync(showtimes);
            _mockMovieRepo.Setup(r => r.Get(movieId)).ReturnsAsync(movie);
            _mockCinemaHallRepo.Setup(r => r.Get(5)).ReturnsAsync(cinemaHall);

            // Act
            var result = await _controller.Index();
            // Assert
            result.Should().BeOfType<ViewResult>();

            var viewResult = (ViewResult)result;

            viewResult.Model.Should().BeOfType<List<ShowtimeVM>>();

            var model = (List<ShowtimeVM>)viewResult.Model;

            model.Should().HaveCount(1);
            model[0].MovieTitle.Should().Be("Test Movie");
            model[0].CinemaHallName.Should().Be("Main Hall");
            model[0].CustomerId.Should().Be(customerId);
        }
    }
}
