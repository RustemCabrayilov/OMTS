using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;

namespace OMTS.UI.ViewComponents
{
	public class BasketViewComponent:ViewComponent
	{
		private readonly IGenericRepository<Ticket> _ticketRepository;

		public BasketViewComponent(IGenericRepository<Ticket> ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var tickets = await GetItemsAsync();
			return View(tickets);
		}

		private async  Task<List<Ticket>> GetItemsAsync()
		{
			var customerId = int.Parse(Request.Cookies["customer_id"]);

			return  _ticketRepository.GetAll().Result.Where(x=>x.CustomerId==customerId).ToList();
		}
	}
}
