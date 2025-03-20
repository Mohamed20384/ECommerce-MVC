using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
