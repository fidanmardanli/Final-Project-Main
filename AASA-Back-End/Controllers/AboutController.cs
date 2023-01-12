using Microsoft.AspNetCore.Mvc;

namespace AASA_Back_End.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
