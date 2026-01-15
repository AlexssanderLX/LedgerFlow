
using Microsoft.AspNetCore.Mvc;


namespace LedgerFlow.Controllers

{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
