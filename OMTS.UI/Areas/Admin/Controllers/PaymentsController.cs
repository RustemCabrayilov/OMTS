using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PaymentsController : Controller
    {
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IGenericRepository<Movie> _movieRepository;

        public PaymentsController(IGenericRepository<Payment> paymentRepository,
            IGenericRepository<Movie> movieRepository)
        {
            _paymentRepository = paymentRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _paymentRepository.GetAll();
            List<PaymentVM> list = new();
            foreach (var payment in payments)
            {
                list.Add(new PaymentVM
                {
                    Id = payment.Id,
                    PaymentDate = payment.PaymentDate,
                    PaymentMethod = payment.PaymentMethod,
                    TicketId = payment.TicketId,

                });
            }
            return View(list);
        }
    }
}
