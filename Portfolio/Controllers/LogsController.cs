using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Iservices;

namespace Portfolio.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogsServices service;
        private readonly IHttpContextAccessor session;
        public LogsController(ILogsServices service, IHttpContextAccessor session)
        {
            this.service = service;
            this.session = session;

        }
        public IActionResult Index()
        {
            if (this.session != null)
            {
                return View(service.getall());
            }
            else
            {
                return Redirect("/User/Login/");
            }
        }
    }
}
