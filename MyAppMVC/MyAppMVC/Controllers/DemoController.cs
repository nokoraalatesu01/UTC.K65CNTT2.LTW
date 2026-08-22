using Microsoft.AspNetCore.Mvc;

namespace MyAppMVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
