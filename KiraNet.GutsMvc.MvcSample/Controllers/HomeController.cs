using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.MvcSample.Controllers
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index(int id)
        {
            var model = new { Name = "zhang", Id = "1501332303" };
            ViewData["Date"] = DateTime.Now.ToShortDateString();
            return View(model);
        }

        public IActionResult User(int Id, User user)
        {
            return View(user);
        }

        public IActionResult Test(User user)
        {
            return View(user);
        }

        public IActionResult GetTest(User user)
        {
            return Json(user);
        }

        [ModelType(typeof(User))]
        public IActionResult GetRazor(int Id)
        {
            return View(new User { Id = "001", Name = "MVC" });
        }
    }
}
