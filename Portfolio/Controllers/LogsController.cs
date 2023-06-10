using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Iservices;

namespace Portfolio.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogsServices service;
        public LogsController(ILogsServices service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View(service.getall());
        }
    }
}
