using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices services;
        private readonly IHttpContextAccessor session;
        public UserController(IUserServices services, IHttpContextAccessor session)
        {
            this.services = services;
            this.session = session;

        }
        public IActionResult Register()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            services.register(user);
            return Redirect("/User/Login/");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            services.login(user);
            if (session.HttpContext.Session.GetString("login") == "true")
            {
                return Redirect("/Investment/All/");
            }
            else
            {
                ModelState.AddModelError("", "pls enter valid username and password");
                return View();
            }
            
        }
        public IActionResult Logout()
        {
            services.logout();
            return Redirect("/Home/Index/");
        }
    }
}
