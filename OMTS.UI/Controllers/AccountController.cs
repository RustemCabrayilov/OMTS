using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Areas.Admin.Models;

namespace OMTS.UI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IGenericRepository<Customer> _customerRepository;

		public AccountController(IGenericRepository<Customer> customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public IActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> LogIn(Account model)
		{
			var cutomers = await _customerRepository.GetAll();
			foreach (var customer in cutomers)
			{
				if (model.Email == customer.Email)
				{
					string key = "customer_id";
					string value = customer.Id.ToString();
					CookieOptions options = new CookieOptions
					{
						Expires = DateTime.Now.AddDays(5)
					};
					Response.Cookies.Append(key,value,options);
					return RedirectToAction("Index", "Movies", new {customerId=customer.Id});
				}
			}
			return View(model);
		}
	}
}
