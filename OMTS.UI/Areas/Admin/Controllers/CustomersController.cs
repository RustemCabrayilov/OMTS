using Microsoft.AspNetCore.Mvc;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
	[Area("admin")]
	public class CustomersController : Controller
	{
		private readonly IGenericRepository<Customer> _customerRepository;

		public CustomersController(IGenericRepository<Customer> customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<IActionResult> Index()
		{
			List<CustomerVM> list = new();
			var customers = await _customerRepository.GetAll();
			foreach (var customer in customers)
			{
				list.Add(new CustomerVM {
				Id = customer.Id,
				Name = customer.UserName,
				Email = customer.Email,
				PhoneNumber = customer.PhoneNumber,
				});
			}
			return View(list);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CustomerVM model)
		{
			Customer customer = new();
			customer.UserName = model.Name;
			customer.Email = model.Email;
			customer.PhoneNumber = model.PhoneNumber;
			await _customerRepository.Add(customer);
			await _customerRepository.SaveAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var customer = await _customerRepository.Get(id);
			CustomerVM customerVM = new();
			customerVM.Id = customer.Id;
			customerVM.Name = customer.UserName;
			customerVM.Email = customer.Email;
			customerVM.PhoneNumber = customer.PhoneNumber;
			return View(customerVM);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(CustomerVM model)
		{
			_customerRepository.Delete(model.Id);
			await _customerRepository.SaveAsync();
			return RedirectToAction("Index");
		}
	}
}
