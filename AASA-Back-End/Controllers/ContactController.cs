using Microsoft.AspNetCore.Mvc;

namespace AASA_Back_End.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
