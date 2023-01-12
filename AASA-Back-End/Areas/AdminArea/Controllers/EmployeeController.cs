using AASA_Back_End.Data;
using AASA_Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASA_Back_End.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {

        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> employees = await _context.Employeees.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();
            //if (searchBy == "Position")
            //{
            //    return View(employees.Where(x => x.Position == search)
            //            .ToList().ToPagedList(page ?? 1, 3));
            //}
            //else
            //{
            //    return (IActionResult)View(employees.Where(x => x.FullName.StartsWith(search)))
            //        .ToString().ToPagedList(page ?? 1, 3)); 
            //}
            return View(employees);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStatus(int id)
        {
            Employee employee = await _context.Employeees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null) return NotFound();

            if (employee.IsActive)
            {
                employee.IsActive = false;
            }
            else
            {
                employee.IsActive = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid) return View();

            Employee newEmployee = new Employee
            {
                FullName = employee.FullName,
                Age = employee.Age,
                Position = employee.Position
            };

            await _context.Employeees.AddAsync(newEmployee);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Employee employee = await GetByIdAsync((int)id);

            if (employee == null) return NotFound();

            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Employee employee)
        {
            if (id is null) return BadRequest();

            var dbEmployee = await GetByIdAsync((int)id);

            if (dbEmployee == null) return NotFound();

            dbEmployee.FullName = employee.FullName;
            dbEmployee.Position = employee.Position;
            dbEmployee.Age = employee.Age;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Employee employee = await GetByIdAsync((int)id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = await GetByIdAsync(id);

            if (employee == null) return NotFound();


            employee.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }







        private async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employeees.FindAsync(id);
        }

    }
}
