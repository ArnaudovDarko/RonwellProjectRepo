using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RonwellProject.Helpers;
using RonwellProject.Interface;
using RonwellProject.Models;

namespace RonwellProject.Controllers
{
    public class EmployeeInfoesController : Controller
    {
        private readonly DataContext _context;
        private IEmployeeServices _employeeService;

        public EmployeeInfoesController(DataContext context, IEmployeeServices employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: EmployeeInfoes
        public async Task<IActionResult> Index()
        {
              return _context.Employees != null ? 
                          View(await _context.Employees.ToListAsync()) :
                          Problem("Entity set 'DataContext.Employees'  is null.");
        }

        // GET: EmployeeInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var employeeInfo = await _employeeService.Details(id);

            if (employeeInfo == null)
            {
                return NotFound();
            }

           

            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Name,Position,Salary")] EmployeeInfo employeeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.Employees.FindAsync(id);
            if (employeeInfo == null)
            {
                return NotFound();
            }
            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Name,Position,Salary")] EmployeeInfo employeeInfo)
        {
            if (id != employeeInfo.EmployeeId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                await _employeeService.Edit(id, employeeInfo);
                 return RedirectToAction(nameof(Index));
            }
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DataContext.Employees'  is null.");
            }
            var employeeInfo = await _context.Employees.FindAsync(id);
            if (employeeInfo != null)
            {
                _context.Employees.Remove(employeeInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
