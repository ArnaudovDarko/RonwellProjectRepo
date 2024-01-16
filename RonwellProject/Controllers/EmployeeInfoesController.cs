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
using RonwellProject.ViewModels;

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
            var employeeEntities = await _employeeService.GetEmployees();

            if(employeeEntities !=null && employeeEntities.Count > 0)
            {
                var employees = new List<EmployeeInfoVM>();

                employees = employeeEntities.Select(e => new EmployeeInfoVM
                {
                    EmployeeId= e.EmployeeId,
                    Name= e.Name,
                    Position= e.Position,
                    Salary = e.Salary   
                }).ToList();

                return View(employees);
            }

            return View();
        }

        // GET: EmployeeInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var employeeInfo = await _employeeService.GetDetails(id);

            if (employeeInfo == null)
            {
                return NotFound();
            }          

            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Create
        public IActionResult Create()
        {
            var employeeInfoVm = new EmployeeInfoVM();
            return View(employeeInfoVm);
        }

        // POST: EmployeeInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeInfoVM employeeInfoVM)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Upsert(employeeInfoVM);
                return RedirectToAction("Index");
            }
            return View(employeeInfoVM);
        }

        // GET: EmployeeInfoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeInfo = await _employeeService.GetEmployeeDetails(id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            var singleEmployeeInfo = new EmployeeInfoVM
            {
                EmployeeId = employeeInfo.EmployeeId,
                Name = employeeInfo.Name,
                Position = employeeInfo.Position,
                Salary = employeeInfo.Salary
            };

            return View(singleEmployeeInfo);
        }

        // POST: EmployeeInfoes/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        
        public async Task<IActionResult> Edit(EmployeeInfoVM employeeInfoVM)
        {
            await _employeeService.Upsert(employeeInfoVM);

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
