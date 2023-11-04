using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Iservices;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    
    public class InvestmentController : Controller
    {
        
        private readonly IInvestmentServices services;
        private readonly IHttpContextAccessor session;
        public InvestmentController(IInvestmentServices services, IHttpContextAccessor session)
        {
            this.services = services;
            this.session = session;

        }
        
        public IActionResult All()
        {
            if (session.HttpContext.Session.GetString("login")=="true")
            {
                return View(services.getAll());
            }
            else
            {
                return Redirect("/User/Login/");
            }
        }
        public IActionResult Create()
        {
            if (session.HttpContext.Session.GetString("login") == "true")
            {
                return View();
            }
            else
            {
                return Redirect("/User/Login/");
            }
        }
        [HttpPost]
        public IActionResult Create(Investment investment)
        {
            if (session.HttpContext.Session.GetString("login") == "true")
            {
                services.add(investment);
            return Redirect("/Investment/All/");
        }
            else
            {
                return Redirect("/User/Login/");
            }
        }
        public IActionResult Delete(int id)
        {
            if (session.HttpContext.Session.GetString("login") == "true")
            {
                services.remove(id);
                return Redirect("/Investment/All/");
            }
            else
            {
                return Redirect("/User/Login/");
            }
            }
        public IActionResult Edit(int id)
        {
                if (session.HttpContext.Session.GetString("login") == "true")
                {
                    return View(services.getbyid(id));
                }
                else
                {
                    return Redirect("/User/Login/");
                }
                }
        [HttpPost]
        public IActionResult Edit(Investment investment)
        {
                if (session.HttpContext.Session.GetString("login") == "true")
                {
                    services.update(investment);
                    return Redirect("/Investment/All/");
                }
                else
                {
                    return Redirect("/User/Login/");
                }
                    }
        public IActionResult Details(int id)
        {
                if (session.HttpContext.Session.GetString("login") == "true")
                {
                    return View(services.getbyid(id));
                }
                else
                {
                    return Redirect("/User/Login/");
                }
        }
        public IActionResult Restore(int id)
        {
            services.restore(id);
            return Redirect("/Logs/Index/");
        }

        public IActionResult Download()
        {
            var excelFileBytes= services.downloadDetails();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Investments.xlsx";
            return File(excelFileBytes, contentType, fileName);
        }
    }
}
