using AASA_Back_End.Data;
using AASA_Back_End.Helpers.Enums;
using AASA_Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASA_Back_End.Areas.AdminArea.Controllers
{
    // GET: PortfolioController
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
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

        //slider'in ichindeki create button'u
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }



        //slider'in ichindeki create button'un ichindeki create button'u
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            } // bosh olub olmadigin check eleyir



            //eyni adda description olub olmadigin check eleyir


            bool isExist = await _context.Portfolios.AnyAsync(m => m.TitleFirst.Trim() == portfolio.TitleFirst.Trim());
            if (isExist)
            {
                ModelState.AddModelError("TitleFirst", "Description already exists");
                return View();
            }


            //save edir
            //await _context.Sliders.AddAsync(slider);

            await _context.Portfolios.AddAsync(portfolio);

            //await _context.AddRangeAsync(portfolio);
            await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
