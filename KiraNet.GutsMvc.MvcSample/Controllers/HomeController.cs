using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KiraNet.GutsMvc.MvcSample.Controllers
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Countryside { get; set; }
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

        [ModelType(typeof(List<User>))]
        public IActionResult GetRazor(int Id)
        {
            return View(new List<User>() { new User(), new User { Id = "001", Name = "MVC" } });
        }

        [HttpPost]
        [HttpGet]
        [Authorize(Users = new string[] { "ZZQ" },
            Roles = new string[] { "User" })]
        public IActionResult GetRazor(User user)
        {
            string msg;
            if (ModelState.IsValid)
            {
                msg = "success";
            }
            else
            {
                msg = "failure";
            }

            return Json(new { status = msg });
        }
    }
}
