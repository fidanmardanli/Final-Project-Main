using Microsoft.AspNetCore.Mvc;
using AASA_Back_End.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AASA_Back_End.Models;
using AASA_Back_End.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AASA_Back_End.Controllers
{
  
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("name", "Fidan");

            //Response.Cookies.Append("surname", "Mardanli");

            Slider slider = _context.Sliders.FirstOrDefault();

       


            //HomeVM model = new HomeVM
            //{
            //    Sliders = slider,

            //    Categories = category
              
            //};
          

            return View(slider);
        }

        //public IActionResult Test()
        //{
        //    var sessionData = HttpContext.Session.GetString("name");

        //    var cookieData = Request.Cookies["surname"];

        //    return Json(sessionData + "-" + cookieData);
        //}
        

        public IActionResult Search(string search) 
        {
            List<Product> searchProduct = _context.Products.Include(m => m.Image)
                .Where(m => m.Title.ToLower()
            .Contains(search.ToLower()) && !m.IsDeleted).ToList();

            ShopVM model = new ShopVM
            {
                Products = searchProduct,
            };

            return View(model);

        }
    }
}
