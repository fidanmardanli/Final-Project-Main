using AASA_Back_End.Data;
using AASA_Back_End.Helpers.Enums;
using AASA_Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASA_Back_End.Areas.AdminArea.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("AdminArea")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Slider slider = _context.Sliders.FirstOrDefault();

            return View(slider);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = await GetByIdAsync((int)id);

            if (slider == null) return NotFound();

            return View(slider);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            var dbslider = await GetByIdAsync((int)id);
            if (dbslider == null) return NotFound();

            
            return View();

        }


        private async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
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
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            } // bosh olub olmadigin check eleyir



            //eyni adda description olub olmadigin check eleyir


            bool isExist = await _context.Sliders.AnyAsync(m => m.DescriptionFirst.Trim() == slider.DescriptionFirst.Trim());
            if (isExist)
            {
                ModelState.AddModelError("DescriptionFirst", "Description already exists");
                return View();
            }


            //save edir
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
