using AASA_Back_End.Data;
using AASA_Back_End.Models;
using AASA_Back_End.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AASA_Back_End.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            

            Portfolio portfolio = _context.Portfolios.FirstOrDefault();

            return View(portfolio);
        }
    }
}
