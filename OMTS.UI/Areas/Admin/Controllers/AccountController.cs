using Microsoft.AspNetCore.Mvc;
using OMTS.UI.Areas.Admin.Models;
using OMTS.UI.Models;

namespace OMTS.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(AccountVM model)
        {
            if(model.Email=="admin@gmail.com"&&model.Password=="admin")
            {
               return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
