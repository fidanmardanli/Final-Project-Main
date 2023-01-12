using AASA_Back_End.Data;
using AASA_Back_End.Helpers;
using AASA_Back_End.Helpers.Enums;
using AASA_Back_End.Models;
using AASA_Back_End.ViewModel.ProductViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASA_Back_End.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> Index(/*int page = 1, int take = 5*/)
        //{
        //    IEnumerable<Product> products = await _context.Products
        //        //.Skip((page*take) - take)
        //        //.Take(take)
        //        .ToListAsync();



        //    return View(products);

        //}

        public async Task<IActionResult> Index(int page = 1, int take = 4)
        {
            List<Product> products = await _context.Products
                .Where(m => !m.IsDeleted)
                .Skip((page * take) - take)
                .Take(take).ToListAsync();

            List<ProductListVM> mapDatas = GetMapDatas(products);

            int count = await GetPageCount(take);

            Paginate<ProductListVM> result = new Paginate<ProductListVM>(mapDatas, page, count);


            return View(result);




        }

        private List<ProductListVM> GetMapDatas(List<Product> products)
        {
            List<ProductListVM> productList = new List<ProductListVM>();
            foreach (var product in products)
            {
                ProductListVM newProduct = new ProductListVM
                {
                    Id= product.Id,
                    Title = product.Title,
                    Image = product.Image,
                };
                productList.Add(newProduct);
            }
            return productList;
        }














        private async Task<int> GetPageCount(int take)
        {
            int productCount = await _context.Products.Where(m=> !m.IsDeleted).CountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View();

            Product newProduct = new Product
            {
                Title = product.Title,
                Image= product.Image,              
            };

            await _context.Products.AddAsync(newProduct);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await GetByIdAsync((int)id);

            if (product == null) return NotFound();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            if (id is null) return BadRequest();

            var dbProduct = await GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            dbProduct.Title = product.Title;
            dbProduct.Image = product.Image;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await GetByIdAsync((int)id);

            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await GetByIdAsync(id);

            if (product == null) return NotFound();


            product.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



      



        private async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
