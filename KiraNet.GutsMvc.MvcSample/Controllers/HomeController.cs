using System.Collections.Generic;

namespace KiraNet.GutsMVC.MvcSample.Controllers
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

            ViewData["User"] = "ZZQ";
            return View(model);
        }

        public IActionResult User(int Id, User user)
        {

            return View(user);
        }

    }
}
